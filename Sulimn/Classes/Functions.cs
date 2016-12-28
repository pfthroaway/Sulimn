using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
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
        /// <returns>Returns list of Keys' IsKeyDown state</returns>
        private static IEnumerable<bool> GetListOfKeys(params Key[] keys)
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
        /// <param name="keyType">Type of input allowed</param>
        internal static void TextBoxTextChanged(object sender, KeyType keyType)
        {
            TextBox txt = (TextBox)sender;
            switch (keyType)
            {
                case KeyType.DecimalNumbers:
                    break;

                case KeyType.Letters:
                    txt.Text = new string((from c in txt.Text
                                           where char.IsLetter(c)
                                           select c).ToArray());
                    break;

                case KeyType.NegativeDecimalNumbers:
                    break;

                case KeyType.NegativeNumbers:
                    break;

                case KeyType.Numbers:
                    txt.Text = new string((from c in txt.Text
                                           where char.IsDigit(c)
                                           select c).ToArray());
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(keyType), keyType, null);
            }
            txt.CaretIndex = txt.Text.Length;
        }

        /// <summary>Previews a pressed key and determines whether or not it is acceptable input.</summary>
        /// <param name="e">Key being pressed</param>
        /// <param name="keyType">Type of input allowed</param>
        internal static void PreviewKeyDown(KeyEventArgs e, KeyType keyType)
        {
            Key k = e.Key;

            IEnumerable<bool> keys = GetListOfKeys(Key.Back, Key.Delete, Key.Home, Key.End, Key.LeftShift, Key.RightShift, Key.Enter, Key.Tab, Key.LeftAlt, Key.RightAlt, Key.Left, Key.Right, Key.LeftCtrl, Key.RightCtrl, Key.Escape);

            switch (keyType)
            {
                case KeyType.DecimalNumbers:
                    break;

                case KeyType.Letters:
                    e.Handled = !keys.Any(key => key) && (Key.A > k || k > Key.Z);
                    break;

                case KeyType.NegativeDecimalNumbers:
                    break;

                case KeyType.NegativeNumbers:
                    break;

                case KeyType.Numbers:
                    e.Handled = !keys.Any(key => key) && (Key.D0 > k || k > Key.D9) && (Key.NumPad0 > k || k > Key.NumPad9);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(keyType), keyType, null);
                    //&& !(Key.D0 <= k && k <= Key.D9) && !(Key.NumPad0 <= k && k <= Key.NumPad9))
                    //|| k == Key.OemMinus || k == Key.Subtract || k == Key.Decimal || k == Key.OemPeriod)
                    //System.Media.SystemSound ss = System.Media.SystemSounds.Beep;
                    //ss.Play();
            }
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