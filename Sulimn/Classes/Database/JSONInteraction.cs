using Newtonsoft.Json;
using Sulimn.Classes.Entities;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Sulimn.Classes.Database
{
    /// <summary>Implements functionality of reading and writing game data from JSON files.</summary>
    public static class JSONInteraction
    {
        private static readonly string DataFolderLocation = Path.Combine(AppData.Location, "Data");
        private static readonly string SaveFolderLocation = Path.Combine(AppData.Location, "Save");

        #region Write

        /// <summary>Writes all important files to disk</summary>
        internal static void WriteAll(List<HeroClass> classes, List<Item> headArmor, List<Item> bodyArmor, List<Item> handArmor, List<Item> legArmor, List<Item> feetArmor, List<Item> rings, List<Item> weapons, List<Item> drinks, List<Item> food, List<Item> potions, List<Spell> spells, List<Enemy> enemies)
        {
            Write(classes, Path.Combine(DataFolderLocation, "classes.json"));
            Write(headArmor, Path.Combine(DataFolderLocation, "head_armor.json"));
            Write(bodyArmor, Path.Combine(DataFolderLocation, "body_armor.json"));
            Write(handArmor, Path.Combine(DataFolderLocation, "hand_armor.json"));
            Write(legArmor, Path.Combine(DataFolderLocation, "leg_armor.json"));
            Write(feetArmor, Path.Combine(DataFolderLocation, "feet_armor.json"));
            Write(rings, Path.Combine(DataFolderLocation, "rings.json"));
            Write(weapons, Path.Combine(DataFolderLocation, "weapons.json"));
            Write(drinks, Path.Combine(DataFolderLocation, "drinks.json"));
            Write(food, Path.Combine(DataFolderLocation, "food.json"));
            Write(potions, Path.Combine(DataFolderLocation, "potions.json"));
            Write(spells, Path.Combine(DataFolderLocation, "spells.json"));
            Write(enemies, Path.Combine(DataFolderLocation, "enemies.json"));
        }

        /// <summary>Writes a List of any type to disk.</summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="list">List of object</param>
        /// <param name="path">Path to where it will be written</param>
        private static void Write<T>(List<T> list, string path)
        {
            if (list.Count > 0)
                File.WriteAllText(path, JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented));
        }

        #endregion Write

        #region Settings

        /// <summary>Loads the game's <see cref="Settings"/> from disk.</summary>
        /// <returns>Game Settings</returns>
        internal static Settings LoadSettings()
        {
            string settingsLocation = Path.Combine(DataFolderLocation, "Settings.xml");
            Settings newSettings = new Settings("", "");
            XmlDocument xmlDoc = new XmlDocument { XmlResolver = null };

            if (File.Exists(settingsLocation))
            {
                try
                {
                    xmlDoc.Load(settingsLocation);
                    foreach (XmlNode xn in xmlDoc.SelectNodes("/Settings"))
                    {
                        newSettings.AdminPassword = xn["AdminPassword"]?.InnerText;
                        newSettings.Theme = xn["Theme"]?.InnerText;
                    }
                }
                catch (XmlException e)
                {
                    GameState.DisplayNotification($"Error loading settings: {e.Message}.", "Sulimn");
                }
            }
            return newSettings;
        }

        /// <summary>Writes the current <see cref="Settings"/> to file.</summary>
        /// <param name="settings">Current <see cref="Settings"/></param>
        internal static void WriteSettings(Settings settings)
        {
            string location = Path.Combine(DataFolderLocation, "Settings.xml");
            using (XmlTextWriter writer = new XmlTextWriter(location, Encoding.UTF8))
            {
                writer.Formatting = System.Xml.Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();

                writer.WriteStartElement("Settings");
                writer.WriteElementString("AdminPassword", settings.AdminPassword);
                writer.WriteElementString("Theme", settings.Theme);
                writer.WriteEndDocument();
            }
        }

        #endregion Settings

        #region Load

        /// <summary>Loads JSON data from a file.</summary>
        /// <param name="path">Path to the file to be loaded</param>
        /// <returns>JSON data from a file</returns>
        private static List<T> LoadJsonFromFile<T>(string path)
        {
            string data = "";
            if (File.Exists(path))
                data = File.ReadAllText(path);
            else
                GameState.DisplayNotification($"{path} does not exist.", "Sulimn");

            return !string.IsNullOrWhiteSpace(data) ? JsonConvert.DeserializeObject<List<T>>(data) : new List<T>();
        }

        /// <summary>Loads all <see cref="HeroClass"/>es from disk.</summary>
        /// <returns>List of <see cref="HeroClass"/>es</returns>
        internal static List<HeroClass> LoadClasses() => LoadJsonFromFile<HeroClass>(Path.Combine(DataFolderLocation, "classes.json"));

        /// <summary>Loads all Armor of specified type.</summary>
        /// <typeparam name="T">Type of Armor</typeparam>
        /// <param name="type">Type of Armor</param>
        /// <returns>List of Armor of specified type</returns>
        internal static List<T> LoadArmor<T>(string type) => LoadJsonFromFile<T>(Path.Combine(DataFolderLocation, $"{type}_armor.json"));

        /// <summary>Loads all Rings from disk.</summary>
        /// <returns>List of Rings</returns>
        internal static List<Item> LoadRings() => LoadJsonFromFile<Item>(Path.Combine(DataFolderLocation, "rings.json"));

        /// <summary>Loads all Drinks from disk.</summary>
        /// <returns>List of Drinks</returns>
        internal static List<Item> LoadDrinks() => LoadJsonFromFile<Item>(Path.Combine(DataFolderLocation, "drinks.json"));

        /// <summary>Loads all Food from disk.</summary>
        /// <returns>List of Food/></returns>
        internal static List<Item> LoadFood() => LoadJsonFromFile<Item>(Path.Combine(DataFolderLocation, "food.json"));

        /// <summary>Loads all Potions from disk.</summary>
        /// <returns>List of Potions</returns>
        internal static List<Item> LoadPotions() => LoadJsonFromFile<Item>(Path.Combine(DataFolderLocation, "potions.json"));

        /// <summary>Loads all <see cref="Spell"/>s from disk.</summary>
        /// <returns>List of <see cref="Spell"/>s</returns>
        internal static List<Spell> LoadSpells() => LoadJsonFromFile<Spell>(Path.Combine(DataFolderLocation, "spells.json"));

        /// <summary>Loads all <see cref="Weapon"/>s from disk.</summary>
        /// <returns>List of <see cref="Weapon"/>s</returns>
        internal static List<Item> LoadWeapons() => LoadJsonFromFile<Item>(Path.Combine(DataFolderLocation, "weapons.json"));

        /// <summary>Loads all <see cref="Enemy"/>s from disk.</summary>
        /// <returns>List of <see cref="Enemy"/>s</returns>
        internal static List<Enemy> LoadEnemies() => LoadJsonFromFile<Enemy>(Path.Combine(DataFolderLocation, "enemies.json"));

        #endregion Load

        #region Hero Manipulation

        /// <summary>Deletes a <see cref="Hero"/> from disk.</summary>
        /// <param name="deleteHero"><see cref="Hero"/> to be deleted</param>
        /// <returns>True if file no longer exists</returns>
        internal static bool DeleteHero(Hero deleteHero)
        {
            string path = Path.Combine(SaveFolderLocation, $"{deleteHero.Name}.json");

            if (File.Exists(path))
            {
                File.Delete(path);
                return File.Exists(path);
            }
            else
                return false;
        }

        /// <summary>Loads all <see cref="Hero"/>es from disk.</summary>
        /// <returns>List of <see cref="Hero"/>es</returns>
        internal static List<Hero> LoadHeroes()
        {
            List<Hero> heroes = new List<Hero>();
            if (!Directory.Exists(SaveFolderLocation))
                Directory.CreateDirectory(SaveFolderLocation);
            string[] files = Directory.GetFiles(SaveFolderLocation);
            if (files.Length > 0)
            {
                foreach (string contents in files)
                    heroes.Add(JsonConvert.DeserializeObject<Hero>(contents));
            }

            return heroes;
        }

        /// <summary>Saves a <see cref="Hero"/> to disk.</summary>
        /// <param name="saveHero"><see cref="Hero"/> to be saved to disk</param>
        internal static void SaveHero(Hero saveHero)
        {
            if (!Directory.Exists(SaveFolderLocation))
                Directory.CreateDirectory(SaveFolderLocation);
            File.WriteAllText(Path.Combine(SaveFolderLocation, $"{saveHero.Name}.json"), JsonConvert.SerializeObject(saveHero, Newtonsoft.Json.Formatting.Indented));
        }

        #endregion Hero Manipulation
    }
}