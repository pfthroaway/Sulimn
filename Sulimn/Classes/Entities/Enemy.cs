namespace Sulimn
{
    /// <summary>Represents an Enemy who opposes the Hero.</summary>
    internal class Enemy : Character
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
                return $"The {Name} takes {damage} damage and has been slain!";
            }
            return $"The {Name} takes {damage} damage.";
        }

        /// <summary>Overrides the ToString() method to return only Name.</summary>
        public sealed override string ToString()
        {
            return Name;
        }

        #region Modifying Properties

        /// <summary>Type of the Enemy</summary>
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Returns the Enemy's level with preceding text.</summary>
        public string LevelToString => $"Level {Level}";

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