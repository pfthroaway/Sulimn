using Sulimn.Classes.Entities;
using Sulimn.Classes.HeroParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sulimn.Classes.Items
{
    internal class Item : INotifyPropertyChanged, IItem
    {
        private string _name, _description;
        private bool _canSell, _isSold;
        private int _currentDurability, _maximumDurability, _minimumLevel, _value, _weight;
        private List<HeroClass> _allowedClasses = new List<HeroClass>();
        //TODO Implement durability and other new features, maybe weapon/armor smiths.

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Modifying Properties

        /// <summary>Name of the <see cref="Item"/></summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>Description of the <see cref="Item"/></summary>
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        /// <summary>How much the <see cref="Item"/> weighs.</summary>
        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged("Weight");
            }
        }

        /// <summary>How much the <see cref="Item"/> is worth</summary>
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        /// <summary>The current durability of an <see cref="Item"/>.</summary>
        public int CurrentDurability
        {
            get => _currentDurability; set
            {
                _currentDurability = value;
                OnPropertyChanged("CurrentDurability");
            }
        }

        /// <summary>The maximum durability of an <see cref="Item"/>.</summary>
        public int MaximumDurability
        {
            get => _maximumDurability;
            set
            {
                _maximumDurability = value;
                OnPropertyChanged("MaximumDurability");
            }
        }

        /// <summary>The minimum level a <see cref="Hero"/> is required to be to use the <see cref="Item"/>.</summary>
        public int MinimumLevel
        {
            get => _minimumLevel;
            set
            {
                _minimumLevel = value;
                OnPropertyChanged("MinimumLevel");
            }
        }

        /// <summary>Can the <see cref="Item"/> be sold to a shop?</summary>
        public bool CanSell
        {
            get => _canSell;
            set
            {
                _canSell = value;
                OnPropertyChanged("CanSell");
            }
        }

        /// <summary>Can the <see cref="Item"/> be sold in a shop?</summary>
        public bool IsSold
        {
            get => _isSold;
            set
            {
                _isSold = value;
                OnPropertyChanged("IsSold");
            }
        }

        /// <summary>Classes permitted to use this <see cref="Item"/>.</summary>
        public List<HeroClass> AllowedClasses
        {
            get => _allowedClasses;
            set
            {
                _allowedClasses = value;
                OnPropertyChanged("AllowedClasses");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>The current durability of an <see cref="Item"/>, with thousands separators.</summary>
        public string CurrentDurabilityToString => CurrentDurability.ToString("N0");

        /// <summary>The maximum durability of an <see cref="Item"/>, with thousands separators.</summary>
        public string MaximumDurabilityToString => MaximumDurability.ToString("N0");

        /// <summary>The durability of an <see cref="Item"/>, formatted.</summary>
        public string Durability => $"{CurrentDurabilityToString} / {MaximumDurabilityToString}";

        /// <summary>The value of the <see cref="Item"/> with thousands separators.</summary>
        public string ValueToString => Value.ToString("N0");

        /// <summary>The value of the <see cref="Item"/> with thousands separators and preceding text.</summary>
        public string ValueToStringWithText => !string.IsNullOrWhiteSpace(Name) ? $"Value: {ValueToString}" : "";

        /// <summary>The value of the Item.</summary>
        public int SellValue => Value / 2;

        /// <summary>The sell value of the <see cref="Item"/> with thousands separators.</summary>
        public string SellValueToString => SellValue.ToString("N0");

        /// <summary>The sell value of the <see cref="Item"/> with thousands separators with preceding text.</summary>
        public string SellValueToStringWithText => !string.IsNullOrWhiteSpace(Name) ? $"Sell Value: {SellValueToString}" : "";

        /// <summary>Returns text relating to the sellability of the <see cref="Item"/>.</summary>
        public string CanSellToString => !string.IsNullOrWhiteSpace(Name) ? (CanSell ? "Sellable" : "Not Sellable") : "";

        public string AllowedClassesToString => String.Join(",", AllowedClasses);

        #endregion Helper Properties

        #region Override Operators

        private static bool Equals(Item left, Item right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) && left.Weight == right.Weight && left.Value == right.Value && left.CanSell == right.CanSell && left.IsSold == right.IsSold;
        }

        public override bool Equals(object obj) => Equals(this, obj as Item);

        public bool Equals(Item otherItem) => Equals(this, otherItem);

        public static bool operator ==(Item left, Item right) => Equals(left, right);

        public static bool operator !=(Item left, Item right) => !Equals(left, right);

        public override int GetHashCode() => base.GetHashCode() ^ 17;

        public override string ToString() => Name;

        #endregion Override Operators
    }
}