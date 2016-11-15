using System;
using System.ComponentModel;

namespace Sulimn
{
    internal class BodyArmor : Armor, IEquatable<BodyArmor>, INotifyPropertyChanged
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

        public static bool Equals(BodyArmor left, BodyArmor right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Type == right.Type && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) && (left.Defense == right.Defense) && (left.Weight == right.Weight) && (left.Value == right.Value) && (left.CanSell == right.CanSell) && (left.IsSold == right.IsSold);
        }

        public override bool Equals(object obj)
        {
            return Equals(this, obj as BodyArmor);
        }

        public bool Equals(BodyArmor otherArmor)
        {
            return Equals(this, otherArmor);
        }

        public static bool operator ==(BodyArmor left, BodyArmor right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BodyArmor left, BodyArmor right)
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
        /// Initializes a default instance of BodyArmor.
        /// </summary>
        internal BodyArmor()
        {
        }

        /// <summary>
        /// Initializes an instance of BodyArmor by setting Properties.
        /// </summary>
        /// <param name="armorName">Name of BodyArmor</param>
        /// <param name="armorType">Type of Item</param>
        /// <param name="armorDescription">Description of BodyArmor</param>
        /// <param name="armorDefense">Defense of BodyArmor</param>
        /// <param name="armorWeight">Weight of BodyArmor</param>
        /// <param name="armorValue">Value of BodyArmor</param>
        /// <param name="armorCanSell">Can Sell BodyArmor?</param>
        /// <param name="armorIsSold">Is BodyArmor Sold?</param>
        internal BodyArmor(string armorName, ItemTypes _itemType, string armorDescription, int armorDefense, int armorWeight, int armorValue, bool armorCanSell, bool armorIsSold)
        {
            Name = armorName;
            Type = _itemType;
            ArmorType = ArmorTypes.Body;
            Description = armorDescription;
            Defense = armorDefense;
            Weight = armorWeight;
            Value = armorValue;
            CanSell = armorCanSell;
            IsSold = armorIsSold;
        }

        /// <summary>
        /// Replaces this instance of BodyArmor with another instance.
        /// </summary>
        /// <param name="otherArmor">Instance of BodyArmor to replace this one</param>
        internal BodyArmor(BodyArmor otherArmor)
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
        /// Casts an Armor class object to BodyArmor.
        /// </summary>
        /// <param name="otherArmor">Armor</param>
        internal BodyArmor(Armor otherArmor)
        {
            Name = otherArmor.Name;
            Type = otherArmor.Type;
            ArmorType = ArmorTypes.Body;
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