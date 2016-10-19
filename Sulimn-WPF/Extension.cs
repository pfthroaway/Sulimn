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

    internal static class Int32Helper
    {
        internal static int Parse(string text)
        {
            int temp = 0;
            int.TryParse(text, out temp);
            return temp;
        }

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

        internal static int Parse(object obj)
        {
            int temp = 0;
            int.TryParse(obj.ToString(), out temp);
            return temp;
        }
    }

    internal static class BoolHelper
    {
        internal static bool Parse(string text)
        {
            bool temp = false;
            bool.TryParse(text, out temp);
            return temp;
        }

        internal static bool Parse(object obj)
        {
            bool temp = false;
            bool.TryParse(obj.ToString(), out temp);
            return temp;
        }
    }

    internal static class DoubleHelper
    {
        internal static double Parse(string text)
        {
            double temp = 0;
            double.TryParse(text, out temp);
            return temp;
        }
    }

    internal static class MyExtensions
    {
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