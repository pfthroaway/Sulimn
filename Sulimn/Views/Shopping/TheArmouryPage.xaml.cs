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
    /// <summary>Interaction logic for TabbedShopping.xaml</summary>
    public partial class TheArmouryPage : INotifyPropertyChanged
    {
        #region Local Variables

        private List<Item> _purchaseHead = new List<Item>();
        private List<Item> _sellHead = new List<Item>();
        private List<Item> _purchaseBody = new List<Item>();
        private List<Item> _sellBody = new List<Item>();
        private List<Item> _purchaseHands = new List<Item>();
        private List<Item> _sellHands = new List<Item>();
        private List<Item> _purchaseLegs = new List<Item>();
        private List<Item> _sellLegs = new List<Item>();
        private List<Item> _purchaseFeet = new List<Item>();
        private List<Item> _sellFeet = new List<Item>();
        private Item _selectedHeadPurchase = new Item();
        private Item _selectedHeadSell = new Item();
        private Item _selectedBodyPurchase = new Item();
        private Item _selectedBodySell = new Item();
        private Item _selectedHandsPurchase = new Item();
        private Item _selectedHandsSell = new Item();
        private Item _selectedLegsPurchase = new Item();
        private Item _selectedLegsSell = new Item();
        private Item _selectedFeetPurchase = new Item();
        private Item _selectedFeetSell = new Item();

        #endregion Local Variables

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindLabels()
        {
            LblHeadNamePurchase.DataContext = _selectedHeadPurchase;
            LblHeadDefensePurchase.DataContext = _selectedHeadPurchase;
            LblHeadDescriptionPurchase.DataContext = _selectedHeadPurchase;
            LblHeadSellablePurchase.DataContext = _selectedHeadPurchase;
            LblHeadValuePurchase.DataContext = _selectedHeadPurchase;
            LblHeadNameSell.DataContext = _selectedHeadSell;
            LblHeadDefenseSell.DataContext = _selectedHeadSell;
            LblHeadDescriptionSell.DataContext = _selectedHeadSell;
            LblHeadSellableSell.DataContext = _selectedHeadSell;
            LblHeadValueSell.DataContext = _selectedHeadSell;

            LblBodyNamePurchase.DataContext = _selectedBodyPurchase;
            LblBodyDefensePurchase.DataContext = _selectedBodyPurchase;
            LblBodyDescriptionPurchase.DataContext = _selectedBodyPurchase;
            LblBodySellablePurchase.DataContext = _selectedBodyPurchase;
            LblBodyValuePurchase.DataContext = _selectedBodyPurchase;
            LblBodyNameSell.DataContext = _selectedBodySell;
            LblBodyDefenseSell.DataContext = _selectedBodySell;
            LblBodyDescriptionSell.DataContext = _selectedBodySell;
            LblBodySellableSell.DataContext = _selectedBodySell;
            LblBodyValueSell.DataContext = _selectedBodySell;

            LblHandsNamePurchase.DataContext = _selectedHandsPurchase;
            LblHandsDefensePurchase.DataContext = _selectedHandsPurchase;
            LblHandsDescriptionPurchase.DataContext = _selectedHandsPurchase;
            LblHandsSellablePurchase.DataContext = _selectedHandsPurchase;
            LblHandsValuePurchase.DataContext = _selectedHandsPurchase;
            LblHandsNameSell.DataContext = _selectedHandsSell;
            LblHandsDefenseSell.DataContext = _selectedHandsSell;
            LblHandsDescriptionSell.DataContext = _selectedHandsSell;
            LblHandsSellableSell.DataContext = _selectedHandsSell;
            LblHandsValueSell.DataContext = _selectedHandsSell;

            LblLegsNamePurchase.DataContext = _selectedLegsPurchase;
            LblLegsDefensePurchase.DataContext = _selectedLegsPurchase;
            LblLegsDescriptionPurchase.DataContext = _selectedLegsPurchase;
            LblLegsSellablePurchase.DataContext = _selectedLegsPurchase;
            LblLegsValuePurchase.DataContext = _selectedLegsPurchase;
            LblLegsNameSell.DataContext = _selectedLegsSell;
            LblLegsDefenseSell.DataContext = _selectedLegsSell;
            LblLegsDescriptionSell.DataContext = _selectedLegsSell;
            LblLegsSellableSell.DataContext = _selectedLegsSell;
            LblLegsValueSell.DataContext = _selectedLegsSell;

            LblFeetNamePurchase.DataContext = _selectedFeetPurchase;
            LblFeetDefensePurchase.DataContext = _selectedFeetPurchase;
            LblFeetDescriptionPurchase.DataContext = _selectedFeetPurchase;
            LblFeetSellablePurchase.DataContext = _selectedFeetPurchase;
            LblFeetValuePurchase.DataContext = _selectedFeetPurchase;
            LblFeetNameSell.DataContext = _selectedFeetSell;
            LblFeetDefenseSell.DataContext = _selectedFeetSell;
            LblFeetDescriptionSell.DataContext = _selectedFeetSell;
            LblFeetSellableSell.DataContext = _selectedFeetSell;
            LblFeetValueSell.DataContext = _selectedFeetSell;

            LblGold.DataContext = GameState.CurrentHero;
        }

        protected void NotifyPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Load Methods

        /// <summary>Loads all the required data.</summary>
        private void LoadAll()
        {
            LstHeadPurchase.ItemsSource = _purchaseHead;
            LstHeadSell.ItemsSource = _sellHead;
            LstBodyPurchase.ItemsSource = _purchaseBody;
            LstBodySell.ItemsSource = _sellBody;
            LstHandsPurchase.ItemsSource = _purchaseHands;
            LstHandsSell.ItemsSource = _sellHands;
            LstLegsPurchase.ItemsSource = _purchaseLegs;
            LstLegsSell.ItemsSource = _sellLegs;
            LstFeetPurchase.ItemsSource = _purchaseFeet;
            LstFeetSell.ItemsSource = _sellFeet;
            LoadAllPurchase();
            LoadAllSell();
            BindLabels();
        }

        /// <summary>Loads the appropriate List and displays its contents in the appropriate ListBox.</summary>
        private void LoadAllPurchase()
        {
            _purchaseHead.Clear();
            _purchaseHead.AddRange(GameState.GetItemsOfType(ItemType.HeadArmor).Where(armor => armor.IsSold));
            _purchaseHead = _purchaseHead.OrderBy(armor => armor.Value).ToList();
            LstHeadPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));

            _purchaseBody.Clear();
            _purchaseBody.AddRange(GameState.GetItemsOfType(ItemType.BodyArmor).Where(armor => armor.IsSold));
            _purchaseBody = _purchaseBody.OrderBy(armor => armor.Value).ToList();
            LstBodyPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));

            _purchaseHands.Clear();
            _purchaseHands.AddRange(GameState.GetItemsOfType(ItemType.HandArmor).Where(armor => armor.IsSold));
            _purchaseHands = _purchaseHands.OrderBy(armor => armor.Value).ToList();
            LstHandsPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));

            _purchaseLegs.Clear();
            _purchaseLegs.AddRange(GameState.GetItemsOfType(ItemType.LegArmor).Where(armor => armor.IsSold));
            _purchaseLegs = _purchaseLegs.OrderBy(armor => armor.Value).ToList();
            LstLegsPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));

            _purchaseFeet.Clear();
            _purchaseFeet.AddRange(GameState.GetItemsOfType(ItemType.FeetArmor).Where(armor => armor.IsSold));
            _purchaseFeet = _purchaseFeet.OrderBy(armor => armor.Value).ToList();
            LstFeetPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));

            LoadAllSell();
        }

        /// <summary>Loads the Hero's inventory into a List and displays its contents in the appropriate TextBox.</summary>
        private void LoadAllSell()
        {
            _sellHead.Clear();
            _sellHead.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.HeadArmor).Where(armor => armor.IsSold));
            _sellHead = _sellHead.OrderBy(armor => armor.Value).ToList();
            LstHeadSell.ItemsSource = _sellHead;
            LstHeadSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));

            _sellBody.Clear();
            _sellBody.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.BodyArmor).Where(armor => armor.IsSold));
            _sellBody = _sellBody.OrderBy(armor => armor.Value).ToList();
            LstBodySell.ItemsSource = _sellBody;
            LstBodySell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));

            _sellHands.Clear();
            _sellHands.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.HandArmor).Where(armor => armor.IsSold));
            _sellHands = _sellHands.OrderBy(armor => armor.Value).ToList();
            LstHandsSell.ItemsSource = _sellHands;
            LstHandsSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));

            _sellLegs.Clear();
            _sellLegs.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.LegArmor).Where(armor => armor.IsSold));
            _sellLegs = _sellLegs.OrderBy(armor => armor.Value).ToList();
            LstLegsSell.ItemsSource = _sellLegs;
            LstLegsSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));

            _sellFeet.Clear();
            _sellFeet.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.FeetArmor).Where(armor => armor.IsSold));
            _sellFeet = _sellFeet.OrderBy(armor => armor.Value).ToList();
            LstFeetSell.ItemsSource = _sellFeet;
            LstFeetSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));

            LstHeadSell.Items.Refresh();
            LstBodySell.Items.Refresh();
            LstHandsSell.Items.Refresh();
            LstLegsSell.Items.Refresh();
            LstFeetSell.Items.Refresh();
        }

        #endregion Load Methods

        #region Transaction Methods

        /// <summary>Checks whether an item type's Purchase button should be enabled after a transaction has occurred.</summary>
        private void CheckPurchaseButtons()
        {
            BtnHeadPurchase.IsEnabled = LstHeadPurchase.SelectedIndex >= 0 && GameState.CurrentHero.Gold >= _selectedHeadPurchase.Value;
            BtnBodyPurchase.IsEnabled = LstBodyPurchase.SelectedIndex >= 0 && GameState.CurrentHero.Gold >= _selectedBodyPurchase.Value;
            BtnHandsPurchase.IsEnabled = LstHandsPurchase.SelectedIndex >= 0 && GameState.CurrentHero.Gold >= _selectedHandsPurchase.Value;
            BtnLegsPurchase.IsEnabled = LstLegsPurchase.SelectedIndex >= 0 && GameState.CurrentHero.Gold >= _selectedLegsPurchase.Value;
            BtnFeetPurchase.IsEnabled = LstFeetPurchase.SelectedIndex >= 0 && GameState.CurrentHero.Gold >= _selectedFeetPurchase.Value;
        }

        /// <summary>Purchases selected Item.</summary>
        /// <param name="itmPurchase">Item to be purchased</param>
        /// <returns></returns>
        private string Purchase(Item itmPurchase)
        {
            GameState.CurrentHero.Gold -= itmPurchase.Value;
            GameState.CurrentHero.AddItem(itmPurchase);
            LoadAllSell();
            CheckPurchaseButtons();
            return $"You have purchased {itmPurchase.Name} for {itmPurchase.ValueToString} gold.";
        }

        /// <summary>Sells selected Item.</summary>
        /// <param name="itmSell">Item to be sold</param>
        /// <returns></returns>
        private string Sell(Item itmSell)
        {
            GameState.CurrentHero.Gold += itmSell.SellValue;
            GameState.CurrentHero.RemoveItem(itmSell);
            LoadAllSell();
            CheckPurchaseButtons();
            return $"You have sold your {itmSell.Name} for {itmSell.SellValueToString} gold.";
        }

        #endregion Transaction Methods

        #region Purchase/Sell Button-Click Methods

        private void BtnHeadPurchase_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtTheArmoury, Purchase(_selectedHeadPurchase));
            LoadAllPurchase();
            LstHeadPurchase.UnselectAll();
        }

        private void BtnHeadSell_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtTheArmoury, Sell(_selectedHeadSell));
            LoadAllSell();
            LstHeadSell.UnselectAll();
        }

        private void BtnBodyPurchase_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtTheArmoury, Purchase(_selectedBodyPurchase));
            LoadAllPurchase();
            LstBodyPurchase.UnselectAll();
        }

        private void BtnBodySell_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtTheArmoury, Sell(_selectedBodySell));
            LoadAllSell();
            LstBodySell.UnselectAll();
        }

        private void BtnHandsPurchase_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtTheArmoury, Purchase(_selectedHandsPurchase));
            LoadAllPurchase();
            LstHandsPurchase.UnselectAll();
        }

        private void BtnHandsSell_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtTheArmoury, Sell(_selectedHandsSell));
            LoadAllSell();
            LstHandsSell.UnselectAll();
        }

        private void BtnLegsPurchase_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtTheArmoury, Purchase(_selectedLegsPurchase));
            LoadAllPurchase();
            LstLegsPurchase.UnselectAll();
        }

        private void BtnLegsSell_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtTheArmoury, Sell(_selectedLegsSell));
            LoadAllSell();
            LstLegsSell.UnselectAll();
        }

        private void BtnFeetPurchase_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtTheArmoury, Purchase(_selectedFeetPurchase));
            LoadAllPurchase();
            LstFeetPurchase.UnselectAll();
        }

        private void BtnFeetSell_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtTheArmoury, Sell(_selectedFeetSell));
            LoadAllSell();
            LstFeetSell.UnselectAll();
        }

        #endregion Purchase/Sell Button-Click Methods

        #region Button-Click Methods

        private void BtnCharacter_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new CharacterPage());

        private void BtnBack_Click(object sender, RoutedEventArgs e) => ClosePage();

        #endregion Button-Click Methods

        #region Purchase/Sell Selection Changed

        private void LstHeadPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedHeadPurchase = LstHeadPurchase.SelectedIndex >= 0
            ? (Item)LstHeadPurchase.SelectedValue
            : new Item();

            BtnHeadPurchase.IsEnabled = _selectedHeadPurchase.Value > 0 && _selectedHeadPurchase.Value <= GameState.CurrentHero.Gold;
            BindLabels();
        }

        private void LstHeadSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedHeadSell = LstHeadSell.SelectedIndex >= 0 ? (Item)LstHeadSell.SelectedValue : new Item();

            BtnHeadSell.IsEnabled = _selectedHeadSell.CanSell;
            BindLabels();
        }

        private void LstBodyPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedBodyPurchase = LstBodyPurchase.SelectedIndex >= 0
            ? (Item)LstBodyPurchase.SelectedValue
            : new Item();

            BtnBodyPurchase.IsEnabled = _selectedBodyPurchase.Value > 0 && _selectedBodyPurchase.Value <= GameState.CurrentHero.Gold;
            BindLabels();
        }

        private void LstBodySell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedBodySell = LstBodySell.SelectedIndex >= 0 ? (Item)LstBodySell.SelectedValue : new Item();

            BtnBodySell.IsEnabled = _selectedBodySell.CanSell;
            BindLabels();
        }

        private void LstHandsPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedHandsPurchase = LstHandsPurchase.SelectedIndex >= 0
            ? (Item)LstHandsPurchase.SelectedValue
            : new Item();

            BtnHandsPurchase.IsEnabled = _selectedHandsPurchase.Value > 0 && _selectedHandsPurchase.Value <= GameState.CurrentHero.Gold;
            BindLabels();
        }

        private void LstHandsSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedHandsSell = LstHandsSell.SelectedIndex >= 0
            ? (Item)LstHandsSell.SelectedValue
            : new Item();

            BtnHandsSell.IsEnabled = _selectedHandsSell.CanSell;
            BindLabels();
        }

        private void LstLegsPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedLegsPurchase = LstLegsPurchase.SelectedIndex >= 0
            ? (Item)LstLegsPurchase.SelectedValue
            : new Item();

            BtnLegsPurchase.IsEnabled = _selectedLegsPurchase.Value > 0 && _selectedLegsPurchase.Value <= GameState.CurrentHero.Gold;
            BindLabels();
        }

        private void LstLegsSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedLegsSell = LstLegsSell.SelectedIndex >= 0 ? (Item)LstLegsSell.SelectedValue : new Item();

            BtnLegsSell.IsEnabled = _selectedLegsSell.CanSell;
            BindLabels();
        }

        private void LstFeetPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedFeetPurchase = LstFeetPurchase.SelectedIndex >= 0
            ? (Item)LstFeetPurchase.SelectedValue
            : new Item();

            BtnFeetPurchase.IsEnabled = _selectedFeetPurchase.Value > 0 && _selectedFeetPurchase.Value <= GameState.CurrentHero.Gold;
            BindLabels();
        }

        private void LstFeetSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedFeetSell = LstFeetSell.SelectedIndex >= 0 ? (Item)LstFeetSell.SelectedValue : new Item();

            BtnFeetSell.IsEnabled = _selectedFeetSell.CanSell;
            BindLabels();
        }

        #endregion Purchase/Sell Selection Changed

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage()
        {
            GameState.SaveHero(GameState.CurrentHero);
            GameState.GoBack();
        }

        public TheArmouryPage()
        {
            InitializeComponent();
            TxtTheArmoury.Text =
            "You enter The Armoury, an old, solid brick building filled with armor pieces of various shapes, sizes, and materials. The shopkeeper beckons you over to examine his wares.";
        }

        private void TheArmouryPage_OnLoaded(object sender, RoutedEventArgs e) => LoadAll();

        #endregion Page-Manipulation Methods
    }
}