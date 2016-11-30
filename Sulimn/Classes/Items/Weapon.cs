using System;
using System.ComponentModel;

namespace Sulimn
{
    /// <summary>
    /// Represents a Weapon a Hero can attack with.
    /// </summary>
    internal class Weapon : Item, IEquatable<Weapon>, INotifyPropertyChanged
    {
        private WeaponTypes _weaponType;
        private int _damage;

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

        public WeaponTypes WeaponType
        {
            get { return _weaponType; }
            set { _weaponType = value; OnPropertyChanged("WeaponType"); }
        }

        public sealed override string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }

        public int Damage
        {
            get { return _damage; }
            set { _damage = value; OnPropertyChanged("DamageToString"); OnPropertyChanged("DamageToStringWithText"); }
        }

        public string DamageToString
        {
            get { return Damage.ToString("N0"); }
        }

        public string DamageToStringWithText
        {
            get
            {
                if (Damage > 0)
                    return "Damage: " + DamageToString;
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

        #region Helper Properties

        /// <summary>The value of the Weapon with thousands separators</summary>
        public sealed override string ValueToString
        {
            get { return Value.ToString("N0"); }
        }

        /// <summary>The value of the Weapon with thousands separators and preceding text</summary>
        public sealed override string ValueToStringWithText
        {
            get { if (!string.IsNullOrWhiteSpace(Name)) return "Value: " + ValueToString; return ""; }
        }

        /// <summary>The value of the Weapon</summary>
        public sealed override int SellValue
        {
            get { return Value / 2; }
        }

        /// <summary>The value of the Weapon with thousands separators</summary>
        public sealed override string SellValueToString
        {
            get { return SellValue.ToString("N0"); }
        }

        /// <summary>The value of the Weapon with thousands separators with preceding text</summary>
        public sealed override string SellValueToStringWithText
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                    return "Sell Value: " + SellValueToString;
                return "";
            }
        }

        /// <summary>Returns text relating to the sellability of the Weapon</summary>
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

        #endregion Helper Properties

        #region Override Operators

        public static bool Equals(Weapon left, Weapon right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Type == right.Type && (left.WeaponType == right.WeaponType) && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) && (left.Damage == right.Damage) && (left.Weight == right.Weight) && (left.Value == right.Value) && (left.CanSell == right.CanSell) && (left.IsSold == right.IsSold);
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(this, obj as Weapon);
        }

        public bool Equals(Weapon otherWeapon)
        {
            return Equals(this, otherWeapon);
        }

        public static bool operator ==(Weapon left, Weapon right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Weapon left, Weapon right)
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
        /// Initializes a default instance of Weapon.
        /// </summary>
        internal Weapon()
        {
        }

        /// <summary>
        /// Initializes an instance of Weapon by assigning Properties.
        /// </summary>
        /// <param name="weaponName">Name of Weapon</param>
        /// <param name="weaponType">Type of Weapon</param>
        /// <param name="weaponDescription">Description of Weapon</param>
        /// <param name="weaponDamage">Damage of Weapon</param>
        /// <param name="weaponWeight">Weight of Weapon</param>
        /// <param name="weaponValue">Value of Weapon</param>
        /// <param name="weaponCanSell">Can Weapon be sold?</param>
        /// <param name="weaponIsSold">Is Weapon sold?</param>
        internal Weapon(string weaponName, WeaponTypes weaponType, string weaponDescription, int weaponDamage, int weaponWeight, int weaponValue, bool weaponCanSell, bool weaponIsSold)
        {
            Name = weaponName;
            Type = ItemTypes.Weapon;
            WeaponType = weaponType;
            Description = weaponDescription;
            Damage = weaponDamage;
            Weight = weaponWeight;
            Value = weaponValue;
            CanSell = weaponCanSell;
            IsSold = weaponIsSold;
        }

        /// <summary>
        /// Replaces this instance of Weapon with another instance.
        /// </summary>
        /// <param name="otherWeapon">Instance to replace this instance</param>
        internal Weapon(Weapon otherWeapon)
        {
            Name = otherWeapon.Name;
            Type = otherWeapon.Type;
            WeaponType = otherWeapon.WeaponType;
            Description = otherWeapon.Description;
            Damage = otherWeapon.Damage;
            Weight = otherWeapon.Weight;
            Value = otherWeapon.Value;
            CanSell = otherWeapon.CanSell;
            IsSold = otherWeapon.IsSold;
        }

        #endregion Constructors
    }
}