using System;
using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for CathedralWindow.xaml
    /// </summary>
    public partial class CathedralWindow : Window
    {
        internal ExploreWindow RefToExploreWindow { get; set; }

        /// <summary>
        /// Adds text to the txtCathedral TextBox.
        /// </summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            string nl = Environment.NewLine;
            txtCathedral.Text += nl + nl + newText;
            txtCathedral.Focus();
            txtCathedral.CaretIndex = txtCathedral.Text.Length;
            txtCathedral.ScrollToEnd();
        }

        /// <summary>Starts a battle.</summary>
        private void StartBattle()
        {
            BattleWindow battleWindow = new BattleWindow();
            battleWindow.RefToCathedralWindow = this;
            battleWindow.PrepareBattle("Cathedral");
            battleWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        #region Button-Click Methods

        private async void btnBasilica_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    AddTextTT(await GameState.EventFindGold(150, 400));
                else if (result <= 40)
                    AddTextTT(await GameState.EventFindItem(150, 400));
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
                AddTextTT("You need to heal before you can explore.");
        }

        private async void btnSanctuary_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 10)
                    AddTextTT(await GameState.EventFindGold(150, 450));
                else if (result <= 20)
                    AddTextTT(await GameState.EventFindItem(150, 450));
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
                AddTextTT("You need to heal before you can explore.");
        }

        private async void btnEpiscopium_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    AddTextTT(await GameState.EventFindGold(200, 500));
                else if (result <= 40)
                    AddTextTT(await GameState.EventFindItem(200, 500));
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
                AddTextTT("You need to heal before you can explore.");
        }

        private async void btnTower_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    AddTextTT(await GameState.EventFindGold(200, 600));
                else if (result <= 40)
                    AddTextTT(await GameState.EventFindItem(200, 600));
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

        public CathedralWindow()
        {
            InitializeComponent();
            txtCathedral.Text = "You approach the abandoned cathedral which casts dread and despair over the city. It has multiple places you can explore, including the Windower bishop's basilica, the public sanctuary, the Windower clergymen's espiscopium, and the looming tower.";
        }

        private void windowCathedral_Closing(object sender, CancelEventArgs e)
        {
            RefToExploreWindow.Show();
            RefToExploreWindow.CheckButtons();
        }

        #endregion Window-Manipulation Methods
    }
}