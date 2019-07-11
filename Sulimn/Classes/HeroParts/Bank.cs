using System.ComponentModel;

namespace Sulimn.Classes.HeroParts
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
        public string GoldInBankToString => GoldInBank.ToString("N0", GameState.CurrentCulture);

        /// <summary>Gold the Hero has available on loan, formatted.</summary>
        public string LoanAvailableToString => LoanAvailable.ToString("N0", GameState.CurrentCulture);

        /// <summary>Gold the Hero has taken out on loan, formatted.</summary>
        public string LoanTakenToString => LoanTaken.ToString("N0", GameState.CurrentCulture);

        #endregion Helper Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Constructors

        /// <summary>Initializes a default instance of Bank.</summary>
        public Bank()
        {
            GoldInBank = 0;
            LoanAvailable = 0;
            LoanTaken = 0;
        }

        /// <summary>Initializes an instance of Bank by assigning Properties.</summary>
        /// <param name="goldInBank">Gold in the bank</param>
        /// <param name="loanTaken">Loan already taken out</param>
        /// <param name="loanAvailable">Loan available to be taken out</param>
        public Bank(int goldInBank, int loanTaken, int loanAvailable)
        {
            GoldInBank = goldInBank;
            LoanTaken = loanTaken;
            LoanAvailable = loanAvailable;
        }

        /// <summary>Replaces this instance of Bank with another instance.</summary>
        /// <param name="other">Instance of Bank to replace this instance</param>
        public Bank(Bank other) : this(other.GoldInBank, other.LoanTaken, other.LoanAvailable)
        {
        }

        #endregion Constructors
    }
}