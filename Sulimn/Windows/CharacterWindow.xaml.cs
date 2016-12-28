using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for CharacterWindow.xaml
    /// </summary>
    public partial class CharacterWindow : INotifyPropertyChanged
    {
        private Hero _copyOfHero = new Hero();
        private string _previousWindow;

        internal BattleWindow RefToBattleWindow { private get; set; }
        internal CityWindow RefToCityWindow { private get; set; }
        internal TheTavernBarWindow RefToTheTavernBarWindow { private get; set; }
        internal TheArmouryWindow RefToTheArmouryWindow { private get; set; }
        internal MagickShoppeWindow RefToMagickShoppeWindow { private get; set; }
        internal SilverEmpireWindow RefToSilverEmpireWindow { private get; set; }
        internal WeaponsRUsWindow RefToWeaponsRUsWindow { private get; set; }
        internal TheGeneralStoreWindow RefToTheGeneralStoreWindow { private get; set; }

        /// <summary>Casts a Spell.</summary>
        /// <param name="spell">Spell to be cast</param>
        internal static void CastSpell(Spell spell)
        {
            if (spell.Type == SpellTypes.Healing)
                GameState.CurrentHero.Heal(spell.Amount);
            GameState.CurrentHero.Statistics.CurrentMagic -= spell.MagicCost;
            //FUTURE SPELL TYPES
        }

        /// <summary>Stores stats for the current Hero for use when assigning skill points.</summary>
        internal void SetupChar()
        {
            _copyOfHero = new Hero(GameState.CurrentHero);
            CheckSkillPoints();
        }

        /// <summary>Sets the previous Window.</summary>
        /// <param name="prevWindow">Previous Window</param>
        internal void SetPreviousWindow(string prevWindow)
        {
            _previousWindow = prevWindow;
        }

        /// <summary>Displays the Hero's information.</summary>
        private void CheckSkillPoints()
        {
            TogglePlus(GameState.CurrentHero.SkillPoints > 0);
            btnReset.IsEnabled = GameState.CurrentHero.SkillPoints != _copyOfHero.SkillPoints;
            GameState.CurrentHero.UpdateStatistics();
        }

        /// <summary>Resets the current Hero to the copy created when the Window loaded.</summary>
        private void Reset()
        {
            GameState.CurrentHero.Attributes.Strength = _copyOfHero.Attributes.Strength;
            GameState.CurrentHero.Attributes.Vitality = _copyOfHero.Attributes.Vitality;
            GameState.CurrentHero.Attributes.Dexterity = _copyOfHero.Attributes.Dexterity;
            GameState.CurrentHero.Attributes.Wisdom = _copyOfHero.Attributes.Wisdom;
            GameState.CurrentHero.SkillPoints = _copyOfHero.SkillPoints;
            GameState.CurrentHero.Statistics.CurrentHealth = _copyOfHero.Statistics.CurrentHealth;
            GameState.CurrentHero.Statistics.MaximumHealth = _copyOfHero.Statistics.MaximumHealth;
            GameState.CurrentHero.Statistics.CurrentMagic = _copyOfHero.Statistics.CurrentMagic;
            GameState.CurrentHero.Statistics.MaximumMagic = _copyOfHero.Statistics.MaximumMagic;
            DisableMinus();
            CheckSkillPoints();
        }

        #region Data Binding

        internal void BindLabels()
        {
            DataContext = GameState.CurrentHero;
            lblStrength.DataContext = GameState.CurrentHero;
            lblVitality.DataContext = GameState.CurrentHero;
            lblDexterity.DataContext = GameState.CurrentHero;
            lblWisdom.DataContext = GameState.CurrentHero;
            lblHealth.DataContext = GameState.CurrentHero.Statistics;
            lblMagic.DataContext = GameState.CurrentHero.Statistics;
            lblGold.DataContext = GameState.CurrentHero.Inventory;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data Binding

        #region Toggle Buttons

        /// <summary>Toggles the IsEnabled Property of the Plus Buttons.</summary>
        /// <param name="enabled">Should the Buttons be enabled?</param>
        private void TogglePlus(bool enabled)
        {
            btnDexterityPlus.IsEnabled = enabled;
            btnStrengthPlus.IsEnabled = enabled;
            btnWisdomPlus.IsEnabled = enabled;
            btnVitalityPlus.IsEnabled = enabled;
        }

        /// <summary>Disables attribute Minus Buttons.</summary>
        private void DisableMinus()
        {
            btnDexterityMinus.IsEnabled = false;
            btnStrengthMinus.IsEnabled = false;
            btnWisdomMinus.IsEnabled = false;
            btnVitalityMinus.IsEnabled = false;
        }

        #endregion Toggle Buttons

        #region Button-Click Methods

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void btnCastSpell_Click(object sender, RoutedEventArgs e)
        {
            CastSpellWindow castSpellWindow = new CastSpellWindow { RefToCharacterWindow = this };
            castSpellWindow.LoadWindow("Character");
            castSpellWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            InventoryWindow inventoryWindow = new InventoryWindow { RefToCharacterWindow = this };
            inventoryWindow.Show();
            Visibility = Visibility.Hidden;
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

            btnStrengthMinus.IsEnabled = GameState.CurrentHero.Attributes.Strength == _copyOfHero.Attributes.Strength;
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

            btnVitalityMinus.IsEnabled = GameState.CurrentHero.Attributes.Vitality == _copyOfHero.Attributes.Vitality;
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

            btnDexterityMinus.IsEnabled = GameState.CurrentHero.Attributes.Dexterity == _copyOfHero.Attributes.Dexterity;
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

            btnWisdomMinus.IsEnabled = GameState.CurrentHero.Attributes.Wisdom == _copyOfHero.Attributes.Wisdom;
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

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public CharacterWindow()
        {
            InitializeComponent();
        }

        private async void windowCharacter_Closing(object sender, CancelEventArgs e)
        {
            switch (_previousWindow)
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