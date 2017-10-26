using System;

namespace Sulimn.Classes.Items
{
    /// <summary>Represents a piece of Armor.</summary>
    internal class Armor : Item, IEquatable<Armor>
    {
        private int _defense;

        #region Modifying Properties

        /// <summary>How much damage the armor can defend against</summary>
        public int Defense
        {
            get => _defense;
            set
            {
                _defense = value;
                OnPropertyChanged("DefenseToString");
                OnPropertyChanged("DefenseToStringWithText");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Returns the defense with a comma separating thousands.</summary>
        public string DefenseToString => Defense.ToString("N0");

        /// <summary>Returns the defense with a comma separating thousands and preceding text.</summary>
        public string DefenseToStringWithText => Defense > 0 ? $"Defense: {DefenseToString}" : "";

        #endregion Helper Properties

        #region Override Operators

        private static bool Equals(Armor left, Armor right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) && left.Defense == right.Defense && left.Weight == right.Weight && left.Value == right.Value && left.CanSell == right.CanSell && left.IsSold == right.IsSold;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Armor);

        public bool Equals(Armor other) => Equals(this, other);

        public static bool operator ==(Armor left, Armor right) => Equals(left, right);

        public static bool operator !=(Armor left, Armor right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        public override string ToString() => Name;

        #endregion Override Operators
    }
}