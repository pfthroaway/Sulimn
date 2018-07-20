using Sulimn.Classes.Entities;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sulimn.Classes.Database
{
    internal static class XMLInteraction
    {
        //TODO Implement XML reading/writing
        //TODO Write all Items, characters, and classes to files
        //TODO Sort things into folders (game_data, player_mods, etc)
        internal static Hero LoadHero(string filename)
        {
            Hero loadHero = new Hero();
            return loadHero;
        }

        internal static void WriteAll()
        {
            //WriteAllHeadArmor();
            //WriteAllBodyArmor();
            //WriteAllHandArmor();
            //WriteAllLegArmor();
            //WriteAllFeetArmor();
            //WriteAllWeapons();
            //WriteAllClasses();
            //WriteAllRings();
            //WriteAllEnemies();
            WriteAllHeroes();
        }

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

                writer.WriteStartElement("AllWeapon");
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
                    writer.WriteElementString("Health", heroClass.MaximumHealth.ToString());
                    writer.WriteElementString("Magic", heroClass.MaximumMagic.ToString());

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

        private static void WriteConsumables()
        {
        }

        private static void WriteEquipment(XmlWriter writer, Equipment equipment)
        {
            writer.WriteEndElement();
            writer.WriteStartElement("BodyArmor");
            writer.WriteEndElement();
            writer.WriteStartElement("HandArmor");
            writer.WriteEndElement();
            writer.WriteStartElement("LegArmor");
            writer.WriteEndElement();
            writer.WriteStartElement("FeetArmor");
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

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

        //private static void ReadMultipleTest()
        //{
        //    List<Character> characters = new List<Character>();
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.Load("Aragon.xml");
        //    XmlNodeList xnList = xmlDoc.SelectNodes("/Character");
        //    foreach (XmlNode xn in xnList)
        //    {
        //        Character newCharacter = new Character
        //        {
        //            Name = xn["Name"]?.InnerText,
        //            Level = Int32Helper.Parse(xn["Level"]?.InnerText),
        //            Experience = Int32Helper.Parse(xn["Experience"]?.InnerText),
        //            Gold = Int32Helper.Parse(xn["Gold"]?.InnerText),
        //            Strength = Int32Helper.Parse(xn["Strength"]?.InnerText),
        //            Vitality = Int32Helper.Parse(xn["Vitality"]?.InnerText),
        //            Dexterity = Int32Helper.Parse(xn["Dexterity"]?.InnerText),
        //            Wisdom = Int32Helper.Parse(xn["Wisdom"]?.InnerText)
        //        };

        //        if (newCharacter != new Character())
        //            characters.Add(newCharacter);
        //        else
        //            Console.WriteLine("Character doesn't exist!");
        //    }

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