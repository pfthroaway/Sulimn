using System;
using System.ComponentModel;

namespace Sulimn
{
    internal class LegArmor : Armor, IEquatable<LegArmor>, INotifyPropertyChanged
    {
        #region Data-Binding

        public override event PropertyChangedEventHandler PropertyChanged;

        protected override void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Properties

        public override string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public override ItemTypes Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged("Type"); }
        }

        public override string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }

        public override int Defense
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

        public override int Weight
        {
            get { return _weight; }
            set { _weight = value; OnPropertyChanged("Weight"); }
        }

        public override int Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged("Value"); }
        }

        public override bool CanSell
        {
            get { return _canSell; }
            set { _canSell = value; OnPropertyChanged("CanSell"); }
        }

        public override bool IsSold
        {
            get { return _isSold; }
            set { _isSold = value; OnPropertyChanged("IsSold"); }
        }

        #endregion Properties

        #region Override Operators

        public static bool Equals(LegArmor left, LegArmor right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Type == right.Type && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) && (left.Defense == right.Defense) && (left.Weight == right.Weight) && (left.Value == right.Value) && (left.CanSell == right.CanSell) && (left.IsSold == right.IsSold);
        }

        public override bool Equals(object obj)
        {
            return Equals(this, obj as LegArmor);
        }

        public bool Equals(LegArmor otherArmor)
        {
            return Equals(this, otherArmor);
        }

        public static bool operator ==(LegArmor left, LegArmor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LegArmor left, LegArmor right)
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

        #region Constructors

        /// <summary>
        /// Initializes a default instance of LegArmor.
        /// </summary>
        internal LegArmor()
        {
        }

        /// <summary>
        /// Initializes an instance of LegArmor by setting Properties.
        /// </summary>
        /// <param name="armorName">Name of LegArmor</param>
        /// <param name="armorType">Type of Item</param>
        /// <param name="armorDescription">Description of LegArmor</param>
        /// <param name="armorDefense">Defense of LegArmor</param>
        /// <param name="armorWeight">Weight of LegArmor</param>
        /// <param name="armorValue">Value of LegArmor</param>
        /// <param name="armorCanSell">Can Sell LegArmor?</param>
        /// <param name="armorIsSold">Is LegArmor Sold?</param>
        internal LegArmor(string armorName, ItemTypes _itemType, string armorDescription, int armorDefense, int armorWeight, int armorValue, bool armorCanSell, bool armorIsSold)
        {
            Name = armorName;
            Type = _itemType;
            ArmorType = ArmorTypes.Legs;
            Description = armorDescription;
            Defense = armorDefense;
            Weight = armorWeight;
            Value = armorValue;
            CanSell = armorCanSell;
            IsSold = armorIsSold;
        }

        /// <summary>
        /// Replaces this instance of LegArmor with another instance.
        /// </summary>
        /// <param name="otherArmor">Instance of LegArmor to replace this one</param>
        internal LegArmor(LegArmor otherArmor)
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

        /// <summary>
        /// Casts an Armor class object to LegArmor.
        /// </summary>
        /// <param name="otherArmor">Armor</param>
        internal LegArmor(Armor otherArmor)
        {
            Name = otherArmor.Name;
            Type = otherArmor.Type;
            ArmorType = ArmorTypes.Legs;
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