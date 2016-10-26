using System.ComponentModel;
using System.Windows;

namespace Sulimn_WPF
{
    /// <summary>
    /// Interaction logic for TavernWindow.xaml
    /// </summary>
    public partial class TavernWindow : Window
    {
        internal CityWindow RefToCityWindow { get; set; }

        #region Button-Click Methods

        private void btnBlackjack_Click(object sender, RoutedEventArgs e)
        {
            BlackjackWindow blackjackWindow = new BlackjackWindow();
            blackjackWindow.RefToTavernWindow = this;
            blackjackWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void btnFood_Click(object sender, RoutedEventArgs e)
        {
            ShopWindow shopWindow = new ShopWindow();
            shopWindow.RefToTavernWindow = this;
            shopWindow.SetShopType("Food");
            shopWindow.LoadAll();
            shopWindow.Show();
            this.Visibility = Visibility.Hidden;
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

        public TavernWindow()
        {
            InitializeComponent();
        }

        private void windowTavern_Closing(object sender, CancelEventArgs e)
        {
            RefToCityWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}