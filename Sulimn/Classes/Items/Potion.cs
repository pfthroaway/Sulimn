namespace Sulimn.Classes.Items
{
    /// <summary>Represents a <see cref="Potion"/> which the <see cref="Hero"/> can consume.</summary>
    internal class Potion : Consumable
    {
        #region Constructors

        /// <summary>/// Initializes a default instance of <see cref="Potion"/>./// </summary>
        internal Potion()
        {
        }

        /// <summary>Initializes an instance of <see cref="Potion"/> by assigning Properties.</summary>
        /// <param name="name">Name of <see cref="Potion"/></param>
        /// <param name="description">Description of <see cref="Potion"/></param>
        /// <param name="restoreHealth">Amount of Health restored</param>
        /// <param name="restoreMagic">Amount of Magic restored</param>
        /// <param name="cures">Cures?</param>
        /// <param name="weight">Weight of <see cref="Potion"/></param>
        /// <param name="value">Value of <see cref="Potion"/></param>
        /// <param name="canSell">Can <see cref="Potion"/> be sold?</param>
        /// <param name="isSold">Is <see cref="Potion"/> sold?</param>
        internal Potion(string name, string description, int restoreHealth, int restoreMagic, bool cures, int weight, int value,
        bool canSell, bool isSold)
        {
            Name = name;
            Description = description;
            RestoreHealth = restoreHealth;
            RestoreMagic = restoreMagic;
            Cures = cures;
            Weight = weight;
            Value = value;
            CanSell = canSell;
            IsSold = isSold;
        }

        /// <summary>Replaces this instance of <see cref="Potion"/> with another instance.</summary>
        /// <param name="other">Instance of <see cref="Potion"/> to replace this instance</param>
        internal Potion(Potion other) : this(other.Name, other.Description, other.RestoreHealth, other.RestoreMagic, other.Cures, other.Weight, other.Value, other.CanSell, other.IsSold)
        {
        }

        #endregion Constructors
    }
}