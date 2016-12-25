using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>Interaction logic for TheTavernBarWindow.xaml</summary>
    public partial class TheTavernBarWindow : INotifyPropertyChanged
    {
        private readonly string _nl = Environment.NewLine;
        private List<Food> _purchaseDrink = new List<Food>();
        private List<Food> _purchaseFood = new List<Food>();
        private Food _selectedDrinkPurchase = new Food();
        private Food _selectedDrinkSell = new Food();
        private Food _selectedFoodPurchase = new Food();
        private Food _selectedFoodSell = new Food();
        private List<Food> _sellDrink = new List<Food>();
        private List<Food> _sellFood = new List<Food>();

        internal TavernWindow RefToTavernWindow { private get; set; }

        /// <summary>Adds text to the txtTheArmoury TextBox.</summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtTheTavernBar.Text += _nl + _nl + newText;
            txtTheTavernBar.Focus();
            txtTheTavernBar.CaretIndex = txtTheTavernBar.Text.Length;
            txtTheTavernBar.ScrollToEnd();
        }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindLabels()
        {
            LoadAllPurchase();
            LoadAllSell();

            lblGold.DataContext = GameState.CurrentHero.Inventory;
        }

        private void BindFoodPurchase(bool reload = true)
        {
            if (reload)
            {
                _purchaseFood.Clear();
                _purchaseFood.AddRange(
                GameState.GetItemsOfType<Food>().Where(food => food.IsSold && food.FoodType == FoodTypes.Food));
                _purchaseFood = _purchaseFood.OrderBy(food => food.Value).ToList();
                lstFoodPurchase.ItemsSource = _purchaseFood;
                lstFoodPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstFoodPurchase.Items.Refresh();
            }
            lblFoodNamePurchase.DataContext = _selectedFoodPurchase;
            lblFoodTypeAmountPurchase.DataContext = _selectedFoodPurchase;
            lblFoodDescriptionPurchase.DataContext = _selectedFoodPurchase;
            lblFoodSellablePurchase.DataContext = _selectedFoodPurchase;
            lblFoodValuePurchase.DataContext = _selectedFoodPurchase;
        }

        private void BindFoodSell(bool reload = true)
        {
            if (reload)
            {
                _sellFood.Clear();
                _sellFood.AddRange(
                GameState.CurrentHero.Inventory.GetItemsOfType<Food>()
                .Where(food => food.FoodType == FoodTypes.Food));
                _sellFood = _sellFood.OrderBy(food => food.Value).ToList();
                lstFoodSell.ItemsSource = _sellFood;
                lstFoodSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));
                lstFoodSell.Items.Refresh();
            }
            lblFoodNameSell.DataContext = _selectedFoodSell;
            lblFoodTypeAmountSell.DataContext = _selectedFoodSell;
            lblFoodDescriptionSell.DataContext = _selectedFoodSell;
            lblFoodSellableSell.DataContext = _selectedFoodSell;
            lblFoodValueSell.DataContext = _selectedFoodSell;
        }

        private void BindDrinkPurchase(bool reload = true)
        {
            if (reload)
            {
                _purchaseDrink.Clear();
                _purchaseDrink.AddRange(
                GameState.GetItemsOfType<Food>().Where(drink => drink.IsSold && drink.FoodType == FoodTypes.Drink));
                _purchaseDrink = _purchaseDrink.OrderBy(food => food.Value).ToList();
                lstDrinkPurchase.ItemsSource = _purchaseDrink;
                lstDrinkPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstDrinkPurchase.Items.Refresh();
            }
            lblDrinkNamePurchase.DataContext = _selectedDrinkPurchase;
            lblDrinkTypeAmountPurchase.DataContext = _selectedDrinkPurchase;
            lblDrinkDescriptionPurchase.DataContext = _selectedDrinkPurchase;
            lblDrinkSellablePurchase.DataContext = _selectedDrinkPurchase;
            lblDrinkValuePurchase.DataContext = _selectedDrinkPurchase;
        }

        private void BindDrinkSell(bool reload = true)
        {
            if (reload)
            {
                _sellDrink.Clear();
                _sellDrink.AddRange(
                GameState.CurrentHero.Inventory.GetItemsOfType<Food>()
                .Where(drink => drink.FoodType == FoodTypes.Drink));
                _sellDrink = _sellDrink.OrderBy(drink => drink.Value).ToList();
                lstDrinkSell.ItemsSource = _sellDrink;
                lstDrinkSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));
                lstDrinkSell.Items.Refresh();
            }
            lblDrinkNameSell.DataContext = _selectedDrinkSell;
            lblDrinkTypeAmountSell.DataContext = _selectedDrinkSell;
            lblDrinkDescriptionSell.DataContext = _selectedDrinkSell;
            lblDrinkSellableSell.DataContext = _selectedDrinkSell;
            lblDrinkValueSell.DataContext = _selectedDrinkSell;
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
            GameState.CurrentHero.Inventory.Gold -= itmPurchase.Value;
            GameState.CurrentHero.Inventory.AddItem(itmPurchase);
            LoadAllPurchase();
            LoadAllSell();
            return "You have purchased " + itmPurchase.Name + " for " + itmPurchase.ValueToString + " gold.";
        }

        /// <summary>Sells selected Item.</summary>
        /// <param name="itmSell">Item to be sold</param>
        /// <returns>Returns text about the sale</returns>
        private string Sell(Item itmSell)
        {
            GameState.CurrentHero.Inventory.Gold += itmSell.SellValue;
            GameState.CurrentHero.Inventory.RemoveItem(itmSell);
            LoadAllSell();
            return "You have sold your " + itmSell.Name + " for " + itmSell.SellValueToString + " gold.";
        }

        #endregion Transaction Methods

        #region Purchase/Sell Button-Click Methods

        private void btnFoodPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(_selectedFoodPurchase));
            lstFoodPurchase.UnselectAll();
        }

        private void btnFoodSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(_selectedFoodSell));
            lstFoodSell.UnselectAll();
        }

        private void btnDrinkPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(_selectedDrinkPurchase));
            lstDrinkPurchase.UnselectAll();
        }

        private void btnDrinkSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(_selectedDrinkSell));
            lstDrinkSell.UnselectAll();
        }

        #endregion Purchase/Sell Button-Click Methods

        #region Purchase/Sell Selection Changed

        private void lstFoodPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedFoodPurchase = lstFoodPurchase.SelectedIndex >= 0
                ? (Food)lstFoodPurchase.SelectedValue
                : new Food();

            btnFoodPurchase.IsEnabled = _selectedFoodPurchase.Value > 0 && _selectedFoodPurchase.Value <= GameState.CurrentHero.Inventory.Gold;
            BindFoodPurchase(false);
        }

        private void lstFoodSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedFoodSell = lstFoodSell.SelectedIndex >= 0 ? (Food)lstFoodSell.SelectedValue : new Food();

            btnFoodSell.IsEnabled = _selectedFoodSell.CanSell;
            BindFoodSell(false);
        }

        private void lstDrinkPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDrinkPurchase = lstDrinkPurchase.SelectedIndex >= 0
                ? (Food)lstDrinkPurchase.SelectedValue
                : new Food();

            btnDrinkPurchase.IsEnabled = _selectedDrinkPurchase.Value > 0 && _selectedDrinkPurchase.Value <= GameState.CurrentHero.Inventory.Gold;
            BindDrinkPurchase(false);
        }

        private void lstDrinkSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDrinkSell = lstDrinkSell.SelectedIndex >= 0 ? (Food)lstDrinkSell.SelectedValue : new Food();

            btnDrinkSell.IsEnabled = _selectedDrinkSell.CanSell;
            BindDrinkSell(false);
        }

        #endregion Purchase/Sell Selection Changed

        #region Window Button-Click Methods

        private void btnCharacter_Click(object sender, RoutedEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow { RefToTheTavernBarWindow = this };
            characterWindow.Show();
            characterWindow.SetupChar();
            characterWindow.SetPreviousWindow("The Tavern Bar");
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

        public TheTavernBarWindow()
        {
            InitializeComponent();
            txtTheTavernBar.Text =
            "You approach the bar at The Tavern. The barkeeper asks you if you'd like a drink or a bite to eat.";
            BindLabels();
        }

        private async void windowTheTavernBar_Closing(object sender, CancelEventArgs e)
        {
            RefToTavernWindow.Show();
            await GameState.SaveHero(GameState.CurrentHero);
        }

        #endregion Window-Manipulation
    }
}