using System;

namespace Sulimn
{
    /// <summary>
    /// Represents a Potion which can be consumed by a Hero.
    /// </summary>
    internal class Potion : Item, IEquatable<Potion>
    {
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

        public PotionTypes PotionType { get; set; }

        public sealed override string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int Amount { get; set; }

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

        /// <summary>The value of the Potion with thousands separators</summary>
        public sealed override string ValueToString => Value.ToString("N0");

        /// <summary>The value of the Potion with thousands separators and preceding text</summary>
        public sealed override string ValueToStringWithText
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                    return "Value: " + ValueToString;
                return "";
            }
        }

        /// <summary>The value of the Potion</summary>
        public sealed override int SellValue => Value / 2;

        /// <summary>The value of the Potion with thousands separators</summary>
        public sealed override string SellValueToString => SellValue.ToString("N0");

        /// <summary>The value of the Potion with thousands separators with preceding text</summary>
        public sealed override string SellValueToStringWithText
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                    return "Sell Value: " + SellValueToString;
                return "";
            }
        }

        /// <summary>Returns text relating to the sellability of the Potion</summary>
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
                    switch (PotionType)
                    {
                        case PotionTypes.Healing:
                            return "Restores " + Amount + " Health.";

                        case PotionTypes.Magic:
                            return "Restores " + Amount + " Magic.";

                        case PotionTypes.Curing:
                            return "Cures any ailments.";
                    }
                return "";
            }
        }

        #endregion Helper Properties

        #region Override Operators

        public static bool Equals(Potion left, Potion right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Type == right.Type &&
             left.PotionType == right.PotionType &&
             string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) &&
             left.Amount == right.Amount && left.Weight == right.Weight && left.Value == right.Value &&
             left.CanSell == right.CanSell && left.IsSold == right.IsSold;
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(this, obj as Potion);
        }

        public bool Equals(Potion otherPotion)
        {
            return Equals(this, otherPotion);
        }

        public static bool operator ==(Potion left, Potion right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Potion left, Potion right)
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
        /// Initializes a default instance of Potion.
        /// </summary>
        internal Potion()
        {
        }

        /// <summary>
        /// Initializes an instance of Potion by assigning Properties.
        /// </summary>
        /// <param name="name">Name of Potion</param>
        /// <param name="potionType">Type of Potion</param>
        /// <param name="description">Description of Potion</param>
        /// <param name="amount">Amount of Potion</param>
        /// <param name="value">Value of Potion</param>
        /// <param name="canSell">Can Potion be sold?</param>
        /// <param name="isSold">Is Potion sold?</param>
        internal Potion(string name, PotionTypes potionType, string description, int amount, int value, bool canSell,
        bool isSold)
        {
            Name = name;
            Type = ItemTypes.Potion;
            PotionType = potionType;
            Description = description;
            Weight = 0;
            Value = value;
            Amount = amount;
            CanSell = canSell;
            IsSold = isSold;
        }

        /// <summary>Replaces this instance of Potion with another instance.</summary>
        /// <param name="otherPotion">Instance of Potion to replace this instance</param>
        internal Potion(Potion otherPotion)
        {
            Name = otherPotion.Name;
            Type = ItemTypes.Potion;
            PotionType = otherPotion.PotionType;
            Description = otherPotion.Description;
            Weight = 0;
            Value = otherPotion.Value;
            Amount = otherPotion.Amount;
            CanSell = otherPotion.CanSell;
            IsSold = otherPotion.IsSold;
        }

        #endregion Constructors
    }
}