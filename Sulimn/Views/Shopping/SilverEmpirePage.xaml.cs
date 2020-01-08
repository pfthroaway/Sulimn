using Extensions;
using Sulimn.Classes;
using Sulimn.Classes.Items;
using Sulimn.Views.Characters;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn.Views.Shopping
{
    /// <summary>Interaction logic for SilverEmpirePage.xaml</summary>
    public partial class SilverEmpirePage : INotifyPropertyChanged
    {
        private List<Item> _purchaseRing = new List<Item>();
        private Item _selectedRingPurchase = new Item();
        private Item _selectedRingSell = new Item();
        private List<Item> _sellRing = new List<Item>();

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindRingPurchase(bool reload = true)
        {
            if (reload)
            {
                _purchaseRing.Clear();
                _purchaseRing.AddRange(GameState.GetItemsOfType(ItemType.Ring).Where(ring => ring.IsSold));
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
                _sellRing.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.Ring));
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

        protected void NotifyPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Load

        /// <summary>Loads all the required data.</summary>
        private void LoadAll()
        {
            LoadAllPurchase();
            LoadAllSell();

            LblGold.DataContext = GameState.CurrentHero;
        }

        private void LoadAllPurchase() => BindRingPurchase();

        private void LoadAllSell() => BindRingSell();

        #endregion Load

        #region Transaction Methods

        /// <summary>Purchases selected Item.</summary>
        /// <param name="itmPurchase">Item to be purchased</param>
        /// <returns>Returns text regarding purchase</returns>
        private string Purchase(Item itmPurchase)
        {
            GameState.CurrentHero.Gold -= itmPurchase.Value;
            GameState.CurrentHero.AddItem(itmPurchase);
            LoadAllPurchase();
            LoadAllSell();
            BtnRingPurchase.IsEnabled = LstRingPurchase.SelectedIndex >= 0 && GameState.CurrentHero.Gold >= _selectedRingPurchase.Value;
            return $"You have purchased {itmPurchase.Name} for {itmPurchase.ValueToString} gold.";
        }

        /// <summary>Sells selected Item.</summary>
        /// <param name="itmSell">Item to be sold</param>
        /// <returns>Returns text regarding sale</returns>
        private string Sell(Item itmSell)
        {
            GameState.CurrentHero.Gold += itmSell.SellValue;
            GameState.CurrentHero.RemoveItem(itmSell);
            LoadAllSell();
            BtnRingPurchase.IsEnabled = LstRingPurchase.SelectedIndex >= 0 && GameState.CurrentHero.Gold >= _selectedRingPurchase.Value;
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
            ? (Item)LstRingPurchase.SelectedValue
            : new Item();

            BtnRingPurchase.IsEnabled = _selectedRingPurchase.Value > 0 && _selectedRingPurchase.Value <= GameState.CurrentHero.Gold;
            BindRingPurchase(false);
        }

        private void LstRingSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedRingSell = LstRingSell.SelectedIndex >= 0 ? (Item)LstRingSell.SelectedValue : new Item();

            BtnRingSell.IsEnabled = _selectedRingSell.CanSell;
            BindRingSell(false);
        }

        #endregion Purchase/Sell Selection Changed

        #region Page Button-Click Methods

        private void BtnCharacter_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new CharacterPage());

        private void BtnBack_Click(object sender, RoutedEventArgs e) => ClosePage();

        #endregion Page Button-Click Methods

        #region Page-Manipulation

        /// <summary>Closes the Page.</summary>
        private void ClosePage()
        {
            GameState.SaveHero(GameState.CurrentHero);
            GameState.GoBack();
        }

        public SilverEmpirePage()
        {
            InitializeComponent();
            TxtSilverEmpire.Text =
            "You enter the impressive establishment named 'Silver Empire'. You are immediately astounded by the glass display cases unlike any other shop in Sulimn. A tough-looking old man sitting behind the counter greets you.";
        }

        private void SilverEmpirePage_OnLoaded(object sender, RoutedEventArgs e) => LoadAll();

        #endregion Page-Manipulation
    }
}