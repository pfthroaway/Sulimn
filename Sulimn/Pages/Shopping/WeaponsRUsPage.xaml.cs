using Extensions;
using Sulimn.Classes;
using Sulimn.Classes.Items;
using Sulimn.Pages.Characters;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn.Pages.Shopping
{
    /// <summary>Interaction logic for WeaponsRUsPage.xaml</summary>
    public partial class WeaponsRUsPage : INotifyPropertyChanged
    {
        private List<Weapon> _purchaseWeapon = new List<Weapon>();
        private Weapon _selectedWeaponPurchase = new Weapon();
        private Weapon _selectedWeaponSell = new Weapon();
        private List<Weapon> _sellWeapon = new List<Weapon>();

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindLabels()
        {
            LoadAllPurchase();
            LoadAllSell();

            LblGold.DataContext = GameState.CurrentHero.Inventory;
        }

        private void BindWeaponPurchase(bool reload = true)
        {
            if (reload)
            {
                _purchaseWeapon.Clear();
                _purchaseWeapon.AddRange(GameState.GetItemsOfType<Weapon>().Where(weapon => weapon.IsSold));
                _purchaseWeapon = _purchaseWeapon.OrderBy(weapon => weapon.Value).ToList();
                LstWeaponPurchase.ItemsSource = _purchaseWeapon;
                LstWeaponPurchase.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                LstWeaponPurchase.Items.Refresh();
            }
            LblWeaponNamePurchase.DataContext = _selectedWeaponPurchase;
            LblWeaponDamagePurchase.DataContext = _selectedWeaponPurchase;
            LblWeaponDescriptionPurchase.DataContext = _selectedWeaponPurchase;
            LblWeaponTypePurchase.DataContext = _selectedWeaponPurchase;
            LblWeaponSellablePurchase.DataContext = _selectedWeaponPurchase;
            LblWeaponValuePurchase.DataContext = _selectedWeaponPurchase;
        }

        private void BindWeaponSell(bool reload = true)
        {
            if (reload)
            {
                _sellWeapon.Clear();
                _sellWeapon.AddRange(GameState.CurrentHero.GetItemsOfType<Weapon>());
                _sellWeapon = _sellWeapon.OrderBy(weapon => weapon.Value).ToList();
                LstWeaponSell.ItemsSource = _sellWeapon;
                LstWeaponSell.Items.SortDescriptions.Add(new SortDescription("SellValue", ListSortDirection.Ascending));
                LstWeaponSell.Items.Refresh();
            }
            LblWeaponNameSell.DataContext = _selectedWeaponSell;
            LblWeaponDamageSell.DataContext = _selectedWeaponSell;
            LblWeaponDescriptionSell.DataContext = _selectedWeaponSell;
            LblWeaponTypeSell.DataContext = _selectedWeaponSell;
            LblWeaponSellableSell.DataContext = _selectedWeaponSell;
            LblWeaponValueSell.DataContext = _selectedWeaponSell;
        }

        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Load

        internal void LoadAll() => BindLabels();

        private void LoadAllPurchase() => BindWeaponPurchase();

        private void LoadAllSell() => BindWeaponSell();

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
            return $"You have sold your {itmSell.Name} for {itmSell.SellValueToString} gold.";
        }

        #endregion Transaction Methods

        #region Purchase/Sell Button-Click Methods

        private void BtnWeaponPurchase_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtWeaponsRUs, Purchase(_selectedWeaponPurchase));
            LstWeaponPurchase.UnselectAll();
        }

        private void BtnWeaponSell_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtWeaponsRUs, Sell(_selectedWeaponSell));
            LstWeaponSell.UnselectAll();
        }

        #endregion Purchase/Sell Button-Click Methods

        #region Purchase/Sell Selection Changed

        private void LstWeaponPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedWeaponPurchase = LstWeaponPurchase.SelectedIndex >= 0
            ? (Weapon)LstWeaponPurchase.SelectedValue
            : new Weapon();

            BtnWeaponPurchase.IsEnabled = _selectedWeaponPurchase.Value > 0 && _selectedWeaponPurchase.Value <= GameState.CurrentHero.Gold;
            BindWeaponPurchase(false);
        }

        private void LstWeaponSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedWeaponSell = LstWeaponSell.SelectedIndex >= 0 ? (Weapon)LstWeaponSell.SelectedValue : new Weapon();

            BtnWeaponSell.IsEnabled = _selectedWeaponSell.CanSell;
            BindWeaponSell(false);
        }

        #endregion Purchase/Sell Selection Changed

        #region Page Button-Click Methods

        private void BtnCharacter_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new CharacterPage());

        private void BtnBack_Click(object sender, RoutedEventArgs e) => ClosePage();

        #endregion Page Button-Click Methods

        #region Page-Manipulation

        /// <summary>Closes the Page.</summary>
        private async void ClosePage()
        {
            await GameState.SaveHero(GameState.CurrentHero);
            GameState.GoBack();
        }

        public WeaponsRUsPage()
        {
            InitializeComponent();
            TxtWeaponsRUs.Text =
            "You enter Weapons 'R' Us, the finest weaponsmith shop in the city of Sulimn. You approach the shopkeeper and he shows you his wares.";
            BindLabels();
        }

        #endregion Page-Manipulation

        private void WeaponsRUsPage_OnLoaded(object sender, RoutedEventArgs e) => GameState.CalculateScale(Grid);
    }
}