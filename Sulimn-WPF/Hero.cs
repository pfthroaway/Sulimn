using System;
using System.ComponentModel;

namespace Sulimn_WPF
{
    /// <summary>
    /// Hero from Sulimn.
    /// </summary>
    internal class Hero : Character, INotifyPropertyChanged
    {
        private string _name, _password, _className;
        private int _level, _experience, _skillPoints, _strength, _vitality, _dexterity, _wisdom, _gold, _currentHealth, _maximumHealth, _currentMagic, _maximumMagic;
        private Weapon _weapon = new Weapon();
        private Armor _head, _body, _legs, _feet = new Armor();
        private Spellbook _spellbook = new Spellbook();
        private Inventory _inventory = new Inventory();

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

        public string ClassName
        {
            get { return _className; }
            set { _className = value; OnPropertyChanged("ClassName"); }
        }

        public sealed override int Level
        {
            get { return _level; }
            set { _level = value; OnPropertyChanged("Level"); OnPropertyChanged("LevelAndClassToString"); }
        }

        public string LevelAndClassToString
        {
            get { return "Level " + Level + " " + ClassName; }
        }

        public sealed override int Experience
        {
            get { return _experience; }
            set { _experience = value; OnPropertyChanged("ExperienceToString"); OnPropertyChanged("ExperienceToStringWithText"); }
        }

        public string ExperienceToString
        {
            get { return Experience.ToString("N0") + " / " + (_level * 100).ToString("N0"); }
        }

        public string ExperienceToStringWithText
        {
            get { return "Experience: " + ExperienceToString; }
        }

        public int SkillPoints
        {
            get { return _skillPoints; }
            set { _skillPoints = value; OnPropertyChanged("SkillPoints"); OnPropertyChanged("SkillPointsToString"); }
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

        public sealed override int Strength
        {
            get { return _strength; }
            set { _strength = value; OnPropertyChanged("Strength"); }
        }

        public sealed override int Vitality
        {
            get { return _vitality; }
            set { _vitality = value; OnPropertyChanged("Vitality"); }
        }

        public sealed override int Dexterity
        {
            get { return _dexterity; }
            set { _dexterity = value; OnPropertyChanged("Dexterity"); }
        }

        public sealed override int Wisdom
        {
            get { return _wisdom; }
            set { _wisdom = value; OnPropertyChanged("Wisdom"); }
        }

        public sealed override int Gold
        {
            get { return _gold; }
            set { _gold = value; OnPropertyChanged("Gold"); OnPropertyChanged("GoldToString"); OnPropertyChanged("GoldToStringWithText"); }
        }

        public string GoldToString
        {
            get { return Gold.ToString("N0"); }
        }

        public string GoldToStringWithText
        {
            get { return "Gold: " + GoldToString; }
        }

        public sealed override int CurrentHealth
        {
            get { return _currentHealth; }
            set { _currentHealth = value; OnPropertyChanged("HealthToString"); OnPropertyChanged("HealthToStringWithText"); }
        }

        public sealed override int MaximumHealth
        {
            get { return _maximumHealth; }
            set { _maximumHealth = value; OnPropertyChanged("HealthToString"); OnPropertyChanged("HealthToStringWithText"); }
        }

        public string HealthToString
        {
            get { return CurrentHealth.ToString("N0") + " / " + MaximumHealth.ToString("N0"); }
        }

        public string HealthToStringWithText
        {
            get { return "Health: " + HealthToString; }
        }

        public sealed override int CurrentMagic
        {
            get { return _currentMagic; }
            set { _currentMagic = value; OnPropertyChanged("MagicToString"); OnPropertyChanged("MagicToStringWithText"); }
        }

        public sealed override int MaximumMagic
        {
            get { return _maximumMagic; }
            set { _maximumMagic = value; OnPropertyChanged("MagicToString"); OnPropertyChanged("MagicToStringWithText"); }
        }

        public string MagicToString
        {
            get { return CurrentMagic.ToString("N0") + " / " + MaximumMagic.ToString("N0"); }
        }

        public string MagicToStringWithText
        {
            get { return "Magic: " + MagicToString; }
        }

        public Spellbook Spellbook
        {
            get { return _spellbook; }
            set { _spellbook = value; OnPropertyChanged("KnownSpells"); }
        }

        public sealed override Weapon Weapon
        {
            get { return _weapon; }
            set { _weapon = value; OnPropertyChanged("Weapon"); }
        }

        public sealed override Armor Head
        {
            get { return _head; }
            set { _head = value; OnPropertyChanged("Head"); }
        }

        public sealed override Armor Body
        {
            get { return _body; }
            set { _body = value; OnPropertyChanged("Body"); }
        }

        public sealed override Armor Legs
        {
            get { return _legs; }
            set { _legs = value; OnPropertyChanged("Legs"); }
        }

        public sealed override Armor Feet
        {
            get { return _feet; }
            set { _feet = value; OnPropertyChanged("Feet"); }
        }

        public Inventory Inventory
        {
            get { return _inventory; }
            set { _inventory = value; OnPropertyChanged("Inventory"); }
        }

        #endregion Properties

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
            CurrentHealth += 5;
            MaximumHealth += 5;
            CurrentMagic += 5;
            MaximumMagic += 5;
            return Environment.NewLine + Environment.NewLine + "You gained a level! You also gained 5 health, 5 magic, and 5 skill points!";
        }

