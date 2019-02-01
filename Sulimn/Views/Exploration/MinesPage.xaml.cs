using Extensions;
using Sulimn.Classes;
using Sulimn.Views.Battle;
using System.Windows;

namespace Sulimn.Views.Exploration
{
    /// <summary>Interaction logic for MinesPage.xaml</summary>
    public partial class MinesPage
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
            Functions.AddTextToTextBox(TxtMines, "You need to heal before you can explore.");
            return false;
        }

        /// <summary>Starts a battle.</summary>
        /// <param name="progress">Will this battle be for progression?</param>
        private void StartBattle(bool progress = false)
        {
            BattlePage battlePage = new BattlePage { RefToMinesPage = this };
            battlePage.PrepareBattle("Mines", progress);
            GameState.Navigate(battlePage);
        }

        #region Button-Click Methods

        private void BtnOffices_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    Functions.AddTextToTextBox(TxtMines, GameState.EventFindGold(200, 600));
                else if (result <= 40)
                    Functions.AddTextToTextBox(TxtMines, GameState.EventFindItem(250, 650));
                else if (result <= 80)
                {
                    GameState.EventEncounterEnemy("Giant Spider", "Lion", "Crazed Miner", "Giant Bat");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Knight", "Adventurer");
                    StartBattle();
                }
            }
        }

        private void BtnOreBin_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    Functions.AddTextToTextBox(TxtMines, GameState.EventFindGold(300, 700));
                else if (result <= 40)
                    Functions.AddTextToTextBox(TxtMines, GameState.EventFindItem(350, 750));
                else if (result <= 80)
                {
                    GameState.EventEncounterEnemy("Giant Spider", "Lion", "Crazed Miner", "Giant Bat");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Knight", "Adventurer");
                    StartBattle();
                }
            }
        }

        private void BtnPumpStation_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 10)
                    Functions.AddTextToTextBox(TxtMines, GameState.EventFindGold(200, 600));
                else if (result <= 30)
                    Functions.AddTextToTextBox(TxtMines, GameState.EventFindItem(250, 650));
                else if (result <= 75)
                {
                    GameState.EventEncounterEnemy("Giant Spider", "Lion", "Crazed Miner", "Giant Bat");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Adventurer", "Gladiator", "Crazed Miner");
                    StartBattle();
                }
            }
        }

        private void BtnWorkshop_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 10)
                    Functions.AddTextToTextBox(TxtMines, GameState.EventFindGold(300, 700));
                else if (result <= 30)
                    Functions.AddTextToTextBox(TxtMines, GameState.EventFindItem(350, 750));
                else if (result <= 80)
                {
                    GameState.EventEncounterEnemy("Giant Spider", "Lion", "Crazed Miner", "Giant Bat");
                    StartBattle();
                }
                else
                {
                    GameState.EventEncounterEnemy("Knight", "Adventurer", "Monk", "Gladiator");
                    StartBattle();
                }
            }
        }

        private void BtnProgress_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                GameState.DisplayNotification(
                    "These mines extend forever. You've been in here for hours in the relative dark, with only your dimming lantern to light your way, searching for whoever was working with John. You've already searched the offices, ore bin, pumping station, and the workshop.\n\n" +
                    "As you're lumbering through another seemingly endless passageway, you see a flicker of light coming from the wall as you walk past. You stop, and examine the light. It was a reflection from your lantern. It's a tiny latch! You pull the latch, and a hidden door opens!\n\n" +
                    "You enter the room that the was revealed by the hidden door, and everything is covered in a fine coat of dust. Whoever was here before hadn't been back in a long time. The room wasn't very large, but it was packed full of all sorts of things: small bits of iron, some empty barrels, some rotting wooden planks, and a small chest in the corner, slightly hidden by some planks. As you go to turn the latch, you hear something behind you.", "Sulimn");
                GameState.DisplayNotification(
                    "As you turn, you see a tough-looking man in dark priest attire. Whoever was behind these searches was obviously well-connected. Dark priests were among the most-feared people for their abilties. Few who ever faced a dark priest lived to tell the tale.\n\n" +
                    "\"So, you found it,\" said the man. \"To think, we'd been looking for weeks, and you managed to find it in less than a day! Hand over that map now or I'll be forced to take it from you,\" he continued.\n\n" +
                    "Not willing to part with what you hadn't even laid your hands on yet, you draw your sword.", "Sulimn");
                GameState.EventEncounterEnemy("Tennyson the Dark Priest");
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

        public MinesPage()
        {
            InitializeComponent();
            TxtMines.Text =
            "You enter the abandoned mines. The path splits very near to the entrance, with paths leading south, east, and west. There are offices nearby. A crumbling, barely legible sign shows that the south path leads to a shaft that goes to the ore bin, the east path leads to the pump station, and the west path leads to the workshop.";
        }

        private void MinesPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            
            if (Progress)
            {
                Progress = false;
                GameState.DisplayNotification(
                    "You have defeated Tennyson the Dark Priest. You return to the chest and open it. Inside is another piece of the map. Next to it is a very full bag of coins, totalling some 5,000 gold!\n\n" +
                    "You return to the body of the dark priest and examine him. Some have said that dark priests cast spells on themselves before death so that their corpses can be effectively \"booby-trapped\", but you see no evidence of that with this one. You find a small diary with the name \"Tennyson\" emblazoned on it. His blood stained most of the pages, but one page is legible.\n\n" +
                    $"\"The proprietor wishes me to follow this new adventurer, {GameState.CurrentHero.Name}, and see if he can find the last piece of the map. I will take the other pieces from him and bring them back to the proprietor for my reward.\"\n\n" +
                    "Whoever this \"proprietor\" is, he's sure getting on your nerves.", "Sulimn");
            }
            BtnProgress.IsEnabled = GameState.CurrentHero.Level >= 20 && !GameState.CurrentHero.Progression.Mines;
        }

        #endregion Page-Manipulation Methods
    }
}