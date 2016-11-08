using System.Collections.Generic;
using System.ComponentModel;

namespace Sulimn_WPF
{
    /// <summary>
    /// Represents a hand of playing cards.
    /// </summary>
    internal class Hand : INotifyPropertyChanged
    {
        private List<Card> _cardList = new List<Card>();

        #region Properties

        public List<Card> CardList
        {
            get { return _cardList; }
            set { _cardList = value; OnPropertyChanged("CardList"); OnPropertyChanged("Value"); }
        }

        public string Value
        {
            get { return "Total: " + TotalValue(); }
        }

        #endregion Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        /// <summary>
        /// Total value of Cards in Hand.
        /// </summary>
        /// <returns>Total value</returns>
        internal int TotalValue()
        {
            int total = 0;
            foreach (Card card in _cardList)
                total += card.Value;
            return total;
        }

        #region Constructors

        /// <summary>
        /// Initializes a default instance of Hand.
        /// </summary>
        internal Hand()
        {
        }

        /// <summary>
        /// Initializes an instance of Hand by assigning the list of Cards.
        /// </summary>
        /// <param name="cardList">List of Cards</param>
        internal Hand(List<Card> cardList)
        {
            CardList = cardList;
        }

        #endregion Constructors
    }
}