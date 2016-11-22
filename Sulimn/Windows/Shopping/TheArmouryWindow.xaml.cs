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
    /// Interaction logic for TabbedShopping.xaml
    /// </summary>
    public partial class TheArmouryWindow : Window, INotifyPropertyChanged
    {
        #region Local Variables

        private List<HeadArmor> purchaseHead = new List<HeadArmor>();
        private List<HeadArmor> sellHead = new List<HeadArmor>();
        private List<BodyArmor> purchaseBody = new List<BodyArmor>();
        private List<BodyArmor> sellBody = new List<BodyArmor>();
        private List<HandArmor> purchaseHands = new List<HandArmor>();
        private List<HandArmor> sellHands = new List<HandArmor>();
        private List<LegArmor> purchaseLegs = new List<LegArmor>();
        private List<LegArmor> sellLegs = new List<LegArmor>();
        private List<FeetArmor> purchaseFeet = new List<FeetArmor>();
        private List<FeetArmor> sellFeet = new List<FeetArmor>();
        private HeadArmor selectedHeadPurchase = new HeadArmor();
        private HeadArmor selectedHeadSell = new HeadArmor();
        private BodyArmor selectedBodyPurchase = new BodyArmor();
        private BodyArmor selectedBodySell = new BodyArmor();
        private HandArmor selectedHandsPurchase = new HandArmor();
        private HandArmor selectedHandsSell = new HandArmor();
        private LegArmor selectedLegsPurchase = new LegArmor();
        private LegArmor selectedLegsSell = new LegArmor();
        private FeetArmor selectedFeetPurchase = new FeetArmor();
        private FeetArmor selectedFeetSell = new FeetArmor();
        private string nl = Environment.NewLine;

        #endregion Local Variables

        internal MarketWindow RefToMarketWindow { get; set; }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindLabels()
        {
            lblHeadNamePurchase.DataContext = selectedHeadPurchase;
            lblHeadDefensePurchase.DataContext = selectedHeadPurchase;
            lblHeadDescriptionPurchase.DataContext = selectedHeadPurchase;
            lblHeadSellablePurchase.DataContext = selectedHeadPurchase;
            lblHeadValuePurchase.DataContext = selectedHeadPurchase;
            lblHeadNameSell.DataContext = selectedHeadSell;
            lblHeadDefenseSell.DataContext = selectedHeadSell;
            lblHeadDescriptionSell.DataContext = selectedHeadSell;
            lblHeadSellableSell.DataContext = selectedHeadSell;
            lblHeadValueSell.DataContext = selectedHeadSell;

            lblBodyNamePurchase.DataContext = selectedBodyPurchase;
            lblBodyDefensePurchase.DataContext = selectedBodyPurchase;
            lblBodyDescriptionPurchase.DataContext = selectedBodyPurchase;
            lblBodySellablePurchase.DataContext = selectedBodyPurchase;
            lblBodyValuePurchase.DataContext = selectedBodyPurchase;
            lblBodyNameSell.DataContext = selectedBodySell;
            lblBodyDefenseSell.DataContext = selectedBodySell;
            lblBodyDescriptionSell.DataContext = selectedBodySell;
            lblBodySellableSell.DataContext = selectedBodySell;
            lblBodyValueSell.DataContext = selectedBodySell;

            lblHandsNamePurchase.DataContext = selectedHandsPurchase;
            lblHandsDefensePurchase.DataContext = selectedHandsPurchase;
            lblHandsDescriptionPurchase.DataContext = selectedHandsPurchase;
            lblHandsSellablePurchase.DataContext = selectedHandsPurchase;
            lblHandsValuePurchase.DataContext = selectedHandsPurchase;
            lblHandsNameSell.DataContext = selectedHandsSell;
            lblHandsDefenseSell.DataContext = selectedHandsSell;
            lblHandsDescriptionSell.DataContext = selectedHandsSell;
            lblHandsSellableSell.DataContext = selectedHandsSell;
            lblHandsValueSell.DataContext = selectedHandsSell;

            lblLegsNamePurchase.DataContext = selectedLegsPurchase;
            lblLegsDefensePurchase.DataContext = selectedLegsPurchase;
            lblLegsDescriptionPurchase.DataContext = selectedLegsPurchase;
            lblLegsSellablePurchase.DataContext = selectedLegsPurchase;
            lblLegsValuePurchase.DataContext = selectedLegsPurchase;
            lblLegsNameSell.DataContext = selectedLegsSell;
            lblLegsDefenseSell.DataContext = selectedLegsSell;
            lblLegsDescriptionSell.DataContext = selectedLegsSell;
            lblLegsSellableSell.DataContext = selectedLegsSell;
            lblLegsValueSell.DataContext = selectedLegsSell;

            lblFeetNamePurchase.DataContext = selectedFeetPurchase;
            lblFeetDefensePurchase.DataContext = selectedFeetPurchase;
            lblFeetDescriptionPurchase.DataContext = selectedFeetPurchase;
            lblFeetSellablePurchase.DataContext = selectedFeetPurchase;
            lblFeetValuePurchase.DataContext = selectedFeetPurchase;
            lblFeetNameSell.DataContext = selectedFeetSell;
            lblFeetDefenseSell.DataContext = selectedFeetSell;
            lblFeetDescriptionSell.DataContext = selectedFeetSell;
            lblFeetSellableSell.DataContext = selectedFeetSell;
            lblFeetValueSell.DataContext = selectedFeetSell;

            lblGold.DataContext = GameState.CurrentHero.Inventory;
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        /// <summary>Adds text to the txtTheArmoury TextBox.</summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtTheArmoury.Text += nl + nl + newText;
            txtTheArmoury.Focus();
            txtTheArmoury.CaretIndex = txtTheArmoury.Text.Length;
            txtTheArmoury.ScrollToEnd();
        }

        #region Load Methods

        /// <summary>Loads everything for data binding.</summary>
        internal void LoadAll()
        {
            lstHeadPurchase.ItemsSource = purchaseHead;
            lstHeadSell.ItemsSource = sellHead;
            lstBodyPurchase.ItemsSource = purchaseBody;
            lstBodySell.ItemsSource = sellBody;
            lstHandsPurchase.ItemsSource = purchaseHands;
            lstHandsSell.ItemsSource = sellHands;
            lstLegsPurchase.ItemsSource = purchaseLegs;
            lstLegsSell.ItemsSource = sellLegs;
            lstFeetPurchase.ItemsSource = purchaseFeet;
            lstFeetSell.ItemsSource = sellFeet;
            LoadAllPurchase();
            LoadAllSell();
            BindLabels();
        }

        /// <summary>Loads the appropriate List and displays its contents in the appropriate ListBox.</summary>
        private void LoadAllPurchase()
        {
            purchaseHead.Clear();
            purchaseHead.AddRange(GameState.GetItemsOfType<HeadArmor>().Where(armor => armor.IsSold == true));
            purchaseHead = purchaseHead.OrderBy(armor => armor.Value).ToList();
            lstHeadPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));

            purchaseBody.Clear();
            purchaseBody.AddRange(GameState.GetItemsOfType<BodyArmor>().Where(armor => armor.IsSold == true));
            purchaseBody = purchaseBody.OrderBy(armor => armor.Value).ToList();
            lstBodyPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));

            purchaseHands.Clear();
            purchaseHands.AddRange(GameState.GetItemsOfType<HandArmor>().Where(armor => armor.IsSold == true));
            purchaseHands = purchaseHands.OrderBy(armor => armor.Value).ToList();
            lstHandsPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));

            purchaseLegs.Clear();
            purchaseLegs.AddRange(GameState.GetItemsOfType<LegArmor>().Where(armor => armor.IsSold == true));
            purchaseLegs = purchaseLegs.OrderBy(armor => armor.Value).ToList();
            lstLegsPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));

            purchaseFeet.Clear();
            purchaseFeet.AddRange(GameState.GetItemsOfType<FeetArmor>().Where(armor => armor.IsSold == true));
            purchaseFeet = purchaseFeet.OrderBy(armor => armor.Value).ToList();
            lstFeetPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));

            LoadAllSell();
        }

        /// <summary>Loads the Hero's inventory into a List and displays its contents in the appropriate TextBox.</summary>
        private void LoadAllSell()
        {
            sellHead.Clear();
            sellHead.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<HeadArmor>().Where(armor => armor.IsSold == true));
            sellHead = sellHead.OrderBy(armor => armor.Value).ToList();
            lstHeadSell.ItemsSource = sellHead;
            lstHeadSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));

            sellBody.Clear();
            sellBody.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<BodyArmor>().Where(armor => armor.IsSold == true));
            sellBody = sellBody.OrderBy(armor => armor.Value).ToList();
            lstBodySell.ItemsSource = sellBody;
            lstBodySell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));

            sellHands.Clear();
            sellHands.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<HandArmor>().Where(armor => armor.IsSold == true));
            sellHands = sellHands.OrderBy(armor => armor.Value).ToList();
            lstHandsSell.ItemsSource = sellHands;
            lstHandsSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));

            sellLegs.Clear();
            sellLegs.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<LegArmor>().Where(armor => armor.IsSold == true));
            sellLegs = sellLegs.OrderBy(armor => armor.Value).ToList();
            lstLegsSell.ItemsSource = sellLegs;
            lstLegsSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));

            sellFeet.Clear();
            sellFeet.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<FeetArmor>().Where(armor => armor.IsSold == true));
            sellFeet = sellFeet.OrderBy(armor => armor.Value).ToList();
            lstFeetSell.ItemsSource = sellFeet;
            lstFeetSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));

            lstHeadSell.Items.Refresh();
            lstBodySell.Items.Refresh();
            lstHandsSell.Items.Refresh();
            lstLegsSell.Items.Refresh();
            lstFeetSell.Items.Refresh();
        }

        #endregion Load Methods

        #region Transaction Methods

        /// <summary>Purchases selected Item.</summary>
        /// <param name="itmPurchase">Item to be purchased</param>
        /// <returns></returns>
        private string Purchase(Item itmPurchase)
        {
            GameState.CurrentHero.Inventory.Gold -= itmPurchase.Value;
            GameState.CurrentHero.Inventory.AddItem(itmPurchase);
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

        private void btnHeadPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(selectedHeadPurchase));
            LoadAllPurchase();
            lstHeadPurchase.UnselectAll();
        }

        private void btnHeadSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(selectedHeadSell));
            LoadAllSell();
            lstHeadSell.UnselectAll();
        }

        private void btnBodyPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(selectedBodyPurchase));
            LoadAllPurchase();
            lstBodyPurchase.UnselectAll();
        }

        private void btnBodySell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(selectedBodySell));
            LoadAllSell();
            lstBodySell.UnselectAll();
        }

        private void btnHandsPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(selectedHandsPurchase));
            LoadAllPurchase();
            lstHandsPurchase.UnselectAll();
        }

        private void btnHandsSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(selectedHandsSell));
            LoadAllSell();
            lstHandsSell.UnselectAll();
        }

        private void btnLegsPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(selectedLegsPurchase));
            LoadAllPurchase();
            lstLegsPurchase.UnselectAll();
        }

        private void btnLegsSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(selectedLegsSell));
            LoadAllSell();
            lstLegsSell.UnselectAll();
        }

        private void btnFeetPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(selectedFeetPurchase));
            LoadAllPurchase();
            lstFeetPurchase.UnselectAll();
        }

        private void btnFeetSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(selectedFeetSell));
            LoadAllSell();
            lstFeetSell.UnselectAll();
        }

        #endregion Purchase/Sell Button-Click Methods

        #region Button-Click Methods

        private void btnCharacter_Click(object sender, RoutedEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow();
            characterWindow.RefToTheArmouryWindow = this;
            characterWindow.Show();
            characterWindow.SetupChar();
            characterWindow.SetPreviousWindow("The Armoury");
            characterWindow.BindLabels();
            this.Visibility = Visibility.Hidden;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Purchase/Sell Selection Changed

        private void lstHeadPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstHeadPurchase.SelectedIndex >= 0)
            {
                selectedHeadPurchase = (HeadArmor)lstHeadPurchase.SelectedValue;

                if (selectedHeadPurchase.Value <= GameState.CurrentHero.Inventory.Gold)
                    btnHeadPurchase.IsEnabled = true;
                else
                    btnHeadPurchase.IsEnabled = false;
            }
            else
            {
                selectedHeadPurchase = new HeadArmor();
                btnHeadPurchase.IsEnabled = false;
            }
            BindLabels();
        }

        private void lstHeadSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstHeadSell.SelectedIndex >= 0)
            {
                selectedHeadSell = (HeadArmor)lstHeadSell.SelectedValue;
                btnHeadSell.IsEnabled = true;
            }
            else
            {
                selectedHeadSell = new HeadArmor();
                btnHeadSell.IsEnabled = false;
            }
            BindLabels();
        }

        private void lstBodyPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBodyPurchase.SelectedIndex >= 0)
            {
                selectedBodyPurchase = (BodyArmor)lstBodyPurchase.SelectedValue;

                if (selectedBodyPurchase.Value <= GameState.CurrentHero.Inventory.Gold)
                    btnBodyPurchase.IsEnabled = true;
                else
                    btnBodyPurchase.IsEnabled = false;
            }
            else
            {
                selectedBodyPurchase = new BodyArmor();
                btnBodyPurchase.IsEnabled = false;
            }
            BindLabels();
        }

        private void lstBodySell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBodySell.SelectedIndex >= 0)
            {
                selectedBodySell = (BodyArmor)lstBodySell.SelectedValue;
                btnBodySell.IsEnabled = true;
            }
            else
            {
                selectedBodySell = new BodyArmor();
                btnBodySell.IsEnabled = false;
            }
            BindLabels();
        }

        private void lstHandsPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstHandsPurchase.SelectedIndex >= 0)
            {
                selectedHandsPurchase = (HandArmor)lstHandsPurchase.SelectedValue;

                if (selectedHandsPurchase.Value <= GameState.CurrentHero.Inventory.Gold)
                    btnHandsPurchase.IsEnabled = true;
                else
                    btnHandsPurchase.IsEnabled = false;
            }
            else
            {
                selectedHandsPurchase = new HandArmor();
                btnHandsPurchase.IsEnabled = false;
            }
            BindLabels();
        }

        private void lstHandsSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstHandsSell.SelectedIndex >= 0)
            {
                selectedHandsSell = (HandArmor)lstHandsSell.SelectedValue;

                btnHandsSell.IsEnabled = true;
            }
            else
            {
                selectedHandsSell = new HandArmor();
                btnHandsSell.IsEnabled = false;
            }
            BindLabels();
        }

        private void lstLegsPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstLegsPurchase.SelectedIndex >= 0)
            {
                selectedLegsPurchase = (LegArmor)lstLegsPurchase.SelectedValue;

                if (selectedLegsPurchase.Value <= GameState.CurrentHero.Inventory.Gold)
                    btnLegsPurchase.IsEnabled = true;
                else
                    btnLegsPurchase.IsEnabled = false;
            }
            else
            {
                selectedLegsPurchase = new LegArmor();
                btnLegsPurchase.IsEnabled = false;
            }
            BindLabels();
        }

        private void lstLegsSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstLegsSell.SelectedIndex >= 0)
            {
                selectedLegsSell = (LegArmor)lstLegsSell.SelectedValue;
                btnLegsSell.IsEnabled = true;
            }
            else
            {
                selectedLegsSell = new LegArmor();
                btnLegsSell.IsEnabled = false;
            }
            BindLabels();
        }

        private void lstFeetPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFeetPurchase.SelectedIndex >= 0)
            {
                selectedFeetPurchase = (FeetArmor)lstFeetPurchase.SelectedValue;

                if (selectedFeetPurchase.Value <= GameState.CurrentHero.Inventory.Gold)
                    btnFeetPurchase.IsEnabled = true;
                else
                    btnFeetPurchase.IsEnabled = false;
            }
            else
            {
                selectedFeetPurchase = new FeetArmor();
                btnFeetPurchase.IsEnabled = false;
            }
            BindLabels();
        }

        private void lstFeetSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFeetSell.SelectedIndex >= 0)
            {
                selectedFeetSell = (FeetArmor)lstFeetSell.SelectedValue;
                btnFeetSell.IsEnabled = true;
            }
            else
            {
                selectedFeetSell = new FeetArmor();
                btnFeetSell.IsEnabled = false;
            }
            BindLabels();
        }

        #endregion Purchase/Sell Selection Changed

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            this.Close();
        }

        public TheArmouryWindow()
        {
            InitializeComponent();
            LoadAll();
            txtTheArmoury.Text = "You enter The Armoury, an old, solid brick building filled with armor pieces of various shapes, sizes, and materials. The shopkeeper beckons you over to examine his wares.";
        }

        private async void windowTheArmoury_Closing(object sender, CancelEventArgs e)
        {
            RefToMarketWindow.Show();
            await GameState.SaveHero(GameState.CurrentHero);
        }

        #endregion Window-Manipulation Methods
    }
}