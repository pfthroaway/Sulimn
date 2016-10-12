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
            DataContext = GameState.currentHero;
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
                GameState.currentHero.Heal(spell.Amount);
            //FUTURE SPELL TYPES
        }

        /// <summary>
        /// Stores stats for the current Hero for use when assigning skill points.
        /// </summary>
        internal void SetupChar()
        {
            copyOfHero = new Hero(GameState.currentHero);
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
            if (GameState.currentHero.SkillPoints > 0)
                EnablePlus();
            else
                DisablePlus();
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

        #endregion Button-Click Methods

        #region Plus/Minus Button Logic

        private void btnStrengthMinus_Click(object sender, RoutedEventArgs e)
        {
            GameState.currentHero.SkillPoints++;
            GameState.currentHero.Strength--;

            if (GameState.currentHero.Strength == copyOfHero.Strength)
                btnStrengthMinus.IsEnabled = false;
            CheckSkillPoints();
        }

        private void btnStrengthPlus_Click(object sender, RoutedEventArgs e)
        {
            GameState.currentHero.SkillPoints--;
            GameState.currentHero.Strength++;
            btnStrengthMinus.IsEnabled = true;
            CheckSkillPoints();
        }

        private void btnVitalityMinus_Click(object sender, RoutedEventArgs e)
        {
            GameState.currentHero.SkillPoints++;
            GameState.currentHero.Vitality--;
            GameState.currentHero.CurrentHealth -= 5;
            GameState.currentHero.MaximumHealth -= 5;

            if (GameState.currentHero.Vitality == copyOfHero.Vitality)
                btnVitalityMinus.IsEnabled = false;
            CheckSkillPoints();
        }

        private void btnVitalityPlus_Click(object sender, RoutedEventArgs e)
        {
            GameState.currentHero.SkillPoints--;
            GameState.currentHero.Vitality++;
            GameState.currentHero.CurrentHealth += 5;
            GameState.currentHero.MaximumHealth += 5;
            btnVitalityMinus.IsEnabled = true;
            CheckSkillPoints();
        }

        private void btnDexterityMinus_Click(object sender, RoutedEventArgs e)
        {
            GameState.currentHero.SkillPoints++;
            GameState.currentHero.Dexterity--;

            if (GameState.currentHero.Dexterity == copyOfHero.Dexterity)
                btnDexterityMinus.IsEnabled = false;
            CheckSkillPoints();
        }

        private void btnDexterityPlus_Click(object sender, RoutedEventArgs e)
        {
            GameState.currentHero.SkillPoints--;
            GameState.currentHero.Dexterity++;
            btnDexterityMinus.IsEnabled = true;
            CheckSkillPoints();
        }

        private void btnWisdomMinus_Click(object sender, RoutedEventArgs e)
        {
            GameState.currentHero.SkillPoints++;
            GameState.currentHero.Wisdom--;
            GameState.currentHero.CurrentMagic -= 5;
            GameState.currentHero.MaximumMagic -= 5;

            if (GameState.currentHero.Wisdom == copyOfHero.Wisdom)
                btnWisdomMinus.IsEnabled = false;
            CheckSkillPoints();
        }

        private void btnWisdomPlus_Click(object sender, RoutedEventArgs e)
        {
            GameState.currentHero.SkillPoints--;
            GameState.currentHero.Wisdom++;
            GameState.currentHero.CurrentMagic += 5;
            GameState.currentHero.MaximumMagic += 5;
            btnWisdomMinus.IsEnabled = true;
            CheckSkillPoints();
        }

        #endregion Plus/Minus Button Logic

        #region Window-Generated Methods

        private void CloseWindow()
        {
            this.Close();
        }

        public CharacterWindow()
        {
            InitializeComponent();
        }

        #endregion Window-Generated Methods

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
    }
}