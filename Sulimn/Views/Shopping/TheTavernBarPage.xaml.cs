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
    /// <summary>Interaction logic for TheTavernBarPage.xaml</summary>
    public partial class TheTavernBarPage : INotifyPropertyChanged
    {
        private List<Drink> _purchaseDrink = new List<Drink>();
        private List<Food> _purchaseFood = new List<Food>();
        private Drink _selectedDrinkPurchase = new Drink();
        private Drink _selectedDrinkSell = new Drink();
        private Food _selectedFoodPurchase = new Food();
        private Food _selectedFoodSell = new Food();
        private List<Drink> _sellDrink = new List<Drink>();
        private List<Food> _sellFood = new List<Food>();

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindFoodPurchase(bool reload = true)
        {
            if (reload)
            {
                _purchaseFood.Clear();
                _purchaseFood.AddRange(
                GameState.GetItemsOfType<Food>().Where(food => food.IsSold));
                _purchaseFood = _purchaseFood.OrderBy(food => food.Value).ToList();
                LstFoodPurchase.ItemsSource = _purchaseFood;
                LstFoodPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                LstFoodPurchase.Items.Refresh();
            }
            LblFoodNamePurchase.DataContext = _selectedFoodPurchase;
            LblFoodTypeAmountPurchase.DataContext = _selectedFoodPurchase;
            LblFoodDescriptionPurchase.DataContext = _selectedFoodPurchase;
            LblFoodSellablePurchase.DataContext = _selectedFoodPurchase;
            LblFoodValuePurchase.DataContext = _selectedFoodPurchase;
        }

        private void BindFoodSell(bool reload = true)
        {
            if (reload)
            {
                _sellFood.Clear();
                _sellFood.AddRange(
                GameState.CurrentHero.GetItemsOfType<Food>());
                _sellFood = _sellFood.OrderBy(food => food.Value).ToList();
                LstFoodSell.ItemsSource = _sellFood;
                LstFoodSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));
                LstFoodSell.Items.Refresh();
            }
            LblFoodNameSell.DataContext = _selectedFoodSell;
            LblFoodTypeAmountSell.DataContext = _selectedFoodSell;
            LblFoodDescriptionSell.DataContext = _selectedFoodSell;
            LblFoodSellableSell.DataContext = _selectedFoodSell;
            LblFoodValueSell.DataContext = _selectedFoodSell;
        }

        private void BindDrinkPurchase(bool reload = true)
        {
            if (reload)
            {
                _purchaseDrink.Clear();
                _purchaseDrink.AddRange(
                GameState.GetItemsOfType<Drink>().Where(drink => drink.IsSold));
                _purchaseDrink = _purchaseDrink.OrderBy(food => food.Value).ToList();
                LstDrinkPurchase.ItemsSource = _purchaseDrink;
                LstDrinkPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                LstDrinkPurchase.Items.Refresh();
            }
            LblDrinkNamePurchase.DataContext = _selectedDrinkPurchase;
            LblDrinkTypeAmountPurchase.DataContext = _selectedDrinkPurchase;
            LblDrinkDescriptionPurchase.DataContext = _selectedDrinkPurchase;
            LblDrinkSellablePurchase.DataContext = _selectedDrinkPurchase;
            LblDrinkValuePurchase.DataContext = _selectedDrinkPurchase;
        }

        private void BindDrinkSell(bool reload = true)
        {
            if (reload)
            {
                _sellDrink.Clear();
                _sellDrink.AddRange(
                GameState.CurrentHero.GetItemsOfType<Drink>());
                _sellDrink = _sellDrink.OrderBy(drink => drink.Value).ToList();
                LstDrinkSell.ItemsSource = _sellDrink;
                LstDrinkSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));
                LstDrinkSell.Items.Refresh();
            }
            LblDrinkNameSell.DataContext = _selectedDrinkSell;
            LblDrinkTypeAmountSell.DataContext = _selectedDrinkSell;
            LblDrinkDescriptionSell.DataContext = _selectedDrinkSell;
            LblDrinkSellableSell.DataContext = _selectedDrinkSell;
            LblDrinkValueSell.DataContext = _selectedDrinkSell;
        }

        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Load

        /// <summary>Loads all the required data.</summary>
        private void LoadAll()
        {
            LoadAllPurchase();
            LoadAllSell();

            LblGold.DataContext = GameState.CurrentHero;
        }

        private void LoadAllPurchase()
        {
            BindFoodPurchase();
            BindDrinkPurchase();
        }

        private void LoadAllSell()
        {
            BindFoodSell();
            BindDrinkSell();
        }

        #endregion Load

        #region Transaction Methods

        /// <summary>Purchases selected Item.</summary>
        /// <param name="itmPurchase">Item to be purchased</param>
        /// <returns>Returns text about the purchase</returns>
        private string Purchase(Item itmPurchase)
        {
            GameState.CurrentHero.Gold -= itmPurchase.Value;
            GameState.CurrentHero.AddItem(itmPurchase);
            LoadAllPurchase();
            LoadAllSell();
            CheckPurchaseButtons();
            return $"You have purchased {itmPurchase.Name} for {itmPurchase.ValueToString} gold.";
        }

        /// <summary>Sells selected Item.</summary>
        /// <param name="itmSell">Item to be sold</param>
        /// <returns>Returns text about the sale</returns>
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

        /// <summary>Checks whether an item type's Purchase button should be enabled after a transaction has occurred.</summary>
        private void CheckPurchaseButtons()
        {
            BtnFoodPurchase.IsEnabled = LstFoodPurchase.SelectedIndex >= 0 && GameState.CurrentHero.Gold >= _selectedFoodPurchase.Value;
            BtnDrinkPurchase.IsEnabled = LstDrinkPurchase.SelectedIndex >= 0 && GameState.CurrentHero.Gold >= _selectedDrinkPurchase.Value;
        }

        private void BtnFoodPurchase_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtTheTavernBar, Purchase(_selectedFoodPurchase));
            LstFoodPurchase.UnselectAll();
        }

        private void BtnFoodSell_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtTheTavernBar, Sell(_selectedFoodSell));
            LstFoodSell.UnselectAll();
        }

        private void BtnDrinkPurchase_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtTheTavernBar, Purchase(_selectedDrinkPurchase));
            LstDrinkPurchase.UnselectAll();
        }

        private void BtnDrinkSell_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtTheTavernBar, Sell(_selectedDrinkSell));
            LstDrinkSell.UnselectAll();
        }

        #endregion Purchase/Sell Button-Click Methods

        #region Purchase/Sell Selection Changed

        private void LstFoodPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedFoodPurchase = LstFoodPurchase.SelectedIndex >= 0
            ? (Food)LstFoodPurchase.SelectedValue
            : new Food();

            BtnFoodPurchase.IsEnabled = _selectedFoodPurchase.Value > 0 && _selectedFoodPurchase.Value <= GameState.CurrentHero.Gold;
            BindFoodPurchase(false);
        }

        private void LstFoodSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedFoodSell = LstFoodSell.SelectedIndex >= 0 ? (Food)LstFoodSell.SelectedValue : new Food();

            BtnFoodSell.IsEnabled = _selectedFoodSell.CanSell;
            BindFoodSell(false);
        }

        private void LstDrinkPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDrinkPurchase = LstDrinkPurchase.SelectedIndex >= 0
            ? (Drink)LstDrinkPurchase.SelectedValue
            : new Drink();

            BtnDrinkPurchase.IsEnabled = _selectedDrinkPurchase.Value > 0 && _selectedDrinkPurchase.Value <= GameState.CurrentHero.Gold;
            BindDrinkPurchase(false);
        }

        private void LstDrinkSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDrinkSell = LstDrinkSell.SelectedIndex >= 0 ? (Drink)LstDrinkSell.SelectedValue : new Drink();

            BtnDrinkSell.IsEnabled = _selectedDrinkSell.CanSell;
            BindDrinkSell(false);
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

        public TheTavernBarPage()
        {
            InitializeComponent();
            TxtTheTavernBar.Text =
            "You approach the bar at The Tavern. The barkeeper asks you if you'd like a drink or a bite to eat.";
        }

        private void TheTavernBarPage_OnLoaded(object sender, RoutedEventArgs e) => LoadAll();

        #endregion Page-Manipulation
    }
}