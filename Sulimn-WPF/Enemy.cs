using System.ComponentModel;

namespace Sulimn_WPF
{
    internal class Enemy : Character, INotifyPropertyChanged
    {
        private string _name, _type;
        private int _level, _experience, _strength, _vitality, _dexterity, _wisdom, _gold, _currentHealth, _maximumHealth, _currentMagic, _maximumMagic;
        private Weapon _weapon;
        private Armor _head, _body, _legs, _feet;

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
            set { _level = value; OnPropertyChanged("Level"); OnPropertyChanged("LevelToString"); }
        }

        public string LevelToString
        {
            get { return "Level: " + Level; }
        }

        public sealed override int Experience
        {
            get { return _experience; }
            set { _experience = value; OnPropertyChanged("Experience"); OnPropertyChanged("ExperienceToString"); }
        }

        public string ExperienceToString
        {
            get { return Experience.ToString("N0"); }
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
            set { _gold = value; OnPropertyChanged("GoldToString"); }
        }

        public string GoldToString
        {
            get { return Gold.ToString("N0"); }
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
            get { return "Health: " + CurrentHealth.ToString("N0") + " / " + MaximumHealth.ToString("N0"); }
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
            get { return "Magic: " + CurrentMagic.ToString("N0") + " / " + MaximumMagic.ToString("N0"); }
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

        #endregion Properties

        #region Health Manipulation

        /// <summary>
        /// The Enemy takes Damage.
        /// </summary>
        /// <param name="damage">Amount damaged</param>
        /// <returns></returns>
        internal override string TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
                return "The " + Name + " takes " + damage + " damage and has been slain!";
            return "The " + Name + " takes " + damage + " damage.";
        }

        /// <summary>
        /// Heals the Enemy for a specified amount.
        /// </summary>
        /// <param name="healAmount">Amount to be healed</param>
        /// <returns></returns>
        internal override string Heal(int healAmount)
        {
            CurrentHealth += healAmount;
            if (CurrentHealth > MaximumHealth)
            {
                CurrentHealth = MaximumHealth;
                return "The " + _name + " heals to its maximum health.";
            }
            return "The " + _name + " regains " + healAmount + " health.";
        }

        #endregion Health Manipulation

        public override string ToString()
        {
            return Name;
        }

        #region Constructors

        internal Enemy()
        {
        }

        internal Enemy(string enemyName, string enemyType, int enemyLvl, int enemyExp, int enemyStr, int enemyVit, int enemyDex, int enemyWis, int enemyGold, int enemyCurrHealth, int enemyMaxHealth, Weapon enemyCurrWeapon, Armor enemyHead, Armor enemyBody, Armor enemyLegs, Armor enemyFeet)
        {
            Name = enemyName;
            Type = enemyType;
            Level = enemyLvl;
            Experience = enemyExp;
            Strength = enemyStr;
            Vitality = enemyVit;
            Dexterity = enemyDex;
            Wisdom = enemyWis;
            Gold = enemyGold;
            CurrentHealth = enemyCurrHealth;
            MaximumHealth = enemyMaxHealth;
            Head = enemyHead;
            Body = enemyBody;
            Legs = enemyLegs;
            Feet = enemyFeet;
            Weapon = enemyCurrWeapon;
        }

        internal Enemy(Enemy otherEnemy)
        {
            Name = otherEnemy.Name;
            Type = otherEnemy.Type;
            Level = otherEnemy.Level;
            Experience = otherEnemy.Experience;
            Strength = otherEnemy.Strength;
            Vitality = otherEnemy.Vitality;
            Dexterity = otherEnemy.Dexterity;
            Wisdom = otherEnemy.Wisdom;
            Gold = otherEnemy.Gold;
            CurrentHealth = otherEnemy.CurrentHealth;
            MaximumHealth = otherEnemy.MaximumHealth;
            Head = otherEnemy.Head;
            Body = otherEnemy.Body;
            Legs = otherEnemy.Legs;
            Feet = otherEnemy.Feet;
            Weapon = otherEnemy.Weapon;
        }

        #endregion Constructors
    }
}