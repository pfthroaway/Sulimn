namespace Sulimn.Classes
{
    /// <summary>Represents the game's <see cref="Settings"/>.</summary>
    internal class Settings
    {
        /// <summary>Hashed administrator password.</summary>
        public string AdminPassword { get; set; }

        /// <summary>Game theme.</summary>
        public string Theme { get; set; }

        /// <summary>Initializes an instance of <see cref="Settings"/>by assigning values to Properties.</summary>
        /// <param name="adminPassword">Hashed administrator password</param>
        /// <param name="theme">Game theme</param>
        public Settings(string adminPassword, string theme)
        {
            AdminPassword = adminPassword;
            Theme = theme;
        }
    }
}