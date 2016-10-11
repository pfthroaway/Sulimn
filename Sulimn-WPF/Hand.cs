using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulimn_WPF
{
    class Hand : INotifyPropertyChanged
    {
        List<Card> _cardList = new List<Card>();

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
        #endregion

        #region Data-Binding
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        internal int TotalValue()
        {
            int total = 0;
            foreach (Card card in _cardList)
                total += card.Value;
            return total;
        }

        #region Constructors
        internal Hand()
        {

        }

        internal Hand(List<Card> cardList)
        {
            CardList = cardList;
        }
        #endregion
    }
}