namespace Sulimn
{
    /// <summary>
    /// Represents a piece of Armor worn on the head.
    /// </summary>
    internal class LegArmor : Armor
    {
        #region Constructors

        /// <summary>Initializes a default instance of LegArmor.</summary>
        internal LegArmor()
        {
        }

        /// <summary>Initializes an instance of LegArmor by assigning Properties.</summary>
        /// <param name="name">Name of LegArmor</param>
        /// <param name="description">Description of LegArmor</param>
        /// <param name="defense">Defense of LegArmor</param>
        /// <param name="weight">Weight of LegArmor</param>
        /// <param name="value">Value of LegArmor</param>
        /// <param name="canSell">Can Sell LegArmor?</param>
        /// <param name="isSold">Is LegArmor Sold?</param>
        internal LegArmor(string name, string description, int defense, int weight, int value,
        bool canSell, bool isSold)
        {
            Name = name;
            Type = ItemTypes.Legs;
            Description = description;
            Defense = defense;
            Weight = weight;
            Value = value;
            CanSell = canSell;
            IsSold = isSold;
        }

        /// <summary>Replaces this instance of LegArmor with another instance.</summary>
        /// <param name="otherArmor">Instance of LegArmor to replace this one</param>
        internal LegArmor(LegArmor otherArmor)
        {
            Name = otherArmor.Name;
            Type = ItemTypes.Legs;
            Description = otherArmor.Description;
            Defense = otherArmor.Defense;
            Weight = otherArmor.Weight;
            Value = otherArmor.Value;
            CanSell = otherArmor.CanSell;
            IsSold = otherArmor.IsSold;
        }

        #endregion Constructors
    }
}