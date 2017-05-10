using System.ComponentModel;
using System.Windows;
using Extensions;

namespace Sulimn
{
    /// <summary>Interaction logic for ExploreWindow.xaml</summary>
    public partial class ExploreWindow
    {
        internal CityWindow RefToCityWindow { private get; set; }
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

        /// <summary>Handles closing the Window when a Hardcore character has died.</summary>
        internal void HardcoreDeath()
        {
            _hardcoreDeath = true;
            CloseWindow();
        }

        #endregion Button Manipulation

        #region Button-Click Methods

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void BtnCatacombs_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                CatacombsWindow catacombsWindow = new CatacombsWindow { RefToExploreWindow = this };
                catacombsWindow.Show();
                Visibility = Visibility.Hidden;
            }
            else
                Functions.AddTextToTextBox(TxtExplore, "You need to heal before you can explore.");
        }

        private void BtnCathedral_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                CathedralWindow cathedralWindow = new CathedralWindow { RefToExploreWindow = this };
                cathedralWindow.Show();
                Visibility = Visibility.Hidden;
            }
            else
                Functions.AddTextToTextBox(TxtExplore, "You need to heal before you can explore.");
        }

        private void BtnFields_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                FieldsWindow fieldsWindow = new FieldsWindow { RefToExploreWindow = this };
                fieldsWindow.Show();
                Visibility = Visibility.Hidden;
            }
            else
                Functions.AddTextToTextBox(TxtExplore, "You need to heal before you can explore.");
        }

        private void BtnForest_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                ForestWindow forestWindow = new ForestWindow { RefToExploreWindow = this };
                forestWindow.Show();
                Visibility = Visibility.Hidden;
            }
            else
                Functions.AddTextToTextBox(TxtExplore, "You need to heal before you can explore.");
        }

        private void BtnMines_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                MinesWindow minesWindow = new MinesWindow { RefToExploreWindow = this };
                minesWindow.Show();
                Visibility = Visibility.Hidden;
            }
            else
                Functions.AddTextToTextBox(TxtExplore, "You need to heal before you can explore.");
        }

        #endregion Button-Click Methods

        #region Window-Generated Methods

        private void CloseWindow()
        {
            Close();
        }

        public ExploreWindow()
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

        private void WindowExplore_Closing(object sender, CancelEventArgs e)
        {
            if (!_hardcoreDeath)
                RefToCityWindow.Show();
            else
                RefToCityWindow.HardcoreDeath();
        }

        #endregion Window-Generated Methods
    }
}