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
                            Armor newArmor = new Armor(ds.Tables[0].Rows[i]["ArmorName"].ToString(), ds.Tables[0].Rows[i]["ArmorType"].ToString(), ds.Tables[0].Rows[i]["ArmorDescription"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"].ToString()), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"].ToString()), Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"].ToString()), Convert.ToBoolean(ds.Tables[0].Rows[i]["CanSell"]));

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
                            Food newFood = new Food(ds.Tables[0].Rows[i]["FoodName"].ToString(), ds.Tables[0].Rows[i]["FoodType"].ToString(), ds.Tables[0].Rows[i]["FoodDescription"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["FoodAmount"].ToString()), Int32Helper.Parse(ds.Tables[0].Rows[i]["FoodWeight"].ToString()), Int32Helper.Parse(ds.Tables[0].Rows[i]["FoodValue"].ToString()), Convert.ToBoolean(ds.Tables[0].Rows[i]["CanSell"]));

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
                            Potion newPotion = new Potion(ds.Tables[0].Rows[i]["PotionName"].ToString(), ds.Tables[0].Rows[i]["PotionType"].ToString(), ds.Tables[0].Rows[i]["PotionDescription"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["PotionAmount"].ToString()), Int32Helper.Parse(ds.Tables[0].Rows[i]["PotionValue"].ToString()), Convert.ToBoolean(ds.Tables[0].Rows[i]["CanSell"]));

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
                            Spell newSpell = new Spell(ds.Tables[0].Rows[i]["SpellName"].ToString(), ds.Tables[0].Rows[i]["SpellType"].ToString(), ds.Tables[0].Rows[i]["SpellDescription"].ToString(), ds.Tables[0].Rows[i]["ReqClass"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["ReqLevel"].ToString()), Int32Helper.Parse(ds.Tables[0].Rows[i]["MagicCost"].ToString()), Int32Helper.Parse(ds.Tables[0].Rows[i]["SpellAmount"].ToString()));

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
                            Weapon newWeapon = new Weapon(ds.Tables[0].Rows[i]["WeaponName"].ToString(), ds.Tables[0].Rows[i]["WeaponType"].ToString(), ds.Tables[0].Rows[i]["WeaponDescription"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["WeaponDamage"].ToString()), Int32Helper.Parse(ds.Tables[0].Rows[i]["WeaponWeight"].ToString()), Int32Helper.Parse(ds.Tables[0].Rows[i]["WeaponValue"].ToString()), Convert.ToBoolean(ds.Tables[0].Rows[i]["CanSell"]));

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

                            int gold = Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyGold"].ToString());

                            Enemy newEnemy = new Enemy(ds.Tables[0].Rows[i]["EnemyName"].ToString(), ds.Tables[0].Rows[i]["EnemyType"].ToString(), Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyLevel"].ToString()), Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyExp"].ToString()), Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyStrength"].ToString()), Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyVitality"].ToString()), Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyDexterity"].ToString()), Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyWisdom"].ToString()), gold, Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyCurrHealth"].ToString()), Int32Helper.Parse(ds.Tables[0].Rows[i]["EnemyMaxHealth"].ToString()), currWeapon, head, body, legs, feet);

                            AllEnemies.Add(newEnemy);
                        }

                        sql = "SELECT * FROM MaxHeroStats";
                        ds = new DataSet();
                        da = new OleDbDataAdapter(sql, con);
                        da.Fill(ds, table);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MaximumStatsHero.Level = Int32Helper.Parse(ds.Tables[0].Rows[0]["Level"].ToString());
                            MaximumStatsHero.Experience = Int32Helper.Parse(ds.Tables[0].Rows[0]["Experience"].ToString());
                            MaximumStatsHero.SkillPoints = Int32Helper.Parse(ds.Tables[0].Rows[0]["SkillPoints"].ToString());
                            MaximumStatsHero.Strength = Int32Helper.Parse(ds.Tables[0].Rows[0]["Strength"].ToString());
                            MaximumStatsHero.Vitality = Int32Helper.Parse(ds.Tables[0].Rows[0]["Vitality"].ToString());
                            MaximumStatsHero.Dexterity = Int32Helper.Parse(ds.Tables[0].Rows[0]["Dexterity"].ToString());
                            MaximumStatsHero.Wisdom = Int32Helper.Parse(ds.Tables[0].Rows[0]["Wisdom"].ToString());
                            MaximumStatsHero.Gold = Int32Helper.Parse(ds.Tables[0].Rows[0]["Gold"].ToString());
                            MaximumStatsHero.CurrentHealth = Int32Helper.Parse(ds.Tables[0].Rows[0]["CurrentHealth"].ToString());
                            MaximumStatsHero.MaximumHealth = Int32Helper.Parse(ds.Tables[0].Rows[0]["MaximumHealth"].ToString());
                            MaximumStatsHero.CurrentMagic = Int32Helper.Parse(ds.Tables[0].Rows[0]["CurrentMagic"].ToString());
                            MaximumStatsHero.MaximumMagic = Int32Helper.Parse(ds.Tables[0].Rows[0]["MaximumMagic"].ToString());
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
    }
}