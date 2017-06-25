using Extensions;
using Sulimn.Classes;
using Sulimn.Classes.Enums;
using Sulimn.Classes.Items;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn.Windows.Shopping
{
    /// <summary>Interaction logic for TheTavernBarWindow.xaml</summary>
    public partial class TheTavernBarWindow : INotifyPropertyChanged
    {
        private List<Food> _purchaseDrink = new List<Food>();
        private List<Food> _purchaseFood = new List<Food>();
        private Food _selectedDrinkPurchase = new Food();
        private Food _selectedDrinkSell = new Food();
        private Food _selectedFoodPurchase = new Food();
        private Food _selectedFoodSell = new Food();
        private List<Food> _sellDrink = new List<Food>();
        private List<Food> _sellFood = new List<Food>();

        internal Exploration.TavernWindow RefToTavernWindow { private get; set; }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindLabels()
        {
            LoadAllPurchase();
            LoadAllSell();

            LblGold.DataContext = GameState.CurrentHero.Inventory;
        }

        private void BindFoodPurchase(bool reload = true)
        {
            if (reload)
            {
                _purchaseFood.Clear();
                _purchaseFood.AddRange(
                GameState.GetItemsOfType<Food>().Where(food => food.IsSold && food.FoodType == FoodTypes.Food));
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
                GameState.CurrentHero.Inventory.GetItemsOfType<Food>()
                .Where(food => food.FoodType == FoodTypes.Food));
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
                GameState.GetItemsOfType<Food>().Where(drink => drink.IsSold && drink.FoodType == FoodTypes.Drink));
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
                GameState.CurrentHero.Inventory.GetItemsOfType<Food>()
                .Where(drink => drink.FoodType == FoodTypes.Drink));
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
            return $"You have purchased {itmPurchase.Name} for {itmPurchase.ValueToString} gold.";
        }

        /// <summary>Sells selected Item.</summary>
        /// <param name="itmSell">Item to be sold</param>
        /// <returns>Returns text about the sale</returns>
        private string Sell(Item itmSell)
        {
            GameState.CurrentHero.Inventory.Gold += itmSell.SellValue;
            GameState.CurrentHero.Inventory.RemoveItem(itmSell);
            LoadAllSell();
            return $"You have sold your {itmSell.Name} for {itmSell.SellValueToString} gold.";
        }

        #endregion Transaction Methods

        #region Purchase/Sell Button-Click Methods

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

            BtnFoodPurchase.IsEnabled = _selectedFoodPurchase.Value > 0 && _selectedFoodPurchase.Value <= GameState.CurrentHero.Inventory.Gold;
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
            ? (Food)LstDrinkPurchase.SelectedValue
            : new Food();

            BtnDrinkPurchase.IsEnabled = _selectedDrinkPurchase.Value > 0 && _selectedDrinkPurchase.Value <= GameState.CurrentHero.Inventory.Gold;
            BindDrinkPurchase(false);
        }

        private void LstDrinkSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDrinkSell = LstDrinkSell.SelectedIndex >= 0 ? (Food)LstDrinkSell.SelectedValue : new Food();

            BtnDrinkSell.IsEnabled = _selectedDrinkSell.CanSell;
            BindDrinkSell(false);
        }

        #endregion Purchase/Sell Selection Changed

        #region Window Button-Click Methods

        private void BtnCharacter_Click(object sender, RoutedEventArgs e)
        {
            Characters.CharacterWindow characterWindow = new Characters.CharacterWindow { RefToTheTavernBarWindow = this };
            characterWindow.Show();
            characterWindow.SetupChar();
            characterWindow.SetPreviousWindow("The Tavern Bar");
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

        public TheTavernBarWindow()
        {
            InitializeComponent();
            TxtTheTavernBar.Text =
            "You approach the bar at The Tavern. The barkeeper asks you if you'd like a drink or a bite to eat.";
            BindLabels();
        }

        private async void WindowTheTavernBar_Closing(object sender, CancelEventArgs e)
        {
            RefToTavernWindow.Show();
            await GameState.SaveHero(GameState.CurrentHero);
        }

        #endregion Window-Manipulation
    }
}