using Sulimn.Classes.HeroParts;
using System.ComponentModel;

namespace Sulimn.Classes.Entities
{
    /// <summary>Represents living entities in Sulimn.</summary>
    internal abstract class Character : ICharacter, INotifyPropertyChanged
    {
        private Attributes _attributes = new Attributes();
        private Equipment _equipment = new Equipment();
        private int _level, _experience, _gold;
        private string _name;
        private Statistics _statistics = new Statistics();

        #region Modifying Properties

        /// <summary>Name of character</summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>Level of character</summary>
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

        /// <summary>Name of character</summary>
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

        /// <summary>Amount of Gold in the inventory.</summary>
        public int Gold
        {
            get => _gold;
            set
            {
                _gold = value;
                OnPropertyChanged("Gold");
                OnPropertyChanged("GoldToString");
                OnPropertyChanged("GoldToStringWithText");
            }
        }

        /// <summary>Attributes of character</summary>
        public Attributes Attributes
        {
            get => _attributes;
            set
            {
                _attributes = value;
                OnPropertyChanged("Attributes");
            }
        }

        /// <summary>Statistics of character</summary>
        public Statistics Statistics
        {
            get => _statistics;
            set
            {
                _statistics = value;
                OnPropertyChanged("Statistics");
            }
        }

        /// <summary>Equipment of character</summary>
        public Equipment Equipment
        {
            get => _equipment;
            set
            {
                _equipment = value;
                OnPropertyChanged("Equipment");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>The experience the Hero has gained this level alongside how much is needed to level up</summary>
        public string ExperienceToString => $"{Experience:N0} / {(_level * 100):N0}";

        /// <summary>The experience the Hero has gained this level alongside how much is needed to level up with preceding text</summary>
        public string ExperienceToStringWithText => $"Experience: {ExperienceToString}";

        /// <summary>Amount of Gold in the inventory, with thousands separator.</summary>
        public string GoldToString => Gold.ToString("N0");

        /// <summary>Amount of Gold in the inventory, with thousands separator and preceding text.</summary>
        public string GoldToStringWithText => $"Gold: {GoldToString}";

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

        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(property));

        #endregion Data-Binding
    }
}