using System;
using System.ComponentModel;

namespace Sulimn
{
    /// <summary>
    /// Represents a Hero from Sulimn.
    /// </summary>
    internal class Hero : Character, INotifyPropertyChanged
    {
        private string _password;
        private HeroClass _class;
        private int _skillPoints;
        private Spellbook _spellbook = new Spellbook();

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

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public HeroClass Class
        {
            get { return _class; }
            set { _class = value; OnPropertyChanged("Class"); }
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

        public int SkillPoints
        {
            get { return _skillPoints; }
            set { _skillPoints = value; OnPropertyChanged("SkillPoints"); OnPropertyChanged("SkillPointsToString"); }
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

        public Spellbook Spellbook
        {
            get { return _spellbook; }
            set { _spellbook = value; OnPropertyChanged("KnownSpells"); }
        }

        public sealed override Inventory Inventory
        {
            get { return _inventory; }
            set { _inventory = value; OnPropertyChanged("Inventory"); }
        }

        #endregion Properties

        #region Helper Properties

        public string LevelAndClassToString
        {
            get { return "Level " + Level + " " + Class.Name; }
        }

        public string ExperienceToString
        {
            get { return Experience.ToString("N0") + " / " + (_level * 100).ToString("N0"); }
        }

        public string ExperienceToStringWithText
        {
            get { return "Experience: " + ExperienceToString; }
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
            Experience -= (Level * 100);
            Level += 1;
            SkillPoints += 5;
            Statistics.CurrentHealth += 5;
            Statistics.MaximumHealth += 5;
            Statistics.CurrentMagic += 5;
            Statistics.MaximumMagic += 5;
            return Environment.NewLine + Environment.NewLine + "You gained a level! You also gained 5 health, 5 magic, and 5 skill points!";
        }

        #endregion Experience Manipulation

        #region Health Manipulation

        /// <summary>
        /// The Hero takes damage.
        /// </summary>
        /// <param name="damage">Damage amount</param>
        /// <returns></returns>
        internal string TakeDamage(int damage)
        {
            Statistics.CurrentHealth -= damage;
            if (Statistics.CurrentHealth <= 0)
                return "You have taken " + damage + " damage and have been slain.";
            return "You have taken " + damage + " damage.";
        }

        /// <summary>
        /// Heals the Hero for a specified amount.
        /// </summary>
        /// <param name="healAmount">Amount to be healed</param>
        /// <returns></returns>
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

        public sealed override string ToString()
        {
            return Name;
        }

        #region Constructors

        /// <summary>
        /// Initializes a default instance of Hero.
        /// </summary>
        internal Hero()
        {
        }

        /// <summary>
        /// Initializes an instance of Hero by assigning Properties.
        /// </summary>
        /// <param name="heroName">Name of Hero</param>
        /// <param name="password">Password of Hero</param>
        /// <param name="heroClass">Class of Hero</param>
        /// <param name="heroLevel">Level of Hero</param>
        /// <param name="heroExperience">Experience of Hero</param>
        /// <param name="heroSkillPts">Skill Points of Hero</param>
        /// <param name="attributes">Attributes of Hero</param>
        /// <param name="statistics">Statistics of Hero</param>
        /// <param name="equipment">Equipment of Hero</param>
        /// <param name="heroSpellbook">Spellbook of Hero</param>
        /// <param name="heroInventory">Inventory of Hero</param>
        internal Hero(string heroName, string password, HeroClass heroClass, int heroLevel, int heroExperience, int heroSkillPts, Attributes attributes, Statistics statistics, Equipment equipment, Spellbook heroSpellbook, Inventory heroInventory)
        {
            Name = heroName;
            Password = password;
            Class = heroClass;
            Level = heroLevel;
            Experience = heroExperience;
            Attributes = attributes;
            Statistics = statistics;
            Equipment = equipment;
            Spellbook = heroSpellbook;
            Inventory = heroInventory;
        }

        /// <summary>
        /// Replaces this instance of Hero with another instance.
        /// </summary>
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