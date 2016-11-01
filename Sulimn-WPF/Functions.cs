using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Sulimn_WPF
{
    internal static class Functions
    {
        private const string _DBPROVIDERANDSOURCE = "Provider=Microsoft.ACE.oledb.12.0;Data Source = Sulimn.accdb";

        #region Random Number Generation

        /// <summary>
        /// Generates a random number between min and max (inclusive).
        /// </summary>
        /// <param name="min">Inclusive minimum number</param>
        /// <param name="max">Inclusive maximum number</param>
        /// <returns>Returns randomly generated integer between min and max.</returns>
        internal static int GenerateRandomNumber(int min, int max)
        {
            return GenerateRandomNumber(min, max, Int32.MaxValue);
        }

        /// <summary>
        /// Generates a random number between min and max (inclusive).
        /// </summary>
        /// <param name="min">Inclusive minimum number</param>
        /// <param name="max">Inclusive maximum number</param>
        /// <param name="upperLimit">Maximum limit for the method, regardless of min and max.</param>
        /// <returns>Returns randomly generated integer between min and max with an upper limit of upperLimit.</returns>
        internal static int GenerateRandomNumber(int min, int max, int upperLimit)
        {
            int result;

            if (min < max)
                result = ThreadSafeRandom.ThisThreadsRandom.Next(min, max + 1);
            else
                result = ThreadSafeRandom.ThisThreadsRandom.Next(max, min + 1);

            if (result > upperLimit)
                return upperLimit;

            return result;
        }

        #endregion Random Number Generation

        /// <summary>
        /// Turns several Keyboard.Keys into a list of Keys which can be tested using List.Any.
        /// </summary>
        /// <param name="keys">Array of Keys</param>
        /// <returns></returns>
        internal static List<bool> GetListOfKeys(params Key[] keys)
        {
            List<bool> allKeys = new List<bool>();
            foreach (Key key in keys)
                allKeys.Add(Keyboard.IsKeyDown(key));
            return allKeys;
        }

        #region Hero Database Manipulation

        // This method adds a new player to the database.
        internal static async Task<bool> NewHero(Hero newHero)
        {
            bool success = false;
            newHero.Head.Name = "Cloth Helmet";
            newHero.Body.Name = "Cloth Shirt";
            newHero.Legs.Name = "Cloth Pants";
            newHero.Feet.Name = "Cloth Shoes";

            string spells = "";

            switch (newHero.ClassName)
            {
                case "Wizard":
                    newHero.Weapon.Name = "Starter Staff";
                    spells += "Fireball";
                    break;

                case "Cleric":
                    newHero.Weapon.Name = "Starter Staff";
                    spells += "Heal Self";
                    break;

                case "Warrior":
                    newHero.Weapon.Name = "Stone Dagger";
                    break;

                case "Rogue":
                    newHero.Weapon.Name = "Starter Bow";
                    break;

                default:
                    newHero.Weapon.Name = "Stone Dagger";
                    break;
            }

            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = _DBPROVIDERANDSOURCE;
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO Players([CharacterName],[CharacterPassword],[Class],[Level],[Experience],[SkillPoints],[Strength],[Vitality],[Dexterity],[Wisdom],[Gold],[CurrHealth],[MaxHealth],[CurrMagic],[MaxMagic],[KnownSpells],[Weapon],[Head],[Body],[Legs],[Feet],[Inventory])Values('" + newHero.Name + "','" + newHero.Password + "','" + newHero.ClassName + "','" + newHero.Level + "','" + newHero.Experience + "','" + newHero.SkillPoints + "','" + newHero.Strength + "','" + newHero.Vitality + "','" + newHero.Dexterity + "','" + newHero.Wisdom + "','" + newHero.Gold + "','" + newHero.CurrentHealth + "','" + newHero.MaximumHealth + "','" + newHero.CurrentMagic + "','" + newHero.MaximumMagic + "','" + spells + "','" + newHero.Weapon.Name + "','" + newHero.Head.Name + "','" + newHero.Body.Name + "','" + newHero.Legs.Name + "','" + newHero.Feet.Name + "','Minor Healing Potion,Minor Healing Potion,Minor Healing Potion')";

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Bank([CharacterName],[Gold],[LoanTaken])Values('" + newHero.Name + "',0,0)";
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

        #endregion Hero Database Manipulation

        #region General Database Manipulation

        // This method fills a DataSet with data from a table.
        internal static async Task<DataSet> FillDataSet(string sql, string tableName)
        {
            OleDbConnection con = new OleDbConnection();
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();
            con.ConnectionString = _DBPROVIDERANDSOURCE;

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    da = new OleDbDataAdapter(sql, con);
                    da.Fill(ds, tableName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Filling DataSet", MessageBoxButton.OK);
                }
                finally { con.Close(); }
            });
            return ds;
        }

        //// This method deletes a record in a database.
        //internal void DeleteRecord(string sql, string tableName, DataSet ds)
        //{
        //  string dbProvider = "Provider=Microsoft.ACE.oledb.12.0;";
        //  string dbSource = "Data Source=Players.accdb";
        //  OleDbConnection con = new OleDbConnection();
        //  OleDbDataAdapter da = new OleDbDataAdapter();
        //  OleDbCommandBuilder cb = new OleDbCommandBuilder(da);

        //  try
        //  {
        //    con.ConnectionString = dbProvider + dbSource;
        //    da = new OleDbDataAdapter(sql, con);
        //    ds.Tables[tableName].Rows[0].Delete();
        //    da.Update(ds, tableName);
        //  }
        //  catch (Exception e)
        //  {
        //    MessageBox.Show(e.Message, "Sulimn", MessageBoxButtons.OK);
        //  }
        //  finally { con.Close(); }
        //}

        //// This method updates a record in a database.
        //internal void UpdateRecord(string sql, string tableName, DataSet ds)
        //{
        //  string dbProvider = "Provider=Microsoft.ACE.oledb.12.0;";
        //  string dbSource = "Data Source=Players.accdb";
        //  OleDbConnection con = new OleDbConnection();
        //  OleDbDataAdapter da = new OleDbDataAdapter();
        //  OleDbCommandBuilder cb = new OleDbCommandBuilder(da);

        //  try
        //  {
        //    con.ConnectionString = dbProvider + dbSource;
        //    da = new OleDbDataAdapter(sql, con);
        //    da.UpdateCommand.CommandText = sql;
        //    da.Update(ds, tableName);
        //  }
        //  catch (Exception e)
        //  {
        //    MessageBox.Show(e.Message, "Sulimn", MessageBoxButtons.OK);
        //  }
        //  finally { con.Close(); }
        //}

        #endregion General Database Manipulation
    }
}