using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace Sulimn_WPF
{
    internal static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        /// <summary>
        /// Returns a Random based on this thread.
        /// </summary>
        internal static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }

    /// <summary>
    /// Extension class to more easily parse Integers.
    /// </summary>
    internal static class Int32Helper
    {
        /// <summary>
        /// Utilizes int.TryParse to easily Parse an Integer.
        /// </summary>
        /// <param name="text">Text to be parsed</param>
        /// <returns></returns>
        internal static int Parse(string text)
        {
            int temp = 0;
            int.TryParse(text, out temp);
            return temp;
        }

        /// <summary>
        /// Utilizes int.TryParse to easily Parse an Integer.
        /// </summary>
        /// <param name="dbl">Double to be parsed</param>
        /// <returns>Parsed integer</returns>
        internal static int Parse(double dbl)
        {
            int temp = 0;
            try
            {
                temp = (int)dbl;
            }
            catch (Exception e)
            { MessageBox.Show(e.Message, "Sulimn", MessageBoxButton.OK); }

            return temp;
        }

        /// <summary>
        /// Utilizes int.TryParse to easily Parse an Integer.
        /// </summary>
        /// <param name="dcml">Decimal to be parsed</param>
        /// <returns>Parsed integer</returns>
        internal static int Parse(decimal dcml)
        {
            int temp = 0;
            try
            {
                temp = (int)dcml;
            }
            catch (Exception e)
            { MessageBox.Show(e.Message, "Sulimn", MessageBoxButton.OK); }

            return temp;
        }

        /// <summary>
        /// Utilizes int.TryParse to easily Parse an Integer.
        /// </summary>
        /// <param name="obj">Object to be parsed</param>
        /// <returns>Parsed integer</returns>
        internal static int Parse(object obj)
        {
            int temp = 0;
            int.TryParse(obj.ToString(), out temp);
            return temp;
        }
    }

    /// <summary>
    /// Extension class to more easily parse Booleans.
    /// </summary>
    internal static class BoolHelper
    {
        /// <summary>
        /// Utilizes bool.TryParse to easily Parse a Boolean.
        /// </summary>
        /// <param name="text">Text to be parsed</param>
        /// <returns>Parsed Boolean</returns>
        internal static bool Parse(string text)
        {
            bool temp = false;
            bool.TryParse(text, out temp);
            return temp;
        }

        /// <summary>
        /// Utilizes bool.TryParse to easily Parse a Boolean.
        /// </summary>
        /// <param name="obj">Object to be parsed</param>
        /// <returns>Parsed Boolean</returns>
        internal static bool Parse(object obj)
        {
            bool temp = false;
            bool.TryParse(obj.ToString(), out temp);
            return temp;
        }
    }

    /// <summary>
    /// Extension class to more easily parse DateTimes.
    /// </summary>
    internal static class DateTimeHelper
    {
        /// <summary>
        /// Utilizes DateTime.TryParse to easily Parse a DateTime.
        /// </summary>
        /// <param name="text">Text to be parsed.</param>
        /// <returns>Parsed DateTime</returns>
        internal static DateTime Parse(string text)
        {
            DateTime temp = new DateTime();
            DateTime.TryParse(text, out temp);
            return temp;
        }

        /// <summary>
        /// Utilizes DateTime.TryParse to easily Parse a DateTime.
        /// </summary>
        /// <param name="obj">Object to be parsed</param>
        /// <returns>Parsed DateTime</returns>
        internal static DateTime Parse(object obj)
        {
            DateTime temp = new DateTime();
            DateTime.TryParse(obj.ToString(), out temp);
            return temp;
        }
    }

    /// <summary>
    /// Extension class to more easily parse Doubles.
    /// </summary>
    internal static class DoubleHelper
    {
        /// <summary>
        /// Utilizes double.TryParse to easily Parse a Double.
        /// </summary>
        /// <param name="text">Text to be parsed</param>
        /// <returns>Parsed Double</returns>
        internal static double Parse(string text)
        {
            double temp = 0;
            double.TryParse(text, out temp);
            return temp;
        }

        /// <summary>
        /// Utilizes double.TryParse to easily Parse a Double.
        /// </summary>
        /// <param name="obj">Object to be parsed</param>
        /// <returns>Parsed Double</returns>
        internal static double Parse(object obj)
        {
            double temp = 0;
            double.TryParse(obj.ToString(), out temp);
            return temp;
        }
    }

    internal static class MyExtensions
    {
        /// <summary>
        /// Shuffles a List.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="list">List Name</param>
        internal static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}