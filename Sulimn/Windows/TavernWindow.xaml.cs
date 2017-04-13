using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>Interaction logic for TavernWindow.xaml</summary>
    public partial class TavernWindow
    {
        internal CityWindow RefToCityWindow { private get; set; }

        #region Button-Click Methods

        private void BtnBlackjack_Click(object sender, RoutedEventArgs e)
        {
            BlackjackWindow blackjackWindow = new BlackjackWindow { RefToTavernWindow = this };
            blackjackWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void BtnFood_Click(object sender, RoutedEventArgs e)
        {
            TheTavernBarWindow theTavernBarWindow = new TheTavernBarWindow { RefToTavernWindow = this };
            theTavernBarWindow.Show();
            Visibility = Visibility.Hidden;
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public TavernWindow()
        {
            InitializeComponent();
        }

        private void WindowTavern_Closing(object sender, CancelEventArgs e)
        {
            RefToCityWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}