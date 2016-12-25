using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>Interaction logic for YeOldeMagickShoppeWindow.xaml</summary>
    public partial class MagickShoppeWindow : INotifyPropertyChanged
    {
        private readonly string _nl = Environment.NewLine;
        private List<Spell> _purchasableSpells = new List<Spell>();
        private Spell _selectedSpell = new Spell();

        internal MarketWindow RefToMarketWindow { private get; set; }

        /// <summary>Adds text to the txtMagickShoppe TextBox.</summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtMagickShoppe.Text += _nl + _nl + newText;
            txtMagickShoppe.Focus();
            txtMagickShoppe.CaretIndex = txtMagickShoppe.Text.Length;
            txtMagickShoppe.ScrollToEnd();
        }

        /// <summary>Loads all the required data.</summary>
        internal void LoadAll()
        {
            BindLabels();
            _purchasableSpells.Clear();
            _purchasableSpells.AddRange(GameState.AllSpells);
            List<Spell> learnSpells = new List<Spell>();

            foreach (Spell spell in _purchasableSpells)
                if (!GameState.CurrentHero.Spellbook.Spells.Contains(spell))
                    if (spell.RequiredClass.Length == 0)
                        learnSpells.Add(spell);
                    else if (GameState.CurrentHero.Class.Name == spell.RequiredClass)
                        learnSpells.Add(spell);

            _purchasableSpells.Clear();
            _purchasableSpells = learnSpells.OrderBy(x => x.Name).ToList();
            lstSpells.ItemsSource = _purchasableSpells;
            lstSpells.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
        }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindLabels()
        {
            DataContext = _selectedSpell;
            lblGold.DataContext = GameState.CurrentHero.Inventory;
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Button-Click Methods

        private void btnPurchase_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.Inventory.Gold -= _selectedSpell.Value;
            AddTextTT(GameState.CurrentHero.Spellbook.LearnSpell(_selectedSpell) + " It cost " +
            _selectedSpell.ValueToString + " gold.");
            LoadAll();
        }

        private void btnCharacter_Click(object sender, RoutedEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow { RefToMagickShoppeWindow = this };
            characterWindow.Show();
            characterWindow.SetupChar();
            characterWindow.SetPreviousWindow("Magick Shoppe");
            characterWindow.BindLabels();
            Visibility = Visibility.Hidden;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public MagickShoppeWindow()
        {
            InitializeComponent();
            txtMagickShoppe.Text =
            "You enter Ye Olde Magick Shoppe, a hut of a building. Inside there is a woman facing away from you, stirring a mixture in a cauldron. Sensing your presence, she turns to you, her face hideous and covered in boils." +
            _nl + _nl + "\"Would you like to learn some spells, " + GameState.CurrentHero.Name +
            "?\" she asks. How she knows your name is beyond you.";
        }

        private void lstSpells_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstSpells.SelectedIndex >= 0)
            {
                _selectedSpell = (Spell)lstSpells.SelectedValue;
                btnPurchase.IsEnabled = _selectedSpell.Value <= GameState.CurrentHero.Inventory.Gold &&
                                        _selectedSpell.RequiredLevel <= GameState.CurrentHero.Level;
            }
            else
            {
                _selectedSpell = new Spell();
                btnPurchase.IsEnabled = false;
            }
            BindLabels();
        }

        private async void windowMagickShoppe_Closing(object sender, CancelEventArgs e)
        {
            RefToMarketWindow.Show();
            await GameState.SaveHero(GameState.CurrentHero);
        }

        #endregion Window-Manipulation Methods
    }
}