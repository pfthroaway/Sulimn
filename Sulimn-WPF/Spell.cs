using System;
using System.ComponentModel;

namespace Sulimn_WPF
{
    internal class Spell : IEquatable<Spell>, INotifyPropertyChanged
    {
        private string _name, _description, _requiredClass;
        private SpellTypes _type;
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

        public SpellTypes Type
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

        public static bool Equals(Spell left, Spell right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Type == right.Type && string.Equals(left.Description, right.Description, StringComparison.OrdinalIgnoreCase) && string.Equals(left.RequiredClass, right.RequiredClass, StringComparison.OrdinalIgnoreCase) && (left.RequiredLevel == right.RequiredLevel) && (left.MagicCost == right.MagicCost) && (left.Amount == right.Amount);
        }

        public override bool Equals(object obj)
        {
            return Equals(this, obj as Spell);
        }

        public bool Equals(Spell otherSpell)
        {
            return Equals(this, otherSpell);
        }

        public static bool operator ==(Spell left, Spell right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Spell left, Spell right)
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

        #region Constructors

        /// <summary>
        /// Initializes a default instance of Spell.
        /// </summary>
        internal Spell()
        {
        }

        /// <summary>
        /// Initializes an instance of Spell by assigning Properties.
        /// </summary>
        /// <param name="spellName">Name of Spell</param>
        /// <param name="spellType">Type of Spell</param>
        /// <param name="spellDescription">Description of Spell</param>
        /// <param name="spellRequiredClass">Required HeroClass of Spell</param>
        /// <param name="spellRequiredLevel">Required Level to learn Spell</param>
        /// <param name="spellMagicCost">Magic cost of Spell</param>
        /// <param name="spellAmount">Amount of Spell</param>
        internal Spell(string spellName, SpellTypes spellType, string spellDescription, string spellRequiredClass, int spellRequiredLevel, int spellMagicCost, int spellAmount)
        {
            Name = spellName;
            Type = spellType;
            Description = spellDescription;
            RequiredClass = spellRequiredClass;
            RequiredLevel = spellRequiredLevel;
            MagicCost = spellMagicCost;
            Amount = spellAmount;
        }

        #endregion Constructors
    }
}