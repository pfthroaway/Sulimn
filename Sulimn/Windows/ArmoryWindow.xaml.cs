using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for ArmoryWindow.xaml
    /// </summary>
    public partial class ArmoryWindow : Window
    {
        private bool goShopping = false;
        private ItemTypes shopType;

        internal MarketWindow RefToMarketWindow { get; set; }

        #region Button-Click Methods

        private void btnHead_Click(object sender, RoutedEventArgs e)
        {
            shopType = ItemTypes.Head;
            goShopping = true;
            CloseWindow();
        }

        private void btnBody_Click(object sender, RoutedEventArgs e)
        {
            shopType = ItemTypes.Body;
            goShopping = true;
            CloseWindow();
        }

        private void btnLegs_Click(object sender, RoutedEventArgs e)
        {
            shopType = ItemTypes.Legs;
            goShopping = true;
            CloseWindow();
        }

        private void btnFeet_Click(object sender, RoutedEventArgs e)
        {
            shopType = ItemTypes.Feet;
            goShopping = true;
            CloseWindow();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
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
            if (goShopping)
            {
                ShopWindow shopWindow = new ShopWindow();
                shopWindow.RefToMarketWindow = RefToMarketWindow;
                shopWindow.SetShopType(shopType);
                shopWindow.LoadAll();
                shopWindow.Show();
            }
            else
                RefToMarketWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}