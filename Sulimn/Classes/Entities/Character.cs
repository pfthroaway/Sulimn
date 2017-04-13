using System.ComponentModel;

namespace Sulimn
{
    /// <summary>Represents living entities in Sulimn.</summary>
    internal abstract class Character : ICharacter, INotifyPropertyChanged
    {
        private Attributes _attributes = new Attributes();
        private Equipment _equipment = new Equipment();
        private Inventory _inventory = new Inventory();
        private int _level, _experience;
        private string _name;
        private Statistics _statistics = new Statistics();

        #region Modifying Properties

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                OnPropertyChanged("Level");
                OnPropertyChanged("LevelAndClassToString");
            }
        }

        public int Experience
        {
            get => _experience;
            set
            {
                _experience = value;
                OnPropertyChanged("ExperienceToString");
                OnPropertyChanged("ExperienceToStringWithText");
            }
        }

        public Attributes Attributes
        {
            get => _attributes;
            set
            {
                _attributes = value;
                OnPropertyChanged("Attributes");
            }
        }

        public Statistics Statistics
        {
            get => _statistics;
            set
            {
                _statistics = value;
                OnPropertyChanged("Statistics");
            }
        }

        public Equipment Equipment
        {
            get => _equipment;
            set
            {
                _equipment = value;
                OnPropertyChanged("Equipment");
            }
        }

        public Inventory Inventory
        {
            get => _inventory;
            set
            {
                _inventory = value;
                OnPropertyChanged("Inventory");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>The experience the Hero has gained this level alongside how much is needed to level up</summary>
        public string ExperienceToString => $"{Experience:N0} / {(_level * 100):N0}";

        /// <summary>The experience the Hero has gained this level alongside how much is needed to level up with preceding text</summary>
        public string ExperienceToStringWithText => $"Experience: {ExperienceToString}";

        /// <summary>Returns the total Strength attribute and bonus produced by the current set of equipment.</summary>
        public int TotalStrength => Attributes.Strength + Equipment.BonusStrength;

        /// <summary>Returns the total Vitality attribute and bonus produced by the current set of equipment.</summary>
        public int TotalVitality => Attributes.Vitality + Equipment.BonusVitality;

        /// <summary>Returns the total Dexterity attribute and bonus produced by the current set of equipment.</summary>
        public int TotalDexterity => Attributes.Dexterity + Equipment.BonusDexterity;

        /// <summary>Returns the total Wisdom attribute and bonus produced by the current set of equipment.</summary>
        public int TotalWisdom => Attributes.Wisdom + Equipment.BonusWisdom;

        #endregion Helper Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding
    }
}