using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>Interaction logic for ArmoryWindow.xaml</summary>
    public partial class ArmoryWindow : Window
    {
        private void GoShopping(ItemTypes shopType)
        {
            ShopWindow shopWindow = new ShopWindow();
            shopWindow.RefToArmoryWindow = this;
            shopWindow.SetShopType(shopType);
            shopWindow.LoadAll();
            shopWindow.Show();
        }

        internal MarketWindow RefToMarketWindow { get; set; }

        #region Button-Click Methods

        private void btnHead_Click(object sender, RoutedEventArgs e)
        {
            GoShopping(ItemTypes.Head);
        }

        private void btnBody_Click(object sender, RoutedEventArgs e)
        {
            GoShopping(ItemTypes.Body);
        }

        private void btnLegs_Click(object sender, RoutedEventArgs e)
        {
            GoShopping(ItemTypes.Legs);
        }

        private void btnFeet_Click(object sender, RoutedEventArgs e)
        {
            GoShopping(ItemTypes.Feet);
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
            this.Close();
        }

        public ArmoryWindow()
        {
            InitializeComponent();
        }

        private void windowArmory_Closing(object sender, CancelEventArgs e)
        {
            RefToMarketWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}