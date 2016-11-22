using System;
using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for MinesWindow.xaml
    /// </summary>
    public partial class MinesWindow : Window
    {
        internal ExploreWindow RefToExploreWindow { get; set; }

        /// <summary>
        /// Adds text to the txtMines TextBox.
        /// </summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            string nl = Environment.NewLine;
            txtMines.Text += nl + nl + newText;
            txtMines.Focus();
            txtMines.CaretIndex = txtMines.Text.Length;
            txtMines.ScrollToEnd();
        }

        /// <summary>
        /// Starts a battle.
        /// </summary>
        private void StartBattle()
        {
            BattleWindow battleWindow = new BattleWindow();
            battleWindow.RefToMinesWindow = this;
            battleWindow.PrepareBattle("Mines");
            battleWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        #region Button-Click Methods

        private async void btnOffices_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    AddTextTT(await GameState.EventFindGold(200, 600));
                else if (result <= 40)
                    AddTextTT(await GameState.EventFindItem(250, 650));
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
                AddTextTT("You need to heal before you can explore.");
        }

        private async void btnOreBin_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    AddTextTT(await GameState.EventFindGold(300, 700));
                else if (result <= 40)
                    AddTextTT(await GameState.EventFindItem(350, 750));
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
                AddTextTT("You need to heal before you can explore.");
        }

        private async void btnPumpStation_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 10)
                    AddTextTT(await GameState.EventFindGold(200, 600));
                else if (result <= 30)
                    AddTextTT(await GameState.EventFindItem(250, 650));
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
                AddTextTT("You need to heal before you can explore.");
        }

        private async void btnWorkshop_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 10)
                    AddTextTT(await GameState.EventFindGold(300, 700));
                else if (result <= 30)
                    AddTextTT(await GameState.EventFindItem(350, 750));
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
                AddTextTT("You need to heal before you can explore.");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
        }

        public MinesWindow()
        {
            InitializeComponent();
            txtMines.Text = "You enter the abandoned mines. The path splits very near to the entrance, with paths leading south, east, and west. There are offices nearby. A crumbling, barely legible sign shows that the south path leads a shaft that goes to the ore bin, the east path leads to pump station, and the west path leads to the workshop.";
        }

        private void windowMines_Closing(object sender, CancelEventArgs e)
        {
            RefToExploreWindow.Show();
            RefToExploreWindow.CheckButtons();
        }

        #endregion Window-Manipulation Methods
    }
}