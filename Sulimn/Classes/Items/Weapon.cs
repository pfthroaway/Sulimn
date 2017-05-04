using System;

namespace Sulimn
{
    /// <summary>Represents a Weapon an entity can attack with.</summary>
    internal class Weapon : Item, IEquatable<Weapon>
    {
        private int _damage;
        private WeaponTypes? _weaponType;

        #region Properties

        public WeaponTypes? WeaponType
        {
            get => _weaponType;
            private set
            {
                _weaponType = value;
                OnPropertyChanged("WeaponType");
                OnPropertyChanged("WeaponTypeToString");
            }
        }

        public int Damage
        {
            get => _damage;
            private set
            {
                _damage = value;
                OnPropertyChanged("DamageToString");
                OnPropertyChanged("DamageToStringWithText");
            }
        }

        #endregion Properties

        #region Helper Properties

        public string DamageToString => Damage.ToString("N0");

        public string DamageToStringWithText
        {
            get
            {
                if (Damage > 0)
                    return $"Damage: {DamageToString}";
                return "";
            }
        }

        #endregion Helper Properties

        #region Override Operators

        private static bool Equals(Weapon left, Weapon right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Type == right.Type &&
             left.WeaponType == right.WeaponType &&
             string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) &&
             left.Damage == right.Damage && left.Weight == right.Weight && left.Value == right.Value &&
             left.CanSell == right.CanSell && left.IsSold == right.IsSold;
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
        /// <param name="name">Name of Weapon</param>
        /// <param name="weaponType">Type of Weapon</param>
        /// <param name="description">Description of Weapon</param>
        /// <param name="damage">Damage of Weapon</param>
        /// <param name="weight">Weight of Weapon</param>
        /// <param name="value">Value of Weapon</param>
        /// <param name="canSell">Can Weapon be sold?</param>
        /// <param name="isSold">Is Weapon sold?</param>
        internal Weapon(string name, WeaponTypes weaponType, string description, int damage, int weight, int value,
        bool canSell, bool isSold)
        {
            Name = name;
            Type = ItemTypes.Weapon;
            WeaponType = weaponType;
            Description = description;
            Damage = damage;
            Weight = weight;
            Value = value;
            CanSell = canSell;
            IsSold = isSold;
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