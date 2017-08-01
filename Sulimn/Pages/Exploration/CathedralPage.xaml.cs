using Extensions;
using Sulimn.Classes;
using Sulimn.Pages.Battle;
using System.Windows;

namespace Sulimn.Pages.Exploration
{
    /// <summary>Interaction logic for CathedralPage.xaml</summary>
    public partial class CathedralPage
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
            BattlePage battlePage = new BattlePage { RefToCathedralPage = this };
            battlePage.PrepareBattle("Cathedral");
            GameState.Navigate(battlePage);
        }

        #region Button-Click Methods

        private async void BtnBasilica_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    Functions.AddTextToTextBox(TxtCathedral, await GameState.EventFindGold(150, 400));
                else if (result <= 40)
                    Functions.AddTextToTextBox(TxtCathedral, await GameState.EventFindItem(150, 400));
                else if (result <= 90)
                {
                    GameState.EventEncounterEnemy("Priest", "Squire", "Monk", "Giant Spider");
                    StartBattle();
                }
                else if (result <= 98)
                {
                    GameState.EventEncounterEnemy("Knight");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Dark Priest");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtCathedral, "You need to heal before you can explore.");
        }

        private async void BtnSanctuary_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 10)
                    Functions.AddTextToTextBox(TxtCathedral, await GameState.EventFindGold(150, 450));
                else if (result <= 20)
                    Functions.AddTextToTextBox(TxtCathedral, await GameState.EventFindItem(150, 450));
                else if (result <= 90)
                {
                    GameState.EventEncounterEnemy("Priest", "Squire", "Monk", "Giant Spider");
                    StartBattle();
                }
                else if (result <= 98)
                {
                    GameState.EventEncounterEnemy("Knight");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Dark Priest");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtCathedral, "You need to heal before you can explore.");
        }

        private async void BtnEpiscopium_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    Functions.AddTextToTextBox(TxtCathedral, await GameState.EventFindGold(200, 500));
                else if (result <= 40)
                    Functions.AddTextToTextBox(TxtCathedral, await GameState.EventFindItem(200, 500));
                else if (result <= 90)
                {
                    GameState.EventEncounterEnemy("Priest", "Squire", "Monk", "Giant Spider");
                    StartBattle();
                }
                else if (result <= 98)
                {
                    GameState.EventEncounterEnemy("Knight");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Dark Priest");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtCathedral, "You need to heal before you can explore.");
        }

        private async void BtnTower_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    Functions.AddTextToTextBox(TxtCathedral, await GameState.EventFindGold(200, 600));
                else if (result <= 40)
                    Functions.AddTextToTextBox(TxtCathedral, await GameState.EventFindItem(200, 600));
                else if (result <= 90)
                {
                    GameState.EventEncounterEnemy("Priest", "Squire", "Monk", "Giant Spider");
                    StartBattle();
                }
                else if (result <= 98)
                {
                    GameState.EventEncounterEnemy("Knight", "Gladiator");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Dark Priest", "Minotaur");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtCathedral, "You need to heal before you can explore.");
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

        public CathedralPage()
        {
            InitializeComponent();
            TxtCathedral.Text =
            "You approach the abandoned cathedral which casts dread and despair over the city. It has multiple places you can explore, including the former bishop's basilica, the public sanctuary, the former clergymen's espiscopium, and the looming tower.";
        }

        #endregion Page-Manipulation Methods

        private void CathedralPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameState.CalculateScale(Grid);
        }
    }
}