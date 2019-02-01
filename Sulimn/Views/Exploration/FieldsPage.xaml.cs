using Extensions;
using Sulimn.Classes;
using Sulimn.Views.Battle;
using System.Windows;

namespace Sulimn.Views.Exploration
{
    /// <summary>Interaction logic for FieldsPage.xaml</summary>
    public partial class FieldsPage
    {
        /// <summary>Has the Hero just completed a progession battle?</summary>
        internal bool Progress { get; set; }

        internal ExplorePage RefToExplorePage { private get; set; }
        private bool _hardcoreDeath;

        /// <summary>Handles closing the Page when a Hardcore character has died.</summary>
        internal void HardcoreDeath()
        {
            _hardcoreDeath = true;
            ClosePage();
        }

        /// <summary>Does the Hero have more than zero health?</summary>
        /// <returns>Whether the Hero has more than zero health</returns>
        private bool Healthy()
        {
            if (GameState.CurrentHero.Statistics.CurrentHealth > 0) return true;
            Functions.AddTextToTextBox(TxtFields, "You need to heal before you can explore.");
            return false;
        }

        /// <summary>Starts a battle.</summary>
        /// <param name="progress">Will this battle be for progression?</param>
        private void StartBattle(bool progress = false)
        {
            BattlePage battlePage = new BattlePage { RefToFieldsPage = this };
            battlePage.PrepareBattle("Fields", progress);
            GameState.Navigate(battlePage);
        }

        #region Button-Click Methods

        private void BtnFarm_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                    Functions.AddTextToTextBox(TxtFields, GameState.EventFindGold(1, 100));
                else if (result <= 30)
                    Functions.AddTextToTextBox(TxtFields, GameState.EventFindItem(1, 200));
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
        }

        private void BtnCellar_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                    Functions.AddTextToTextBox(TxtFields, GameState.EventFindGold(1, 150));
                else if (result <= 30)
                    Functions.AddTextToTextBox(TxtFields, GameState.EventFindItem(1, 250));
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
        }

        private void BtnCropFields_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 5)
                    Functions.AddTextToTextBox(TxtFields, GameState.EventFindGold(25, 200));
                else if (result <= 30)
                    Functions.AddTextToTextBox(TxtFields, GameState.EventFindItem(1, 300));
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
        }

        private void BtnOrchard_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                    Functions.AddTextToTextBox(TxtFields, GameState.EventFindGold(50, 250));
                else if (result <= 30)
                    Functions.AddTextToTextBox(TxtFields, GameState.EventFindItem(1, 350));
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
        }

        private void BtnProgress_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                GameState.DisplayNotification(
                    "You feel a presence behind you and turn to look. A Knave stands at the ready. He calls out to you,\n\n" +
                    "\"I've been watching your progress fighting these wild animals and weaklings. You show promise. However, you'll never make it to the Forest in time to stop what we're planning. It's a pity that I, Leon, have to kill you now.\"\n\n" +
                    "With that, he draws his sword and rushes at you. You prepare your defense.", "Sulimn");
                GameState.EventEncounterEnemy("Leon the Knave");
                StartBattle(true);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => ClosePage();

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage()
        {
            if (_hardcoreDeath)
                RefToExplorePage.HardcoreDeath();

            GameState.GoBack();
        }

        public FieldsPage()
        {
            InitializeComponent();
            TxtFields.Text = "You enter the farmlands and head toward the crop fields. On the way, you see an abandoned farmhouse that is overgrown with weeds and vines. You stop at a crumbling stone wall that used to be its property line and see an overgrown door to a root cellar. In the distance, you see an orchard.";
        }

        private void FieldsPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            
            if (Progress)
            {
                Progress = false;
                GameState.DisplayNotification("You have defeated Leon the Knave. As you rummage through his belongings, you discover a crudely drawn map which shows a location deep in the Forest outside of town. You store the map in your pouch and remember this location for when you're strong enough to face whoever lurks there.", "Sulimn");
            }
            BtnProgress.IsEnabled = GameState.CurrentHero.Level >= 5 && !GameState.CurrentHero.Progression.Fields;
        }

        #endregion Page-Manipulation Methods
    }
}