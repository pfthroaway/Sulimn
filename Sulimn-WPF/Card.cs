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

        /// <summary>
        /// Initializes a default instance of Card.
        /// </summary>
        internal Card()
        {
        }

        /// <summary>
        /// Initializes an instance of Card by setting Properties.
        /// </summary>
        /// <param name="name">Name of Card</param>
        /// <param name="suit">Suit of Card</param>
        /// <param name="value">Value of Card</param>
        internal Card(string name, string suit, int value)
        {
            Name = name;
            Suit = suit;
            Value = value;
        }

        /// <summary>
        /// Replaces this instance of Card with another instance.
        /// </summary>
        /// <param name="otherCard">Instance to replace this instance</param>
        internal Card(Card otherCard)
        {
            Name = otherCard.Name;
            Suit = otherCard.Suit;
            Value = otherCard.Value;
        }

        #endregion Constructors
    }
}