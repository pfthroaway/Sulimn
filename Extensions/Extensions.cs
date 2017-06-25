using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Extensions
{
    public static class MyExtensions
    {
        /// <summary>Adds multiple ranges to a List.</summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="list">List for others to be added to</param>
        /// <param name="lists">Lists to be added to list</param>
        public static void AddRanges<T>(this List<T> list, params IEnumerable<T>[] lists)
        {
            foreach (IEnumerable<T> currentList in lists)
                list.AddRange(currentList);
        }

        /// <summary>Determines if this character is a hyphen.</summary>
        /// <param name="c">Character to be evaluated</param>
        /// <returns>Returns true if character is a hyphen</returns>
        public static bool IsHyphen(this char c) => c.Equals('-');

        /// <summary>Determines if this character is a period.</summary>
        /// <param name="c">Character to be evaluated</param>
        /// <returns>Returns true if character is a period</returns>
        public static bool IsPeriod(this char c) => c.Equals('.');

        /// <summary>Determines if this character is a period or comma.</summary>
        /// <param name="c">Character to be evaluated</param>
        /// <returns>Returns true if character is a period or comma</returns>
        public static bool IsPeriodOrComma(this char c) => c.Equals('.') | c.Equals(',');

        /// <summary>Shuffles a List.</summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="list">List Name</param>
        public static void Shuffle<T>(this IList<T> list)
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

        /// <summary>Waits for an asynchronous Process to exit asynchronously.</summary>
        /// <param name="process">Process to be awaited</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        public static Task WaitForExitAsync(this Process process,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            process.EnableRaisingEvents = true;
            process.Exited += (sender, args) => tcs.TrySetResult(null);
            if (cancellationToken != default(CancellationToken))
                cancellationToken.Register(tcs.SetCanceled);

            return tcs.Task;
        }
    }
}