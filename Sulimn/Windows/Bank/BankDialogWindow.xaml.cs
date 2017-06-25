using Extensions;
using Sulimn.Classes;
using Sulimn.Classes.Enums;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sulimn.Windows.Bank
{
    /// <summary>Interaction logic for BankDialogWindow.xaml</summary>
    public partial class BankDialogWindow
    {
        private BankAction _action;
        private string _actionText = "";
        private int _maximum;
        private int _textAmount;
        private string _dialogText = "";
        internal BankWindow RefToBankWindow { private get; set; }

        public string DialogText
        {
            get => _dialogText;
            set
            {
                _dialogText = value;
                OnPropertyChanged("DialogText");
            }
        }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        /// <summary>Load the necessary data for the Window.</summary>
        /// <param name="maximum">Maximum amount of gold to be used.</param>
        /// <param name="type">What type of transaction is taking place.</param>
        internal void LoadWindow(int maximum, BankAction type)
        {
            _maximum = maximum;
            _action = type;

            switch (_action)
            {
                case BankAction.Deposit:
                    DialogText = $"How much gold would you like to deposit? You have {_maximum:N0} gold with you.";
                    BtnAction.Content = new AccessText { Text = "_Deposit" };
                    break;

                case BankAction.Withdrawal:
                    DialogText = $"How much gold would you like to withdraw? You have {_maximum:N0} gold in your account.";
                    BtnAction.Content = "_Withdraw";
                    break;

                case BankAction.Repay:
                    DialogText = $"How much of your loan would you like to repay? You currently owe {_maximum:N0} gold. You have {GameState.CurrentHero.Inventory.Gold:N0} with you.";
                    BtnAction.Content = "_Repay";
                    break;

                case BankAction.Borrow:
                    DialogText =
                    $"How much gold would you like to take out on loan? Your credit deems you worthy of receiving up to {_maximum:N0} gold. Remember, we have a 5% loan fee.";
                    BtnAction.Content = "_Borrow";
                    break;

                default:
                    GameState.DisplayNotification("How did you break my game?", "Sulimn", this);
                    break;
            }
        }

        #region Transaction Methods

        /// <summary>Deposit money into the bank.</summary>
        private void Deposit()
        {
            RefToBankWindow.HeroBank.GoldInBank += _textAmount;
            GameState.CurrentHero.Inventory.Gold -= _textAmount;
            CloseWindow($"You deposit {_textAmount:N0} gold.");
        }

        /// <summary>Repay the loan.</summary>
        private void RepayLoan()
        {
            RefToBankWindow.HeroBank.LoanTaken -= _textAmount;
            RefToBankWindow.HeroBank.LoanAvailable += _textAmount;
            GameState.CurrentHero.Inventory.Gold -= _textAmount;
            CloseWindow($"You repay {_textAmount:N0} gold on your loan.");
        }

        /// <summary>Take out a loan.</summary>
        private void TakeOutLoan()
        {
            RefToBankWindow.HeroBank.LoanTaken += _textAmount + _textAmount / 20;
            RefToBankWindow.HeroBank.LoanAvailable -= _textAmount + _textAmount / 20;
            GameState.CurrentHero.Inventory.Gold += _textAmount;
            CloseWindow($"You take out a loan for {_textAmount:N0} gold.");
        }

        /// <summary>Withdraw money from the bank account.</summary>
        private void Withdrawal()
        {
            RefToBankWindow.HeroBank.GoldInBank -= _textAmount;
            GameState.CurrentHero.Inventory.Gold += _textAmount;
            CloseWindow($"You withdraw {_textAmount:N0} gold from your account.");
        }

        #endregion Transaction Methods

        #region Button-Click Methods

        private void BtnAction_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(TxtBank.Text, out _textAmount);

            if (_textAmount <= _maximum && _textAmount > 0)
                switch (_action)
                {
                    case BankAction.Deposit:
                        if (_textAmount <= GameState.CurrentHero.Inventory.Gold)
                            Deposit();
                        else
                            GameState.DisplayNotification($"Please enter a value less than or equal to your current gold. You currently have {GameState.CurrentHero.Inventory.GoldToString} gold.", "Sulimn", this);
                        break;

                    case BankAction.Withdrawal:
                        Withdrawal();
                        break;

                    case BankAction.Repay:
                        if (_textAmount <= GameState.CurrentHero.Inventory.Gold)
                            RepayLoan();
                        else
                            GameState.DisplayNotification($"Please enter a value less than or equal to your current gold. You currently have {GameState.CurrentHero.Inventory.GoldToString} gold.", "Sulimn",
                            this);
                        break;

                    case BankAction.Borrow:
                        TakeOutLoan();
                        break;

                    default:
                        GameState.DisplayNotification("How did you break my game?", "Sulimn", this);
                        break;
                }
            else
                GameState.DisplayNotification($"Please enter a positive value less than or equal to your current gold. You currently have {GameState.CurrentHero.Inventory.GoldToString} gold.", "Sulimn", this);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow("");
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>Closes the Window</summary>
        /// <param name="text">Text to be passed back to the Bank Window</param>
        private void CloseWindow(string text)
        {
            _actionText = text;
            Close();
        }

        public BankDialogWindow()
        {
            InitializeComponent();
            TxtBank.Focus();
            DataContext = this;
        }

        private void TxtBank_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.TextBoxGotFocus(sender);
        }

        private void TxtBank_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Functions.PreviewKeyDown(e, KeyType.Integers);
        }

        private void TxtBank_TextChanged(object sender, TextChangedEventArgs e)
        {
            Functions.TextBoxTextChanged(sender, KeyType.Integers);
            BtnAction.IsEnabled = TxtBank.Text.Length > 0;
        }

        private void WindowBankDialog_Closing(object sender, CancelEventArgs e)
        {
            RefToBankWindow.Show();
            if (_actionText.Length > 0)
                RefToBankWindow.AddTextToTextBox(_actionText);
            RefToBankWindow.CheckButtons();
        }

        #endregion Window-Manipulation Methods
    }
}