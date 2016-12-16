using System.ComponentModel;

namespace Sulimn
{
    /// <summary>Represents an Enemy who opposes the Hero.</summary>
    internal class Enemy : Character, INotifyPropertyChanged
    {
        private string _type;

        /// <summary>The Enemy takes Damage.</summary>
        /// <param name="damage">Amount damaged</param>
        /// <returns>Text saying the Enemy took damageJesus</returns>
        internal string TakeDamage(int damage)
        {
            Statistics.CurrentHealth -= damage;
            if (Statistics.CurrentHealth <= 0)
            {
                Statistics.CurrentHealth = 0;
                return "The " + Name + " takes " + damage + " damage and has been slain!";
            }
            return "The " + Name + " takes " + damage + " damage.";
        }

        /// <summary>Overrides the ToString() method to return only Name.</summary>
        public sealed override string ToString()
        {
            return Name;
        }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Modifying Properties

        /// <summary>Name of the Enemy</summary>
        public sealed override string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>Type of the Enemy</summary>
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }

        /// <summary>Level of the Enemy</summary>
        public sealed override int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged("Level");
                OnPropertyChanged("LevelAndClassToString");
            }
        }

        /// <summary>Experience given to the Hero when the Enemy dies</summary>
        public sealed override int Experience
        {
            get { return _experience; }
            set
            {
                _experience = value;
                OnPropertyChanged("ExperienceToString");
                OnPropertyChanged("ExperienceToStringWithText");
            }
        }

        /// <summary>Attributes of the Enemy</summary>
        public sealed override Attributes Attributes
        {
            get { return _attributes; }
            set
            {
                _attributes = value;
                OnPropertyChanged("Attributes");
            }
        }

        /// <summary>Statistics of the Enemy</summary>
        public sealed override Statistics Statistics
        {
            get { return _statistics; }
            set
            {
                _statistics = value;
                OnPropertyChanged("Statistics");
            }
        }

        /// <summary>Equpiment owned by the Enemy</summary>
        public sealed override Equipment Equipment
        {
            get { return _equipment; }
            set
            {
                _equipment = value;
                OnPropertyChanged("Equipment");
            }
        }

        /// <summary>Inventory of the Enemy</summary>
        public sealed override Inventory Inventory
        {
            get { return _inventory; }
            set
            {
                _inventory = value;
                OnPropertyChanged("Inventory");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Returns the total Strength attribute and bonus produced by the current set of equipment.</summary>
        public int TotalStrength => Attributes.Strength + Equipment.BonusStrength;

        /// <summary>Returns the total Vitality attribute and bonus produced by the current set of equipment.</summary>
        public int TotalVitality => Attributes.Vitality + Equipment.BonusVitality;

        /// <summary>Returns the total Dexterity attribute and bonus produced by the current set of equipment.</summary>
        public int TotalDexterity => Attributes.Dexterity + Equipment.BonusDexterity;

        /// <summary>Returns the total Wisdom attribute and bonus produced by the current set of equipment.</summary>
        public int TotalWisdom => Attributes.Wisdom + Equipment.BonusWisdom;

        /// <summary>Returns the Enemy's level with preceding text.</summary>
        public string LevelToString => "Level " + Level;

        /// <summary>Returns the experience of the Enemy with thousand separators.</summary>
        public string ExperienceToString => Experience.ToString("N0");

        /// <summary>Returns the experience of the Enemy with thousand separators and preceding text.</summary>
        public string ExperienceToStringWithText => "Experience: " + ExperienceToString;

        #endregion Helper Properties

        #region Constructors

        /// <summary>Initializes a default instance of Enemy.</summary>
        internal Enemy()
        {
        }

        /// <summary>Initializes an instance of Enemy by assigning Properties.</summary>
        /// <param name="name">Name of Enemy</param>
        /// <param name="type">Type of Enemy</param>
        /// <param name="level">Level of Enemy</param>
        /// <param name="experience">Experience Enemy can provide</param>
        /// <param name="attributes">Attributes of Enemy</param>
        /// <param name="statistics">Statistics of Enemy</param>
        /// <param name="equipment">Equipment of Enemy</param>
        /// <param name="inventory">Inventory of Enemy</param>
        internal Enemy(string name, string type, int level, int experience, Attributes attributes, Statistics statistics,
        Equipment equipment, Inventory inventory)
        {
            Name = name;
            Type = type;
            Level = level;
            Experience = experience;
            Attributes = new Attributes(attributes);
            Statistics = new Statistics(statistics);
            Equipment = new Equipment(equipment);
            Inventory = new Inventory(inventory);
        }

        /// <summary>Replaces this instance of Enemy with another instance.</summary>
        /// <param name="otherEnemy">Instance of Enemy that replaces this one</param>
        internal Enemy(Enemy otherEnemy)
        {
            Name = otherEnemy.Name;
            Type = otherEnemy.Type;
            Level = otherEnemy.Level;
            Experience = otherEnemy.Experience;
            Attributes = new Attributes(otherEnemy.Attributes);
            Statistics = new Statistics(otherEnemy.Statistics);
            Equipment = new Equipment(otherEnemy.Equipment);
            Inventory = new Inventory(otherEnemy.Inventory);
        }

        #endregion Constructors
    }
}