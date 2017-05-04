namespace Sulimn
{
    /// <summary>Represents a piece of Armor worn on the head.</summary>
    internal class HeadArmor : Armor
    {
        #region Constructors

        /// <summary>Initializes a default instance of HeadArmor.</summary>
        internal HeadArmor()
        {
        }

        /// <summary>Initializes an instance of HeadArmor by assigning Properties.</summary>
        /// <param name="name">Name of HeadArmor</param>
        /// <param name="description">Description of HeadArmor</param>
        /// <param name="defense">Defense of HeadArmor</param>
        /// <param name="weight">Weight of HeadArmor</param>
        /// <param name="value">Value of HeadArmor</param>
        /// <param name="canSell">Can Sell HeadArmor?</param>
        /// <param name="isSold">Is HeadArmor Sold?</param>
        internal HeadArmor(string name, string description, int defense, int weight, int value,
        bool canSell, bool isSold)
        {
            Name = name;
            Type = ItemTypes.Head;
            Description = description;
            Defense = defense;
            Weight = weight;
            Value = value;
            CanSell = canSell;
            IsSold = isSold;
        }

        /// <summary>Replaces this instance of HeadArmor with another instance.</summary>
        /// <param name="otherArmor">Instance of HeadArmor to replace this one</param>
        internal HeadArmor(HeadArmor otherArmor)
        {
            Name = otherArmor.Name;
            Type = ItemTypes.Head;
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