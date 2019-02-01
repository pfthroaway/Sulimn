using Extensions;
using Sulimn.Classes;
using System.Windows;

namespace Sulimn.Views.Exploration
{
    /// <summary>Interaction logic for ExplorePage.xaml</summary>
    public partial class ExplorePage
    {
        internal CityPage RefToCityPage { private get; set; }
        private bool _hardcoreDeath;

        #region Button Manipulation

        /// <summary>Checks the player's level to determine which buttons to allow to be enabled.</summary>
        private void CheckButtons()
        {
            BtnBack.IsEnabled = true;
            BtnFields.IsEnabled = true;
            BtnForest.IsEnabled = GameState.CurrentHero.Level >= 5 && GameState.CurrentHero.Progression.Fields;
            BtnCathedral.IsEnabled = GameState.CurrentHero.Level >= 10 && GameState.CurrentHero.Progression.Forest;
            BtnMines.IsEnabled = GameState.CurrentHero.Level >= 15 && GameState.CurrentHero.Progression.Cathedral;
            BtnCatacombs.IsEnabled = GameState.CurrentHero.Level >= 20 && GameState.CurrentHero.Progression.Mines;
            BtnCastle.IsEnabled = false; //GameState.CurrentHero.Level >= 25 && GameState.CurrentHero.Progression.Catacombs;
        }

        /// <summary>Handles closing the Page when a Hardcore character has died.</summary>
        internal void HardcoreDeath()
        {
            _hardcoreDeath = true;
            ClosePage();
        }

        /// <summary>Does the Hero have more than zero health?</summary>
        /// <returns>Whether the Hero has more than zero health</returns>
        private bool Healthy()
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0) return true;
            Functions.AddTextToTextBox(TxtExplore, "You need to heal before you can explore.");
            return false;
        }

        #endregion Button Manipulation

        #region Button-Click Methods

        private void BtnBack_Click(object sender, RoutedEventArgs e) => ClosePage();

        private void BtnCatacombs_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
                GameState.Navigate(new CatacombsPage { RefToExplorePage = this });
        }

        private void BtnCastle_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
                GameState.Navigate(new CastlePage { RefToExplorePage = this });
        }

        private void BtnCathedral_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
                GameState.Navigate(new CathedralPage { RefToExplorePage = this });
        }

        private void BtnFields_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
                GameState.Navigate(new FieldsPage { RefToExplorePage = this });
        }

        private void BtnForest_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
                GameState.Navigate(new ForestPage { RefToExplorePage = this });
        }

        private void BtnMines_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
                GameState.Navigate(new MinesPage { RefToExplorePage = this });
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
        }

        private void ExplorePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            
            CheckButtons();
        }

        #endregion Page-Generated Methods
    }
}