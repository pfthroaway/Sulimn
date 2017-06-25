using System.ComponentModel;
using System.Windows;

namespace Sulimn.Windows.Shopping
{
    /// <summary>Interaction logic for MarketWindow.xaml</summary>
    public partial class MarketWindow
    {
        internal Exploration.CityWindow RefToCityWindow { private get; set; }

        #region Button-Click Methods

        private void BtnWeaponShop_Click(object sender, RoutedEventArgs e)
        {
            WeaponsRUsWindow weaponsRUsWindow = new WeaponsRUsWindow { RefToMarketWindow = this };
            weaponsRUsWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnArmorShop_Click(object sender, RoutedEventArgs e)
        {
            TheArmouryWindow theArmouryWindow = new TheArmouryWindow { RefToMarketWindow = this };
            theArmouryWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnGeneralStore_Click(object sender, RoutedEventArgs e)
        {
            TheGeneralStoreWindow theGeneralStoreWindow = new TheGeneralStoreWindow { RefToMarketWindow = this };
            theGeneralStoreWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnMagicShop_Click(object sender, RoutedEventArgs e)
        {
            MagickShoppeWindow magickShoppeWindow = new MagickShoppeWindow { RefToMarketWindow = this };
            magickShoppeWindow.LoadAll();
            magickShoppeWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnSilverEmpire_Click(object sender, RoutedEventArgs e)
        {
            SilverEmpireWindow silverEmpireWindow = new SilverEmpireWindow { RefToMarketWindow = this };
            silverEmpireWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public MarketWindow()
        {
            InitializeComponent();
            TxtMarket.Text = "You enter a bustling market.There are many shops here, the most interesting being:\n\n" +
            "Weapons 'R' Us - A weapons shop.\n\n" +
            "The Armoury - An armor shop.\n\n" +
            "The General Store - A shop supplying general goods like potions.\n\n" +
            "Ye Old Magick Shoppe - A shop selling magical spells and equipment.\n\n" +
            "Silver Empire - A smithery selling the finest jewelry.";
        }

        private void WindowMarket_Closing(object sender, CancelEventArgs e)
        {
            RefToCityWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}