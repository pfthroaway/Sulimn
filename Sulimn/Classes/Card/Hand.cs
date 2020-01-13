using System.Collections.Generic;
using System.Linq;

namespace Sulimn.Classes.Card
{
    /// <summary>Represents a hand of playing cards.</summary>
    internal class Hand : BaseINPC
    {
        private List<Card> _cardList = new List<Card>();

        #region Modifyering Properties

        /// <summary>List of <see cref="Card"/>s in the <see cref="Hand"/>.</summary>
        public List<Card> CardList
        {
            get => _cardList;
            set
            {
                _cardList = value;
                UpdateProperties();
            }
        }

        #endregion Modifyering Properties

        #region Helper Properties

        /// <summary>Actual value of <see cref="Card"/>s in the <see cref="Hand"/>.</summary>
        /// <returns>Actual value</returns>
        internal int ActualValue => _cardList.Sum(card => card.Value);

        /// <summary>Total value of <see cref="Card"/>s in <see cref="Hand"/>, excluding Hidden <see cref="Card"/>s.</summary>
        /// <returns>Total value</returns>
        internal int TotalValue => _cardList.Where(card => !card.Hidden).Sum(card => card.Value);

        /// <summary>Count of <see cref="Card"/>s in the <see cref="Hand"/>.</summary>
        internal int Count => CardList.Count;

        /// <summary>Current total value of the <see cref="Hand"/>, with preceding text.</summary>
        public string Value => TotalValue > 0 ? $"Total: {TotalValue}" : "";

        #endregion Helper Properties

        #region Hand Management

        /// <summary>Adds a <see cref="Card"/>s to the <see cref="Hand"/>.</summary>
        /// <param name="newCard">Card to be added.</param>
        internal void AddCard(Card newCard)
        {
            CardList.Add(newCard);
            UpdateProperties();
        }

        /// <summary>Can the <see cref="Hand"/> be split?</summary>
        /// <returns>True if <see cref="Hand"/> can be split</returns>
        internal bool CanSplit() => CardList.Count == 2 && CardList[0].Name == CardList[1].Name && CardList[0].Value == CardList[1].Value;

        /// <summary>Checks whether the <see cref="Hand"/> can be Doubled Down.</summary>
        /// <returns>Returns true if <see cref="Hand"/> can be Doubled Down</returns>
        internal bool CanDoubleDown() => CardList.Count == 2 && TotalValue >= 9 && TotalValue <= 11;

        /// <summary>Clears the Hidden state of all <see cref="Card"/>s in the <see cref="Hand"/>.</summary>
        internal void ClearHidden()
        {
            foreach (Card card in CardList)
                card.Hidden = false;
            UpdateProperties();
        }

        /// <summary>Converts an 11-valued Ace <see cref="Card"/> to be valued at 1.</summary>
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

        /// <summary>Checks whether a <see cref="Hand"/> has an Ace valued at eleven in it.</summary>
        /// <returns>Returns true if <see cref="Hand"/> has an Ace valued at eleven in it</returns>
        internal bool HasAceEleven() => CardList.Any(card => card.Value == 11);

        /// <summary>Checks whether a <see cref="Hand"/> has reached Blackjack.</summary>
        /// <returns>Returns true if the <see cref="Hand"/>'s value is 21</returns>
        internal bool HasBlackjack() => TotalValue == 21;

        /// <summary>Checks whether the <see cref="Hand"/> has a Five Card Charlie.</summary>
        /// <returns>Returns true if the <see cref="Hand"/> has a Five Card Charlie</returns>
        internal bool HasFiveCardCharlie() => Count == 5 && (TotalValue < 21 || (HasAceEleven() && TotalValue <= 31));

        /// <summary>Checks whether the current <see cref="Hand"/> has gone Bust.</summary>
        /// <returns>Returns true if <see cref="Hand"/> has gone Bust</returns>
        internal bool IsBust() => !HasAceEleven() && ActualValue > 21;

        /// <summary>Updates the 3 important Properties of the Hand.</summary>
        private void UpdateProperties() => NotifyPropertyChanged(nameof(ActualValue), nameof(CardList), nameof(Count), nameof(TotalValue), nameof(Value));

        #endregion Hand Management

        #region Constructors

        /// <summary>Initializes a default instance of <see cref="Hand"/>.</summary>
        internal Hand()
        {
        }

        /// <summary>Initializes an instance of <see cref="Hand"/> by assigning the list of <see cref="Card"/>s.</summary>
        /// <param name="cardList">List of <see cref="Card"/>s</param>
        internal Hand(List<Card> cardList) => CardList = cardList;

        #endregion Constructors
    }
}