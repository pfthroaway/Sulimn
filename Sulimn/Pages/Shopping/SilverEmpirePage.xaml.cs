using Extensions;
using Sulimn.Classes;
using Sulimn.Classes.Items;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn.Pages.Shopping
{
    /// <summary>Interaction logic for SilverEmpirePage.xaml</summary>
    public partial class SilverEmpirePage : INotifyPropertyChanged
    {
        private List<Ring> _purchaseRing = new List<Ring>();
        private Ring _selectedRingPurchase = new Ring();
        private Ring _selectedRingSell = new Ring();
        private List<Ring> _sellRing = new List<Ring>();

        internal MarketPage RefToMarketPage { private get; set; }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindLabels()
        {
            LoadAllPurchase();
            LoadAllSell();

            LblGold.DataContext = GameState.CurrentHero.Inventory;
        }

        private void BindRingPurchase(bool reload = true)
        {
            if (reload)
            {
                _purchaseRing.Clear();
                _purchaseRing.AddRange(GameState.GetItemsOfType<Ring>().Where(ring => ring.IsSold));
                _purchaseRing = _purchaseRing.OrderBy(ring => ring.Value).ToList();
                LstRingPurchase.ItemsSource = _purchaseRing;
                LstRingPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                LstRingPurchase.Items.Refresh();
            }
            LblRingNamePurchase.DataContext = _selectedRingPurchase;
            LblRingBonusPurchase.DataContext = _selectedRingPurchase;
            LblRingDescriptionPurchase.DataContext = _selectedRingPurchase;
            LblRingSellablePurchase.DataContext = _selectedRingPurchase;
            LblRingValuePurchase.DataContext = _selectedRingPurchase;
        }

        private void BindRingSell(bool reload = true)
        {
            if (reload)
            {
                _sellRing.Clear();
                _sellRing.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<Ring>());
                _sellRing = _sellRing.OrderBy(ring => ring.Value).ToList();
                LstRingSell.ItemsSource = _sellRing;
                LstRingSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));
                LstRingSell.Items.Refresh();
            }
            LblRingNameSell.DataContext = _selectedRingSell;
            LblRingBonusSell.DataContext = _selectedRingSell;
            LblRingDescriptionSell.DataContext = _selectedRingSell;
            LblRingSellableSell.DataContext = _selectedRingSell;
            LblRingValueSell.DataContext = _selectedRingSell;
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
            return $"You have purchased {itmPurchase.Name} for {itmPurchase.ValueToString} gold.";
        }

        /// <summary>Sells selected Item.</summary>
        /// <param name="itmSell">Item to be sold</param>
        /// <returns>Returns text regarding sale</returns>
        private string Sell(Item itmSell)
        {
            GameState.CurrentHero.Inventory.Gold += itmSell.SellValue;
            GameState.CurrentHero.Inventory.RemoveItem(itmSell);
            LoadAllSell();
            return $"You have sold your {itmSell.Name} for {itmSell.SellValueToString} gold.";
        }

        #endregion Transaction Methods

        #region Purchase/Sell Button-Click Methods

        private void BtnRingPurchase_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtSilverEmpire, Purchase(_selectedRingPurchase));
            LstRingPurchase.UnselectAll();
        }

        private void BtnRingSell_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtSilverEmpire, Sell(_selectedRingSell));
            LstRingSell.UnselectAll();
        }

        #endregion Purchase/Sell Button-Click Methods

        #region Purchase/Sell Selection Changed

        private void LstRingPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedRingPurchase = LstRingPurchase.SelectedIndex >= 0
            ? (Ring)LstRingPurchase.SelectedValue
            : new Ring();

            BtnRingPurchase.IsEnabled = _selectedRingPurchase.Value > 0 && _selectedRingPurchase.Value <= GameState.CurrentHero.Inventory.Gold;
            BindRingPurchase(false);
        }

        private void LstRingSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedRingSell = LstRingSell.SelectedIndex >= 0 ? (Ring)LstRingSell.SelectedValue : new Ring();

            BtnRingSell.IsEnabled = _selectedRingSell.CanSell;
            BindRingSell(false);
        }

        #endregion Purchase/Sell Selection Changed

        #region Page Button-Click Methods

        private void BtnCharacter_Click(object sender, RoutedEventArgs e)
        {
            Characters.CharacterPage characterPage = new Characters.CharacterPage { RefToSilverEmpirePage = this };
            characterPage.SetupChar();
            characterPage.SetPreviousPage("Silver Empire");
            characterPage.BindLabels();
            GameState.MainWindow.MainFrame.Navigate(characterPage);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }

        #endregion Page Button-Click Methods

        #region Page-Manipulation

        /// <summary>Closes the Page.</summary>
        private async void ClosePage()
        {
            await GameState.SaveHero(GameState.CurrentHero);
            GameState.MainWindow.MainFrame.GoBack();
        }

        public SilverEmpirePage()
        {
            InitializeComponent();
            TxtSilverEmpire.Text =
            "You enter the impressive establishment named 'Silver Empire'. You are immediately astounded by the glass display cases unlike any other shop in Sulimn. A tough-looking old man sitting behind the counter greets you.";
            BindLabels();
        }

        #endregion Page-Manipulation

        private void SilverEmpirePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameState.CalculateScale(Grid);
        }
    }
}