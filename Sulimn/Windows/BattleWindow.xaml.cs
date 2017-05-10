using Extensions;
using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>Interaction logic for BattleWindow.xaml</summary>
    public partial class BattleWindow : INotifyPropertyChanged
    {
        private BattleAction _enemyAction, _heroAction;
        private int _heroShield;
        private bool _battleEnded;
        private Spell _currentSpell = new Spell();
        private string _previousWindow;
        private bool _blnHardcoreDeath = false;

        private enum BattleAction
        {
            Attack,
            Flee,
            Cast
        }

        #region Properties

        internal ExploreWindow RefToExploreWindow { get; set; }

        internal FieldsWindow RefToFieldsWindow { private get; set; }

        internal ForestWindow RefToForestWindow { private get; set; }

        internal CathedralWindow RefToCathedralWindow { private get; set; }

        internal MinesWindow RefToMinesWindow { private get; set; }

        internal CatacombsWindow RefToCatacombsWindow { private get; set; }

        public int HeroShield
        {
            get => _heroShield;
            set
            {
                _heroShield = value;
                OnPropertyChanged("HeroShieldToString");
            }
        }

        public string HeroShieldToString => $"Shield: {_heroShield}";

        #endregion Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Binds text to the labels.</summary>
        private void BindLabels()
        {
            LblCharName.DataContext = GameState.CurrentHero;
            LblCharHealth.DataContext = GameState.CurrentHero.Statistics;
            LblCharMagic.DataContext = GameState.CurrentHero.Statistics;
            LblShield.DataContext = this;
            LblEnemyName.DataContext = GameState.CurrentEnemy;
            LblEnemyHealth.DataContext = GameState.CurrentEnemy.Statistics;
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Battle Management

        /// <summary>Sets up the battle engine.</summary>
        /// <param name="prevWindow">Previous Window</param>
        internal void PrepareBattle(string prevWindow)
        {
            BindLabels();
            _previousWindow = prevWindow;

            TxtBattle.Text = $"You encounter an enemy. The {GameState.CurrentEnemy.Name} seems openly hostile to you. Prepare to defend yourself.";
        }

        /// <summary>Ends the battle and allows the user to exit the Window.</summary>
        private void EndBattle()
        {
            _battleEnded = true;
            BtnReturn.IsEnabled = true;
        }

        #endregion Battle Management

        #region Battle Logic

        /// <summary>Starts a new round of battle.</summary>
        /// <param name="heroAction">Action the hero chose to perform this round</param>
        private void NewRound(BattleAction heroAction)
        {
            ToggleButtons(false);
            _heroAction = heroAction;

            // if Hero Dexterity is greater
            // chance to attack first is between 51 and 90%
            // chance to hit is 50 + Hero Dexterity - Enemy Dexterity
            // chance for Enemy to hit is 50 - Hero Dexterity + Enemy Dexterity
            // 10% chance to hit/miss no matter how big the difference is between the two

            int chanceHeroAttacksFirst = GameState.CurrentHero.Attributes.Dexterity > GameState.CurrentEnemy.Attributes.Dexterity ? Functions.GenerateRandomNumber(51, 90) : Functions.GenerateRandomNumber(10, 49);
            int attacksFirst = Functions.GenerateRandomNumber(1, 100);

            if (attacksFirst <= chanceHeroAttacksFirst)
            {
                HeroTurn();
                if (GameState.CurrentEnemy.Statistics.CurrentHealth > 0 && !_battleEnded)
                {
                    EnemyTurn();
                    if (GameState.CurrentHero.Statistics.CurrentHealth <= 0)
                        Death();
                }
            }
            else
            {
                EnemyTurn();
                if (GameState.CurrentHero.Statistics.CurrentHealth > 0 && !_battleEnded)
                    HeroTurn();
                else if (GameState.CurrentHero.Statistics.CurrentHealth <= 0)
                    Death();
            }

            CheckButtons();
        }

        /// <summary>The Hero's turn this round of battle.</summary>
        private void HeroTurn()
        {
            switch (_heroAction)
            {
                case BattleAction.Attack:
                    if (GameState.CurrentHero.Equipment.Weapon.WeaponType == WeaponTypes.Melee)
                        HeroAttack(GameState.CurrentHero.TotalStrength, GameState.CurrentHero.Equipment.TotalDamage);
                    else if (GameState.CurrentHero.Equipment.Weapon.WeaponType == WeaponTypes.Ranged)
                        HeroAttack(GameState.CurrentHero.TotalDexterity, GameState.CurrentHero.Equipment.TotalDamage);
                    break;

                case BattleAction.Cast:

                    Functions.AddTextToTextBox(TxtBattle, $"You cast {_currentSpell.Name}.");

                    switch (_currentSpell.Type)
                    {
                        case SpellTypes.Damage:
                            HeroAttack(GameState.CurrentHero.TotalWisdom, _currentSpell.Amount);
                            break;

                        case SpellTypes.Healing:
                            Functions.AddTextToTextBox(TxtBattle, GameState.CurrentHero.Heal(_currentSpell.Amount));
                            break;

                        case SpellTypes.Shield:
                            HeroShield += _currentSpell.Amount;
                            Functions.AddTextToTextBox(TxtBattle, $"You now have a magical shield which will help protect you from {HeroShield} damage.");
                            break;
                    }

                    GameState.CurrentHero.Statistics.CurrentMagic -= _currentSpell.MagicCost;
                    break;

                case BattleAction.Flee:

                    if (FleeAttempt(GameState.CurrentHero.TotalDexterity, GameState.CurrentEnemy.TotalDexterity))
                    {
                        EndBattle();
                        Functions.AddTextToTextBox(TxtBattle, $"You successfully fled from the {GameState.CurrentEnemy.Name}.");
                    }
                    else
                        Functions.AddTextToTextBox(TxtBattle, $"The {GameState.CurrentEnemy.Name} blocked your attempt to flee.");
                    break;
            }
        }

        /// <summary>The Hero attacks the Enemy.</summary>
        /// <param name="statModifier">Stat to be given 20% bonus to damage</param>
        /// <param name="damage">Damage</param>
        private void HeroAttack(int statModifier, int damage)
        {
            int chanceHeroHits =
            Functions.GenerateRandomNumber(
            50 + GameState.CurrentHero.Attributes.Dexterity - GameState.CurrentEnemy.Attributes.Dexterity, 90,
            10, 90);
            int heroHits = Functions.GenerateRandomNumber(10, 90);

            if (heroHits <= chanceHeroHits)
            {
                int maximumHeroDamage = Int32Helper.Parse(statModifier * 0.2 + damage);
                int maximumEnemyAbsorb = GameState.CurrentEnemy.Equipment.TotalDefense;
                int actualDamage = Functions.GenerateRandomNumber(maximumHeroDamage / 10, maximumHeroDamage, 1);
                int actualAbsorb = Functions.GenerateRandomNumber(maximumEnemyAbsorb / 10, maximumEnemyAbsorb);

                string absorb = "";
                if (actualAbsorb > 0)
                    absorb = $"The {GameState.CurrentEnemy.Name}'s armor absorbed {actualAbsorb} damage. ";

                if (actualDamage > actualAbsorb)
                {
                    Functions.AddTextToTextBox(TxtBattle, $"You attack for {actualDamage} damage. {absorb}{GameState.CurrentEnemy.TakeDamage(actualDamage - actualAbsorb)}");
                    if (GameState.CurrentEnemy.Statistics.CurrentHealth <= 0)
                    {
                        EndBattle();
                        Functions.AddTextToTextBox(TxtBattle, GameState.CurrentHero.GainExperience(GameState.CurrentEnemy.Experience));
                        if (GameState.CurrentEnemy.Inventory.Gold > 0)
                            Functions.AddTextToTextBox(TxtBattle, $"You find {GameState.CurrentEnemy.Inventory.Gold} gold on the body.");

                        GameState.CurrentHero.Inventory.Gold += GameState.CurrentEnemy.Inventory.Gold;
                    }
                }
                else
                    Functions.AddTextToTextBox(TxtBattle, $"You attack for {actualDamage}, but its armor absorbs all of it.");
            }
            else
                Functions.AddTextToTextBox(TxtBattle, "You miss.");
        }

        /// <summary>Sets the current Spell.</summary>
        /// <param name="spell">Spell to be set</param>
        internal void SetSpell(Spell spell)
        {
            _currentSpell = spell;
            NewRound(BattleAction.Cast);
        }

        /// <summary>Sets the Enemy's action for the round.</summary>
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
                if (GameState.CurrentEnemy.Statistics.CurrentHealth >
                GameState.CurrentEnemy.Statistics.MaximumHealth / 3) //one third or more health, no chance to want to flee.
                    _enemyAction = BattleAction.Attack;
                else if (GameState.CurrentEnemy.Statistics.CurrentHealth >
                 GameState.CurrentEnemy.Statistics.MaximumHealth / 5) //20% or more health, 2% chance to want to flee.
                    _enemyAction = result <= 98 ? BattleAction.Attack : BattleAction.Flee;
                else //20% or less health, 5% chance to want to flee.
                    _enemyAction = result <= 95 ? BattleAction.Attack : BattleAction.Flee;
            }
        }

        /// <summary>The Enemy's turn this round of battle.</summary>
        private void EnemyTurn()
        {
            SetEnemyAction();
            switch (_enemyAction)
            {
                case BattleAction.Attack:
                    if (GameState.CurrentEnemy.Equipment.Weapon.WeaponType == WeaponTypes.Melee)
                        EnemyAttack(GameState.CurrentEnemy.TotalStrength, GameState.CurrentEnemy.Equipment.TotalDamage);
                    else if (GameState.CurrentEnemy.Equipment.Weapon.WeaponType == WeaponTypes.Ranged)
                        EnemyAttack(GameState.CurrentEnemy.TotalDexterity, GameState.CurrentEnemy.Equipment.TotalDamage);
                    break;

                case BattleAction.Flee:
                    if (FleeAttempt(GameState.CurrentEnemy.TotalDexterity, GameState.CurrentHero.TotalDexterity))
                    {
                        EndBattle();
                        Functions.AddTextToTextBox(TxtBattle, $"The {GameState.CurrentEnemy.Name} fled from the battle.");
                        Functions.AddTextToTextBox(TxtBattle, GameState.CurrentHero.GainExperience(GameState.CurrentEnemy.Experience / 2));
                    }
                    else
                        Functions.AddTextToTextBox(TxtBattle, $"You block the {GameState.CurrentEnemy.Name}'s attempt to flee.");
                    break;
            }
        }

        /// <summary>
        /// The Enemy attacks the Hero.
        /// </summary>
        private void EnemyAttack(int statModifier, int damage)
        {
            int chanceEnemyHits =
            Functions.GenerateRandomNumber(
            50 + GameState.CurrentHero.Attributes.Dexterity - GameState.CurrentEnemy.Attributes.Dexterity, 90,
            10, 90);
            int heroHits = Functions.GenerateRandomNumber(10, 90);

            if (heroHits <= chanceEnemyHits)
            {
                int maximumDamage =
                Int32Helper.Parse(statModifier * 0.2 + damage);
                int heroDefense = GameState.CurrentHero.Equipment.TotalDefense;
                int actualDamage = Functions.GenerateRandomNumber(maximumDamage / 10, maximumDamage, 1);
                int maximumShieldAbsorb = Functions.GenerateRandomNumber(HeroShield / 10, HeroShield);
                int maximumArmorAbsorb = Functions.GenerateRandomNumber(heroDefense / 10, heroDefense);
                int actualArmorAbsorb = 0;

                // shield absorbs actualDamage up to maxShieldAbsorb
                int actualShieldAbsorb = maximumShieldAbsorb >= actualDamage ? actualDamage : maximumShieldAbsorb;

                HeroShield -= actualShieldAbsorb;

                // if shield absorbs all damage, actualArmorAbsorb is 0, otherwise check actualDamage - maxShieldAbsorb
                if (actualShieldAbsorb < actualDamage)
                    if (maximumArmorAbsorb >= actualDamage - actualShieldAbsorb)
                        actualArmorAbsorb = actualDamage - actualShieldAbsorb;
                    else
                        actualArmorAbsorb = maximumArmorAbsorb;

                string absorb = "";
                string shield = "";

                if (actualShieldAbsorb > 0)
                    shield = $" Your magical shield absorbs {actualShieldAbsorb} damage.";
                if (actualArmorAbsorb > 0)
                    absorb = $" Your armor absorbs {actualArmorAbsorb} damage. ";

                if (actualDamage > actualShieldAbsorb + actualArmorAbsorb) //the player actually takes damage
                {
                    Functions.AddTextToTextBox(TxtBattle, $"The {GameState.CurrentEnemy.Name} attacks you for {actualDamage} damage. {shield}{absorb}{GameState.CurrentHero.TakeDamage(actualDamage - actualShieldAbsorb - actualArmorAbsorb)}");
                }
                else
                {
                    if (actualShieldAbsorb > 0 && actualArmorAbsorb > 0)
                        Functions.AddTextToTextBox(TxtBattle, $"The {GameState.CurrentEnemy.Name} attacks you for {actualDamage}, but {shield.ToLower()}{absorb.ToLower()}");
                    else if (actualDamage == actualShieldAbsorb)
                        Functions.AddTextToTextBox(TxtBattle, $"The {GameState.CurrentEnemy.Name} attacks you for {actualDamage}, but your shield absorbed all of it.");
                    else
                        Functions.AddTextToTextBox(TxtBattle, $"The {GameState.CurrentEnemy.Name} attacks you for {actualDamage}, but your armor absorbed all of it.");
                }
            }
            else
                Functions.AddTextToTextBox(TxtBattle, $"The {GameState.CurrentEnemy.Name} misses.");
        }

        /// <summary>Determines whether a flight attempt is successful.</summary>
        /// <param name="fleeAttemptDexterity">Whoever is attempting to flee's Dexterity</param>
        /// <param name="blockAttemptDexterity">Whoever is not attempting to flee's Dexterity</param>
        /// <returns>Returns true if the flight attempt is successful</returns>
        private static bool FleeAttempt(int fleeAttemptDexterity, int blockAttemptDexterity)
        {
            int chanceToFlee = Functions.GenerateRandomNumber(1, 20 + fleeAttemptDexterity - blockAttemptDexterity, 1,
            90);
            int flee = Functions.GenerateRandomNumber(1, 100);

            return flee <= chanceToFlee;
        }

        /// <summary>A fairy is summoned to resurrect the Hero.</summary>
        private void Death()
        {
            EndBattle();
            if (!GameState.CurrentHero.Hardcore)
            {
                Functions.AddTextToTextBox(TxtBattle,
              "A mysterious fairy appears, and, seeing your crumpled body on the ground, resurrects you. You have just enough health to make it back to town.");
                GameState.CurrentHero.Statistics.CurrentHealth = 1;
            }
            else
            {
                Functions.AddTextToTextBox(TxtBattle, "Your Hardcore Hero has been killed. This character will be deleted when you click Return.");
                _blnHardcoreDeath = true;
            }
        }

        #endregion Battle Logic

        #region Button Management

        /// <summary>Checks whether to enable/disable battle buttons.</summary>
        private void CheckButtons()
        {
            ToggleButtons(!_battleEnded);
        }

        /// <summary>Toggles whether the Window's Buttons are enabled.</summary>
        /// <param name="enabled">Are the buttons enabled?</param>
        private void ToggleButtons(bool enabled)
        {
            BtnAttack.IsEnabled = enabled;
            BtnCastSpell.IsEnabled = enabled;
            BtnFlee.IsEnabled = enabled;
        }

        #endregion Button Management

        #region Button-Click Methods

        private void BtnAttack_Click(object sender, RoutedEventArgs e)
        {
            NewRound(BattleAction.Attack);
        }

        private void BtnCharDetails_Click(object sender, RoutedEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow { RefToBattleWindow = this };
            characterWindow.SetupChar();
            characterWindow.BindLabels();
            characterWindow.SetPreviousWindow("Battle");
            characterWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnEnemyDetails_Click(object sender, RoutedEventArgs e)
        {
            EnemyDetailsWindow enemyDetailsWindow = new EnemyDetailsWindow { RefToBattleWindow = this };
            enemyDetailsWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnCastSpell_Click(object sender, RoutedEventArgs e)
        {
            CastSpellWindow castSpellWindow = new CastSpellWindow { RefToBattleWindow = this };
            castSpellWindow.LoadWindow("Battle");
            castSpellWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnFlee_Click(object sender, RoutedEventArgs e)
        {
            NewRound(BattleAction.Flee);
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        public BattleWindow()
        {
            InitializeComponent();
        }

        private async void WindowBattle_Closing(object sender, CancelEventArgs e)
        {
            if (_battleEnded)
            {
                switch (_previousWindow)
                {
                    case "Explore":
                        if (_blnHardcoreDeath)
                            RefToExploreWindow.HardcoreDeath();
                        else
                        {
                            RefToExploreWindow.Show();
                            RefToExploreWindow.CheckButtons();
                        }
                        break;

                    case "Fields":
                        if (_blnHardcoreDeath)
                            RefToFieldsWindow.HardcoreDeath();
                        else
                            RefToFieldsWindow.Show();
                        break;

                    case "Forest":
                        if (_blnHardcoreDeath)
                            RefToForestWindow.HardcoreDeath();
                        else
                            RefToForestWindow.Show();
                        break;

                    case "Cathedral":
                        if (_blnHardcoreDeath)
                            RefToCathedralWindow.HardcoreDeath();
                        else
                            RefToCathedralWindow.Show();
                        break;

                    case "Mines":
                        if (_blnHardcoreDeath)
                            RefToMinesWindow.HardcoreDeath();
                        else
                            RefToMinesWindow.Show();
                        break;

                    case "Catacombs":
                        if (_blnHardcoreDeath)
                            RefToCatacombsWindow.HardcoreDeath();
                        else
                            RefToCatacombsWindow.Show();
                        break;
                }
                if (!_blnHardcoreDeath)
                    await GameState.SaveHero(GameState.CurrentHero);
            }
            else
            {
                e.Cancel = true;
            }
        }

        #endregion Window-Manipulation Methods
    }
}