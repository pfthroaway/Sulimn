using System;
using System.ComponentModel;

namespace Sulimn
{
    /// <summary>
    /// Represents the Class of a Hero.
    /// </summary>
    internal class HeroClass : IEquatable<HeroClass>, INotifyPropertyChanged
    {
        private string _name, _description;
        private int _skillPoints, _strength, _vitality, _dexterity, _wisdom, _currentHealth, _maximumHealth, _currentMagic, _maximumMagic;

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Properties

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }

        public int SkillPoints
        {
            get { return _skillPoints; }
            set { _skillPoints = value; OnPropertyChanged("SkillPointsToString"); }
        }

        public string SkillPointsToString
        {
            get
            {
                if (SkillPoints != 1)
                    return SkillPoints.ToString("N0") + " Skill Points Available";
                else
                    return SkillPoints.ToString("N0") + " Skill Point Available";
            }
        }

        public int Strength
        {
            get { return _strength; }
            set { _strength = value; OnPropertyChanged("Strength"); }
        }

        public int Vitality
        {
            get { return _vitality; }
            set { _vitality = value; CurrentHealth = Vitality * 5; MaximumHealth = Vitality * 5; OnPropertyChanged("Vitality"); }
        }

        public int Dexterity
        {
            get { return _dexterity; }
            set { _dexterity = value; OnPropertyChanged("Dexterity"); }
        }

        public int Wisdom
        {
            get { return _wisdom; }
            set { _wisdom = value; CurrentMagic = Wisdom * 5; MaximumMagic = Wisdom * 5; OnPropertyChanged("Wisdom"); }
        }

        public int CurrentHealth
        {
            get { return _currentHealth; }
            set { _currentHealth = value; OnPropertyChanged("HealthToString"); }
        }

        public int MaximumHealth
        {
            get { return _maximumHealth; }
            set { _maximumHealth = value; OnPropertyChanged("HealthToString"); }
        }

        public int CurrentMagic
        {
            get { return _currentMagic; }
            set { _currentMagic = value; OnPropertyChanged("MagicToString"); }
        }

        public int MaximumMagic
        {
            get { return _maximumMagic; }
            set { _maximumMagic = value; OnPropertyChanged("MagicToString"); }
        }

        public string HealthToString
        {
            get { return CurrentHealth + " / " + MaximumHealth; }
        }

        public string MagicToString
        {
            get { return CurrentMagic + " / " + MaximumMagic; }
        }

        #endregion Properties

        #region Override Operators

        public static bool Equals(HeroClass left, HeroClass right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) && (left.SkillPoints == right.SkillPoints) && (left.Strength == right.Strength) && (left.Vitality == right.Vitality) && (left.Dexterity == right.Dexterity) && (left.Wisdom == right.Wisdom);
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

        /// <summary>
        /// Initializes a default instance of HeroClass.
        /// </summary>
        internal HeroClass()
        {
        }

        /// <summary>
        /// Initializes an instance of HeroClass by assigning Properties.
        /// </summary>
        /// <param name="className">Name of HeroClass</param>
        /// <param name="classDescription">Description of HeroClass</param>
        /// <param name="skillPoints">Skill Points</param>
        /// <param name="classStrength">Strength</param>
        /// <param name="classVitality">Vitality</param>
        /// <param name="classDexterity">Dexterity</param>
        /// <param name="classWisdom">Wisdom</param>
        internal HeroClass(string className, string classDescription, int skillPoints, int classStrength, int classVitality, int classDexterity, int classWisdom)
        {
            Name = className;
            Description = classDescription;
            SkillPoints = skillPoints;
            Strength = classStrength;
            Vitality = classVitality;
            Dexterity = classDexterity;
            Wisdom = classWisdom;
        }

        /// <summary>
        /// Replaces this instance of HeroClass with another instance.
        /// </summary>
        /// <param name="otherClass">Instance of HeroClass to replace this instance</param>
        internal HeroClass(HeroClass otherClass)
        {
            Name = otherClass.Name;
            Description = otherClass.Description;
            Strength = otherClass.Strength;
            SkillPoints = otherClass.SkillPoints;
            Vitality = otherClass.Vitality;
            Dexterity = otherClass.Dexterity;
            Wisdom = otherClass.Wisdom;
        }

        #endregion Constructors
    }
}