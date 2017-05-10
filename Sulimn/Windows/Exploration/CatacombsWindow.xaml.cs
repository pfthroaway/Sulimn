using Extensions;
using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>Interaction logic for CatacombsWindow.xaml</summary>
    public partial class CatacombsWindow
    {
        internal ExploreWindow RefToExploreWindow { private get; set; }
        private bool _hardcoreDeath = false;

        /// <summary>Starts a battle.</summary>
        private void StartBattle()
        {
            BattleWindow battleWindow = new BattleWindow { RefToCatacombsWindow = this };
            battleWindow.PrepareBattle("Catacombs");
            battleWindow.Show();
            Visibility = Visibility.Hidden;
        }

        /// <summary>Handles closing the Window when a Hardcore character has died.</summary>
        internal void HardcoreDeath()
        {
            _hardcoreDeath = true;
            CloseWindow();
        }

        #region Button-Click Methods

        private async void BtnCrypts_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    Functions.AddTextToTextBox(TxtCatacombs, await GameState.EventFindGold(400, 800));
                else if (result <= 40)
                    Functions.AddTextToTextBox(TxtCatacombs, await GameState.EventFindItem(500, 1000));
                else
                {
                    GameState.EventEncounterEnemy("Giant Spider", "Necromancer", "Priest", "Dark Priest", "Adventurer",
                    "Knight", "Minotaur", "Evil Knight", "Giant Bat");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtCatacombs, "You need to heal before you can explore.");
        }

        private async void BtnShantytown_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                    Functions.AddTextToTextBox(TxtCatacombs, await GameState.EventFindGold(50, 200));
                else if (result <= 30)
                    Functions.AddTextToTextBox(TxtCatacombs, await GameState.EventFindItem(100, 300));
                else
                {
                    GameState.EventEncounterEnemy("Beggar", "Thief", "Butcher", "Squire", "Adventurer", "Knave",
                    "Mangy Dog");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtCatacombs, "You need to heal before you can explore.");
        }

        private async void BtnRavine_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 5)
                    Functions.AddTextToTextBox(TxtCatacombs, await GameState.EventFindGold(400, 800));
                else if (result <= 20)
                    Functions.AddTextToTextBox(TxtCatacombs, await GameState.EventFindItem(500, 1000));
                else
                {
                    GameState.EventEncounterEnemy("Giant Spider", "Necromancer", "Priest", "Dark Priest", "Adventurer",
                    "Knight", "Minotaur", "Evil Knight", "Giant Bat", "Mangy Dog");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtCatacombs, "You need to heal before you can explore.");
        }

        private async void BtnAqueduct_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 5)
                    Functions.AddTextToTextBox(TxtCatacombs, await GameState.EventFindGold(450, 900));
                else if (result <= 20)
                    Functions.AddTextToTextBox(TxtCatacombs, await GameState.EventFindItem(500, 1000));
                else
                {
                    GameState.EventEncounterEnemy("Giant Spider", "Necromancer", "Priest", "Dark Priest", "Adventurer",
                    "Knight", "Minotaur", "Evil Knight", "Giant Bat", "Mangy Dog");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtCatacombs, "You need to heal before you can explore.");
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

        public CatacombsWindow()
        {
            InitializeComponent();
            TxtCatacombs.Text =
            "You find the entrance to the catacombs, a long series of underground passages created throughout the last several hundred years. Thousands of people were buried here in crypts. You've heard of a shantytown down here, a place for the less fortunate to sleep at night. There is also supposed to be a large ravine down here to explore. Also, an ancient aqueduct system runs beneath the city, transporting water all over the city.";
        }

        private void WindowCatacombs_Closing(object sender, CancelEventArgs e)
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