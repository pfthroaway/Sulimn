using System;
using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow : Window, INotifyPropertyChanged
    {
        private string previousWindow;
        private string nl = Environment.NewLine;
        private bool battleEnded = false;
        private int _heroShield;
        private Spell currentSpell = new Spell();

        private enum BattleAction { Attack, Flee, Cast }

        private BattleAction _heroAction;
        private BattleAction _enemyAction;

        #region Properties

        internal ExploreWindow RefToExploreWindow { get; set; }

        internal FieldsWindow RefToFieldsWindow { get; set; }

        internal ForestWindow RefToForestWindow { get; set; }

        internal CathedralWindow RefToCathedralWindow { get; set; }

        internal MinesWindow RefToMinesWindow { get; set; }

        internal CatacombsWindow RefToCatacombsWindow { get; set; }

        public int HeroShield
        {
            get { return _heroShield; }
            set { _heroShield = value; OnPropertyChanged("HeroShieldToString"); }
        }

        public string HeroShieldToString
        {
            get { return "Shield: " + _heroShield; }
        }

        #endregion Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Binds text to the labels.
        /// </summary>
        internal void BindLabels()
        {
            lblCharName.DataContext = GameState.CurrentHero;
            lblCharHealth.DataContext = GameState.CurrentHero.Statistics;
            lblCharMagic.DataContext = GameState.CurrentHero.Statistics;
            lblShield.DataContext = this;
            lblEnemyName.DataContext = GameState.CurrentEnemy;
            lblEnemyHealth.DataContext = GameState.CurrentEnemy.Statistics;
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        /// <summary>
        /// Adds text to the txtBattle TextBox.
        /// </summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtBattle.Text += nl + nl + newText;
            txtBattle.Focus();
            txtBattle.CaretIndex = txtBattle.Text.Length;
            txtBattle.ScrollToEnd();
        }

        #region Battle Management

        /// <summary>
        /// Sets up the battle engine.
        /// </summary>
        /// <param name="Hero">Current Hero</param>
        /// <param name="enemy">Current Enemy</param>
        internal void PrepareBattle(string prevWindow)
        {
            BindLabels();
            previousWindow = prevWindow;

            txtBattle.Text = "You encounter an enemy. The " + GameState.CurrentEnemy.Name + " seems openly hostile to you. Prepare to defend yourself.";
        }

        /// <summary>
        /// Ends the battle and allows the user to exit the Window.
        /// </summary>
        private void EndBattle()
        {
            battleEnded = true;
            btnReturn.IsEnabled = true;
        }

        #endregion Battle Management

        #region Battle Logic

        /// <summary>
        /// Starts a new round of battle.
        /// </summary>
        /// <param name="heroAction">Action the hero chose to perform this round</param>
        private void NewRound(BattleAction heroAction)
        {
            DisableButtons();
            _heroAction = heroAction;

            // if Hero Dexterity is greater
            // chance to attack first is between 51 and 90%
            // chance to hit is 50 + Hero Dexterity - Enemy Dexterity
            // chance for Enemy to hit is 50 - Hero Dexterity + Enemy Dexterity
            // 10% chance to hit/miss no matter how big the difference is between the two

            int chanceHeroAttacksFirst;

            if (GameState.CurrentHero.Attributes.Dexterity > GameState.CurrentEnemy.Attributes.Dexterity)
                chanceHeroAttacksFirst = Functions.GenerateRandomNumber(51, 90);
            else
                chanceHeroAttacksFirst = Functions.GenerateRandomNumber(10, 49);

            int attacksFirst = Functions.GenerateRandomNumber(1, 100);

            if (attacksFirst <= chanceHeroAttacksFirst)
            {
                HeroTurn();
                if (GameState.CurrentEnemy.Statistics.CurrentHealth > 0 && !battleEnded)
                {
                    EnemyTurn();
                    if (GameState.CurrentHero.Statistics.CurrentHealth <= 0)
                        Fairy();
                }
            }
            else
            {
                EnemyTurn();
                if (GameState.CurrentHero.Statistics.CurrentHealth > 0 && !battleEnded)
                    HeroTurn();
                else if (GameState.CurrentHero.Statistics.CurrentHealth <= 0)
                    Fairy();
            }

            CheckButtons();
        }

        /// <summary>
        /// The Hero's turn this round of battle.
        /// </summary>
        private void HeroTurn()
        {
            switch (_heroAction)
            {
                case BattleAction.Attack:
                    if (GameState.CurrentHero.Equipment.Weapon.WeaponType == WeaponTypes.Melee)
                        HeroAttack(GameState.CurrentHero.Attributes.Strength + GameState.CurrentHero.Equipment.BonusStrength, GameState.CurrentHero.Equipment.TotalDamage);
                    else if (GameState.CurrentHero.Equipment.Weapon.WeaponType == WeaponTypes.Ranged)
                        HeroAttack(GameState.CurrentHero.Attributes.Dexterity + GameState.CurrentHero.Equipment.BonusDexterity, GameState.CurrentHero.Equipment.TotalDamage);
                    break;

                case BattleAction.Cast:

                    AddTextTT("You cast " + currentSpell.Name + ".");

                    if (currentSpell.Type == SpellTypes.Damage)
                        HeroAttack(GameState.CurrentHero.Attributes.Wisdom + GameState.CurrentHero.Equipment.BonusWisdom, currentSpell.Amount);
                    else if (currentSpell.Type == SpellTypes.Healing)
                        AddTextTT(GameState.CurrentHero.Heal(currentSpell.Amount));
                    else if (currentSpell.Type == SpellTypes.Shield)
                    {
                        HeroShield += currentSpell.Amount;
                        AddTextTT("You now have a magical shield which will help protect you from " + HeroShield + " damage.");
                    }
                    else
                    {
                        //FUTURE SPELL TYPES
                    }

                    GameState.CurrentHero.Statistics.CurrentMagic -= currentSpell.MagicCost;
                    break;

                case BattleAction.Flee:

                    if (FleeAttempt(GameState.CurrentHero.Attributes.Dexterity + GameState.CurrentHero.Equipment.BonusDexterity, GameState.CurrentEnemy.Attributes.Dexterity + GameState.CurrentEnemy.Equipment.BonusDexterity))
                    {
                        EndBattle();
                        AddTextTT("You successfully fled from the " + GameState.CurrentEnemy.Name + ".");
                    }
                    else
                        AddTextTT("The " + GameState.CurrentEnemy.Name + " blocked your attempt to flee.");
                    break;
            }
        }

        /// <summary>
        /// The Hero attacks the Enemy.
        /// </summary>
        /// <param name="statModifier">Stat to be given 20% bonus to damage</param>
        /// <param name="damage">Damage</param>
        private void HeroAttack(int statModifier, int damage)
        {
            int chanceHeroHits;
            chanceHeroHits = Functions.GenerateRandomNumber(50 + GameState.CurrentHero.Attributes.Dexterity - GameState.CurrentEnemy.Attributes.Dexterity, 90, 10, 90);
            int heroHits = Functions.GenerateRandomNumber(10, 90);

            if (heroHits <= chanceHeroHits)
            {
                int maximumHeroDamage = Int32Helper.Parse(statModifier * 0.2 + damage);
                int maximumEnemyAbsorb = GameState.CurrentEnemy.Equipment.TotalDefense;

                int actualDamage = Functions.GenerateRandomNumber(maximumHeroDamage / 10, maximumHeroDamage, 1, int.MaxValue);
                int actualAbsorb = Functions.GenerateRandomNumber(maximumEnemyAbsorb / 10, maximumEnemyAbsorb);

                string absorb = "";
                if (actualAbsorb > 0)
                    absorb = "The " + GameState.CurrentEnemy.Name + "'s armor absorbed " + actualAbsorb + " damage. ";

                if (actualDamage > actualAbsorb)
                {
                    AddTextTT("You attack for " + actualDamage + " damage. " + absorb + GameState.CurrentEnemy.TakeDamage(actualDamage - actualAbsorb));
                    if (GameState.CurrentEnemy.Statistics.CurrentHealth <= 0)
                    {
                        EndBattle();
                        AddTextTT(GameState.CurrentHero.GainExperience(GameState.CurrentEnemy.Experience));
                        if (GameState.CurrentEnemy.Inventory.Gold > 0)
                            AddTextTT("You find " + GameState.CurrentEnemy.Inventory.Gold + " gold on the body.");
                        GameState.CurrentHero.Inventory.Gold += GameState.CurrentEnemy.Inventory.Gold;
                    }
                }
                else
                    AddTextTT("You attack for " + actualDamage + ", but its armor absorbs all of it.");
            }
            else
                AddTextTT("You miss.");
        }

        /// <summary>
        /// Sets the current Spell.
        /// </summary>
        /// <param name="spell">Spell to be set</param>
        internal void SetSpell(Spell spell)
        {
            currentSpell = spell;
            NewRound(BattleAction.Cast);
        }

        /// <summary>
        /// Sets the Enemy's action for the round.
        /// </summary>
        private void SetEnemyAction()
        {
            _enemyAction = BattleAction.Attack;
            if (GameState.CurrentHero.Level - GameState.CurrentEnemy.Level >= 20)
            {
                int flee = Functions.GenerateRandomNumber(1, 100);
                if (flee <= 5)
                    _enemyAction = BattleAction.Flee;
            }
            else if (GameState.CurrentHero.Level - GameState.CurrentEnemy.Level >= 10)
            {
                int flee = Functions.GenerateRandomNumber(1, 100);
                if (flee <= 2)
                    _enemyAction = BattleAction.Flee;
            }
            if (_enemyAction != BattleAction.Flee)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (GameState.CurrentEnemy.Statistics.CurrentHealth > (GameState.CurrentEnemy.Statistics.MaximumHealth / 3))
                    _enemyAction = BattleAction.Attack;
                else if (GameState.CurrentEnemy.Statistics.CurrentHealth > (GameState.CurrentEnemy.Statistics.MaximumHealth / 5))
                {
                    if (result <= 98)
                        _enemyAction = BattleAction.Attack;
                    else
                        _enemyAction = BattleAction.Flee;
                }
                else
                {
                    if (result <= 95)
                        _enemyAction = BattleAction.Attack;
                    else
                        _enemyAction = BattleAction.Flee;
                }
            }
        }

        /// <summary>
        /// The Enemy's turn this round of battle.
        /// </summary>
        private void EnemyTurn()
        {
            SetEnemyAction();
            switch (_enemyAction)
            {
                case BattleAction.Attack:
                    if (GameState.CurrentEnemy.Equipment.Weapon.WeaponType == WeaponTypes.Melee)
                        EnemyAttack(GameState.CurrentEnemy.Attributes.Strength + GameState.CurrentEnemy.Equipment.BonusStrength, GameState.CurrentEnemy.Equipment.TotalDamage);
                    else if (GameState.CurrentEnemy.Equipment.Weapon.WeaponType == WeaponTypes.Ranged)
                        EnemyAttack(GameState.CurrentEnemy.Attributes.Dexterity + GameState.CurrentEnemy.Equipment.BonusDexterity, GameState.CurrentEnemy.Equipment.TotalDamage);
                    break;

                case BattleAction.Flee:
                    if (FleeAttempt(GameState.CurrentEnemy.Attributes.Dexterity + GameState.CurrentEnemy.Equipment.BonusDexterity, GameState.CurrentHero.Attributes.Dexterity + GameState.CurrentHero.Equipment.BonusDexterity))
                    {
                        EndBattle();
                        AddTextTT("The " + GameState.CurrentEnemy.Name + " fled from the battle.");
                        AddTextTT(GameState.CurrentHero.GainExperience(GameState.CurrentEnemy.Experience / 2));
                    }
                    else
                        AddTextTT("You block the " + GameState.CurrentEnemy.Name + "'s attempt to flee.");
                    break;
            }
        }

        /// <summary>
        /// The Enemy attacks the Hero.
        /// </summary>
        private void EnemyAttack(int statModifier, int damage)
        {
            int chanceHeroHits;
            chanceHeroHits = Functions.GenerateRandomNumber(50 + GameState.CurrentHero.Attributes.Dexterity - GameState.CurrentEnemy.Attributes.Dexterity, 90, 10, 90);
            int heroHits = Functions.GenerateRandomNumber(10, 90);

            if (heroHits <= chanceHeroHits)
            {
                int maximumDamage = Int32Helper.Parse(GameState.CurrentEnemy.Attributes.Strength * 0.2 + GameState.CurrentEnemy.Equipment.Weapon.Damage);
                int HeroDefense = GameState.CurrentHero.Equipment.TotalDefense;
                int actualDamage = Functions.GenerateRandomNumber(maximumDamage / 10, maximumDamage, 1, int.MaxValue);
                int maximumShieldAbsorb = Functions.GenerateRandomNumber(HeroShield / 10, HeroShield);
                int maximumArmorAbsorb = Functions.GenerateRandomNumber(HeroDefense / 10, HeroDefense);
                int actualShieldAbsorb = 0;
                int actualArmorAbsorb = 0;

                // shield absorbs actualDamage up to maxShieldAbsorb
                if (maximumShieldAbsorb >= actualDamage)
                    actualShieldAbsorb = actualDamage;
                else
                    actualShieldAbsorb = maximumShieldAbsorb;

                HeroShield -= actualShieldAbsorb;

                // if shield absorbs all damage, actualArmorAbsorb is 0, otherwise check actualDamage - maxShieldAbsorb
                if (actualShieldAbsorb < actualDamage)
                {
                    if (maximumArmorAbsorb >= (actualDamage - actualShieldAbsorb))
                        actualArmorAbsorb = actualDamage - actualShieldAbsorb;
                    else
                        actualArmorAbsorb = maximumArmorAbsorb;
                }

                string absorb = "";
                string shield = "";

                if (actualShieldAbsorb > 0)
                    shield = " Your magical shield absorbs " + actualShieldAbsorb + " damage.";

                if (actualArmorAbsorb > 0)
                    absorb = " Your armor absorbs " + actualArmorAbsorb + " damage. ";

                if (actualDamage > (actualShieldAbsorb + actualArmorAbsorb)) //the player actually takes damage
                    AddTextTT("The " + GameState.CurrentEnemy.Name + " attacks you for " + actualDamage + " damage. " + shield + absorb + GameState.CurrentHero.TakeDamage(actualDamage - actualShieldAbsorb - actualArmorAbsorb));
                else
                {
                    if (actualShieldAbsorb > 0 && actualArmorAbsorb > 0)
                        AddTextTT("The " + GameState.CurrentEnemy.Name + " attacks you for " + actualDamage + ", but" + shield.ToLower() + absorb.ToLower());
                    else if (actualDamage == actualShieldAbsorb)
                        AddTextTT("The " + GameState.CurrentEnemy.Name + " attacks you for " + actualDamage + ", but your shield absorbed all of it.");
                    else
                        AddTextTT("The " + GameState.CurrentEnemy.Name + " attacks you for " + actualDamage + ", but your armor absorbed all of it.");
                }
            }
            else
                AddTextTT("The " + GameState.CurrentEnemy.Name + " misses.");
        }

        /// <summary>
        /// Determines whether a flight attempt is successful.
        /// </summary>
        /// <param name="fleeAttemptDexterity">Whoever is attempting to flee's Dexterity</param>
        /// <param name="blockAttemptDexterity">Whoever is not attempting to flee's Dexterity</param>
        /// <returns></returns>
        private bool FleeAttempt(int fleeAttemptDexterity, int blockAttemptDexterity)
        {
            int chanceToFlee = Functions.GenerateRandomNumber(20 + fleeAttemptDexterity - blockAttemptDexterity, 90, 1, 90);
            int flee = Functions.GenerateRandomNumber(1, 100);

            if (flee <= chanceToFlee)
                return true;
            return false;
        }

        /// <summary>
        /// A fairy is summoned to resurrect the Hero.
        /// </summary>
        private void Fairy()
        {
            EndBattle();
            AddTextTT("A mysterious fairy appears, and, seeing your crumpled body on the ground, resurrects you. You have just enough health to make it back to town.");
            GameState.CurrentHero.Statistics.CurrentHealth = 1;
        }

        #endregion Battle Logic

        #region Button Management

        /// <summary>Checks whether to enable/disable battle buttons.</summary>
        private void CheckButtons()
        {
            if (!battleEnded)
                EnableButtons();
            else
                DisableButtons();
        }

        /// <summary>Disables battle buttons on Window</summary>
        private void DisableButtons()
        {
            btnAttack.IsEnabled = false;
            btnCastSpell.IsEnabled = false;
            btnFlee.IsEnabled = false;
        }

        /// <summary>Enables battle buttons on Window</summary>
        private void EnableButtons()
        {
            btnAttack.IsEnabled = true;
            btnCastSpell.IsEnabled = true;
            btnFlee.IsEnabled = true;
        }

        #endregion Button Management

        #region Button-Click Methods

        private void btnAttack_Click(object sender, RoutedEventArgs e)
        {
            NewRound(BattleAction.Attack);
        }

        private void btnCharDetails_Click(object sender, RoutedEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow();
            characterWindow.RefToBattleWindow = this;
            characterWindow.SetupChar();
            characterWindow.BindLabels();
            characterWindow.SetPreviousWindow("Battle");
            characterWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void btnEnemyDetails_Click(object sender, RoutedEventArgs e)
        {
            EnemyDetailsWindow enemyDetailsWindow = new EnemyDetailsWindow();
            enemyDetailsWindow.RefToBattleWindow = this;
            enemyDetailsWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCastSpell_Click(object sender, RoutedEventArgs e)
        {
            CastSpellWindow castSpellWindow = new CastSpellWindow();
            castSpellWindow.RefToBattleWindow = this;
            castSpellWindow.LoadWindow("Battle");
            castSpellWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void btnFlee_Click(object sender, RoutedEventArgs e)
        {
            NewRound(BattleAction.Flee);
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        public BattleWindow()
        {
            InitializeComponent();
        }

        private void windowBattle_Closing(object sender, CancelEventArgs e)
        {
            GameState.SaveHero(GameState.CurrentHero);
            if (battleEnded)
            {
                switch (previousWindow)
                {
                    case "Explore":
                        RefToExploreWindow.Show();
                        RefToExploreWindow.CheckButtons();
                        break;

                    case "Fields":
                        RefToFieldsWindow.Show();
                        break;

                    case "Forest":
                        RefToForestWindow.Show();
                        break;

                    case "Cathedral":
                        RefToCathedralWindow.Show();
                        break;

                    case "Mines":
                        RefToMinesWindow.Show();
                        break;

                    case "Catacombs":
                        RefToCatacombsWindow.Show();
                        break;
                }
            }
            else
                e.Cancel = true;
        }

        #endregion Window-Manipulation Methods
    }
}