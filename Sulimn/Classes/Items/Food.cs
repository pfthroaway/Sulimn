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
            private set { _foodType = value; }
        }

        public sealed override string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int Amount
        {
            get { return _amount; }
            private set { _amount = value; }
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
            set
            {
                _isSold = value;
                OnPropertyChanged("IsSold");
            }
        }

        #endregion Properties

        #region Helper Properties

        /// <summary>The value of the Food with thousands separators</summary>
        public sealed override string ValueToString => Value.ToString("N0");

        /// <summary>The value of the Food with thousands separators and preceding text</summary>
        public sealed override string ValueToStringWithText
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name)) return "Value: " + ValueToString;
                return "";
            }
        }

        /// <summary>The value of the Food</summary>
        public sealed override int SellValue => Value / 2;

        /// <summary>The value of the Food with thousands separators</summary>
        public sealed override string SellValueToString => SellValue.ToString("N0");

        /// <summary>The value of the Food with thousands separators with preceding text</summary>
        public sealed override string SellValueToStringWithText
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                    return "Sell Value: " + SellValueToString;
                return "";
            }
        }

        /// <summary>Returns text relating to the sellability of the Food</summary>
        public sealed override string CanSellToString
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    if (CanSell)
                        return "Sellable";
                    return "Not Sellable";
                }
                return "";
            }
        }

        /// <summary>Returns text relating to the type and amount of the Potion</summary>
        public string TypeAmount
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                    if (FoodType == FoodTypes.Food)
                        return "Restores " + Amount + " Health.";
                    else
                        return "Restores " + Amount + " Magic.";
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

        /// <summary>
        /// Initializes a default instance of Food.
        /// </summary>
        internal Food()
        {
        }

        /// <summary>
        /// Initializes an instance of Food by assigning Properties.
        /// </summary>
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
            Weight = 0;
            Value = otherFood.Value;
            Amount = otherFood.Amount;
            CanSell = otherFood.CanSell;
            IsSold = otherFood.IsSold;
        }

        #endregion Constructors
    }
}