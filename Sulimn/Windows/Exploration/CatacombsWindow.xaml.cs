using System;
using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for CatacombsWindow.xaml
    /// </summary>
    public partial class CatacombsWindow
    {
        internal ExploreWindow RefToExploreWindow { get; set; }

        /// <summary>
        /// Adds text to the txtCatacombs TextBox.
        /// </summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            string nl = Environment.NewLine;
            txtCatacombs.Text += nl + nl + newText;
            txtCatacombs.Focus();
            txtCatacombs.CaretIndex = txtCatacombs.Text.Length;
            txtCatacombs.ScrollToEnd();
        }

        /// <summary>
        /// Starts a battle.
        /// </summary>
        private void StartBattle()
        {
            BattleWindow battleWindow = new BattleWindow { RefToCatacombsWindow = this };
            battleWindow.PrepareBattle("Catacombs");
            battleWindow.Show();
            Visibility = Visibility.Hidden;
        }

        #region Button-Click Methods

        private async void btnCrypts_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                {
                    AddTextTT(await GameState.EventFindGold(400, 800));
                }
                else if (result <= 40)
                {
                    AddTextTT(await GameState.EventFindItem(500, 1000));
                }
                else
                {
                    GameState.EventEncounterEnemy("Giant Spider", "Necromancer", "Priest", "Dark Priest", "Adventurer",
                    "Knight", "Minotaur", "Evil Knight", "Giant Bat");
                    StartBattle();
                }
            }
            else
            {
                AddTextTT("You need to heal before you can explore.");
            }
        }

        private async void btnShantytown_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                {
                    AddTextTT(await GameState.EventFindGold(50, 200));
                }
                else if (result <= 30)
                {
                    AddTextTT(await GameState.EventFindItem(100, 300));
                }
                else
                {
                    GameState.EventEncounterEnemy("Beggar", "Thief", "Butcher", "Squire", "Adventurer", "Knave",
                    "Mangy Dog");
                    StartBattle();
                }
            }
            else
            {
                AddTextTT("You need to heal before you can explore.");
            }
        }

        private async void btnRavine_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 5)
                {
                    AddTextTT(await GameState.EventFindGold(400, 800));
                }
                else if (result <= 20)
                {
                    AddTextTT(await GameState.EventFindItem(500, 1000));
                }
                else
                {
                    GameState.EventEncounterEnemy("Giant Spider", "Necromancer", "Priest", "Dark Priest", "Adventurer",
                    "Knight", "Minotaur", "Evil Knight", "Giant Bat", "Mangy Dog");
                    StartBattle();
                }
            }
            else
            {
                AddTextTT("You need to heal before you can explore.");
            }
        }

        private async void btnAqueduct_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 5)
                {
                    AddTextTT(await GameState.EventFindGold(450, 900));
                }
                else if (result <= 20)
                {
                    AddTextTT(await GameState.EventFindItem(500, 1000));
                }
                else
                {
                    GameState.EventEncounterEnemy("Giant Spider", "Necromancer", "Priest", "Dark Priest", "Adventurer",
                    "Knight", "Minotaur", "Evil Knight", "Giant Bat", "Mangy Dog");
                    StartBattle();
                }
            }
            else
            {
                AddTextTT("You need to heal before you can explore.");
            }
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
            Close();
        }

        public CatacombsWindow()
        {
            InitializeComponent();
            txtCatacombs.Text =
            "You find the entrance to the catacombs, a long series of underground passages created throughout the last several hundred years. Thousands of people were buried here in crypts. You've heard of a shantytown down here, a place for the less fortunate to sleep at night. There is also supposed to be a large ravine down here to explore. Also, an ancient aqueduct system runs beneath the city, transporting water all over the city.";
        }

        private void windowCatacombs_Closing(object sender, CancelEventArgs e)
        {
            RefToExploreWindow.Show();
            RefToExploreWindow.CheckButtons();
        }

        #endregion Window-Manipulation Methods
    }
}