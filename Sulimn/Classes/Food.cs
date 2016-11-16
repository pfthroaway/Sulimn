using System;

namespace Sulimn
{
    /// <summary>
    /// Represents a Food which the Hero can consume.
    /// </summary>
    internal class Food : Item, IEquatable<Food>
    {
        private FoodTypes _foodType;
        private int _amount;

        #region Properties

        public sealed override string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public sealed override ItemTypes Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public FoodTypes FoodType
        {
            get { return _foodType; }
            set { _foodType = value; }
        }

        public sealed override string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public sealed override int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public sealed override int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public sealed override bool CanSell
        {
            get { return _canSell; }
            set { _canSell = value; }
        }

        public sealed override bool IsSold
        {
            get { return _isSold; }
            set { _isSold = value; OnPropertyChanged("IsSold"); }
        }

        #endregion Properties

        #region Override Operators

        public static bool Equals(Food left, Food right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Type == right.Type && left.FoodType == right.FoodType && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) && (left.Amount == right.Amount) && (left.Weight == right.Weight) && (left.Value == right.Value) && (left.CanSell == right.CanSell) && (left.IsSold == right.IsSold);
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

        /// <summary>
        /// Initializes a default instance of Food.
        /// </summary>
        internal Food()
        {
        }

        /// <summary>
        /// Initializes an instance of Food by assigning Properties.
        /// </summary>
        /// <param name="foodName">Name of Food</param>
        /// <param name="foodType">Type of Food</param>
        /// <param name="foodDescription">Description of Food</param>
        /// <param name="foodAmount">Amount of Food</param>
        /// <param name="foodWeight">Weight of Food</param>
        /// <param name="foodValue">Value of Food</param>
        /// <param name="foodCanSell">Can Food be sold?</param>
        /// <param name="foodIsSold">Is Food sold?</param>
        internal Food(string foodName, FoodTypes foodType, string foodDescription, int foodAmount, int foodWeight, int foodValue, bool foodCanSell, bool foodIsSold)
        {
            Name = foodName;
            Type = ItemTypes.Food;
            FoodType = foodType;
            Description = foodDescription;
            Weight = foodWeight;
            Value = foodValue;
            Amount = foodAmount;
            CanSell = foodCanSell;
            IsSold = foodIsSold;
        }

        #endregion Constructors
    }
}