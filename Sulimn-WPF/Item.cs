using System.ComponentModel;

namespace Sulimn_WPF
{
    internal abstract class Item : INotifyPropertyChanged
    {
        protected string _name, _type, _description;
        protected int _weight, _value;
        protected bool _canSell, _isSold;

        #region Data-Binding

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Properties

        abstract public string Name { get; set; }

        abstract public string Type { get; set; }

        abstract public string Description { get; set; }

        abstract public int Weight { get; set; }

        abstract public int Value { get; set; }

        abstract public bool CanSell { get; set; }

        abstract public bool IsSold { get; set; }

        #endregion Properties
    }
}