using Extensions;
using Sulimn.Classes;
using Sulimn.Classes.HeroParts;
using Sulimn.Views.Characters;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn.Views.Shopping
{
    /// <summary>Interaction logic for YeOldeMagickShoppePage.xaml</summary>
    public partial class MagickShoppePage : INotifyPropertyChanged
    {
        private List<Spell> _purchasableSpells = new List<Spell>();
        private Spell _selectedSpell = new Spell();

        /// <summary>Loads all the required data.</summary>
        private void LoadAll()
        {
            BindLabels();
            _purchasableSpells.Clear();
            _purchasableSpells.AddRange(GameState.AllSpells);
            List<Spell> learnSpells = new List<Spell>();

            foreach (Spell spell in _purchasableSpells)
            {
                if (!GameState.CurrentHero.Spellbook.Spells.Contains(spell))
                {
                    if (spell.AllowedClasses.Count == 0)
                        learnSpells.Add(spell);
                    else if (spell.AllowedClasses.Contains(GameState.CurrentHero.Class))
                        learnSpells.Add(spell);
                }
            }

            _purchasableSpells.Clear();
            _purchasableSpells = learnSpells.OrderBy(x => x.Name).ToList();
            LstSpells.ItemsSource = _purchasableSpells;
            LstSpells.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
        }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindLabels()
        {
            DataContext = _selectedSpell;
            LblGold.DataContext = GameState.CurrentHero;
        }

        protected void NotifyPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Click Methods

        private void BtnPurchase_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.Gold -= _selectedSpell.Value;
            Functions.AddTextToTextBox(TxtMagickShoppe, $"{GameState.CurrentHero.Spellbook.LearnSpell(_selectedSpell)} It cost {_selectedSpell.ValueToString} gold.");
            LoadAll();
        }

        private void BtnCharacter_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new CharacterPage());

        private void BtnBack_Click(object sender, RoutedEventArgs e) => ClosePage();

        private void LstSpells_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedSpell = LstSpells.SelectedIndex >= 0 ? (Spell)LstSpells.SelectedValue : new Spell();

            BtnPurchase.IsEnabled = _selectedSpell.Value > 0 && _selectedSpell.Value <= GameState.CurrentHero.Gold && _selectedSpell.RequiredLevel <= GameState.CurrentHero.Level;
            BindLabels();
        }

        #endregion Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage()
        {
            GameState.SaveHero(GameState.CurrentHero);
            GameState.GoBack();
        }

        public MagickShoppePage()
        {
            InitializeComponent();
            TxtMagickShoppe.Text =
            "You enter Ye Olde Magick Shoppe, a hut of a building. Inside there is a woman facing away from you, stirring a mixture in a cauldron. Sensing your presence, she turns to you, her face hideous and covered in boils.\n\n" +
            $"\"Would you like to learn some spells, {GameState.CurrentHero.Name}?\" she asks. How she knows your name is beyond you.";
        }

        private void MagickShoppePage_OnLoaded(object sender, RoutedEventArgs e) => LoadAll();

        #endregion Page-Manipulation Methods
    }
}