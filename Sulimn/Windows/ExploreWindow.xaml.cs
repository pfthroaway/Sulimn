using System;
using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for ExploreWindow.xaml
    /// </summary>
    public partial class ExploreWindow : Window
    {
        internal CityWindow RefToCityWindow { get; set; }
        private string nl = Environment.NewLine;

        /// <summary>
        /// Adds text to the txtExplore TextBox.
        /// </summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtExplore.Text += nl + nl + newText;
            txtExplore.Focus();
            txtExplore.CaretIndex = txtExplore.Text.Length;
            txtExplore.ScrollToEnd();
        }

        #region Button Manipulation

        /// <summary>
        /// Checks the player's level to determine which buttons to allow to be enabled.
        /// </summary>
        internal void CheckButtons()
        {
            btnBack.IsEnabled = true;
            btnFields.IsEnabled = true;
            if (GameState.CurrentHero.Level >= 5)
                btnForest.IsEnabled = true;
            if (GameState.CurrentHero.Level >= 10)
                btnCathedral.IsEnabled = true;
            if (GameState.CurrentHero.Level >= 15)
                btnMines.IsEnabled = true;
            if (GameState.CurrentHero.Level >= 20)
                btnCatacombs.IsEnabled = true;
        }

        /// <summary>
        /// Disable all the buttons.
        /// </summary>
        private void DisableButtons()
        {
            btnBack.IsEnabled = false;
            btnCathedral.IsEnabled = false;
            btnFields.IsEnabled = false;
            btnForest.IsEnabled = false;
            btnMines.IsEnabled = false;
        }

        #endregion Button Manipulation

        #region Button-Click Methods

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void btnCatacombs_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                CatacombsWindow catacombsWindow = new CatacombsWindow();
                catacombsWindow.RefToExploreWindow = this;
                catacombsWindow.Show();
                this.Visibility = Visibility.Hidden;
            }
            else
                AddTextTT("You need to heal before you can explore.");
        }

        private void btnCathedral_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                CathedralWindow cathedralWindow = new CathedralWindow();
                cathedralWindow.RefToExploreWindow = this;
                cathedralWindow.Show();
                this.Visibility = Visibility.Hidden;
            }
            else
                AddTextTT("You need to heal before you can explore.");
        }

        private void btnFields_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                FieldsWindow fieldsWindow = new FieldsWindow();
                fieldsWindow.RefToExploreWindow = this;
                fieldsWindow.Show();
                this.Visibility = Visibility.Hidden;
            }
            else
                AddTextTT("You need to heal before you can explore.");
        }

        private void btnForest_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                ForestWindow forestWindow = new ForestWindow();
                forestWindow.RefToExploreWindow = this;
                forestWindow.Show();
                this.Visibility = Visibility.Hidden;
            }
            else
                AddTextTT("You need to heal before you can explore.");
        }

        private void btnMines_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                MinesWindow minesWindow = new MinesWindow();
                minesWindow.RefToExploreWindow = this;
                minesWindow.Show();
                this.Visibility = Visibility.Hidden;
            }
            else
                AddTextTT("You need to heal before you can explore.");
        }

        #endregion Button-Click Methods

        #region Window-Generated Methods

        private void CloseWindow()
        {
            this.Close();
        }

        public ExploreWindow()
        {
            InitializeComponent();
            txtExplore.Text = "There are some safe-looking farms and crop fields to the east where many adventurers have gone to prove their worth." + nl + nl + "There is a dark forest to the west, which looks like it has devoured its fair share of adventurers." + nl + nl + "There is a dilapidated cathedral at the north end of the city, spreading fear and hopelessness to everyone in its shadow." + nl + nl + "There is an abandoned mining complex on the south side of the city, which looks like no one has entered or come out of it for years." + nl + nl + "You have also heard stories of catacombs running beneath the city. The entrance will reveal itself after you have explored more of Sulimn.";
            CheckButtons();
        }

        private void windowExplore_Closing(object sender, CancelEventArgs e)
        {
            RefToCityWindow.Show();
        }

        #endregion Window-Generated Methods
    }
}