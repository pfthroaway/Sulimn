using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for CharacterWindow.xaml
    /// </summary>
    public partial class CharacterWindow : Window, INotifyPropertyChanged
    {
        private Hero copyOfHero = new Hero();
        private string PreviousWindow;

        internal BattleWindow RefToBattleWindow { get; set; }
        internal CityWindow RefToCityWindow { get; set; }
        internal TheTavernBarWindow RefToTheTavernBarWindow { get; set; }
        internal TheArmouryWindow RefToTheArmouryWindow { get; set; }
        internal MagickShoppeWindow RefToMagickShoppeWindow { get; set; }
        internal SilverEmpireWindow RefToSilverEmpireWindow { get; set; }
        internal WeaponsRUsWindow RefToWeaponsRUsWindow { get; set; }
        internal TheGeneralStoreWindow RefToTheGeneralStoreWindow { get; set; }

        #region Data Binding

        internal void BindLabels()
        {
            DataContext = GameState.CurrentHero;
            lblStrength.DataContext = GameState.CurrentHero.Attributes;
            lblVitality.DataContext = GameState.CurrentHero.Attributes;
            lblDexterity.DataContext = GameState.CurrentHero.Attributes;
            lblWisdom.DataContext = GameState.CurrentHero.Attributes;
            lblHealth.DataContext = GameState.CurrentHero.Statistics;
            lblMagic.DataContext = GameState.CurrentHero.Statistics;
            lblGold.DataContext = GameState.CurrentHero.Inventory;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data Binding

        internal void CastSpell(Spell spell)
        {
            if (spell.Type == SpellTypes.Healing)
                GameState.CurrentHero.Heal(spell.Amount);
            GameState.CurrentHero.Statistics.CurrentMagic -= spell.MagicCost;
            //FUTURE SPELL TYPES
        }

        /// <summary>
        /// Stores stats for the current Hero for use when assigning skill points.
        /// </summary>
        internal void SetupChar()
        {
            copyOfHero = new Hero(GameState.CurrentHero);
            CheckSkillPoints();
        }

        /// <summary>
        /// Sets the previous Window.
        /// </summary>
        /// <param name="prevWindow">Previous Window</param>
        internal void SetPreviousWindow(string prevWindow)
        {
            PreviousWindow = prevWindow;
        }

        #region Enable/Disable Buttons

        private void DisablePlus()
        {
            btnDexterityPlus.IsEnabled = false;
            btnStrengthPlus.IsEnabled = false;
            btnWisdomPlus.IsEnabled = false;
            btnVitalityPlus.IsEnabled = false;
        }

        private void EnablePlus()
        {
            btnDexterityPlus.IsEnabled = true;
            btnStrengthPlus.IsEnabled = true;
            btnWisdomPlus.IsEnabled = true;
            btnVitalityPlus.IsEnabled = true;
        }

        private void DisableMinus()
        {
            btnDexterityMinus.IsEnabled = false;
            btnStrengthMinus.IsEnabled = false;
            btnWisdomMinus.IsEnabled = false;
            btnVitalityMinus.IsEnabled = false;
        }

        private void EnableMinus()
        {
            btnDexterityMinus.IsEnabled = true;
            btnStrengthMinus.IsEnabled = true;
            btnWisdomMinus.IsEnabled = true;
            btnVitalityMinus.IsEnabled = true;
        }

        #endregion Enable/Disable Buttons

        /// <summary>
        /// Displays the Hero's information.
        /// </summary>
        internal void CheckSkillPoints()
        {
            if (GameState.CurrentHero.SkillPoints > 0)
                EnablePlus();
            else
                DisablePlus();

            if (GameState.CurrentHero.SkillPoints != copyOfHero.SkillPoints)
                btnReset.IsEnabled = true;
            else
                btnReset.IsEnabled = false;
        }

        /// <summary>
        /// Resets the current Hero to the copy created when the Window loaded.
        /// </summary>
        private void Reset()
        {
            GameState.CurrentHero.Attributes.Strength = copyOfHero.Attributes.Strength;
            GameState.CurrentHero.Attributes.Vitality = copyOfHero.Attributes.Vitality;
            GameState.CurrentHero.Attributes.Dexterity = copyOfHero.Attributes.Dexterity;
            GameState.CurrentHero.Attributes.Wisdom = copyOfHero.Attributes.Wisdom;
            GameState.CurrentHero.SkillPoints = copyOfHero.SkillPoints;
            GameState.CurrentHero.Statistics.CurrentHealth = copyOfHero.Statistics.CurrentHealth;
            GameState.CurrentHero.Statistics.MaximumHealth = copyOfHero.Statistics.MaximumHealth;
            GameState.CurrentHero.Statistics.CurrentMagic = copyOfHero.Statistics.CurrentMagic;
            GameState.CurrentHero.Statistics.MaximumMagic = copyOfHero.Statistics.MaximumMagic;
            DisableMinus();
            CheckSkillPoints();
        }

        #region Button-Click Methods

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void btnCastSpell_Click(object sender, RoutedEventArgs e)
        {
            CastSpellWindow CastSpellWindow = new CastSpellWindow();
            CastSpellWindow.RefToCharacterWindow = this;
            CastSpellWindow.LoadWindow("Character");
            CastSpellWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            InventoryWindow inventoryWindow = new InventoryWindow();
            inventoryWindow.RefToCharacterWindow = this;
            inventoryWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        #endregion Button-Click Methods

        #region Plus/Minus Button Logic

        private void btnStrengthMinus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints++;
            GameState.CurrentHero.Attributes.Strength--;

            if (GameState.CurrentHero.Attributes.Strength == copyOfHero.Attributes.Strength)
                btnStrengthMinus.IsEnabled = false;
            CheckSkillPoints();
        }

        private void btnStrengthPlus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints--;
            GameState.CurrentHero.Attributes.Strength++;
            btnStrengthMinus.IsEnabled = true;
            CheckSkillPoints();
        }

        private void btnVitalityMinus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints++;
            GameState.CurrentHero.Attributes.Vitality--;
            GameState.CurrentHero.Statistics.CurrentHealth -= 5;
            GameState.CurrentHero.Statistics.MaximumHealth -= 5;

            if (GameState.CurrentHero.Attributes.Vitality == copyOfHero.Attributes.Vitality)
                btnVitalityMinus.IsEnabled = false;
            CheckSkillPoints();
        }

        private void btnVitalityPlus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints--;
            GameState.CurrentHero.Attributes.Vitality++;
            GameState.CurrentHero.Statistics.CurrentHealth += 5;
            GameState.CurrentHero.Statistics.MaximumHealth += 5;
            btnVitalityMinus.IsEnabled = true;
            CheckSkillPoints();
        }

        private void btnDexterityMinus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints++;
            GameState.CurrentHero.Attributes.Dexterity--;

            if (GameState.CurrentHero.Attributes.Dexterity == copyOfHero.Attributes.Dexterity)
                btnDexterityMinus.IsEnabled = false;
            CheckSkillPoints();
        }

        private void btnDexterityPlus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints--;
            GameState.CurrentHero.Attributes.Dexterity++;
            btnDexterityMinus.IsEnabled = true;
            CheckSkillPoints();
        }

        private void btnWisdomMinus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints++;
            GameState.CurrentHero.Attributes.Wisdom--;
            GameState.CurrentHero.Statistics.CurrentMagic -= 5;
            GameState.CurrentHero.Statistics.MaximumMagic -= 5;

            if (GameState.CurrentHero.Attributes.Wisdom == copyOfHero.Attributes.Wisdom)
                btnWisdomMinus.IsEnabled = false;
            CheckSkillPoints();
        }

        private void btnWisdomPlus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints--;
            GameState.CurrentHero.Attributes.Wisdom++;
            GameState.CurrentHero.Statistics.CurrentMagic += 5;
            GameState.CurrentHero.Statistics.MaximumMagic += 5;
            btnWisdomMinus.IsEnabled = true;
            CheckSkillPoints();
        }

        #endregion Plus/Minus Button Logic

        #region Window-Generated Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
        }

        public CharacterWindow()
        {
            InitializeComponent();
        }

        private async void windowCharacter_Closing(object sender, CancelEventArgs e)
        {
            switch (PreviousWindow)
            {
                case "Battle":
                    RefToBattleWindow.Show();
                    break;

                case "City":
                    RefToCityWindow.Show();
                    break;

                case "The Tavern Bar":
                    RefToTheTavernBarWindow.Show();
                    RefToTheTavernBarWindow.LoadAll();
                    break;

                case "The Armoury":
                    RefToTheArmouryWindow.Show();
                    RefToTheArmouryWindow.LoadAll();
                    break;

                case "Magick Shoppe":
                    RefToMagickShoppeWindow.Show();
                    RefToMagickShoppeWindow.LoadAll();
                    break;

                case "Silver Empire":
                    RefToSilverEmpireWindow.Show();
                    RefToSilverEmpireWindow.LoadAll();
                    break;

                case "Weapons 'R' Us":
                    RefToWeaponsRUsWindow.Show();
                    RefToWeaponsRUsWindow.LoadAll();
                    break;

                case "The General Store":
                    RefToTheGeneralStoreWindow.Show();
                    RefToTheGeneralStoreWindow.LoadAll();
                    break;
            }
            await GameState.SaveHero(GameState.CurrentHero);
        }

        #endregion Window-Generated Methods
    }
}