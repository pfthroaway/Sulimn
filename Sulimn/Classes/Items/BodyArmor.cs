namespace Sulimn.Classes.Items
{
    /// <summary>Represents a piece of Armor worn on the head.</summary>
    internal class BodyArmor : Armor
    {
        #region Constructors

        /// <summary>Initializes a default instance of BodyArmor.</summary>
        internal BodyArmor()
        {
        }

        /// <summary>Initializes an instance of BodyArmor by assigning Properties.</summary>
        /// <param name="name">Name of BodyArmor</param>
        /// <param name="description">Description of BodyArmor</param>
        /// <param name="defense">Defense of BodyArmor</param>
        /// <param name="weight">Weight of BodyArmor</param>
        /// <param name="value">Value of BodyArmor</param>
        /// <param name="canSell">Can Sell BodyArmor?</param>
        /// <param name="isSold">Is BodyArmor Sold?</param>
        internal BodyArmor(string name, string description, int defense, int weight, int value,
        bool canSell, bool isSold)
        {
            Name = name;
            Description = description;
            Defense = defense;
            Weight = weight;
            Value = value;
            CanSell = canSell;
            IsSold = isSold;
        }

        /// <summary>Replaces this instance of BodyArmor with another instance.</summary>
        /// <param name="other">Instance of BodyArmor to replace this one</param>
        internal BodyArmor(BodyArmor other) : this(other.Name, other.Description, other.Defense, other.Weight, other.Value,
            other.CanSell, other.IsSold)
        {
        }

        #endregion Constructors
    }
}