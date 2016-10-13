using System;
using System.ComponentModel;
using System.Windows;

namespace Sulimn_WPF
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

        #region Properties

        internal ExploreWindow RefToExploreWindow { get; set; }

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
            lblCharHealth.DataContext = GameState.CurrentHero;
            lblCharMagic.DataContext = GameState.CurrentHero;
            lblShield.DataContext = this;
            lblEnemyName.DataContext = GameState.CurrentEnemy;
            lblEnemyHealth.DataContext = GameState.CurrentEnemy;
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
            btnAttack.IsEnabled = false;
            btnCastSpell.IsEnabled = false;
            btnFlee.IsEnabled = false;
            battleEnded = true;
            btnReturn.IsEnabled = true;
        }

        #region Battle Logic

        /// <summary>
        /// Casts a Spell.
        /// </summary>
        /// <param name="spell">Spell to be cast</param>
        internal void CastSpell(Spell spell)
        {
            // if Hero Dexterity is greater
            // chance to cast first is between 51 and 90%
            // chance to hit is 50 + Hero Dexterity - Enemy Dexterity
            // chance for Enemy to hit is 50 - Hero Dexterity + Enemy Dexterity
            // 10% chance to hit/miss no matter how big the difference is between the two

            int chanceHeroAttacksFirst, chanceHeroHits, chanceEnemyHits;

            if (GameState.CurrentHero.Dexterity > GameState.CurrentEnemy.Dexterity)
                chanceHeroAttacksFirst = Functions.GenerateRandomNumber(51, 90);
            else
                chanceHeroAttacksFirst = Functions.GenerateRandomNumber(10, 49);

            chanceHeroHits = Functions.GenerateRandomNumber(50 + GameState.CurrentHero.Dexterity - GameState.CurrentEnemy.Dexterity, 90, 90);
            chanceEnemyHits = Functions.GenerateRandomNumber(50 - GameState.CurrentHero.Dexterity + GameState.CurrentEnemy.Dexterity, 90, 90);

            int attacksFirst = Functions.GenerateRandomNumber(1, 100);

            if (attacksFirst <= chanceHeroAttacksFirst)
            {
                // Player casts first
                AddTextTT("You cast " + spell.Name + ".");

                if (spell.Type == "Damage")
                {
                    int playerHits = Functions.GenerateRandomNumber(10, 90);
                    if (playerHits <= chanceHeroHits)
                        HeroAttack(spell);
                    else
                        AddTextTT("You miss.");
                }
                else if (spell.Type == "Healing")
                {
                    AddTextTT(GameState.CurrentHero.Heal(spell.Amount));
                }
                else if (spell.Type == "Shield")
                {
                    HeroShield += spell.Amount;
                    AddTextTT("You now have a magical shield which will help protect you from " + HeroShield + " damage.");
                }
                else
                {
                    //FUTURE SPELL TYPES
                }

                // Then Enemy's turn
                if (GameState.CurrentEnemy.CurrentHealth > 0)
                {
                    int enemyHits = Functions.GenerateRandomNumber(10, 90);
                    if (enemyHits <= chanceEnemyHits)
                        EnemyAttack();
                    else
                        AddTextTT("The enemy misses.");
                }
            }
            else
            {
                // Enemy attacks first
                int enemyHits = Functions.GenerateRandomNumber(10, 90);
                if (enemyHits <= chanceEnemyHits)
                    EnemyAttack();
                else
                    AddTextTT("The enemy misses.");

                // Then Hero's turn
                if (GameState.CurrentHero.CurrentHealth > 0)
                {
                    // Player casts
                    AddTextTT("You cast " + spell.Name + ".");

                    if (spell.Type == "Damage")
                    {
                        int playerHits = Functions.GenerateRandomNumber(10, 90);
                        if (playerHits <= chanceHeroHits)
                            HeroAttack(spell);
                        else
                            AddTextTT("You miss.");
                    }
                    else if (spell.Type == "Healing")
                    {
                        GameState.CurrentHero.Heal(spell.Amount);
                    }
                    else if (spell.Type == "Shield")
                    {
                        HeroShield += spell.Amount;
                        AddTextTT("You now have a magical shield which will help protect you from " + HeroShield + " damage.");
                    }
                    else
                    {
                        //FUTURE
                    }
                }
                else
                    GameState.CurrentHero.CurrentHealth = 1;
            }
            GameState.CurrentHero.CurrentMagic -= spell.MagicCost;
        }

        /// <summary>
        /// The Hero attacks the Enemy.
        /// </summary>
        private void HeroAttack()
        {
            HeroAttack(GameState.CurrentHero.Strength, GameState.CurrentHero.Weapon.Damage);
        }

        /// <summary>
        /// The Hero attacks the Enemy.
        /// </summary>
        /// <param name="statModifier">Stat to be given 10% bonus to damage</param>
        /// <param name="damage">Damage</param>
        private void HeroAttack(int statModifier, int damage)
        {
            int maxHeroDamage = Convert.ToInt32(statModifier * 0.2 + damage);
            int maxEnemyAbsorb = GameState.CurrentEnemy.Head.Defense + GameState.CurrentEnemy.Body.Defense + GameState.CurrentEnemy.Legs.Defense + GameState.CurrentEnemy.Feet.Defense;

            int actualDamage = Functions.GenerateRandomNumber(1, maxHeroDamage);
            int actualAbsorb = Functions.GenerateRandomNumber(0, maxEnemyAbsorb);

            string absorb = "";
            if (actualAbsorb > 0)
                absorb = "Its armor absorbed " + actualAbsorb + " damage. ";

            if (actualDamage > actualAbsorb)
            {
                AddTextTT("You attack for " + actualDamage + " damage. " + absorb + GameState.CurrentEnemy.TakeDamage(actualDamage - actualAbsorb));
                if (GameState.CurrentEnemy.CurrentHealth <= 0)
                {
                    EndBattle();
                    AddTextTT(GameState.CurrentHero.GainExperience(GameState.CurrentEnemy.Experience));
                    if (GameState.CurrentEnemy.Gold > 0)
                        AddTextTT("You find " + GameState.CurrentEnemy.Gold + " gold on the body.");
                    GameState.CurrentHero.Gold += GameState.CurrentEnemy.Gold;
                }
            }
            else
                AddTextTT("You attack for " + actualDamage + ", but its armor absorbs all of it.");
        }

        /// <summary>
        /// The Hero attacks the Enemy.
        /// </summary>
        private void HeroAttack(Spell spell)
        {
            HeroAttack(GameState.CurrentHero.Wisdom, spell.Amount);
        }

        /// <summary>
        /// The Enemy attacks the Hero.
        /// </summary>
        private void EnemyAttack()
        {
            int maxDamage = Convert.ToInt32(GameState.CurrentEnemy.Strength * 0.1 + GameState.CurrentEnemy.Weapon.Damage);
            int HeroDefense = GameState.CurrentHero.Head.Defense + GameState.CurrentHero.Body.Defense + GameState.CurrentHero.Legs.Defense + GameState.CurrentHero.Feet.Defense;
            int actualDamage = Functions.GenerateRandomNumber(1, maxDamage);
            int maxShieldAbsorb = Functions.GenerateRandomNumber(0, HeroShield);
            int maxArmorAbsorb = Functions.GenerateRandomNumber(0, HeroDefense);
            int actualShieldAbsorb = 0;
            int actualArmorAbsorb = 0;

            // shield absorbs actualDamage up to maxShieldAbsorb
            if (maxShieldAbsorb >= actualDamage)
                actualShieldAbsorb = actualDamage;
            else
                actualShieldAbsorb = maxShieldAbsorb;

            HeroShield -= actualShieldAbsorb;

            // if shield absorbs all damage, actualArmorAbsorb is 0, otherwise check actualDamage - maxShieldAbsorb
            if (actualShieldAbsorb < actualDamage)
            {
                if (maxArmorAbsorb >= (actualDamage - actualShieldAbsorb))
                    actualArmorAbsorb = actualDamage - actualShieldAbsorb;
                else
                    actualArmorAbsorb = maxArmorAbsorb;
            }

            string absorb = "";
            string shield = "";

            if (actualShieldAbsorb > 0)
                shield = " Your magical shield absorbs " + actualShieldAbsorb + " damage.";

            if (actualArmorAbsorb > 0)
                absorb = " Your armor absorbs " + actualArmorAbsorb + " damage. ";

            if (actualDamage > (maxShieldAbsorb + maxArmorAbsorb)) //the player actually takes damage
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

            if (GameState.CurrentHero.CurrentHealth <= 0)
            {
                EndBattle();
                AddTextTT("A mysterious fairy appears, and, seeing your crumpled body on the ground, resurrects you. You have just enough health to make it back to town.");
                GameState.CurrentHero.CurrentHealth = 1;
            }
        }

        #endregion Battle Logic

        #region Button-Click Methods

        private void btnAttack_Click(object sender, RoutedEventArgs e)
        {
            // if Hero Dexterity is greater
            // chance to attack first is between 51 and 90%
            // chance to hit is 50 + Hero Dexterity - Enemy Dexterity
            // chance for Enemy to hit is 50 - Hero Dexterity + Enemy Dexterity
            // 10% chance to hit/miss no matter how big the difference is between the two

            int chanceHeroAttacksFirst, chanceHeroHits, chanceEnemyHits;

            if (GameState.CurrentHero.Dexterity > GameState.CurrentEnemy.Dexterity)
                chanceHeroAttacksFirst = Functions.GenerateRandomNumber(51, 90);
            else
                chanceHeroAttacksFirst = Functions.GenerateRandomNumber(10, 49);

            chanceHeroHits = Functions.GenerateRandomNumber(50 + GameState.CurrentHero.Dexterity - GameState.CurrentEnemy.Dexterity, 90, 90);
            chanceEnemyHits = Functions.GenerateRandomNumber(50 - GameState.CurrentHero.Dexterity + GameState.CurrentEnemy.Dexterity, 90, 90);

            int attacksFirst = Functions.GenerateRandomNumber(1, 100);

            if (attacksFirst <= chanceHeroAttacksFirst)
            {
                // Player attacks first
                int playerHits = Functions.GenerateRandomNumber(10, 90);
                if (playerHits <= chanceHeroHits)
                    HeroAttack();
                else
                    AddTextTT("You miss.");

                // Then Enemy's turn
                if (GameState.CurrentEnemy.CurrentHealth > 0)
                {
                    int enemyHits = Functions.GenerateRandomNumber(10, 90);
                    if (enemyHits <= chanceEnemyHits)
                        EnemyAttack();
                    else
                        AddTextTT("The enemy misses.");
                }
            }
            else
            {
                // Enemy attacks first
                int enemyHits = Functions.GenerateRandomNumber(10, 90);
                if (enemyHits <= chanceEnemyHits)
                    EnemyAttack();
                else
                    AddTextTT("The enemy misses.");

                // Then Hero's turn
                if (GameState.CurrentHero.CurrentHealth > 0 && !battleEnded)
                {
                    int playerHits = Functions.GenerateRandomNumber(10, 90);
                    if (playerHits <= chanceHeroHits)
                        HeroAttack();
                    else
                        AddTextTT("You miss.");
                }
                else
                    GameState.CurrentHero.CurrentHealth = 1;
            }
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
            this.Visibility = Visibility.Hidden;
            castSpellWindow.Show();
        }

        private void btnFlee_Click(object sender, RoutedEventArgs e)
        {
            int chanceToFlee;
            if (GameState.CurrentHero.Dexterity > GameState.CurrentEnemy.Dexterity)
                chanceToFlee = Functions.GenerateRandomNumber(50 + GameState.CurrentHero.Dexterity - GameState.CurrentEnemy.Dexterity, 90, 90);
            else
                chanceToFlee = Functions.GenerateRandomNumber(50 - GameState.CurrentHero.Dexterity + GameState.CurrentEnemy.Dexterity, 90, 90);

            int flee = Functions.GenerateRandomNumber(1, 100);

            if (flee <= chanceToFlee)
            {
                EndBattle();
                AddTextTT("You successfully fled from the " + GameState.CurrentEnemy.Name + ".");
            }
            else
            {
                // Enemy attacks

                int chanceEnemyHits;
                if (GameState.CurrentHero.Dexterity > GameState.CurrentEnemy.Dexterity)
                    chanceEnemyHits = Functions.GenerateRandomNumber(50 + GameState.CurrentHero.Dexterity - GameState.CurrentEnemy.Dexterity, 90, 90);
                else
                    chanceEnemyHits = Functions.GenerateRandomNumber(50 - GameState.CurrentHero.Dexterity + GameState.CurrentEnemy.Dexterity, 90, 90);

                int enemyHits = Functions.GenerateRandomNumber(1, 90);
                if (enemyHits <= chanceEnemyHits)
                    EnemyAttack();
                else
                    AddTextTT("The enemy misses.");
            }
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        public BattleWindow()
        {
            InitializeComponent();
        }

        private void windowBattle_Closing(object sender, CancelEventArgs e)
        {
            GameState.SaveHero();
            if (battleEnded)
            {
                if (previousWindow == "Explore")
                {
                    RefToExploreWindow.Show();
                    RefToExploreWindow.CheckButtons();
                }
            }
            else
                e.Cancel = true;
        }

        #endregion Window-Manipulation Methods
    }
}