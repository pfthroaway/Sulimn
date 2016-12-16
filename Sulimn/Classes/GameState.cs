using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sulimn
{
    internal static class GameState
    {
        private const string _DBPROVIDERANDSOURCE = "Data Source = Sulimn.sqlite;Version=3";

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

        /// <summary>
        /// Determines whether a Hero's credentials are authentic.
        /// </summary>
        /// <param name="username">Hero's name</param>
        /// <param name="password">Hero's password</param>
        /// <returns></returns>
        internal static bool CheckLogin(string username, string password)
        {
            try
            {
                Hero checkHero = AllHeroes.Find(hero => hero.Name == username);
                if (PasswordHash.ValidatePassword(password, checkHero.Password))
                {
                    CurrentHero = new Hero(checkHero);
                    return true;
                }
                MessageBox.Show("Invalid login.", "Sulimn", MessageBoxButton.OK);
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid login.", "Sulimn", MessageBoxButton.OK);
                return false;
            }
        }

        /// <summary>
        /// Loads almost everything from the database.
        /// </summary>
        internal static async Task LoadAll()
        {
            SQLiteConnection con = new SQLiteConnection();
            SQLiteDataAdapter da;
            DataSet ds = new DataSet();
            con.ConnectionString = _DBPROVIDERANDSOURCE;

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    da = new SQLiteDataAdapter("SELECT * FROM Classes", con);
                    da.Fill(ds, "Classes");

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        HeroClass newClass = new HeroClass(
            ds.Tables[0].Rows[i]["ClassName"].ToString(),
            ds.Tables[0].Rows[i]["ClassDescription"].ToString(),
            Int32Helper.Parse(ds.Tables[0].Rows[i]["SkillPoints"]),
            Int32Helper.Parse(ds.Tables[0].Rows[i]["Strength"]),
            Int32Helper.Parse(ds.Tables[0].Rows[i]["Vitality"]),
            Int32Helper.Parse(ds.Tables[0].Rows[i]["Dexterity"]),
            Int32Helper.Parse(ds.Tables[0].Rows[i]["Wisdom"]));

                        AllClasses.Add(newClass);
                    }

                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM HeadArmor", con);
                    da.Fill(ds, "HeadArmor");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            HeadArmor newHeadArmor = new HeadArmor(
                ds.Tables[0].Rows[i]["ArmorName"].ToString(),
                ItemTypes.Head,
                ds.Tables[0].Rows[i]["ArmorDescription"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newHeadArmor);
                            AllHeadArmor.Add(newHeadArmor);
                        }

                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM BodyArmor", con);
                    da.Fill(ds, "BodyArmor");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            BodyArmor newBodyArmor = new BodyArmor(
                ds.Tables[0].Rows[i]["ArmorName"].ToString(),
                ItemTypes.Body,
                ds.Tables[0].Rows[i]["ArmorDescription"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newBodyArmor);
                            AllBodyArmor.Add(newBodyArmor);
                        }

                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM HandArmor", con);
                    da.Fill(ds, "HandArmor");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            HandArmor newHandArmor = new HandArmor(
                ds.Tables[0].Rows[i]["ArmorName"].ToString(),
                ItemTypes.Hands,
                ds.Tables[0].Rows[i]["ArmorDescription"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newHandArmor);
                            AllHandArmor.Add(newHandArmor);
                        }

                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM LegArmor", con);
                    da.Fill(ds, "LegArmor");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            LegArmor newLegArmor = new LegArmor(
                ds.Tables[0].Rows[i]["ArmorName"].ToString(),
                ItemTypes.Legs,
                ds.Tables[0].Rows[i]["ArmorDescription"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newLegArmor);
                            AllLegArmor.Add(newLegArmor);
                        }

                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM FeetArmor", con);
                    da.Fill(ds, "FeetArmor");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            FeetArmor newFeetArmor = new FeetArmor(
                ds.Tables[0].Rows[i]["ArmorName"].ToString(),
                ItemTypes.Feet,
                ds.Tables[0].Rows[i]["ArmorDescription"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newFeetArmor);
                            AllFeetArmor.Add(newFeetArmor);
                        }

                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Rings", con);
                    da.Fill(ds, "Rings");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Ring newRing = new Ring(
                ds.Tables[0].Rows[i]["Name"].ToString(),
                ItemTypes.Ring,
                ds.Tables[0].Rows[i]["Description"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Damage"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Defense"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Strength"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Vitality"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Dexterity"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Wisdom"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Weight"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Value"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newRing);
                            AllRings.Add(newRing);
                        }

                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Admin", con);
                    da.Fill(ds, "Admin");

                    if (ds.Tables[0].Rows.Count > 0)
                        AdminPassword = ds.Tables[0].Rows[0]["AdminPassword"].ToString();

                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Food", con);
                    da.Fill(ds, "Food");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            FoodTypes currentFoodType;
                            Enum.TryParse(ds.Tables[0].Rows[i]["FoodType"].ToString(), out currentFoodType);
                            Food newFood = new Food(
                ds.Tables[0].Rows[i]["FoodName"].ToString(),
                currentFoodType,
                ds.Tables[0].Rows[i]["FoodDescription"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["FoodAmount"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["FoodWeight"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["FoodValue"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newFood);
                            AllFood.Add(newFood);
                        }

                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Potions", con);
                    da.Fill(ds, "Potions");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            PotionTypes currentPotionType;
                            Enum.TryParse(ds.Tables[0].Rows[i]["PotionType"].ToString(), out currentPotionType);
                            Potion newPotion = new Potion(
                ds.Tables[0].Rows[i]["PotionName"].ToString(),
                currentPotionType,
                ds.Tables[0].Rows[i]["PotionDescription"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["PotionAmount"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["PotionValue"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newPotion);
                            AllPotions.Add(newPotion);
                        }

                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Spells", con);
                    da.Fill(ds, "Spells");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            SpellTypes currentSpellType;
                            Enum.TryParse(ds.Tables[0].Rows[i]["SpellType"].ToString(), out currentSpellType);
                            Spell newSpell = new Spell(
                ds.Tables[0].Rows[i]["SpellName"].ToString(),
                currentSpellType,
                ds.Tables[0].Rows[i]["SpellDescription"].ToString(),
                ds.Tables[0].Rows[i]["RequiredClass"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["RequiredLevel"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["MagicCost"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["SpellAmount"]));

                            AllSpells.Add(newSpell);
                        }

                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Weapons", con);
                    da.Fill(ds, "Weapons");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            WeaponTypes currentWeaponType;
                            Enum.TryParse(ds.Tables[0].Rows[i]["WeaponType"].ToString(), out currentWeaponType);
                            Weapon newWeapon = new Weapon(
                ds.Tables[0].Rows[i]["WeaponName"].ToString(),
                currentWeaponType,
                ds.Tables[0].Rows[i]["WeaponDescription"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["WeaponDamage"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["WeaponWeight"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["WeaponValue"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newWeapon);
                            AllWeapons.Add(newWeapon);
                        }

                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Enemies", con);
                    da.Fill(ds, "Enemies");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Weapon weapon = new Weapon();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Weapon"].ToString()))
                                weapon =
                    new Weapon(
                    AllWeapons.Find(wpn => wpn.Name == ds.Tables[0].Rows[i]["Weapon"].ToString()));
                            HeadArmor head = new HeadArmor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Head"].ToString()))
                                head =
                    new HeadArmor(
                    AllHeadArmor.Find(armr => armr.Name == ds.Tables[0].Rows[i]["Head"].ToString()));
                            BodyArmor body = new BodyArmor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Body"].ToString()))
                                body =
                    new BodyArmor(
                    AllBodyArmor.Find(armr => armr.Name == ds.Tables[0].Rows[i]["Body"].ToString()));
                            HandArmor hands = new HandArmor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Hands"].ToString()))
                                hands =
                    new HandArmor(
                    AllHandArmor.Find(armr => armr.Name == ds.Tables[0].Rows[i]["Hands"].ToString()));
                            LegArmor legs = new LegArmor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Legs"].ToString()))
                                legs =
                    new LegArmor(
                    AllLegArmor.Find(armr => armr.Name == ds.Tables[0].Rows[i]["Legs"].ToString()));
                            FeetArmor feet = new FeetArmor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Feet"].ToString()))
                                feet =
                    new FeetArmor(
                    AllFeetArmor.Find(armr => armr.Name == ds.Tables[0].Rows[i]["Feet"].ToString()));
                            Ring leftRing = new Ring();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["LeftRing"].ToString()))
                                leftRing =
                    new Ring(
                    AllRings.Find(ring => ring.Name == ds.Tables[0].Rows[i]["LeftRing"].ToString()));
                            Ring rightRing = new Ring();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["RightRing"].ToString()))
                                rightRing =
                    new Ring(
                    AllRings.Find(ring => ring.Name == ds.Tables[0].Rows[i]["RightRing"].ToString()));

                            int gold = Int32Helper.Parse(ds.Tables[0].Rows[i]["Gold"]);

                            Enemy newEnemy = new Enemy(
                ds.Tables[0].Rows[i]["EnemyName"].ToString(),
                ds.Tables[0].Rows[i]["EnemyType"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Level"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Experience"]),
                new Attributes(
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Strength"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Vitality"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Dexterity"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Wisdom"])),
                new Statistics(
                Int32Helper.Parse(ds.Tables[0].Rows[i]["CurrentHealth"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["MaximumHealth"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["CurrentMagic"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["MaximumMagic"])),
                new Equipment(
                weapon,
                head,
                body,
                hands,
                legs,
                feet,
                leftRing,
                rightRing),
                new Inventory(
                new List<Item>(),
                gold));

                            AllEnemies.Add(newEnemy);
                        }

                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Players", con);
                    da.Fill(ds, "Players");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string name = ds.Tables[0].Rows[i]["CharacterName"].ToString();
                            DataSet equipmentDS = new DataSet();
                            SQLiteDataAdapter equipmentDA =
                new SQLiteDataAdapter("SELECT * FROM Equipment WHERE [CharacterName]='" + name + "'",
                con);
                            equipmentDA.Fill(equipmentDS, "Equipment");

                            string leftRingText = equipmentDS.Tables[0].Rows[0]["LeftRing"].ToString();
                            string rightRingText = equipmentDS.Tables[0].Rows[0]["RightRing"].ToString();

                            Ring leftRing = new Ring();
                            Ring rightRing = new Ring();
                            if (!string.IsNullOrWhiteSpace(leftRingText))
                                leftRing = AllRings.Find(x => x.Name == leftRingText);
                            if (!string.IsNullOrWhiteSpace(rightRingText))
                                rightRing = AllRings.Find(x => x.Name == rightRingText);

                            Hero newHero = new Hero(
                name,
                ds.Tables[0].Rows[i]["CharacterPassword"].ToString(),
                new HeroClass(
                AllClasses.Find(
                heroClass => heroClass.Name == ds.Tables[0].Rows[i]["Class"].ToString())),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Level"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Experience"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["SkillPoints"]),
                new Attributes(
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Strength"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Vitality"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Dexterity"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Wisdom"])),
                new Statistics(
                Int32Helper.Parse(ds.Tables[0].Rows[i]["CurrentHealth"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["MaximumHealth"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["CurrentMagic"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["MaximumMagic"])),
                new Equipment(
                AllWeapons.Find(x => x.Name == equipmentDS.Tables[0].Rows[0]["Weapon"].ToString()),
                AllHeadArmor.Find(x => x.Name == equipmentDS.Tables[0].Rows[0]["Head"].ToString()),
                AllBodyArmor.Find(x => x.Name == equipmentDS.Tables[0].Rows[0]["Body"].ToString()),
                AllHandArmor.Find(x => x.Name == equipmentDS.Tables[0].Rows[0]["Hands"].ToString()),
                AllLegArmor.Find(x => x.Name == equipmentDS.Tables[0].Rows[0]["Legs"].ToString()),
                AllFeetArmor.Find(x => x.Name == equipmentDS.Tables[0].Rows[0]["Feet"].ToString()),
                leftRing,
                rightRing),
                SetSpellbook(ds.Tables[0].Rows[i]["KnownSpells"].ToString()),
                SetInventory(ds.Tables[0].Rows[i]["Inventory"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Gold"])));

                            AllHeroes.Add(newHero);
                        }

                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM MaxHeroStats", con);
                    da.Fill(ds, "MaxHeroStats");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MaximumStatsHero.Level = Int32Helper.Parse(ds.Tables[0].Rows[0]["Level"]);
                        MaximumStatsHero.Experience = Int32Helper.Parse(ds.Tables[0].Rows[0]["Experience"]);
                        MaximumStatsHero.SkillPoints = Int32Helper.Parse(ds.Tables[0].Rows[0]["SkillPoints"]);
                        MaximumStatsHero.Attributes.Strength = Int32Helper.Parse(ds.Tables[0].Rows[0]["Strength"]);
                        MaximumStatsHero.Attributes.Vitality = Int32Helper.Parse(ds.Tables[0].Rows[0]["Vitality"]);
                        MaximumStatsHero.Attributes.Dexterity = Int32Helper.Parse(ds.Tables[0].Rows[0]["Dexterity"]);
                        MaximumStatsHero.Attributes.Wisdom = Int32Helper.Parse(ds.Tables[0].Rows[0]["Wisdom"]);
                        MaximumStatsHero.Inventory.Gold = Int32Helper.Parse(ds.Tables[0].Rows[0]["Gold"]);
                        MaximumStatsHero.Statistics.CurrentHealth =
            Int32Helper.Parse(ds.Tables[0].Rows[0]["CurrentHealth"]);
                        MaximumStatsHero.Statistics.MaximumHealth =
            Int32Helper.Parse(ds.Tables[0].Rows[0]["MaximumHealth"]);
                        MaximumStatsHero.Statistics.CurrentMagic =
            Int32Helper.Parse(ds.Tables[0].Rows[0]["CurrentMagic"]);
                        MaximumStatsHero.Statistics.MaximumMagic =
            Int32Helper.Parse(ds.Tables[0].Rows[0]["MaximumMagic"]);
                    }

                    AllItems = AllItems.OrderBy(item => item.Name).ToList();
                    AllEnemies = AllEnemies.OrderBy(enemy => enemy.Name).ToList();
                    AllSpells = AllSpells.OrderBy(spell => spell.Name).ToList();
                    AllClasses = AllClasses.OrderBy(x => x.Name).ToList();

                    DefaultWeapon = AllWeapons.Find(weapon => weapon.Name == "Fists");
                    DefaultHead = AllHeadArmor.Find(armor => armor.Name == "Cloth Helmet");
                    DefaultBody = AllBodyArmor.Find(armor => armor.Name == "Cloth Shirt");
                    DefaultHands = AllHandArmor.Find(armor => armor.Name == "Cloth Gloves");
                    DefaultLegs = AllLegArmor.Find(armor => armor.Name == "Cloth Pants");
                    DefaultFeet = AllFeetArmor.Find(armor => armor.Name == "Cloth Shoes");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Filling DataSet", MessageBoxButton.OK);
                }
                finally
                {
                    con.Close();
                }
            });
        }

        /// <summary>
        /// Gets a specific Enemy based on its name.
        /// </summary>
        /// <param name="name">Name of Enemy</param>
        /// <returns>Enemy</returns>
        internal static Enemy GetEnemy(string name)
        {
            return new Enemy(AllEnemies.Find(enemy => enemy.Name == name));
        }

        /// <summary>
        /// Gets a specific Item based on its name.
        /// </summary>
        /// <param name="name">Item name</param>
        /// <returns>Item</returns>
        internal static Item GetItem(string name)
        {
            return AllItems.Find(itm => itm.Name == name);
        }

        #region Item Management

        /// <summary>
        /// Retrieves a List of all Items of specified Type.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>List of specified Type.</returns>
        public static List<T> GetItemsOfType<T>()
        {
            List<T> newList = AllItems.OfType<T>().ToList();
            return newList;
        }

        /// <summary>
        /// Sets the Hero's inventory.
        /// </summary>
        /// <param name="inventory">Inventory to be converted</param>
        /// <param name="gold">Gold in the inventory</param>
        /// <returns>Inventory List</returns>
        private static Inventory SetInventory(string inventory, int gold)
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

        /// <summary>
        /// Sets the list of the Hero's known spells.
        /// </summary>
        /// <param name="spells">String list of spells</param>
        /// <returns>List of known Spells</returns>
        private static Spellbook SetSpellbook(string spells)
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

            SQLiteConnection con = new SQLiteConnection { ConnectionString = _DBPROVIDERANDSOURCE };
            SQLiteCommand cmd = con.CreateCommand();
            cmd.CommandText =
            "INSERT INTO Players([CharacterName],[CharacterPassword],[Class],[Level],[Experience],[SkillPoints],[Strength],[Vitality],[Dexterity],[Wisdom],[Gold],[CurrentHealth],[MaximumHealth],[CurrentMagic],[MaximumMagic],[KnownSpells],[Inventory])Values('" +
            newHero.Name + "','" + newHero.Password + "','" + newHero.Class.Name + "','" + newHero.Level + "','" +
            newHero.Experience + "','" + newHero.SkillPoints + "','" + newHero.Attributes.Strength + "','" +
            newHero.Attributes.Vitality + "','" + newHero.Attributes.Dexterity + "','" + newHero.Attributes.Wisdom +
            "','" + newHero.Inventory.Gold + "','" + newHero.Statistics.CurrentHealth + "','" +
            newHero.Statistics.MaximumHealth + "','" + newHero.Statistics.CurrentMagic + "','" +
            newHero.Statistics.MaximumMagic + "','" + newHero.Spellbook + "','" + newHero.Inventory + "')";

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Bank([CharacterName],[Gold],[LoanTaken])Values('" + newHero.Name +
        "',0,0)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText =
        "INSERT INTO Equipment([CharacterName],[Weapon],[Head],[Body],[Hands],[Legs],[Feet])Values('" +
        newHero.Name + "','" + newHero.Equipment.Weapon.Name + "','" + newHero.Equipment.Head.Name +
        "','" + newHero.Equipment.Body.Name + "','" + newHero.Equipment.Hands.Name + "','" +
        newHero.Equipment.Legs.Name + "','" + newHero.Equipment.Feet.Name + "')";
                    cmd.ExecuteNonQuery();

                    AllHeroes.Add(newHero);
                    success = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Creating New Hero", MessageBoxButton.OK);
                }
                finally
                {
                    con.Close();
                }
            });
            return success;
        }

        /// <summary>
        /// Saves Hero to database.
        /// </summary>
        internal static async Task<bool> SaveHero(Hero saveHero)
        {
            bool success = false;
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteConnection con = new SQLiteConnection { ConnectionString = _DBPROVIDERANDSOURCE };

            cmd.CommandText =
            "UPDATE Players SET [Level] = @level, [Experience] = @experience, [SkillPoints] = @skillPoints, [Strength] = @strength, [Vitality] = @vitality, [Dexterity] = @dexterity, [Wisdom] = @wisdom, [Gold] = @gold, [CurrentHealth] = @currentHealth, [MaximumHealth] = @maximumHealth, [CurrentMagic] = @currentMagic, [MaximumMagic] = @maximumMagic, [KnownSpells] = @spells, [Inventory] = @inventory WHERE [CharacterName] = @name";
            cmd.Parameters.AddWithValue("@level", saveHero.Level);
            cmd.Parameters.AddWithValue("@experience", saveHero.Experience.ToString());
            cmd.Parameters.AddWithValue("@skillPoints", saveHero.SkillPoints.ToString());
            cmd.Parameters.AddWithValue("@strength", saveHero.Attributes.Strength.ToString());
            cmd.Parameters.AddWithValue("@vitality", saveHero.Attributes.Vitality.ToString());
            cmd.Parameters.AddWithValue("@dexterity", saveHero.Attributes.Dexterity.ToString());
            cmd.Parameters.AddWithValue("@wisdom", saveHero.Attributes.Wisdom.ToString());
            cmd.Parameters.AddWithValue("@gold", saveHero.Inventory.Gold.ToString());
            cmd.Parameters.AddWithValue("@currentHealth", saveHero.Statistics.CurrentHealth.ToString());
            cmd.Parameters.AddWithValue("@maximumHealth", saveHero.Statistics.MaximumHealth.ToString());
            cmd.Parameters.AddWithValue("@currentMagic", saveHero.Statistics.CurrentMagic.ToString());
            cmd.Parameters.AddWithValue("@maximumMagic", saveHero.Statistics.MaximumMagic.ToString());
            cmd.Parameters.AddWithValue("@spells", saveHero.Spellbook.ToString());
            cmd.Parameters.AddWithValue("@inventory", saveHero.Inventory.ToString());
            cmd.Parameters.AddWithValue("@name", saveHero.Name);

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    cmd.CommandText =
        "UPDATE Equipment SET [Weapon] = @weapon, [Head] = @head, [Body] = @body, [Hands] = hands, [Legs] = @legs, [Feet] = @feet, [LeftRing] = @leftRing, [RightRing] = @rightRing WHERE [CharacterName] = @name";
                    cmd.Parameters.AddWithValue("@weapon", saveHero.Equipment.Weapon.Name);
                    cmd.Parameters.AddWithValue("@head", saveHero.Equipment.Head.Name);
                    cmd.Parameters.AddWithValue("@body", saveHero.Equipment.Body.Name);
                    cmd.Parameters.AddWithValue("@hands", saveHero.Equipment.Hands.Name);
                    cmd.Parameters.AddWithValue("@legs", saveHero.Equipment.Legs.Name);
                    cmd.Parameters.AddWithValue("@feet", saveHero.Equipment.Feet.Name);
                    cmd.Parameters.AddWithValue("@leftRing", saveHero.Equipment.LeftRing.Name);
                    cmd.Parameters.AddWithValue("@rightRing", saveHero.Equipment.RightRing.Name);
                    cmd.Parameters.AddWithValue("@name", saveHero.Name);
                    cmd.ExecuteNonQuery();

                    int index = AllHeroes.FindIndex(hero => hero.Name == saveHero.Name);
                    AllHeroes[index] = new Hero(saveHero);
                    success = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Saving Hero", MessageBoxButton.OK);
                }
                finally
                {
                    con.Close();
                }
            });
            return success;
        }

        /// <summary>
        /// Saves the Hero's bank information.
        /// </summary>
        /// <param name="goldInBank">Gold in the bank</param>
        /// <param name="loanTaken">Loan taken out</param>
        internal static async void SaveHeroBank(int goldInBank, int loanTaken)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteConnection con = new SQLiteConnection { ConnectionString = _DBPROVIDERANDSOURCE };

            cmd.CommandText = "UPDATE Bank SET [Gold] = @gold, [LoanTaken] = @loanTaken WHERE [CharacterName] = @name";
            cmd.Parameters.AddWithValue("@gold", goldInBank);
            cmd.Parameters.AddWithValue("@loanTaken", loanTaken);
            cmd.Parameters.AddWithValue("@name", CurrentHero.Name);

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Saving Hero Bank", MessageBoxButton.OK);
                }
                finally
                {
                    con.Close();
                }
            });
        }

        /// <summary>
        /// Saves the Hero's password to the database.
        /// </summary>
        /// <param name="saveHero">Hero whose password needs to be saved</param>
        internal static async void SaveHeroPassword(Hero saveHero)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteConnection con = new SQLiteConnection { ConnectionString = _DBPROVIDERANDSOURCE };

            cmd.CommandText = "UPDATE Players SET [CharacterPassword] = @password WHERE [CharacterName] = @name";
            cmd.Parameters.AddWithValue("@password", saveHero.Password);
            cmd.Parameters.AddWithValue("@name", saveHero.Name);

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    int index = AllHeroes.FindIndex(hero => hero.Name == CurrentHero.Name);
                    AllHeroes[index] = new Hero(CurrentHero);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Saving Hero Password", MessageBoxButton.OK);
                }
                finally
                {
                    con.Close();
                }
            });
        }

        #endregion Hero Saving

        #region Exploration Events

        /// <summary>
        /// Event where the Hero finds gold.
        /// </summary>
        internal static async Task<string> EventFindGold(int minGold, int maxGold)
        {
            int foundGold = Functions.GenerateRandomNumber(minGold, maxGold);
            CurrentHero.Inventory.Gold += foundGold;
            await SaveHero(CurrentHero);
            return "You find " + foundGold.ToString("N0") + " gold!";
        }

        /// <summary>
        /// Event where the Hero finds an item.
        /// </summary>
        /// <param name="minValue">Minimum value of Item</param>
        /// <param name="maxValue">Maximum value of Item</param>
        /// <param name="canSell">Can the item be sold?</param>
        /// <returns></returns>
        internal static async Task<string> EventFindItem(int minValue, int maxValue, bool canSell = true)
        {
            List<Item> availableItems = AllItems.Where(x => x.Value >= minValue && x.Value <= maxValue && x.IsSold).ToList();
            int item = Functions.GenerateRandomNumber(0, availableItems.Count - 1);

            CurrentHero.Inventory.AddItem(availableItems[item]);
            await SaveHero(CurrentHero);
            return "You find a " + availableItems[item].Name + "!";
        }

        internal static async void EventFindItem(params string[] names)
        {
            List<Item> availableItems = new List<Item>();
            foreach (string name in names)
                availableItems.Add(GetItem(name));
            int item = Functions.GenerateRandomNumber(0, availableItems.Count - 1);

            CurrentHero.Inventory.AddItem(availableItems[item]);

            await SaveHero(CurrentHero);
        }

        /// <summary>
        /// Event where the Hero encounters a hostile animal.
        /// </summary>
        /// <param name="minLevel">Minimum level of animal</param>
        /// <param name="maxLevel">Maximum level of animal</param>
        internal static void EventEncounterAnimal(int minLevel, int maxLevel)
        {
            List<Enemy> availableEnemies = AllEnemies.Where(o => o.Level >= minLevel && o.Level <= maxLevel).ToList();
            int enemyNum = Functions.GenerateRandomNumber(0, availableEnemies.Count - 1);
            CurrentEnemy = new Enemy(availableEnemies[enemyNum]);
        }

        /// <summary>
        /// Event where the Hero encounters a hostile Enemy.
        /// </summary>
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

        /// <summary>
        /// Event where the Hero encounters a hostile Enemy.
        /// </summary>
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

        /// <summary>
        /// Event where the Hero encounters a water stream and restores health and magic.
        /// </summary>
        /// <returns>String saying Hero has been healed</returns>
        internal static string EventEncounterStream()
        {
            CurrentHero.Statistics.CurrentHealth = CurrentHero.Statistics.MaximumHealth;
            CurrentHero.Statistics.CurrentMagic = CurrentHero.Statistics.MaximumMagic;

            return
            "You stumble across a stream. You stop to drink some of the water and rest a while. You feel recharged!";
        }

        #endregion Exploration Events
    }
}