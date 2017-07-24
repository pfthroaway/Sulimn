using Extensions;
using Sulimn.Classes;
using Sulimn.Pages.Battle;
using System.Windows;

namespace Sulimn.Pages.Exploration
{
    /// <summary>Interaction logic for MinesPage.xaml</summary>
    public partial class MinesPage
    {
        internal ExplorePage RefToExplorePage { private get; set; }
        private bool _hardcoreDeath = false;

        /// <summary>Handles closing the Page when a Hardcore character has died.</summary>
        internal void HardcoreDeath()
        {
            _hardcoreDeath = true;
            ClosePage();
        }

        /// <summary>Starts a battle.</summary>
        private void StartBattle()
        {
            BattlePage battlePage = new BattlePage { RefToMinesPage = this };
            battlePage.PrepareBattle("Mines");
            GameState.MainWindow.MainFrame.Navigate(battlePage);
        }

        #region Button-Click Methods

        private async void BtnOffices_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    Functions.AddTextToTextBox(TxtMines, await GameState.EventFindGold(200, 600));
                else if (result <= 40)
                    Functions.AddTextToTextBox(TxtMines, await GameState.EventFindItem(250, 650));
                else if (result <= 80)
                {
                    GameState.EventEncounterEnemy("Giant Spider", "Lion", "Crazed Miner", "Giant Bat");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Knight", "Adventurer");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtMines, "You need to heal before you can explore.");
        }

        private async void BtnOreBin_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    Functions.AddTextToTextBox(TxtMines, await GameState.EventFindGold(300, 700));
                else if (result <= 40)
                    Functions.AddTextToTextBox(TxtMines, await GameState.EventFindItem(350, 750));
                else if (result <= 80)
                {
                    GameState.EventEncounterEnemy("Giant Spider", "Lion", "Crazed Miner", "Giant Bat");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Knight", "Adventurer");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtMines, "You need to heal before you can explore.");
        }

        private async void BtnPumpStation_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 10)
                    Functions.AddTextToTextBox(TxtMines, await GameState.EventFindGold(200, 600));
                else if (result <= 30)
                    Functions.AddTextToTextBox(TxtMines, await GameState.EventFindItem(250, 650));
                else if (result <= 75)
                {
                    GameState.EventEncounterEnemy("Giant Spider", "Lion", "Crazed Miner", "Giant Bat");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Adventurer", "Gladiator", "Crazed Miner");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtMines, "You need to heal before you can explore.");
        }

        private async void BtnWorkshop_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 10)
                    Functions.AddTextToTextBox(TxtMines, await GameState.EventFindGold(300, 700));
                else if (result <= 30)
                    Functions.AddTextToTextBox(TxtMines, await GameState.EventFindItem(350, 750));
                else if (result <= 80)
                {
                    GameState.EventEncounterEnemy("Giant Spider", "Lion", "Crazed Miner", "Giant Bat");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Knight", "Adventurer", "Monk", "Gladiator");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtMines, "You need to heal before you can explore.");
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage()
        {
            if (!_hardcoreDeath)
                RefToExplorePage.CheckButtons();
            else
                RefToExplorePage.HardcoreDeath();
            GameState.MainWindow.MainFrame.GoBack();
        }

        public MinesPage()
        {
            InitializeComponent();
            TxtMines.Text =
            "You enter the abandoned mines. The path splits very near to the entrance, with paths leading south, east, and west. There are offices nearby. A crumbling, barely legible sign shows that the south path leads a shaft that goes to the ore bin, the east path leads to pump station, and the west path leads to the workshop.";
        }

        #endregion Page-Manipulation Methods

        private void MinesPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameState.CalculateScale(Grid);
        }
    }
}