using Sulimn.Classes;
using Sulimn.Classes.Entities;
using Sulimn.Classes.Enums;
using Sulimn.Classes.HeroParts;
using System.ComponentModel;
using System.Windows;

namespace Sulimn.Views.Characters
{
    /// <summary>Interaction logic for CharacterPage.xaml</summary>
    public partial class CharacterPage : INotifyPropertyChanged
    {
        private Hero _copyOfHero = new Hero();
        private string _previousPage;

        /// <summary>Casts a Spell.</summary>
        /// <param name="spell">Spell to be cast</param>
        internal void CastSpell(Spell spell)
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

        /// <summary>Sets the previous Page.</summary>
        /// <param name="prevPage">Previous Page</param>
        internal void SetPreviousPage(string prevPage) => _previousPage = prevPage;

        /// <summary>Displays the Hero's information.</summary>
        private void CheckSkillPoints()
        {
            TogglePlus(GameState.CurrentHero.SkillPoints > 0);
            BtnReset.IsEnabled = GameState.CurrentHero.SkillPoints != _copyOfHero.SkillPoints;
            GameState.CurrentHero.UpdateStatistics();
        }

        /// <summary>Resets the current Hero to the copy created when the Page loaded.</summary>
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
            LblStrength.DataContext = GameState.CurrentHero;
            LblVitality.DataContext = GameState.CurrentHero;
            LblDexterity.DataContext = GameState.CurrentHero;
            LblWisdom.DataContext = GameState.CurrentHero;
            LblHealth.DataContext = GameState.CurrentHero.Statistics;
            LblMagic.DataContext = GameState.CurrentHero.Statistics;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data Binding

        #region Toggle Buttons

        /// <summary>Toggles the IsEnabled Property of the Plus Buttons.</summary>
        /// <param name="enabled">Should the Buttons be enabled?</param>
        private void TogglePlus(bool enabled)
        {
            BtnDexterityPlus.IsEnabled = enabled;
            BtnStrengthPlus.IsEnabled = enabled;
            BtnWisdomPlus.IsEnabled = enabled;
            BtnVitalityPlus.IsEnabled = enabled;
        }

        /// <summary>Disables attribute Minus Buttons.</summary>
        private void DisableMinus()
        {
            BtnDexterityMinus.IsEnabled = false;
            BtnStrengthMinus.IsEnabled = false;
            BtnWisdomMinus.IsEnabled = false;
            BtnVitalityMinus.IsEnabled = false;
        }

        #endregion Toggle Buttons

        #region Button-Click Methods

        private void BtnBack_Click(object sender, RoutedEventArgs e) => ClosePage();

        private void BtnCastSpell_Click(object sender, RoutedEventArgs e)
        {
            CastSpellPage castSpellPage = new CastSpellPage { RefToCharacterPage = this };
            castSpellPage.LoadPage("Character");
            GameState.Navigate(castSpellPage);
        }

        private void BtnInventory_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new InventoryPage());

        private void BtnReset_Click(object sender, RoutedEventArgs e) => Reset();

        #endregion Button-Click Methods

        #region Plus/Minus Button Logic

        private void BtnStrengthMinus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints++;
            GameState.CurrentHero.Attributes.Strength--;

            BtnStrengthMinus.IsEnabled = GameState.CurrentHero.Attributes.Strength != _copyOfHero.Attributes.Strength;
            CheckSkillPoints();
        }

        private void BtnStrengthPlus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints--;
            GameState.CurrentHero.Attributes.Strength++;
            BtnStrengthMinus.IsEnabled = true;
            CheckSkillPoints();
        }

        private void BtnVitalityMinus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints++;
            GameState.CurrentHero.Attributes.Vitality--;
            GameState.CurrentHero.Statistics.CurrentHealth -= 5;
            GameState.CurrentHero.Statistics.MaximumHealth -= 5;

            BtnVitalityMinus.IsEnabled = GameState.CurrentHero.Attributes.Vitality != _copyOfHero.Attributes.Vitality;
            CheckSkillPoints();
        }

        private void BtnVitalityPlus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints--;
            GameState.CurrentHero.Attributes.Vitality++;
            GameState.CurrentHero.Statistics.CurrentHealth += 5;
            GameState.CurrentHero.Statistics.MaximumHealth += 5;
            BtnVitalityMinus.IsEnabled = true;
            CheckSkillPoints();
        }

        private void BtnDexterityMinus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints++;
            GameState.CurrentHero.Attributes.Dexterity--;

            BtnDexterityMinus.IsEnabled = GameState.CurrentHero.Attributes.Dexterity != _copyOfHero.Attributes.Dexterity;
            CheckSkillPoints();
        }

        private void BtnDexterityPlus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints--;
            GameState.CurrentHero.Attributes.Dexterity++;
            BtnDexterityMinus.IsEnabled = true;
            CheckSkillPoints();
        }

        private void BtnWisdomMinus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints++;
            GameState.CurrentHero.Attributes.Wisdom--;
            GameState.CurrentHero.Statistics.CurrentMagic -= 5;
            GameState.CurrentHero.Statistics.MaximumMagic -= 5;

            BtnWisdomMinus.IsEnabled = GameState.CurrentHero.Attributes.Wisdom != _copyOfHero.Attributes.Wisdom;
            CheckSkillPoints();
        }

        private void BtnWisdomPlus_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.SkillPoints--;
            GameState.CurrentHero.Attributes.Wisdom++;
            GameState.CurrentHero.Statistics.CurrentMagic += 5;
            GameState.CurrentHero.Statistics.MaximumMagic += 5;
            BtnWisdomMinus.IsEnabled = true;
            CheckSkillPoints();
        }

        #endregion Plus/Minus Button Logic

        #region Page-Generated Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage()
        {
            GameState.SaveHero(GameState.CurrentHero);
            GameState.GoBack();
        }

        public CharacterPage() => InitializeComponent();

        private void CharacterPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            SetupChar();
            BindLabels();
        }

        #endregion Page-Generated Methods
    }
}