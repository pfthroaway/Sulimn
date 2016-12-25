using System;
using System.ComponentModel;

namespace Sulimn
{
    /// <summary>Represents the Class of a Hero.</summary>
    internal class HeroClass : IEquatable<HeroClass>, INotifyPropertyChanged
    {
        private string _name, _description;

        private int _skillPoints, _strength, _vitality, _dexterity, _wisdom, _currentHealth, _maximumHealth, _currentMagic, _maximumMagic;

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Properties

        /// <summary>Name of the Class.</summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>Description of the Class.</summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        /// <summary>Maximum number of skill points a Class can have when initially being assigned.</summary>
        public int SkillPoints
        {
            get { return _skillPoints; }
            set
            {
                _skillPoints = value;
                OnPropertyChanged("SkillPointsToString");
            }
        }

        /// <summary>Maximum number of skill points a Class can have when initially being assigned, with thousands separator.</summary>
        public string SkillPointsToString
        {
            get
            {
                if (SkillPoints != 1)
                    return SkillPoints.ToString("N0") + " Skill Points Available";
                return SkillPoints.ToString("N0") + " Skill Point Available";
            }
        }

        /// <summary>Amount of Strength the Class has by default.</summary>
        public int Strength
        {
            get { return _strength; }
            set
            {
                _strength = value;
                OnPropertyChanged("Strength");
            }
        }

        /// <summary>Amount of Vitality the Class has by default.</summary>
        public int Vitality
        {
            get { return _vitality; }
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
            get { return _dexterity; }
            set
            {
                _dexterity = value;
                OnPropertyChanged("Dexterity");
            }
        }

        /// <summary>Amount of Wisdom the Class has by default.</summary>
        public int Wisdom
        {
            get { return _wisdom; }
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
            get { return _currentHealth; }
            set
            {
                _currentHealth = value;
                OnPropertyChanged("HealthToString");
            }
        }

        /// <summary>Amount of maximum health the Class has.</summary>
        public int MaximumHealth
        {
            get { return _maximumHealth; }
            set
            {
                _maximumHealth = value;
                OnPropertyChanged("HealthToString");
            }
        }

        /// <summary>Amount of current magic the Class has.</summary>
        public int CurrentMagic
        {
            get { return _currentMagic; }
            set
            {
                _currentMagic = value;
                OnPropertyChanged("MagicToString");
            }
        }

        /// <summary>Amount of maximum magic the Class has.</summary>
        public int MaximumMagic
        {
            get { return _maximumMagic; }
            set
            {
                _maximumMagic = value;
                OnPropertyChanged("MagicToString");
            }
        }

        /// <summary>Amount of health the Class has, formatted.</summary>
        public string HealthToString => CurrentHealth.ToString("N0") + " / " + MaximumHealth.ToString("N0");

        /// <summary>Amount of magic the Class has, formatted.</summary>
        public string MagicToString => CurrentMagic.ToString("N0") + " / " + MaximumMagic.ToString("N0");

        #endregion Properties

        #region Override Operators

        private static bool Equals(HeroClass left, HeroClass right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) &&
             string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) &&
             left.SkillPoints == right.SkillPoints && left.Strength == right.Strength &&
             left.Vitality == right.Vitality && left.Dexterity == right.Dexterity && left.Wisdom == right.Wisdom;
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(this, obj as HeroClass);
        }

        public bool Equals(HeroClass otherClass)
        {
            return Equals(this, otherClass);
        }

        public static bool operator ==(HeroClass left, HeroClass right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HeroClass left, HeroClass right)
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
        /// <param name="otherClass">Instance of HeroClass to replace this instance</param>
        internal HeroClass(HeroClass otherClass)
        {
            Name = otherClass.Name;
            Description = otherClass.Description;
            SkillPoints = otherClass.SkillPoints;
            Strength = otherClass.Strength;
            Vitality = otherClass.Vitality;
            Dexterity = otherClass.Dexterity;
            Wisdom = otherClass.Wisdom;
        }

        #endregion Constructors
    }
}