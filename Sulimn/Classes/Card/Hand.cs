using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Sulimn
{
    /// <summary>Represents a hand of playing cards.</summary>
    internal class Hand : INotifyPropertyChanged
    {
        private List<Card> _cardList = new List<Card>();

        /// <summary>Actual value of Cards in Hand.</summary>
        /// <returns>Actual value</returns>
        internal int ActualValue => _cardList.Sum(card => card.Value);

        /// <summary>Total value of Cards in Hand.</summary>
        /// <returns>Total value</returns>
        internal int TotalValue => _cardList.Where(card => !card.Hidden).Sum(card => card.Value);

        #region Properties

        /// <summary>List of cards in the hand.</summary>
        public List<Card> CardList
        {
            get { return _cardList; }
            set
            {
                _cardList = value;
                UpdateProperties();
            }
        }

        /// <summary>Current value of the Hand.</summary>
        public string Value => "Total: " + TotalValue;

        #endregion Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Hand Management

        /// <summary>Adds a Card to the Hand.</summary>
        /// <param name="newCard">Card to be added.</param>
        internal void AddCard(Card newCard)
        {
            CardList.Add(newCard);
            UpdateProperties();
        }

        /// <summary>Clears the Hidden state of all Cards in the Hand.</summary>
        internal void ClearHidden()
        {
            foreach (Card card in CardList)
                card.Hidden = false;
            UpdateProperties();
        }

        /// <summary>Converts an 11-valued Ace to be valued at 1.</summary>
        internal void ConvertAce()
        {
            foreach (Card card in CardList)
                if (card.Value == 11)
                {
                    card.Value = 1;
                    break;
                }
            UpdateProperties();
        }

        /// <summary>Updates the 3 important Properties of the Hand.</summary>
        private void UpdateProperties()
        {
            OnPropertyChanged("CardList");
            OnPropertyChanged("TotalValue");
            OnPropertyChanged("Value");
        }

        #endregion Hand Management

        #region Constructors

        /// <summary>Initializes a default instance of Hand.</summary>
        internal Hand()
        {
        }

        /// <summary>Initializes an instance of Hand by assigning the list of Cards.</summary>
        /// <param name="cardList">List of Cards</param>
        internal Hand(List<Card> cardList)
        {
            CardList = cardList;
        }

        #endregion Constructors
    }
}