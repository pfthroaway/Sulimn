using Extensions;
using Sulimn.Classes;
using System.ComponentModel;
using System.Windows;

namespace Sulimn.Pages.Exploration
{
    /// <summary>Interaction logic for ExplorePage.xaml</summary>
    public partial class ExplorePage
    {
        internal CityPage RefToCityPage { private get; set; }
        private bool _hardcoreDeath = false;

        #region Button Manipulation

        /// <summary>Checks the player's level to determine which buttons to allow to be enabled.</summary>
        internal void CheckButtons()
        {
            BtnBack.IsEnabled = true;
            BtnFields.IsEnabled = true;
            BtnForest.IsEnabled = GameState.CurrentHero.Level >= 5;
            BtnCathedral.IsEnabled = GameState.CurrentHero.Level >= 10;
            BtnMines.IsEnabled = GameState.CurrentHero.Level >= 15;
            BtnCatacombs.IsEnabled = GameState.CurrentHero.Level >= 20;
        }

        /// <summary>Handles closing the Page when a Hardcore character has died.</summary>
        internal void HardcoreDeath()
        {
            _hardcoreDeath = true;
            ClosePage();
        }

        #endregion Button Manipulation

        #region Button-Click Methods

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }

        private void BtnCatacombs_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
                GameState.Navigate(new CatacombsPage { RefToExplorePage = this });
            else
                Functions.AddTextToTextBox(TxtExplore, "You need to heal before you can explore.");
        }

        private void BtnCathedral_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
                GameState.Navigate(new CathedralPage { RefToExplorePage = this });
            else
                Functions.AddTextToTextBox(TxtExplore, "You need to heal before you can explore.");
        }

        private void BtnFields_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
                GameState.Navigate(new FieldsPage { RefToExplorePage = this });
            else
                Functions.AddTextToTextBox(TxtExplore, "You need to heal before you can explore.");
        }

        private void BtnForest_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
                GameState.Navigate(new ForestPage { RefToExplorePage = this });
            else
                Functions.AddTextToTextBox(TxtExplore, "You need to heal before you can explore.");
        }

        private void BtnMines_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
                GameState.Navigate(new MinesPage { RefToExplorePage = this });
            else
                Functions.AddTextToTextBox(TxtExplore, "You need to heal before you can explore.");
        }

        #endregion Button-Click Methods

        #region Page-Generated Methods

        private void ClosePage()
        {
            if (_hardcoreDeath)
                RefToCityPage.HardcoreDeath();
            GameState.GoBack();
        }

        public ExplorePage()
        {
            InitializeComponent();
            TxtExplore.Text =
            "There are some safe-looking farms and crop fields to the east where many adventurers have gone to prove their worth.\n\n" +
            "There is a dark forest to the west, which looks like it has devoured its fair share of adventurers.\n\n" +
            "There is a dilapidated cathedral at the north end of the city, spreading fear and hopelessness to everyone in its shadow.\n\n" +
            "There is an abandoned mining complex on the south side of the city, which looks like no one has entered or come out of it for years.\n\n" +
            "You have also heard stories of catacombs running beneath the city. The entrance will reveal itself after you have explored more of Sulimn.";
            CheckButtons();
        }

        private void PageExplore_Closing(object sender, CancelEventArgs e)
        {
        }

        #endregion Page-Generated Methods

        private void ExplorePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameState.CalculateScale(Grid);
        }
    }
}