using System.ComponentModel;

namespace Sulimn
{
    /// <summary>
    /// Represents an Enemy who opposes the Hero.
    /// </summary>
    internal class Enemy : Character, INotifyPropertyChanged
    {
        private string _type;

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Properties

        public sealed override string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged("Type"); }
        }

        public sealed override int Level
        {
            get { return _level; }
            set { _level = value; OnPropertyChanged("Level"); OnPropertyChanged("LevelAndClassToString"); }
        }

        public sealed override int Experience
        {
            get { return _experience; }
            set { _experience = value; OnPropertyChanged("ExperienceToString"); OnPropertyChanged("ExperienceToStringWithText"); }
        }

        public sealed override Attributes Attributes
        {
            get { return _attributes; }
            set { _attributes = value; OnPropertyChanged("Attributes"); }
        }

        public sealed override Statistics Statistics
        {
            get { return _statistics; }
            set { _statistics = value; OnPropertyChanged("Statistics"); }
        }

        public sealed override Equipment Equipment
        {
            get { return _equipment; }
            set { _equipment = value; OnPropertyChanged("Equipment"); }
        }

        public sealed override Inventory Inventory
        {
            get { return _inventory; }
            set { _inventory = value; OnPropertyChanged("Inventory"); }
        }

        #endregion Properties

        #region Helper Properties

        public string LevelToString
        {
            get { return "Level " + Level; }
        }

        public string ExperienceToString
        {
            get { return Experience.ToString("N0") + " / " + (_level * 100).ToString("N0"); }
        }

        public string ExperienceToStringWithText
        {
            get { return "Experience: " + ExperienceToString; }
        }

        #endregion Helper Properties

        /// <summary>
        /// The Enemy takes Damage.
        /// </summary>
        /// <param name="damage">Amount damaged</param>
        /// <returns></returns>
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

        public override string ToString()
        {
            return Name;
        }

        #region Constructors

        /// <summary>
        /// Initializes a default instance of Enemy.
        /// </summary>
        internal Enemy()
        {
        }

        /// <summary>
        /// Initializes an instance of Enemy by assigning Properties.
        /// </summary>
        /// <param name="name">Name of Enemy</param>
        /// <param name="type">Type of Enemy</param>
        /// <param name="level">Level of Enemy</param>
        /// <param name="experience">Experience Enemy can provide</param>
        /// <param name="attributes">Attributes of Enemy</param>
        /// <param name="statistics">Statistics of Enemy</param>
        /// <param name="equipment">Equipment of Enemy</param>
        /// <param name="inventory">Inventory of Enemy</param>
        internal Enemy(string name, string type, int level, int experience, Attributes attributes, Statistics statistics, Equipment equipment, Inventory inventory)
        {
            Name = name;
            Type = type;
            Level = level;
            Experience = experience;
            Attributes = attributes;
            Statistics = statistics;
            Equipment = equipment;
            Inventory = inventory;
        }

        /// <summary>
        /// Replaces this instance of Enemy with another instance.
        /// </summary>
        /// <param name="otherEnemy">Instance of Enemy that replaces this one</param>
        internal Enemy(Enemy otherEnemy)
        {
            Name = otherEnemy.Name;
            Type = otherEnemy.Type;
            Level = otherEnemy.Level;
            Experience = otherEnemy.Experience;
            Attributes = otherEnemy.Attributes;
            Statistics = otherEnemy.Statistics;
            Equipment = otherEnemy.Equipment;
            Inventory = otherEnemy.Inventory;
        }

        #endregion Constructors
    }
}