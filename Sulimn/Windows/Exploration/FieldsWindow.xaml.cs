using Extensions;
using Sulimn.Classes;
using System.ComponentModel;
using System.Windows;

namespace Sulimn.Windows.Exploration
{
    /// <summary>Interaction logic for FieldsWindow.xaml</summary>
    public partial class FieldsWindow
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
            Battle.BattleWindow battleWindow = new Battle.BattleWindow { RefToFieldsWindow = this };
            battleWindow.PrepareBattle("Fields");
            battleWindow.Show();
            Visibility = Visibility.Hidden;
        }

        #region Button-Click Methods

        private async void BtnFarm_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                    Functions.AddTextToTextBox(TxtFields, await GameState.EventFindGold(1, 100));
                else if (result <= 30)
                    Functions.AddTextToTextBox(TxtFields, await GameState.EventFindItem(1, 200));
                else if (result <= 65)
                {
                    GameState.EventEncounterAnimal(1, 3);
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy(1, 3);
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtFields, "You need to heal before you can explore.");
        }

        private async void BtnCellar_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                    Functions.AddTextToTextBox(TxtFields, await GameState.EventFindGold(1, 150));
                else if (result <= 30)
                    Functions.AddTextToTextBox(TxtFields, await GameState.EventFindItem(1, 250));
                else if (result <= 80)
                {
                    GameState.EventEncounterEnemy("Rabbit", "Snake");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Beggar", "Thief");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtFields, "You need to heal before you can explore.");
        }

        private async void BtnCropFields_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 5)
                    Functions.AddTextToTextBox(TxtFields, await GameState.EventFindGold(25, 200));
                else if (result <= 30)
                    Functions.AddTextToTextBox(TxtFields, await GameState.EventFindItem(1, 300));
                else if (result <= 65)
                {
                    GameState.EventEncounterEnemy("Rabbit", "Snake", "Mangy Dog", "Chicken");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Thief");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtFields, "You need to heal before you can explore.");
        }

        private async void BtnOrchard_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                    Functions.AddTextToTextBox(TxtFields, await GameState.EventFindGold(50, 250));
                else if (result <= 30)
                    Functions.AddTextToTextBox(TxtFields, await GameState.EventFindItem(1, 350));
                else if (result <= 80)
                {
                    GameState.EventEncounterEnemy("Rabbit", "Snake", "Mangy Dog", "Chicken");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Beggar", "Thief", "Knave");
                    StartBattle();
                }
            }
            else
                Functions.AddTextToTextBox(TxtFields, "You need to heal before you can explore.");
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

        public FieldsWindow()
        {
            InitializeComponent();
            TxtFields.Text =
            "You enter the farmlands and head toward the crop fields. On the way, you see an abandoned farmhouse that is overgrown with weeds and vines. You stop at a crumbling stone wall that used to be its property line and see an overgrown door to a root cellar. In the distance, you see an orchard.";
        }

        private void WindowFields_Closing(object sender, CancelEventArgs e)
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