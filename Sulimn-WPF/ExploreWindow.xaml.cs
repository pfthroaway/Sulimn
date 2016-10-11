using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sulimn_WPF
{
    /// <summary>
    /// Interaction logic for ExploreWindow.xaml
    /// </summary>
    public partial class ExploreWindow : Window
    {
        internal CityWindow RefToCityWindow { get; set; }
        private string nl = Environment.NewLine;

        internal void LoadExplore()
        {
            txtExplore.Text = "There are some safe-looking crop fields to the east where many adventurers have gone to prove their worth." + nl + nl + "There is a dark forest to the west, which looks like it has devoured its fair share of adventurers." + nl + nl + "There is a dilapidated cathedral at the north end of the city, spreading fear and hopelessness to everyone in its shadow." + nl + nl + "There is an abandoned mining complex on the south side of the city, which looks like no one has entered, or come out of it for years.";
            CheckButtons();
        }

        /// <summary>
        /// Adds text to the txtExplore TextBox.
        /// </summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtExplore.Text += nl + nl + newText;
            txtExplore.Focus();
            txtExplore.CaretIndex = txtExplore.Text.Length;
            txtExplore.ScrollToEnd();
        }

        #region Button Manipulation

        /// <summary>
        /// Checks the player's level to determine which buttons to allow to be enabled.
        /// </summary>
        internal void CheckButtons()
        {
            btnBack.IsEnabled = true;
            btnFields.IsEnabled = true;
            if (GameState.currentHero.Level >= 5)
                btnForest.IsEnabled = true;
            if (GameState.currentHero.Level >= 10)
                btnCathedral.IsEnabled = true;
            if (GameState.currentHero.Level >= 15)
                btnMines.IsEnabled = true;
            if (GameState.currentHero.Level >= 20)
                btnCatacombs.IsEnabled = true;
        }

        /// <summary>
        /// Disable all the buttons.
        /// </summary>
        private void DisableButtons()
        {
            btnBack.IsEnabled = false;
            btnCathedral.IsEnabled = false;
            btnFields.IsEnabled = false;
            btnForest.IsEnabled = false;
            btnMines.IsEnabled = false;
        }

        #endregion Button Manipulation

        #region Events

        /// <summary>
        /// Event where the Hero finds gold.
        /// </summary>
        private void EventFindGold(int minGold, int maxGold)
        {
            int foundGold = Functions.GenerateRandomNumber(minGold, maxGold);
            GameState.currentHero.Gold += foundGold;
            AddTextTT("You find " + foundGold.ToString("N0") + " gold!");
            GameState.SaveHero();
            CheckButtons();
        }

        /// <summary>
        /// Event where the Hero finds an item.
        /// </summary>
        private void EventFindItem(int minValue, int maxValue, bool canSell = true)
        {
            List<Item> availableItems = new List<Item>();
            availableItems = GameState.AllItems.Where(x => x.Value >= minValue && x.Value <= maxValue && x.CanSell == true).ToList();
            int item = Functions.GenerateRandomNumber(0, availableItems.Count - 1);

            GameState.currentHero.Inventory.AddItem(availableItems[item]);

            AddTextTT("You find a " + availableItems[item].Name + "!");
            GameState.SaveHero();
            CheckButtons();
        }

        /// <summary>
        /// Event where the Hero encounters a hostile animal.
        /// </summary>
        /// <param name="minLevel">Minimum level of animal</param>
        /// <param name="maxLevel">Maximum level of animal</param>
        private void EventEncounterAnimal(int minLevel, int maxLevel)
        {
            List<Enemy> availableEnemies = new List<Enemy>();
            availableEnemies = GameState.AllEnemies.Where(o => o.Level >= minLevel && o.Level <= maxLevel).ToList();
            int enemyNum = Functions.GenerateRandomNumber(0, availableEnemies.Count - 1);
            GameState.currentEnemy = new Enemy(availableEnemies[enemyNum]);
            BattleWindow battleWindow = new BattleWindow();
            battleWindow.RefToExploreWindow = this;
            battleWindow.PrepareBattle("Explore");
            battleWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Event where the Hero encounters a hostile Enemy.
        /// </summary>
        /// <param name="minLevel">Minimum level of Enemy.</param>
        /// <param name="maxLevel">Maximum level of Enemy.</param>
        private void EventEncounterEnemy(int minLevel, int maxLevel)
        {
            List<Enemy> availableEnemies = new List<Enemy>();
            availableEnemies = GameState.AllEnemies.Where(o => o.Level >= minLevel && o.Level <= maxLevel).ToList();
            int enemyNum = Functions.GenerateRandomNumber(0, availableEnemies.Count - 1);
            GameState.currentEnemy = new Enemy(availableEnemies[enemyNum]);
            if (GameState.currentEnemy.Gold > 0)
                GameState.currentEnemy.Gold = Functions.GenerateRandomNumber(GameState.currentEnemy.Gold / 2, GameState.currentEnemy.Gold);
            BattleWindow battleWindow = new BattleWindow();
            battleWindow.RefToExploreWindow = this;
            battleWindow.PrepareBattle("Explore");
            battleWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        #endregion Events

        #region Button-Click Methods

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void btnCatacombs_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.currentHero.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                {
                    EventFindGold(300, 800);
                }
                else if (result <= 30)
                {
                    EventFindItem(300, 800);
                }
                else if (result <= 40)
                {
                    EventEncounterAnimal(10, 25);
                }
                else
                {
                    EventEncounterEnemy(10, 25);
                }
            }
            else
                MessageBox.Show("You need to heal before you can explore.");
        }

        private void btnCathedral_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.currentHero.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                {
                    EventFindGold(150, 400);
                }
                else if (result <= 30)
                {
                    EventFindItem(150, 400);
                }
                else if (result <= 40)
                {
                    EventEncounterAnimal(5, 15);
                }
                else
                {
                    EventEncounterEnemy(5, 15);
                }
            }
            else
                MessageBox.Show("You need to heal before you can explore.");
        }

        private void btnFields_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.currentHero.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                {
                    EventFindGold(1, 125);
                }
                else if (result <= 30)
                {
                    EventFindItem(1, 250);
                }
                else if (result <= 65)
                {
                    EventEncounterAnimal(1, 5);
                }
                else
                {
                    EventEncounterEnemy(1, 5);
                }
            }
            else
                MessageBox.Show("You need to heal before you can explore.");
        }

        private void btnForest_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.currentHero.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                {
                    EventFindGold(100, 250);
                }
                else if (result <= 30)
                {
                    EventFindItem(100, 400);
                }
                else if (result <= 80)
                {
                    EventEncounterAnimal(3, 10);
                }
                else
                {
                    EventEncounterEnemy(3, 10);
                }
            }
            else
                MessageBox.Show("You need to heal before you can explore.");
        }

        private void btnMines_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.currentHero.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                {
                    EventFindGold(200, 600);
                }
                else if (result <= 30)
                {
                    EventFindItem(200, 600);
                }
                else if (result <= 50)
                {
                    EventEncounterAnimal(8, 20);
                }
                else
                {
                    EventEncounterEnemy(8, 20);
                }
            }
            else
                MessageBox.Show("You need to heal before you can explore.");
        }

        #endregion Button-Click Methods

        #region Window-Generated Methods

        private void CloseWindow()
        {
            this.Close();
        }

        public ExploreWindow()
        {
            InitializeComponent();
        }

        private void windowExplore_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RefToCityWindow.Show();
        }

        #endregion Window-Generated Methods
    }
}