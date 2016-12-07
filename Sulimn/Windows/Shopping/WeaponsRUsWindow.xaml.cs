using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for WeaponsRUsWindow.xaml
    /// </summary>
    public partial class WeaponsRUsWindow : Window, INotifyPropertyChanged
    {
        private List<Weapon> purchaseWeapon = new List<Weapon>();
        private List<Weapon> sellWeapon = new List<Weapon>();
        private Weapon selectedWeaponPurchase = new Weapon();
        private Weapon selectedWeaponSell = new Weapon();
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

        private void BindWeaponPurchase(bool reload = true)
        {
            if (reload)
            {
                purchaseWeapon.Clear();
                purchaseWeapon.AddRange(GameState.GetItemsOfType<Weapon>().Where(weapon => weapon.IsSold == true));
                purchaseWeapon = purchaseWeapon.OrderBy(weapon => weapon.Value).ToList();
                lstWeaponPurchase.ItemsSource = purchaseWeapon;
                lstWeaponPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstWeaponPurchase.Items.Refresh();
            }
            lblWeaponNamePurchase.DataContext = selectedWeaponPurchase;
            lblWeaponDamagePurchase.DataContext = selectedWeaponPurchase;
            lblWeaponDescriptionPurchase.DataContext = selectedWeaponPurchase;
            lblWeaponTypePurchase.DataContext = selectedWeaponPurchase;
            lblWeaponSellablePurchase.DataContext = selectedWeaponPurchase;
            lblWeaponValuePurchase.DataContext = selectedWeaponPurchase;
        }

        private void BindWeaponSell(bool reload = true)
        {
            if (reload)
            {
                sellWeapon.Clear();
                sellWeapon.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<Weapon>());
                sellWeapon = sellWeapon.OrderBy(weapon => weapon.Value).ToList();
                lstWeaponSell.ItemsSource = sellWeapon;
                lstWeaponSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));
                lstWeaponSell.Items.Refresh();
            }
            lblWeaponNameSell.DataContext = selectedWeaponSell;
            lblWeaponDamageSell.DataContext = selectedWeaponSell;
            lblWeaponDescriptionSell.DataContext = selectedWeaponSell;
            lblWeaponTypeSell.DataContext = selectedWeaponSell;
            lblWeaponSellableSell.DataContext = selectedWeaponSell;
            lblWeaponValueSell.DataContext = selectedWeaponSell;
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
            BindWeaponPurchase();
        }

        private void LoadAllSell()
        {
            BindWeaponSell();
        }

        #endregion Load

        /// <summary>Adds text to the txtTheArmoury TextBox.</summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtWeaponsRUs.Text += nl + nl + newText;
            txtWeaponsRUs.Focus();
            txtWeaponsRUs.CaretIndex = txtWeaponsRUs.Text.Length;
            txtWeaponsRUs.ScrollToEnd();
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

        private void btnWeaponPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(selectedWeaponPurchase));
            lstWeaponPurchase.UnselectAll();
        }

        private void btnWeaponSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(selectedWeaponSell));
            lstWeaponSell.UnselectAll();
        }

        #endregion Purchase/Sell Button-Click Methods

        #region Purchase/Sell Selection Changed

        private void lstWeaponPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstWeaponPurchase.SelectedIndex >= 0)
            {
                selectedWeaponPurchase = (Weapon)lstWeaponPurchase.SelectedValue;

                if (selectedWeaponPurchase.Value <= GameState.CurrentHero.Inventory.Gold)
                    btnWeaponPurchase.IsEnabled = true;
                else
                    btnWeaponPurchase.IsEnabled = false;
            }
            else
            {
                selectedWeaponPurchase = new Weapon();
                btnWeaponPurchase.IsEnabled = false;
            }
            BindWeaponPurchase(false);
        }

        private void lstWeaponSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstWeaponSell.SelectedIndex >= 0)
            {
                selectedWeaponSell = (Weapon)lstWeaponSell.SelectedValue;
                if (selectedWeaponSell.CanSell)
                    btnWeaponSell.IsEnabled = true;
                else
                    btnWeaponSell.IsEnabled = false;
            }
            else
            {
                selectedWeaponSell = new Weapon();
                btnWeaponSell.IsEnabled = false;
            }
            BindWeaponSell(false);
        }

        #endregion Purchase/Sell Selection Changed

        #region Window Button-Click Methods

        private void btnCharacter_Click(object sender, RoutedEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow();
            characterWindow.RefToWeaponsRUsWindow = this;
            characterWindow.Show();
            characterWindow.SetupChar();
            characterWindow.SetPreviousWindow("Weapons 'R' Us");
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

        public WeaponsRUsWindow()
        {
            InitializeComponent();
            txtWeaponsRUs.Text = "You enter Weapons 'R' Us, the finest weaponsmith shop in the city of Sulimn. You approach the shopkeeper and he shows you his wares.";
            BindLabels();
        }

        private async void windowWeaponsRUs_Closing(object sender, CancelEventArgs e)
        {
            RefToMarketWindow.Show();
            await GameState.SaveHero(GameState.CurrentHero);
        }

        #endregion Window-Manipulation
    }
}