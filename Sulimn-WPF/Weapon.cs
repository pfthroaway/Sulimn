using System;
using System.ComponentModel;

namespace Sulimn_WPF
{
    internal class Weapon : Item, IEquatable<Weapon>, INotifyPropertyChanged
    {
        private string _weaponType;
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

        public sealed override string Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged("Type"); }
        }

        public string WeaponType
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
            get { return "Damage: " + DamageToString; }
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

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Weapon wpn = obj as Weapon;
            if ((System.Object)wpn == null)
                return false;

            return (this.Name == wpn.Name) && (this.Type == wpn.Type) && (this.WeaponType == wpn.WeaponType) && (this.Description == wpn.Description) && (this.Damage == wpn.Damage) && (this.Weight == wpn.Weight) && (this.Value == wpn.Value) && (this.CanSell == wpn.CanSell) && (this.IsSold == wpn.IsSold);
        }

        public bool Equals(Weapon otherWeapon)
        {
            if ((object)otherWeapon == null)
                return false;

            return (this.Name == otherWeapon.Name) && (this.Type == otherWeapon.Type) && (this.WeaponType == otherWeapon.WeaponType) && (this.Description == otherWeapon.Description) && (this.Damage == otherWeapon.Damage) && (this.Weight == otherWeapon.Weight) && (this.Value == otherWeapon.Value) && (this.CanSell == otherWeapon.CanSell) && (this.IsSold == otherWeapon.IsSold);
        }

        public static bool operator ==(Weapon left, Weapon right)
        {
            if (System.Object.ReferenceEquals(left, right))
                return true;

            if (((object)left == null) || ((object)right == null))
                return false;

            return (left.Name == right.Name) && (left.Type == right.Type) && (left.WeaponType == right.WeaponType) && (left.Description == right.Description) && (left.Damage == right.Damage) && (left.Weight == right.Weight) && (left.Value == right.Value) && (left.CanSell == right.CanSell) && (left.IsSold == right.IsSold);
        }

        public static bool operator !=(Weapon left, Weapon right)
        {
            return !(left == right);
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

        internal Weapon()
        {
        }

        internal Weapon(string weaponName, string weaponType, string weaponDescription, int weaponDamage, int weaponWeight, int weaponValue, bool weaponCanSell, bool weaponIsSold)
        {
            Name = weaponName;
            Type = "Weapon";
            WeaponType = weaponType;
            Description = weaponDescription;
            Damage = weaponDamage;
            Weight = weaponWeight;
            Value = weaponValue;
            CanSell = weaponCanSell;
            IsSold = weaponIsSold;
        }

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

        internal Weapon(Item otherItem, string weaponType, int weaponDamage)
        {
            Name = otherItem.Name;
            Type = "Weapon";
            WeaponType = weaponType;
            Description = otherItem.Description;
            Damage = weaponDamage;
            Weight = otherItem.Weight;
            Value = otherItem.Value;
            CanSell = otherItem.CanSell;
            IsSold = otherItem.IsSold;
        }

        #endregion Constructors
    }
}