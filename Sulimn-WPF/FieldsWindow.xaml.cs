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
    /// Interaction logic for FieldsWindow.xaml
    /// </summary>
    public partial class FieldsWindow : Window
    {
        internal ExploreWindow RefToExploreWindow { get; set; }

        /// <summary>
        /// Adds text to the txtFields TextBox.
        /// </summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            string nl = Environment.NewLine;
            txtFields.Text += nl + nl + newText;
            txtFields.Focus();
            txtFields.CaretIndex = txtFields.Text.Length;
            txtFields.ScrollToEnd();
        }

        /// <summary>
        /// Starts a battle.
        /// </summary>
        private void StartBattle()
        {
            BattleWindow battleWindow = new BattleWindow();
            battleWindow.RefToFieldsWindow = this;
            battleWindow.PrepareBattle("Fields");
            battleWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        #region Button-Click Methods

        private void btnFarm_Click(object sender, RoutedEventArgs e)
        {
            int result = Functions.GenerateRandomNumber(1, 100);
            if (result <= 15)
                AddTextTT(GameState.EventFindGold(1, 100));
            else if (result <= 30)
                AddTextTT(GameState.EventFindItem(1, 200));
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

        private void btnCellar_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                    AddTextTT(GameState.EventFindGold(1, 150));
                else if (result <= 30)
                    AddTextTT(GameState.EventFindItem(1, 250));
                else if (result <= 65)
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
                MessageBox.Show("You need to heal before you can explore.");
        }

        private void btnCropFields_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 5)
                    AddTextTT(GameState.EventFindGold(25, 200));
                else if (result <= 30)
                    AddTextTT(GameState.EventFindItem(1, 300));
                else if (result <= 65)
                {
                    GameState.EventEncounterEnemy("Rabbit", "Snake", "Mangy Dog");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Thief");
                    StartBattle();
                }
            }
            else
                MessageBox.Show("You need to heal before you can explore.");
        }

        private void btnOrchard_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.CurrentHealth > 0)
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                    AddTextTT(GameState.EventFindGold(50, 250));
                else if (result <= 30)
                    AddTextTT(GameState.EventFindItem(1, 350));
                else if (result <= 65)
                {
                    GameState.EventEncounterEnemy("Rabbit", "Snake");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Beggar", "Thief", "Knave");
                    StartBattle();
                }
            }
            else
                MessageBox.Show("You need to heal before you can explore.");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        private void CloseWindow()
        {
            this.Close();
        }

        public FieldsWindow()
        {
            InitializeComponent();
            txtFields.Text = "You enter the farmlands and head toward the crop fields. On the way, you see an abandoned farmhouse that is overgrown with weeds and vines. You stop at a crumbling stone wall that used to be its property line and see an overgrown door to a root cellar. In the distance, you see an orchard.";
        }

        private void windowFields_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RefToExploreWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}