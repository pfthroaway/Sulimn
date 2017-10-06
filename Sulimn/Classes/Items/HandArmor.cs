﻿using Sulimn.Classes.Enums;

namespace Sulimn.Classes.Items
{
    /// <summary>Represents a piece of Armor worn on the hands.</summary>
    internal class HandArmor : Armor
    {
        #region Constructors

        /// <summary>Initializes a default instance of HandArmor.</summary>
        internal HandArmor()
        {
        }

        /// <summary>Initializes an instance of HandArmor by assigning Properties.</summary>
        /// <param name="name">Name of HandArmor</param>
        /// <param name="description">Description of HandArmor</param>
        /// <param name="defense">Defense of HandArmor</param>
        /// <param name="weight">Weight of HandArmor</param>
        /// <param name="value">Value of HandArmor</param>
        /// <param name="canSell">Can Sell HandArmor?</param>
        /// <param name="isSold">Is HandArmor Sold?</param>
        internal HandArmor(string name, string description, int defense, int weight, int value,
        bool canSell, bool isSold)
        {
            Name = name;
            Description = description;
            Defense = defense;
            Weight = weight;
            Value = value;
            CanSell = canSell;
            IsSold = isSold;
        }

        /// <summary>Replaces this instance of HandArmor with another instance.</summary>
        /// <param name="other">Instance of HandArmor to replace this one</param>
        internal HandArmor(HandArmor other) : this(other.Name, other.Description, other.Defense, other.Weight, other.Value,
            other.CanSell, other.IsSold)
        {
        }

        #endregion Constructors
    }
}