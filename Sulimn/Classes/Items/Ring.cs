using System;
using System.Linq;

namespace Sulimn.Classes.Items
{
    /// <summary>Represents a Ring worn on an entity's finger.</summary>
    internal class Ring : Item, IEquatable<Ring>
    {
        private int _damage, _defense, _strength, _vitality, _dexterity, _wisdom;

        #region Modifying Properties

        /// <summary>How much bonus damage the Ring inflicts</summary>
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

        /// <summary>How much bonus damage the Ring can defend against</summary>
        public int Defense
        {
            get => _defense;
            private set
            {
                _defense = value;
                OnPropertyChanged("DefenseToString");
                OnPropertyChanged("DefenseToStringWithText");
            }
        }

        /// <summary>How much bonus Strength the Ring grants</summary>
        public int Strength
        {
            get => _strength;
            private set
            {
                _strength = value;
                OnPropertyChanged("Strength");
            }
        }

        /// <summary>How much bonus Vitality the Ring grants</summary>
        public int Vitality
        {
            get => _vitality;
            private set
            {
                _vitality = value;
                OnPropertyChanged("Vitality");
            }
        }

        /// <summary>How much bonus Dexterity the Ring grants</summary>
        public int Dexterity
        {
            get => _dexterity;
            private set
            {
                _dexterity = value;
                OnPropertyChanged("Dexterity");
            }
        }

        /// <summary>How much bonus Wisdom the Ring grants</summary>
        public int Wisdom
        {
            get => _wisdom;
            private set
            {
                _wisdom = value;
                OnPropertyChanged("Wisdom");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Returns the damage with a comma separating thousands.</summary>
        public string DamageToString => Damage.ToString("N0");

        /// <summary>Returns the damage with a comma separating thousands and preceding text.</summary>
        public string DamageToStringWithText => Damage > 0 ? $"Damage: {DamageToString}" : "";

        /// <summary>Returns the defense with a comma separating thousands.</summary>
        public string DefenseToString => Defense.ToString("N0");

        /// <summary>Returns the defense with a comma separating thousands and preceding text.</summary>
        public string DefenseToStringWithText => Defense > 0 ? $"Defense: {DefenseToString}" : "";

        /// <summary>Returns the Strength and preceding text.</summary>
        public string StrengthToString => Strength > 0 ? $"Strength: {Strength}" : "";

        /// <summary>Returns the Vitality and preceding text.</summary>
        public string VitalityToString => Vitality > 0 ? $"Vitality: {Vitality}" : "";

        /// <summary>Returns the Dexterity and preceding text.</summary>
        public string DexterityToString => Dexterity > 0 ? $"Dexterity: {Dexterity}" : "";

        /// <summary>Returns the Wisdom and preceding text.</summary>
        public string WisdomToString => Wisdom > 0 ? $"Wisdom: {Wisdom}" : "";

        /// <summary>Returns all bonuses in string format.</summary>
        public string BonusToString
        {
            get
            {
                string[] bonuses =
                {
                    DamageToStringWithText, DefenseToStringWithText, StrengthToString, VitalityToString,
                    DexterityToString, WisdomToString
                };

                return string.Join(", ", bonuses.Where(bonus => bonus.Length > 0));
            }
        }

        #endregion Helper Properties

        #region Override Operators

        private static bool Equals(Ring left, Ring right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) && left.Damage == right.Damage && left.Defense == right.Defense && left.Strength == right.Strength && left.Vitality == right.Vitality && left.Dexterity == right.Dexterity && left.Wisdom == right.Wisdom && left.Weight == right.Weight && left.Value == right.Value && left.CanSell == right.CanSell && left.IsSold == right.IsSold;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Ring);

        public bool Equals(Ring other) => Equals(this, other);

        public static bool operator ==(Ring left, Ring right) => Equals(left, right);

        public static bool operator !=(Ring left, Ring right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        public sealed override string ToString() => Name;

        #endregion Override Operators

        #region Constructors

        /// <summary>Initializes a default instance of Ring.</summary>
        internal Ring()
        {
        }

        /// <summary>Initializes an instance of Ring by assigning Properties.</summary>
        /// <param name="name">Name of Ring</param>
        /// <param name="itemType">Type of Item</param>
        /// <param name="description">Description of Ring</param>
        /// <param name="damage">Damage bonus provided by Ring</param>
        /// <param name="defense">Defense bonus provided by Ring</param>
        /// <param name="strength">Strength bonus provided by Ring</param>
        /// <param name="vitality">Vitality bonus provided by Ring</param>
        /// <param name="dexterity">Dexterity bonus provided by Ring</param>
        /// <param name="wisdom">Wisdom bonus provided by Ring</param>
        /// <param name="weight">Weight of Ring</param>
        /// <param name="value">Value of Ring</param>
        /// <param name="canSell">Can Sell Ring?</param>
        /// <param name="isSold">Is Ring Sold?</param>
        internal Ring(string name, string description, int damage, int defense, int strength,
        int vitality, int dexterity, int wisdom, int weight, int value, bool canSell, bool isSold)
        {
            Name = name;
            Description = description;
            Damage = damage;
            Defense = defense;
            Strength = strength;
            Vitality = vitality;
            Dexterity = dexterity;
            Wisdom = wisdom;
            Weight = weight;
            Value = value;
            CanSell = canSell;
            IsSold = isSold;
        }

        /// <summary>Replaces this instance of Ring with another instance.</summary>
        /// <param name="otherRing">Instance of Ring to replace this one</param>
        internal Ring(Ring otherRing) : this(otherRing.Name, otherRing.Description, otherRing.Damage,
            otherRing.Defense, otherRing.Strength, otherRing.Vitality, otherRing.Dexterity, otherRing.Wisdom,
            otherRing.Weight, otherRing.Value, otherRing.CanSell, otherRing.IsSold)
        {
        }

        #endregion Constructors
    }
}