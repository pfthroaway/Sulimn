using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sulimn_WPF
{
    internal static class GameState
    {
        private const string _DBPROVIDERANDSOURCE = "Provider=Microsoft.ACE.oledb.12.0;Data Source = Sulimn.accdb";

        internal static string AdminPassword = "";
        internal static Hero CurrentHero = new Hero();
        internal static Enemy CurrentEnemy = new Enemy();
        internal static Hero MaximumStatsHero = new Hero();
        internal static List<Enemy> AllEnemies = new List<Enemy>();
        internal static List<Item> AllItems = new List<Item>();
        internal static List<Spell> AllSpells = new List<Spell>();

        /// <summary>
        /// Loads almost everything from the database.
        /// </summary>
        internal static async void LoadAll()
        {
            OleDbConnection con = new OleDbConnection();
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();
            con.ConnectionString = _DBPROVIDERANDSOURCE;

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    string sql = "SELECT * FROM Armor";
                    string table = "Items";
                    da = new OleDbDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Armor newArmor = new Armor(ds.Tables[0].Rows[i]["ArmorName"].ToString(), ds.Tables[0].Rows[i]["ArmorType"].ToString(), ds.Tables[0].Rows[i]["ArmorDescription"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newArmor);
                        }
                    }

                    sql = "SELECT * FROM Admin";
                    ds = new DataSet();
                    da = new OleDbDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                        AdminPassword = ds.Tables[0].Rows[0]["AdminPassword"].ToString();

                    sql = "SELECT * FROM Food";
                    ds = new DataSet();
                    da = new OleDbDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Food newFood = new Food(ds.Tables[0].Rows[i]["FoodName"].ToString(), ds.Tables[0].Rows[i]["FoodType"].ToString(), ds.Tables[0].Rows[i]["FoodDescription"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["FoodAmount"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["FoodWeight"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["FoodValue"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newFood);
                        }
                    }

                    sql = "SELECT * FROM Potions";
                    ds = new DataSet();
                    da = new OleDbDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Potion newPotion = new Potion(ds.Tables[0].Rows[i]["PotionName"].ToString(), ds.Tables[0].Rows[i]["PotionType"].ToString(), ds.Tables[0].Rows[i]["PotionDescription"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["PotionAmount"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["PotionValue"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newPotion);
                        }
                    }

                    sql = "SELECT * FROM Spells";
                    ds = new DataSet();
                    da = new OleDbDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Spell newSpell = new Spell(ds.Tables[0].Rows[i]["SpellName"].ToString(), ds.Tables[0].Rows[i]["SpellType"].ToString(), ds.Tables[0].Rows[i]["SpellDescription"].ToString(), ds.Tables[0].Rows[i]["ReqClass"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["ReqLevel"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["MagicCost"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["SpellAmount"]));

                            AllSpells.Add(newSpell);
                        }
                    }

                    sql = "SELECT * FROM Weapons";
                    ds = new DataSet();
                    da = new OleDbDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Weapon newWeapon = new Weapon(ds.Tables[0].Rows[i]["WeaponName"].ToString(), ds.Tables[0].Rows[i]["WeaponType"].ToString(), ds.Tables[0].Rows[i]["WeaponDescription"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["WeaponDamage"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["WeaponWeight"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["WeaponValue"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]), BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            AllItems.Add(newWeapon);
                        }
                    }

                    sql = "SELECT * FROM Enemies";
                    ds = new DataSet();
                    da = new OleDbDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Weapon weapon = new Weapon();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["EnemyWeapon"].ToString()))
                                weapon = (Weapon)AllItems.Find(wpn => wpn.Name == (ds.Tables[0].Rows[i]["EnemyWeapon"].ToString()));
                            Armor head = new Armor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["EnemyHead"].ToString()))
                                head = (Armor)AllItems.Find(armr => armr.Name == (ds.Tables[0].Rows[i]["EnemyHead"].ToString()));
                            Armor body = new Armor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["EnemyBody"].ToString()))
                                body = (Armor)AllItems.Find(armr => armr.Name == (ds.Tables[0].Rows[i]["EnemyBody"].ToString()));
                            Armor legs = new Armor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["EnemyLegs"].ToString()))
                                legs = (Armor)AllItems.Find(armr => armr.Name == (ds.Tables[0].Rows[i]["EnemyLegs"].ToString()));
                            Armor feet = new Armor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["EnemyFeet"].ToString()))
                                feet = (Armor)AllItems.Find(armr => armr.Name == (ds.Tables[0].Rows[i]["EnemyFeet"].ToString()));

                            int gold = Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyGold"]);

                            Enemy newEnemy = new Enemy(ds.Tables[0].Rows[i]["EnemyName"].ToString(), ds.Tables[0].Rows[i]["EnemyType"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyLevel"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyExp"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyStrength"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyVitality"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyDexterity"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyWisdom"]), gold, Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyCurrHealth"]), Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyMaxHealth"]), weapon, head, body, legs, feet);

                            AllEnemies.Add(newEnemy);
                        }

                        sql = "SELECT * FROM MaxHeroStats";
                        ds = new DataSet();
                        da = new OleDbDataAdapter(sql, con);
                        da.Fill(ds, table);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MaximumStatsHero.Level = Int32Helper.Parse(ds.Tables[0].Rows[0]["Level"]);
                            MaximumStatsHero.Experience = Int32Helper.Parse(ds.Tables[0].Rows[0]["Experience"]);
                            MaximumStatsHero.SkillPoints = Int32Helper.Parse(ds.Tables[0].Rows[0]["SkillPoints"]);
                            MaximumStatsHero.Strength = Int32Helper.Parse(ds.Tables[0].Rows[0]["Strength"]);
                            MaximumStatsHero.Vitality = Int32Helper.Parse(ds.Tables[0].Rows[0]["Vitality"]);
                            MaximumStatsHero.Dexterity = Int32Helper.Parse(ds.Tables[0].Rows[0]["Dexterity"]);
                            MaximumStatsHero.Wisdom = Int32Helper.Parse(ds.Tables[0].Rows[0]["Wisdom"]);
                            MaximumStatsHero.Gold = Int32Helper.Parse(ds.Tables[0].Rows[0]["Gold"]);
                            MaximumStatsHero.CurrentHealth = Int32Helper.Parse(ds.Tables[0].Rows[0]["CurrentHealth"]);
                            MaximumStatsHero.MaximumHealth = Int32Helper.Parse(ds.Tables[0].Rows[0]["MaximumHealth"]);
                            MaximumStatsHero.CurrentMagic = Int32Helper.Parse(ds.Tables[0].Rows[0]["CurrentMagic"]);
                            MaximumStatsHero.MaximumMagic = Int32Helper.Parse(ds.Tables[0].Rows[0]["MaximumMagic"]);
                        }
                    }

                    AllItems = AllItems.OrderBy(item => item.Name).ToList();
                    AllEnemies = AllEnemies.OrderBy(enemy => enemy.Name).ToList();
                    AllSpells = AllSpells.OrderBy(spell => spell.Name).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Filling DataSet", MessageBoxButton.OK);
                }
                finally { con.Close(); }
            });
        }

        /// <summary>
        /// Saves Hero to database.
        /// </summary>
        internal static async void SaveHero()
        {
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = _DBPROVIDERANDSOURCE;
            string sql = "UPDATE Players SET [Level] = @level, [Experience] = @exp, [SkillPoints] = @skillPts, [Strength] = @str, [Vitality] = @vit, [Dexterity] = @dex, [Wisdom] = @wis, [Gold] = @gold, [CurrHealth] = @currHealth, [MaxHealth] = @maxHealth, [CurrMagic] = @currMagic, [MaxMagic] = @maxMagic, [KnownSpells] = @spells, [Weapon] = @weapon, [Head] = @head, [Body] = @body, [Legs] = @legs, [Feet] = @feet, [Inventory] = @inv WHERE [CharacterName] = @name";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@level", CurrentHero.Level);
            cmd.Parameters.AddWithValue("@exp", CurrentHero.Experience.ToString());
            cmd.Parameters.AddWithValue("@skillPts", CurrentHero.SkillPoints.ToString());
            cmd.Parameters.AddWithValue("@str", CurrentHero.Strength.ToString());
            cmd.Parameters.AddWithValue("@vit", CurrentHero.Vitality.ToString());
            cmd.Parameters.AddWithValue("@dex", CurrentHero.Dexterity.ToString());
            cmd.Parameters.AddWithValue("@wis", CurrentHero.Wisdom.ToString());
            cmd.Parameters.AddWithValue("@gold", CurrentHero.Gold.ToString());
            cmd.Parameters.AddWithValue("@currHealth", CurrentHero.CurrentHealth.ToString());
            cmd.Parameters.AddWithValue("@maxHealth", CurrentHero.MaximumHealth.ToString());
            cmd.Parameters.AddWithValue("@currMagic", CurrentHero.CurrentMagic.ToString());
            cmd.Parameters.AddWithValue("@maxMagic", CurrentHero.MaximumMagic.ToString());
            cmd.Parameters.AddWithValue("@spells", CurrentHero.Spellbook.ToString());
            cmd.Parameters.AddWithValue("@weapon", CurrentHero.Weapon.Name);
            cmd.Parameters.AddWithValue("@head", CurrentHero.Head.Name);
            cmd.Parameters.AddWithValue("@body", CurrentHero.Body.Name);
            cmd.Parameters.AddWithValue("@legs", CurrentHero.Legs.Name);
            cmd.Parameters.AddWithValue("@feet", CurrentHero.Feet.Name);
            cmd.Parameters.AddWithValue("@inv", CurrentHero.Inventory.ToString());
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
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection con = new OleDbConnection();
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

        #region Exploration Events

        /// <summary>
        /// Event where the Hero finds gold.
        /// </summary>
        internal static string EventFindGold(int minGold, int maxGold)
        {
            int foundGold = Functions.GenerateRandomNumber(minGold, maxGold);
            CurrentHero.Gold += foundGold;
            SaveHero();
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
            availableItems = GameState.AllItems.Where(x => x.Value >= minValue && x.Value <= maxValue && x.IsSold == true).ToList();
            int item = Functions.GenerateRandomNumber(0, availableItems.Count - 1);

            CurrentHero.Inventory.AddItem(availableItems[item]);
            SaveHero();
            return "You find a " + availableItems[item].Name + "!";
        }

        internal static void EventFindItem(params string[] names)
        {
            List<Item> availableItems = new List<Item>();
            foreach (string name in names)
                availableItems.Add(GetItem(name));
            int item = Functions.GenerateRandomNumber(0, availableItems.Count - 1);

            CurrentHero.Inventory.AddItem(availableItems[item]);

            SaveHero();
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
            if (CurrentEnemy.Gold > 0)
                CurrentEnemy.Gold = Functions.GenerateRandomNumber(CurrentEnemy.Gold / 2, CurrentEnemy.Gold);
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
            if (CurrentEnemy.Gold > 0)
                CurrentEnemy.Gold = Functions.GenerateRandomNumber(CurrentEnemy.Gold / 2, CurrentEnemy.Gold);
        }

        /// <summary>
        /// Event where the Hero encounters a water stream and restores health and magic.
        /// </summary>
        /// <returns>String saying Hero has been healed</returns>
        internal static string EventEncounterStream()
        {
            CurrentHero.CurrentHealth = CurrentHero.MaximumHealth;
            CurrentHero.CurrentMagic = CurrentHero.MaximumMagic;

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