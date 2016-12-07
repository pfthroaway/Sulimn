using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for TheGeneralStoreWindow.xaml
    /// </summary>
    public partial class TheGeneralStoreWindow : Window, INotifyPropertyChanged
    {
        private List<Potion> purchasePotion = new List<Potion>();
        private List<Potion> sellPotion = new List<Potion>();
        private Potion selectedPotionPurchase = new Potion();
        private Potion selectedPotionSell = new Potion();
        private string nl = Environment.NewLine;

        internal MarketWindow RefToMarketWindow { get; set; }

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
                purchasePotion.Clear();
                purchasePotion.AddRange(GameState.GetItemsOfType<Potion>().Where(potion => potion.IsSold == true));
                purchasePotion = purchasePotion.OrderBy(potion => potion.Value).ToList();
                lstPotionPurchase.ItemsSource = purchasePotion;
                lstPotionPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstPotionPurchase.Items.Refresh();
            }
            lblPotionNamePurchase.DataContext = selectedPotionPurchase;
            lblPotionBonusPurchase.DataContext = selectedPotionPurchase;
            lblPotionDescriptionPurchase.DataContext = selectedPotionPurchase;
            lblPotionSellablePurchase.DataContext = selectedPotionPurchase;
            lblPotionValuePurchase.DataContext = selectedPotionPurchase;
        }

        private void BindPotionSell(bool reload = true)
        {
            if (reload)
            {
                sellPotion.Clear();
                sellPotion.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<Potion>());
                sellPotion = sellPotion.OrderBy(potion => potion.Value).ToList();
                lstPotionSell.ItemsSource = sellPotion;
                lstPotionSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));
                lstPotionSell.Items.Refresh();
            }
            lblPotionNameSell.DataContext = selectedPotionSell;
            lblPotionBonusSell.DataContext = selectedPotionSell;
            lblPotionDescriptionSell.DataContext = selectedPotionSell;
            lblPotionSellableSell.DataContext = selectedPotionSell;
            lblPotionValueSell.DataContext = selectedPotionSell;
        }

        protected void OnPropertyChanged(string property)
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

        /// <summary>Adds text to the txtTheArmoury TextBox.</summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtTheGeneralStore.Text += nl + nl + newText;
            txtTheGeneralStore.Focus();
            txtTheGeneralStore.CaretIndex = txtTheGeneralStore.Text.Length;
            txtTheGeneralStore.ScrollToEnd();
        }

        #region Transaction Methods

        /// <summary>Purchases selected Item.</summary>
        /// <param name="itmPurchase">Item to be purchased</param>
        /// <returns></returns>
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
        /// <returns></returns>
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
            AddTextTT(Purchase(selectedPotionPurchase));
            lstPotionPurchase.UnselectAll();
        }

        private void btnPotionSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(selectedPotionSell));
            lstPotionSell.UnselectAll();
        }

        #endregion Purchase/Sell Button-Click Methods

        #region Purchase/Sell Selection Changed

        private void lstPotionPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstPotionPurchase.SelectedIndex >= 0)
            {
                selectedPotionPurchase = (Potion)lstPotionPurchase.SelectedValue;

                if (selectedPotionPurchase.Value <= GameState.CurrentHero.Inventory.Gold)
                    btnPotionPurchase.IsEnabled = true;
                else
                    btnPotionPurchase.IsEnabled = false;
            }
            else
            {
                selectedPotionPurchase = new Potion();
                btnPotionPurchase.IsEnabled = false;
            }
            BindPotionPurchase(false);
        }

        private void lstPotionSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstPotionSell.SelectedIndex >= 0)
            {
                selectedPotionSell = (Potion)lstPotionSell.SelectedValue;
                if (selectedPotionSell.CanSell)
                    btnPotionSell.IsEnabled = true;
                else
                    btnPotionSell.IsEnabled = false;
            }
            else
            {
                selectedPotionSell = new Potion();
                btnPotionSell.IsEnabled = false;
            }
            BindPotionSell(false);
        }

        #endregion Purchase/Sell Selection Changed

        #region Window Button-Click Methods

        private void btnCharacter_Click(object sender, RoutedEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow();
            characterWindow.RefToTheGeneralStoreWindow = this;
            characterWindow.Show();
            characterWindow.SetupChar();
            characterWindow.SetPreviousWindow("The General Store");
            characterWindow.BindLabels();
            this.Visibility = Visibility.Hidden;
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
            this.Close();
        }

        public TheGeneralStoreWindow()
        {
            InitializeComponent();
            txtTheGeneralStore.Text = "You enter The General Store, a solid wooden building near the center of the market. A beautiful young woman is standing behind a counter, smiling at you. You approach her and examine her wares.";
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