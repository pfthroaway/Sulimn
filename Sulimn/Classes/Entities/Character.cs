using Newtonsoft.Json;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using System.Collections.Generic;
using System.Linq;

namespace Sulimn.Classes.Entities
{
    /// <summary>Represents living entities in Sulimn.</summary>
    internal abstract class Character : BaseINPC
    {
        private string name;
        private int level, experience, gold;
        private Attributes attributes = new Attributes();
        private Statistics statistics = new Statistics();
        private Equipment equipment = new Equipment();
        private Spellbook spellbook = new Spellbook();
        private Spell currentSpell = new Spell();
        private List<Item> inventory = new List<Item>();

        #region Modifying Properties

        /// <summary>Name of the <see cref="Character"/>.</summary>
        [JsonProperty(Order = -5)]
        public string Name
        {
            get => name;
            set { name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        /// <summary>Level of the <see cref="Character"/>.</summary>
        [JsonProperty(Order = 1)]
        public int Level
        {
            get => level;
            set { level = value; NotifyPropertyChanged(nameof(Level), nameof(LevelToString)); }
        }

        /// <summary>Name of the <see cref="Character"/>.</summary>
        [JsonProperty(Order = 2)]
        public int Experience
        {
            get => experience;
            set { experience = value; NotifyPropertyChanged(nameof(Experience), nameof(ExperienceToString), nameof(ExperienceToStringWithText)); }
        }

        /// <summary>Amount of Gold in the <see cref="Character"/>'s inventory.</summary>
        [JsonProperty(Order = 3)]
        public int Gold
        {
            get => gold;
            set { gold = value; NotifyPropertyChanged(nameof(Gold), nameof(GoldToString), nameof(GoldToStringWithText)); }
        }

        /// <summary>Attributes of the <see cref="Character"/>.</summary>
        [JsonProperty(Order = 4)]
        public Attributes Attributes
        {
            get => attributes;
            set { attributes = value; NotifyPropertyChanged(nameof(Attributes)); }
        }

        /// <summary>Statistics of the <see cref="Character"/>.</summary>
        [JsonProperty(Order = 5)]
        public Statistics Statistics
        {
            get => statistics;
            set { statistics = value; NotifyPropertyChanged(nameof(Statistics)); }
        }

        /// <summary>Equipment of the <see cref="Character"/>.</summary>
        [JsonProperty(Order = 6)]
        public Equipment Equipment
        {
            get => equipment;
            set { equipment = value; NotifyPropertyChanged(nameof(Equipment)); }
        }

        /// <summary>The list of <see cref="Spell"/>s the <see cref="Character"/> currently knows.</summary>
        [JsonProperty(Order = 8)]
        public Spellbook Spellbook
        {
            get => spellbook;
            set { spellbook = value; NotifyPropertyChanged(nameof(Spellbook)); }
        }

        /// <summary>Currently prepared <see cref="Spell"/>.</summary>
        [JsonIgnore]
        public Spell CurrentSpell
        {
            get => currentSpell;
            set { currentSpell = value; NotifyPropertyChanged(nameof(CurrentSpell), nameof(CurrentSpellString)); }
        }

        /// <summary>Currently prepared <see cref="Spell"/>, to string.</summary>
        [JsonProperty(Order = 10)]
        public string CurrentSpellString
        {
            get => CurrentSpell.Name;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    CurrentSpell = GameState.AllSpells.Find(spl => spl.Name == value.Trim());
            }
        }

        /// <summary>List of <see cref="Item"/>s in the <see cref="Character"/>'s inventory.</summary>
        [JsonProperty(Order = 11)]
        public List<Item> Inventory
        {
            get => inventory;
            set { inventory = value; NotifyPropertyChanged(nameof(Inventory), nameof(InventoryToString)); }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Returns the <see cref="Character"/>'s level with preceding text.</summary>
        [JsonIgnore]
        public string LevelToString => $"Level {Level}";

        /// <summary>The experience the <see cref="Character"/> has gained this level alongside how much is needed to level up.</summary>
        [JsonIgnore]
        public string ExperienceToString => $"{Experience:N0} / {Level * 100:N0}";

        /// <summary>The experience the <see cref="Character"/> has gained this level alongside how much is needed to level up with preceding text.</summary>
        [JsonIgnore]
        public string ExperienceToStringWithText => $"Experience: {ExperienceToString}";

        /// <summary>Amount of Gold in the <see cref="Character"/>'s inventory, with thousands separator.</summary>
        [JsonIgnore]
        public string GoldToString => Gold.ToString("N0");

        /// <summary>Amount of Gold in the <see cref="Character"/>'s inventory, with thousands separator and preceding text.</summary>
        [JsonIgnore]
        public string GoldToStringWithText => $"Gold: {GoldToString}";

        /// <summary>List of <see cref="Item"/>s in the <see cref="Character"/>'s inventory, formatted.</summary>
        [JsonIgnore]
        public string InventoryToString => string.Join(",", Inventory);

        /// <summary>Combined weight of all <see cref="Item"/>s in a <see cref="Character"/>'s inventory.</summary>
        [JsonIgnore]
        public int CarryingWeight => Inventory.Count > 0 ? Inventory.Sum(itm => itm.Weight) : 0;

        /// <summary>Combined weight of all <see cref="Item"/>s in a <see cref="Character"/>'s inventory and all the Equipment currently equipped.</summary>
        [JsonIgnore]
        public int TotalWeight => CarryingWeight + Equipment.TotalWeight;

        /// <summary>Combined weight of all <see cref="Items"/>s in a <see cref="Character"/>'s inventory and all the Equipment currently equipped, formatted.</summary>
        [JsonIgnore]
        public string TotalWeightToString => TotalWeight.ToString("N0");

        /// <summary>Maximum weight a <see cref="Character"/> can carry.</summary>
        [JsonIgnore]
        public int MaximumWeight => TotalStrength * 10;

        /// <summary>Maximum weight a <see cref="Character"/> can carry, formatted.</summary>
        [JsonIgnore]
        public string MaximumWeightToString => MaximumWeight.ToString("N0");

        /// <summary>Displays the <see cref="Character"/>'s currently held weight alongside the maximum weight.</summary>
        [JsonIgnore]
        public string WeightToString => $"Weight Carrying: {TotalWeightToString} / {MaximumWeightToString}";

        /// <summary>Is the <see cref="Character"/> carrying more than they should be able to?</summary>
        [JsonIgnore]
        public bool Overweight => TotalWeight > MaximumWeight;

        /// <summary>Displays text regarding the <see cref="Character"/> carrying more than they should be able to carry.</summary>
        [JsonIgnore]
        public string OverweightToString => Overweight ? "Overweight" : "Not Overweight";

        /// <summary>Ratio of Total Strength to Total Weight.</summary>
        [JsonIgnore]
        public decimal StrengthWeightRatio => TotalStrength * 10m / TotalWeight;

        /// <summary>Total Strength value including from Attributes and Equipment.</summary>
        [JsonIgnore]
        public int TotalStrength => Attributes.Strength + Equipment.BonusStrength;

        /// <summary>Total Vitality value including from Attributes and Equipment.</summary>
        [JsonIgnore]
        public int TotalVitality => Attributes.Vitality + Equipment.BonusVitality;

        /// <summary>Total Dexterity value including from Attributes and Equipment.</summary>
        [JsonIgnore]
        public int TotalDexterity => Attributes.Dexterity + Equipment.BonusDexterity;

        /// <summary>Total Wisdom value including from Attributes and Equipment.</summary>
        [JsonIgnore]
        public int TotalWisdom => Attributes.Wisdom + Equipment.BonusWisdom;

        #endregion Helper Properties

        #region Inventory Management

        /// <summary>Adds an <see cref="Item"/> to the inventory.</summary>
        /// <param name="item"><see cref="Item"/> to be removed</param>
        public void AddItem(Item item)
        {
            Inventory.Add(item);
            Inventory = Inventory.OrderBy(itm => itm.Name).ToList();
        }

        /// <summary>Removes an <see cref="Item"/> from the inventory.</summary>
        /// <param name="item"><see cref="Item"/> to be removed</param>
        public void RemoveItem(Item item) => Inventory.Remove(item);

        #endregion Inventory Management
    }
}