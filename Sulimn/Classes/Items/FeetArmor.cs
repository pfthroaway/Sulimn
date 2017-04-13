namespace Sulimn
{
    /// <summary>
    /// Represents a piece of Armor worn on the head.
    /// </summary>
    internal class FeetArmor : Armor
    {
        #region Constructors

        /// <summary>Initializes a default instance of FeetArmor.</summary>
        internal FeetArmor()
        {
        }

        /// <summary>Initializes an instance of FeetArmor by assigning Properties.</summary>
        /// <param name="name">Name of FeetArmor</param>
        /// <param name="description">Description of FeetArmor</param>
        /// <param name="defense">Defense of FeetArmor</param>
        /// <param name="weight">Weight of FeetArmor</param>
        /// <param name="value">Value of FeetArmor</param>
        /// <param name="canSell">Can Sell FeetArmor?</param>
        /// <param name="isSold">Is FeetArmor Sold?</param>
        internal FeetArmor(string name, string description, int defense, int weight, int value,
        bool canSell, bool isSold)
        {
            Name = name;
            Type = ItemTypes.Feet;
            Description = description;
            Defense = defense;
            Weight = weight;
            Value = value;
            CanSell = canSell;
            IsSold = isSold;
        }

        /// <summary>Replaces this instance of FeetArmor with another instance.</summary>
        /// <param name="otherArmor">Instance of FeetArmor to replace this one</param>
        internal FeetArmor(FeetArmor otherArmor)
        {
            Name = otherArmor.Name;
            Type = ItemTypes.Feet;
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