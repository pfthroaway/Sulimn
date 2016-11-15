﻿using System;
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
            btnAttack.IsEnabled = false;
            btnCastSpell.IsEnabled = false;
            btnFlee.IsEnabled = false;
            battleEnded = true;
            btnReturn.IsEnabled = true;
        }

        #endregion Battle Management

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

            if (GameState.CurrentHero.Attributes.Dexterity > GameState.CurrentEnemy.Attributes.Dexterity)
                chanceHeroAttacksFirst = Functions.GenerateRandomNumber(51, 90);
            else
                chanceHeroAttacksFirst = Functions.GenerateRandomNumber(10, 49);

            chanceHeroHits = Functions.GenerateRandomNumber(50 + GameState.CurrentHero.Attributes.Dexterity - GameState.CurrentEnemy.Attributes.Dexterity, 90, 90);
            chanceEnemyHits = Functions.GenerateRandomNumber(50 - GameState.CurrentHero.Attributes.Dexterity + GameState.CurrentEnemy.Attributes.Dexterity, 90, 90);

            int attacksFirst = Functions.GenerateRandomNumber(1, 100);

            if (attacksFirst <= chanceHeroAttacksFirst)
            {
                // Player casts first
                AddTextTT("You cast " + spell.Name + ".");

                if (spell.Type == SpellTypes.Damage)
                {
                    int playerHits = Functions.GenerateRandomNumber(10, 90);
                    if (playerHits <= chanceHeroHits)
                        HeroAttack(spell);
                    else
                        AddTextTT("You miss.");
                }
                else if (spell.Type == SpellTypes.Healing)
                {
                    AddTextTT(GameState.CurrentHero.Heal(spell.Amount));
                }
                else if (spell.Type == SpellTypes.Shield)
                {
                    HeroShield += spell.Amount;
                    AddTextTT("You now have a magical shield which will help protect you from " + HeroShield + " damage.");
                }
                else
                {
                    //FUTURE SPELL TYPES
                }

                // Then Enemy's turn
                if (GameState.CurrentEnemy.Statistics.CurrentHealth > 0)
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
                if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
                {
                    // Player casts
                    AddTextTT("You cast " + spell.Name + ".");

                    if (spell.Type == SpellTypes.Damage)
                    {
                        int playerHits = Functions.GenerateRandomNumber(10, 90);
                        if (playerHits <= chanceHeroHits)
                            HeroAttack(spell);
                        else
                            AddTextTT("You miss.");
                    }
                    else if (spell.Type == SpellTypes.Healing)
                    {
                        GameState.CurrentHero.Heal(spell.Amount);
                    }
                    else if (spell.Type == SpellTypes.Shield)
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
                    GameState.CurrentHero.Statistics.CurrentHealth = 1;
            }
            GameState.CurrentHero.Statistics.CurrentMagic -= spell.MagicCost;
        }

        /// <summary>
        /// The Hero attacks the Enemy.
        /// </summary>
        private void HeroAttack()
        {
            if (GameState.CurrentHero.Equipment.Weapon.WeaponType == WeaponTypes.Melee)
                HeroAttack(GameState.CurrentHero.Attributes.Strength, GameState.CurrentHero.Equipment.Weapon.Damage);
            else if (GameState.CurrentHero.Equipment.Weapon.WeaponType == WeaponTypes.Ranged)
                HeroAttack(GameState.CurrentHero.Attributes.Dexterity, GameState.CurrentHero.Equipment.Weapon.Damage);
        }

        /// <summary>
        /// The Hero attacks the Enemy.
        /// </summary>
        /// <param name="statModifier">Stat to be given 20% bonus to damage</param>
        /// <param name="damage">Damage</param>
        private void HeroAttack(int statModifier, int damage)
        {
            int maxHeroDamage = Int32Helper.Parse(statModifier * 0.2 + damage);
            int maxEnemyAbsorb = GameState.CurrentEnemy.Equipment.Head.Defense + GameState.CurrentEnemy.Equipment.Body.Defense + GameState.CurrentEnemy.Equipment.Legs.Defense + GameState.CurrentEnemy.Equipment.Feet.Defense;

            int actualDamage = Functions.GenerateRandomNumber(1, maxHeroDamage);
            int actualAbsorb = Functions.GenerateRandomNumber(0, maxEnemyAbsorb);

            string absorb = "";
            if (actualAbsorb > 0)
                absorb = "Its armor absorbed " + actualAbsorb + " damage. ";

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

        /// <summary>
        /// The Hero attacks the Enemy.
        /// </summary>
        private void HeroAttack(Spell spell)
        {
            HeroAttack(GameState.CurrentHero.Attributes.Wisdom, spell.Amount);
        }

        /// <summary>
        /// The Enemy attacks the Hero.
        /// </summary>
        private void EnemyAttack()
        {
            int maxDamage = Int32Helper.Parse(GameState.CurrentEnemy.Attributes.Strength * 0.2 + GameState.CurrentEnemy.Equipment.Weapon.Damage);
            int HeroDefense = GameState.CurrentHero.Equipment.Head.Defense + GameState.CurrentHero.Equipment.Body.Defense + GameState.CurrentHero.Equipment.Legs.Defense + GameState.CurrentHero.Equipment.Feet.Defense;
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

            if (GameState.CurrentHero.Statistics.CurrentHealth <= 0)
            {
                EndBattle();
                AddTextTT("A mysterious fairy appears, and, seeing your crumpled body on the ground, resurrects you. You have just enough health to make it back to town.");
                GameState.CurrentHero.Statistics.CurrentHealth = 1;
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

            if (GameState.CurrentHero.Attributes.Dexterity > GameState.CurrentEnemy.Attributes.Dexterity)
                chanceHeroAttacksFirst = Functions.GenerateRandomNumber(51, 90);
            else
                chanceHeroAttacksFirst = Functions.GenerateRandomNumber(10, 49);

            chanceHeroHits = Functions.GenerateRandomNumber(50 + GameState.CurrentHero.Attributes.Dexterity - GameState.CurrentEnemy.Attributes.Dexterity, 90, 90);
            chanceEnemyHits = Functions.GenerateRandomNumber(50 - GameState.CurrentHero.Attributes.Dexterity + GameState.CurrentEnemy.Attributes.Dexterity, 90, 90);

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
                if (GameState.CurrentEnemy.Statistics.CurrentHealth > 0)
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
                if (GameState.CurrentHero.Statistics.CurrentHealth > 0 && !battleEnded)
                {
                    int playerHits = Functions.GenerateRandomNumber(10, 90);
                    if (playerHits <= chanceHeroHits)
                        HeroAttack();
                    else
                        AddTextTT("You miss.");
                }
                else
                    GameState.CurrentHero.Statistics.CurrentHealth = 1;
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
            castSpellWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void btnFlee_Click(object sender, RoutedEventArgs e)
        {
            int chanceToFlee;
            if (GameState.CurrentHero.Attributes.Dexterity > GameState.CurrentEnemy.Attributes.Dexterity)
                chanceToFlee = Functions.GenerateRandomNumber(50 + GameState.CurrentHero.Attributes.Dexterity - GameState.CurrentEnemy.Attributes.Dexterity, 90, 90);
            else
                chanceToFlee = Functions.GenerateRandomNumber(50 - GameState.CurrentHero.Attributes.Dexterity + GameState.CurrentEnemy.Attributes.Dexterity, 90, 90);

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
                if (GameState.CurrentHero.Attributes.Dexterity > GameState.CurrentEnemy.Attributes.Dexterity)
                    chanceEnemyHits = Functions.GenerateRandomNumber(50 + GameState.CurrentHero.Attributes.Dexterity - GameState.CurrentEnemy.Attributes.Dexterity, 90, 90);
                else
                    chanceEnemyHits = Functions.GenerateRandomNumber(50 - GameState.CurrentHero.Attributes.Dexterity + GameState.CurrentEnemy.Attributes.Dexterity, 90, 90);

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