using Extensions;
using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>Interaction logic for CathedralWindow.xaml</summary>
    public partial class CathedralWindow
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
            BattleWindow battleWindow = new BattleWindow { RefToCathedralWindow = this };
            battleWindow.PrepareBattle("Cathedral");
            battleWindow.Show();
            Visibility = Visibility.Hidden;
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
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public CathedralWindow()
        {
            InitializeComponent();
            TxtCathedral.Text =
            "You approach the abandoned cathedral which casts dread and despair over the city. It has multiple places you can explore, including the Windower bishop's basilica, the public sanctuary, the Windower clergymen's espiscopium, and the looming tower.";
        }

        private void WindowCathedral_Closing(object sender, CancelEventArgs e)
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