using System;

namespace Sulimn.Classes.Items
{
    internal abstract class Consumable : Item, IEquatable<Consumable>
    {
        private int _restoreHealth, _restoreMagic;
        private bool _cures;

        #region Modifying Properties

        /// <summary>Amount of health this <see cref="Consumable"/> restores.</summary>
        public int RestoreHealth
        {
            get => _restoreHealth;
            set
            {
                _restoreHealth = value;
                OnPropertyChanged("RestoreHealth");
            }
        }

        /// <summary>Amount of health this <see cref="Consumable"/> restores.</summary>
        public int RestoreMagic
        {
            get => _restoreMagic;
            set
            {
                _restoreMagic = value;
                OnPropertyChanged("RestoreMagic");
            }
        }

        /// <summary>Does this <see cref="Consumable"/> cure?</summary>
        public bool Cures
        {
            get => _cures;
            set
            {
                _cures = value;
                OnPropertyChanged("Cures");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Returns text relating to the amount of Health restored by the <see cref="Consumable"/>.</summary>
        public string RestoreHealthToString => RestoreHealth > 0 ? $"Restores {RestoreHealth:N0} Health." : "";

        /// <summary>Returns text relating to the amount of Magic restored by the <see cref="Consumable"/>.</summary>
        public string RestoreMagicToString => RestoreMagic > 0 ? $"Restores {RestoreMagic:N0} Magic." : "";

        /// <summary>Returns text regarding if the <see cref="Consumable"/> can heal the user.</summary>
        public string CuresToString => Cures ? $"Cures ailments." : "";

        #endregion Helper Properties

        #region Override Operators

        private static bool Equals(Consumable left, Consumable right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) && left.RestoreHealth == right.RestoreHealth && left.RestoreMagic == right.RestoreMagic && left.Cures == right.Cures && left.Weight == right.Weight && left.Value == right.Value && left.CanSell == right.CanSell && left.IsSold == right.IsSold;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Consumable);

        public bool Equals(Consumable other) => Equals(this, other);

        public static bool operator ==(Consumable left, Consumable right) => Equals(left, right);

        public static bool operator !=(Consumable left, Consumable right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        public sealed override string ToString() => Name;

        #endregion Override Operators
    }
}