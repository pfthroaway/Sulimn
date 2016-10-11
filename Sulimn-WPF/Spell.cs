using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulimn_WPF
{
    internal class Spell : IEquatable<Spell>, INotifyPropertyChanged
    {
        private string _name, _type, _description, _requiredClass;
        private int _requiredLevel, _magicCost, _amount;

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Properties

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged("Type"); OnPropertyChanged("TypeAmount"); }
        }

        public string TypeAmount
        {
            get
            {
                if (Amount > 0)
                    return Type + ": " + Amount;
                return "";
            }
        }

        public string RequiredClass
        {
            get { return _requiredClass; }
            set { _requiredClass = value; OnPropertyChanged("RequiredClass"); }
        }

        public int RequiredLevel
        {
            get { return _requiredLevel; }
            set { _requiredLevel = value; OnPropertyChanged("RequiredLevel"); }
        }

        public int MagicCost
        {
            get { return _magicCost; }
            set { _magicCost = value; OnPropertyChanged("MagicCost"); }
        }

        public string MagicCostToString
        {
            get
            {
                if (MagicCost > 0)
                    return "Magic Cost: " + MagicCost.ToString("N0");
                return "";
            }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; OnPropertyChanged("Amount"); OnPropertyChanged("TypeAmount"); }
        }

        #endregion Properties

        #region Override Operators

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Spell spl = obj as Spell;
            if ((System.Object)spl == null)
                return false;

            return (this.Name == spl.Name) && (this.Type == spl.Type) && (this.Description == spl.Description) && (this.Description == spl.Description) && (this.RequiredClass == spl.RequiredClass) && (this.RequiredLevel == spl.RequiredLevel) && (this.MagicCost == spl.MagicCost) && (this.Amount == spl.Amount);
        }

        public bool Equals(Spell otherSpell)
        {
            if ((object)otherSpell == null)
                return false;

            return (this.Name == otherSpell.Name) && (this.Type == otherSpell.Type) && (this.Description == otherSpell.Description) && (this.Description == otherSpell.Description) && (this.RequiredClass == otherSpell.RequiredClass) && (this.RequiredLevel == otherSpell.RequiredLevel) && (this.MagicCost == otherSpell.MagicCost) && (this.Amount == otherSpell.Amount);
        }

        public static bool operator ==(Spell left, Spell right)
        {
            if (System.Object.ReferenceEquals(left, right))
                return true;

            if (((object)left == null) || ((object)right == null))
                return false;

            return (left.Name == right.Name) && (left.Type == right.Type) && (left.Description == right.Description) && (left.Description == right.Description) && (left.RequiredClass == right.RequiredClass) && (left.RequiredLevel == right.RequiredLevel) && (left.MagicCost == right.MagicCost) && (left.Amount == right.Amount);
        }

        public static bool operator !=(Spell left, Spell right)
        {
            return !(left == right);
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

        #region Constructors

        internal Spell()
        {
        }

        internal Spell(string spellName, string spellType, string spellDescription, string spellRequiredClass, int spellRequiredLevel, int spellMagicCost, int spellAmount)
        {
            _name = spellName;
            _type = spellType;
            _description = spellDescription;
            _requiredClass = spellRequiredClass;
            _requiredLevel = spellRequiredLevel;
            _magicCost = spellMagicCost;
            _amount = spellAmount;
        }

        #endregion Constructors
    }
}