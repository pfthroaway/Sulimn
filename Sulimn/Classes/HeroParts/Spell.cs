﻿using Sulimn.Classes.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Sulimn.Classes.HeroParts
{
    /// <summary>Represents a Spell a Hero can cast.</summary>
    public class Spell : INotifyPropertyChanged, IEquatable<Spell>
    {
        private string _name, _description;
        private int _requiredLevel, _magicCost, _amount;
        private SpellTypes _type;
        private List<HeroClass> _allowedClasses = new List<HeroClass>();

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(property));

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

        /// <summary><see cref="HeroClass"/>es allowed to use the Spell.</summary>
        public List<HeroClass> AllowedClasses
        {
            get => _allowedClasses;
            private set
            {
                _allowedClasses = value;
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

        /// <summary><see cref="HeroClass"/>es allowed to use the Spell, formatted.</summary>
        public string AllowedClassesToString => AllowedClasses.Count > 0 ? string.Join(",", AllowedClasses) : "";

        /// <summary><see cref="HeroClass"/>es allowed to use the Spell, formatted, with preceding text.</summary>
        public string AllowedClassesToStringWithText => AllowedClasses.Count > 0 ? $"Allowed Classes: {string.Join(",", AllowedClasses)}" : "";

        /// <summary>Type of the Spell, in string format.</summary>
        public string TypeToString => !string.IsNullOrWhiteSpace(Name) ? Type.ToString() : "";

        /// <summary>Type and amount of the Spell.</summary>
        public string TypeAmount => Amount > 0 ? $"{Type}: {Amount}" : "";

        /// <summary>Magic cost of the Spell, with preceding text.</summary>
        public string MagicCostToString => MagicCost > 0 ? $"Magic Cost: {MagicCost:N0}" : "";

        /// <summary>Required Level of the Spell, with preceding text.</summary>
        public string RequiredLevelToString => !string.IsNullOrWhiteSpace(Name) ? $"Required Level: {RequiredLevel}" : "";

        /// <summary>Value of the Spell.</summary>
        public int Value => RequiredLevel * 200;

        /// <summary>Value of the Spell, with preceding text.</summary>
        public string ValueToString => Value.ToString("N0");

        /// <summary>Value of the Spell, with thousands separator and preceding text.</summary>
        public string ValueToStringWithText => !string.IsNullOrWhiteSpace(Name) ? $"Value: {ValueToString}" : "";

        #endregion Helper Properties

        #region Override Operators

        private static bool Equals(Spell left, Spell right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase)
                   && left.Type == right.Type
                   && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase)
                   && left.AllowedClasses == right.AllowedClasses
                   && !left.AllowedClasses.Except(right.AllowedClasses).Any()
                   && left.RequiredLevel == right.RequiredLevel && left.MagicCost == right.MagicCost
                   && left.Amount == right.Amount;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Spell);

        public bool Equals(Spell other) => Equals(this, other);

        public static bool operator ==(Spell left, Spell right) => Equals(left, right);

        public static bool operator !=(Spell left, Spell right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        public sealed override string ToString() => Name;

        #endregion Override Operators

        #region Constructors

        /// <summary>Initializes a default instance of Spell.</summary>
        internal Spell()
        {
        }

        /// <summary>Initializes an instance of <see cref="Spell"/> by assigning Properties.</summary>
        /// <param name="name">Name of <see cref="Spell"/></param>
        /// <param name="spellType">Type of <see cref="Spell"/></param>
        /// <param name="description">Description of <see cref="Spell"/></param>
        /// <param name="allowedClasses"><see cref="HeroClass"/>es allowed to learn the <see cref="<see cref="Spell"/>"/></param>
        /// <param name="requiredLevel">Required Level to learn <see cref="Spell"/></param>
        /// <param name="magicCost">Magic cost of <see cref="Spell"/></param>
        /// <param name="amount">Amount of <see cref="Spell"/></param>
        internal Spell(string name, SpellTypes spellType, string description, List<HeroClass> allowedClasses, int requiredLevel,
        int magicCost, int amount)
        {
            Name = name;
            Type = spellType;
            Description = description;
            AllowedClasses = allowedClasses;
            RequiredLevel = requiredLevel;
            MagicCost = magicCost;
            Amount = amount;
        }

        #endregion Constructors
    }
}