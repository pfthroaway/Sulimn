using System;
using System.ComponentModel;

namespace Sulimn
{
    /// <summary>Represents a playing card.</summary>
    internal class Card : INotifyPropertyChanged, IEquatable<Card>
    {
        private string _name;
        private CardSuit _suit;
        private int _value;
        private bool _hidden;

        #region Properties

        /// <summary>The name of the card.</summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>The suit of the card.</summary>
        public CardSuit Suit
        {
            get { return _suit; }
            set
            {
                _suit = value;
                OnPropertyChanged("CardSuit");
            }
        }

        /// <summary>The value of the card.</summary>
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        /// <summary>Should the Card be hidden from the player?</summary>
        public bool Hidden
        {
            get { return _hidden; }
            set
            {
                _hidden = value;
                OnPropertyChanged("Hidden");
            }
        }

        /// <summary>Returns the name and suit of the card.</summary>
        public string CardToString => Hidden ? "?? of ??" : Name + " of " + Suit;

        #endregion Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Override Operators

        private static bool Equals(Card left, Card right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.Suit == right.Suit &&
             left.Value == right.Value && left.Hidden == right.Hidden;
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

        /// <summary>Initializes a default instance of Card.</summary>
        internal Card()
        {
        }

        /// <summary>Initializes an instance of Card by assigning Properties.</summary>
        /// <param name="name">Name of Card</param>
        /// <param name="suit">Suit of Card</param>
        /// <param name="value">Value of Card</param>
        /// <param name="hidden">Should the Card be hidden from the player?</param>
        internal Card(string name, CardSuit suit, int value, bool hidden)
        {
            Name = name;
            Suit = suit;
            Value = value;
            Hidden = hidden;
        }

        /// <summary>Replaces this instance of Card with another instance.</summary>
        /// <param name="otherCard">Instance to replace this instance</param>
        internal Card(Card otherCard)
        {
            Name = otherCard.Name;
            Suit = otherCard.Suit;
            Value = otherCard.Value;
            Hidden = otherCard.Hidden;
        }

        /// <summary>Replaces this instance of Card with another instance.</summary>
        /// <param name="otherCard">Instance to replace this instance</param>
        /// <param name="hidden">Should the Card be hidden from the player?</param>
        internal Card(Card otherCard, bool hidden)
        {
            Name = otherCard.Name;
            Suit = otherCard.Suit;
            Value = otherCard.Value;
            Hidden = hidden;
        }

        #endregion Constructors
    }
}