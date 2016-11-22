using System.ComponentModel;

namespace Sulimn
{
    /// <summary>
    /// Represents an Item.
    /// </summary>
    internal abstract class Item : INotifyPropertyChanged
    {
        protected string _name, _description;
        protected ItemTypes _type;
        protected int _weight, _value;
        protected bool _canSell, _isSold;

        #region Data-Binding

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Modifying Properties

        public abstract string Name { get; set; }
        public abstract ItemTypes Type { get; set; }
        public abstract string Description { get; set; }
        public abstract int Weight { get; set; }
        public abstract int Value { get; set; }
        public abstract bool CanSell { get; set; }
        public abstract bool IsSold { get; set; }

        #endregion Modifying Properties

        #region Helper Properties

        public abstract string ValueToString { get; }
        public abstract string ValueToStringWithText { get; }
        public abstract int SellValue { get; }
        public abstract string SellValueToString { get; }
        public abstract string SellValueToStringWithText { get; }
        public abstract string CanSellToString { get; }

        #endregion Helper Properties
    }
}