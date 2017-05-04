using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sulimn
{
    /// <summary>Represents the current state of the game.</summary>
    internal static class GameState
    {
        internal static string AdminPassword = "";
        internal static Hero CurrentHero = new Hero();
        internal static Enemy CurrentEnemy = new Enemy();
        internal static Hero MaximumStatsHero = new Hero();
        internal static List<Enemy> AllEnemies = new List<Enemy>();
        internal static List<Item> AllItems = new List<Item>();
        internal static List<HeadArmor> AllHeadArmor = new List<HeadArmor>();
        internal static List<BodyArmor> AllBodyArmor = new List<BodyArmor>();
        internal static List<HandArmor> AllHandArmor = new List<HandArmor>();
        internal static List<LegArmor> AllLegArmor = new List<LegArmor>();
        internal static List<FeetArmor> AllFeetArmor = new List<FeetArmor>();
        internal static List<Ring> AllRings = new List<Ring>();
        internal static List<Weapon> AllWeapons = new List<Weapon>();
        internal static List<Food> AllFood = new List<Food>();
        internal static List<Potion> AllPotions = new List<Potion>();
        internal static List<Spell> AllSpells = new List<Spell>();
        internal static List<Hero> AllHeroes = new List<Hero>();
        internal static List<HeroClass> AllClasses = new List<HeroClass>();
        internal static Weapon DefaultWeapon = new Weapon();
        internal static HeadArmor DefaultHead = new HeadArmor();
        internal static BodyArmor DefaultBody = new BodyArmor();
        internal static HandArmor DefaultHands = new HandArmor();
        internal static LegArmor DefaultLegs = new LegArmor();
        internal static FeetArmor DefaultFeet = new FeetArmor();

        internal static SQLiteDatabaseInteraction DatabaseInteraction = new SQLiteDatabaseInteraction();

        /// <summary>Changes the administrator password in the database.</summary>
        /// <param name="newPassword">New administrator password</param>
        /// <returns>Returns true if password successfully updated</returns>
        internal static async Task<bool> ChangeAdminPassword(string newPassword)
        {
            return await DatabaseInteraction.ChangeAdminPassword(newPassword);
        }

        /// <summary>Determines whether a Hero's credentials are authentic.</summary>
        /// <param name="username">Hero's name</param>
        /// <param name="password">Hero's password</param>
        /// <returns>Returns true if valid login</returns>
        internal static bool CheckLogin(string username, string password)
        {
            try
            {
                Hero checkHero = AllHeroes.Find(hero => hero.Name == username);
                if (checkHero != null && checkHero != new Hero())
                    if (Argon2.ValidatePassword(checkHero.Password, password))
                    {
                        CurrentHero = new Hero(checkHero);
                        return true;
                    }
                DisplayNotification("Invalid login.", "Sulimn");
                return false;
            }
            catch (Exception ex)
            {
                DisplayNotification(ex.Message, "Sulimn");
                return false;
            }
        }

        /// <summary>Loads almost everything from the database.</summary>
        internal static async Task LoadAll()
        {
            DatabaseInteraction.VerifyDatabaseIntegrity();

            AdminPassword = await DatabaseInteraction.LoadAdminPassword();
            AllClasses = await DatabaseInteraction.LoadClasses();
            AllHeadArmor = await DatabaseInteraction.LoadHeadArmor();
            AllBodyArmor = await DatabaseInteraction.LoadBodyArmor();
            AllHandArmor = await DatabaseInteraction.LoadHandArmor();
            AllLegArmor = await DatabaseInteraction.LoadLegArmor();
            AllFeetArmor = await DatabaseInteraction.LoadFeetArmor();
            AllRings = await DatabaseInteraction.LoadRings();
            AllFood = await DatabaseInteraction.LoadFood();
            AllPotions = await DatabaseInteraction.LoadPotions();
            AllSpells = await DatabaseInteraction.LoadSpells();
            AllWeapons = await DatabaseInteraction.LoadWeapons();
            AllItems.AddRanges(AllHeadArmor, AllBodyArmor, AllHandArmor, AllLegArmor, AllFeetArmor, AllRings, AllFood, AllPotions, AllWeapons);
            AllEnemies = await DatabaseInteraction.LoadEnemies();
            AllHeroes = await DatabaseInteraction.LoadHeroes();
            MaximumStatsHero = await DatabaseInteraction.LoadMaxHeroStats();

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

        internal static async Task<Bank> LoadHeroBank(Hero bankHero)
        {
            return await DatabaseInteraction.LoadBank(bankHero);
        }

        /// <summary>Gets a specific Enemy based on its name.</summary>
        /// <param name="name">Name of Enemy</param>
        /// <returns>Enemy</returns>
        private static Enemy GetEnemy(string name)
        {
            return new Enemy(AllEnemies.Find(enemy => enemy.Name == name));
        }

        /// <summary>Gets a specific Item based on its name.</summary>
        /// <param name="name">Item name</param>
        /// <returns>Item</returns>
        private static Item GetItem(string name)
        {
            return AllItems.Find(itm => itm.Name == name);
        }

        #region Item Management

        /// <summary>Retrieves a List of all Items of specified Type.</summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>List of specified Type.</returns>
        public static List<T> GetItemsOfType<T>()
        {
            List<T> newList = AllItems.OfType<T>().ToList();
            return newList;
        }

        /// <summary>Sets the Hero's inventory.</summary>
        /// <param name="inventory">Inventory to be converted</param>
        /// <param name="gold">Gold in the inventory</param>
        /// <returns>Inventory List</returns>
        internal static Inventory SetInventory(string inventory, int gold)
        {
            List<Item> itemList = new List<Item>();

            if (inventory.Length > 0)
            {
                string[] arrInventory = inventory.Split(',');

                foreach (string str in arrInventory)
                    itemList.Add(AllItems.Find(x => x.Name == str.Trim()));
            }
            return new Inventory(itemList, gold);
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

        #region Hero Saving

        /// <summary>Creates a new Hero and adds it to the database.</summary>
        /// <param name="newHero">New Hero</param>
        /// <returns>Returns true if successfully created</returns>
        internal static async Task<bool> NewHero(Hero newHero)
        {
            bool success = false;
            newHero.Equipment.Head = AllHeadArmor.Find(armr => armr.Name == DefaultHead.Name);
            newHero.Equipment.Body = AllBodyArmor.Find(armr => armr.Name == DefaultBody.Name);
            newHero.Equipment.Hands = AllHandArmor.Find(armr => armr.Name == DefaultHands.Name);
            newHero.Equipment.Legs = AllLegArmor.Find(armr => armr.Name == DefaultLegs.Name);
            newHero.Equipment.Feet = AllFeetArmor.Find(armr => armr.Name == DefaultFeet.Name);

            switch (newHero.Class.Name)
            {
                case "Wizard":
                    newHero.Equipment.Weapon = (Weapon)AllItems.Find(wpn => wpn.Name == "Starter Staff");
                    newHero.Spellbook.LearnSpell(AllSpells.Find(spell => spell.Name == "Fire Bolt"));
                    break;

                case "Cleric":
                    newHero.Equipment.Weapon = (Weapon)AllItems.Find(wpn => wpn.Name == "Starter Staff");
                    newHero.Spellbook.LearnSpell(AllSpells.Find(spell => spell.Name == "Heal Self"));
                    break;

                case "Warrior":
                    newHero.Equipment.Weapon = (Weapon)AllItems.Find(wpn => wpn.Name == "Stone Dagger");
                    break;

                case "Rogue":
                    newHero.Equipment.Weapon = (Weapon)AllItems.Find(wpn => wpn.Name == "Starter Bow");
                    break;

                default:
                    newHero.Equipment.Weapon = (Weapon)AllItems.Find(wpn => wpn.Name == "Stone Dagger");
                    break;
            }

            for (int i = 0; i < 3; i++)
                newHero.Inventory.AddItem(AllPotions.Find(itm => itm.Name == "Minor Healing Potion"));

            if (await DatabaseInteraction.NewHero(newHero))
            {
                AllHeroes.Add(newHero);
                success = true;
            }

            return success;
        }

        /// <summary>Saves Hero to database.</summary>
        /// <param name="saveHero">Hero to be saved</param>
        /// <returns>Returns true if successfully saved</returns>
        internal static async Task<bool> SaveHero(Hero saveHero)
        {
            bool success = false;
            if (await DatabaseInteraction.SaveHero(saveHero))
            {
                int index = AllHeroes.FindIndex(hero => hero.Name == saveHero.Name);
                AllHeroes[index] = new Hero(saveHero);
                success = true;
            }
            return success;
        }

        /// <summary>Saves the Hero's bank information.</summary>
        /// <param name="goldInBank">Gold in the bank</param>
        /// <param name="loanTaken">Loan taken out</param>
        internal static async Task<bool> SaveHeroBank(int goldInBank, int loanTaken)
        {
            return await DatabaseInteraction.SaveHeroBank(CurrentHero, goldInBank, loanTaken);
        }

        /// <summary>Saves the Hero's password to the database.</summary>
        /// <param name="saveHero">Hero whose password needs to be saved</param>
        internal static async Task<bool> SaveHeroPassword(Hero saveHero)
        {
            bool success = false;
            if (await DatabaseInteraction.SaveHeroPassword(saveHero))
            {
                int index = AllHeroes.FindIndex(hero => hero.Name == CurrentHero.Name);
                AllHeroes[index] = new Hero(CurrentHero);
                success = true;
            }
            return success;
        }

        #endregion Hero Saving

        #region Exploration Events

        /// <summary>Event where the Hero finds gold.</summary>
        /// <param name="minGold">Minimum amount of gold to be found</param>
        /// <param name="maxGold">Maximum amount of gold to be found</param>
        /// <returns>Returns text regarding gold found</returns>
        internal static async Task<string> EventFindGold(int minGold, int maxGold)
        {
            int foundGold = Functions.GenerateRandomNumber(minGold, maxGold);
            CurrentHero.Inventory.Gold += foundGold;
            await SaveHero(CurrentHero);
            return $"You find {foundGold:N0} gold!";
        }

        /// <summary>Event where the Hero finds an item.</summary>
        /// <param name="minValue">Minimum value of Item</param>
        /// <param name="maxValue">Maximum value of Item</param>
        /// <param name="canSell">Can the item be sold?</param>
        /// <returns>Returns text about found Item</returns>
        internal static async Task<string> EventFindItem(int minValue, int maxValue, bool canSell = true)
        {
            List<Item> availableItems = AllItems.Where(x => x.Value >= minValue && x.Value <= maxValue && x.IsSold).ToList();
            int item = Functions.GenerateRandomNumber(0, availableItems.Count - 1);

            CurrentHero.Inventory.AddItem(availableItems[item]);
            await SaveHero(CurrentHero);
            return $"You find a {availableItems[item].Name}!";
        }

        /// <summary>Event where the Hero finds an item.</summary>
        /// <param name="names">List of names of available Items</param>
        /// <returns>Returns text about found Item</returns>
        internal static async Task<string> EventFindItem(params string[] names)
        {
            List<Item> availableItems = new List<Item>();
            foreach (string name in names)
                availableItems.Add(GetItem(name));
            int item = Functions.GenerateRandomNumber(0, availableItems.Count - 1);

            CurrentHero.Inventory.AddItem(availableItems[item]);

            await SaveHero(CurrentHero);
            return $"You find a {availableItems[item].Name}!";
        }

        /// <summary>Event where the Hero encounters a hostile animal.</summary>
        /// <param name="minLevel">Minimum level of animal</param>
        /// <param name="maxLevel">Maximum level of animal</param>
        internal static void EventEncounterAnimal(int minLevel, int maxLevel)
        {
            List<Enemy> availableEnemies = AllEnemies.Where(o => o.Level >= minLevel && o.Level <= maxLevel).ToList();
            int enemyNum = Functions.GenerateRandomNumber(0, availableEnemies.Count - 1);
            CurrentEnemy = new Enemy(availableEnemies[enemyNum]);
        }

        /// <summary>Event where the Hero encounters a hostile Enemy.</summary>
        /// <param name="minLevel">Minimum level of Enemy.</param>
        /// <param name="maxLevel">Maximum level of Enemy.</param>
        internal static void EventEncounterEnemy(int minLevel, int maxLevel)
        {
            List<Enemy> availableEnemies = AllEnemies.Where(enemy => enemy.Level >= minLevel && enemy.Level <= maxLevel).ToList();
            int enemyNum = Functions.GenerateRandomNumber(0, availableEnemies.Count - 1);
            CurrentEnemy = new Enemy(availableEnemies[enemyNum]);
            if (CurrentEnemy.Inventory.Gold > 0)
                CurrentEnemy.Inventory.Gold = Functions.GenerateRandomNumber(CurrentEnemy.Inventory.Gold / 2,
                CurrentEnemy.Inventory.Gold);
        }

        /// <summary>Event where the Hero encounters a hostile Enemy.</summary>
        /// <param name="names">Array of names</param>
        internal static void EventEncounterEnemy(params string[] names)
        {
            List<Enemy> availableEnemies = new List<Enemy>();
            foreach (string name in names)
                availableEnemies.Add(GetEnemy(name));
            int enemyNum = Functions.GenerateRandomNumber(0, availableEnemies.Count - 1);
            CurrentEnemy = new Enemy(availableEnemies[enemyNum]);
            if (CurrentEnemy.Inventory.Gold > 0)
                CurrentEnemy.Inventory.Gold = Functions.GenerateRandomNumber(CurrentEnemy.Inventory.Gold / 2,
                CurrentEnemy.Inventory.Gold);
        }

        /// <summary>Event where the Hero encounters a water stream and restores health and magic.</summary>
        /// <returns>String saying Hero has been healed</returns>
        internal static string EventEncounterStream()
        {
            CurrentHero.Statistics.CurrentHealth = CurrentHero.Statistics.MaximumHealth;
            CurrentHero.Statistics.CurrentMagic = CurrentHero.Statistics.MaximumMagic;

            return
            "You stumble across a stream. You stop to drink some of the water and rest a while. You feel recharged!";
        }

        #endregion Exploration Events

        #region Notification Management

        /// <summary>Displays a new Notification in a thread-safe way.</summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="title">Title of the Notification Window</param>
        internal static void DisplayNotification(string message, string title)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                new Notification(message, title, NotificationButtons.OK).ShowDialog();
            });
        }

        /// <summary>Displays a new Notification in a thread-safe way.</summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="title">Title of the Notification Window</param>
        /// <param name="window">Window being referenced</param>
        internal static void DisplayNotification(string message, string title, Window window)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                new Notification(message, title, NotificationButtons.OK, window).ShowDialog();
            });
        }

        /// <summary>Displays a new Notification in a thread-safe way and retrieves a boolean result upon its closing.</summary>
        /// <param name="message">Message to be displayed</param>
        /// <param name="title">Title of the Notification Window</param>
        /// <param name="window">Window being referenced</param>
        /// <returns>Returns value of clicked button on Notification.</returns>
        internal static bool YesNoNotification(string message, string title, Window window)
        {
            bool result = false;
            Application.Current.Dispatcher.Invoke(delegate
            {
                if (new Notification(message, title, NotificationButtons.YesNo, window).ShowDialog() == true)
                    result = true;
            });
            return result;
        }

        #endregion Notification Management
    }
}