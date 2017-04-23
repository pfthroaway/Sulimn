using System.ComponentModel;

namespace Sulimn
{
    /// <summary>Represents an account at the Bank.</summary>
    public class Bank : INotifyPropertyChanged
    {
        private int _goldInBank, _loanAvailable, _loanTaken;

        #region Modifying Properties

        /// <summary>Gold the Hero has in the bank.</summary>
        public int GoldInBank
        {
            get => _goldInBank;
            set
            {
                _goldInBank = value;
                OnPropertyChanged("GoldInBankToString");
            }
        }

        /// <summary>Gold the Hero has available on loan.</summary>
        public int LoanAvailable
        {
            get => _loanAvailable;
            set
            {
                _loanAvailable = value;
                OnPropertyChanged("LoanAvailableToString");
            }
        }

        /// <summary>Gold the Hero has taken out on loan.</summary>
        public int LoanTaken
        {
            get => _loanTaken;
            set
            {
                _loanTaken = value;
                OnPropertyChanged("LoanTakenToString");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Gold the Hero has in the bank, formatted.</summary>
        public string GoldInBankToString => GoldInBank.ToString("N0");

        /// <summary>Gold the Hero has available on loan, formatted.</summary>
        public string LoanAvailableToString => LoanAvailable.ToString("N0");

        /// <summary>Gold the Hero has taken out on loan, formatted.</summary>
        public string LoanTakenToString => LoanTaken.ToString("N0");

        #endregion Helper Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Constructors

        public Bank()
        {
        }

        public Bank(int goldInBank, int loanTaken, int loanAvailable)
        {
            GoldInBank = goldInBank;
            LoanTaken = loanTaken;
            LoanAvailable = loanAvailable;
        }

        public Bank(Bank otherBank)
        {
            GoldInBank = otherBank.GoldInBank;
            LoanTaken = otherBank.LoanTaken;
            LoanAvailable = otherBank.LoanAvailable;
        }

        #endregion Constructors
    }
}