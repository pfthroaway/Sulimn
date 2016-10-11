using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sulimn_WPF
{
    internal static class GameState
    {
        private const string _DBPROVIDERANDSOURCE = "Provider=Microsoft.ACE.oledb.12.0;Data Source = Sulimn.accdb";

        internal static Hero currentHero = new Hero();
        internal static Enemy currentEnemy = new Enemy();
        internal static Hero maximumStatsHero = new Hero();
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
                            Armor newArmor = new Armor(ds.Tables[0].Rows[i]["ArmorName"].ToString(), ds.Tables[0].Rows[i]["ArmorType"].ToString(), ds.Tables[0].Rows[i]["ArmorDescription"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["ArmorDefense"]), Convert.ToInt32(ds.Tables[0].Rows[i]["ArmorWeight"]), Convert.ToInt32(ds.Tables[0].Rows[i]["ArmorValue"]), Convert.ToBoolean(ds.Tables[0].Rows[i]["CanSell"]));

                            AllItems.Add(newArmor);
                        }
                    }

                    sql = "SELECT * FROM Food";
                    ds = new DataSet();
                    da = new OleDbDataAdapter(sql, con);
                    da.Fill(ds, table);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Food newFood = new Food(ds.Tables[0].Rows[i]["FoodName"].ToString(), ds.Tables[0].Rows[i]["FoodType"].ToString(), ds.Tables[0].Rows[i]["FoodDescription"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["FoodAmount"]), Convert.ToInt32(ds.Tables[0].Rows[i]["FoodWeight"]), Convert.ToInt32(ds.Tables[0].Rows[i]["FoodValue"]), Convert.ToBoolean(ds.Tables[0].Rows[i]["CanSell"]));

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
                            Potion newPotion = new Potion(ds.Tables[0].Rows[i]["PotionName"].ToString(), ds.Tables[0].Rows[i]["PotionType"].ToString(), ds.Tables[0].Rows[i]["PotionDescription"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["PotionAmount"]), Convert.ToInt32(ds.Tables[0].Rows[i]["PotionValue"]), Convert.ToBoolean(ds.Tables[0].Rows[i]["CanSell"]));

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
                            Spell newSpell = new Spell(ds.Tables[0].Rows[i]["SpellName"].ToString(), ds.Tables[0].Rows[i]["SpellType"].ToString(), ds.Tables[0].Rows[i]["SpellDescription"].ToString(), ds.Tables[0].Rows[i]["ReqClass"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["ReqLevel"]), Convert.ToInt32(ds.Tables[0].Rows[i]["MagicCost"]), Convert.ToInt32(ds.Tables[0].Rows[i]["SpellAmount"]));

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
                            Weapon newWeapon = new Weapon(ds.Tables[0].Rows[i]["WeaponName"].ToString(), ds.Tables[0].Rows[i]["WeaponType"].ToString(), ds.Tables[0].Rows[i]["WeaponDescription"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["WeaponDamage"]), Convert.ToInt32(ds.Tables[0].Rows[i]["WeaponWeight"]), Convert.ToInt32(ds.Tables[0].Rows[i]["WeaponValue"]), Convert.ToBoolean(ds.Tables[0].Rows[i]["CanSell"]));

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
                            Weapon currWeapon = new Weapon();
                            if (ds.Tables[0].Rows[i]["EnemyWeapon"].ToString() != "")
                                currWeapon = (Weapon)AllItems.Find(x => x.Name == (ds.Tables[0].Rows[i]["EnemyWeapon"].ToString()));
                            Armor head = new Armor();
                            if (ds.Tables[0].Rows[i]["EnemyHead"].ToString() != "")
                                head = (Armor)AllItems.Find(x => x.Name == (ds.Tables[0].Rows[i]["EnemyHead"].ToString()));
                            Armor body = new Armor();
                            if (ds.Tables[0].Rows[i]["EnemyBody"].ToString() != "")
                                body = (Armor)AllItems.Find(x => x.Name == (ds.Tables[0].Rows[i]["EnemyBody"].ToString()));
                            Armor legs = new Armor();
                            if (ds.Tables[0].Rows[i]["EnemyLegs"].ToString() != "")
                                legs = (Armor)AllItems.Find(x => x.Name == (ds.Tables[0].Rows[i]["EnemyLegs"].ToString()));
                            Armor feet = new Armor();
                            if (ds.Tables[0].Rows[i]["EnemyFeet"].ToString() != "")
                                feet = (Armor)AllItems.Find(x => x.Name == (ds.Tables[0].Rows[i]["EnemyFeet"].ToString()));

                            int gold = Convert.ToInt32(ds.Tables[0].Rows[i]["EnemyGold"]);

                            Enemy newEnemy = new Enemy(ds.Tables[0].Rows[i]["EnemyName"].ToString(), ds.Tables[0].Rows[i]["EnemyType"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["EnemyLevel"]), Convert.ToInt32(ds.Tables[0].Rows[i]["EnemyExp"]), Convert.ToInt32(ds.Tables[0].Rows[i]["EnemyStrength"]), Convert.ToInt32(ds.Tables[0].Rows[i]["EnemyVitality"]), Convert.ToInt32(ds.Tables[0].Rows[i]["EnemyDexterity"]), Convert.ToInt32(ds.Tables[0].Rows[i]["EnemyWisdom"]), gold, Convert.ToInt32(ds.Tables[0].Rows[i]["EnemyCurrHealth"]), Convert.ToInt32(ds.Tables[0].Rows[i]["EnemyMaxHealth"]), currWeapon, head, body, legs, feet);

                            AllEnemies.Add(newEnemy);
                        }

                        sql = "SELECT * FROM MaxHeroStats";
                        ds = new DataSet();
                        da = new OleDbDataAdapter(sql, con);
                        da.Fill(ds, table);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            maximumStatsHero.Level = Convert.ToInt32(ds.Tables[0].Rows[0]["Level"]);
                            maximumStatsHero.Experience = Convert.ToInt32(ds.Tables[0].Rows[0]["Experience"]);
                            maximumStatsHero.SkillPoints = Convert.ToInt32(ds.Tables[0].Rows[0]["SkillPoints"]);
                            maximumStatsHero.Strength = Convert.ToInt32(ds.Tables[0].Rows[0]["Strength"]);
                            maximumStatsHero.Vitality = Convert.ToInt32(ds.Tables[0].Rows[0]["Vitality"]);
                            maximumStatsHero.Dexterity = Convert.ToInt32(ds.Tables[0].Rows[0]["Dexterity"]);
                            maximumStatsHero.Wisdom = Convert.ToInt32(ds.Tables[0].Rows[0]["Wisdom"]);
                            maximumStatsHero.Gold = Convert.ToInt32(ds.Tables[0].Rows[0]["Gold"]);
                            maximumStatsHero.CurrentHealth = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrentHealth"]);
                            maximumStatsHero.MaximumHealth = Convert.ToInt32(ds.Tables[0].Rows[0]["MaximumHealth"]);
                            maximumStatsHero.CurrentMagic = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrentMagic"]);
                            maximumStatsHero.MaximumMagic = Convert.ToInt32(ds.Tables[0].Rows[0]["MaximumMagic"]);
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
            cmd.Parameters.AddWithValue("@level", currentHero.Level);
            cmd.Parameters.AddWithValue("@exp", currentHero.Experience.ToString());
            cmd.Parameters.AddWithValue("@skillPts", currentHero.SkillPoints.ToString());
            cmd.Parameters.AddWithValue("@str", currentHero.Strength.ToString());
            cmd.Parameters.AddWithValue("@vit", currentHero.Vitality.ToString());
            cmd.Parameters.AddWithValue("@dex", currentHero.Dexterity.ToString());
            cmd.Parameters.AddWithValue("@wis", currentHero.Wisdom.ToString());
            cmd.Parameters.AddWithValue("@gold", currentHero.Gold.ToString());
            cmd.Parameters.AddWithValue("@currHealth", currentHero.CurrentHealth.ToString());
            cmd.Parameters.AddWithValue("@maxHealth", currentHero.MaximumHealth.ToString());
            cmd.Parameters.AddWithValue("@currMagic", currentHero.CurrentMagic.ToString());
            cmd.Parameters.AddWithValue("@maxMagic", currentHero.MaximumMagic.ToString());
            cmd.Parameters.AddWithValue("@spells", currentHero.Spellbook.ToString());
            cmd.Parameters.AddWithValue("@weapon", currentHero.Weapon.Name);
            cmd.Parameters.AddWithValue("@head", currentHero.Head.Name);
            cmd.Parameters.AddWithValue("@body", currentHero.Body.Name);
            cmd.Parameters.AddWithValue("@legs", currentHero.Legs.Name);
            cmd.Parameters.AddWithValue("@feet", currentHero.Feet.Name);
            cmd.Parameters.AddWithValue("@inv", currentHero.Inventory.ToString());
            cmd.Parameters.AddWithValue("@name", currentHero.Name);

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
            cmd.Parameters.AddWithValue("@name", currentHero.Name);

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
    }
}