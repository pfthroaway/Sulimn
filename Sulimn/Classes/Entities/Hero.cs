using System;
using System.ComponentModel;

namespace Sulimn
{
    /// <summary>
    /// Represents a Hero from Sulimn.
    /// </summary>
    internal class Hero : Character, INotifyPropertyChanged
    {
        private HeroClass _class;
        private int _skillPoints;
        private Spellbook _spellbook = new Spellbook();

        internal void UpdateStatistics()
        {
            if (Statistics.MaximumHealth != (TotalVitality + Level - 1) * 5)
            {
                int diff = (TotalVitality + Level - 1) * 5 - Statistics.MaximumHealth;

                Statistics.CurrentHealth += diff;
                Statistics.MaximumHealth += diff;
            }

            if (Statistics.MaximumMagic != (TotalWisdom + Level - 1) * 5)
            {
                int diff = (TotalWisdom + Level - 1) * 5 - Statistics.MaximumMagic;

                Statistics.CurrentMagic += diff;
                Statistics.MaximumMagic += diff;
            }

            OnPropertyChanged("TotalStrength");
            OnPropertyChanged("TotalVitality");
            OnPropertyChanged("TotalDexterity");
            OnPropertyChanged("TotalWisdom");
        }

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

        #region Properties

        /// <summary>The name of the Hero</summary>
        public sealed override string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>The hashed password of the Hero</summary>
        public string Password { get; set; }

        /// <summary>The HeroClass of the Hero</summary>
        public HeroClass Class
        {
            get { return _class; }
            set
            {
                _class = value;
                OnPropertyChanged("Class");
            }
        }

        /// <summary>The level of the Hero</summary>
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

        /// <summary>The amount of experience the Hero has gained this level</summary>
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

        /// <summary>The amount of available skill points the Hero has</summary>
        public int SkillPoints
        {
            get { return _skillPoints; }
            set
            {
                _skillPoints = value;
                OnPropertyChanged("SkillPoints");
                OnPropertyChanged("SkillPointsToString");
            }
        }

        /// <summary>The attributes of the Hero</summary>
        public sealed override Attributes Attributes
        {
            get { return _attributes; }
            set
            {
                _attributes = value;
                OnPropertyChanged("Attributes");
            }
        }

        /// <summary>The statistics of the Hero</summary>
        public sealed override Statistics Statistics
        {
            get { return _statistics; }
            set
            {
                _statistics = value;
                OnPropertyChanged("Statistics");
            }
        }

        /// <summary>The equipment the Hero is currently using</summary>
        public sealed override Equipment Equipment
        {
            get { return _equipment; }
            set
            {
                _equipment = value;
                OnPropertyChanged("Equipment");
            }
        }

        /// <summary>The list of Spells the Hero currently knows</summary>
        public Spellbook Spellbook
        {
            get { return _spellbook; }
            set
            {
                _spellbook = value;
                OnPropertyChanged("KnownSpells");
            }
        }

        /// <summary>The list of Items the Hero is currently carrying</summary>
        public sealed override Inventory Inventory
        {
            get { return _inventory; }
            set
            {
                _inventory = value;
                OnPropertyChanged("Inventory");
            }
        }

        #endregion Properties

        #region Helper Properties

        /// <summary>The level and class of the Hero</summary>
        public string LevelAndClassToString => "Level " + Level + " " + Class.Name;

        /// <summary>The experience the Hero has gained this level alongside how much is needed to level up</summary>
        public string ExperienceToString => Experience.ToString("N0") + " / " + (_level * 100).ToString("N0");

        /// <summary>The experience the Hero has gained this level alongside how much is needed to level up with preceding text</summary>
        public string ExperienceToStringWithText => "Experience: " + ExperienceToString;

        /// <summary>The amount of skill points the Hero has available to spend</summary>
        public string SkillPointsToString
        {
            get
            {
                if (SkillPoints != 1)
                    return SkillPoints.ToString("N0") + " Skill Points Available";
                return SkillPoints.ToString("N0") + " Skill Point Available";
            }
        }

        /// <summary>Returns the total Strength attribute and bonus produced by the current set of equipment.</summary>
        public int TotalStrength => Attributes.Strength + Equipment.BonusStrength;

        /// <summary>Returns the total Vitality attribute and bonus produced by the current set of equipment.</summary>
        public int TotalVitality => Attributes.Vitality + Equipment.BonusVitality;

        /// <summary>Returns the total Dexterity attribute and bonus produced by the current set of equipment.</summary>
        public int TotalDexterity => Attributes.Dexterity + Equipment.BonusDexterity;

