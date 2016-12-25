using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>Interaction logic for TabbedShopping.xaml</summary>
    public partial class TheArmouryWindow : INotifyPropertyChanged
    {
        internal MarketWindow RefToMarketWindow { private get; set; }

        /// <summary>Adds text to the txtTheArmoury TextBox.</summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtTheArmoury.Text += _nl + _nl + newText;
            txtTheArmoury.Focus();
            txtTheArmoury.CaretIndex = txtTheArmoury.Text.Length;
            txtTheArmoury.ScrollToEnd();
        }

        #region Local Variables

        private List<HeadArmor> _purchaseHead = new List<HeadArmor>();
        private List<HeadArmor> _sellHead = new List<HeadArmor>();
        private List<BodyArmor> _purchaseBody = new List<BodyArmor>();
        private List<BodyArmor> _sellBody = new List<BodyArmor>();
        private List<HandArmor> _purchaseHands = new List<HandArmor>();
        private List<HandArmor> _sellHands = new List<HandArmor>();
        private List<LegArmor> _purchaseLegs = new List<LegArmor>();
        private List<LegArmor> _sellLegs = new List<LegArmor>();
        private List<FeetArmor> _purchaseFeet = new List<FeetArmor>();
        private List<FeetArmor> _sellFeet = new List<FeetArmor>();
        private HeadArmor _selectedHeadPurchase = new HeadArmor();
        private HeadArmor _selectedHeadSell = new HeadArmor();
        private BodyArmor _selectedBodyPurchase = new BodyArmor();
        private BodyArmor _selectedBodySell = new BodyArmor();
        private HandArmor _selectedHandsPurchase = new HandArmor();
        private HandArmor _selectedHandsSell = new HandArmor();
        private LegArmor _selectedLegsPurchase = new LegArmor();
        private LegArmor _selectedLegsSell = new LegArmor();
        private FeetArmor _selectedFeetPurchase = new FeetArmor();
        private FeetArmor _selectedFeetSell = new FeetArmor();
        private readonly string _nl = Environment.NewLine;

        #endregion Local Variables

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindLabels()
        {
            lblHeadNamePurchase.DataContext = _selectedHeadPurchase;
            lblHeadDefensePurchase.DataContext = _selectedHeadPurchase;
            lblHeadDescriptionPurchase.DataContext = _selectedHeadPurchase;
            lblHeadSellablePurchase.DataContext = _selectedHeadPurchase;
            lblHeadValuePurchase.DataContext = _selectedHeadPurchase;
            lblHeadNameSell.DataContext = _selectedHeadSell;
            lblHeadDefenseSell.DataContext = _selectedHeadSell;
            lblHeadDescriptionSell.DataContext = _selectedHeadSell;
            lblHeadSellableSell.DataContext = _selectedHeadSell;
            lblHeadValueSell.DataContext = _selectedHeadSell;

            lblBodyNamePurchase.DataContext = _selectedBodyPurchase;
            lblBodyDefensePurchase.DataContext = _selectedBodyPurchase;
            lblBodyDescriptionPurchase.DataContext = _selectedBodyPurchase;
            lblBodySellablePurchase.DataContext = _selectedBodyPurchase;
            lblBodyValuePurchase.DataContext = _selectedBodyPurchase;
            lblBodyNameSell.DataContext = _selectedBodySell;
            lblBodyDefenseSell.DataContext = _selectedBodySell;
            lblBodyDescriptionSell.DataContext = _selectedBodySell;
            lblBodySellableSell.DataContext = _selectedBodySell;
            lblBodyValueSell.DataContext = _selectedBodySell;

            lblHandsNamePurchase.DataContext = _selectedHandsPurchase;
            lblHandsDefensePurchase.DataContext = _selectedHandsPurchase;
            lblHandsDescriptionPurchase.DataContext = _selectedHandsPurchase;
            lblHandsSellablePurchase.DataContext = _selectedHandsPurchase;
            lblHandsValuePurchase.DataContext = _selectedHandsPurchase;
            lblHandsNameSell.DataContext = _selectedHandsSell;
            lblHandsDefenseSell.DataContext = _selectedHandsSell;
            lblHandsDescriptionSell.DataContext = _selectedHandsSell;
            lblHandsSellableSell.DataContext = _selectedHandsSell;
            lblHandsValueSell.DataContext = _selectedHandsSell;

            lblLegsNamePurchase.DataContext = _selectedLegsPurchase;
            lblLegsDefensePurchase.DataContext = _selectedLegsPurchase;
            lblLegsDescriptionPurchase.DataContext = _selectedLegsPurchase;
            lblLegsSellablePurchase.DataContext = _selectedLegsPurchase;
            lblLegsValuePurchase.DataContext = _selectedLegsPurchase;
            lblLegsNameSell.DataContext = _selectedLegsSell;
            lblLegsDefenseSell.DataContext = _selectedLegsSell;
            lblLegsDescriptionSell.DataContext = _selectedLegsSell;
            lblLegsSellableSell.DataContext = _selectedLegsSell;
            lblLegsValueSell.DataContext = _selectedLegsSell;

            lblFeetNamePurchase.DataContext = _selectedFeetPurchase;
            lblFeetDefensePurchase.DataContext = _selectedFeetPurchase;
            lblFeetDescriptionPurchase.DataContext = _selectedFeetPurchase;
            lblFeetSellablePurchase.DataContext = _selectedFeetPurchase;
            lblFeetValuePurchase.DataContext = _selectedFeetPurchase;
            lblFeetNameSell.DataContext = _selectedFeetSell;
            lblFeetDefenseSell.DataContext = _selectedFeetSell;
            lblFeetDescriptionSell.DataContext = _selectedFeetSell;
            lblFeetSellableSell.DataContext = _selectedFeetSell;
            lblFeetValueSell.DataContext = _selectedFeetSell;

            lblGold.DataContext = GameState.CurrentHero.Inventory;
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Load Methods

        /// <summary>Loads everything for data binding.</summary>
        internal void LoadAll()
        {
            lstHeadPurchase.ItemsSource = _purchaseHead;
            lstHeadSell.ItemsSource = _sellHead;
            lstBodyPurchase.ItemsSource = _purchaseBody;
            lstBodySell.ItemsSource = _sellBody;
            lstHandsPurchase.ItemsSource = _purchaseHands;
            lstHandsSell.ItemsSource = _sellHands;
            lstLegsPurchase.ItemsSource = _purchaseLegs;
            lstLegsSell.ItemsSource = _sellLegs;
            lstFeetPurchase.ItemsSource = _purchaseFeet;
            lstFeetSell.ItemsSource = _sellFeet;
            LoadAllPurchase();
            LoadAllSell();
            BindLabels();
        }

        /// <summary>Loads the appropriate List and displays its contents in the appropriate ListBox.</summary>
        private void LoadAllPurchase()
        {
            _purchaseHead.Clear();
            _purchaseHead.AddRange(GameState.GetItemsOfType<HeadArmor>().Where(armor => armor.IsSold));
            _purchaseHead = _purchaseHead.OrderBy(armor => armor.Value).ToList();
            lstHeadPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));

            _purchaseBody.Clear();
            _purchaseBody.AddRange(GameState.GetItemsOfType<BodyArmor>().Where(armor => armor.IsSold));
            _purchaseBody = _purchaseBody.OrderBy(armor => armor.Value).ToList();
            lstBodyPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));

            _purchaseHands.Clear();
            _purchaseHands.AddRange(GameState.GetItemsOfType<HandArmor>().Where(armor => armor.IsSold));
            _purchaseHands = _purchaseHands.OrderBy(armor => armor.Value).ToList();
            lstHandsPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));

            _purchaseLegs.Clear();
            _purchaseLegs.AddRange(GameState.GetItemsOfType<LegArmor>().Where(armor => armor.IsSold));
            _purchaseLegs = _purchaseLegs.OrderBy(armor => armor.Value).ToList();
            lstLegsPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));

            _purchaseFeet.Clear();
            _purchaseFeet.AddRange(GameState.GetItemsOfType<FeetArmor>().Where(armor => armor.IsSold));
            _purchaseFeet = _purchaseFeet.OrderBy(armor => armor.Value).ToList();
            lstFeetPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));

            LoadAllSell();
        }

        /// <summary>Loads the Hero's inventory into a List and displays its contents in the appropriate TextBox.</summary>
        private void LoadAllSell()
        {
            _sellHead.Clear();
            _sellHead.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<HeadArmor>().Where(armor => armor.IsSold));
            _sellHead = _sellHead.OrderBy(armor => armor.Value).ToList();
            lstHeadSell.ItemsSource = _sellHead;
            lstHeadSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));

            _sellBody.Clear();
            _sellBody.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<BodyArmor>().Where(armor => armor.IsSold));
            _sellBody = _sellBody.OrderBy(armor => armor.Value).ToList();
            lstBodySell.ItemsSource = _sellBody;
            lstBodySell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));

            _sellHands.Clear();
            _sellHands.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<HandArmor>().Where(armor => armor.IsSold));
            _sellHands = _sellHands.OrderBy(armor => armor.Value).ToList();
            lstHandsSell.ItemsSource = _sellHands;
            lstHandsSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));

            _sellLegs.Clear();
            _sellLegs.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<LegArmor>().Where(armor => armor.IsSold));
            _sellLegs = _sellLegs.OrderBy(armor => armor.Value).ToList();
            lstLegsSell.ItemsSource = _sellLegs;
            lstLegsSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));

            _sellFeet.Clear();
            _sellFeet.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<FeetArmor>().Where(armor => armor.IsSold));
            _sellFeet = _sellFeet.OrderBy(armor => armor.Value).ToList();
            lstFeetSell.ItemsSource = _sellFeet;
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
            AddTextTT(Purchase(_selectedHeadPurchase));
            LoadAllPurchase();
            lstHeadPurchase.UnselectAll();
        }

        private void btnHeadSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(_selectedHeadSell));
            LoadAllSell();
            lstHeadSell.UnselectAll();
        }

        private void btnBodyPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(_selectedBodyPurchase));
            LoadAllPurchase();
            lstBodyPurchase.UnselectAll();
        }

        private void btnBodySell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(_selectedBodySell));
            LoadAllSell();
            lstBodySell.UnselectAll();
        }

        private void btnHandsPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(_selectedHandsPurchase));
            LoadAllPurchase();
            lstHandsPurchase.UnselectAll();
        }

        private void btnHandsSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(_selectedHandsSell));
            LoadAllSell();
            lstHandsSell.UnselectAll();
        }

        private void btnLegsPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(_selectedLegsPurchase));
            LoadAllPurchase();
            lstLegsPurchase.UnselectAll();
        }

        private void btnLegsSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(_selectedLegsSell));
            LoadAllSell();
            lstLegsSell.UnselectAll();
        }

        private void btnFeetPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(_selectedFeetPurchase));
            LoadAllPurchase();
            lstFeetPurchase.UnselectAll();
        }

        private void btnFeetSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(_selectedFeetSell));
            LoadAllSell();
            lstFeetSell.UnselectAll();
        }

        #endregion Purchase/Sell Button-Click Methods

        #region Button-Click Methods

        private void btnCharacter_Click(object sender, RoutedEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow { RefToTheArmouryWindow = this };
            characterWindow.Show();
            characterWindow.SetupChar();
            characterWindow.SetPreviousWindow("The Armoury");
            characterWindow.BindLabels();
            Visibility = Visibility.Hidden;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Purchase/Sell Selection Changed

        private void lstHeadPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedHeadPurchase = lstHeadPurchase.SelectedIndex >= 0
                ? (HeadArmor)lstHeadPurchase.SelectedValue
                : new HeadArmor();

            btnHeadPurchase.IsEnabled = _selectedHeadPurchase.Value > 0 && _selectedHeadPurchase.Value <= GameState.CurrentHero.Inventory.Gold;
            BindLabels();
        }

        private void lstHeadSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedHeadSell = lstHeadSell.SelectedIndex >= 0 ? (HeadArmor)lstHeadSell.SelectedValue : new HeadArmor();

            btnHeadSell.IsEnabled = _selectedHeadSell.CanSell;
            BindLabels();
        }

        private void lstBodyPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedBodyPurchase = lstBodyPurchase.SelectedIndex >= 0
                ? (BodyArmor)lstBodyPurchase.SelectedValue
                : new BodyArmor();

            btnBodyPurchase.IsEnabled = _selectedBodyPurchase.Value > 0 && _selectedBodyPurchase.Value <= GameState.CurrentHero.Inventory.Gold;
            BindLabels();
        }

        private void lstBodySell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedBodySell = lstBodySell.SelectedIndex >= 0 ? (BodyArmor)lstBodySell.SelectedValue : new BodyArmor();

            btnBodySell.IsEnabled = _selectedBodySell.CanSell;
            BindLabels();
        }

        private void lstHandsPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedHandsPurchase = lstHandsPurchase.SelectedIndex >= 0
                ? (HandArmor)lstHandsPurchase.SelectedValue
                : new HandArmor();

            btnHandsPurchase.IsEnabled = _selectedHandsPurchase.Value > 0 && _selectedHandsPurchase.Value <= GameState.CurrentHero.Inventory.Gold;
            BindLabels();
        }

        private void lstHandsSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedHandsSell = lstHandsSell.SelectedIndex >= 0
                ? (HandArmor)lstHandsSell.SelectedValue
                : new HandArmor();

            btnHandsSell.IsEnabled = _selectedHandsSell.CanSell;
            BindLabels();
        }

        private void lstLegsPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedLegsPurchase = lstLegsPurchase.SelectedIndex >= 0
                ? (LegArmor)lstLegsPurchase.SelectedValue
                : new LegArmor();

            btnLegsPurchase.IsEnabled = _selectedLegsPurchase.Value > 0 && _selectedLegsPurchase.Value <= GameState.CurrentHero.Inventory.Gold;
            BindLabels();
        }

        private void lstLegsSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedLegsSell = lstLegsSell.SelectedIndex >= 0 ? (LegArmor)lstLegsSell.SelectedValue : new LegArmor();

            btnLegsSell.IsEnabled = _selectedLegsSell.CanSell;
            BindLabels();
        }

        private void lstFeetPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedFeetPurchase = lstFeetPurchase.SelectedIndex >= 0
                ? (FeetArmor)lstFeetPurchase.SelectedValue
                : new FeetArmor();

            btnFeetPurchase.IsEnabled = _selectedFeetPurchase.Value > 0 && _selectedFeetPurchase.Value <= GameState.CurrentHero.Inventory.Gold;
            BindLabels();
        }

        private void lstFeetSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedFeetSell = lstFeetSell.SelectedIndex >= 0 ? (FeetArmor)lstFeetSell.SelectedValue : new FeetArmor();

            btnFeetSell.IsEnabled = _selectedFeetSell.CanSell;
            BindLabels();
        }

        #endregion Purchase/Sell Selection Changed

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public TheArmouryWindow()
        {
            InitializeComponent();
            LoadAll();
            txtTheArmoury.Text =
            "You enter The Armoury, an old, solid brick building filled with armor pieces of various shapes, sizes, and materials. The shopkeeper beckons you over to examine his wares.";
        }

        private async void windowTheArmoury_Closing(object sender, CancelEventArgs e)
        {
            RefToMarketWindow.Show();
            await GameState.SaveHero(GameState.CurrentHero);
        }

        #endregion Window-Manipulation Methods
    }
}