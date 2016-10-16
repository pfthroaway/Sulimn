using System;
using System.ComponentModel;
using System.Windows;

namespace Sulimn_WPF
{
    /// <summary>
    /// Interaction logic for CharacterWindow.xaml
    /// </summary>
    public partial class CharacterWindow : Window, INotifyPropertyChanged
    {
        private Hero copyOfHero = new Hero();
        internal BattleWindow RefToBattleWindow { get; set; }
        internal CityWindow RefToCityWindow { get; set; }

        private string PreviousWindow;

        #region Data Binding

        internal void BindLabels()
        {
            DataContext = GameState.CurrentHero;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data Binding

        internal void CastSpell(Spell spell)
        {
            if (spell.Type == "Healing")
                GameState.CurrentHero.Heal(spell.Amount);
            GameState.CurrentHero.CurrentMagic -= spell.MagicCost;
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
            GameState.CurrentHero.Strength = copyOfHero.Strength;
            GameState.CurrentHero.Vitality = copyOfHero.Vitality;
            GameState.CurrentHero.Dexterity = copyOfHero.Dexterity;
            GameState.CurrentHero.Wisdom = copyOfHero.Wisdom;
            GameState.CurrentHero.SkillPoints = copyOfHero.SkillPoints;
            GameState.CurrentHero.CurrentHealth = copyOfHero.CurrentHealth;
            GameState.CurrentHero.MaximumHealth = copyOfHero.MaximumHealth;
            GameState.CurrentHero.CurrentMagic = copyOfHero.CurrentMagic;
            GameState.CurrentHero.MaximumMagic = copyOfHero.MaximumMagic;
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
            inventoryWindow.DisplayAllInfo();
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
            GameState.CurrentHero.Strength--;

            if (GameState.CurrentHero.Strength == copyOfHero.Strength)
                btnStrengthMinus.IsEnabled = false;
            CheckSkillPoints();
        }

        private void btnStrengthPlus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints--;
            GameState.CurrentHero.Strength++;
            btnStrengthMinus.IsEnabled = true;
            CheckSkillPoints();
        }

        private void btnVitalityMinus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints++;
            GameState.CurrentHero.Vitality--;
            GameState.CurrentHero.CurrentHealth -= 5;
            GameState.CurrentHero.MaximumHealth -= 5;

            if (GameState.CurrentHero.Vitality == copyOfHero.Vitality)
                btnVitalityMinus.IsEnabled = false;
            CheckSkillPoints();
        }

        private void btnVitalityPlus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints--;
            GameState.CurrentHero.Vitality++;
            GameState.CurrentHero.CurrentHealth += 5;
            GameState.CurrentHero.MaximumHealth += 5;
            btnVitalityMinus.IsEnabled = true;
            CheckSkillPoints();
        }

        private void btnDexterityMinus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints++;
            GameState.CurrentHero.Dexterity--;

            if (GameState.CurrentHero.Dexterity == copyOfHero.Dexterity)
                btnDexterityMinus.IsEnabled = false;
            CheckSkillPoints();
        }

        private void btnDexterityPlus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints--;
            GameState.CurrentHero.Dexterity++;
            btnDexterityMinus.IsEnabled = true;
            CheckSkillPoints();
        }

        private void btnWisdomMinus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints++;
            GameState.CurrentHero.Wisdom--;
            GameState.CurrentHero.CurrentMagic -= 5;
            GameState.CurrentHero.MaximumMagic -= 5;

            if (GameState.CurrentHero.Wisdom == copyOfHero.Wisdom)
                btnWisdomMinus.IsEnabled = false;
            CheckSkillPoints();
        }

        private void btnWisdomPlus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints--;
            GameState.CurrentHero.Wisdom++;
            GameState.CurrentHero.CurrentMagic += 5;
            GameState.CurrentHero.MaximumMagic += 5;
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

        private void windowCharacter_Closing(object sender, CancelEventArgs e)
        {
            GameState.SaveHero();

            switch (PreviousWindow)
            {
                case "Battle":
                    RefToBattleWindow.Show();
                    break;

                case "City":
                    RefToCityWindow.Show();
                    break;
            }
        }

        #endregion Window-Generated Methods
    }
}