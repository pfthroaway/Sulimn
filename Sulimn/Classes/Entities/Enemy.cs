using Newtonsoft.Json;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using System;
using System.Collections.Generic;

namespace Sulimn.Classes.Entities
{
    /// <summary>Represents an Enemy who opposes the Hero.</summary>
    internal class Enemy : Character
    {
        #region Modifying Properties

        /// <summary>Type of the <see cref="Enemy"/>.</summary>
        [JsonProperty(Order = -4)]
        public string Type { get; set; }

        #endregion Modifying Properties

        #region Health Manipulation

        /// <summary>Heals the <see cref="Enemy"/> for a specified amount.</summary>
        /// <param name="healAmount">Amount to be healed</param>
        /// <returns>Text saying the <see cref="Enemy"/> was healed</returns>
        internal string Heal(int healAmount)
        {
            Statistics.CurrentHealth += healAmount;
            if (Statistics.CurrentHealth > Statistics.MaximumHealth)
            {
                Statistics.CurrentHealth = Statistics.MaximumHealth;
                return "The {Name} to its maximum health.";
            }
            return $"The {Name} heals for {healAmount:N0} health.";
        }

        /// <summary>The <see cref="Enemy"/> takes Damage.</summary>
        /// <param name="damage">Amount damaged</param>
        /// <returns>Text saying the <see cref="Enemy"/> took damage</returns>
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

        #endregion Health Manipulation

        #region Override Operators

        public static bool Equals(Enemy left, Enemy right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Level == right.Level && left.Experience == right.Experience && left.Gold == right.Gold && left.Attributes == right.Attributes && left.Equipment == right.Equipment && left.Gold == right.Gold && left.Statistics == right.Statistics && left.Type == right.Type;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Enemy);

        public bool Equals(Enemy otherEnemy) => Equals(this, otherEnemy);

        public static bool operator ==(Enemy left, Enemy right) => Equals(left, right);

        public static bool operator !=(Enemy left, Enemy right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        /// <summary>Overrides the ToString() method to return only Name.</summary>
        public sealed override string ToString() => Name;

        #endregion Override Operators

        #region Constructors

        /// <summary>Initializes a default instance of <see cref="Enemy"/>.</summary>
        internal Enemy()
        {
        }

        /// <summary>Initializes an instance of <see cref="Enemy"/> by assigning Properties.</summary>
        /// <param name="name">Name of <see cref="Enemy"/></param>
        /// <param name="type">Type of <see cref="Enemy"/></param>
        /// <param name="level">Level of <see cref="Enemy"/></param>
        /// <param name="experience">Experience <see cref="Enemy"/> can provide</param>
        /// <param name="gold">Gold carried by <see cref="Enemy"/></param>
        /// <param name="attributes">Attributes of <see cref="Enemy"/></param>
        /// <param name="statistics">Statistics of <see cref="Enemy"/></param>
        /// <param name="equipment">Equipment of <see cref="Enemy"/></param>
        /// <param name="inventory">Inventory of <see cref="Enemy"/></param>
        internal Enemy(string name, string type, int level, int experience, int gold, Attributes attributes, Statistics statistics, Equipment equipment, List<Item> inventory)
        {
            Name = name;
            Type = type;
            Level = level;
            Experience = experience;
            Gold = gold;
            Attributes = new Attributes(attributes);
            Statistics = new Statistics(statistics);
            Equipment = new Equipment(equipment);
            Inventory = inventory;
        }

        /// <summary>Replaces this instance of <see cref="Enemy"/> with another instance.</summary>
        /// <param name="other">Instance of <see cref="Enemy"/> that replaces this one</param>
        internal Enemy(Enemy other) : this(other.Name, other.Type, other.Level, other.Experience, other.Gold, new Attributes(other.Attributes), new Statistics(other.Statistics), new Equipment(other.Equipment), new List<Item>(other.Inventory))
        {
        }

        #endregion Constructors
    }
}