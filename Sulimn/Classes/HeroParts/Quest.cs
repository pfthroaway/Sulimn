using Sulimn.Classes.Enums;
using Sulimn.Classes.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sulimn.Classes.HeroParts
{
    /// <summary>Represents a <see cref="Quest"/> that is being undertaken.</summary>
    public class Quest
    {
        #region Modifying Properties

        /// <summary>Name of the <see cref="Quest"/>.</summary>
        public string Name { get; set; }

        /// <summary>Description of the <see cref="Quest"/>.</summary>
        public string Description { get; set; }

        /// <summary><see cref="QuestType"/> of the <see cref="Quest"/>.</summary>
        public QuestType QuestType { get; set; }

        /// <summary><see cref="QuestItems"/> required by the <see cref="Quest"/>.</summary>
        public List<QuestItem> QuestItems { get; set; } = new List<QuestItem>();

        /// <summary>Amount of Gold to be awarded upon completion.</summary>
        public int RewardGold { get; set; }

        /// <summary><see cref="Item"/>s to be awarded upon completion.</summary>
        public List<Item> RewardItems { get; set; }

        #endregion Modifying Properties

        #region Override Operators

        public static bool Equals(Quest left, Quest right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase)
                && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase)
                   && left.QuestType == right.QuestType
                   && left.RewardGold == right.RewardGold
                   && !left.QuestItems.Except(right.QuestItems).Any()
                   && !right.QuestItems.Except(left.QuestItems).Any()
                   && !left.RewardItems.Except(right.RewardItems).Any()
                   && !right.RewardItems.Except(left.RewardItems).Any();
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Quest);

        public bool Equals(Quest other) => Equals(this, other);

        public static bool operator ==(Quest left, Quest right) => Equals(left, right);

        public static bool operator !=(Quest left, Quest right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        public sealed override string ToString() => Name;

        #endregion Override Operators

        #region Helper Properties

        /// <summary>Are all the requirements of the <see cref="Quest"/> complete?</summary>
        public bool IsComplete => QuestItems.All(quest => quest.IsComplete);

        #endregion Helper Properties

        #region Constructors

        /// <summary>Initializes a default instance of <see cref="Quest"/>.</summary>
        public Quest()
        {
        }

        /// <summary>Initializes an instance of <see cref="Quest"/> by assigning Property values.</summary>
        /// <param name="name">Name of the <see cref="Quest"/></param>
        /// <param name="description">Description of the <see cref="Quest"/></param>
        /// <param name="type"><see cref="QuestType"/> of the <see cref="Quest"/></param>
        /// <param name="items"><see cref="QuestItem"/>s required by the <see cref="Quest"/></param>
        /// <param name="rewardGold">Amount of Gold to be awarded upon completion.</param>
        /// <param name="rewardItems"><see cref="Item"/>s to be awarded upon completion.</param>
        public Quest(string name, string description, QuestType type, List<QuestItem> items, int rewardGold, List<Item> rewardItems)
        {
            Name = name;
            Description = description;
            QuestType = type;
            QuestItems = items;
            RewardGold = rewardGold;
            RewardItems = rewardItems;
        }

        /// <summary>Replaces an instance of <see cref="Quest"/> with another instance.</summary>
        /// <param name="other">Instance of <see cref="Quest"/> to replace this instance</param>
        public Quest(Quest other) : this(other.Name, other.Description, other.QuestType, other.QuestItems, other.RewardGold, other.RewardItems)
        {
        }

        #endregion Constructors
    }
}