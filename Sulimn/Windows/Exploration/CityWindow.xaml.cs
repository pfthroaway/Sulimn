using Extensions;
using Sulimn.Classes;
using System.ComponentModel;
using System.Windows;

namespace Sulimn.Windows.Exploration
{
    /// <summary>Interaction logic for CityWindow.xaml</summary>
    public partial class CityWindow
    {
        internal MainWindow RefToMainWindow { private get; set; }

        internal async void HardcoreDeath()
        {
            await GameState.DeleteHero(GameState.CurrentHero);
            CloseWindow();
        }

        #region Button-Click Methods

        private void BtnBank_Click(object sender, RoutedEventArgs e)
        {
            Bank.BankWindow bankWindow = new Bank.BankWindow { RefToCityWindow = this };
            bankWindow.LoadBank();
            bankWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private async void BtnChapel_Click(object sender, RoutedEventArgs e)
        {
            if (
            decimal.Divide(GameState.CurrentHero.Statistics.CurrentHealth,
            GameState.CurrentHero.Statistics.MaximumHealth) <= 0.25M)
            {
                Functions.AddTextToTextBox(TxtCity, "You enter a local chapel and approach the altar. A priest approaches you.\n" +
                "\"Let me heal your wounds. You look like you've been through a tough battle.\"\n" +
                "The priest gives you a potion which heals you to full health!\n" +
                "You thank the priest and return to the streets.");
                GameState.CurrentHero.Statistics.CurrentHealth = GameState.CurrentHero.Statistics.MaximumHealth;

                await GameState.SaveHero(GameState.CurrentHero);
            }
            else
            {
                Functions.AddTextToTextBox(TxtCity, "You enter a local chapel. A priest approaches you.\n" +
                "\"You look healthy to me. If you ever need healing, don't hesitate to come see me.\"\n\n" +
                "You thank the priest and return to the streets.");
            }
        }

        private void BtnCharacter_Click(object sender, RoutedEventArgs e)
        {
            Characters.CharacterWindow characterWindow = new Characters.CharacterWindow { RefToCityWindow = this };
            characterWindow.Show();
            characterWindow.SetupChar();
            characterWindow.SetPreviousWindow("City");
            characterWindow.BindLabels();
            Visibility = Visibility.Hidden;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.YesNoNotification("Are you sure you want to exit?", "Sulimn", this))
                CloseWindow();
        }

        private void BtnExplore_Click(object sender, RoutedEventArgs e)
        {
            ExploreWindow exploreWindow = new ExploreWindow { RefToCityWindow = this };
            exploreWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnMarket_Click(object sender, RoutedEventArgs e)
        {
            Shopping.MarketWindow marketWindow = new Shopping.MarketWindow { RefToCityWindow = this };
            marketWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnOptions_Click(object sender, RoutedEventArgs e)
        {
            Options.HeroChangePasswordWindow heroChangePasswordWindow = new Options.HeroChangePasswordWindow { RefToCityWindow = this };
            heroChangePasswordWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnTavern_Click(object sender, RoutedEventArgs e)
        {
            TavernWindow tavernWindow = new TavernWindow { RefToCityWindow = this };
            tavernWindow.Show();
            Visibility = Visibility.Hidden;
        }

        #endregion Button-Click Methods

        #region Window-Generated Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public CityWindow()
        {
            InitializeComponent();
            TxtCity.Text =
            "You are in the city of Sulimn. There is a bustling market in the center of the city, a dilapidated cathedral sprawling over the cityscape at the north end, an abandoned mining complex on the south side, crop fields to the east, and a forest to the west.";
        }

        private void WindowCity_Closing(object sender, CancelEventArgs e)
        {
            RefToMainWindow.Show();
        }

        #endregion Window-Generated Methods
    }
}