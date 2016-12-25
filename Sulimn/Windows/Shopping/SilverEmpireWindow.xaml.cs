using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>Interaction logic for SilverEmpireWindow.xaml</summary>
    public partial class SilverEmpireWindow : INotifyPropertyChanged
    {
        private readonly string _nl = Environment.NewLine;
        private List<Ring> _purchaseRing = new List<Ring>();
        private Ring _selectedRingPurchase = new Ring();
        private Ring _selectedRingSell = new Ring();
        private List<Ring> _sellRing = new List<Ring>();

        internal MarketWindow RefToMarketWindow { private get; set; }

        /// <summary>Adds text to the txtTheArmoury TextBox.</summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtSilverEmpire.Text += _nl + _nl + newText;
            txtSilverEmpire.Focus();
            txtSilverEmpire.CaretIndex = txtSilverEmpire.Text.Length;
            txtSilverEmpire.ScrollToEnd();
        }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindLabels()
        {
            LoadAllPurchase();
            LoadAllSell();

            lblGold.DataContext = GameState.CurrentHero.Inventory;
        }

        private void BindRingPurchase(bool reload = true)
        {
            if (reload)
            {
                _purchaseRing.Clear();
                _purchaseRing.AddRange(GameState.GetItemsOfType<Ring>().Where(ring => ring.IsSold));
                _purchaseRing = _purchaseRing.OrderBy(ring => ring.Value).ToList();
                lstRingPurchase.ItemsSource = _purchaseRing;
                lstRingPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstRingPurchase.Items.Refresh();
            }
            lblRingNamePurchase.DataContext = _selectedRingPurchase;
            lblRingBonusPurchase.DataContext = _selectedRingPurchase;
            lblRingDescriptionPurchase.DataContext = _selectedRingPurchase;
            lblRingSellablePurchase.DataContext = _selectedRingPurchase;
            lblRingValuePurchase.DataContext = _selectedRingPurchase;
        }

        private void BindRingSell(bool reload = true)
        {
            if (reload)
            {
                _sellRing.Clear();
                _sellRing.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<Ring>());
                _sellRing = _sellRing.OrderBy(ring => ring.Value).ToList();
                lstRingSell.ItemsSource = _sellRing;
                lstRingSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));
                lstRingSell.Items.Refresh();
            }
            lblRingNameSell.DataContext = _selectedRingSell;
            lblRingBonusSell.DataContext = _selectedRingSell;
            lblRingDescriptionSell.DataContext = _selectedRingSell;
            lblRingSellableSell.DataContext = _selectedRingSell;
            lblRingValueSell.DataContext = _selectedRingSell;
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Load

        internal void LoadAll()
        {
            BindLabels();
        }

        private void LoadAllPurchase()
        {
            BindRingPurchase();
        }

        private void LoadAllSell()
        {
            BindRingSell();
        }

        #endregion Load

        #region Transaction Methods

        /// <summary>Purchases selected Item.</summary>
        /// <param name="itmPurchase">Item to be purchased</param>
        /// <returns>Returns text regarding purchase</returns>
        private string Purchase(Item itmPurchase)
        {
            GameState.CurrentHero.Inventory.Gold -= itmPurchase.Value;
            GameState.CurrentHero.Inventory.AddItem(itmPurchase);
            LoadAllPurchase();
            LoadAllSell();
            return "You have purchased " + itmPurchase.Name + " for " + itmPurchase.ValueToString + " gold.";
        }

        /// <summary>Sells selected Item.</summary>
        /// <param name="itmSell">Item to be sold</param>
        /// <returns>Returns text regarding sale</returns>
        private string Sell(Item itmSell)
        {
            GameState.CurrentHero.Inventory.Gold += itmSell.SellValue;
            GameState.CurrentHero.Inventory.RemoveItem(itmSell);
            LoadAllSell();
            return "You have sold your " + itmSell.Name + " for " + itmSell.SellValueToString + " gold.";
        }

        #endregion Transaction Methods

        #region Purchase/Sell Button-Click Methods

        private void btnRingPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(_selectedRingPurchase));
            lstRingPurchase.UnselectAll();
        }

        private void btnRingSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(_selectedRingSell));
            lstRingSell.UnselectAll();
        }

        #endregion Purchase/Sell Button-Click Methods

        #region Purchase/Sell Selection Changed

        private void lstRingPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedRingPurchase = lstRingPurchase.SelectedIndex >= 0
                ? (Ring)lstRingPurchase.SelectedValue
                : new Ring();

            btnRingPurchase.IsEnabled = _selectedRingPurchase.Value <= GameState.CurrentHero.Inventory.Gold;
            BindRingPurchase(false);
        }

        private void lstRingSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedRingSell = lstRingSell.SelectedIndex >= 0 ? (Ring)lstRingSell.SelectedValue : new Ring();

            btnRingSell.IsEnabled = _selectedRingSell.CanSell;
            BindRingSell(false);
        }

        #endregion Purchase/Sell Selection Changed

        #region Window Button-Click Methods

        private void btnCharacter_Click(object sender, RoutedEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow { RefToSilverEmpireWindow = this };
            characterWindow.Show();
            characterWindow.SetupChar();
            characterWindow.SetPreviousWindow("Silver Empire");
            characterWindow.BindLabels();
            Visibility = Visibility.Hidden;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Window Button-Click Methods

        #region Window-Manipulation

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public SilverEmpireWindow()
        {
            InitializeComponent();
            txtSilverEmpire.Text =
            "You enter the impressive establishment named 'Silver Empire'. You are immediately astounded by the glass display cases unlike any other shop in Sulimn. A tough-looking old man sitting behind the counter greets you.";
            BindLabels();
        }

        private async void windowSilverEmpire_Closing(object sender, CancelEventArgs e)
        {
            RefToMarketWindow.Show();
            await GameState.SaveHero(GameState.CurrentHero);
        }

        #endregion Window-Manipulation
    }
}