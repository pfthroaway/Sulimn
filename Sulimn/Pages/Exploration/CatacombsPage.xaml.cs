using Extensions;
using Sulimn.Classes;
using System.ComponentModel;
using System.Windows;

namespace Sulimn.Pages.Exploration
{
    /// <summary>Interaction logic for CatacombsPage.xaml</summary>
    public partial class CatacombsPage
    {
        internal ExplorePage RefToExplorePage { private get; set; }

        private bool _hardcoreDeath = false;

        /// <summary>Starts a battle.</summary>
        private void StartBattle()
        {
            Battle.BattlePage battlePage = new Battle.BattlePage { RefToCatacombsPage = this, };
            battlePage.PrepareBattle("Catacombs");
            GameState.Navigate(battlePage);
        }

        /// <summary>Handles closing the Page when a Hardcore character has died.</summary>
        internal void HardcoreDeath()
        {
            _hardcoreDeath = true;
            ClosePage();
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

            GameState.GoBack();
        }

        public CatacombsPage()
        {
            InitializeComponent();
            TxtCatacombs.Text =
            "You find the entrance to the catacombs, a long series of underground passages created throughout the last several hundred years. Thousands of people were buried here in crypts. You've heard of a shantytown down here, a place for the less fortunate to sleep at night. There is also supposed to be a large ravine down here to explore. Also, an ancient aqueduct system runs beneath the city, transporting water all over the city.";
        }

        private void PageCatacombs_Closing(object sender, CancelEventArgs e)
        {
        }

        #endregion Page-Manipulation Methods

        private void CatacombsPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameState.CalculateScale(Grid);
        }
    }
}