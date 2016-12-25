using System;
using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>Interaction logic for MarketWindow.xaml</summary>
    public partial class MarketWindow
    {
        private readonly string _nl = Environment.NewLine;
        internal CityWindow RefToCityWindow { private get; set; }

        /// <summary>Adds text to the TextBox.</summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtMarket.Text += _nl + _nl + newText;
            txtMarket.Focus();
            txtMarket.CaretIndex = txtMarket.Text.Length;
            txtMarket.ScrollToEnd();
        }

        #region Button-Click Methods

        private void btnWeaponShop_Click(object sender, RoutedEventArgs e)
        {
            WeaponsRUsWindow weaponsRUsWindow = new WeaponsRUsWindow { RefToMarketWindow = this };
            weaponsRUsWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void btnArmorShop_Click(object sender, RoutedEventArgs e)
        {
            TheArmouryWindow theArmouryWindow = new TheArmouryWindow { RefToMarketWindow = this };
            theArmouryWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void btnGeneralStore_Click(object sender, RoutedEventArgs e)
        {
            TheGeneralStoreWindow theGeneralStoreWindow = new TheGeneralStoreWindow { RefToMarketWindow = this };
            theGeneralStoreWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void btnMagicShop_Click(object sender, RoutedEventArgs e)
        {
            MagickShoppeWindow magickShoppeWindow = new MagickShoppeWindow { RefToMarketWindow = this };
            magickShoppeWindow.LoadAll();
            magickShoppeWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void btnSilverEmpire_Click(object sender, RoutedEventArgs e)
        {
            SilverEmpireWindow silverEmpireWindow = new SilverEmpireWindow { RefToMarketWindow = this };
            silverEmpireWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
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
            txtMarket.Text = "You enter a bustling market.There are many shops here, the most interesting being:" + _nl +
             _nl + "Weapons 'R' Us - A weapons shop." + _nl + _nl + "The Armoury - An armor shop." + _nl +
             _nl + "The General Store - A shop supplying general goods like potions." + _nl + _nl +
             "Ye Old Magick Shoppe - A shop selling magical spells and equipment." + _nl + _nl +
             "Silver Empire - A smithery selling the finest jewelry.";
        }

        private void windowMarket_Closing(object sender, CancelEventArgs e)
        {
            RefToCityWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}