        /// <summary>Returns the total Wisdom attribute and bonus produced by the current set of equipment.</summary>
        public int TotalWisdom => Attributes.Wisdom + Equipment.BonusWisdom;

        #endregion Helper Properties

        #region Experience Manipulation

        /// <summary>
        /// Gains experience for Hero.
        /// </summary>
        /// <param name="exp">Experience</param>
        /// <returns></returns>
        internal string GainExperience(int exp)
        {
            Experience += exp;
            return "You gained " + exp + " experience!" + CheckLevelUp();
        }

        /// <summary>
        /// Checks where a Hero has leveled up.
        /// </summary>
        /// <returns>Returns null if Hero doesn't level up.</returns>
        internal string CheckLevelUp()
        {
            if (Experience >= Level * 100)
                return LevelUp();

            return null;
        }

        /// <summary>
        /// Levels up a Hero.
        /// </summary>
        /// <returns>Returns level up String.</returns>
        private string LevelUp()
        {
            Experience -= Level * 100;
            Level += 1;
            SkillPoints += 5;
            Statistics.CurrentHealth += 5;
            Statistics.MaximumHealth += 5;
            Statistics.CurrentMagic += 5;
            Statistics.MaximumMagic += 5;
            return Environment.NewLine + Environment.NewLine +
             "You gained a level! You also gained 5 health, 5 magic, and 5 skill points!";
        }

        #endregion Experience Manipulation

        #region Health Manipulation

        /// <summary>The Hero takes damage.</summary>
        /// <param name="damage">Damage amount</param>
        /// <returns></returns>
        internal string TakeDamage(int damage)
        {
            Statistics.CurrentHealth -= damage;
            if (Statistics.CurrentHealth <= 0)
                return "You have taken " + damage + " damage and have been slain.";
            return "You have taken " + damage + " damage.";
        }

        /// <summary>Heals the Hero for a specified amount.</summary>
        /// <param name="healAmount">Amount to be healed</param>
        /// <returns>Returns text saying the Hero was healed</returns>
        internal string Heal(int healAmount)
        {
            Statistics.CurrentHealth += healAmount;
            if (Statistics.CurrentHealth > Statistics.MaximumHealth)
            {
                Statistics.CurrentHealth = Statistics.MaximumHealth;
                return "You heal to your maximum health.";
            }
            return "You heal for " + healAmount + " health.";
        }

        #endregion Health Manipulation

        #region Constructors

        /// <summary>Initializes a default instance of Hero.</summary>
        internal Hero()
        {
        }

        /// <summary>Initializes an instance of Hero by assigning Properties.</summary>
        /// <param name="name">Name of Hero</param>
        /// <param name="password">Password of Hero</param>
        /// <param name="heroClass">Class of Hero</param>
        /// <param name="level">Level of Hero</param>
        /// <param name="experience">Experience of Hero</param>
        /// <param name="skillPoints">Skill Points of Hero</param>
        /// <param name="attributes">Attributes of Hero</param>
        /// <param name="statistics">Statistics of Hero</param>
        /// <param name="equipment">Equipment of Hero</param>
        /// <param name="spellbook">Spellbook of Hero</param>
        /// <param name="inventory">Inventory of Hero</param>
        internal Hero(string name, string password, HeroClass heroClass, int level, int experience, int skillPoints,
        Attributes attributes, Statistics statistics, Equipment equipment, Spellbook spellbook, Inventory inventory)
        {
            Name = name;
            Password = password;
            Class = heroClass;
            Level = level;
            Experience = experience;
            SkillPoints = skillPoints;
            Attributes = attributes;
            Statistics = statistics;
            Equipment = equipment;
            Spellbook = spellbook;
            Inventory = inventory;
        }

        /// <summary>Replaces this instance of Hero with another instance.</summary>
        /// <param name="otherHero">Instance of Hero to replace this one</param>
        internal Hero(Hero otherHero)
        {
            Name = otherHero.Name;
            Password = otherHero.Password;
            Class = otherHero.Class;
            Level = otherHero.Level;
            Experience = otherHero.Experience;
            SkillPoints = otherHero.SkillPoints;
            Attributes = new Attributes(otherHero.Attributes);
            Statistics = new Statistics(otherHero.Statistics);
            Equipment = new Equipment(otherHero.Equipment);
            Spellbook = new Spellbook(otherHero.Spellbook);
            Inventory = new Inventory(otherHero.Inventory);
        }

        #endregion Constructors
    }
}