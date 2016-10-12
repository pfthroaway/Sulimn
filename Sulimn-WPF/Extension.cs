using System;
using System.Collections.Generic;
using System.Threading;

namespace Sulimn_WPF
{
    internal static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        internal static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
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