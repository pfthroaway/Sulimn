using System;
using System.ComponentModel;

namespace Sulimn_WPF
{
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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            HeroClass otherClass = obj as HeroClass;
            if ((object)otherClass == null)
                return false;

            return (this.Name == otherClass.Name) && (this.Description == otherClass.Description) && (this.Strength == otherClass.Strength) && (this.Vitality == otherClass.Vitality) && (this.Dexterity == otherClass.Dexterity) && (this.Wisdom == otherClass.Wisdom);
        }

        public bool Equals(HeroClass otherClass)
        {
            if ((object)otherClass == null)
                return false;

            return (this.Name == otherClass.Name) && (this.Description == otherClass.Description) && (this.Strength == otherClass.Strength) && (this.Vitality == otherClass.Vitality) && (this.Dexterity == otherClass.Dexterity) && (this.Wisdom == otherClass.Wisdom);
        }

        public static bool operator ==(HeroClass left, HeroClass right)
        {
            if (System.Object.ReferenceEquals(left, right))
                return true;

            if (((object)left == null) || ((object)right == null))
                return false;

            return (left.Name == right.Name) && (left.Description == right.Description) && (left.Strength == right.Strength) && (left.Vitality == right.Vitality) && (left.Dexterity == right.Dexterity) && (left.Wisdom == right.Wisdom);
        }

        public static bool operator !=(HeroClass left, HeroClass right)
        {
            return !(left == right);
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

        internal HeroClass()
        {
        }

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