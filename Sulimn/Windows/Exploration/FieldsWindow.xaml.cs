﻿using System;
using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>Interaction logic for FieldsWindow.xaml</summary>
    public partial class FieldsWindow
    {
        internal ExploreWindow RefToExploreWindow { private get; set; }

        /// <summary>Adds text to the txtFields TextBox.</summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            string nl = Environment.NewLine;
            txtFields.Text += nl + nl + newText;
            txtFields.Focus();
            txtFields.CaretIndex = txtFields.Text.Length;
            txtFields.ScrollToEnd();
        }

        /// <summary>Starts a battle.</summary>
        private void StartBattle()
        {
            BattleWindow battleWindow = new BattleWindow { RefToFieldsWindow = this };
            battleWindow.PrepareBattle("Fields");
            battleWindow.Show();
            Visibility = Visibility.Hidden;
        }

        #region Button-Click Methods

        private async void btnFarm_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                    AddTextTT(await GameState.EventFindGold(1, 100));
                else if (result <= 30)
                    AddTextTT(await GameState.EventFindItem(1, 200));
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
                AddTextTT("You need to heal before you can explore.");
        }

        private async void btnCellar_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                    AddTextTT(await GameState.EventFindGold(1, 150));
                else if (result <= 30)
                    AddTextTT(await GameState.EventFindItem(1, 250));
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
                AddTextTT("You need to heal before you can explore.");
        }

        private async void btnCropFields_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 5)
                    AddTextTT(await GameState.EventFindGold(25, 200));
                else if (result <= 30)
                    AddTextTT(await GameState.EventFindItem(1, 300));
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
                AddTextTT("You need to heal before you can explore.");
        }

        private async void btnOrchard_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                    AddTextTT(await GameState.EventFindGold(50, 250));
                else if (result <= 30)
                    AddTextTT(await GameState.EventFindItem(1, 350));
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
                AddTextTT("You need to heal before you can explore.");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
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
            txtFields.Text =
            "You enter the farmlands and head toward the crop fields. On the way, you see an abandoned farmhouse that is overgrown with weeds and vines. You stop at a crumbling stone wall that used to be its property line and see an overgrown door to a root cellar. In the distance, you see an orchard.";
        }

        private void windowFields_Closing(object sender, CancelEventArgs e)
        {
            RefToExploreWindow.Show();
            RefToExploreWindow.CheckButtons();
        }

        #endregion Window-Manipulation Methods
    }
}