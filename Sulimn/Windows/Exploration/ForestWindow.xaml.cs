using Extensions;
using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for ForestWindow.xaml
    /// </summary>
    public partial class ForestWindow
    {
        internal ExploreWindow RefToExploreWindow { private get; set; }
        private bool _hardcoreDeath = false;

        /// <summary>Handles closing the Window when a Hardcore character has died.</summary>
        internal void HardcoreDeath()
        {
            _hardcoreDeath = true;
            CloseWindow();
        }

        /// <summary>Starts a battle.</summary>
        private void StartBattle()
        {
            BattleWindow battleWindow = new BattleWindow { RefToForestWindow = this };
            battleWindow.PrepareBattle("Forest");
            battleWindow.Show();
            Visibility = Visibility.Hidden;
        }

        /// <summary>Special encounter.</summary>
        private async void SpecialEncounter()
        {
            Functions.AddTextToTextBox(TxtForest, await GameState.EventFindGold(200, 1000));
        }

        #region Button-Click Methods

        private async void BtnClearing_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                    Functions.AddTextToTextBox(TxtForest, await GameState.EventFindGold(50, 300));
                else if (result <= 50)
                    Functions.AddTextToTextBox(TxtForest, await GameState.EventFindItem(100, 350));
                else if (result <= 85)
                {
                    GameState.EventEncounterEnemy("Knave", "Wolf", "Wild Boar");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Mangy Dog", "Snake", "Thief");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtForest, "You need to heal before you can explore.");
        }

        private async void BtnCottage_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    Functions.AddTextToTextBox(TxtForest, await GameState.EventFindGold(25, 200));
                else if (result <= 60)
                    Functions.AddTextToTextBox(TxtForest, await GameState.EventFindItem(50, 250));
                else if (result <= 80)
                {
                    GameState.EventEncounterEnemy("Butcher", "Knave");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Squire");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtForest, "You need to heal before you can explore.");
        }

        private async void BtnCave_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 10)
                    Functions.AddTextToTextBox(TxtForest, await GameState.EventFindGold(50, 300));
                else if (result <= 30)
                    Functions.AddTextToTextBox(TxtForest, await GameState.EventFindItem(100, 350));
                else if (result <= 90)
                {
                    GameState.EventEncounterEnemy("Bear", "Wolf", "Wild Boar");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Mangy Dog", "Beggar");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtForest, "You need to heal before you can explore.");
        }

        private void BtnInvestigate_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 5)
                    SpecialEncounter();
                else
                {
                    GameState.EventEncounterEnemy("Butcher", "Bear", "Wild Boar", "Thief", "Knave");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtForest, "You need to heal before you can explore.");
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

        public ForestWindow()
        {
            InitializeComponent();
            TxtForest.Text =
            "You travel west along a beaten path into the dark forest. After a short while, you come to a \"T\" fork in the path. You can see the faint silhouette of a cottage to your left, and the sun pouring into a clearing to your right. Ahead of you, through the trees, you see a small cave entrance. Suddenly, you hear the distinct sound of a stick snapping close behind you.";
        }

        private void WindowForest_Closing(object sender, CancelEventArgs e)
        {
            if (!_hardcoreDeath)
            {
                RefToExploreWindow.Show();
                RefToExploreWindow.CheckButtons();
            }
            else
                RefToExploreWindow.HardcoreDeath();
        }

        #endregion Window-Manipulation Methods
    }
}