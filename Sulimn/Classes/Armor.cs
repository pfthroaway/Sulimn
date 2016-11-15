using System;
using System.ComponentModel;

namespace Sulimn
{
    /// <summary>
    /// Represents a piece of Armor that a Hero can wear.
    /// </summary>
    internal class Armor : Item, IEquatable<Armor>, INotifyPropertyChanged
    {
        protected ArmorTypes _armorType;
        protected int _defense;

        #region Data-Binding

        public override event PropertyChangedEventHandler PropertyChanged;

        protected  override void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Properties

        public  override string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public  override ItemTypes Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged("Type"); }
        }

        public virtual ArmorTypes ArmorType
        {
            get { return _armorType; }
            set { _armorType = value; OnPropertyChanged("ArmorType"); }
        }

        public  override string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }

        public virtual int Defense
        {
            get { return _defense; }
            set { _defense = value; OnPropertyChanged("DefenseToString"); OnPropertyChanged("DefenseToStringWithText"); }
        }

        public  override int Weight
        {
            get { return _weight; }
            set { _weight = value; OnPropertyChanged("Weight"); }
        }

        public  override int Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged("Value"); }
        }

        public  override bool CanSell
        {
            get { return _canSell; }
            set { _canSell = value; OnPropertyChanged("CanSell"); }
        }

        public  override bool IsSold
        {
            get { return _isSold; }
            set { _isSold = value; OnPropertyChanged("IsSold"); }
        }

        #endregion Properties

        #region Override Operators

        public static bool Equals(Armor left, Armor right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Type == right.Type && left.ArmorType == right.ArmorType && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) && (left.Defense == right.Defense) && (left.Weight == right.Weight) && (left.Value == right.Value) && (left.CanSell == right.CanSell) && (left.IsSold == right.IsSold);
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

        #region Constructors

        /// <summary>
        /// Initializes a default instance of Armor.
        /// </summary>
        internal Armor()
        {
        }

        /// <summary>
        /// Initializes an instance of Armor by setting Properties.
        /// </summary>
        /// <param name="armorName">Name of Armor</param>
        /// <param name="armorType">Type of Armor</param>
        /// <param name="armorDescription">Description of armor</param>
        /// <param name="armorDefense">Defense of Armor</param>
        /// <param name="armorWeight">Weight of Armor</param>
        /// <param name="armorValue">Value of Armor</param>
        /// <param name="armorCanSell">Can Sell Armor?</param>
        /// <param name="armorIsSold">Is Armor Sold?</param>
        internal Armor(string armorName, ArmorTypes armorType, string armorDescription, int armorDefense, int armorWeight, int armorValue, bool armorCanSell, bool armorIsSold)
        {
            Name = armorName;
            Type = ItemTypes.HeadArmor;
            ArmorType = armorType;
            Description = armorDescription;
            Defense = armorDefense;
            Weight = armorWeight;
            Value = armorValue;
            CanSell = armorCanSell;
            IsSold = armorIsSold;
        }

        /// <summary>
        /// Replaces this instance of Armor with another instance.
        /// </summary>
        /// <param name="otherArmor">Instance of Armor to replace this one</param>
        internal Armor(Armor otherArmor)
        {
            Name = otherArmor.Name;
            Type = otherArmor.Type;
            ArmorType = otherArmor.ArmorType;
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