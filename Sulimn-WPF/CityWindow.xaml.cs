using System;
using System.ComponentModel;
using System.Windows;

namespace Sulimn_WPF
{
    /// <summary>
    /// Interaction logic for CityWindow.xaml
    /// </summary>
    public partial class CityWindow : Window
    {
        internal MainWindow RefToMainWindow { get; set; }
        private string nl = Environment.NewLine;

        /// <summary>
        /// Adds text to the txtCity TextBox.
        /// </summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtCity.Text += nl + nl + newText;
            txtCity.Focus();
            txtCity.CaretIndex = txtCity.Text.Length;
            txtCity.ScrollToEnd();
        }

        /// <summary>
        /// Displays text on entering the city.
        /// </summary>
        internal void EnterCity()
        {
            txtCity.Text = "You are in the city of Sulimn. There is a bustling market in the center of the city, a dilapidated cathedral sprawling over the cityscape at the north end, an abandoned mining complex on the south side, crop fields to the east, and a forest to the west.";
        }

        #region Button-Click Methods

        private void btnBank_Click(object sender, RoutedEventArgs e)
        {
            //frmBank BankWindow = new frmBank();
            //BankWindow.RefToCityWindow = this;
            //BankWindow.LoadBank();
            //BankWindow.Show();
            //this.Visibility = Visibility.Hidden;
        }

        private void btnChapel_Click(object sender, RoutedEventArgs e)
        {
            if (Decimal.Divide(GameState.currentHero.CurrentHealth, GameState.currentHero.MaximumHealth) <= Convert.ToDecimal(0.25))
            {
                AddTextTT("You enter a local chapel and approach the altar. A priest approaches you." + nl + "\"Let me assist you. You look like you've been through a tough battle.\"" + nl + "The priest gives you a potion which heals you to full health!" + nl + "You thank the priest and return to the streets.");
                GameState.currentHero.CurrentHealth = GameState.currentHero.MaximumHealth;
                GameState.SaveHero();
            }
            else
                AddTextTT("You enter a local chapel. A priest approaches you." + nl + "\"You look healthy to me. If you ever need healing, don't hesitate to come see me.\"" + nl + nl + "You thank the priest and return to the streets.");
        }

        private void btnCharacter_Click(object sender, RoutedEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow();

            characterWindow.RefToCityWindow = this;
            this.Visibility = Visibility.Hidden;
            characterWindow.Show();
            characterWindow.SetupChar();
            characterWindow.SetPreviousWindow("City");
            characterWindow.BindLabels();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void btnExplore_Click(object sender, RoutedEventArgs e)
        {
            ExploreWindow exploreWindow = new ExploreWindow();
            exploreWindow.LoadExplore();
            exploreWindow.RefToCityWindow = this;
            this.Visibility = Visibility.Hidden;
            exploreWindow.Show();
        }

        private void btnMarket_Click(object sender, RoutedEventArgs e)
        {
            //frmMarket MarketWindow = new frmMarket();
            //MarketWindow.RefToCityWindow = this;
            //MarketWindow.Show();
            //this.Visibility = Visibility.Hidden;
        }

        private void btnTavern_Click(object sender, RoutedEventArgs e)
        {
            //frmTavern tavernWindow = new frmTavern();
            //tavernWindow.RefToCityWindow = this;
            //tavernWindow.Show();
            //this.Visibility = Visibility.Hidden;
        }

        #endregion Button-Click Methods

        #region Window-Generated Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
        }

        public CityWindow()
        {
            InitializeComponent();
        }

        private void windowCity_Closing(object sender, CancelEventArgs e)
        {
            RefToMainWindow.Show();
        }

        #endregion Window-Generated Methods
    }
}