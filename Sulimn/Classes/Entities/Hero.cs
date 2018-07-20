﻿using Sulimn.Classes.Enums;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sulimn.Classes.Entities
{
    /// <summary>Represents a Hero from Sulimn.</summary>
    internal class Hero : Character, IEnumerable<Item>
    {
        private HeroClass _class;
        private int _skillPoints;
        private Spellbook _spellbook = new Spellbook();
        private List<Item> _inventory;
        private bool _hardcore;

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

        /// <summary>List of Items in the inventory.</summary>
        public ReadOnlyCollection<Item> Inventory => new ReadOnlyCollection<Item>(_inventory);

        /// <summary>Combined weight of all Items in a Hero's Inventory.</summary>
        public int CarryingWeight => Inventory.Sum(itm => itm.Weight);

        /// <summary>Combined weight of all Items in a Hero's Inventory and all the Equipment currently equipped.</summary>
        public int TotalWeight => CarryingWeight + Equipment.TotalWeight;

        /// <summary>Maximum weight a Hero can carry.</summary>
        public int MaximumWeight => TotalStrength * 10;

        /// <summary>Is the Hero carrying more than they should be able to?</summary>
        public bool Overweight => TotalWeight > MaximumWeight;

        /// <summary>List of Items in the inventory, formatted.</summary>
        public string InventoryToString => string.Join(",", Inventory);

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

        #region Inventory Management

        /// <summary>Adds an Item to the inventory.</summary>
        /// <param name="item">Item to be removed</param>
        internal void AddItem(Item item)
        {
            _inventory.Add(item);
            _inventory = Inventory.OrderBy(itm => itm.Name).ToList();
            OnPropertyChanged("Inventory");
        }

        /// <summary>Removes an Item from the inventory.</summary>
        /// <param name="item">Item to be removed</param>
        internal void RemoveItem(Item item)
        {
            _inventory.Remove(item);
            OnPropertyChanged("Inventory");
        }

        /// <summary>Equips an Item into a Hero's Equipment.</summary>
        /// <param name="item">Item to be equipped</param>
        /// <param name="side">If Item is a Ring, which side is it?</param>
        internal void Equip(Item item, RingHand side = RingHand.Left)
        {
            switch (item)
            {
                case Weapon weapon:
                    if (Equipment.Weapon != GameState.DefaultWeapon)
                        AddItem(Equipment.Weapon);
                    Equipment.Weapon = new Weapon(weapon);
                    break;

                case HeadArmor headArmor:
                    if (Equipment.Head != GameState.DefaultHead)
                        AddItem(Equipment.Head);
                    Equipment.Head = new HeadArmor(headArmor);
                    break;

                case BodyArmor bodyArmor:
                    if (Equipment.Body != GameState.DefaultBody)
                        AddItem(Equipment.Body);
                    Equipment.Body = new BodyArmor(bodyArmor);
                    break;

                case HandArmor handArmor:
                    if (Equipment.Hands != GameState.DefaultHands)
                        AddItem(Equipment.Hands);
                    Equipment.Hands = new HandArmor(handArmor);
                    break;

                case LegArmor legArmor:
                    if (Equipment.Legs != GameState.DefaultLegs)
                        AddItem(Equipment.Legs);
                    Equipment.Legs = new LegArmor(legArmor);
                    break;

                case FeetArmor feetArmor:
                    if (Equipment.Feet != GameState.DefaultFeet)
                        AddItem(Equipment.Feet);
                    Equipment.Feet = new FeetArmor(feetArmor);
                    break;

                case Ring ring:
                    switch (side)
                    {
                        case RingHand.Left:
                            if (Equipment.LeftRing != new Ring())
                                AddItem(Equipment.LeftRing);
                            Equipment.LeftRing = new Ring(ring);
                            break;

                        case RingHand.Right:
                            if (Equipment.RightRing != new Ring())
                                AddItem(Equipment.RightRing);
                            Equipment.RightRing = new Ring(ring);
                            break;
                    }
                    break;

                default:
                    GameState.DisplayNotification("You have attempted to equip an Item which doesn't fit a current type of item to be equipped.", "Sulimn");
                    break;
            }

            RemoveItem(item);
        }

        /// <summary>Unequips an Item from a Hero's Equipment.</summary>
        /// <param name="item">Item to be unequipped</param>
        /// <param name="side">If Item is a Ring, which side is it?</param>
        internal void Unequip(Item item, RingHand side = RingHand.Left)
        {
            switch (item)
            {
                case Weapon weapon:
                    if (weapon != GameState.DefaultWeapon)
                        AddItem(weapon);
                    Equipment.Weapon = new Weapon(GameState.DefaultWeapon);
                    break;

                case HeadArmor headArmor:
                    if (headArmor != GameState.DefaultHead)
                        AddItem(headArmor);
                    Equipment.Head = new HeadArmor(GameState.DefaultHead);
                    break;

                case BodyArmor bodyArmor:
                    if (bodyArmor != GameState.DefaultBody)
                        AddItem(bodyArmor);
                    Equipment.Body = new BodyArmor(GameState.DefaultBody);
                    break;

                case HandArmor handArmor:
                    if (handArmor != GameState.DefaultHands)
                        AddItem(handArmor);
                    Equipment.Hands = new HandArmor(GameState.DefaultHands);
                    break;

                case LegArmor legArmor:
                    if (legArmor != GameState.DefaultLegs)
                        AddItem(legArmor);
                    Equipment.Legs = new LegArmor(GameState.DefaultLegs);
                    break;

                case FeetArmor feetArmor:
                    if (feetArmor != GameState.DefaultFeet)
                        AddItem(feetArmor);
                    Equipment.Feet = new FeetArmor(GameState.DefaultFeet);
                    break;

                case Ring ring:
                    if (ring != new Ring())
                        AddItem(ring);
                    switch (side)
                    {
                        case RingHand.Left:
                            Equipment.LeftRing = new Ring();
                            break;

                        case RingHand.Right:
                            Equipment.RightRing = new Ring();
                            break;
                    }
                    break;
            }
        }

        /// <summary>Gets all Items of specified Type.</summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Items of specified Type</returns>
        internal List<T> GetItemsOfType<T>() => Inventory.OfType<T>().ToList();

        #endregion Inventory Management

        #region Enumerator

        public IEnumerator<Item> GetEnumerator() => Inventory.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion Enumerator

        #region Override Operators

        private static bool Equals(Hero left, Hero right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Level == right.Level && left.Experience == right.Experience && left.SkillPoints == right.SkillPoints && left.Hardcore == right.Hardcore && left.Spellbook == right.Spellbook && left.Class == right.Class && left.Attributes == right.Attributes && left.Equipment == right.Equipment && left.Gold == right.Gold && left.Statistics == right.Statistics && left.Progression == right.Progression && !left.Inventory.Except(right.Inventory).Any();
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
        /// <param name="progression">The progress the Hero has made</param>
        /// <param name="hardcore">Will the character be deleted on death?</param>
        internal Hero(string name, string password, HeroClass heroClass, int level, int experience, int skillPoints, int gold,
        Attributes attributes, Statistics statistics, Equipment equipment, Spellbook spellbook, IEnumerable<Item> inventory, Progression progression, bool hardcore)
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
            List<Item> items = new List<Item>();
            items.AddRange(inventory);
            _inventory = items;
            Progression = progression;
            Hardcore = hardcore;
        }

        /// <summary>Replaces this instance of Hero with another instance.</summary>
        /// <param name="other">Instance of Hero to replace this one</param>
        internal Hero(Hero other) : this(other.Name, other.Password, other.Class, other.Level, other.Experience, other.SkillPoints, other.Gold, new Attributes(other.Attributes), new Statistics(other.Statistics), new Equipment(other.Equipment), new Spellbook(other.Spellbook), other.Inventory, other.Progression, other.Hardcore)
        {
        }

        #endregion Constructors
    }
}