using Extensions;
using Sulimn.Classes;
using Sulimn.Views.Battle;
using System.Windows;

namespace Sulimn.Views.Exploration
{
    /// <summary>Interaction logic for CathedralPage.xaml</summary>
    public partial class CathedralPage
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
            Functions.AddTextToTextBox(TxtCathedral, "You need to heal before you can explore.");
            return false;
        }

        /// <summary>Starts a battle.</summary>
        /// <param name="progress">Will this battle be for progression?</param>
        private void StartBattle(bool progress = false)
        {
            BattlePage battlePage = new BattlePage { RefToCathedralPage = this };
            battlePage.PrepareBattle("Cathedral", progress);
            GameState.Navigate(battlePage);
        }

        #region Button-Click Methods

        private void BtnBasilica_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    Functions.AddTextToTextBox(TxtCathedral, GameState.EventFindGold(150, 400));
                else if (result <= 40)
                    Functions.AddTextToTextBox(TxtCathedral, GameState.EventFindItem(150, 400));
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
        }

        private void BtnSanctuary_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 10)
                    Functions.AddTextToTextBox(TxtCathedral, GameState.EventFindGold(150, 450));
                else if (result <= 20)
                    Functions.AddTextToTextBox(TxtCathedral, GameState.EventFindItem(150, 450));
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
        }

        private void BtnEpiscopium_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    Functions.AddTextToTextBox(TxtCathedral, GameState.EventFindGold(200, 500));
                else if (result <= 40)
                    Functions.AddTextToTextBox(TxtCathedral, GameState.EventFindItem(200, 500));
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
        }

        private void BtnTower_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                int result = Functions.GenerateRandomNumber(1, 100);
                if (result <= 20)
                    Functions.AddTextToTextBox(TxtCathedral, GameState.EventFindGold(200, 600));
                else if (result <= 40)
                    Functions.AddTextToTextBox(TxtCathedral, GameState.EventFindItem(200, 600));
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
        }

        private void BtnProgress_Click(object sender, RoutedEventArgs e)
        {
            if (Healthy())
            {
                GameState.DisplayNotification(
                      "You have searched all but one room in this cathedral, and it's time to enter the last room. You open the door, and what appears to be a priest is standing there, though his demeanor nor eyes give off any shred of religion. He glares at you intently, then speaks.\n\n" +
                      "\"So, you've found me. I knew when Theon didn't come to see me, I would eventually be sought out by whoever killed him. I've got many of my brethren searching for the last piece of the map in the mines, and you won't be able to stop me. I won't be defeated as easily as Theon!\"\n\n" +
                      "With that, John unsheathes his sword and prepares for battle.", "Sulimn");
                GameState.EventEncounterEnemy("John the Priest");
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

        public CathedralPage()
        {
            InitializeComponent();
            TxtCathedral.Text =
            "You approach the abandoned cathedral which casts dread and despair over the city. It has multiple places you can explore, including the former bishop's basilica, the public sanctuary, the former clergymen's espiscopium, and the looming tower.";
        }

        private void CathedralPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            
            if (Progress)
            {
                Progress = false;
                GameState.DisplayNotification("You have defeated John the Priest. John clearly didn't expect to be defeated by the likes of you, as the portion of the map in his possession wasn't hidden, despite him knowing you were coming. The map is definitely part of the catacombs. Something must be hidden there that the people John, Theon, and Leon are searching for. As you rummage through his pockets, you find an enchanted ring. Hopefully that'll come in handy. It's off to the mines for you to search for the last part of the map.", "Sulimn");
            }
            BtnProgress.IsEnabled = GameState.CurrentHero.Level >= 15 && !GameState.CurrentHero.Progression.Cathedral;
        }

        #endregion Page-Manipulation Methods
    }
}