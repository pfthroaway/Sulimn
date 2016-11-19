using System;
using System.ComponentModel;

namespace Sulimn
{
    /// <summary>
    /// Represents a playing card.
    /// </summary>
    internal class Card : INotifyPropertyChanged, IEquatable<Card>
    {
        private string _name;
        private CardSuit _suit;
        private int _value;

        #region Properties

        /// <summary>The name of the card.</summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        /// <summary>The suit of the card.</summary>
        public CardSuit Suit
        {
            get { return _suit; }
            set { _suit = value; OnPropertyChanged("CardSuit"); }
        }

        /// <summary>The value of the card.</summary>
        public int Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged("Value"); }
        }

        /// <summary>Returns the name and suit of the card.</summary>
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

        public static bool Equals(Card left, Card right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Suit == right.Suit && left.Value == right.Value;
        }

        public sealed override bool Equals(object obj)
        {
            return Equals(this, obj as Card);
        }

        public bool Equals(Card otherCard)
        {
            return Equals(this, otherCard);
        }

        public static bool operator ==(Card left, Card right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Card left, Card right)
        {
            return !Equals(left, right);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode() ^ 17;
        }

        public sealed override string ToString()
        {
            return CardToString;
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
        internal Card(string name, CardSuit suit, int value)
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