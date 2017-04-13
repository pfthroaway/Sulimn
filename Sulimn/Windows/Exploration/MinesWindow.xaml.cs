using Extensions;
using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>Interaction logic for MinesWindow.xaml</summary>
    public partial class MinesWindow
    {
        internal ExploreWindow RefToExploreWindow { private get; set; }

        /// <summary>Starts a battle.</summary>
        private void StartBattle()
        {
            BattleWindow battleWindow = new BattleWindow { RefToMinesWindow = this };
            battleWindow.PrepareBattle("Mines");
            battleWindow.Show();
            Visibility = Visibility.Hidden;
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
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public MinesWindow()
        {
            InitializeComponent();
            TxtMines.Text =
            "You enter the abandoned mines. The path splits very near to the entrance, with paths leading south, east, and west. There are offices nearby. A crumbling, barely legible sign shows that the south path leads a shaft that goes to the ore bin, the east path leads to pump station, and the west path leads to the workshop.";
        }

        private void WindowMines_Closing(object sender, CancelEventArgs e)
        {
            RefToExploreWindow.Show();
            RefToExploreWindow.CheckButtons();
        }

        #endregion Window-Manipulation Methods
    }
}