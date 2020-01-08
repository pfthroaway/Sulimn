using Newtonsoft.Json;
using Sulimn.Classes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sulimn.Classes.HeroParts
{
    /// <summary>Represents a <see cref="Spell"/> a Hero can cast.</summary>
    public class Spell : BaseINPC
    {
        private string _name, _description;
        private int _magicCost, _amount;
        private SpellType _type;
        private List<HeroClass> _allowedClasses = new List<HeroClass>();
        private int minimumLevel;

        #region Modifying Properties

        /// <summary>Name of the <see cref="Spell"/>.</summary>
        [JsonProperty(Order = 1)]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        /// <summary>Description of the <see cref="Spell"/>.</summary>
        [JsonProperty(Order = 2)]
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChanged(nameof(Description));
            }
        }

        /// <summary>Type of the <see cref="Spell"/>.</summary>
        [JsonProperty(Order = 3)]
        public SpellType Type
        {
            get => _type;
            set
            {
                _type = value;
                NotifyPropertyChanged(nameof(Type));
                NotifyPropertyChanged(nameof(TypeAmount));
            }
        }

        /// <summary>Magic cost of the <see cref="Spell"/>.</summary>
        [JsonProperty(Order = 4)]
        public int MagicCost
        {
            get => _magicCost;
            set
            {
                _magicCost = value;
                NotifyPropertyChanged(nameof(MagicCost));
            }
        }

        /// <summary>Amount of the <see cref="Spell"/>.</summary>
        [JsonProperty(Order = 5)]
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                NotifyPropertyChanged(nameof(Amount));
                NotifyPropertyChanged(nameof(TypeAmount));
            }
        }

        /// <summary>Required Level of the <see cref="Spell"/>.</summary>
        [JsonProperty(Order = 6)]
        public int MinimumLevel
        {
            get => minimumLevel;
            set
            {
                minimumLevel = value; NotifyPropertyChanged(nameof(MinimumLevel));
            }
        }

        /// <summary><see cref="HeroClass"/>es allowed to use the <see cref="Spell"/>.</summary>
        [JsonIgnore]
        public List<HeroClass> AllowedClasses
        {
            get => _allowedClasses;
            set
            {
                _allowedClasses = value;
                NotifyPropertyChanged(nameof(AllowedClasses));
            }
        }

        /// <summary><see cref="HeroClass"/>es allowed to use the <see cref="Spell"/>, set up to import from JSON.</summary>
        [JsonProperty(Order = 7)]
        public string AllowedClassesJson
        {
            get => AllowedClasses?.Count > 0 ? string.Join(",", AllowedClasses) : "";
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    AllowedClasses = new List<HeroClass>();
                    AllowedClasses.AddRange(from string heroClass in value.Split(',')
                                            select GameState.AllClasses.Find(cls => cls.Name == heroClass.Trim()));
                }
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary><see cref="HeroClass"/>es allowed to use the <see cref="Spell"/>, formatted.</summary>
        [JsonIgnore]
        public string AllowedClassesToString => AllowedClasses?.Count > 0 ? string.Join(",", AllowedClasses) : "";

        /// <summary><see cref="HeroClass"/>es allowed to use the <see cref="Spell"/>, formatted, with preceding text.</summary>
        [JsonIgnore]
        public string AllowedClassesToStringWithText => AllowedClasses?.Count > 0 ? $"Allowed Classes: {string.Join(",", AllowedClasses)}" : "";

        /// <summary>Type of the <see cref="Spell"/>, in string format.</summary>
        [JsonIgnore]
        public string TypeToString => !string.IsNullOrWhiteSpace(Name) ? Type.ToString() : "";

        /// <summary>Type and amount of the <see cref="Spell"/>.</summary>
        [JsonIgnore]
        public string TypeAmount => Amount > 0 ? $"{Type}: {Amount}" : "";

        /// <summary>Magic cost of the <see cref="Spell"/>, with preceding text.</summary>
        [JsonIgnore]
        public string MagicCostToString => MagicCost > 0 ? $"Magic Cost: {MagicCost:N0}" : "";

        /// <summary>Required Level of the <see cref="Spell"/>, with preceding text.</summary>
        [JsonIgnore]
        public string RequiredLevelToString => !string.IsNullOrWhiteSpace(Name) ? $"Required Level: {MinimumLevel}" : "";

        /// <summary>Value of the <see cref="Spell"/>.</summary>
        [JsonIgnore]
        public int Value => MinimumLevel * 200;

        /// <summary>Value of the <see cref="Spell"/>, with preceding text.</summary>
        [JsonIgnore]
        public string ValueToString => Value.ToString("N0");

        /// <summary>Value of the <see cref="Spell"/>, with thousands separator and preceding text.</summary>
        [JsonIgnore]
        public string ValueToStringWithText => !string.IsNullOrWhiteSpace(Name) ? $"Value: {ValueToString}" : "";

        #endregion Helper Properties

        #region Override Operators

        public static bool Equals(Spell left, Spell right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase)
                   && left.Type == right.Type
                   && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase)
                   && !left.AllowedClasses.Except(right.AllowedClasses).Any()
                   && left.MinimumLevel == right.MinimumLevel
                   && left.MagicCost == right.MagicCost
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

        /// <summary>Initializes a default instance of <see cref="Spell"/>.</summary>
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
        internal Spell(string name, SpellType spellType, string description, List<HeroClass> allowedClasses, int requiredLevel,
        int magicCost, int amount)
        {
            Name = name;
            Type = spellType;
            Description = description;
            AllowedClasses = allowedClasses;
            MinimumLevel = requiredLevel;
            MagicCost = magicCost;
            Amount = amount;
        }

        #endregion Constructors
    }
}