using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>Interaction logic for TheGeneralStoreWindow.xaml</summary>
    public partial class TheGeneralStoreWindow : INotifyPropertyChanged
    {
        private readonly string _nl = Environment.NewLine;
        private List<Potion> _purchasePotion = new List<Potion>();
        private Potion _selectedPotionPurchase = new Potion();
        private Potion _selectedPotionSell = new Potion();
        private List<Potion> _sellPotion = new List<Potion>();

        internal MarketWindow RefToMarketWindow { private get; set; }

        /// <summary>Adds text to the txtTheArmoury TextBox.</summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtTheGeneralStore.Text += _nl + _nl + newText;
            txtTheGeneralStore.Focus();
            txtTheGeneralStore.CaretIndex = txtTheGeneralStore.Text.Length;
            txtTheGeneralStore.ScrollToEnd();
        }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindLabels()
        {
            LoadAllPurchase();
            LoadAllSell();

            lblGold.DataContext = GameState.CurrentHero.Inventory;
        }

        private void BindPotionPurchase(bool reload = true)
        {
            if (reload)
            {
                _purchasePotion.Clear();
                _purchasePotion.AddRange(GameState.GetItemsOfType<Potion>().Where(potion => potion.IsSold));
                _purchasePotion = _purchasePotion.OrderBy(potion => potion.Value).ToList();
                lstPotionPurchase.ItemsSource = _purchasePotion;
                lstPotionPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstPotionPurchase.Items.Refresh();
            }
            lblPotionNamePurchase.DataContext = _selectedPotionPurchase;
            lblPotionBonusPurchase.DataContext = _selectedPotionPurchase;
            lblPotionDescriptionPurchase.DataContext = _selectedPotionPurchase;
            lblPotionSellablePurchase.DataContext = _selectedPotionPurchase;
            lblPotionValuePurchase.DataContext = _selectedPotionPurchase;
        }

        private void BindPotionSell(bool reload = true)
        {
            if (reload)
            {
                _sellPotion.Clear();
                _sellPotion.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<Potion>());
                _sellPotion = _sellPotion.OrderBy(potion => potion.Value).ToList();
                lstPotionSell.ItemsSource = _sellPotion;
                lstPotionSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));
                lstPotionSell.Items.Refresh();
            }
            lblPotionNameSell.DataContext = _selectedPotionSell;
            lblPotionBonusSell.DataContext = _selectedPotionSell;
            lblPotionDescriptionSell.DataContext = _selectedPotionSell;
            lblPotionSellableSell.DataContext = _selectedPotionSell;
            lblPotionValueSell.DataContext = _selectedPotionSell;
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
            BindPotionPurchase();
        }

        private void LoadAllSell()
        {
            BindPotionSell();
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

        private void btnPotionPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(_selectedPotionPurchase));
            lstPotionPurchase.UnselectAll();
        }

        private void btnPotionSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(_selectedPotionSell));
            lstPotionSell.UnselectAll();
        }

        #endregion Purchase/Sell Button-Click Methods

        #region Purchase/Sell Selection Changed

        private void lstPotionPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedPotionPurchase = lstPotionPurchase.SelectedIndex >= 0
                ? (Potion)lstPotionPurchase.SelectedValue
                : new Potion();

            btnPotionPurchase.IsEnabled = _selectedPotionPurchase.Value <= GameState.CurrentHero.Inventory.Gold;
            BindPotionPurchase(false);
        }

        private void lstPotionSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedPotionSell = lstPotionSell.SelectedIndex >= 0 ? (Potion)lstPotionSell.SelectedValue : new Potion();

            btnPotionSell.IsEnabled = _selectedPotionSell.CanSell;
            BindPotionSell(false);
        }

        #endregion Purchase/Sell Selection Changed

        #region Window Button-Click Methods

        private void btnCharacter_Click(object sender, RoutedEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow { RefToTheGeneralStoreWindow = this };
            characterWindow.Show();
            characterWindow.SetupChar();
            characterWindow.SetPreviousWindow("The General Store");
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

        public TheGeneralStoreWindow()
        {
            InitializeComponent();
            txtTheGeneralStore.Text =
            "You enter The General Store, a solid wooden building near the center of the market. A beautiful young woman is standing behind a counter, smiling at you. You approach her and examine her wares.";
            BindLabels();
        }

        private async void windowTheGeneralStore_Closing(object sender, CancelEventArgs e)
        {
            RefToMarketWindow.Show();
            await GameState.SaveHero(GameState.CurrentHero);
        }

        #endregion Window-Manipulation
    }
}