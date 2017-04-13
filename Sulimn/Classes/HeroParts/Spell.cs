﻿using System;
using System.ComponentModel;

namespace Sulimn
{
    /// <summary>Represents a Spell a Hero can cast.</summary>
    internal class Spell : IEquatable<Spell>, INotifyPropertyChanged
    {
        private string _name, _description, _requiredClass;
        private int _requiredLevel, _magicCost, _amount;
        private SpellTypes _type;

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Modifying Properties

        /// <summary>Name of the Spell.</summary>
        public string Name
        {
            get => _name;
            private set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>Description of the Spell.</summary>
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        /// <summary>Type of the Spell.</summary>
        public SpellTypes Type
        {
            get => _type;
            private set
            {
                _type = value;
                OnPropertyChanged("Type");
                OnPropertyChanged("TypeAmount");
            }
        }

        /// <summary>Required Class of the Spell.</summary>
        public string RequiredClass
        {
            get => _requiredClass;
            private set
            {
                _requiredClass = value;
                OnPropertyChanged("RequiredClass");
            }
        }

        /// <summary>Required Level of the Spell.</summary>
        public int RequiredLevel
        {
            get => _requiredLevel;
            private set
            {
                _requiredLevel = value;
                OnPropertyChanged("RequiredLevel");
            }
        }

        /// <summary>Magic cost of the Spell.</summary>
        public int MagicCost
        {
            get => _magicCost;
            private set
            {
                _magicCost = value;
                OnPropertyChanged("MagicCost");
            }
        }

        /// <summary>Amount of the Spell.</summary>
        public int Amount
        {
            get => _amount;
            private set
            {
                _amount = value;
                OnPropertyChanged("Amount");
                OnPropertyChanged("TypeAmount");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Required Class of the Spell, with preceding text.</summary>
        public string RequiredClassToString
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                    return $"Required Class: {RequiredClass}";
                return "";
            }
        }

        /// <summary>Type of the Spell, in string format.</summary>
        public string TypeToString
        {
            get
            {
                if (!string.IsNullOrEmpty(Name))
                    return Type.ToString();
                return "";
            }
        }

        /// <summary>Type and amount of the Spell.</summary>
        public string TypeAmount
        {
            get
            {
                if (Amount > 0)
                    return $"{Type}: {Amount}";
                return "";
            }
        }

        /// <summary>Magic cost of the Spell, with preceding text.</summary>
        public string MagicCostToString
        {
            get
            {
                if (MagicCost > 0)
                    return $"Magic Cost: {MagicCost:N0}";
                return "";
            }
        }

        /// <summary>Required Level of the Spell, with preceding text.</summary>
        public string RequiredLevelToString
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                    return $"Required Level: {RequiredLevel}";
                return "";
            }
        }

        /// <summary>Value of the Spell.</summary>
        public int Value => RequiredLevel * 200;

        /// <summary>Value of the Spell, with preceding text.</summary>
        public string ValueToString => Value.ToString("N0");

        /// <summary>Value of the Spell, with thousands separator and preceding text.</summary>
        public string ValueToStringWithText
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                    return $"Value: {ValueToString}";
                return "";
            }
        }

        #endregion Helper Properties

        #region Override Operators

        private static bool Equals(Spell left, Spell right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Type == right.Type &&
             string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) &&
             string.Equals(left.RequiredClass, right.RequiredClass, StringComparison.OrdinalIgnoreCase) &&
             left.RequiredLevel == right.RequiredLevel && left.MagicCost == right.MagicCost &&
             left.Amount == right.Amount;
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(this, obj as Spell);
        }

        public bool Equals(Spell otherSpell)
        {
            return Equals(this, otherSpell);
        }

        public static bool operator ==(Spell left, Spell right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Spell left, Spell right)
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

        /// <summary>Initializes a default instance of Spell.</summary>
        internal Spell()
        {
        }

        /// <summary>Initializes an instance of Spell by assigning Properties.</summary>
        /// <param name="name">Name of Spell</param>
        /// <param name="spellType">Type of Spell</param>
        /// <param name="description">Description of Spell</param>
        /// <param name="requiredClass">Required HeroClass of Spell</param>
        /// <param name="requiredLevel">Required Level to learn Spell</param>
        /// <param name="magicCost">Magic cost of Spell</param>
        /// <param name="amount">Amount of Spell</param>
        internal Spell(string name, SpellTypes spellType, string description, string requiredClass, int requiredLevel,
        int magicCost, int amount)
        {
            Name = name;
            Type = spellType;
            Description = description;
            RequiredClass = requiredClass;
            RequiredLevel = requiredLevel;
            MagicCost = magicCost;
            Amount = amount;
        }

        #endregion Constructors
    }
}