using Newtonsoft.Json;
using Sulimn.Classes.Enums;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sulimn.Classes.Entities
{
    /// <summary>Represents a Hero from Sulimn.</summary>
    internal class Hero : Character
    {
        private HeroClass _class;
        private int _skillPoints;
        private Bank _bank = new Bank();
        private bool _hardcore;
        private string _password;

        /// <summary>Updates the Hero's Statistics.</summary>
        internal void UpdateStatistics()
        {
            if (Statistics.MaximumHealth != (TotalVitality + Level - 1) * 5)
            {
                int diff = ((TotalVitality + Level - 1) * 5) - Statistics.MaximumHealth;

                Statistics.CurrentHealth += diff;
                Statistics.MaximumHealth += diff;
            }

            if (Statistics.MaximumMagic != (TotalWisdom + Level - 1) * 5)
            {
                int diff = ((TotalWisdom + Level - 1) * 5) - Statistics.MaximumMagic;

                Statistics.CurrentMagic += diff;
                Statistics.MaximumMagic += diff;
            }

            NotifyPropertyChanged(nameof(TotalStrength), nameof(TotalVitality), nameof(TotalDexterity), nameof(TotalWisdom));
        }

        #region Modifying Properties

        /// <summary>The hashed password of the <see cref="Hero"/>.</summary>
        [JsonProperty(Order = -4)]
        public string Password
        {
            get => _password;
            set { _password = value; NotifyPropertyChanged(nameof(Password)); }
        }

        /// <summary>The <see cref="HeroClass"/> of the <see cref="Hero"/>.</summary>
        [JsonIgnore]
        public HeroClass Class
        {
            get => _class;
            set { _class = value; NotifyPropertyChanged(nameof(Class), nameof(LevelAndClassToString)); }
        }

        /// <summary>The <see cref="HeroClass"/> of the <see cref="Hero"/>, set up to import from JSON.</summary>
        [JsonProperty(Order = -3)]
        public string ClassString
        {
            get => Class.Name;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    Class = GameState.AllClasses.Find(o => o.Name == value);
            }
        }

        /// <summary>Will the player be deleted on death?</summary>
        [JsonProperty(Order = -2)]
        public bool Hardcore
        {
            get => _hardcore;
            set { _hardcore = value; NotifyPropertyChanged(nameof(Hardcore), nameof(HardcoreToString)); }
        }

        /// <summary>The amount of available skill points the <see cref="Hero"/> has</summary>
        [JsonProperty(Order = -1)]
        public int SkillPoints
        {
            get => _skillPoints;
            set { _skillPoints = value; NotifyPropertyChanged(nameof(SkillPoints), nameof(SkillPointsToString)); }
        }

        /// <summary>The progress the <see cref="Hero"/> has made.</summary>
        [JsonProperty(Order = 7)]
        public Progression Progression { get; set; }

        /// <summary>The <see cref="Hero"/>'s <see cref="HeroParts.Bank"/>.</summary>
        [JsonProperty(Order = 9)]
        public Bank Bank
        {
            get => _bank;
            set { _bank = value; NotifyPropertyChanged(nameof(Bank)); }
        }

        /// <summary>The <see cref="Hero"/>'s current <see cref="Quest"/>s.</summary>
        [JsonProperty(Order = 12)]
        public List<Quest> Quests { get; set; } = new List<Quest>();

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>The amount of gold it would cost to repair all <see cref="Item"/>s in the inventory.</summary>
        [JsonIgnore]
        public int InventoryRepairCost => Inventory.Sum(itm => itm.RepairCost);

        /// <summary>The amount of gold it would cost to repair all <see cref="Item"/>s in the inventory, formatted.</summary>
        [JsonIgnore]
        public string InventoryRepairCostToString => InventoryRepairCost.ToString("N0");

        /// <summary>The amount of gold it would cost to repair all <see cref="Item"/>s in the inventory, formatted with preceding text.</summary>
        [JsonIgnore]
        public string InventoryRepairCostToStringWithText => $"Repair Cost: {InventoryRepairCostToString}";

        /// <summary>The amount of gold it would cost to repair all <see cref="Item"/>s in the inventory and <see cref="Equipment"/>.</summary>
        [JsonIgnore]
        public int TotalRepairCost => InventoryRepairCost + Equipment.RepairCost;

        /// <summary>The amount of gold it would cost to repair all <see cref="Item"/>s in the inventory and <see cref="Equipment"/>, formatted.</summary>
        [JsonIgnore]
        public string TotalRepairCostToString => TotalRepairCost.ToString("N0");

        /// <summary>The amount of gold it would cost to repair all <see cref="Item"/>s in the inventory and <see cref="Equipment"/>, formatted with preceding text.</summary>
        [JsonIgnore]
        public string TotalRepairCostToStringWithText => $"Total Repair Cost: {TotalRepairCostToString}";

        /// <summary>Will the player be deleted on death?</summary>
        [JsonIgnore]
        public string HardcoreToString => Hardcore ? "Hardcore" : "Softcore";

        /// <summary>The level and class of the <see cref="Hero"/>.</summary>
        [JsonIgnore]
        public string LevelAndClassToString => $"Level {Level} {Class.Name}";

        /// <summary>The amount of skill points the <see cref="Hero"/> has available to spend, formatted.</summary>
        [JsonIgnore]
        public string SkillPointsToString => SkillPoints != 1 ? $"{SkillPoints:N0} Skill Points Available" : $"{SkillPoints:N0} Skill Point Available";

        #endregion Helper Properties

        #region Experience Manipulation

        /// <summary>Gains experience for <see cref="Hero"/>.</summary>
        /// <param name="exp">Experience</param>
        /// <returns>Returns text about the <see cref="Hero"/> gaining experience</returns>
        internal string GainExperience(int exp)
        {
            Experience += exp;
            return $"You gained {exp} experience!{CheckLevelUp()}";
        }

        /// <summary>Checks where a <see cref="Hero"/> has leveled up.</summary>
        /// <returns>Returns null if <see cref="Hero"/> doesn't level up</returns>
        private string CheckLevelUp() => Experience >= Level * 100 ? LevelUp() : null;

        /// <summary>Levels up a <see cref="Hero"/>.</summary>
        /// <returns>Returns text about the <see cref="Hero"/> leveling up</returns>
        private string LevelUp()
        {
            Experience -= Level * 100;
            Level++;
            SkillPoints += 5;
            Statistics.CurrentHealth += 5;
            Statistics.MaximumHealth += 5;
            Statistics.CurrentMagic += 5;
            Statistics.MaximumMagic += 5;
            Bank.LoanAvailable += 250;
            return "\n\nYou gained a level! You also gained 5 health, 5 magic, and 5 skill points!";
        }

        #endregion Experience Manipulation

        #region Health Manipulation

        /// <summary>The <see cref="Hero"/> takes damage.</summary>
        /// <param name="damage">Damage amount</param>
        /// <returns>Returns text about the <see cref="Hero"/> leveling up.</returns>
        internal string TakeDamage(int damage)
        {
            Statistics.CurrentHealth -= damage;
            return Statistics.CurrentHealth <= 0
            ? $"You have taken {damage} damage and have been slain."
            : $"You have taken {damage} damage.";
        }

        /// <summary>Heals the <see cref="Hero"/> for a specified amount.</summary>
        /// <param name="healAmount">Amount to be healed</param>
        /// <returns>Returns text saying the <see cref="Hero"/> was healed</returns>
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

        #region Inventory Management

        /// <summary>Equips an <see cref="Item"/> into a <see cref="Hero"/>'s Equipment.</summary>
        /// <param name="item"><see cref="Item"/> to be equipped</param>
        /// <param name="side">If <see cref="Item"/> is a Ring, which side is it?</param>
        internal void Equip(Item item, RingHand side = RingHand.Left)
        {
            switch (item.Type)
            {
                case ItemType.MeleeWeapon:
                case ItemType.RangedWeapon:
                    if (Equipment.Weapon != GameState.DefaultWeapon)
                        AddItem(Equipment.Weapon);
                    Equipment.Weapon = new Item(item);
                    break;

                case ItemType.HeadArmor:
                    if (Equipment.Head != GameState.DefaultHead)
                        AddItem(Equipment.Head);
                    Equipment.Head = new Item(item);
                    break;

                case ItemType.BodyArmor:
                    if (Equipment.Body != GameState.DefaultBody)
                        AddItem(Equipment.Body);
                    Equipment.Body = new Item(item);
                    break;

                case ItemType.HandArmor:
                    if (Equipment.Hands != GameState.DefaultHands)
                        AddItem(Equipment.Hands);
                    Equipment.Hands = new Item(item);
                    break;

                case ItemType.LegArmor:
                    if (Equipment.Legs != GameState.DefaultLegs)
                        AddItem(Equipment.Legs);
                    Equipment.Legs = new Item(item);
                    break;

                case ItemType.FeetArmor:
                    if (Equipment.Feet != GameState.DefaultFeet)
                        AddItem(Equipment.Feet);
                    Equipment.Feet = new Item(item);
                    break;

                case ItemType.Ring:
                    switch (side)
                    {
                        case RingHand.Left:
                            if (Equipment.LeftRing != new Item())
                                AddItem(Equipment.LeftRing);
                            Equipment.LeftRing = new Item(item);
                            break;

                        case RingHand.Right:
                            if (Equipment.RightRing != new Item())
                                AddItem(Equipment.RightRing);
                            Equipment.RightRing = new Item(item);
                            break;
                    }
                    break;

                default:
                    //GameState.DisplayNotification("You have attempted to equip an Item which doesn't fit a current type of item to be equipped.", "Sulimn");
                    break;
            }

            RemoveItem(item);
        }

        /// <summary>Unequips an <see cref="Item"/> from a <see cref="Hero"/>'s Equipment.</summary>
        /// <param name="item"><see cref="Item"/> to be unequipped</param>
        /// <param name="side">If <see cref="Item"/> is a Ring, which side is it?</param>
        internal void Unequip(Item item, RingHand side = RingHand.Left)
        {
            switch (item.Type)
            {
                case ItemType.MeleeWeapon:
                case ItemType.RangedWeapon:
                    if (item != GameState.DefaultWeapon)
                        AddItem(item);
                    break;

                case ItemType.HeadArmor:
                    if (item != GameState.DefaultHead)
                        AddItem(item);
                    break;

                case ItemType.BodyArmor:
                    if (item != GameState.DefaultBody)
                        AddItem(item);
                    break;

                case ItemType.HandArmor:
                    if (item != GameState.DefaultHands)
                        AddItem(item);
                    break;

                case ItemType.LegArmor:
                    if (item != GameState.DefaultLegs)
                        AddItem(item);
                    break;

                case ItemType.FeetArmor:
                    if (item != GameState.DefaultFeet)
                        AddItem(item);
                    break;

                case ItemType.Ring:
                    if (item != new Item())
                        AddItem(item);
                    switch (side)
                    {
                        case RingHand.Left:
                            Equipment.LeftRing = new Item();
                            break;

                        case RingHand.Right:
                            Equipment.RightRing = new Item();
                            break;
                    }
                    break;
            }
        }

        /// <summary>Gets all <see cref="Item"/>s of specified Type.</summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns><see cref="Item"/>s of specified Type</returns>
        internal List<T> GetItemsOfType<T>() => Inventory.OfType<T>().ToList();

        /// <summary>Gets all <see cref="Item"/>s of specified Type.</summary>
        /// <param name="type">Type</param>
        /// <returns><see cref="Item"/>s of specified Type</returns>
        internal List<Item> GetItemsOfType(ItemType type) => Inventory.Where(itm => itm.Type == type).ToList();

        #endregion Inventory Management

        #region Override Operators

        public static bool Equals(Hero left, Hero right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase)
                && left.Level == right.Level
                && left.Experience == right.Experience
                && left.SkillPoints == right.SkillPoints
                && left.Hardcore == right.Hardcore
                && left.Spellbook == right.Spellbook
                && left.Class == right.Class
                && left.Attributes == right.Attributes
                && left.Equipment == right.Equipment
                && left.Gold == right.Gold
                && left.Statistics == right.Statistics
                && left.Progression == right.Progression
                && !left.Inventory.Except(right.Inventory).Any();
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
        /// <param name="gold">Gold of Hero</param>
        /// <param name="attributes">Attributes of Hero</param>
        /// <param name="statistics">Statistics of Hero</param>
        /// <param name="equipment">Equipment of Hero</param>
        /// <param name="spellbook">Spellbook of Hero</param>
        /// <param name="inventory">Inventory of Hero</param>
        /// <param name="bank">Bank of the Hero</param>
        /// <param name="progression">The progress the Hero has made</param>
        /// <param name="hardcore">Will the character be deleted on death?</param>
        internal Hero(string name, string password, HeroClass heroClass, int level, int experience, int skillPoints, int gold,
        Attributes attributes, Statistics statistics, Equipment equipment, Spellbook spellbook, List<Item> inventory, Bank bank, Progression progression, bool hardcore, List<Quest> quests)
        {
            Name = name;
            Password = password;
            Class = heroClass;
            Level = level;
            Experience = experience;
            Gold = gold;
            SkillPoints = skillPoints;
            Attributes = attributes;
            Statistics = statistics;
            Equipment = equipment;
            Spellbook = spellbook;
            Inventory = inventory;
            Bank = bank;
            Progression = progression;
            Hardcore = hardcore;
            Quests = quests;
        }

        /// <summary>Replaces this instance of Hero with another instance.</summary>
        /// <param name="other">Instance of Hero to replace this one</param>
        internal Hero(Hero other) : this(other.Name, other.Password, other.Class, other.Level, other.Experience, other.SkillPoints, other.Gold, new Attributes(other.Attributes), new Statistics(other.Statistics), new Equipment(other.Equipment), new Spellbook(other.Spellbook), new List<Item>(other.Inventory), other.Bank, other.Progression, other.Hardcore, other.Quests)
        {
        }

        #endregion Constructors
    }
}