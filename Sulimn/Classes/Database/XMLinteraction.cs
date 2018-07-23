using Extensions.DataTypeHelpers;
using Sulimn.Classes.Entities;
using Sulimn.Classes.Enums;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sulimn.Classes.Database
{
    internal static class XMLInteraction
    {
        #region Load

        //TODO Change all Items to implement durability, allowed classes, and minimum level

        /// <summary>Loads all Classes from the database.</summary>
        /// <returns>List of Classes</returns>
        public static List<HeroClass> LoadClasses()
        {
            List<HeroClass> allClasses = new List<HeroClass>();
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("Data/Classes/Classes.xml"))
            {
                try
                {
                    xmlDoc.Load("Data/Classes/Classes.xml");
                    foreach (XmlNode xn in xmlDoc.SelectNodes("/AllClasses/Class"))
                    {
                        HeroClass newClass = new HeroClass(
                        name: xn["Name"].InnerText,
                        description: xn["Description"].InnerText,
                        skillPoints: Int32Helper.Parse(xn["SkillPoints"].InnerText),
                        strength: Int32Helper.Parse(xn["Strength"].InnerText),
                        vitality: Int32Helper.Parse(xn["Vitality"].InnerText),
                        dexterity: Int32Helper.Parse(xn["Dexterity"].InnerText),
                        wisdom: Int32Helper.Parse(xn["Wisdom"].InnerText));

                        allClasses.Add(newClass);
                    }
                }
                catch (Exception e)
                {
                    GameState.DisplayNotification($"Error loading classes: {e.Message}.", "Sulimn");
                }
            }

            return allClasses;
        }

        #region Entities

        /// <summary>Loads all Enemies from the database.</summary>
        /// <returns>List of Enemies</returns>
        public static List<Enemy> LoadEnemies()
        {
            List<Enemy> allEnemies = new List<Enemy>();
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("Data/Classes/Classes.xml"))
            {
                try
                {
                    xmlDoc.Load("Data/Classes/Classes.xml");
                    foreach (XmlNode xn in xmlDoc.SelectNodes("/AllClasses"))
                    {
                        Weapon weapon = new Weapon();
                        if (!string.IsNullOrWhiteSpace(xn["Weapon"].ToString()))
                            weapon =
                            new Weapon(
                            GameState.AllWeapons.Find(wpn => wpn.Name == xn["Weapon"].ToString()));
                        HeadArmor head = new HeadArmor();
                        if (!string.IsNullOrWhiteSpace(xn["Head"].ToString()))
                            head =
                            new HeadArmor(
                            GameState.AllHeadArmor.Find(armr => armr.Name == xn["Head"].ToString()));
                        BodyArmor body = new BodyArmor();
                        if (!string.IsNullOrWhiteSpace(xn["Body"].ToString()))
                            body =
                            new BodyArmor(
                            GameState.AllBodyArmor.Find(armr => armr.Name == xn["Body"].ToString()));
                        HandArmor hands = new HandArmor();
                        if (!string.IsNullOrWhiteSpace(xn["Hands"].ToString()))
                            hands =
                            new HandArmor(
                            GameState.AllHandArmor.Find(armr => armr.Name == xn["Hands"].ToString()));
                        LegArmor legs = new LegArmor();
                        if (!string.IsNullOrWhiteSpace(xn["Legs"].ToString()))
                            legs =
                            new LegArmor(
                            GameState.AllLegArmor.Find(armr => armr.Name == xn["Legs"].ToString()));
                        FeetArmor feet = new FeetArmor();
                        if (!string.IsNullOrWhiteSpace(xn["Feet"].ToString()))
                            feet =
                            new FeetArmor(
                            GameState.AllFeetArmor.Find(armr => armr.Name == xn["Feet"].ToString()));
                        Ring leftRing = new Ring();
                        if (!string.IsNullOrWhiteSpace(xn["LeftRing"].ToString()))
                            leftRing =
                            new Ring(
                            GameState.AllRings.Find(ring => ring.Name == xn["LeftRing"].ToString()));
                        Ring rightRing = new Ring();
                        if (!string.IsNullOrWhiteSpace(xn["RightRing"].ToString()))
                            rightRing =
                            new Ring(
                            GameState.AllRings.Find(ring => ring.Name == xn["RightRing"].ToString()));

                        Enemy newEnemy = new Enemy(
                        xn["Name"].ToString(),
                        xn["Type"].ToString(),
                        Int32Helper.Parse(xn["Level"]),
                        Int32Helper.Parse(xn["Experience"]),
                        Int32Helper.Parse(xn["Gold"]),
                        new Attributes(
                        Int32Helper.Parse(xn["Strength"]),
                        Int32Helper.Parse(xn["Vitality"]),
                        Int32Helper.Parse(xn["Dexterity"]),
                        Int32Helper.Parse(xn["Wisdom"])),
                        new Statistics(
                        Int32Helper.Parse(xn["CurrentHealth"]),
                        Int32Helper.Parse(xn["MaximumHealth"]),
                        Int32Helper.Parse(xn["CurrentMagic"]),
                        Int32Helper.Parse(xn["MaximumMagic"])),
                        new Equipment(
                        weapon,
                        head,
                        body,
                        hands,
                        legs,
                        feet,
                        leftRing,
                        rightRing));

                        allEnemies.Add(newEnemy);
                    }
                }
                catch (Exception e)
                {
                    GameState.DisplayNotification($"Error loading enemies: {e.Message}.", "Sulimn");
                }
            }

            return allEnemies;
        }

        /// <summary>Loads all Heroes from the database.</summary>
        /// <returns>List of Heroes</returns>
        public static List<Hero> LoadHeroes()
        {
            List<Hero> allHeroes = new List<Hero>();
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("Data/Classes/Classes.xml"))
            {
                try
                {
                    xmlDoc.Load("Data/Classes/Classes.xml");
                    foreach (XmlNode xn in xmlDoc.SelectNodes("/AllClasses"))
                    {
                        string name = xn["Name"].ToString();
                        string leftRingText = xn["LeftRing"].ToString();
                        string rightRingText = xn["RightRing"].ToString();

                        Ring leftRing = new Ring();
                        Ring rightRing = new Ring();
                        if (!string.IsNullOrWhiteSpace(leftRingText))
                            leftRing = GameState.AllRings.Find(x => x.Name == leftRingText);
                        if (!string.IsNullOrWhiteSpace(rightRingText))
                            rightRing = GameState.AllRings.Find(x => x.Name == rightRingText);

                        string className = xn["Class"].ToString();
                        Hero newHero = new Hero(
                        name,
                        xn["Password"].ToString(),
                        new HeroClass(
                        GameState.AllClasses.Find(
                        heroClass => heroClass.Name == className)),
                        Int32Helper.Parse(xn["Level"]),
                        Int32Helper.Parse(xn["Experience"]),
                        Int32Helper.Parse(xn["SkillPoints"]),
                        Int32Helper.Parse(xn["Gold"]),
                        new Attributes(
                        Int32Helper.Parse(xn["Strength"]),
                        Int32Helper.Parse(xn["Vitality"]),
                        Int32Helper.Parse(xn["Dexterity"]),
                        Int32Helper.Parse(xn["Wisdom"])),
                        new Statistics(
                        Int32Helper.Parse(xn["CurrentHealth"]),
                        Int32Helper.Parse(xn["MaximumHealth"]),
                        Int32Helper.Parse(xn["CurrentMagic"]),
                        Int32Helper.Parse(xn["MaximumMagic"])),
                        new Equipment(
                        GameState.AllWeapons.Find(x => x.Name == xn["Weapon"].ToString()),
                        GameState.AllHeadArmor.Find(x => x.Name == xn["Head"].ToString()),
                        GameState.AllBodyArmor.Find(x => x.Name == xn["Body"].ToString()),
                        GameState.AllHandArmor.Find(x => x.Name == xn["Hands"].ToString()),
                        GameState.AllLegArmor.Find(x => x.Name == xn["Legs"].ToString()),
                        GameState.AllFeetArmor.Find(x => x.Name == xn["Feet"].ToString()),
                        leftRing,
                        rightRing),
                        GameState.SetSpellbook(xn["KnownSpells"].ToString()),
                        GameState.SetInventory(xn["Inventory"].ToString()),
                        new Progression(BoolHelper.Parse(xn["Fields"]), BoolHelper.Parse(xn["Forest"]), BoolHelper.Parse(xn["Cathedral"]), BoolHelper.Parse(xn["Mines"]), BoolHelper.Parse(xn["Catacombs"]), BoolHelper.Parse(xn["Courtyard"]), BoolHelper.Parse(xn["Battlements"]), BoolHelper.Parse(xn["Armoury"]), BoolHelper.Parse(xn["Spire"]), BoolHelper.Parse(xn["ThroneRoom"])),
                        BoolHelper.Parse(xn["Hardcore"]));

                        allHeroes.Add(newHero);
                    }
                }
                catch (Exception e)
                {
                    GameState.DisplayNotification($"Error loading heroes: {e.Message}.", "Sulimn");
                }
            }

            return allHeroes;
        }

        #endregion Entities

        #region Items

        #region Equipment

        #region Armor

        /// <summary>Loads all Head Armor from the database.</summary>
        /// <returns>List of Head Armor</returns>
        public static List<HeadArmor> LoadHeadArmor()
        {
            List<HeadArmor> allHeadArmor = new List<HeadArmor>();
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("Data/Armor/HeadArmor.xml"))
            {
                try
                {
                    xmlDoc.Load("Data/Armor/HeadArmor.xml");
                    foreach (XmlNode xn in xmlDoc.SelectNodes("/AllHeadArmor/HeadArmor"))
                    {
                        HeadArmor newHeadArmor = new HeadArmor(
                        xn["Name"].InnerText,
                        xn["Description"].InnerText,
                        Int32Helper.Parse(xn["Defense"].InnerText),
                        Int32Helper.Parse(xn["Weight"].InnerText),
                        Int32Helper.Parse(xn["Value"].InnerText),
                        BoolHelper.Parse(xn["CanSell"].InnerText),
                        BoolHelper.Parse(xn["IsSold"].InnerText));

                        allHeadArmor.Add(newHeadArmor);
                    }
                }
                catch (Exception e)
                {
                    GameState.DisplayNotification($"Error loading Head Armor: {e.Message}.", "Sulimn");
                }
            }

            return allHeadArmor;
        }

        /// <summary>Loads all Body Armor from the database.</summary>
        /// <returns>List of Body Armor</returns>
        public static List<BodyArmor> LoadBodyArmor()
        {
            List<BodyArmor> allBodyArmor = new List<BodyArmor>();
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("Data/Armor/BodyArmor.xml"))
            {
                try
                {
                    xmlDoc.Load("Data/Armor/BodyArmor.xml");
                    foreach (XmlNode xn in xmlDoc.SelectNodes("/AllBodyArmor/BodyArmor"))
                    {
                        BodyArmor newBodyArmor = new BodyArmor(
                        xn["Name"].InnerText,
                        xn["Description"].InnerText,
                        Int32Helper.Parse(xn["Defense"].InnerText),
                        Int32Helper.Parse(xn["Weight"].InnerText),
                        Int32Helper.Parse(xn["Value"].InnerText),
                        BoolHelper.Parse(xn["CanSell"].InnerText),
                        BoolHelper.Parse(xn["IsSold"].InnerText));

                        allBodyArmor.Add(newBodyArmor);
                    }
                }
                catch (Exception e)
                {
                    GameState.DisplayNotification($"Error loading Body Armor: {e.Message}.", "Sulimn");
                }
            }

            return allBodyArmor;
        }

        /// <summary>Loads all Hand Armor from the database.</summary>
        /// <returns>List of Hand Armor</returns>
        public static List<HandArmor> LoadHandArmor()
        {
            List<HandArmor> allHandArmor = new List<HandArmor>();
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("Data/Armor/HandArmor.xml"))
            {
                try
                {
                    xmlDoc.Load("Data/Armor/HandArmor.xml");
                    foreach (XmlNode xn in xmlDoc.SelectNodes("/AllHandArmor/HandArmor"))
                    {
                        HandArmor newHandArmor = new HandArmor(
                        xn["Name"].InnerText,
                        xn["Description"].InnerText,
                        Int32Helper.Parse(xn["Defense"].InnerText),
                        Int32Helper.Parse(xn["Weight"].InnerText),
                        Int32Helper.Parse(xn["Value"].InnerText),
                        BoolHelper.Parse(xn["CanSell"].InnerText),
                        BoolHelper.Parse(xn["IsSold"].InnerText));

                        allHandArmor.Add(newHandArmor);
                    }
                }
                catch (Exception e)
                {
                    GameState.DisplayNotification($"Error loading Hand Armor: {e.Message}.", "Sulimn");
                }
            }

            return allHandArmor;
        }

        /// <summary>Loads all Leg Armor from the database.</summary>
        /// <returns>List of Leg Armor</returns>
        public static List<LegArmor> LoadLegArmor()
        {
            List<LegArmor> allLegArmor = new List<LegArmor>();
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("Data/Armor/LegArmor.xml"))
            {
                try
                {
                    xmlDoc.Load("Data/Armor/LegArmor.xml");
                    foreach (XmlNode xn in xmlDoc.SelectNodes("/AllLegArmor/LegArmor"))
                    {
                        LegArmor newLegArmor = new LegArmor(
                        xn["Name"].InnerText,
                        xn["Description"].InnerText,
                        Int32Helper.Parse(xn["Defense"].InnerText),
                        Int32Helper.Parse(xn["Weight"].InnerText),
                        Int32Helper.Parse(xn["Value"].InnerText),
                        BoolHelper.Parse(xn["CanSell"].InnerText),
                        BoolHelper.Parse(xn["IsSold"].InnerText));

                        allLegArmor.Add(newLegArmor);
                    }
                }
                catch (Exception e)
                {
                    GameState.DisplayNotification($"Error loading Leg Armor: {e.Message}.", "Sulimn");
                }
            }

            return allLegArmor;
        }

        /// <summary>Loads all Feet Armor from the database.</summary>
        /// <returns>List of Feet Armor</returns>
        public static List<FeetArmor> LoadFeetArmor()
        {
            List<FeetArmor> allFeetArmor = new List<FeetArmor>();
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("Data/Armor/FeetArmor.xml"))
            {
                try
                {
                    xmlDoc.Load("Data/Armor/FeetArmor.xml");
                    foreach (XmlNode xn in xmlDoc.SelectNodes("/AllFeetArmor/FeetArmor"))
                    {
                        FeetArmor newFeetArmor = new FeetArmor(
                        xn["Name"].InnerText,
                        xn["Description"].InnerText,
                        Int32Helper.Parse(xn["Defense"].InnerText),
                        Int32Helper.Parse(xn["Weight"].InnerText),
                        Int32Helper.Parse(xn["Value"].InnerText),
                        BoolHelper.Parse(xn["CanSell"].InnerText),
                        BoolHelper.Parse(xn["IsSold"].InnerText));

                        allFeetArmor.Add(newFeetArmor);
                    }
                }
                catch (Exception e)
                {
                    GameState.DisplayNotification($"Error loading Feet Armor: {e.Message}.", "Sulimn");
                }
            }

            return allFeetArmor;
        }

        #endregion Armor

        /// <summary>Loads all Rings from the database.</summary>
        /// <returns>List of Rings</returns>
        public static List<Ring> LoadRings()
        {
            List<Ring> allRings = new List<Ring>();
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("Data/Rings/Rings.xml"))
            {
                try
                {
                    xmlDoc.Load("Data/Rings/Rings.xml");
                    foreach (XmlNode xn in xmlDoc.SelectNodes("/AllRings/Ring"))
                    {
                        Ring newRing = new Ring(
                        xn["Name"].InnerText,
                        xn["Description"].InnerText,
                        Int32Helper.Parse(xn["Damage"].InnerText),
                        Int32Helper.Parse(xn["Defense"].InnerText),
                        Int32Helper.Parse(xn["Strength"].InnerText),
                        Int32Helper.Parse(xn["Vitality"].InnerText),
                        Int32Helper.Parse(xn["Dexterity"].InnerText),
                        Int32Helper.Parse(xn["Wisdom"].InnerText),
                        Int32Helper.Parse(xn["Weight"].InnerText),
                        Int32Helper.Parse(xn["Value"].InnerText),
                        BoolHelper.Parse(xn["CanSell"].InnerText),
                        BoolHelper.Parse(xn["IsSold"].InnerText));

                        allRings.Add(newRing);
                    }
                }
                catch (Exception e)
                {
                    GameState.DisplayNotification($"Error loading Rings: {e.Message}.", "Sulimn");
                }
            }

            return allRings;
        }

        /// <summary>Loads all Weapons from the database.</summary>
        /// <returns>List of Weapons</returns>
        public static List<Weapon> LoadWeapons()
        {
            List<Weapon> allWeapons = new List<Weapon>();
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("Data/Weapons/Weapons.xml"))
            {
                try
                {
                    xmlDoc.Load("Data/Weapons/Weapons.xml");
                    foreach (XmlNode xn in xmlDoc.SelectNodes("/AllWeapons/Weapon"))
                    {
                        Weapon newWeapon = new Weapon(
                        xn["Name"].InnerText,
                        EnumHelper.Parse<WeaponTypes>(xn["WeaponType"].InnerText),
                        xn["Description"].InnerText,
                        Int32Helper.Parse(xn["Damage"].InnerText),
                        Int32Helper.Parse(xn["Weight"].InnerText),
                        Int32Helper.Parse(xn["Value"].InnerText),
                        BoolHelper.Parse(xn["CanSell"].InnerText),
                        BoolHelper.Parse(xn["IsSold"].InnerText));

                        allWeapons.Add(newWeapon);
                    }
                }
                catch (Exception e)
                {
                    GameState.DisplayNotification($"Error loading Weapons: {e.Message}.", "Sulimn");
                }
            }

            return allWeapons;
        }

        #endregion Equipment

        /// <summary>Loads all Potions from the database.</summary>
        /// <returns>List of Potions</returns>
        public static List<Potion> LoadPotions()
        {
            List<Potion> allPotions = new List<Potion>();
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("Data/Potions/Potions.xml"))
            {
                try
                {
                    xmlDoc.Load("Data/Potions/Potions.xml");
                    foreach (XmlNode xn in xmlDoc.SelectNodes("/AllPotions/Potion"))
                    {
                        Potion newPotion = new Potion(
                        xn["Name"].InnerText,
                        xn["Description"].InnerText,
                        Int32Helper.Parse(xn["RestoreHealth"].InnerText),
                        Int32Helper.Parse(xn["RestoreMagic"].InnerText),
                        BoolHelper.Parse(xn["Cures"].InnerText),
                        Int32Helper.Parse(xn["Weight"].InnerText),
                        Int32Helper.Parse(xn["Value"].InnerText),
                        BoolHelper.Parse(xn["CanSell"].InnerText),
                        BoolHelper.Parse(xn["IsSold"].InnerText));

                        allPotions.Add(newPotion);
                    }
                }
                catch (Exception e)
                {
                    GameState.DisplayNotification($"Error loading Potions: {e.Message}.", "Sulimn");
                }
            }

            return allPotions;
        }

        /// <summary>Loads all Drink from the database.</summary>
        /// <returns>List of Drink</returns>
        public static List<Drink> LoadDrinks()
        {
            List<Drink> allDrinks = new List<Drink>();
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("Data/Drinks/Drinks.xml"))
            {
                try
                {
                    xmlDoc.Load("Data/Drinks/Drinks.xml");
                    foreach (XmlNode xn in xmlDoc.SelectNodes("/AllDrinks/Drink"))
                    {
                        Drink newDrink = new Drink(
                        xn["Name"].InnerText,
                        xn["Description"].InnerText,
                        Int32Helper.Parse(xn["RestoreHealth"].InnerText),
                        Int32Helper.Parse(xn["RestoreMagic"].InnerText),
                        BoolHelper.Parse(xn["Cures"].InnerText),
                        Int32Helper.Parse(xn["Weight"].InnerText),
                        Int32Helper.Parse(xn["Value"].InnerText),
                        BoolHelper.Parse(xn["CanSell"].InnerText),
                        BoolHelper.Parse(xn["IsSold"].InnerText));

                        allDrinks.Add(newDrink);
                    }
                }
                catch (Exception e)
                {
                    GameState.DisplayNotification($"Error loading Drinks: {e.Message}.", "Sulimn");
                }
            }

            return allDrinks;
        }

        /// <summary>Loads all Food from the database.</summary>
        /// <returns>List of Food</returns>
        public static List<Food> LoadFood()
        {
            List<Food> allFood = new List<Food>();
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("Data/Food/Food.xml"))
            {
                try
                {
                    xmlDoc.Load("Data/Food/Food.xml");
                    foreach (XmlNode xn in xmlDoc.SelectNodes("/AllFood/Food"))
                    {
                        Food newFood = new Food(
                        xn["Name"].InnerText,
                        xn["Description"].InnerText,
                        Int32Helper.Parse(xn["RestoreHealth"].InnerText),
                        Int32Helper.Parse(xn["RestoreMagic"].InnerText),
                        BoolHelper.Parse(xn["Cures"].InnerText),
                        Int32Helper.Parse(xn["Weight"].InnerText),
                        Int32Helper.Parse(xn["Value"].InnerText),
                        BoolHelper.Parse(xn["CanSell"].InnerText),
                        BoolHelper.Parse(xn["IsSold"].InnerText));

                        allFood.Add(newFood);
                    }
                }
                catch (Exception e)
                {
                    GameState.DisplayNotification($"Error loading Food: {e.Message}.", "Sulimn");
                }
            }

            return allFood;
        }

        /// <summary>Loads all Spells from the database.</summary>
        /// <returns>List of Spells</returns>
        public static List<Spell> LoadSpells()
        {
            List<Spell> allSpells = new List<Spell>();
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("Data/Spells/Spells.xml"))
            {
                try
                {
                    xmlDoc.Load("Data/Spells/Spells.xml");
                    foreach (XmlNode xn in xmlDoc.SelectNodes("/AllSpells/Spell"))
                    {
                        Spell newSpell = new Spell(
                        xn["Name"].InnerText,
                        EnumHelper.Parse<SpellTypes>(xn["Type"].InnerText),
                        xn["Description"].InnerText,
                        GameState.SetAllowedClasses(xn["AllowedClasses"].InnerText),
                        Int32Helper.Parse(xn["RequiredLevel"].InnerText),
                        Int32Helper.Parse(xn["MagicCost"].InnerText),
                        Int32Helper.Parse(xn["Amount"].InnerText));

                        allSpells.Add(newSpell);
                    }
                }
                catch (Exception e)
                {
                    GameState.DisplayNotification($"Error loading Spells: {e.Message}.", "Sulimn");
                }
            }

            return allSpells;
        }

        #endregion Items

        #endregion Load

        internal static Hero LoadHero(string filename)
        {
            Hero loadHero = new Hero();
            return loadHero;
        }

        #region Write

        internal static void WriteAll()
        {
            //WriteAllHeadArmor();
            //WriteAllBodyArmor();
            //WriteAllHandArmor();
            //WriteAllLegArmor();
            //WriteAllFeetArmor();
            //WriteAllWeapons();
            WriteAllClasses();
            //WriteAllRings();
            //WriteAllEnemies();
            //WriteAllHeroes();
            //WriteAllDrinks();
            //WriteAllFood();
            //WriteAllPotions();
            //WriteAllSpells();
        }

        #region Write Consumables

        internal static void WriteAllDrinks()
        {
            using (XmlTextWriter writer = new XmlTextWriter("Data/Drinks/Drinks.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();

                writer.WriteStartElement("AllDrinks");
                foreach (Drink drink in GameState.AllDrinks)
                {
                    writer.WriteStartElement("Drink");
                    writer.WriteElementString("Name", drink.Name);
                    writer.WriteElementString("Description", drink.Description);
                    writer.WriteElementString("RestoreHealth", drink.RestoreHealth.ToString());
                    writer.WriteElementString("RestoreMagic", drink.RestoreMagic.ToString());
                    writer.WriteElementString("Cures", drink.Cures.ToString());
                    writer.WriteElementString("Value", drink.ValueToString);
                    writer.WriteElementString("Weight", drink.Weight.ToString());
                    writer.WriteElementString("CanSell", drink.CanSell.ToString());
                    writer.WriteElementString("IsSold", drink.IsSold.ToString());

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        internal static void WriteAllFood()
        {
            using (XmlTextWriter writer = new XmlTextWriter("Data/Food/Food.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();

                writer.WriteStartElement("AllFood");
                foreach (Food food in GameState.AllFood)
                {
                    writer.WriteStartElement("Food");
                    writer.WriteElementString("Name", food.Name);
                    writer.WriteElementString("Description", food.Description);
                    writer.WriteElementString("RestoreHealth", food.RestoreHealth.ToString());
                    writer.WriteElementString("RestoreMagic", food.RestoreMagic.ToString());
                    writer.WriteElementString("Cures", food.Cures.ToString());
                    writer.WriteElementString("Value", food.ValueToString);
                    writer.WriteElementString("Weight", food.Weight.ToString());
                    writer.WriteElementString("CanSell", food.CanSell.ToString());
                    writer.WriteElementString("IsSold", food.IsSold.ToString());

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        internal static void WriteAllPotions()
        {
            using (XmlTextWriter writer = new XmlTextWriter("Data/Potions/Potions.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();

                writer.WriteStartElement("AllPotions");
                foreach (Potion potion in GameState.AllPotions)
                {
                    writer.WriteStartElement("Potion");
                    writer.WriteElementString("Name", potion.Name);
                    writer.WriteElementString("Description", potion.Description);
                    writer.WriteElementString("RestoreHealth", potion.RestoreHealth.ToString());
                    writer.WriteElementString("RestoreMagic", potion.RestoreMagic.ToString());
                    writer.WriteElementString("Cures", potion.Cures.ToString());
                    writer.WriteElementString("Value", potion.ValueToString);
                    writer.WriteElementString("Weight", potion.Weight.ToString());
                    writer.WriteElementString("CanSell", potion.CanSell.ToString());
                    writer.WriteElementString("IsSold", potion.IsSold.ToString());

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        #endregion Write Consumables

        #region Write Armor

        internal static void WriteAllHeadArmor()
        {
            using (XmlTextWriter writer = new XmlTextWriter("Data/Armor/HeadArmor.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();

                writer.WriteStartElement("AllHeadArmor");
                foreach (HeadArmor head in GameState.AllHeadArmor)
                {
                    writer.WriteStartElement("HeadArmor");
                    writer.WriteElementString("Name", head.Name);
                    writer.WriteElementString("Description", head.Description);
                    writer.WriteElementString("Defense", head.DefenseToString);
                    writer.WriteElementString("Durability", head.MaximumDurabilityToString);
                    writer.WriteElementString("MinimumLevel", head.MinimumLevel.ToString());
                    writer.WriteElementString("Value", head.ValueToString);
                    writer.WriteElementString("Weight", head.Weight.ToString());
                    writer.WriteElementString("CanSell", head.CanSell.ToString());
                    writer.WriteElementString("IsSold", head.IsSold.ToString());
                    writer.WriteElementString("AllowedClasses", head.AllowedClassesToString);

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        internal static void WriteAllBodyArmor()
        {
            using (XmlTextWriter writer = new XmlTextWriter("Data/Armor/BodyArmor.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();

                writer.WriteStartElement("AllBodyArmor");
                foreach (BodyArmor body in GameState.AllBodyArmor)
                {
                    writer.WriteStartElement("BodyArmor");
                    writer.WriteElementString("Name", body.Name);
                    writer.WriteElementString("Description", body.Description);
                    writer.WriteElementString("Defense", body.DefenseToString);
                    writer.WriteElementString("Durability", body.MaximumDurabilityToString);
                    writer.WriteElementString("MinimumLevel", body.MinimumLevel.ToString());
                    writer.WriteElementString("Value", body.ValueToString);
                    writer.WriteElementString("Weight", body.Weight.ToString());
                    writer.WriteElementString("CanSell", body.CanSell.ToString());
                    writer.WriteElementString("IsSold", body.IsSold.ToString());
                    writer.WriteElementString("AllowedClasses", body.AllowedClassesToString);

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        internal static void WriteAllHandArmor()
        {
            using (XmlTextWriter writer = new XmlTextWriter("Data/Armor/HandArmor.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();

                writer.WriteStartElement("AllHandArmor");
                foreach (HandArmor hand in GameState.AllHandArmor)
                {
                    writer.WriteStartElement("HandArmor");
                    writer.WriteElementString("Name", hand.Name);
                    writer.WriteElementString("Description", hand.Description);
                    writer.WriteElementString("Defense", hand.DefenseToString);
                    writer.WriteElementString("Durability", hand.MaximumDurabilityToString);
                    writer.WriteElementString("MinimumLevel", hand.MinimumLevel.ToString());
                    writer.WriteElementString("Value", hand.ValueToString);
                    writer.WriteElementString("Weight", hand.Weight.ToString());
                    writer.WriteElementString("CanSell", hand.CanSell.ToString());
                    writer.WriteElementString("IsSold", hand.IsSold.ToString());
                    writer.WriteElementString("AllowedClasses", hand.AllowedClassesToString);

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        internal static void WriteAllLegArmor()
        {
            using (XmlTextWriter writer = new XmlTextWriter("Data/Armor/LegArmor.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();

                writer.WriteStartElement("AllLegArmor");
                foreach (LegArmor legs in GameState.AllLegArmor)
                {
                    writer.WriteStartElement("LegArmor");
                    writer.WriteElementString("Name", legs.Name);
                    writer.WriteElementString("Description", legs.Description);
                    writer.WriteElementString("Defense", legs.DefenseToString);
                    writer.WriteElementString("Durability", legs.MaximumDurabilityToString);
                    writer.WriteElementString("MinimumLevel", legs.MinimumLevel.ToString());
                    writer.WriteElementString("Value", legs.ValueToString);
                    writer.WriteElementString("Weight", legs.Weight.ToString());
                    writer.WriteElementString("CanSell", legs.CanSell.ToString());
                    writer.WriteElementString("IsSold", legs.IsSold.ToString());
                    writer.WriteElementString("AllowedClasses", legs.AllowedClassesToString);

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        internal static void WriteAllFeetArmor()
        {
            using (XmlTextWriter writer = new XmlTextWriter("Data/Armor/FeetArmor.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();

                writer.WriteStartElement("AllFeetArmor");
                foreach (FeetArmor feet in GameState.AllFeetArmor)
                {
                    writer.WriteStartElement("FeetArmor");
                    writer.WriteElementString("Name", feet.Name);
                    writer.WriteElementString("Description", feet.Description);
                    writer.WriteElementString("Defense", feet.DefenseToString);
                    writer.WriteElementString("Durability", feet.MaximumDurabilityToString);
                    writer.WriteElementString("MinimumLevel", feet.MinimumLevel.ToString());
                    writer.WriteElementString("Value", feet.ValueToString);
                    writer.WriteElementString("Weight", feet.Weight.ToString());
                    writer.WriteElementString("CanSell", feet.CanSell.ToString());
                    writer.WriteElementString("IsSold", feet.IsSold.ToString());
                    writer.WriteElementString("AllowedClasses", feet.AllowedClassesToString);

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        internal static void WriteAllRings()
        {
            using (XmlTextWriter writer = new XmlTextWriter("Data/Rings/Rings.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();

                writer.WriteStartElement("AllRings");
                foreach (Ring ring in GameState.AllRings)
                {
                    writer.WriteStartElement("Ring");
                    writer.WriteElementString("Name", ring.Name);
                    writer.WriteElementString("Description", ring.Description);
                    writer.WriteElementString("Durability", ring.MaximumDurabilityToString);
                    writer.WriteElementString("MinimumLevel", ring.MinimumLevel.ToString());
                    writer.WriteElementString("Value", ring.ValueToString);
                    writer.WriteElementString("Weight", ring.Weight.ToString());
                    writer.WriteElementString("CanSell", ring.CanSell.ToString());
                    writer.WriteElementString("IsSold", ring.IsSold.ToString());
                    writer.WriteElementString("AllowedClasses", ring.AllowedClassesToString);
                    writer.WriteElementString("Damage", ring.DamageToString);
                    writer.WriteElementString("Defense", ring.DefenseToString);
                    writer.WriteElementString("Strength", ring.Strength.ToString());
                    writer.WriteElementString("Vitality", ring.Vitality.ToString());
                    writer.WriteElementString("Dexterity", ring.Dexterity.ToString());
                    writer.WriteElementString("Wisdom", ring.Wisdom.ToString());

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        #endregion Write Armor

        internal static void WriteAllWeapons()
        {
            using (XmlTextWriter writer = new XmlTextWriter("Data/Weapons/Weapons.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();

                writer.WriteStartElement("AllWeapons");
                foreach (Weapon weapon in GameState.AllWeapons)
                {
                    writer.WriteStartElement("Weapon");
                    writer.WriteElementString("Name", weapon.Name);
                    writer.WriteElementString("Description", weapon.Description);
                    writer.WriteElementString("WeaponType", weapon.WeaponTypeToString);
                    writer.WriteElementString("Damage", weapon.DamageToString);
                    writer.WriteElementString("Durability", weapon.MaximumDurabilityToString);
                    writer.WriteElementString("MinimumLevel", weapon.MinimumLevel.ToString());
                    writer.WriteElementString("Value", weapon.ValueToString);
                    writer.WriteElementString("Weight", weapon.Weight.ToString());
                    writer.WriteElementString("CanSell", weapon.CanSell.ToString());
                    writer.WriteElementString("IsSold", weapon.IsSold.ToString());
                    writer.WriteElementString("AllowedClasses", weapon.AllowedClassesToString);

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        internal static void WriteAllClasses()
        {
            using (XmlTextWriter writer = new XmlTextWriter("Data/Classes/Classes.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();

                writer.WriteStartElement("AllClasses");
                foreach (HeroClass heroClass in GameState.AllClasses)
                {
                    writer.WriteStartElement("Class");
                    writer.WriteElementString("Name", heroClass.Name);
                    writer.WriteElementString("Description", heroClass.Description);
                    writer.WriteElementString("SkillPoints", heroClass.SkillPoints.ToString());
                    writer.WriteElementString("Strength", heroClass.Strength.ToString());
                    writer.WriteElementString("Vitality", heroClass.Vitality.ToString());
                    writer.WriteElementString("Dexterity", heroClass.Dexterity.ToString());
                    writer.WriteElementString("Wisdom", heroClass.Wisdom.ToString());

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        internal static void WriteAllEnemies()
        {
            using (XmlTextWriter writer = new XmlTextWriter("Data/Enemies/Enemies.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();

                writer.WriteStartElement("AllEnemies");
                foreach (Enemy enemy in GameState.AllEnemies)
                {
                    writer.WriteStartElement("Enemy");

                    writer.WriteStartElement("Information");
                    writer.WriteElementString("Name", enemy.Name);
                    writer.WriteElementString("Level", enemy.Level.ToString());
                    writer.WriteElementString("Experience", enemy.Experience.ToString());
                    writer.WriteElementString("Type", enemy.Type);

                    writer.WriteEndElement();

                    writer.WriteStartElement("Inventory");
                    writer.WriteElementString("Gold", enemy.GoldToString);
                    writer.WriteEndElement();

                    writer.WriteStartElement("Attributes");
                    writer.WriteElementString("Strength", enemy.Attributes.Strength.ToString());
                    writer.WriteElementString("Vitality", enemy.Attributes.Vitality.ToString());
                    writer.WriteElementString("Dexterity", enemy.Attributes.Dexterity.ToString());
                    writer.WriteElementString("Wisdom", enemy.Attributes.Wisdom.ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Statistics");
                    writer.WriteElementString("Health", enemy.Statistics.MaximumHealth.ToString());
                    writer.WriteElementString("Magic", enemy.Statistics.MaximumMagic.ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Equipment");

                    writer.WriteElementString("HeadArmor", enemy.Equipment.Head.Name);
                    writer.WriteElementString("BodyArmor", enemy.Equipment.Body.Name);
                    writer.WriteElementString("HandArmor", enemy.Equipment.Hands.Name);
                    writer.WriteElementString("LegArmor", enemy.Equipment.Legs.Name);
                    writer.WriteElementString("FeetArmor", enemy.Equipment.Feet.Name);
                    writer.WriteElementString("LeftRing", enemy.Equipment.LeftRing.Name);
                    writer.WriteElementString("RightRing", enemy.Equipment.RightRing.Name);
                    writer.WriteElementString("Weapon", enemy.Equipment.Weapon.Name);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        internal static void WriteAllHeroes()
        {
            using (XmlTextWriter writer = new XmlTextWriter("Data/Heroes/Heroes.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();

                writer.WriteStartElement("AllHeroes");
                foreach (Hero hero in GameState.AllHeroes)
                {
                    writer.WriteStartElement("Hero");

                    writer.WriteStartElement("Information");
                    writer.WriteElementString("Name", hero.Name);
                    writer.WriteElementString("Class", hero.Class.Name);
                    writer.WriteElementString("Level", hero.Level.ToString());
                    writer.WriteElementString("Experience", hero.Experience.ToString());
                    writer.WriteElementString("SkillPoints", hero.SkillPoints.ToString());
                    writer.WriteElementString("Hardcore", hero.Hardcore.ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Inventory");
                    writer.WriteElementString("Gold", hero.GoldToString);
                    foreach (Item item in hero.Inventory)
                    {
                        writer.WriteStartElement("Item");
                        writer.WriteElementString("Name", item.Name);
                        writer.WriteElementString("Durability", item.CurrentDurabilityToString);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("Attributes");
                    writer.WriteElementString("Strength", hero.Attributes.Strength.ToString());
                    writer.WriteElementString("Vitality", hero.Attributes.Vitality.ToString());
                    writer.WriteElementString("Dexterity", hero.Attributes.Dexterity.ToString());
                    writer.WriteElementString("Wisdom", hero.Attributes.Wisdom.ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Statistics");
                    writer.WriteElementString("CurrentHealth", hero.Statistics.CurrentHealth.ToString());
                    writer.WriteElementString("MaximumHealth", hero.Statistics.MaximumHealth.ToString());
                    writer.WriteElementString("CurrentMagic", hero.Statistics.CurrentMagic.ToString());
                    writer.WriteElementString("MaximumMagic", hero.Statistics.MaximumMagic.ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Equipment");

                    writer.WriteStartElement("HeadArmor");
                    writer.WriteElementString("Name", hero.Equipment.Head.Name);
                    writer.WriteElementString("Durability", hero.Equipment.Head.CurrentDurabilityToString);
                    writer.WriteEndElement();

                    writer.WriteStartElement("BodyArmor");
                    writer.WriteElementString("Name", hero.Equipment.Body.Name);
                    writer.WriteElementString("Durability", hero.Equipment.Body.CurrentDurabilityToString);
                    writer.WriteEndElement();

                    writer.WriteStartElement("HandArmor");
                    writer.WriteElementString("Name", hero.Equipment.Hands.Name);
                    writer.WriteElementString("Durability", hero.Equipment.Hands.CurrentDurabilityToString);
                    writer.WriteEndElement();

                    writer.WriteStartElement("LegArmor");
                    writer.WriteElementString("Name", hero.Equipment.Legs.Name);
                    writer.WriteElementString("Durability", hero.Equipment.Legs.CurrentDurabilityToString);
                    writer.WriteEndElement();

                    writer.WriteStartElement("FeetArmor");
                    writer.WriteElementString("Name", hero.Equipment.Feet.Name);
                    writer.WriteElementString("Durability", hero.Equipment.Feet.CurrentDurabilityToString);
                    writer.WriteEndElement();

                    writer.WriteStartElement("LeftRing");
                    writer.WriteElementString("Name", hero.Equipment.LeftRing.Name);
                    writer.WriteElementString("Durability", hero.Equipment.LeftRing.CurrentDurabilityToString);
                    writer.WriteEndElement();

                    writer.WriteStartElement("RightRing");
                    writer.WriteElementString("Name", hero.Equipment.RightRing.Name);
                    writer.WriteElementString("Durability", hero.Equipment.RightRing.CurrentDurabilityToString);
                    writer.WriteEndElement();

                    writer.WriteStartElement("Weapon");
                    writer.WriteElementString("Name", hero.Equipment.Weapon.Name);
                    writer.WriteElementString("Durability", hero.Equipment.Weapon.CurrentDurabilityToString);
                    writer.WriteEndElement();

                    writer.WriteEndElement(); //finish Equipment

                    writer.WriteStartElement("Spellbook");
                    foreach (Spell spell in hero.Spellbook.Spells)
                    {
                        writer.WriteElementString("Name", spell.Name);
                    }
                    writer.WriteEndElement();

                    writer.WriteEndElement(); //finish Hero
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        private static void WriteAllSpells()
        {
            using (XmlTextWriter writer = new XmlTextWriter("Data/Spells/Spells.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();

                writer.WriteStartElement("AllSpells");
                foreach (Spell spell in GameState.AllSpells)
                {
                    writer.WriteStartElement("Spell");
                    writer.WriteElementString("Name", spell.Name);
                    writer.WriteElementString("Description", spell.Description);
                    writer.WriteElementString("RequiredLevel", spell.RequiredLevel.ToString());
                    writer.WriteElementString("MagicCost", spell.MagicCost.ToString());
                    writer.WriteElementString("Amount", spell.Amount.ToString());
                    writer.WriteElementString("Type", spell.TypeToString);
                    writer.WriteElementString("AllowedClasses", spell.AllowedClassesToString);

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        #endregion Write

        //#region Read

        //private static void ReadSingleTest()
        //{
        //    Character newCharacter = new Character();
        //    XmlDocument xmlDoc = new XmlDocument();
        //    if (File.Exists("Aragon.xml"))
        //    {
        //        xmlDoc.Load("Aragon.xml");
        //        XmlNode xn = xmlDoc.SelectSingleNode("/Character/Information");
        //        if (xn != null)
        //        {
        //            newCharacter.Name = xn["Name"]?.InnerText;
        //            newCharacter.Level = Int32Helper.Parse(xn["Level"]?.InnerText);
        //            newCharacter.Experience = Int32Helper.Parse(xn["Experience"]?.InnerText);
        //            newCharacter.Gold = Int32Helper.Parse(xn["Gold"]?.InnerText);
        //        }
        //        xn = xmlDoc.SelectSingleNode("/Character/Attributes");
        //        if (xn != null)
        //        {
        //            newCharacter.Strength = Int32Helper.Parse(xn["Strength"]?.InnerText);
        //            newCharacter.Vitality = Int32Helper.Parse(xn["Vitality"]?.InnerText);
        //            newCharacter.Dexterity = Int32Helper.Parse(xn["Dexterity"]?.InnerText);
        //            newCharacter.Wisdom = Int32Helper.Parse(xn["Wisdom"]?.InnerText);
        //        };
        //        xn = xmlDoc.SelectSingleNode("/Character/Inventory");
        //        if (xn != null)
        //        {
        //            XmlNodeList xnList = xmlDoc.SelectNodes("/Character/Inventory/Item");
        //            foreach (XmlNode xmlNode in xnList)
        //            {
        //                Item currentItem = new Item(AllItems.Find(itm => itm.Name == xmlNode["Name"]?.InnerText));
        //                currentItem.Durability = 69;
        //                newCharacter.Inventory.Add(currentItem);
        //            }
        //        }
        //    } // end check file

        //    if (newCharacter == new Character())
        //        Console.WriteLine("Character doesn't exist!");
        //    else
        //        Console.WriteLine($"{newCharacter.Name}: {newCharacter.Level}");
        //    Console.ReadKey();
        //}

        private static void ReadMultipleTest()
        {
        }

        //#endregion Read

        //#region Write

        //private static void WriteSingleTest()
        //{
        //    Character newCharacter = new Character("Aragon", 12, 110, 612, 15, 12, 13, 22,
        //        new List<Item>
        //    {
        //        new Item("Short Sword", "A short sword.", 100),
        //        new Item("Cloth Shirt", "A cloth shirt.", 100)
        //    });

        //    using (XmlTextWriter writer = new XmlTextWriter("Aragon.xml", Encoding.UTF8))
        //    {
        //        writer.Formatting = Formatting.Indented;
        //        writer.Indentation = 4;

        //        writer.WriteStartDocument();

        //        writer.WriteStartElement("Character");
        //        writer.WriteStartElement("Information");
        //        writer.WriteElementString("Name", newCharacter.Name);
        //        writer.WriteElementString("Level", newCharacter.Level.ToString());
        //        writer.WriteElementString("Experience", newCharacter.Experience.ToString());
        //        writer.WriteElementString("Gold", newCharacter.Gold.ToString());
        //        writer.WriteEndElement();

        //        writer.WriteStartElement("Attributes");
        //        writer.WriteElementString("Strength", newCharacter.Strength.ToString());
        //        writer.WriteElementString("Vitality", newCharacter.Vitality.ToString());
        //        writer.WriteElementString("Dexterity", newCharacter.Dexterity.ToString());
        //        writer.WriteElementString("Wisdom", newCharacter.Wisdom.ToString());
        //        writer.WriteEndElement();

        //        if (newCharacter.Inventory.Count > 0)
        //        {
        //            writer.WriteStartElement("Inventory");
        //            foreach (Item item in newCharacter.Inventory)
        //            {
        //                writer.WriteStartElement("Item");
        //                writer.WriteElementString("Name", item.Name);
        //                writer.WriteElementString("Durability", item.Durability.ToString());
        //                writer.WriteEndElement();
        //            }
        //            writer.WriteEndElement();
        //        }
        //        writer.WriteEndElement();
        //        writer.WriteEndDocument();
        //    }
        //}

        //private static void WriteMultipleTest()
        //{
        //    Character[] characters = new Character[2];
        //    //characters[0] = new Character("Aragon", 12, 15, 12, 13, 22);
        //    //characters[1] = new Character("Null", 1, 10, 10, 10, 10);
        //    foreach (Character character in characters)
        //    {
        //        using (XmlTextWriter writer = new XmlTextWriter($"{character.Name}.xml", Encoding.UTF8))
        //        {
        //            writer.Formatting = Formatting.Indented;
        //            writer.Indentation = 4;

        //            writer.WriteStartDocument();

        //            writer.WriteStartElement("Character");
        //            writer.WriteElementString("Name", character.Name);
        //            writer.WriteElementString("Level", character.Level.ToString());
        //            writer.WriteElementString("Strength", character.Strength.ToString());
        //            writer.WriteElementString("Vitality", character.Vitality.ToString());
        //            writer.WriteElementString("Dexterity", character.Dexterity.ToString());
        //            writer.WriteElementString("Wisdom", character.Wisdom.ToString());

        //            writer.WriteEndElement();
        //            writer.WriteEndDocument();
        //        }
        //    }
        //}

        //#endregion Write
    }
}