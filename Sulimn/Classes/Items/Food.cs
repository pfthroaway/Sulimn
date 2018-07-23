namespace Sulimn.Classes.Items
{
    /// <summary>Represents a <see cref="Food"/> which the <see cref="Hero"/> can consume.</summary>
    internal class Food : Consumable
    {
        #region Constructors

        /// <summary>/// Initializes a default instance of <see cref="Food" />./// </summary>
        internal Food()
        {
        }

        /// <summary>Initializes an instance of <see cref="Food" /> by assigning Properties.</summary>
        /// <param name="name">Name of <see cref="Food"/></param>
        /// <param name="description">Description of <see cref="Food"/></param>
        /// <param name="restoreHealth">Amount of Health restored</param>
        /// <param name="restoreMagic">Amount of Magic restored</param>
        /// <param name="cures">Cures?</param>
        /// <param name="weight">Weight of <see cref="Food"/></param>
        /// <param name="value">Value of <see cref="Food"/></param>
        /// <param name="canSell">Can <see cref="Food"/> be sold?</param>
        /// <param name="isSold">Is <see cref="Food"/> sold?</param>
        internal Food(string name, string description, int restoreHealth, int restoreMagic, bool cures, int weight, int value,
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

        /// <summary>Replaces this instance of <see cref="Food" /> with another instance.</summary>
        /// <param name="other">Instance of <see cref="Food" /> to replace this instance</param>
        internal Food(Food other) : this(other.Name, other.Description, other.RestoreHealth, other.RestoreMagic, other.Cures, other.Weight, other.Value, other.CanSell, other.IsSold)
        {
        }

        #endregion Constructors
    }
}