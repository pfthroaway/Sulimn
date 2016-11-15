using System;
using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for ForestWindow.xaml
    /// </summary>
    public partial class ForestWindow : Window
    {
        internal ExploreWindow RefToExploreWindow { get; set; }

        /// <summary>
        /// Adds text to the txtForest TextBox.
        /// </summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            string nl = Environment.NewLine;
            txtForest.Text += nl + nl + newText;
            txtForest.Focus();
            txtForest.CaretIndex = txtForest.Text.Length;
            txtForest.ScrollToEnd();
        }

        /// <summary>
        /// Starts a battle.
        /// </summary>
        private void StartBattle()
        {
            BattleWindow battleWindow = new BattleWindow();
            battleWindow.RefToForestWindow = this;
            battleWindow.PrepareBattle("Forest");
            battleWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        #region Button-Click Methods

        private void btnClearing_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                    AddTextTT(GameState.EventFindGold(50, 300));
                else if (result <= 50)
                    AddTextTT(GameState.EventFindItem(100, 350));
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
                AddTextTT("You need to heal before you can explore.");
        }

        private void btnCottage_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    AddTextTT(GameState.EventFindGold(25, 200));
                else if (result <= 60)
                    AddTextTT(GameState.EventFindItem(50, 250));
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
                AddTextTT("You need to heal before you can explore.");
        }

        private void btnCave_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 10)
                    AddTextTT(GameState.EventFindGold(50, 300));
                else if (result <= 30)
                    AddTextTT(GameState.EventFindItem(100, 350));
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
                AddTextTT("You need to heal before you can explore.");
        }

        private void btnInvestigate_Click(object sender, RoutedEventArgs e)
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
                AddTextTT("You need to heal before you can explore.");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        /// <summary>
        /// Special encounter.
        /// </summary>
        private void SpecialEncounter()
        {
            AddText(GameState.EventFindGold(200, 1000));
        }

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
        }

        public ForestWindow()
        {
            InitializeComponent();
            txtForest.Text = "You travel west along a beaten path into the dark forest. After a short while, you come to a \"T\" fork in the path. You can see the faint silhouette of a cottage to your left, and the sun pouring into a clearing to your right. Ahead of you, through the trees, you see a small cave entrance. Suddenly, you hear the distinct sound of a stick snapping close behind you.";
        }

        private void windowForest_Closing(object sender, CancelEventArgs e)
        {
            RefToExploreWindow.Show();
            RefToExploreWindow.CheckButtons();
        }

        #endregion Window-Manipulation Methods
    }
}