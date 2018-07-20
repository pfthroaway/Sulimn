using Sulimn.Classes.Enums;
using System;

namespace Sulimn.Classes.Items
{
    /// <summary>Represents a Weapon an entity can attack with.</summary>
    internal class Weapon : Item, IEquatable<Weapon>
    {
        private int _damage;
        private WeaponTypes _weaponType;

        #region Properties

        /// <summary>Type of Weapon</summary>
        public WeaponTypes WeaponType
        {
            get => _weaponType;
            private set
            {
                _weaponType = value;
                OnPropertyChanged("WeaponType");
                OnPropertyChanged("WeaponTypeToString");
            }
        }

        /// <summary>Damage the weapon inflicts</summary>
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

        /// <summary>Damage the weapon inflicts, formatted.</summary>
        public string DamageToString => Damage.ToString("N0");

        /// <summary>Damage the weapon inflicts, formatted, with preceding text.</summary>
        public string DamageToStringWithText => Damage > 0 ? $"Damage: {DamageToString}" : "";

        /// <summary>Type of Weapon to string.</summary>
        public string WeaponTypeToString => !string.IsNullOrWhiteSpace(Name) ? WeaponType.ToString() : "";

        #endregion Helper Properties

        #region Override Operators

        private static bool Equals(Weapon left, Weapon right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.WeaponType == right.WeaponType && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) && left.Damage == right.Damage && left.Weight == right.Weight && left.Value == right.Value && left.CanSell == right.CanSell && left.IsSold == right.IsSold;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Weapon);

        public bool Equals(Weapon other) => Equals(this, other);

        public static bool operator ==(Weapon left, Weapon right) => Equals(left, right);

        public static bool operator !=(Weapon left, Weapon right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        public sealed override string ToString() => Name;

        #endregion Override Operators

        #region Constructors

        /// <summary>Initializes a default instance of Weapon.</summary>
        internal Weapon()
        {
        }

        /// <summary>Initializes an instance of Weapon by assigning Properties.</summary>
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
            WeaponType = weaponType;
            Description = description;
            Damage = damage;
            Weight = weight;
            Value = value;
            CanSell = canSell;
            IsSold = isSold;
        }

        /// <summary>Replaces this instance of Weapon with another instance.</summary>
        /// <param name="other">Instance to replace this instance</param>
        internal Weapon(Weapon other) : this(other.Name, other.WeaponType, other.Description, other.Damage, other.Weight, other.Value, other.CanSell, other.IsSold)
        { }

        #endregion Constructors
    }
}