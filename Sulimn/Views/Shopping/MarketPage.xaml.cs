using Sulimn.Classes;
using System.Windows;

namespace Sulimn.Views.Shopping
{
    /// <summary>Interaction logic for MarketPage.xaml</summary>
    public partial class MarketPage
    {
        #region Button-Click Methods

        private void BtnWeaponShop_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new WeaponsRUsPage());

        private void BtnArmorShop_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new TheArmouryPage());

        private void BtnGeneralStore_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new TheGeneralStorePage());

        private void BtnMagicShop_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new MagickShoppePage());

        private void BtnSilverEmpire_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new SilverEmpirePage());

        private void BtnSmithy_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new TheSmithyPage());

        private void BtnBack_Click(object sender, RoutedEventArgs e) => GameState.GoBack();

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>

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
    }
}