using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading;
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

        #region Hero Database Manipulation

        // This method adds a new player to the database.
        internal static async void NewHero(Hero newChar)
        {
            newChar.Head.Name = "Cloth Helmet";
            newChar.Body.Name = "Cloth Shirt";
            newChar.Legs.Name = "Cloth Pants";
            newChar.Feet.Name = "Cloth Shoes";

            string spells = "";

            switch (newChar.ClassName)
            {
                case "Wizard":
                    newChar.Weapon.Name = "Starter Staff";
                    spells += "Fireball";
                    break;

                case "Cleric":
                    newChar.Weapon.Name = "Starter Staff";
                    spells += "Heal Self";
                    break;

                case "Warrior":
                    newChar.Weapon.Name = "Stone Dagger";
                    break;

                case "Rogue":
                    newChar.Weapon.Name = "Starter Bow";
                    break;

                default:
                    newChar.Weapon.Name = "Stone Dagger";
                    break;
            }

            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = _DBPROVIDERANDSOURCE;
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandText = "Insert into Players([CharacterName],[Class],[Level],[Experience],[SkillPoints],[Strength],[Vitality],[Dexterity],[Wisdom],[Gold],[CurrHealth],[MaxHealth],[CurrMagic],[MaxMagic],[KnownSpells],[Weapon],[Head],[Body],[Legs],[Feet],[Inventory])Values('" + newChar.Name + "','" + newChar.ClassName + "','" + newChar.Level + "','" + newChar.Experience + "','" + newChar.SkillPoints + "','" + newChar.Strength + "','" + newChar.Vitality + "','" + newChar.Dexterity + "','" + newChar.Wisdom + "','" + newChar.Gold + "','" + newChar.CurrentHealth + "','" + newChar.MaximumHealth + "','" + newChar.CurrentMagic + "','" + newChar.MaximumMagic + "','" + spells + "','" + newChar.Weapon.Name + "','" + newChar.Head.Name + "','" + newChar.Body.Name + "','" + newChar.Legs.Name + "','" + newChar.Feet.Name + "','Minor Healing Potion,Minor Healing Potion,Minor Healing Potion')";

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "Insert into Bank([CharacterName],[Gold],[LoanTaken])Values('" + newChar.Name + "',0,0)";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("New user was successfully added!", "Congrats", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Creating New Hero", MessageBoxButton.OK);
                }
                finally { con.Close(); }
            });
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