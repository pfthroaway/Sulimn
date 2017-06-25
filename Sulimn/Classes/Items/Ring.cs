using Sulimn.Classes.Enums;
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
        public string DamageToStringWithText
        {
            get
            {
                if (Damage > 0)
                    return $"Damage: {DamageToString}";
                return "";
            }
        }

        /// <summary>Returns the defense with a comma separating thousands.</summary>
        public string DefenseToString => Defense.ToString("N0");

        /// <summary>Returns the defense with a comma separating thousands and preceding text.</summary>
        public string DefenseToStringWithText
        {
            get
            {
                if (Defense > 0)
                    return $"Defense: {DefenseToString}";
                return "";
            }
        }

        /// <summary>Returns the Strength and preceding text.</summary>
        public string StrengthToString
        {
            get
            {
                if (Strength > 0)
                    return $"Strength: {Strength}";
                return "";
            }
        }

        /// <summary>Returns the Vitality and preceding text.</summary>
        public string VitalityToString
        {
            get
            {
                if (Vitality > 0)
                    return $"Vitality: {Vitality}";
                return "";
            }
        }

        /// <summary>Returns the Dexterity and preceding text.</summary>
        public string DexterityToString
        {
            get
            {
                if (Dexterity > 0)
                    return $"Dexterity: {Dexterity}";
                return "";
            }
        }

        /// <summary>Returns the Wisdom and preceding text.</summary>
        public string WisdomToString
        {
            get
            {
                if (Wisdom > 0)
                    return $"Wisdom: {Wisdom}";
                return "";
            }
        }

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
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Type == right.Type &&
            string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) &&
            left.Damage == right.Damage && left.Defense == right.Defense && left.Strength == right.Strength &&
            left.Vitality == right.Vitality && left.Dexterity == right.Dexterity && left.Wisdom == right.Wisdom &&
            left.Weight == right.Weight && left.Value == right.Value && left.CanSell == right.CanSell &&
            left.IsSold == right.IsSold;
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(this, obj as Ring);
        }

        public bool Equals(Ring otherArmor)
        {
            return Equals(this, otherArmor);
        }

        public static bool operator ==(Ring left, Ring right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Ring left, Ring right)
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
        internal Ring(string name, ItemTypes itemType, string description, int damage, int defense, int strength,
        int vitality, int dexterity, int wisdom, int weight, int value, bool canSell, bool isSold)
        {
            Name = name;
            Type = itemType;
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
        internal Ring(Ring otherRing)
        {
            Name = otherRing.Name;
            Type = otherRing.Type;
            Description = otherRing.Description;
            Damage = otherRing.Damage;
            Defense = otherRing.Defense;
            Strength = otherRing.Strength;
            Vitality = otherRing.Vitality;
            Dexterity = otherRing.Dexterity;
            Wisdom = otherRing.Wisdom;
            Weight = otherRing.Weight;
            Value = otherRing.Value;
            CanSell = otherRing.CanSell;
            IsSold = otherRing.IsSold;
        }

        #endregion Constructors
    }
}