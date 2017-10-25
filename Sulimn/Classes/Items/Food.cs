using Sulimn.Classes.Enums;
using System;

namespace Sulimn.Classes.Items
{
    /// <summary>Represents a Food which the Hero can consume.</summary>
    internal class Food : Item, IEquatable<Food>
    {
        #region Properties

        /// <summary>Type of food.</summary>
        public FoodTypes FoodType { get; }

        /// <summary>Amount food heals you.</summary>
        public int Amount { get; }

        #endregion Properties

        #region Helper Properties

        /// <summary>Returns text relating to the type and amount of the Potion</summary>
        public string TypeAmount => !string.IsNullOrWhiteSpace(Name)
            ? FoodType == FoodTypes.Food
                ? $"Restores {Amount:N0} Health."
                : $"Restores {Amount:N0} Magic."
            : "";

        #endregion Helper Properties

        #region Override Operators

        private static bool Equals(Food left, Food right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) &&
                   left.FoodType == right.FoodType &&
                   string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) &&
                   left.Amount == right.Amount && left.Weight == right.Weight && left.Value == right.Value &&
                   left.CanSell == right.CanSell && left.IsSold == right.IsSold;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Food);

        public bool Equals(Food otherFood) => Equals(this, otherFood);

        public static bool operator ==(Food left, Food right) => Equals(left, right);

        public static bool operator !=(Food left, Food right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        public sealed override string ToString() => Name;

        #endregion Override Operators

        #region Constructors

        /// <summary>/// Initializes a default instance of Food./// </summary>
        internal Food()
        {
        }

        /// <summary>Initializes an instance of Food by assigning Properties.</summary>
        /// <param name="name">Name of Food</param>
        /// <param name="foodType">Type of Food</param>
        /// <param name="description">Description of Food</param>
        /// <param name="amount">Amount of Food</param>
        /// <param name="weight">Weight of Food</param>
        /// <param name="value">Value of Food</param>
        /// <param name="canSell">Can Food be sold?</param>
        /// <param name="isSold">Is Food sold?</param>
        internal Food(string name, FoodTypes foodType, string description, int amount, int weight, int value,
        bool canSell, bool isSold)
        {
            Name = name;
            FoodType = foodType;
            Description = description;
            Weight = weight;
            Value = value;
            Amount = amount;
            CanSell = canSell;
            IsSold = isSold;
        }

        /// <summary>Replaces this instance of Food with another instance.</summary>
        /// <param name="otherFood">Instance of Food to replace this instance</param>
        internal Food(Food otherFood) : this(otherFood.Name, otherFood.FoodType, otherFood.Description, otherFood.Amount, otherFood.Weight, otherFood.Value, otherFood.CanSell, otherFood.IsSold)
        {
        }

        #endregion Constructors
    }
}