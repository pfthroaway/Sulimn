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

        /// <summary>
        /// Determines whether a Hero's credentials are authentic.
        /// </summary>
        /// <param name="username">Hero's name</param>
        /// <param name="password">Hero's password</param>
        /// <returns></returns>
        internal static bool CheckLogin(string username, string password)
        {
            Hero checkHero = new Hero();
            try
            {
                checkHero = AllHeroes.Find(hero => hero.Name == username);
                if (PasswordHash.ValidatePassword(password, checkHero.Password))
                {
                    CurrentHero = new Hero(checkHero);
                    return true;
                }
                else
                {
                    MessageBox.Show("Invalid login.", "Sulimn", MessageBoxButton.OK);
                    return false;
                }
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
        internal static async void LoadAll()
        {
            SQLiteConnection con = new SQLiteConnection();
            SQLiteDataAdapter da = new SQLiteDataAdapter();
            DataSet ds = new DataSet();
            con.ConnectionString = _DBPROVIDERANDSOURCE;

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    string sql = "SELECT * FROM Classes";
                    string table = "LoadStuff";
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, table);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        HeroClass newClass = new HeroClass();

                        newClass.Name = ds.Tables[0].Rows[i]["ClassName"].ToString();
                        newClass.Description = ds.Tables[0].Rows[i]["ClassDescription"].ToString();
                        newClass.SkillPoints = Int32Helper.Parse(ds.Tables[0].Rows[i]["SkillPoints"]);
                        newClass.Strength = Int32Helper.Parse(ds.Tables[0].Rows[i]["Strength"]);
                        newClass.Vitality = Int32Helper.Parse(ds.Tables[0].Rows[i]["Vitality"]);
                        newClass.Dexterity = Int32Helper.Parse(ds.Tables[0].Rows[i]["Dexterity"]);
                        newClass.Wisdom = Int32Helper.Parse(ds.Tables[0].Rows[i]["Wisdom"]);

                        AllClasses.Add(newClass);
                    }

                    sql = "SELECT * FROM HeadArmor";
                    ds = new DataSet();
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            HeadArmor newHeadArmor = new HeadArmor(ds.Tables[0].Rows[i]["ArmorName"].ToString(), ItemTypes.Head, ds.Tables[0].Rows[i]["ArmorDescription"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newHeadArmor);
                            AllHeadArmor.Add(newHeadArmor);
                        }
                    }

                    sql = "SELECT * FROM BodyArmor";
                    ds = new DataSet();
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            BodyArmor newBodyArmor = new BodyArmor(ds.Tables[0].Rows[i]["ArmorName"].ToString(), ItemTypes.Body, ds.Tables[0].Rows[i]["ArmorDescription"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newBodyArmor);
                            AllBodyArmor.Add(newBodyArmor);
                        }
                    }

                    sql = "SELECT * FROM HandArmor";
                    ds = new DataSet();
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            HandArmor newHandArmor = new HandArmor(ds.Tables[0].Rows[i]["ArmorName"].ToString(), ItemTypes.Hands, ds.Tables[0].Rows[i]["ArmorDescription"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newHandArmor);
                            AllHandArmor.Add(newHandArmor);
                        }
                    }

                    sql = "SELECT * FROM LegArmor";
                    ds = new DataSet();
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            LegArmor newLegArmor = new LegArmor(ds.Tables[0].Rows[i]["ArmorName"].ToString(), ItemTypes.Legs, ds.Tables[0].Rows[i]["ArmorDescription"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newLegArmor);
                            AllLegArmor.Add(newLegArmor);
                        }
                    }

                    sql = "SELECT * FROM FeetArmor";
                    ds = new DataSet();
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            FeetArmor newFeetArmor = new FeetArmor(ds.Tables[0].Rows[i]["ArmorName"].ToString(), ItemTypes.Feet, ds.Tables[0].Rows[i]["ArmorDescription"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newFeetArmor);
                            AllFeetArmor.Add(newFeetArmor);
                        }
                    }

                    sql = "SELECT * FROM Rings";
                    ds = new DataSet();
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Ring newRing = new Ring(ds.Tables[0].Rows[i]["Name"].ToString(), ItemTypes.Ring, ds.Tables[0].Rows[i]["Description"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["Damage"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["Defense"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["Strength"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["Vitality"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["Dexterity"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["Wisdom"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["Weight"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["Value"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newRing);
                            AllRings.Add(newRing);
                        }
                    }

                    sql = "SELECT * FROM Admin";
                    ds = new DataSet();
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                        AdminPassword = ds.Tables[0].Rows[0]["AdminPassword"].ToString();

                    sql = "SELECT * FROM Food";
                    ds = new DataSet();
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            FoodTypes currentFoodType;
                            Enum.TryParse(ds.Tables[0].Rows[i]["FoodType"].ToString(), out currentFoodType);
                            Food newFood = new Food(ds.Tables[0].Rows[i]["FoodName"].ToString(), currentFoodType, ds.Tables[0].Rows[i]["FoodDescription"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["FoodAmount"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["FoodWeight"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["FoodValue"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newFood);
                            AllFood.Add(newFood);
                        }
                    }

                    sql = "SELECT * FROM Potions";
                    ds = new DataSet();
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            PotionTypes currentPotionType;
                            Enum.TryParse(ds.Tables[0].Rows[i]["PotionType"].ToString(), out currentPotionType);
                            Potion newPotion = new Potion(ds.Tables[0].Rows[i]["PotionName"].ToString(), currentPotionType, ds.Tables[0].Rows[i]["PotionDescription"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["PotionAmount"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["PotionValue"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newPotion);
                            AllPotions.Add(newPotion);
                        }
                    }

                    sql = "SELECT * FROM Spells";
                    ds = new DataSet();
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            SpellTypes currentSpellType;
                            Enum.TryParse(ds.Tables[0].Rows[i]["SpellType"].ToString(), out currentSpellType);
                            Spell newSpell = new Spell(ds.Tables[0].Rows[i]["SpellName"].ToString(), currentSpellType, ds.Tables[0].Rows[i]["SpellDescription"].ToString(), ds.Tables[0].Rows[i]["RequiredClass"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["RequiredLevel"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["MagicCost"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["SpellAmount"]));

                            AllSpells.Add(newSpell);
                        }
                    }

                    sql = "SELECT * FROM Weapons";
                    ds = new DataSet();
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            WeaponTypes currentWeaponType;
                            Enum.TryParse(ds.Tables[0].Rows[i]["WeaponType"].ToString(), out currentWeaponType);
                            Weapon newWeapon = new Weapon(ds.Tables[0].Rows[i]["WeaponName"].ToString(), currentWeaponType, ds.Tables[0].Rows[i]["WeaponDescription"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["WeaponDamage"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["WeaponWeight"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["WeaponValue"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newWeapon);
                            AllWeapons.Add(newWeapon);
                        }
                    }

                    sql = "SELECT * FROM Enemies";
                    ds = new DataSet();
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Weapon weapon = new Weapon();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Weapon"].ToString()))
                                weapon = new Weapon(AllWeapons.Find(wpn => wpn.Name == ds.Tables[0].Rows[i]["Weapon"].ToString()));
                            HeadArmor head = new HeadArmor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Head"].ToString()))
                                head = new HeadArmor(AllHeadArmor.Find(armr => armr.Name == ds.Tables[0].Rows[i]["Head"].ToString()));
                            BodyArmor body = new BodyArmor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Body"].ToString()))
                                body = new BodyArmor(AllBodyArmor.Find(armr => armr.Name == ds.Tables[0].Rows[i]["Body"].ToString()));
                            HandArmor hands = new HandArmor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Hands"].ToString()))
                                hands = new HandArmor(AllHandArmor.Find(armr => armr.Name == ds.Tables[0].Rows[i]["Hands"].ToString()));
                            LegArmor legs = new LegArmor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Legs"].ToString()))
                                legs = new LegArmor(AllLegArmor.Find(armr => armr.Name == ds.Tables[0].Rows[i]["Legs"].ToString()));
                            FeetArmor feet = new FeetArmor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Feet"].ToString()))
                                feet = new FeetArmor(AllFeetArmor.Find(armr => armr.Name == ds.Tables[0].Rows[i]["Feet"].ToString()));
                            Ring leftRing = new Ring();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["LeftRing"].ToString()))
                                leftRing = new Ring(AllRings.Find(armr => armr.Name == ds.Tables[0].Rows[i]["LeftRing"].ToString()));
                            Ring rightRing = new Ring();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["RightRing"].ToString()))
                                rightRing = new Ring(AllRings.Find(armr => armr.Name == ds.Tables[0].Rows[i]["RightRing"].ToString()));

                            int gold = Int32Helper.Parse(ds.Tables[0].Rows[i]["Gold"]);

                            Enemy newEnemy = new Enemy(ds.Tables[0].Rows[i]["EnemyName"].ToString(), ds.Tables[0].Rows[i]["EnemyType"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["Level"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["Experience"]), new Attributes(Int32Helper.Parse(ds.Tables[0].Rows[i]["Strength"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["Vitality"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["Dexterity"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["Wisdom"])), new Statistics(Int32Helper.Parse(ds.Tables[0].Rows[i]["CurrentHealth"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["MaximumHealth"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["CurrentMagic"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["MaximumMagic"])), new Equipment(weapon, head, hands, body, legs, feet, leftRing, rightRing), new Inventory(new List<Item>(), gold));

                            AllEnemies.Add(newEnemy);
                        }
                    }

                    sql = "SELECT * FROM Players";
                    ds = new DataSet();
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Hero newHero = new Hero();
                            string spells, weapon, head, body, hands, legs, feet, leftRing, rightRing, inventory;

                            newHero.Name = ds.Tables[0].Rows[i]["CharacterName"].ToString();
                            newHero.Password = ds.Tables[0].Rows[i]["CharacterPassword"].ToString();
                            newHero.Class = new HeroClass(AllClasses.Find(heroClass => heroClass.Name == ds.Tables[0].Rows[i]["Class"].ToString()));
                            newHero.Level = Int32Helper.Parse(ds.Tables[0].Rows[i]["Level"]);
                            newHero.Experience = Int32Helper.Parse(ds.Tables[0].Rows[i]["Experience"]);
                            newHero.SkillPoints = Int32Helper.Parse(ds.Tables[0].Rows[i]["SkillPoints"]);
                            newHero.Attributes.Strength = Int32Helper.Parse(ds.Tables[0].Rows[i]["Strength"]);
                            newHero.Attributes.Vitality = Int32Helper.Parse(ds.Tables[0].Rows[i]["Vitality"]);
                            newHero.Attributes.Dexterity = Int32Helper.Parse(ds.Tables[0].Rows[i]["Dexterity"]);
                            newHero.Attributes.Wisdom = Int32Helper.Parse(ds.Tables[0].Rows[i]["Wisdom"]);
                            newHero.Statistics.CurrentHealth = Int32Helper.Parse(ds.Tables[0].Rows[i]["CurrentHealth"]);
                            newHero.Statistics.MaximumHealth = Int32Helper.Parse(ds.Tables[0].Rows[i]["MaximumHealth"]);
                            newHero.Statistics.CurrentMagic = Int32Helper.Parse(ds.Tables[0].Rows[i]["CurrentMagic"]);
                            newHero.Statistics.MaximumMagic = Int32Helper.Parse(ds.Tables[0].Rows[i]["MaximumMagic"]);
                            spells = ds.Tables[0].Rows[i]["KnownSpells"].ToString();
                            inventory = ds.Tables[0].Rows[i]["Inventory"].ToString();

                            if (spells.Length > 0)
                                newHero.Spellbook = SetSpellbook(spells);

                            if (inventory.Length > 0)
                                newHero.Inventory = SetInventory(inventory);
                            newHero.Inventory.Gold = Int32Helper.Parse(ds.Tables[0].Rows[i]["Gold"]);

                            string equipmentSQL = "SELECT * FROM Equipment WHERE [CharacterName]='" + newHero.Name + "'";
                            DataSet equipmentDS = new DataSet();
                            SQLiteDataAdapter equipmentDA = new SQLiteDataAdapter(equipmentSQL, con);
                            equipmentDA.Fill(equipmentDS, "Equipment");

                            weapon = equipmentDS.Tables[0].Rows[0]["Weapon"].ToString();
                            head = equipmentDS.Tables[0].Rows[0]["Head"].ToString();
                            body = equipmentDS.Tables[0].Rows[0]["Body"].ToString();
                            hands = equipmentDS.Tables[0].Rows[0]["Hands"].ToString();
                            legs = equipmentDS.Tables[0].Rows[0]["Legs"].ToString();
                            feet = equipmentDS.Tables[0].Rows[0]["Feet"].ToString();
                            leftRing = equipmentDS.Tables[0].Rows[0]["LeftRing"].ToString();
                            rightRing = equipmentDS.Tables[0].Rows[0]["RightRing"].ToString();

                            newHero.Equipment.Weapon = AllWeapons.Find(x => x.Name == weapon);
                            newHero.Equipment.Head = AllHeadArmor.Find(x => x.Name == head);
                            newHero.Equipment.Body = AllBodyArmor.Find(x => x.Name == body);
                            newHero.Equipment.Hands = AllHandArmor.Find(x => x.Name == hands);
                            newHero.Equipment.Legs = AllLegArmor.Find(x => x.Name == legs);
                            newHero.Equipment.Feet = AllFeetArmor.Find(x => x.Name == feet);
                            if (!string.IsNullOrWhiteSpace(leftRing))
                                newHero.Equipment.LeftRing = AllRings.Find(x => x.Name == leftRing);
                            if (!string.IsNullOrWhiteSpace(rightRing))
                                newHero.Equipment.RightRing = AllRings.Find(x => x.Name == rightRing);

                            AllHeroes.Add(newHero);
                        }
                    }

                    sql = "SELECT * FROM MaxHeroStats";
                    ds = new DataSet();
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, table);

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
                        MaximumStatsHero.Statistics.CurrentHealth = Int32Helper.Parse(ds.Tables[0].Rows[0]["CurrentHealth"]);
                        MaximumStatsHero.Statistics.MaximumHealth = Int32Helper.Parse(ds.Tables[0].Rows[0]["MaximumHealth"]);
                        MaximumStatsHero.Statistics.CurrentMagic = Int32Helper.Parse(ds.Tables[0].Rows[0]["CurrentMagic"]);
                        MaximumStatsHero.Statistics.MaximumMagic = Int32Helper.Parse(ds.Tables[0].Rows[0]["MaximumMagic"]);
                    }

                    AllItems = AllItems.OrderBy(item => item.Name).ToList();
                    AllEnemies = AllEnemies.OrderBy(enemy => enemy.Name).ToList();
                    AllSpells = AllSpells.OrderBy(spell => spell.Name).ToList();
                    AllClasses = AllClasses.OrderBy(x => x.Name).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Filling DataSet", MessageBoxButton.OK);
                }
                finally { con.Close(); }
            });
        }

        #region Item Management

        /// <summary>
        /// Retrieves a List of all Items of specified Type.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>List of specified Type.</returns>
        static public List<T> GetItemsOfType<T>()
        {
            List<T> newList = new List<T>();
            newList = AllItems.OfType<T>().ToList<T>();
            return newList;
        }

        /// <summary>
        /// Sets the Hero's inventory.
        /// </summary>
        /// <param name="inventory">Inventory to be converted</param>
        /// <returns>Inventory List</returns>
        private static Inventory SetInventory(string inventory)
        {
            List<Item> itemList = new List<Item>();
            string[] arrInventory = inventory.Split(',');

            foreach (string str in arrInventory)
            {
                ItemTypes type = AllItems.Find(x => x.Name == (str.Trim())).Type;
                itemList.Add(AllItems.Find(x => x.Name == str.Trim()));
            }
            return new Inventory(itemList, 0);
        }

        /// <summary>
        /// Sets the list of the Hero's known spells.
        /// </summary>
        /// <param name="spells">String list of spells</param>
        /// <returns>List of known Spells</returns>
        private static Spellbook SetSpellbook(string spells)
        {
            List<Spell> spellList = new List<Spell>();
            string[] arrSpell = spells.Split(',');

            foreach (string str in arrSpell)
                spellList.Add(AllSpells.Find(x => x.Name == str.Trim()));
            return new Spellbook(spellList);
        }

        #endregion Item Management

        #region Hero Saving

        internal static async Task<bool> NewHero(Hero newHero)
        {
            bool success = false;
            newHero.Equipment.Head = AllHeadArmor.Find(armr => armr.Name == "Cloth Helmet");
            newHero.Equipment.Body = AllBodyArmor.Find(armr => armr.Name == "Cloth Shirt");
            newHero.Equipment.Hands = AllHandArmor.Find(armr => armr.Name == "Cloth Gloves");
            newHero.Equipment.Legs = AllLegArmor.Find(armr => armr.Name == "Cloth Pants");
            newHero.Equipment.Feet = AllFeetArmor.Find(armr => armr.Name == "Cloth Shoes");

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

            SQLiteConnection con = new SQLiteConnection();
            con.ConnectionString = _DBPROVIDERANDSOURCE;
            SQLiteCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO Players([CharacterName],[CharacterPassword],[Class],[Level],[Experience],[SkillPoints],[Strength],[Vitality],[Dexterity],[Wisdom],[Gold],[CurrentHealth],[MaximumHealth],[CurrentMagic],[MaximumMagic],[KnownSpells],[Inventory])Values('" + newHero.Name + "','" + newHero.Password + "','" + newHero.Class.Name + "','" + newHero.Level + "','" + newHero.Experience + "','" + newHero.SkillPoints + "','" + newHero.Attributes.Strength + "','" + newHero.Attributes.Vitality + "','" + newHero.Attributes.Dexterity + "','" + newHero.Attributes.Wisdom + "','" + newHero.Inventory.Gold + "','" + newHero.Statistics.CurrentHealth + "','" + newHero.Statistics.MaximumHealth + "','" + newHero.Statistics.CurrentMagic + "','" + newHero.Statistics.MaximumMagic + "','" + newHero.Spellbook.ToString() + "','" + newHero.Inventory + "')";

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Bank([CharacterName],[Gold],[LoanTaken])Values('" + newHero.Name + "',0,0)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Equipment([CharacterName],[Weapon],[Head],[Body],[Hands],[Legs],[Feet])Values('" + newHero.Name + "','" + newHero.Equipment.Weapon.Name + "','" + newHero.Equipment.Head.Name + "','" + newHero.Equipment.Body.Name + "','" + newHero.Equipment.Hands.Name + "','" + newHero.Equipment.Legs.Name + "','" + newHero.Equipment.Feet.Name + "')";
                    cmd.ExecuteNonQuery();

                    GameState.AllHeroes.Add(newHero);
                    success = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Creating New Hero", MessageBoxButton.OK);
                }
                finally { con.Close(); }
            });
            return success;
        }

        /// <summary>
        /// Saves Hero to database.
        /// </summary>
        internal static async void SaveHero(Hero saveHero)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteConnection con = new SQLiteConnection();
            con.ConnectionString = _DBPROVIDERANDSOURCE;
            string sql = "UPDATE Players SET [Level] = @level, [Experience] = @experience, [SkillPoints] = @skillPoints, [Strength] = @strength, [Vitality] = @vitality, [Dexterity] = @dexterity, [Wisdom] = @wisdom, [Gold] = @gold, [CurrentHealth] = @currentHealth, [MaximumHealth] = @maximumHealth, [CurrentMagic] = @currentMagic, [MaximumMagic] = @maximumMagic, [KnownSpells] = @spells, [Inventory] = @inventory WHERE [CharacterName] = @name";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
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

                    string leftRing, rightRing;
                    if (saveHero.Equipment.LeftRing != new Ring())
                        leftRing = saveHero.Equipment.LeftRing.Name;
                    else
                        leftRing = "";
                    if (saveHero.Equipment.LeftRing != new Ring())
                        rightRing = saveHero.Equipment.LeftRing.Name;
                    else
                        rightRing = "";

                    sql = "UPDATE Equipment SET [Weapon] = @weapon, [Head] = @head, [Body] = @body, [Hands] = hands, [Legs] = @legs, [Feet] = @feet, [LeftRing] = leftRing, [RightRing] = rightRing WHERE [CharacterName] = @name";
                    cmd = new SQLiteCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@weapon", saveHero.Equipment.Weapon.Name);
                    cmd.Parameters.AddWithValue("@head", saveHero.Equipment.Head.Name);
                    cmd.Parameters.AddWithValue("@body", saveHero.Equipment.Body.Name);
                    cmd.Parameters.AddWithValue("@hands", saveHero.Equipment.Hands.Name);
                    cmd.Parameters.AddWithValue("@legs", saveHero.Equipment.Legs.Name);
                    cmd.Parameters.AddWithValue("@feet", saveHero.Equipment.Feet.Name);
                    cmd.Parameters.AddWithValue("@leftRing", leftRing);
                    cmd.Parameters.AddWithValue("@rightRing", rightRing);
                    int index = AllHeroes.FindIndex(hero => hero.Name == saveHero.Name);
                    cmd.Parameters.AddWithValue("@name", saveHero.Name);
                    cmd.ExecuteNonQuery();

                    AllHeroes[index] = new Hero(saveHero);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Saving Hero", MessageBoxButton.OK);
                }
                finally { con.Close(); }
            });
        }

        /// <summary>
        /// Saves the Hero's bank information.
        /// </summary>
        /// <param name="heroName">Name of Hero</param>
        /// <param name="goldInBank">Gold in the bank</param>
        /// <param name="loanTaken">Loan taken out</param>
        internal static async void SaveHeroBank(int goldInBank, int loanTaken)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteConnection con = new SQLiteConnection();
            con.ConnectionString = _DBPROVIDERANDSOURCE;
            string sql = "UPDATE Bank SET [Gold] = @gold, [LoanTaken] = @loanTaken WHERE [CharacterName] = @name";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
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
                finally { con.Close(); }
            });
        }

        /// <summary>
        /// Saves the Hero's password to the database.
        /// </summary>
        /// <param name="saveHero">Hero whose password needs to be saved</param>
        internal static async void SaveHeroPassword(Hero saveHero)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            SQLiteConnection con = new SQLiteConnection();
            con.ConnectionString = _DBPROVIDERANDSOURCE;
            string sql = "UPDATE Players SET [CharacterPassword] = @password WHERE [CharacterName] = @name";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
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
                finally { con.Close(); }
            });
        }

        #endregion Hero Saving

        #region Exploration Events

        /// <summary>
        /// Event where the Hero finds gold.
        /// </summary>
        internal static string EventFindGold(int minGold, int maxGold)
        {
            int foundGold = Functions.GenerateRandomNumber(minGold, maxGold);
            CurrentHero.Inventory.Gold += foundGold;
            SaveHero(CurrentHero);
            return "You find " + foundGold.ToString("N0") + " gold!";
        }

        /// <summary>
        /// Event where the Hero finds an item.
        /// </summary>
        /// <param name="minValue">Minimum value of Item</param>
        /// <param name="maxValue">Maximum value of Item</param>
        /// <param name="canSell">Can the item be sold?</param>
        /// <returns></returns>
        internal static string EventFindItem(int minValue, int maxValue, bool canSell = true)
        {
            List<Item> availableItems = new List<Item>();
            availableItems = AllItems.Where(x => x.Value >= minValue && x.Value <= maxValue && x.IsSold == true).ToList();
            int item = Functions.GenerateRandomNumber(0, availableItems.Count - 1);

            CurrentHero.Inventory.AddItem(availableItems[item]);
            SaveHero(CurrentHero);
            return "You find a " + availableItems[item].Name + "!";
        }

        internal static void EventFindItem(params string[] names)
        {
            List<Item> availableItems = new List<Item>();
            foreach (string name in names)
                availableItems.Add(GetItem(name));
            int item = Functions.GenerateRandomNumber(0, availableItems.Count - 1);

            CurrentHero.Inventory.AddItem(availableItems[item]);

            SaveHero(CurrentHero);
        }

        /// <summary>
        /// Event where the Hero encounters a hostile animal.
        /// </summary>
        /// <param name="minLevel">Minimum level of animal</param>
        /// <param name="maxLevel">Maximum level of animal</param>
        internal static void EventEncounterAnimal(int minLevel, int maxLevel)
        {
            List<Enemy> availableEnemies = new List<Enemy>();
            availableEnemies = AllEnemies.Where(o => o.Level >= minLevel && o.Level <= maxLevel).ToList();
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
            List<Enemy> availableEnemies = new List<Enemy>();
            availableEnemies = AllEnemies.Where(o => o.Level >= minLevel && o.Level <= maxLevel).ToList();
            int enemyNum = Functions.GenerateRandomNumber(0, availableEnemies.Count - 1);
            CurrentEnemy = new Enemy(availableEnemies[enemyNum]);
            if (CurrentEnemy.Inventory.Gold > 0)
                CurrentEnemy.Inventory.Gold = Functions.GenerateRandomNumber(CurrentEnemy.Inventory.Gold / 2, CurrentEnemy.Inventory.Gold);
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
                CurrentEnemy.Inventory.Gold = Functions.GenerateRandomNumber(CurrentEnemy.Inventory.Gold / 2, CurrentEnemy.Inventory.Gold);
        }

        /// <summary>
        /// Event where the Hero encounters a water stream and restores health and magic.
        /// </summary>
        /// <returns>String saying Hero has been healed</returns>
        internal static string EventEncounterStream()
        {
            CurrentHero.Statistics.CurrentHealth = CurrentHero.Statistics.MaximumHealth;
            CurrentHero.Statistics.CurrentMagic = CurrentHero.Statistics.MaximumMagic;

            return "You stumble across a stream. You stop to drink some of the water and rest a while. You feel recharged!";
        }

        #endregion Exploration Events

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
    }
}