using System;
using System.ComponentModel;

namespace Sulimn
{
    /// <summary>
    /// Represents a piece of Armor worn on feet.
    /// </summary>
    internal class FeetArmor : Item, IEquatable<FeetArmor>, INotifyPropertyChanged
    {
        protected int _defense;

        #region Data-Binding

        public sealed override event PropertyChangedEventHandler PropertyChanged;

        protected sealed override void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Properties

        public sealed override string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public sealed override ItemTypes Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged("Type"); }
        }

        public sealed override string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }

        public int Defense
        {
            get { return _defense; }
            set { _defense = value; OnPropertyChanged("DefenseToString"); OnPropertyChanged("DefenseToStringWithText"); }
        }

        public string DefenseToString
        {
            get { return Defense.ToString("N0"); }
        }

        public string DefenseToStringWithText
        {
            get
            {
                if (Defense > 0)
                    return "Defense: " + DefenseToString;
                return "";
            }
        }

        public sealed override int Weight
        {
            get { return _weight; }
            set { _weight = value; OnPropertyChanged("Weight"); }
        }

        public sealed override int Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged("Value"); }
        }

        public sealed override bool CanSell
        {
            get { return _canSell; }
            set { _canSell = value; OnPropertyChanged("CanSell"); }
        }

        public sealed override bool IsSold
        {
            get { return _isSold; }
            set { _isSold = value; OnPropertyChanged("IsSold"); }
        }

        #endregion Properties

        #region Override Operators

        public static bool Equals(FeetArmor left, FeetArmor right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Type == right.Type && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) && (left.Defense == right.Defense) && (left.Weight == right.Weight) && (left.Value == right.Value) && (left.CanSell == right.CanSell) && (left.IsSold == right.IsSold);
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(this, obj as FeetArmor);
        }

        public bool Equals(FeetArmor otherArmor)
        {
            return Equals(this, otherArmor);
        }

        public static bool operator ==(FeetArmor left, FeetArmor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FeetArmor left, FeetArmor right)
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
        /// Initializes a default instance of FeetArmor.
        /// </summary>
        internal FeetArmor()
        {
        }

        /// <summary>
        /// Initializes an instance of FeetArmor by setting Properties.
        /// </summary>
        /// <param name="armorName">Name of FeetArmor</param>
        /// <param name="armorType">Type of Item</param>
        /// <param name="armorDescription">Description of FeetArmor</param>
        /// <param name="armorDefense">Defense of FeetArmor</param>
        /// <param name="armorWeight">Weight of FeetArmor</param>
        /// <param name="armorValue">Value of FeetArmor</param>
        /// <param name="armorCanSell">Can Sell FeetArmor?</param>
        /// <param name="armorIsSold">Is FeetArmor Sold?</param>
        internal FeetArmor(string armorName, ItemTypes _itemType, string armorDescription, int armorDefense, int armorWeight, int armorValue, bool armorCanSell, bool armorIsSold)
        {
            Name = armorName;
            Type = _itemType;
            Description = armorDescription;
            Defense = armorDefense;
            Weight = armorWeight;
            Value = armorValue;
            CanSell = armorCanSell;
            IsSold = armorIsSold;
        }

        /// <summary>
        /// Replaces this instance of FeetArmor with another instance.
        /// </summary>
        /// <param name="otherArmor">Instance of FeetArmor to replace this one</param>
        internal FeetArmor(FeetArmor otherArmor)
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

        #endregion Constructors
    }
}