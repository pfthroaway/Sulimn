namespace Sulimn.Classes.Items
{
    /// <summary>Represents a piece of Armor worn on the feet.</summary>
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
            Description = description;
            Defense = defense;
            Weight = weight;
            Value = value;
            CanSell = canSell;
            IsSold = isSold;
        }

        /// <summary>Replaces this instance of FeetArmor with another instance.</summary>
        /// <param name="other">Instance of FeetArmor to replace this one</param>
        internal FeetArmor(FeetArmor other) : this(other.Name, other.Description, other.Defense, other.Weight, other.Value,
            other.CanSell, other.IsSold)
        {
        }

        #endregion Constructors
    }
}