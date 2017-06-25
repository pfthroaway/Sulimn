using Sulimn.Classes.Enums;
using System;

namespace Sulimn.Classes.Items
{
    /// <summary>Represents a Food which the Hero can consume.</summary>
    internal class Food : Item, IEquatable<Food>
    {
        #region Properties

        public FoodTypes FoodType { get; }

        public int Amount { get; }

        #endregion Properties

        #region Helper Properties

        /// <summary>Returns text relating to the type and amount of the Potion</summary>
        public string TypeAmount
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                    if (FoodType == FoodTypes.Food)
                        return $"Restores {Amount:N0} Health.";
                    else
                        return $"Restores {Amount:N0} Magic.";
                return "";
            }
        }

        #endregion Helper Properties

        #region Override Operators

        private static bool Equals(Food left, Food right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Type == right.Type &&
            left.FoodType == right.FoodType &&
            string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) &&
            left.Amount == right.Amount && left.Weight == right.Weight && left.Value == right.Value &&
            left.CanSell == right.CanSell && left.IsSold == right.IsSold;
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(this, obj as Food);
        }

        public bool Equals(Food otherFood)
        {
            return Equals(this, otherFood);
        }

        public static bool operator ==(Food left, Food right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Food left, Food right)
        {
            return !Equals(left, right);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode() ^ 17;
        }

        public sealed override string ToString()
        {
            return Name;
        }

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
            Type = ItemTypes.Food;
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
        internal Food(Food otherFood)
        {
            Name = otherFood.Name;
            Type = ItemTypes.Food;
            FoodType = otherFood.FoodType;
            Description = otherFood.Description;
            Weight = otherFood.Weight;
            Value = otherFood.Value;
            Amount = otherFood.Amount;
            CanSell = otherFood.CanSell;
            IsSold = otherFood.IsSold;
        }

        #endregion Constructors
    }
}