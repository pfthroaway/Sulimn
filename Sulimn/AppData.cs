using System;
using System.IO;
using System.Reflection;

namespace Sulimn
{
    public static class AppData
    {
        internal static string Location = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Sulimn");
    }
}