using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>Interaction logic for WeaponsRUsWindow.xaml</summary>
    public partial class WeaponsRUsWindow : INotifyPropertyChanged
    {
        private readonly string _nl = Environment.NewLine;
        private List<Weapon> _purchaseWeapon = new List<Weapon>();
        private Weapon _selectedWeaponPurchase = new Weapon();
        private Weapon _selectedWeaponSell = new Weapon();
        private List<Weapon> _sellWeapon = new List<Weapon>();

        internal MarketWindow RefToMarketWindow { private get; set; }

        /// <summary>Adds text to the txtTheArmoury TextBox.</summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtWeaponsRUs.Text += _nl + _nl + newText;
            txtWeaponsRUs.Focus();
            txtWeaponsRUs.CaretIndex = txtWeaponsRUs.Text.Length;
            txtWeaponsRUs.ScrollToEnd();
        }

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
                _purchaseWeapon.Clear();
                _purchaseWeapon.AddRange(GameState.GetItemsOfType<Weapon>().Where(weapon => weapon.IsSold));
                _purchaseWeapon = _purchaseWeapon.OrderBy(weapon => weapon.Value).ToList();
                lstWeaponPurchase.ItemsSource = _purchaseWeapon;
                lstWeaponPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstWeaponPurchase.Items.Refresh();
            }
            lblWeaponNamePurchase.DataContext = _selectedWeaponPurchase;
            lblWeaponDamagePurchase.DataContext = _selectedWeaponPurchase;
            lblWeaponDescriptionPurchase.DataContext = _selectedWeaponPurchase;
            lblWeaponTypePurchase.DataContext = _selectedWeaponPurchase;
            lblWeaponSellablePurchase.DataContext = _selectedWeaponPurchase;
            lblWeaponValuePurchase.DataContext = _selectedWeaponPurchase;
        }

        private void BindWeaponSell(bool reload = true)
        {
            if (reload)
            {
                _sellWeapon.Clear();
                _sellWeapon.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<Weapon>());
                _sellWeapon = _sellWeapon.OrderBy(weapon => weapon.Value).ToList();
                lstWeaponSell.ItemsSource = _sellWeapon;
                lstWeaponSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));
                lstWeaponSell.Items.Refresh();
            }
            lblWeaponNameSell.DataContext = _selectedWeaponSell;
            lblWeaponDamageSell.DataContext = _selectedWeaponSell;
            lblWeaponDescriptionSell.DataContext = _selectedWeaponSell;
            lblWeaponTypeSell.DataContext = _selectedWeaponSell;
            lblWeaponSellableSell.DataContext = _selectedWeaponSell;
            lblWeaponValueSell.DataContext = _selectedWeaponSell;
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
            BindWeaponPurchase();
        }

        private void LoadAllSell()
        {
            BindWeaponSell();
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

        private void btnWeaponPurchase_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Purchase(_selectedWeaponPurchase));
            lstWeaponPurchase.UnselectAll();
        }

        private void btnWeaponSell_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT(Sell(_selectedWeaponSell));
            lstWeaponSell.UnselectAll();
        }

        #endregion Purchase/Sell Button-Click Methods

        #region Purchase/Sell Selection Changed

        private void lstWeaponPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedWeaponPurchase = lstWeaponPurchase.SelectedIndex >= 0
                ? (Weapon)lstWeaponPurchase.SelectedValue
                : new Weapon();

            btnWeaponPurchase.IsEnabled = _selectedWeaponPurchase.Value > 0 && _selectedWeaponPurchase.Value <= GameState.CurrentHero.Inventory.Gold;
            BindWeaponPurchase(false);
        }

        private void lstWeaponSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedWeaponSell = lstWeaponSell.SelectedIndex >= 0 ? (Weapon)lstWeaponSell.SelectedValue : new Weapon();

            btnWeaponSell.IsEnabled = _selectedWeaponSell.CanSell;
            BindWeaponSell(false);
        }

        #endregion Purchase/Sell Selection Changed

        #region Window Button-Click Methods

        private void btnCharacter_Click(object sender, RoutedEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow { RefToWeaponsRUsWindow = this };
            characterWindow.Show();
            characterWindow.SetupChar();
            characterWindow.SetPreviousWindow("Weapons 'R' Us");
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

        public WeaponsRUsWindow()
        {
            InitializeComponent();
            txtWeaponsRUs.Text =
            "You enter Weapons 'R' Us, the finest weaponsmith shop in the city of Sulimn. You approach the shopkeeper and he shows you his wares.";
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