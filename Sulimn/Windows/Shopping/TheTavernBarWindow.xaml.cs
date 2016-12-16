using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for TheTavernBarWindow.xaml
    /// </summary>
    public partial class TheTavernBarWindow : INotifyPropertyChanged
    {
        private readonly string nl = Environment.NewLine;
        private List<Food> purchaseDrink = new List<Food>();
        private List<Food> purchaseFood = new List<Food>();
        private Food selectedDrinkPurchase = new Food();
        private Food selectedDrinkSell = new Food();
        private Food selectedFoodPurchase = new Food();
        private Food selectedFoodSell = new Food();
        private List<Food> sellDrink = new List<Food>();
        private List<Food> sellFood = new List<Food>();

        internal TavernWindow RefToTavernWindow { get; set; }

        /// <summary>Adds text to the txtTheArmoury TextBox.</summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtTheTavernBar.Text += nl + nl + newText;
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
                purchaseFood.Clear();
                purchaseFood.AddRange(
                GameState.GetItemsOfType<Food>().Where(food => food.IsSold && food.FoodType == FoodTypes.Food));
                purchaseFood = purchaseFood.OrderBy(food => food.Value).ToList();
                lstFoodPurchase.ItemsSource = purchaseFood;
                lstFoodPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstFoodPurchase.Items.Refresh();
            }
            lblFoodNamePurchase.DataContext = selectedFoodPurchase;
            lblFoodTypeAmountPurchase.DataContext = selectedFoodPurchase;
            lblFoodDescriptionPurchase.DataContext = selectedFoodPurchase;
            lblFoodSellablePurchase.DataContext = selectedFoodPurchase;
            lblFoodValuePurchase.DataContext = selectedFoodPurchase;
        }

        private void BindFoodSell(bool reload = true)
        {
            if (reload)
            {
                sellFood.Clear();
                sellFood.AddRange(
                GameState.CurrentHero.Inventory.GetItemsOfType<Food>()
                .Where(food => food.FoodType == FoodTypes.Food));
                sellFood = sellFood.OrderBy(food => food.Value).ToList();
                lstFoodSell.ItemsSource = sellFood;
                lstFoodSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));
                lstFoodSell.Items.Refresh();
            }
            lblFoodNameSell.DataContext = selectedFoodSell;
            lblFoodTypeAmountSell.DataContext = selectedFoodSell;
            lblFoodDescriptionSell.DataContext = selectedFoodSell;
            lblFoodSellableSell.DataContext = selectedFoodSell;
            lblFoodValueSell.DataContext = selectedFoodSell;
        }

        private void BindDrinkPurchase(bool reload = true)
        {
            if (reload)
            {
                purchaseDrink.Clear();
                purchaseDrink.AddRange(
                GameState.GetItemsOfType<Food>().Where(drink => drink.IsSold && drink.FoodType == FoodTypes.Drink));
                purchaseDrink = purchaseDrink.OrderBy(food => food.Value).ToList();
                lstDrinkPurchase.ItemsSource = purchaseDrink;
                lstDrinkPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstDrinkPurchase.Items.Refresh();
            }
            lblDrinkNamePurchase.DataContext = selectedDrinkPurchase;
            lblDrinkTypeAmountPurchase.DataContext = selectedDrinkPurchase;
            lblDrinkDescriptionPurchase.DataContext = selectedDrinkPurchase;
            lblDrinkSellablePurchase.DataContext = selectedDrinkPurchase;
            lblDrinkValuePurchase.DataContext = selectedDrinkPurchase;
        }

        private void BindDrinkSell(bool reload = true)
        {
            if (reload)
            {
                sellDrink.Clear();
                sellDrink.AddRange(
                GameState.CurrentHero.Inventory.GetItemsOfType<Food>()
                .Where(drink => drink.FoodType == FoodTypes.Drink));
                sellDrink = sellDrink.OrderBy(drink => drink.Value).ToList();
                lstDrinkSell.ItemsSource = sellDrink;
                lstDrinkSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));
                lstDrinkSell.Items.Refresh();
            }
            lblDrinkNameSell.DataContext = selectedDrinkSell;
            lblDrinkTypeAmountSell.DataContext = selectedDrinkSell;
            lblDrinkDescriptionSell.DataContext = selectedDrinkSell;
            lblDrinkSellableSell.DataContext = selectedDrinkSell;
            lblDrinkValueSell.DataContext = selectedDrinkSell;
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

        private void btnFoodPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(selectedFoodPurchase));
            lstFoodPurchase.UnselectAll();
        }

        private void btnFoodSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(selectedFoodSell));
            lstFoodSell.UnselectAll();
        }

        private void btnDrinkPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(selectedDrinkPurchase));
            lstDrinkPurchase.UnselectAll();
        }

        private void btnDrinkSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(selectedDrinkSell));
            lstDrinkSell.UnselectAll();
        }

        #endregion Purchase/Sell Button-Click Methods

        #region Purchase/Sell Selection Changed

        private void lstFoodPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFoodPurchase.SelectedIndex >= 0)
            {
                selectedFoodPurchase = (Food)lstFoodPurchase.SelectedValue;

                btnFoodPurchase.IsEnabled = selectedFoodPurchase.Value <= GameState.CurrentHero.Inventory.Gold;
            }
            else
            {
                selectedFoodPurchase = new Food();
                btnFoodPurchase.IsEnabled = false;
            }
            BindFoodPurchase(false);
        }

        private void lstFoodSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFoodSell.SelectedIndex >= 0)
            {
                selectedFoodSell = (Food)lstFoodSell.SelectedValue;
                btnFoodSell.IsEnabled = selectedFoodSell.CanSell;
            }
            else
            {
                selectedFoodSell = new Food();
                btnFoodSell.IsEnabled = false;
            }
            BindFoodSell(false);
        }

        private void lstDrinkPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstDrinkPurchase.SelectedIndex >= 0)
            {
                selectedDrinkPurchase = (Food)lstDrinkPurchase.SelectedValue;

                btnDrinkPurchase.IsEnabled = selectedDrinkPurchase.Value <= GameState.CurrentHero.Inventory.Gold;
            }
            else
            {
                selectedDrinkPurchase = new Food();
                btnDrinkPurchase.IsEnabled = false;
            }
            BindDrinkPurchase(false);
        }

        private void lstDrinkSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstDrinkSell.SelectedIndex >= 0)
            {
                selectedDrinkSell = (Food)lstDrinkSell.SelectedValue;
                btnDrinkSell.IsEnabled = selectedDrinkSell.CanSell;
            }
            else
            {
                selectedDrinkSell = new Food();
                btnDrinkSell.IsEnabled = false;
            }
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