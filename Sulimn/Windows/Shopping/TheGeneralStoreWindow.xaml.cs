using Extensions;
using Sulimn.Classes;
using Sulimn.Classes.Items;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn.Windows.Shopping
{
    /// <summary>Interaction logic for TheGeneralStoreWindow.xaml</summary>
    public partial class TheGeneralStoreWindow : INotifyPropertyChanged
    {
        private List<Potion> _purchasePotion = new List<Potion>();
        private Potion _selectedPotionPurchase = new Potion();
        private Potion _selectedPotionSell = new Potion();
        private List<Potion> _sellPotion = new List<Potion>();

        internal MarketWindow RefToMarketWindow { private get; set; }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindLabels()
        {
            LoadAllPurchase();
            LoadAllSell();

            LblGold.DataContext = GameState.CurrentHero.Inventory;
        }

        private void BindPotionPurchase(bool reload = true)
        {
            if (reload)
            {
                _purchasePotion.Clear();
                _purchasePotion.AddRange(GameState.GetItemsOfType<Potion>().Where(potion => potion.IsSold));
                _purchasePotion = _purchasePotion.OrderBy(potion => potion.Value).ToList();
                LstPotionPurchase.ItemsSource = _purchasePotion;
                LstPotionPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                LstPotionPurchase.Items.Refresh();
            }
            LblPotionNamePurchase.DataContext = _selectedPotionPurchase;
            LblPotionBonusPurchase.DataContext = _selectedPotionPurchase;
            LblPotionDescriptionPurchase.DataContext = _selectedPotionPurchase;
            LblPotionSellablePurchase.DataContext = _selectedPotionPurchase;
            LblPotionValuePurchase.DataContext = _selectedPotionPurchase;
        }

        private void BindPotionSell(bool reload = true)
        {
            if (reload)
            {
                _sellPotion.Clear();
                _sellPotion.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<Potion>());
                _sellPotion = _sellPotion.OrderBy(potion => potion.Value).ToList();
                LstPotionSell.ItemsSource = _sellPotion;
                LstPotionSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));
                LstPotionSell.Items.Refresh();
            }
            LblPotionNameSell.DataContext = _selectedPotionSell;
            LblPotionBonusSell.DataContext = _selectedPotionSell;
            LblPotionDescriptionSell.DataContext = _selectedPotionSell;
            LblPotionSellableSell.DataContext = _selectedPotionSell;
            LblPotionValueSell.DataContext = _selectedPotionSell;
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

        private void BtnPotionPurchase_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtTheGeneralStore, Purchase(_selectedPotionPurchase));
            LstPotionPurchase.UnselectAll();
        }

        private void BtnPotionSell_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtTheGeneralStore, Sell(_selectedPotionSell));
            LstPotionSell.UnselectAll();
        }

        #endregion Purchase/Sell Button-Click Methods

        #region Purchase/Sell Selection Changed

        private void LstPotionPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedPotionPurchase = LstPotionPurchase.SelectedIndex >= 0
            ? (Potion)LstPotionPurchase.SelectedValue
            : new Potion();

            BtnPotionPurchase.IsEnabled = _selectedPotionPurchase.Value > 0 && _selectedPotionPurchase.Value <= GameState.CurrentHero.Inventory.Gold;
            BindPotionPurchase(false);
        }

        private void LstPotionSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedPotionSell = LstPotionSell.SelectedIndex >= 0 ? (Potion)LstPotionSell.SelectedValue : new Potion();

            BtnPotionSell.IsEnabled = _selectedPotionSell.CanSell;
            BindPotionSell(false);
        }

        #endregion Purchase/Sell Selection Changed

        #region Window Button-Click Methods

        private void BtnCharacter_Click(object sender, RoutedEventArgs e)
        {
            Characters.CharacterWindow characterWindow = new Characters.CharacterWindow { RefToTheGeneralStoreWindow = this };
            characterWindow.Show();
            characterWindow.SetupChar();
            characterWindow.SetPreviousWindow("The General Store");
            characterWindow.BindLabels();
            Visibility = Visibility.Hidden;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
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
            TxtTheGeneralStore.Text =
            "You enter The General Store, a solid wooden building near the center of the market. A beautiful young woman is standing behind a counter, smiling at you. You approach her and examine her wares.";
            BindLabels();
        }

        private async void WindowTheGeneralStore_Closing(object sender, CancelEventArgs e)
        {
            RefToMarketWindow.Show();
            await GameState.SaveHero(GameState.CurrentHero);
        }

        #endregion Window-Manipulation
    }
}