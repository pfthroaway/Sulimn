using System;
using System.ComponentModel;

namespace Sulimn
{
    internal class Item : IItem, INotifyPropertyChanged
    {
        private bool _canSell, _isSold;
        private string _name, _description;
        private ItemTypes _type;
        private int _weight, _value;

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Modifying Properties

        /// <summary>Name of the armor</summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>Type of the armor</summary>
        public ItemTypes Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }

        /// <summary>Description of the armor</summary>
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        /// <summary>How much the armor weighs</summary>
        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged("Weight");
            }
        }

        /// <summary>How much the armor is worth</summary>
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        /// <summary>Can the armor be sold to a shop?</summary>
        public bool CanSell
        {
            get => _canSell;
            set
            {
                _canSell = value;
                OnPropertyChanged("CanSell");
            }
        }

        /// <summary>Can the armor be sold in a shop?</summary>
        public bool IsSold
        {
            get => _isSold;
            set
            {
                _isSold = value;
                OnPropertyChanged("IsSold");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>The value of the armor with thousands separators</summary>
        public string ValueToString => Value.ToString("N0");

        /// <summary>The value of the armor with thousands separators and preceding text</summary>
        public string ValueToStringWithText
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Name))
                    return $"Value: {ValueToString}";
                return "";
            }
        }

        /// <summary>The value of the armor</summary>
        public int SellValue => Value / 2;

        /// <summary>The value of the armor with thousands separators</summary>
        public string SellValueToString => SellValue.ToString("N0");

        /// <summary>The value of the armor with thousands separators with preceding text</summary>
        public string SellValueToStringWithText => !string.IsNullOrWhiteSpace(Name) ? $"Sell Value: {SellValueToString}" : "";

        /// <summary>Returns text relating to the sellability of the armor</summary>
        public string CanSellToString => !string.IsNullOrWhiteSpace(Name) ? (CanSell ? "Sellable" : "Not Sellable") : "";

        #endregion Helper Properties

        #region Override Operators

        private static bool Equals(Item left, Item right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Type == right.Type &&
             string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) &&
              left.Weight == right.Weight && left.Value == right.Value &&
             left.CanSell == right.CanSell && left.IsSold == right.IsSold;
        }

        public override bool Equals(object obj)
        {
            return Equals(this, obj as Item);
        }

        public bool Equals(Item otherItem)
        {
            return Equals(this, otherItem);
        }

        public static bool operator ==(Item left, Item right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Item left, Item right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ 17;
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion Override Operators
    }
}