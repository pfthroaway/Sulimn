using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulimn_WPF
{
    internal class Potion : Item, IEquatable<Potion>
    {
        private string _potionType;
        private int _amount;

        #region Properties

        public sealed override string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public sealed override string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string PotionType { get { return _potionType; } set { _potionType = value; } }

        public sealed override string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public sealed override int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public sealed override int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public sealed override bool CanSell
        {
            get { return _canSell; }
            set { _canSell = value; }
        }

        #endregion Properties

        #region Override Operators

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Potion potn = obj as Potion;
            if ((object)potn == null)
                return false;

            return (this.Name == potn.Name) && (this.Type == potn.Type) && (this.Description == potn.Description) && (this.Amount == potn.Amount) && (this.Weight == potn.Weight) && (this.Value == potn.Value) && (this.CanSell == potn.CanSell);
        }

        public bool Equals(Potion otherPotion)
        {
            if ((object)otherPotion == null)
                return false;

            return (this.Name == otherPotion.Name) && (this.Type == otherPotion.Type) && (this.Description == otherPotion.Description) && (this.Amount == otherPotion.Amount) && (this.Weight == otherPotion.Weight) && (this.Value == otherPotion.Value) && (this.CanSell == otherPotion.CanSell);
        }

        public static bool operator ==(Potion left, Potion right)
        {
            if (System.Object.ReferenceEquals(left, right))
                return true;

            if (((object)left == null) || ((object)right == null))
                return false;

            return (left.Name == right.Name) && (left.Type == right.Type) && (left.Description == right.Description) && (left.Amount == right.Amount) && (left.Weight == right.Weight) && (left.Value == right.Value) && (left.CanSell == right.CanSell);
        }

        public static bool operator !=(Potion left, Potion right)
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

        internal Potion()
        {
        }

        internal Potion(string potionName, string potionType, string potionDescription, int potionAmount, int potionValue, bool potionCanSell)
        {
            Name = potionName;
            Type = "Potion";
            PotionType = potionType;
            Description = potionDescription;
            Weight = 0;
            Value = potionValue;
            Amount = potionAmount;
            CanSell = potionCanSell;
        }

        #endregion Constructors
    }
}