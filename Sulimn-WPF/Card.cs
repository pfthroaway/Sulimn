using System;
using System.ComponentModel;

namespace Sulimn_WPF
{
    internal class Card : INotifyPropertyChanged, IEquatable<Card>
    {
        private string _name;
        private string _suit;
        private int _value;

        #region Properties

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public string Suit
        {
            get { return _suit; }
            set { _suit = value; OnPropertyChanged("Suit"); }
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged("Value"); }
        }

        public string CardToString
        {
            get { return Name + " of " + Suit; }
        }

        #endregion Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Override Operators

        public sealed override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Card otherCard = obj as Card;
            if ((object)otherCard == null)
                return false;

            return (this.Name == otherCard.Name) && (this.Suit == otherCard.Suit) && (this.Value == otherCard.Value);
        }

        public bool Equals(Card otherCard)
        {
            if ((object)otherCard == null)
                return false;

            return (this.Name == otherCard.Name) && (this.Suit == otherCard.Suit) && (this.Value == otherCard.Value);
        }

        public static bool operator ==(Card left, Card right)
        {
            if (System.Object.ReferenceEquals(left, right))
                return true;

            if (((object)left == null) || ((object)right == null))
                return false;

            return (left.Name == right.Name) && (left.Suit == right.Suit) && (left.Value == right.Value);
        }

        public static bool operator !=(Card left, Card right)
        {
            return !(left == right);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode() ^ 17;
        }

        public sealed override string ToString()
        {
            return Name + " of " + Suit;
        }

        #endregion Override Operators

        #region Constructors

        internal Card()
        {
        }

        internal Card(string name, string suit, int value)
        {
            Name = name;
            Suit = suit;
            Value = value;
        }

        internal Card(Card otherCard)
        {
            Name = otherCard.Name;
            Suit = otherCard.Suit;
            Value = otherCard.Value;
        }

        #endregion Constructors
    }
}