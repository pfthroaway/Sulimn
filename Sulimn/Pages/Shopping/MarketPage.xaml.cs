using Sulimn.Classes;
using Sulimn.Pages.Exploration;
using System.Windows;

namespace Sulimn.Pages.Shopping
{
    /// <summary>Interaction logic for MarketPage.xaml</summary>
    public partial class MarketPage
    {
        internal CityPage RefToCityPage { private get; set; }

        #region Button-Click Methods

        private void BtnWeaponShop_Click(object sender, RoutedEventArgs e) => GameState.Navigate(
            new WeaponsRUsPage { RefToMarketPage = this });

        private void BtnArmorShop_Click(object sender, RoutedEventArgs e) => GameState.Navigate(
            new TheArmouryPage { RefToMarketPage = this });

        private void BtnGeneralStore_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new TheGeneralStorePage { RefToMarketPage = this });

        private void BtnMagicShop_Click(object sender, RoutedEventArgs e)
        {
            MagickShoppePage magickShoppePage = new MagickShoppePage { RefToMarketPage = this };
            magickShoppePage.LoadAll();
            GameState.Navigate(magickShoppePage);
        }

        private void BtnSilverEmpire_Click(object sender, RoutedEventArgs e) =>
            GameState.Navigate(new SilverEmpirePage { RefToMarketPage = this });

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage()
        {
            GameState.GoBack();
        }

        public MarketPage()
        {
            InitializeComponent();
            TxtMarket.Text = "You enter a bustling market.There are many shops here, the most interesting being:\n\n" +
            "Weapons 'R' Us - A weapons shop.\n\n" +
            "The Armoury - An armor shop.\n\n" +
            "The General Store - A shop supplying general goods like potions.\n\n" +
            "Ye Old Magick Shoppe - A shop selling magical spells and equipment.\n\n" +
            "Silver Empire - A smithery selling the finest jewelry.";
        }

        #endregion Page-Manipulation Methods

        private void MarketPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameState.CalculateScale(Grid);
        }
    }
}