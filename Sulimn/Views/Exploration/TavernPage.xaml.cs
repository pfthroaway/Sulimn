using Sulimn.Classes;
using Sulimn.Views.Gambling;
using Sulimn.Views.Shopping;
using System.Windows;

namespace Sulimn.Views.Exploration
{
    /// <summary>Interaction logic for TavernPage.xaml</summary>
    public partial class TavernPage
    {
        #region Button-Click Methods

        private void BtnBlackjack_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new BlackjackPage());

        private void BtnExit_Click(object sender, RoutedEventArgs e) => ClosePage();

        private void BtnFood_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new TheTavernBarPage());

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage() => GameState.GoBack();

        public TavernPage() => InitializeComponent();

        #endregion Page-Manipulation Methods
    }
}