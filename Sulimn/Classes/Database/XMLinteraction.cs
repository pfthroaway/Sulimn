using Extensions.DataTypeHelpers;
using Sulimn.Classes.Entities;
using Sulimn.Classes.Enums;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Sulimn.Classes.Database
{
    /// <summary>Represents XML file interactions.</summary>
    internal static class XMLInteraction
    {
        #region Load

        /// <summary>Loads the game's settings from disk.</summary>
        /// <returns></returns>
        internal static Settings LoadSettings()
        {
            Settings newSettings = new Settings("", "");
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("Data/Settings.xml"))
            {
                try
                {
                    xmlDoc.Load("Data/Settings.xml");
                    foreach (XmlNode xn in xmlDoc.SelectNodes("/Settings"))
                    {
                        newSettings.AdminPassword = xn["AdminPassword"]?.InnerText;
                        newSettings.Theme = xn["Theme"]?.InnerText;
                    }
                }
                catch (Exception e)
                {
                    GameState.DisplayNotification($"Error loading settings: {e.Message}.", "Sulimn");
                }
            }
            return newSettings;
        }

        //TODO Change all Items to implement durability, allowed classes, and minimum level
        //TODO Figure out a way to always display current health and magic along the bottom of the screen.

        /// <summary>Loads all Classes from the database.</summary>
        /// <returns>List of Classes</returns>
        internal static List<HeroClass> LoadClasses()
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
                        name: xn["Name"]?.InnerText,
                        description: xn["Description"]?.InnerText,
                        skillPoints: Int32Helper.Parse(xn["SkillPoints"]?.InnerText),
                        strength: Int32Helper.Parse(xn["Strength"]?.InnerText),
                        vitality: Int32Helper.Parse(xn["Vitality"]?.InnerText),
                        dexterity: Int32Helper.Parse(xn["Dexterity"]?.InnerText),
                        wisdom: Int32Helper.Parse(xn["Wisdom"]?.InnerText));

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
        internal static List<Enemy> LoadEnemies()
        {
            List<Enemy> allEnemies = new List<Enemy>();
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists("Data/Enemies/Enemies.xml"))
            {
                try
                {
                    xmlDoc.Load("Data/Enemies/Enemies.xml");
                    foreach (XmlNode node in xmlDoc.SelectNodes("/AllEnemies/Enemy"))
                    {
                        Enemy newEnemy = new Enemy();

                        XmlNode info = node.SelectSingleNode(".//Information");
                        if (info != null)
                        {
                            newEnemy.Name = info["Name"]?.InnerText;
                            newEnemy.Level = Int32Helper.Parse(info["Level"]?.InnerText);
                            newEnemy.Experience = Int32Helper.Parse(info["Experience"]?.InnerText);
                            newEnemy.Type = info["Type"]?.InnerText;
                        }
                        else
                            GameState.DisplayNotification($"Information unavailable for Enemy.", "Sulimn");

                        XmlNode inventory = node.SelectSingleNode(".//Inventory");
                        if (inventory != null)
                            newEnemy.Gold = Int32Helper.Parse(inventory["Gold"]?.InnerText);
                        else
                            GameState.DisplayNotification($"Gold unavailable for Enemy {newEnemy.Name}.", "Sulimn");

                        XmlNode attributes = node.SelectSingleNode(".//Attributes");
                        if (attributes != null)
                            newEnemy.Attributes = new Attributes(Int32Helper.Parse(attributes["Strength"]?.InnerText), Int32Helper.Parse(attributes["Vitality"]?.InnerText), Int32Helper.Parse(attributes["Dexterity"]?.InnerText), Int32Helper.Parse(attributes["Wisdom"]?.InnerText));
                        else
                            GameState.DisplayNotification($"Attributes unavailable for Enemy {newEnemy.Name}.", "Sulimn");

                        XmlNode statistics = node.SelectSingleNode(".//Statistics");
                        if (statistics != null)
                            newEnemy.Statistics = new Statistics(Int32Helper.Parse(statistics["Health"]?.InnerText), Int32Helper.Parse(statistics["Health"]?.InnerText), Int32Helper.Parse(statistics["Magic"]?.InnerText), Int32Helper.Parse(statistics["Magic"]?.InnerText));
                        else
                            GameState.DisplayNotification($"Statistics unavailable for Enemy {newEnemy.Name}.", "Sulimn");

                        XmlNode equipment = node.SelectSingleNode(".//Equipment");
                        if (equipment != null)
                        {
                            newEnemy.Equipment = new Equipment();

                            newEnemy.Equipment.Head = !string.IsNullOrWhiteSpace(equipment["HeadArmor"]?.InnerText) ? new HeadArmor(GameState.AllHeadArmor.Find(armr => armr.Name == equipment["HeadArmor"]?.InnerText)) : new HeadArmor();
                            newEnemy.Equipment.Body = !string.IsNullOrWhiteSpace(equipment["BodyArmor"]?.InnerText) ? new BodyArmor(GameState.AllBodyArmor.Find(armr => armr.Name == equipment["BodyArmor"]?.InnerText)) : new BodyArmor();
                            newEnemy.Equipment.Hands = !string.IsNullOrWhiteSpace(equipment["HandArmor"]?.InnerText) ? new HandArmor(GameState.AllHandArmor.Find(armr => armr.Name == equipment["HandArmor"]?.InnerText)) : new HandArmor();
                            newEnemy.Equipment.Legs = !string.IsNullOrWhiteSpace(equipment["LegArmor"]?.InnerText) ? new LegArmor(GameState.AllLegArmor.Find(armr => armr.Name == equipment["LegArmor"]?.InnerText)) : new LegArmor();
                            newEnemy.Equipment.Feet = !string.IsNullOrWhiteSpace(equipment["FeetArmor"]?.InnerText) ? new FeetArmor(GameState.AllFeetArmor.Find(armr => armr.Name == equipment["FeetArmor"]?.InnerText)) : new FeetArmor();
                            newEnemy.Equipment.LeftRing = !string.IsNullOrWhiteSpace(equipment["LeftRing"]?.InnerText) ? new Ring(GameState.AllRings.Find(ring => ring.Name == equipment["LeftRing"]?.InnerText)) : new Ring();
                            newEnemy.Equipment.RightRing = !string.IsNullOrWhiteSpace(equipment["RightRing"]?.InnerText) ? new Ring(GameState.AllRings.Find(ring => ring.Name == equipment["RightRing"]?.InnerText)) : new Ring();
                            newEnemy.Equipment.Weapon = !string.IsNullOrWhiteSpace(equipment["Weapon"]?.InnerText) ? new Weapon(GameState.AllWeapons.Find(wpn => wpn.Name == equipment["Weapon"]?.InnerText)) : new Weapon();
                        }
                        else
                            GameState.DisplayNotification($"Equipment unavailable for Enemy {newEnemy.Name}.", "Sulimn");

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

        #endregion Entities

        #region Items

        #region Equipment

        #region Armor

        /// <summary>Loads all Head Armor from the database.</summary>
        /// <returns>List of Head Armor</returns>
        internal static List<HeadArmor> LoadHeadArmor()
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
                        xn["Name"]?.InnerText,
                        xn["Description"]?.InnerText,
                        Int32Helper.Parse(xn["Defense"]?.InnerText),
                        Int32Helper.Parse(xn["Weight"]?.InnerText),
                        Int32Helper.Parse(xn["Value"]?.InnerText),
                        BoolHelper.Parse(xn["CanSell"]?.InnerText),
                        BoolHelper.Parse(xn["IsSold"]?.InnerText));

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
        internal static List<BodyArmor> LoadBodyArmor()
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
                        xn["Name"]?.InnerText,
                        xn["Description"]?.InnerText,
                        Int32Helper.Parse(xn["Defense"]?.InnerText),
                        Int32Helper.Parse(xn["Weight"]?.InnerText),
                        Int32Helper.Parse(xn["Value"]?.InnerText),
                        BoolHelper.Parse(xn["CanSell"]?.InnerText),
                        BoolHelper.Parse(xn["IsSold"]?.InnerText));

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
        internal static List<HandArmor> LoadHandArmor()
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
                        xn["Name"]?.InnerText,
                        xn["Description"]?.InnerText,
                        Int32Helper.Parse(xn["Defense"]?.InnerText),
                        Int32Helper.Parse(xn["Weight"]?.InnerText),
                        Int32Helper.Parse(xn["Value"]?.InnerText),
                        BoolHelper.Parse(xn["CanSell"]?.InnerText),
                        BoolHelper.Parse(xn["IsSold"]?.InnerText));

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
        internal static List<LegArmor> LoadLegArmor()
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
                        xn["Name"]?.InnerText,
                        xn["Description"]?.InnerText,
                        Int32Helper.Parse(xn["Defense"]?.InnerText),
                        Int32Helper.Parse(xn["Weight"]?.InnerText),
                        Int32Helper.Parse(xn["Value"]?.InnerText),
                        BoolHelper.Parse(xn["CanSell"]?.InnerText),
                        BoolHelper.Parse(xn["IsSold"]?.InnerText));

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
        internal static List<FeetArmor> LoadFeetArmor()
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
                        xn["Name"]?.InnerText,
                        xn["Description"]?.InnerText,
                        Int32Helper.Parse(xn["Defense"]?.InnerText),
                        Int32Helper.Parse(xn["Weight"]?.InnerText),
                        Int32Helper.Parse(xn["Value"]?.InnerText),
                        BoolHelper.Parse(xn["CanSell"]?.InnerText),
                        BoolHelper.Parse(xn["IsSold"]?.InnerText));

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
        internal static List<Ring> LoadRings()
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
                        xn["Name"]?.InnerText,
                        xn["Description"]?.InnerText,
                        Int32Helper.Parse(xn["Damage"]?.InnerText),
                        Int32Helper.Parse(xn["Defense"]?.InnerText),
                        Int32Helper.Parse(xn["Strength"]?.InnerText),
                        Int32Helper.Parse(xn["Vitality"]?.InnerText),
                        Int32Helper.Parse(xn["Dexterity"]?.InnerText),
                        Int32Helper.Parse(xn["Wisdom"]?.InnerText),
                        Int32Helper.Parse(xn["Weight"]?.InnerText),
                        Int32Helper.Parse(xn["Value"]?.InnerText),
                        BoolHelper.Parse(xn["CanSell"]?.InnerText),
                        BoolHelper.Parse(xn["IsSold"]?.InnerText));

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
        internal static List<Weapon> LoadWeapons()
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
                        xn["Name"]?.InnerText,
                        EnumHelper.Parse<WeaponTypes>(xn["WeaponType"]?.InnerText),
                        xn["Description"]?.InnerText,
                        Int32Helper.Parse(xn["Damage"]?.InnerText),
                        Int32Helper.Parse(xn["Weight"]?.InnerText),
                        Int32Helper.Parse(xn["Value"]?.InnerText),
                        BoolHelper.Parse(xn["CanSell"]?.InnerText),
                        BoolHelper.Parse(xn["IsSold"]?.InnerText));

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
        internal static List<Potion> LoadPotions()
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
                        xn["Name"]?.InnerText,
                        xn["Description"]?.InnerText,
                        Int32Helper.Parse(xn["RestoreHealth"]?.InnerText),
                        Int32Helper.Parse(xn["RestoreMagic"]?.InnerText),
                        BoolHelper.Parse(xn["Cures"]?.InnerText),
                        Int32Helper.Parse(xn["Weight"]?.InnerText),
                        Int32Helper.Parse(xn["Value"]?.InnerText),
                        BoolHelper.Parse(xn["CanSell"]?.InnerText),
                        BoolHelper.Parse(xn["IsSold"]?.InnerText));

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
        internal static List<Drink> LoadDrinks()
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
                        xn["Name"]?.InnerText,
                        xn["Description"]?.InnerText,
                        Int32Helper.Parse(xn["RestoreHealth"]?.InnerText),
                        Int32Helper.Parse(xn["RestoreMagic"]?.InnerText),
                        BoolHelper.Parse(xn["Cures"]?.InnerText),
                        Int32Helper.Parse(xn["Weight"]?.InnerText),
                        Int32Helper.Parse(xn["Value"]?.InnerText),
                        BoolHelper.Parse(xn["CanSell"]?.InnerText),
                        BoolHelper.Parse(xn["IsSold"]?.InnerText));

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
        internal static List<Food> LoadFood()
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
                        xn["Name"]?.InnerText,
                        xn["Description"]?.InnerText,
                        Int32Helper.Parse(xn["RestoreHealth"]?.InnerText),
                        Int32Helper.Parse(xn["RestoreMagic"]?.InnerText),
                        BoolHelper.Parse(xn["Cures"]?.InnerText),
                        Int32Helper.Parse(xn["Weight"]?.InnerText),
                        Int32Helper.Parse(xn["Value"]?.InnerText),
                        BoolHelper.Parse(xn["CanSell"]?.InnerText),
                        BoolHelper.Parse(xn["IsSold"]?.InnerText));

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
        internal static List<Spell> LoadSpells()
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
                        xn["Name"]?.InnerText,
                        EnumHelper.Parse<SpellTypes>(xn["Type"]?.InnerText),
                        xn["Description"]?.InnerText,
                        GameState.SetAllowedClasses(xn["AllowedClasses"]?.InnerText),
                        Int32Helper.Parse(xn["RequiredLevel"]?.InnerText),
                        Int32Helper.Parse(xn["MagicCost"]?.InnerText),
                        Int32Helper.Parse(xn["Amount"]?.InnerText));

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

        #region Heroes

        /// <summary>Deletes a <see cref="Hero"/> from disk.</summary>
        /// <param name="deleteHero"><see cref="Hero"/> to be deleted</param>
        /// <returns>Returns true if the file is deleted.</returns>
        internal static bool DeleteHero(Hero deleteHero)
        {
            File.Delete($"Data/Heroes/{deleteHero.Name}.xml");
            return !File.Exists($"Data/Heroes/{deleteHero.Name}.xml");
        }

        /// <summary>Loads all Heroes from the database.</summary>
        /// <returns>List of Heroes</returns>
        internal static List<Hero> LoadHeroes()
        {
            //TODO Learn LINQ to XML
            //TODO Make sure all nodes are verified as existing before trying to pull data

            List<Hero> allHeroes = new List<Hero>();
            XmlDocument xmlDoc = new XmlDocument();

            if (Directory.Exists("Data/Heroes"))
            {
                foreach (string file in Directory.GetFiles("Data/Heroes"))
                {
                    try
                    {
                        xmlDoc.Load(file);
                        foreach (XmlNode node in xmlDoc.SelectNodes("/Hero"))
                        {
                            allHeroes.Add(LoadHero(node));
                        }
                    }
                    catch (Exception e)
                    {
                        GameState.DisplayNotification($"Error loading heroes: {e.Message}.", "Sulimn");
                    }
                }
            }

            return allHeroes;
        }

        /// <summary>Loads a <see cref="Hero"/> from an <see cref="XmlNode"/>.</summary>
        /// <param name="node"><see cref="XmlNode"/> to load the <see cref="Hero"/> from.</param>
        /// <returns><see cref="Hero"/></returns>
        internal static Hero LoadHero(XmlNode node)
        {
            Hero newHero = new Hero();

            XmlNode info = node.SelectSingleNode(".//Information");
            if (info != null)
            {
                newHero.Name = info["Name"]?.InnerText;
                newHero.Password = info["Password"]?.InnerText;
                newHero.Class = GameState.AllClasses.Find(cls => cls.Name == info["Class"]?.InnerText);
                newHero.Level = Int32Helper.Parse(info["Level"]?.InnerText);
                newHero.Experience = Int32Helper.Parse(info["Experience"]?.InnerText);
                newHero.SkillPoints = Int32Helper.Parse(info["SkillPoints"]?.InnerText);
                newHero.Hardcore = BoolHelper.Parse(info["Hardcore"]?.InnerText);
            }
            else
                GameState.DisplayNotification($"Information unavailable for Hero.", "Sulimn");

            XmlNode inventory = node.SelectSingleNode(".//Inventory");
            if (inventory != null)
            {
                newHero.Gold = Int32Helper.Parse(inventory["Gold"]?.InnerText);
                if (inventory.SelectSingleNode("Item") != null)
                {
                    foreach (XmlNode item in inventory.SelectNodes(".//Item"))
                    {
                        List<Item> items = new List<Item>();
                        items.Add(GameState.AllItems.Find(itm => itm.Name == item["Name"]?.InnerText));

                        if (items[0].GetType() == typeof(HeadArmor))
                        {
                            HeadArmor itm = new HeadArmor((HeadArmor)items[0]);
                            itm.CurrentDurability = Int32Helper.Parse(item["Durability"]);
                            newHero.AddItem(itm);
                        }
                        else if (items[0].GetType() == typeof(BodyArmor))
                        {
                            BodyArmor itm = new BodyArmor((BodyArmor)items[0]);
                            itm.CurrentDurability = Int32Helper.Parse(item["Durability"]);
                            newHero.AddItem(itm);
                        }
                        else if (items[0].GetType() == typeof(HandArmor))
                        {
                            HandArmor itm = new HandArmor((HandArmor)items[0]);
                            itm.CurrentDurability = Int32Helper.Parse(item["Durability"]);
                            newHero.AddItem(itm);
                        }
                        else if (items[0].GetType() == typeof(LegArmor))
                        {
                            LegArmor itm = new LegArmor((LegArmor)items[0]);
                            itm.CurrentDurability = Int32Helper.Parse(item["Durability"]);
                            newHero.AddItem(itm);
                        }
                        else if (items[0].GetType() == typeof(FeetArmor))
                        {
                            FeetArmor itm = new FeetArmor((FeetArmor)items[0]);
                            itm.CurrentDurability = Int32Helper.Parse(item["Durability"]);
                            newHero.AddItem(itm);
                        }
                        else if (items[0].GetType() == typeof(Ring))
                        {
                            Ring itm = new Ring((Ring)items[0]);
                            itm.CurrentDurability = Int32Helper.Parse(item["Durability"]);
                            newHero.AddItem(itm);
                        }
                        else if (items[0].GetType() == typeof(Weapon))
                        {
                            Weapon itm = new Weapon((Weapon)items[0]);
                            itm.CurrentDurability = Int32Helper.Parse(item["Durability"]);
                            newHero.AddItem(itm);
                        }
                        else if (items[0].GetType() == typeof(Potion))
                        {
                            newHero.AddItem(items[0]);
                        }
                        else if (items[0].GetType() == typeof(Drink))
                        {
                            newHero.AddItem(items[0]);
                        }
                        else if (items[0].GetType() == typeof(Food))
                        {
                            newHero.AddItem(items[0]);
                        }
                    }
                }
            }
            else
                GameState.DisplayNotification($"Inventory unavailable for Hero {newHero.Name}.", "Sulimn");

            XmlNode bank = node.SelectSingleNode(".//Bank");
            if (bank != null)
                newHero.Bank = new Bank(Int32Helper.Parse(bank["GoldInBank"]?.InnerText), Int32Helper.Parse(bank["LoanTaken"]?.InnerText), Int32Helper.Parse(bank["LoanAvailable"]?.InnerText));
            else
                GameState.DisplayNotification($"Bank unavailable for Hero {newHero.Name}.", "Sulimn");

            XmlNode attributes = node.SelectSingleNode(".//Attributes");
            if (attributes != null)
                newHero.Attributes = new Attributes(Int32Helper.Parse(attributes["Strength"]?.InnerText), Int32Helper.Parse(attributes["Vitality"]?.InnerText), Int32Helper.Parse(attributes["Dexterity"]?.InnerText), Int32Helper.Parse(attributes["Wisdom"]?.InnerText));
            else
                GameState.DisplayNotification($"Attributes unavailable for Hero {newHero.Name}.", "Sulimn");

            XmlNode statistics = node.SelectSingleNode(".//Statistics");
            if (statistics != null)
                newHero.Statistics = new Statistics(Int32Helper.Parse(statistics["CurrentHealth"]?.InnerText), Int32Helper.Parse(statistics["MaximumHealth"]?.InnerText), Int32Helper.Parse(statistics["CurrentMagic"]?.InnerText), Int32Helper.Parse(statistics["MaximumMagic"]?.InnerText));
            else
                GameState.DisplayNotification($"Statistics unavailable for Hero {newHero.Name}.", "Sulimn");

            XmlNode equipment = node.SelectSingleNode(".//Equipment");
            if (equipment != null)
            {
                newHero.Equipment = new Equipment();

                XmlNode head = equipment.SelectSingleNode(".//HeadArmor");
                newHero.Equipment.Head = head != null && !string.IsNullOrWhiteSpace(head["Name"]?.InnerText) ? new HeadArmor(GameState.AllHeadArmor.Find(armr => armr.Name == head["Name"]?.InnerText)) : new HeadArmor();
                newHero.Equipment.Head.CurrentDurability = Int32Helper.Parse(head["Durability"]);

                XmlNode body = equipment.SelectSingleNode(".//BodyArmor");
                newHero.Equipment.Body = body != null && !string.IsNullOrWhiteSpace(body["Name"]?.InnerText) ? new BodyArmor(GameState.AllBodyArmor.Find(armr => armr.Name == body["Name"]?.InnerText)) : new BodyArmor();
                newHero.Equipment.Body.CurrentDurability = Int32Helper.Parse(body["Durability"]);

                XmlNode hands = equipment.SelectSingleNode(".//HandArmor");
                newHero.Equipment.Hands = hands != null && !string.IsNullOrWhiteSpace(hands["Name"]?.InnerText) ? new HandArmor(GameState.AllHandArmor.Find(armr => armr.Name == hands["Name"]?.InnerText)) : new HandArmor();
                newHero.Equipment.Hands.CurrentDurability = Int32Helper.Parse(hands["Durability"]);

                XmlNode legs = equipment.SelectSingleNode(".//LegArmor");
                newHero.Equipment.Legs = legs != null && !string.IsNullOrWhiteSpace(legs["Name"]?.InnerText) ? new LegArmor(GameState.AllLegArmor.Find(armr => armr.Name == legs["Name"]?.InnerText)) : new LegArmor();
                newHero.Equipment.Legs.CurrentDurability = Int32Helper.Parse(legs["Durability"]);

                XmlNode feet = equipment.SelectSingleNode(".//FeetArmor");
                newHero.Equipment.Feet = feet != null && !string.IsNullOrWhiteSpace(feet["Name"]?.InnerText) ? new FeetArmor(GameState.AllFeetArmor.Find(armr => armr.Name == feet["Name"]?.InnerText)) : new FeetArmor();
                newHero.Equipment.Feet.CurrentDurability = Int32Helper.Parse(feet["Durability"]);

                XmlNode leftRing = equipment.SelectSingleNode(".//LeftRing");
                newHero.Equipment.LeftRing = leftRing != null && !string.IsNullOrWhiteSpace(leftRing["Name"]?.InnerText) ? new Ring(GameState.AllRings.Find(ring => ring.Name == leftRing["Name"]?.InnerText)) : new Ring();
                newHero.Equipment.LeftRing.CurrentDurability = Int32Helper.Parse(leftRing["Durability"]);

                XmlNode rightRing = equipment.SelectSingleNode(".//RightRing");
                newHero.Equipment.RightRing = rightRing != null && !string.IsNullOrWhiteSpace(rightRing["Name"]?.InnerText) ? new Ring(GameState.AllRings.Find(ring => ring.Name == rightRing["Name"]?.InnerText)) : new Ring();
                newHero.Equipment.RightRing.CurrentDurability = Int32Helper.Parse(rightRing["Durability"]);

                XmlNode weapon = equipment.SelectSingleNode(".//Weapon");
                newHero.Equipment.Weapon = weapon != null && !string.IsNullOrWhiteSpace(weapon["Name"]?.InnerText) ? new Weapon(GameState.AllWeapons.Find(wpn => wpn.Name == weapon["Name"]?.InnerText)) : new Weapon();
                newHero.Equipment.Weapon.CurrentDurability = Int32Helper.Parse(weapon["Durability"]);
            }
            else
                GameState.DisplayNotification($"Equipment unavailable for Hero {newHero.Name}.", "Sulimn");

            XmlNode spellbook = node.SelectSingleNode(".//Spellbook");
            if (spellbook != null)
            {
                foreach (XmlNode spell in spellbook)
                    newHero.Spellbook.LearnSpell(GameState.AllSpells.Find(spl => spl.Name == spell?.InnerText));
            }

            newHero.Progression = new Progression();
            XmlNode progression = node.SelectSingleNode(".//Progression");
            if (progression != null)
            {
                newHero.Progression.Fields = BoolHelper.Parse(progression["Fields"]?.InnerText);
                newHero.Progression.Forest = BoolHelper.Parse(progression["Forest"]?.InnerText);
                newHero.Progression.Cathedral = BoolHelper.Parse(progression["Cathedral"]?.InnerText);
                newHero.Progression.Mines = BoolHelper.Parse(progression["Mines"]?.InnerText);
                newHero.Progression.Catacombs = BoolHelper.Parse(progression["Catacombs"]?.InnerText);
                newHero.Progression.Courtyard = BoolHelper.Parse(progression["Courtyard"]?.InnerText);
                newHero.Progression.Battlements = BoolHelper.Parse(progression["Battlements"]?.InnerText);
                newHero.Progression.Armoury = BoolHelper.Parse(progression["Armoury"]?.InnerText);
                newHero.Progression.Spire = BoolHelper.Parse(progression["Spire"]?.InnerText);
                newHero.Progression.ThroneRoom = BoolHelper.Parse(progression["ThroneRoom"]?.InnerText);
            }

            return newHero;
        }

        /// <summary>Saves a <see cref="Hero"/> to file.</summary>
        /// <param name="saveHero"><see cref="Hero"/> to be saved.</param>
        internal static void SaveHero(Hero saveHero)
        {
            using (XmlTextWriter writer = new XmlTextWriter($"Data/Heroes/{saveHero.Name}.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();
                WriteHero(writer, saveHero);
                writer.WriteEndDocument();
            }
        }

        #endregion Heroes

        #region Write

        /// <summary>Writes the current <see cref="Settings"/> to file.</summary>
        /// <param name="settings">Current <see cref="Settings"/></param>
        internal static void WriteSettings(Settings settings)
        {
            using (XmlTextWriter writer = new XmlTextWriter("Data/Settings.xml", Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                writer.WriteStartDocument();

                writer.WriteStartElement("Settings");
                writer.WriteElementString("AdminPassword", settings.AdminPassword);
                writer.WriteElementString("Theme", settings.Theme);
                writer.WriteEndDocument();
            }
        }

        internal static void WriteAll()
        {
            WriteAllHeadArmor();
            WriteAllBodyArmor();
            WriteAllHandArmor();
            WriteAllLegArmor();
            WriteAllFeetArmor();
            WriteAllWeapons();
            WriteAllClasses();
            WriteAllRings();
            WriteAllEnemies();
            WriteAllHeroes();
            WriteAllDrinks();
            WriteAllFood();
            WriteAllPotions();
            WriteAllSpells();
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

        /// <summary>Modifies a Hero's details in the database.</summary>
        /// <param name="oldHero">Hero whose details need to be modified</param>
        /// <param name="newHero">Hero with new details</param>
        /// <returns>True if successful</returns>
        internal static bool ChangeHeroDetails(Hero oldHero, Hero newHero)
        {
            bool success = false;
            if (!File.Exists($"Data/Heroes/{newHero.Name}.xml"))
            {
                SaveHero(newHero);
                DeleteHero(oldHero);
                success = true;
            }
            else
                GameState.DisplayNotification("This username is already taken. Unable to change hero details.", "Sulimn");

            return success;
        }

        /// <summary>Writes a <see cref="Hero"/> to file.</summary>
        /// <param name="writer">Settings for the file to be written</param>
        /// <param name="hero"><see cref="Hero"/> to be saved.</param>
        /// <returns>Properly formatted XML file</returns>
        internal static XmlTextWriter WriteHero(XmlTextWriter writer, Hero hero)
        {
            writer.WriteStartElement("Hero");

            writer.WriteStartElement("Information");
            writer.WriteElementString("Name", hero.Name);
            writer.WriteElementString("Password", hero.Password);
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
                if (item != null)
                {
                    writer.WriteStartElement("Item");
                    writer.WriteElementString("Name", item.Name);
                    writer.WriteElementString("Durability", item.CurrentDurabilityToString);
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Bank");
            writer.WriteElementString("GoldInBank", hero.Bank.GoldInBankToString);
            writer.WriteElementString("LoanTaken", hero.Bank.LoanTakenToString);
            writer.WriteElementString("LoanAvailable", hero.Bank.LoanAvailableToString);
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
            writer.WriteEndElement(); //finish Spellbook

            writer.WriteStartElement("Progression");
            writer.WriteElementString("Fields", hero.Progression.Fields.ToString());
            writer.WriteElementString("Forest", hero.Progression.Forest.ToString());
            writer.WriteElementString("Cathedral", hero.Progression.Cathedral.ToString());
            writer.WriteElementString("Mines", hero.Progression.Mines.ToString());
            writer.WriteElementString("Catacombs", hero.Progression.Catacombs.ToString());
            writer.WriteElementString("Courtyard", hero.Progression.Courtyard.ToString());
            writer.WriteElementString("Battlements", hero.Progression.Battlements.ToString());
            writer.WriteElementString("Armoury", hero.Progression.Armoury.ToString());
            writer.WriteElementString("Spire", hero.Progression.Spire.ToString());
            writer.WriteElementString("ThroneRoom", hero.Progression.ThroneRoom.ToString());
            writer.WriteEndElement(); //finish Progression

            writer.WriteEndElement(); //finish Hero

            return writer;
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
                    WriteHero(writer, hero);
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