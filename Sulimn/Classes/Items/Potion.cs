using System;

namespace Sulimn
{
    /// <summary>Represents a Potion which can be consumed by a Hero.</summary>
    internal class Potion : Item, IEquatable<Potion>
    {
        #region Properties

        public PotionTypes PotionType { get; private set; }

        public int Amount { get; private set; }

        #endregion Properties

        #region Helper Properties

        /// <summary>Returns text relating to the type and amount of the Potion</summary>
        public string TypeAmount
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                    switch (PotionType)
                    {
                        case PotionTypes.Healing:
                            return $"Restores {Amount:N0} Health.";

                        case PotionTypes.Magic:
                            return $"Restores {Amount:N0} Magic.";

                        case PotionTypes.Curing:
                            return "Cures any ailments.";
                    }
                return "";
            }
        }

        #endregion Helper Properties

        #region Override Operators

        private static bool Equals(Potion left, Potion right)
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

        /// <summary>Initializes a default instance of Potion.</summary>
        internal Potion()
        {
        }

        /// <summary>Initializes an instance of Potion by assigning Properties.</summary>
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