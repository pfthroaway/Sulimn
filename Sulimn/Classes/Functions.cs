using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sulimn
{
    /// <summary>Represents various common functions which receive use throughout the application.</summary>
    internal static class Functions
    {
        private const string _DBPROVIDERANDSOURCE = "Data Source = Sulimn.sqlite;Version=3";

        /// <summary>Turns several Keyboard.Keys into a list of Keys which can be tested using List.Any.</summary>
        /// <param name="keys">Array of Keys</param>
        /// <returns></returns>
        internal static IEnumerable<bool> GetListOfKeys(params Key[] keys)
        {
            return keys.Select(Keyboard.IsKeyDown).ToList();
        }

        /// <summary>Selects all text in passed TextBox.</summary>
        /// <param name="sender">Object to be cast</param>
        internal static void TextBoxGotFocus(object sender)
        {
            TextBox txt = (TextBox)sender;
            txt.SelectAll();
        }

        /// <summary>Selects all text in passed PasswordBox.</summary>
        /// <param name="sender">Object to be cast</param>
        internal static void PasswordBoxGotFocus(object sender)
        {
            PasswordBox txt = (PasswordBox)sender;
            txt.SelectAll();
        }

        /// <summary>Deletes all text in textbox which isn't a letter.</summary>
        /// <param name="sender">Object to be cast</param>
        internal static void TextBoxLettersOnly(object sender)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = new string((from c in txt.Text
                                   where char.IsLetter(c)
                                   select c).ToArray());
            txt.CaretIndex = txt.Text.Length;
        }

        /// <summary>Deletes all text in textbox which isn't a number.</summary>
        /// <param name="sender">Object to be cast</param>
        internal static void TextBoxNumbersOnly(object sender)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = new string((from c in txt.Text
                                   where char.IsDigit(c)
                                   select c).ToArray());
            txt.CaretIndex = txt.Text.Length;
        }

        #region General Database Manipulation

        /// <summary>Fills a DataSet.</summary>
        /// <param name="sql"></param>
        /// <param name="tableName"></param>
        /// <returns>Returns a DataSet</returns>
        internal static async Task<DataSet> FillDataSet(string sql, string tableName)
        {
            SQLiteConnection con = new SQLiteConnection();
            SQLiteDataAdapter da;
            DataSet ds = new DataSet();
            con.ConnectionString = _DBPROVIDERANDSOURCE;

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    da = new SQLiteDataAdapter(sql, con);
                    da.Fill(ds, tableName);
                }
                catch (Exception ex)
                {
                    new Notification(ex.Message, "Error Filling DataSet", NotificationButtons.OK).ShowDialog();
                }
                finally
                {
                    con.Close();
                }
            });
            return ds;
        }

        #endregion General Database Manipulation

        #region Random Number Generation

        /// <summary>Generates a random number between min and max (inclusive).</summary>
        /// <param name="min">Inclusive minimum number</param>
        /// <param name="max">Inclusive maximum number</param>
        /// <param name="lowerLimit">Minimum limit for the method, regardless of min and max.</param>
        /// <param name="upperLimit">Maximum limit for the method, regardless of min and max.</param>
        /// <returns>Returns randomly generated integer between min and max with an upper limit of upperLimit.</returns>
        internal static int GenerateRandomNumber(int min, int max, int lowerLimit = int.MinValue, int upperLimit = int.MaxValue)
        {
            int result = min < max ? ThreadSafeRandom.ThisThreadsRandom.Next(min, max + 1) : ThreadSafeRandom.ThisThreadsRandom.Next(max, min + 1);

            return result < lowerLimit ? lowerLimit : (result > upperLimit ? upperLimit : result);
        }

        #endregion Random Number Generation
    }
}