using System;
using System.ComponentModel;

namespace Sulimn.Classes.HeroParts
{
    /// <summary>Represents the Class of a Hero.</summary>
    public class HeroClass : IEquatable<HeroClass>, INotifyPropertyChanged
    {
        private string _name, _description;

        private int _skillPoints, _strength, _vitality, _dexterity, _wisdom, _currentHealth, _maximumHealth, _currentMagic, _maximumMagic;

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Modifying Properties

        /// <summary>Name of the Class.</summary>
        public string Name
        {
            get => _name;
            private set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>Description of the Class.</summary>
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        /// <summary>Maximum number of skill points a Class can have when initially being assigned.</summary>
        public int SkillPoints
        {
            get => _skillPoints;
            set
            {
                _skillPoints = value;
                OnPropertyChanged("SkillPointsToString");
            }
        }

        /// <summary>Amount of Strength the Class has by default.</summary>
        public int Strength
        {
            get => _strength;
            set
            {
                _strength = value;
                OnPropertyChanged("Strength");
            }
        }

        /// <summary>Amount of Vitality the Class has by default.</summary>
        public int Vitality
        {
            get => _vitality;
            set
            {
                _vitality = value;
                CurrentHealth = Vitality * 5;
                MaximumHealth = Vitality * 5;
                OnPropertyChanged("Vitality");
            }
        }

        /// <summary>Amount of Dexterity the Class has by default.</summary>
        public int Dexterity
        {
            get => _dexterity;
            set
            {
                _dexterity = value;
                OnPropertyChanged("Dexterity");
            }
        }

        /// <summary>Amount of Wisdom the Class has by default.</summary>
        public int Wisdom
        {
            get => _wisdom;
            set
            {
                _wisdom = value;
                CurrentMagic = Wisdom * 5;
                MaximumMagic = Wisdom * 5;
                OnPropertyChanged("Wisdom");
            }
        }

        /// <summary>Amount of current health the Class has.</summary>
        public int CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                _currentHealth = value;
                OnPropertyChanged("HealthToString");
            }
        }

        /// <summary>Amount of maximum health the Class has.</summary>
        public int MaximumHealth
        {
            get => _maximumHealth;
            private set
            {
                _maximumHealth = value;
                OnPropertyChanged("HealthToString");
            }
        }

        /// <summary>Amount of current magic the Class has.</summary>
        public int CurrentMagic
        {
            get => _currentMagic;
            private set
            {
                _currentMagic = value;
                OnPropertyChanged("MagicToString");
            }
        }

        /// <summary>Amount of maximum magic the Class has.</summary>
        public int MaximumMagic
        {
            get => _maximumMagic;
            private set
            {
                _maximumMagic = value;
                OnPropertyChanged("MagicToString");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Maximum number of skill points a Class can have when initially being assigned, with thousands separator.</summary>
        /// <summary>The amount of skill points the Hero has available to spend</summary>
        public string SkillPointsToString => SkillPoints != 1 ? $"{SkillPoints:N0} Skill Points Available" : $"{SkillPoints:N0} Skill Point Available";

        /// <summary>Amount of health the Class has, formatted.</summary>
        public string HealthToString => $"{CurrentHealth:N0} / {MaximumHealth:N0}";

        /// <summary>Amount of magic the Class has, formatted.</summary>
        public string MagicToString => $"{CurrentMagic:N0} / {MaximumMagic:N0}";

        #endregion Helper Properties

        #region Override Operators

        private static bool Equals(HeroClass left, HeroClass right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase)
                   && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) && left.SkillPoints == right.SkillPoints && left.Strength == right.Strength && left.Vitality == right.Vitality && left.Dexterity == right.Dexterity && left.Wisdom == right.Wisdom && left.CurrentHealth == right.CurrentHealth && left.MaximumHealth == right.MaximumHealth && left.CurrentMagic == right.CurrentMagic && left.MaximumMagic == right.MaximumMagic;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as HeroClass);

        public bool Equals(HeroClass other) => Equals(this, other);

        public static bool operator ==(HeroClass left, HeroClass right) => Equals(left, right);

        public static bool operator !=(HeroClass left, HeroClass right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        public sealed override string ToString() => Name;

        #endregion Override Operators

        #region Constructors

        /// <summary>Initializes a default instance of HeroClass.</summary>
        internal HeroClass()
        {
        }

        /// <summary>Initializes an instance of HeroClass by assigning Properties.</summary>
        /// <param name="name">Name of HeroClass</param>
        /// <param name="description">Description of HeroClass</param>
        /// <param name="skillPoints">Skill Points</param>
        /// <param name="strength">Strength</param>
        /// <param name="vitality">Vitality</param>
        /// <param name="dexterity">Dexterity</param>
        /// <param name="wisdom">Wisdom</param>
        internal HeroClass(string name, string description, int skillPoints, int strength, int vitality, int dexterity,
        int wisdom)
        {
            Name = name;
            Description = description;
            SkillPoints = skillPoints;
            Strength = strength;
            Vitality = vitality;
            Dexterity = dexterity;
            Wisdom = wisdom;
        }

        /// <summary>Replaces this instance of HeroClass with another instance.</summary>
        /// <param name="other">Instance of HeroClass to replace this instance</param>
        internal HeroClass(HeroClass other) : this(other.Name, other.Description, other.SkillPoints, other.Strength,
            other.Vitality, other.Dexterity, other.Wisdom)
        {
        }

        #endregion Constructors
    }
}