        #endregion Experience Manipulation

        #region Health Manipulation

        /// <summary>
        /// The Hero takes damage.
        /// </summary>
        /// <param name="damage">Damage amount</param>
        /// <returns></returns>
        internal override string TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
                return "You have taken " + damage + " damage and have been slain.";
            return "You have taken " + damage + " damage.";
        }

        /// <summary>
        /// Heals the Hero for a specified amount.
        /// </summary>
        /// <param name="healAmount">Amount to be healed</param>
        /// <returns></returns>
        internal override string Heal(int healAmount)
        {
            CurrentHealth += healAmount;
            if (CurrentHealth > MaximumHealth)
            {
                CurrentHealth = MaximumHealth;
                return "You heal to your maximum health.";
            }
            return "You heal for " + healAmount + " health.";
        }

        #endregion Health Manipulation

        /// <summary>
        /// Restores magic to the Hero.
        /// </summary>
        /// <param name="restoreAmount">Amount of Magic to be restored.</param>
        /// <returns>String saying magic was restored</returns>
        internal string RestoreMagic(int restoreAmount)
        {
            CurrentMagic += restoreAmount;
            if (CurrentMagic > MaximumMagic)
            {
                CurrentMagic = MaximumMagic;
                return "You restore your magic to its maximum.";
            }
            return "You restore " + restoreAmount + " magic.";
        }

        public override string ToString()
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
        /// Initializes an instance of Hero by setting Properties.
        /// </summary>
        /// <param name="heroName">Name of Hero</param>
        /// <param name="password">Password of Hero</param>
        /// <param name="heroClassName">Class of Hero</param>
        /// <param name="heroLevel">Level of Hero</param>
        /// <param name="heroExperience">Experience of Hero</param>
        /// <param name="heroSkillPts">Skill Points of Hero</param>
        /// <param name="heroStrength">Strength of Hero</param>
        /// <param name="heroVitality">Vitality of Hero</param>
        /// <param name="heroDexterity">Dexterity of Hero</param>
        /// <param name="heroWisdom">Wisdom of Hero</param>
        /// <param name="heroGold">Gold possessed by Hero</param>
        /// <param name="heroCurrentHealth">Current health of Hero</param>
        /// <param name="heroMaximumHealth">Maximum health of Hero</param>
        /// <param name="heroCurrentMagic">Current magic of Hero</param>
        /// <param name="heroMaximumMagic">Maximum magic of Hero</param>
        /// <param name="heroHead">Head Armor worn by Hero</param>
        /// <param name="heroBody">Body Armor worn by Hero</param>
        /// <param name="heroLegs">Leg Armor worn by Hero</param>
        /// <param name="heroFeet">Feet Armor worn by Hero</param>
        /// <param name="heroWeapon">Weapon wielded by Hero</param>
        /// <param name="heroSpellbook">Spellbook of Hero</param>
        /// <param name="heroInventory">Inventory of Hero</param>
        internal Hero(string heroName, string password, string heroClassName, int heroLevel, int heroExperience, int heroSkillPts, int heroStrength, int heroVitality, int heroDexterity, int heroWisdom, int heroGold, int heroCurrentHealth, int heroMaximumHealth, int heroCurrentMagic, int heroMaximumMagic, Armor heroHead, Armor heroBody, Armor heroLegs, Armor heroFeet, Weapon heroWeapon, Spellbook heroSpellbook, Inventory heroInventory)
        {
            Name = heroName;
            Password = password;
            ClassName = heroClassName;
            Level = heroLevel;
            Experience = heroExperience;
            Strength = heroStrength;
            Vitality = heroVitality;
            Dexterity = heroDexterity;
            Wisdom = heroWisdom;
            Gold = heroGold;
            CurrentHealth = heroCurrentHealth;
            MaximumHealth = heroMaximumHealth;
            CurrentMagic = heroCurrentMagic;
            MaximumMagic = heroMaximumMagic;
            Head = heroHead;
            Body = heroBody;
            Legs = heroLegs;
            Feet = heroFeet;
            Weapon = heroWeapon;
            Spellbook = heroSpellbook;
            Inventory = heroInventory;
        }

        /// <summary>
        /// Replaces this instance of Hero with another instance
        /// </summary>
        /// <param name="otherHero">Instance of Hero to replace this one</param>
        internal Hero(Hero otherHero)
        {
            Name = otherHero.Name;
            Password = otherHero.Password;
            ClassName = otherHero.ClassName;
            Level = otherHero.Level;
            Experience = otherHero.Experience;
            SkillPoints = otherHero.SkillPoints;
            Strength = otherHero.Strength;
            Vitality = otherHero.Vitality;
            Dexterity = otherHero.Dexterity;
            Wisdom = otherHero.Wisdom;
            Gold = otherHero.Gold;
            CurrentHealth = otherHero.CurrentHealth;
            MaximumHealth = otherHero.MaximumHealth;
            CurrentMagic = otherHero.CurrentMagic;
            MaximumMagic = otherHero.MaximumMagic;
            Head = otherHero.Head;
            Body = otherHero.Body;
            Legs = otherHero.Legs;
            Feet = otherHero.Feet;
            Weapon = otherHero.Weapon;
            Spellbook = otherHero.Spellbook;
            Inventory = otherHero.Inventory;
        }

        #endregion Constructors
    }
}