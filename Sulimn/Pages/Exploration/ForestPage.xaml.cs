using Extensions;
using Sulimn.Classes;
using Sulimn.Pages.Battle;
using System.Windows;

namespace Sulimn.Pages.Exploration
{
    /// <summary>Interaction logic for ForestPage.xaml</summary>
    public partial class ForestPage
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
            Functions.AddTextToTextBox(TxtForest, "You need to heal before you can explore.");
            return false;
        }

        /// <summary>Starts a battle.</summary>
        /// <param name="progress">Will this battle be for progression?</param>
        private void StartBattle(bool progress = false)
        {
            BattlePage battlePage = new BattlePage { RefToForestPage = this };
            battlePage.PrepareBattle("Forest", progress);
            GameState.Navigate(battlePage);
        }

        /// <summary>Special encounter.</summary>
        private async void SpecialEncounter() => Functions.AddTextToTextBox(TxtForest, await GameState.EventFindGold(200, 1000));

        #region Button-Click Methods

        private async void BtnClearing_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 15)
                    Functions.AddTextToTextBox(TxtForest, await GameState.EventFindGold(50, 300));
                else if (result <= 50)
                    Functions.AddTextToTextBox(TxtForest, await GameState.EventFindItem(100, 350));
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
        }

        private async void BtnCottage_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    Functions.AddTextToTextBox(TxtForest, await GameState.EventFindGold(25, 200));
                else if (result <= 60)
                    Functions.AddTextToTextBox(TxtForest, await GameState.EventFindItem(50, 250));
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
        }

        private async void BtnCave_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 10)
                    Functions.AddTextToTextBox(TxtForest, await GameState.EventFindGold(50, 300));
                else if (result <= 30)
                    Functions.AddTextToTextBox(TxtForest, await GameState.EventFindItem(100, 350));
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
        }

        private void BtnInvestigate_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
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
        }

        private void BtnProgress_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                GameState.DisplayNotification(
                    "You head deep into the woods toward the location you discovered on Leon's map. Sometime after dark, you stumble upon a hut illuminating the surrounding area from a lantern inside. You stand on some firewood and peek through the window.\n\n" +
                    "Standing next to a table is a man dressed in a squire's gear. He's examining something on the table. The firewood you're standing on slips and make a clatter as it falls to the ground, you alongside it.\n\n" +
                    "Hearing the commotion, the squire mutters, \"What was that?\" and comes outside. Seeing you getting back onto your feet, he draws his sword and yells, \"Who are you?\" but doesn't wait for you to answer before moving toward you hastily.",
                    "Sulimn");

                GameState.EventEncounterEnemy("Theon the Squire");
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

        public ForestPage()
        {
            InitializeComponent();
            TxtForest.Text =
            "You travel west along a beaten path into the dark forest. After a short while, you come to a \"T\" fork in the path. You can see the faint silhouette of a cottage to your left, and the sun pouring into a clearing to your right. Ahead of you, through the trees, you see a small cave entrance. Suddenly, you hear the distinct sound of a stick snapping close behind you.";
        }

        private void ForestPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameState.CalculateScale(Grid);
            if (Progress)
            {
                Progress = false;
                GameState.DisplayNotification(
                    "You have defeated Theon the Squire. You enter the hut and look at the table. It's another map! This one is incomplete, but looks like it might be part of a map of the catacombs beneath the city. You raise the map to put it in your inventory, and notice another piece of paper underneath it. It's a note that reads,\n\n" +
                    "\"Theon,\n\nCome see me at the cathedral when you have obtained another piece of the map. Our proprietor is getting impatient.\n\n- John\"\n\n" +
                    "You tuck the note and map away and search for anything of value. You step on a creaky floorboard and realize it's loose. You pull it away and find a pouch of 1,000 gold coins inside!", "Sulimn");
            }
            BtnProgress.IsEnabled = GameState.CurrentHero.Level >= 10 && !GameState.CurrentHero.Progression.Forest;
        }

        #endregion Page-Manipulation Methods
    }
}