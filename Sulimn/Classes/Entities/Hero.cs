using System;
using Sulimn.Classes.HeroParts;

namespace Sulimn.Classes.Entities
{
    /// <summary>Represents a Hero from Sulimn.</summary>
    internal class Hero : Character
    {
        private HeroClass _class;
        private int _skillPoints;
        private Progression progression;
        private Spellbook _spellbook = new Spellbook();
        private bool _hardcore;

        /// <summary>Updates the Hero's Statistics.</summary>
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

        #region Modifying Properties

        /// <summary>The hashed password of the Hero</summary>
        public string Password { get; set; }

        /// <summary>The HeroClass of the Hero</summary>
        public HeroClass Class
        {
            get => _class;
            private set
            {
                _class = value;
                OnPropertyChanged("Class");
            }
        }

        /// <summary>The amount of available skill points the Hero has</summary>
        public int SkillPoints
        {
            get => _skillPoints;
            set
            {
                _skillPoints = value;
                OnPropertyChanged("SkillPoints");
                OnPropertyChanged("SkillPointsToString");
            }
        }

        /// <summary>The progress the Hero has made.</summary>
        public Progression Progression { get; set; }

        /// <summary>The list of Spells the Hero currently knows</summary>
        public Spellbook Spellbook
        {
            get => _spellbook;
            private set
            {
                _spellbook = value;
                OnPropertyChanged("Spellbook");
            }
        }

        /// <summary>Will the player be deleted on death?</summary>
        public bool Hardcore
        {
            get => _hardcore; set
            {
                _hardcore = value;
                OnPropertyChanged("Hardcore");
                OnPropertyChanged("HardcoreToString");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Will the player be deleted on death?</summary>
        public string HardcoreToString => Hardcore ? "Hardcore" : "Softcore";

        /// <summary>The level and class of the Hero</summary>
        public string LevelAndClassToString => $"Level {Level} {Class.Name}";

        /// <summary>The amount of skill points the Hero has available to spend</summary>
        public string SkillPointsToString => SkillPoints != 1 ? $"{SkillPoints:N0} Skill Points Available" : $"{SkillPoints:N0} Skill Point Available";

        #endregion Helper Properties

        #region Experience Manipulation

        /// <summary>Gains experience for Hero.</summary>
        /// <param name="exp">Experience</param>
        /// <returns>Returns text about the Hero gaining experience</returns>
        internal string GainExperience(int exp)
        {
            Experience += exp;
            return $"You gained {exp} experience!{CheckLevelUp()}";
        }

        /// <summary>Checks where a Hero has leveled up.</summary>
        /// <returns>Returns null if Hero doesn't level up</returns>
        private string CheckLevelUp() => Experience >= Level * 100 ? LevelUp() : null;

        /// <summary>Levels up a Hero.</summary>
        /// <returns>Returns text about the Hero leveling up</returns>
        private string LevelUp()
        {
            Experience -= Level * 100;
            Level++;
            SkillPoints += 5;
            Statistics.CurrentHealth += 5;
            Statistics.MaximumHealth += 5;
            Statistics.CurrentMagic += 5;
            Statistics.MaximumMagic += 5;
            return "\n\nYou gained a level! You also gained 5 health, 5 magic, and 5 skill points!";
        }

        #endregion Experience Manipulation

        #region Health Manipulation

        /// <summary>The Hero takes damage.</summary>
        /// <param name="damage">Damage amount</param>
        /// <returns>Returns text about the Hero leveling up.</returns>
        internal string TakeDamage(int damage)
        {
            Statistics.CurrentHealth -= damage;
            return Statistics.CurrentHealth <= 0
            ? $"You have taken {damage} damage and have been slain."
            : $"You have taken {damage} damage.";
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
            return $"You heal for {healAmount:N0} health.";
        }

        #endregion Health Manipulation

        #region Override Operators

        private static bool Equals(Hero left, Hero right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Level == right.Level && left.Experience == right.Experience && left.SkillPoints == right.SkillPoints && left.Hardcore == right.Hardcore && left.Spellbook == right.Spellbook && left.Class == right.Class && left.Attributes == right.Attributes && left.Equipment == right.Equipment && left.Inventory == right.Inventory && left.Statistics == right.Statistics && left.Progression == right.Progression;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Hero);

        public bool Equals(Hero otherHero) => Equals(this, otherHero);

        public static bool operator ==(Hero left, Hero right) => Equals(left, right);

        public static bool operator !=(Hero left, Hero right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        public sealed override string ToString() => Name;

        #endregion Override Operators

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
        /// <param name="progression">The progress the Hero has made</param>
        /// <param name="hardcore">Will the character be deleted on death?</param>
        internal Hero(string name, string password, HeroClass heroClass, int level, int experience, int skillPoints,
        Attributes attributes, Statistics statistics, Equipment equipment, Spellbook spellbook, Inventory inventory, Progression progression, bool hardcore)
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
            Progression = progression;
            Hardcore = hardcore;
        }

        /// <summary>Replaces this instance of Hero with another instance.</summary>
        /// <param name="other">Instance of Hero to replace this one</param>
        internal Hero(Hero other) : this(other.Name, other.Password, other.Class, other.Level, other.Experience, other.SkillPoints, new Attributes(other.Attributes), new Statistics(other.Statistics), new Equipment(other.Equipment), new Spellbook(other.Spellbook), new Inventory(other.Inventory), other.Progression, other.Hardcore)
        {
        }

        #endregion Constructors
    }
}