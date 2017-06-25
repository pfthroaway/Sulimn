using System;

namespace Sulimn.Classes.Items
{
    internal class Armor : Item, IEquatable<Armor>
    {
        private int _defense;

        #region Modifying Properties

        /// <summary>How much damage the armor can defend against</summary>
        public int Defense
        {
            get => _defense;
            set
            {
                _defense = value;
                OnPropertyChanged("DefenseToString");
                OnPropertyChanged("DefenseToStringWithText");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Returns the defense with a comma separating thousands.</summary>
        public string DefenseToString => Defense.ToString("N0");

        /// <summary>Returns the defense with a comma separating thousands and preceding text.</summary>
        public string DefenseToStringWithText => Defense > 0 ? $"Defense: {DefenseToString}" : "";

        #endregion Helper Properties

        #region Override Operators

        private static bool Equals(Armor left, Armor right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Type == right.Type &&
            string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) &&
            left.Defense == right.Defense && left.Weight == right.Weight && left.Value == right.Value &&
            left.CanSell == right.CanSell && left.IsSold == right.IsSold;
        }

        public override bool Equals(object obj)
        {
            return Equals(this, obj as Armor);
        }

        public bool Equals(Armor otherArmor)
        {
            return Equals(this, otherArmor);
        }

        public static bool operator ==(Armor left, Armor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Armor left, Armor right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ 17;
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion Override Operators

        public Armor()
        {
        }

        /// <summary>Initializes an instance of Armor by assigning Properties.</summary>
        /// <param name="name">Name of Armor</param>
        /// <param name="description">Description of Armor</param>
        /// <param name="defense">Defense of Armor</param>
        /// <param name="weight">Weight of Armor</param>
        /// <param name="value">Value of Armor</param>
        /// <param name="canSell">Can Sell Armor?</param>
        /// <param name="isSold">Is Armor Sold?</param>
        internal Armor(string name, string description, int defense, int weight, int value,
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

        /// <summary>Replaces this instance of Armor with another instance.</summary>
        /// <param name="otherArmor">Instance of Armor to replace this one</param>
        internal Armor(Armor otherArmor)
        {
            Name = otherArmor.Name;
            Type = otherArmor.Type;
            Description = otherArmor.Description;
            Defense = otherArmor.Defense;
            Weight = otherArmor.Weight;
            Value = otherArmor.Value;
            CanSell = otherArmor.CanSell;
            IsSold = otherArmor.IsSold;
        }
    }
}