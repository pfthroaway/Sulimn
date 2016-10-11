using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulimn_WPF
{
    abstract class Item : INotifyPropertyChanged
    {
        protected string _name, _type, _description;
        protected int _weight, _value;
        protected bool _canSell;

        #region Data-Binding
        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        #region Properties
        abstract public string Name { get; set; }

        abstract public string Type { get; set; }

        abstract public string Description { get; set; }

        abstract public int Weight { get; set; }

        abstract public int Value { get; set; }

        abstract public bool CanSell { get; set; }
        #endregion
        
        #region Constructors
        internal Item() { }

        internal Item(string iName, string iType, string iDescription, int iWeight, int iValue, bool iCanSell)
        {
            _name = iName;
            _type = iType;
            _description = iDescription;
            _weight = iWeight;
            _value = iValue;
            _canSell = iCanSell;
        }

        internal Item(Weapon wpn)
        {
            _name = wpn.Name;
            _type = "Weapon";
            _description =wpn.Description;
            _weight =wpn.Weight;
            _value =wpn.Value;
            _canSell = wpn.CanSell;
        }

        internal Item(Armor armr)
        {
            _name = armr.Name;
            _type = "Armor";
            _description = armr.Description;
            _weight = armr.Weight;
            _value = armr.Value;
            _canSell = armr.CanSell;
        }
        
        internal Item(Potion potn)
        {
            _name = potn.Name;
            _type = "Potion";
            _description = potn.Description;
            _weight = potn.Weight;
            _value = potn.Value;
            _canSell = potn.CanSell;
        }
        #endregion
    }
}