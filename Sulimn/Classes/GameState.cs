using Extensions;
using Extensions.Encryption;
using Extensions.Enums;
using Sulimn.Classes.Database;
using Sulimn.Classes.Entities;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using Sulimn.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn.Classes
{
    /// <summary>Represents the current state of the game.</summary>
    internal static class GameState
    {
        internal static CultureInfo CurrentCulture = new CultureInfo("en-US");
        internal static Settings CurrentSettings;
        internal static Hero CurrentHero = new Hero();
        internal static Enemy CurrentEnemy = new Enemy();
        internal static List<Enemy> AllEnemies = new List<Enemy>();
        internal static List<Item> AllItems = new List<Item>();
        internal static List<Item> AllHeadArmor = new List<Item>();
        internal static List<Item> AllBodyArmor = new List<Item>();
        internal static List<Item> AllHandArmor = new List<Item>();
        internal static List<Item> AllLegArmor = new List<Item>();
        internal static List<Item> AllFeetArmor = new List<Item>();
        internal static List<Item> AllRings = new List<Item>();
        internal static List<Item> AllWeapons = new List<Item>();
        internal static List<Item> AllFood = new List<Item>();
        internal static List<Item> AllDrinks = new List<Item>();
        internal static List<Item> AllPotions = new List<Item>();
        internal static List<Spell> AllSpells = new List<Spell>();
        internal static List<Hero> AllHeroes = new List<Hero>();
        internal static List<HeroClass> AllClasses = new List<HeroClass>();
        internal static Item DefaultWeapon = new Item();
        internal static Item DefaultHead = new Item();
        internal static Item DefaultBody = new Item();
        internal static Item DefaultHands = new Item();
        internal static Item DefaultLegs = new Item();
        internal static Item DefaultFeet = new Item();

        #region Navigation

        /// <summary>Instance of MainWindow currently loaded</summary>
        internal static MainWindow MainWindow { get; set; }

        /// <summary>Navigates to selected Page.</summary>
        /// <param name="newPage">Page to navigate to.</param>
        internal static void Navigate(Page newPage) => MainWindow.MainFrame.Navigate(newPage);

        /// <summary>Navigates to the previous Page.</summary>
        internal static void GoBack()
        {
            if (MainWindow.MainFrame.CanGoBack)
                MainWindow.MainFrame.GoBack();
        }

        /// <summary>Handles a Hardcore <see cref="Hero"/>'s death.</summary>
        internal static void HardcoreDeath()
        {
            while (MainWindow.MainFrame.CanGoBack)
                MainWindow.MainFrame.GoBack();
            if (CurrentHero != new Hero())
            {
                DeleteHero(CurrentHero);
                CurrentHero = new Hero();
            }
        }

        #endregion Navigation

        /// <summary>Determines whether a Hero's credentials are authentic.</summary>
        /// <param name="username">Hero's name</param>
        /// <param name="password">Hero's password</param>
        /// <returns>Returns true if valid login</returns>
        internal static bool CheckLogin(string username, string password)
        {
            Hero checkHero = AllHeroes.Find(hero => string.Equals(hero.Name, username, StringComparison.InvariantCultureIgnoreCase));
            if (checkHero != null && checkHero != new Hero())
            {
                if (Argon2.ValidatePassword(checkHero.Password, password))
                {
                    CurrentHero = checkHero;
                    return true;
                }
            }

            DisplayNotification("Invalid login.", "Sulimn");
            return false;
        }

        /// <summary>Changes the current theme in the database.</summary>
        /// <param name="theme">Current theme</param>
        /// <returns>True if successful</returns>
        internal static void ChangeTheme(string theme)
        {
            CurrentSettings.Theme = theme;
            ChangeSettings();
        }

        /// <summary>Writes the current <see cref="Settings"/> to file.</summary>
        internal static void ChangeSettings() => JSONInteraction.WriteSettings(CurrentSettings);

        internal static void FileManagement()
        {
            if (!Directory.Exists(AppData.Location))
                Directory.CreateDirectory(AppData.Location);

            string DataFolderLocation = Path.Combine(AppData.Location, "Data");
            if (!Directory.Exists(DataFolderLocation) || Directory.GetFiles(DataFolderLocation).Length == 0)
            {
                string zipLocation = Path.Combine(AppData.Location, "Data.zip");
                File.WriteAllBytes(zipLocation, Properties.Resources.Data);
                using (ZipArchive archive = new ZipArchive(File.Open(zipLocation, FileMode.Open)))
                    archive.ExtractToDirectory(AppData.Location, true);
                File.Delete(zipLocation);
            }
        }

        /// <summary>Loads almost everything from the database.</summary>
        internal static void LoadAll()
        {
            FileManagement();
            CurrentSettings = JSONInteraction.LoadSettings();
            AllClasses = JSONInteraction.LoadClasses();
            AllHeadArmor = JSONInteraction.LoadArmor<Item>("head").OrderBy(o => o.Value).ToList();
            AllBodyArmor = JSONInteraction.LoadArmor<Item>("body").OrderBy(o => o.Value).ToList();
            AllHandArmor = JSONInteraction.LoadArmor<Item>("hand").OrderBy(o => o.Value).ToList();
            AllLegArmor = JSONInteraction.LoadArmor<Item>("leg").OrderBy(o => o.Value).ToList();
            AllFeetArmor = JSONInteraction.LoadArmor<Item>("feet").OrderBy(o => o.Value).ToList();
            AllRings = JSONInteraction.LoadRings();
            AllDrinks = JSONInteraction.LoadDrinks();
            AllFood = JSONInteraction.LoadFood();
            AllPotions = JSONInteraction.LoadPotions();
            AllSpells = JSONInteraction.LoadSpells();
            AllWeapons = JSONInteraction.LoadWeapons();
            AllItems.AddRanges(AllHeadArmor, AllBodyArmor, AllHandArmor, AllLegArmor, AllFeetArmor, AllRings, AllFood, AllDrinks, AllPotions, AllWeapons);
            AllEnemies = JSONInteraction.LoadEnemies();
            AllHeroes = JSONInteraction.LoadHeroes();
            //MaximumStatsHero = await DatabaseInteraction.LoadMaxHeroStats();

            AllItems = AllItems.OrderBy(item => item.Name).ToList();
            AllEnemies = AllEnemies.OrderBy(enemy => enemy.Name).ToList();
            AllSpells = AllSpells.OrderBy(spell => spell.Name).ToList();
            AllClasses = AllClasses.OrderBy(heroClass => heroClass.Name).ToList();
            AllRings = AllRings.OrderBy(ring => ring.Name).ToList();

            DefaultWeapon = AllWeapons.Find(weapon => weapon.Name == "Fists");
            DefaultHead = AllHeadArmor.Find(armor => armor.Name == "Cloth Helmet");
            DefaultBody = AllBodyArmor.Find(armor => armor.Name == "Cloth Shirt");
            DefaultHands = AllHandArmor.Find(armor => armor.Name == "Cloth Gloves");
            DefaultLegs = AllLegArmor.Find(armor => armor.Name == "Cloth Pants");
            DefaultFeet = AllFeetArmor.Find(armor => armor.Name == "Cloth Shoes");
        }

        /// <summary>Gets a specific Enemy based on its name.</summary>
        /// <param name="name">Name of Enemy</param>
        /// <returns>Enemy</returns>
        private static Enemy GetEnemy(string name) => new Enemy(AllEnemies.Find(enemy => enemy.Name == name));

        #region Item Management

        /// <summary>Gets a specific Item based on its name.</summary>
        /// <param name="name">Item name</param>
        /// <returns>Item</returns>
        private static Item GetItem(string name) => AllItems.Find(itm => itm.Name == name);

        /// <summary>Retrieves a List of all Items of specified Type.</summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>List of specified Type.</returns>
        public static List<T> GetItemsOfType<T>() => AllItems.OfType<T>().ToList();

        /// <summary>Gets all Items of specified Type.</summary>
        /// <param name="type">Type</param>
        /// <returns>Items of specified Type</returns>
        internal static List<Item> GetItemsOfType(ItemType type) => AllItems.Where(itm => itm.Type == type).ToList();

        /// <summary>Sets allowed <see cref="HeroClass"/>es based on a string.</summary>
        /// <param name="classes">AllowedClasses to be converted</param>
        /// <returns>List of allowed <see cref="HeroClass"/>es</returns>
        internal static List<HeroClass> SetAllowedClasses(string classes)
        {
            List<HeroClass> newAllowedClasses = new List<HeroClass>();

            if (classes.Length > 0)
            {
                string[] arrAllowedClasses = classes.Split(',');
                newAllowedClasses.AddRange(arrAllowedClasses.Select(str => AllClasses.Find(x => x.Name == str.Trim())));
            }
            return newAllowedClasses;
        }

        /// <summary>Sets the Hero's inventory.</summary>
        /// <param name="inventory">Inventory to be converted</param>
        /// <returns>Inventory List</returns>
        internal static List<Item> SetInventory(string inventory)
        {
            List<Item> newInventory = new List<Item>();

            if (inventory.Length > 0)
            {
                string[] arrInventory = inventory.Split(',');
                newInventory.AddRange(arrInventory.Select(str => AllItems.Find(x => x.Name == str.Trim())));
            }
            return newInventory;
        }

        /// <summary>Sets the list of the Hero's known spells.</summary>
        /// <param name="spells">String list of spells</param>
        /// <returns>List of known Spells</returns>
        internal static Spellbook SetSpellbook(string spells)
        {
            List<Spell> spellList = new List<Spell>();

            if (spells.Length > 0)
            {
                string[] arrSpell = spells.Split(',');

                foreach (string str in arrSpell)
                    spellList.Add(AllSpells.Find(x => x.Name == str.Trim()));
            }
            return new Spellbook(spellList);
        }

        #endregion Item Management

        #region Hero Management

        /// <summary>Modifies a Hero's details in the database.</summary>
        /// <param name="oldHero">Hero whose details need to be modified</param>
        /// <param name="newHero">Hero with new details</param>
        /// <returns>True if successful</returns>
        internal static void ChangeHeroDetails(Hero oldHero, Hero newHero)
        {
            DeleteHero(oldHero);
            SaveHero(newHero);
        }

        /// <summary>Deletes a Hero from the game and database.</summary>
        /// <param name="deleteHero">Hero to be deleted</param>
        /// <returns>Whether deletion was successful</returns>
        internal static void DeleteHero(Hero deleteHero)
        {
            JSONInteraction.DeleteHero(deleteHero);
            AllHeroes.Remove(deleteHero);
        }

        /// <summary>Creates a new Hero and adds it to the database.</summary>
        /// <param name="newHero">New Hero</param>
        internal static void NewHero(Hero newHero)
        {
            if (newHero.Equipment.Head == null || newHero.Equipment.Head == new Item())
                newHero.Equipment.Head = AllHeadArmor.Find(armor => armor.Name == DefaultHead.Name);
            if (newHero.Equipment.Body == null || newHero.Equipment.Body == new Item())
                newHero.Equipment.Body = AllBodyArmor.Find(armor => armor.Name == DefaultBody.Name);
            if (newHero.Equipment.Hands == null || newHero.Equipment.Hands == new Item())
                newHero.Equipment.Hands = AllHandArmor.Find(armor => armor.Name == DefaultHands.Name);
            if (newHero.Equipment.Legs == null || newHero.Equipment.Legs == new Item())
                newHero.Equipment.Legs = AllLegArmor.Find(armor => armor.Name == DefaultLegs.Name);
            if (newHero.Equipment.Feet == null || newHero.Equipment.Feet == new Item())
                newHero.Equipment.Feet = AllFeetArmor.Find(armor => armor.Name == DefaultFeet.Name);
            if (newHero.Equipment.Weapon == null || newHero.Equipment.Weapon == new Item())
            {
                switch (newHero.Class.Name)
                {
                    case "Wizard":
                        newHero.Equipment.Weapon = AllWeapons.Find(wpn => wpn.Name == "Starter Staff");
                        if (newHero.Spellbook == null || newHero.Spellbook == new Spellbook())
                            newHero.Spellbook?.LearnSpell(AllSpells.Find(spell => spell.Name == "Fire Bolt"));
                        break;

                    case "Cleric":
                        newHero.Equipment.Weapon = AllWeapons.Find(wpn => wpn.Name == "Starter Staff");
                        if (newHero.Spellbook == null || newHero.Spellbook == new Spellbook())
                            newHero.Spellbook?.LearnSpell(AllSpells.Find(spell => spell.Name == "Heal Self"));
                        break;

                    case "Warrior":
                        newHero.Equipment.Weapon = AllWeapons.Find(wpn => wpn.Name == "Stone Dagger");
                        break;

                    case "Rogue":
                        newHero.Equipment.Weapon = AllWeapons.Find(wpn => wpn.Name == "Starter Bow");
                        break;

                    default:
                        newHero.Equipment.Weapon = AllWeapons.Find(wpn => wpn.Name == "Stone Dagger");
                        break;
                }
            }

            newHero.Gold = 250;
            for (int i = 0; i < 3; i++)
                newHero.AddItem(AllPotions.Find(itm => itm.Name == "Minor Healing Potion"));

            JSONInteraction.SaveHero(newHero);
            AllHeroes.Add(newHero);
        }

        /// <summary>Saves Hero to database.</summary>
        /// <param name="saveHero">Hero to be saved</param>
        /// <returns>Returns true if successfully saved</returns>
        internal static bool SaveHero(Hero saveHero)
        {
            JSONInteraction.SaveHero(saveHero);

            int index = AllHeroes.FindIndex(hero => hero.Name == saveHero.Name);
            AllHeroes[index] = saveHero;

            return true;
        }

        #endregion Hero Management

        #region Exploration Events

        /// <summary>Event where the Hero finds gold.</summary>
        /// <param name="minGold">Minimum amount of gold to be found</param>
        /// <param name="maxGold">Maximum amount of gold to be found</param>
        /// <returns>Returns text regarding gold found</returns>
        internal static string EventFindGold(int minGold, int maxGold)
        {
            int foundGold = minGold == maxGold ? minGold : Functions.GenerateRandomNumber(minGold, maxGold);
            CurrentHero.Gold += foundGold;
            SaveHero(CurrentHero);
            return $"You find {foundGold:N0} gold!";
        }

        /// <summary>Event where the <see cref="Hero"/> finds an <see cref="Item"/>.</summary>
        /// <param name="minValue">Minimum value of <see cref="Item"/></param>
        /// <param name="maxValue">Maximum value of <see cref="Item"/></param>
        /// <param name="isSold">Is the <see cref="Item"/> sold?</param>
        /// <returns>Returns text about found <see cref="Item"/></returns>
        internal static string EventFindItem(int minValue, int maxValue, bool isSold = true)
        {
            Item item = GetRandomItem(minValue, maxValue, isSold);
            if (CurrentHero.Inventory.Count < 40)
            {
                CurrentHero.AddItem(item);
                SaveHero(CurrentHero);
                return $"You find a {item.Name}!";
            }
            return $"You find a {item.Name},\nbut your inventory is full.";
        }

        /// <summary>Event where the <see cref="Hero"/> finds an <see cref="Item"/>.</summary>
        /// <param name="minValue">Minimum value of <see cref="Item"/></param>
        /// <param name="maxValue">Maximum value of <see cref="Item"/></param>
        /// <param name="type">Type of <see cref="Item"/></param>
        /// <param name="isSold">Is the <see cref="Item"/> sold?</param>
        /// <returns>Returns text about found <see cref="Item"/></returns>
        internal static string EventFindItem(int minValue, int maxValue, ItemType type, bool isSold = true)
        {
            Item item = GetRandomItem(minValue, maxValue, type, isSold);
            if (CurrentHero.Inventory.Count < 40)
            {
                CurrentHero.AddItem(item);
                SaveHero(CurrentHero);
                return $"You find a {item.Name}!";
            }
            return $"You find a {item.Name},\nbut your inventory is full.";
        }

        /// <summary>Gets a random <see cref="Item"/>.</summary>
        /// <param name="minValue">Minimum value of <see cref="Item"/></param>
        /// <param name="maxValue">Maximum value of <see cref="Item"/></param>
        /// <param name="isSold">Is the <see cref="Item"/> sold?</param>
        /// <returns>Random <see cref="Item"/></returns>
        private static Item GetRandomItem(int minValue, int maxValue, bool isSold = true)
        {
            List<Item> availableItems = new List<Item>(AllItems.FindAll(itm => itm.Value >= minValue && itm.Value <= maxValue && itm.IsSold == isSold));
            return availableItems[Functions.GenerateRandomNumber(0, availableItems.Count - 1)];
        }

        /// <summary>Gets a random <see cref="Item"/> of a given <see cref="ItemType"/>.</summary>
        /// <param name="minValue">Minimum value of <see cref="Item"/></param>
        /// <param name="maxValue">Maximum value of It<see cref="Item"/>em</param>
        /// <param name="type">Type of <see cref="Item"/></param>
        /// <param name="isSold">Is the <see cref="Item"/> sold?</param>
        /// <returns>Random <see cref="Item"/></returns>
        private static Item GetRandomItem(int minValue, int maxValue, ItemType type, bool isSold = true)
        {
            List<Item> availableItems = new List<Item>(AllItems.FindAll(itm => itm.Type == type && itm.Value >= minValue && itm.Value <= maxValue && itm.IsSold == isSold));
            return availableItems[Functions.GenerateRandomNumber(0, availableItems.Count - 1)];
        }

        /// <summary>Event where the <see cref="Hero"/> finds an <see cref="Item"/>.</summary>
        /// <param name="names">List of names of available <see cref="Item"/>s</param>
        /// <returns>Returns text about found <see cref="Item"/></returns>
        internal static string EventFindItem(params string[] names)
        {
            List<Item> availableItems = new List<Item>();
            foreach (string name in names)
                availableItems.Add(GetItem(name));
            int item = Functions.GenerateRandomNumber(0, availableItems.Count - 1);

            CurrentHero.AddItem(availableItems[item]);

            SaveHero(CurrentHero);
            return $"You find a {availableItems[item].Name}!";
        }

        /// <summary>Event where the Hero encounters a hostile animal.</summary>
        /// <param name="minLevel">Minimum level of animal</param>
        /// <param name="maxLevel">Maximum level of animal</param>
        internal static void EventEncounterAnimal(int minLevel, int maxLevel) => EventEncounterEnemy(AllEnemies.Where(enemy => enemy.Level >= minLevel && enemy.Level <= maxLevel && enemy.Type == "Animal").ToList());

        /// <summary>Event where the Hero encounters a hostile Enemy.</summary>
        /// <param name="minLevel">Minimum level of Enemy.</param>
        /// <param name="maxLevel">Maximum level of Enemy.</param>
        internal static void EventEncounterEnemy(int minLevel, int maxLevel) => EventEncounterEnemy(AllEnemies.Where(enemy => enemy.Level >= minLevel && enemy.Level <= maxLevel && enemy.Type != "Boss").ToList());

        /// <summary>Event where the Hero encounters a hostile Enemy.</summary>
        /// <param name="names">Array of names</param>
        internal static void EventEncounterEnemy(params string[] names) =>
            EventEncounterEnemy(names.Select(GetEnemy).ToList());

        internal static void EventEncounterEnemy(List<Enemy> availableEnemies)
        {
            int enemyNum = Functions.GenerateRandomNumber(0, availableEnemies.Count - 1);
            CurrentEnemy = new Enemy(availableEnemies[enemyNum]);
            if (CurrentEnemy.Gold > 0)
                CurrentEnemy.Gold = Functions.GenerateRandomNumber(CurrentEnemy.Gold / 2, CurrentEnemy.Gold);
            if (CurrentEnemy.Type == "Human" || CurrentEnemy.Type == "Boss")
            {
                if (Functions.GenerateRandomNumber(1, 100) <= 20)
                    CurrentEnemy.AddItem(GetRandomItem(1, CurrentEnemy.Level * 50));
                if (Functions.GenerateRandomNumber(1, 100) <= 10)
                    CurrentEnemy.AddItem(GetRandomItem(1, CurrentEnemy.Level * 100));
                if (CurrentEnemy.Type == "Boss" && Functions.GenerateRandomNumber(1, 100) <= 10)
                    CurrentEnemy.AddItem(GetRandomItem(1, CurrentEnemy.Level * 200));
            }
        }

        /// <summary>Event where the Hero encounters a water stream and restores health and magic.</summary>
        /// <returns>String saying Hero has been healed</returns>
        internal static string EventEncounterStream()
        {
            CurrentHero.Statistics.CurrentHealth = CurrentHero.Statistics.MaximumHealth;
            CurrentHero.Statistics.CurrentMagic = CurrentHero.Statistics.MaximumMagic;

            return
            "You stumble across a stream.\nYou stop to drink some of\nthe water and rest a while.\nYou feel recharged!";
        }

        #endregion Exploration Events

        #region Notification Management

        /// <summary>Displays a new Notification in a thread-safe way.</summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="title">Title of the Notification window</param>
        internal static void DisplayNotification(string message, string title) => Application.Current.Dispatcher.Invoke(() => new Notification(message, title, NotificationButton.OK, MainWindow).ShowDialog());

        /// <summary>Displays a new Notification in a thread-safe way and retrieves a boolean result upon its closing.</summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="title">Title of the Notification window</param>
        /// <returns>Returns value of clicked button on Notification.</returns>
        internal static bool YesNoNotification(string message, string title) => Application.Current.Dispatcher.Invoke(() => new Notification(message, title, NotificationButton.YesNo, MainWindow).ShowDialog() == true);

        #endregion Notification Management
    }
}