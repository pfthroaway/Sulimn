using Extensions;
using Extensions.Enums;
using Sulimn.Classes;
using Sulimn.Classes.Enums;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sulimn.Pages.BankPages
{
    /// <summary>Interaction logic for BankDialogPage.xaml</summary>
    public partial class BankDialogPage
    {
        private BankAction _action;
        private string _actionText = "";
        private int _maximum;
        private int _textAmount;
        private string _dialogText = "";
        internal BankPage RefToBankPage { private get; set; }

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

        protected virtual void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        /// <summary>Load the necessary data for the Page.</summary>
        /// <param name="maximum">Maximum amount of gold to be used.</param>
        /// <param name="type">What type of transaction is taking place.</param>
        internal void LoadPage(int maximum, BankAction type)
        {
            _maximum = maximum;
            _action = type;

            switch (_action)
            {
                case BankAction.Deposit:
                    DialogText = $"How much gold would you like to deposit? You have {_maximum:N0} gold with you.";
                    BtnAction.Content = "_Deposit";
                    break;

                case BankAction.Withdrawal:
                    DialogText = $"How much gold would you like to withdraw? You have {_maximum:N0} gold in your account.";
                    BtnAction.Content = "_Withdraw";
                    break;

                case BankAction.Repay:
                    DialogText = $"How much of your loan would you like to repay? You currently owe {_maximum:N0} gold. You have {GameState.CurrentHero.GoldToString} with you.";
                    BtnAction.Content = "_Repay";
                    break;

                case BankAction.Borrow:
                    DialogText =
                    $"How much gold would you like to take out on loan? Your credit deems you worthy of receiving up to {_maximum:N0} gold. Remember, we have a 5% loan fee.";
                    BtnAction.Content = "_Borrow";
                    break;

                default:
                    GameState.DisplayNotification("How did you break my game?", "Sulimn");
                    break;
            }
        }

        #region Transaction Methods

        /// <summary>Deposit money into the bank.</summary>
        private void Deposit()
        {
            RefToBankPage.HeroBank.GoldInBank += _textAmount;
            GameState.CurrentHero.Gold -= _textAmount;
            ClosePage($"You deposit {_textAmount:N0} gold.");
        }

        /// <summary>Repay the loan.</summary>
        private void RepayLoan()
        {
            RefToBankPage.HeroBank.LoanTaken -= _textAmount;
            RefToBankPage.HeroBank.LoanAvailable += _textAmount;
            GameState.CurrentHero.Gold -= _textAmount;
            ClosePage($"You repay {_textAmount:N0} gold on your loan.");
        }

        /// <summary>Take out a loan.</summary>
        private void TakeOutLoan()
        {
            RefToBankPage.HeroBank.LoanTaken += _textAmount + _textAmount / 20;
            RefToBankPage.HeroBank.LoanAvailable -= _textAmount + _textAmount / 20;
            GameState.CurrentHero.Gold += _textAmount;
            ClosePage($"You take out a loan for {_textAmount:N0} gold.");
        }

        /// <summary>Withdraw money from the bank account.</summary>
        private void Withdrawal()
        {
            RefToBankPage.HeroBank.GoldInBank -= _textAmount;
            GameState.CurrentHero.Gold += _textAmount;
            ClosePage($"You withdraw {_textAmount:N0} gold from your account.");
        }

        #endregion Transaction Methods

        #region Button-Click Methods

        private void BtnAction_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(TxtBank.Text, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out _textAmount);

            if (_textAmount <= _maximum && _textAmount > 0)
                switch (_action)
                {
                    case BankAction.Deposit:
                        if (_textAmount <= GameState.CurrentHero.Gold)
                            Deposit();
                        else
                            GameState.DisplayNotification($"Please enter a value less than or equal to your current gold. You currently have {GameState.CurrentHero.GoldToString} gold.", "Sulimn");
                        break;

                    case BankAction.Withdrawal:
                        Withdrawal();
                        break;

                    case BankAction.Repay:
                        if (_textAmount <= GameState.CurrentHero.Gold)
                            RepayLoan();
                        else
                            GameState.DisplayNotification($"Please enter a value less than or equal to your current gold. You currently have {GameState.CurrentHero.GoldToString} gold.", "Sulimn");
                        break;

                    case BankAction.Borrow:
                        TakeOutLoan();
                        break;

                    default:
                        GameState.DisplayNotification("How did you break my game?", "Sulimn");
                        break;
                }
            else
                GameState.DisplayNotification($"Please enter a positive value less than or equal to your current gold. You currently have {GameState.CurrentHero.GoldToString} gold.", "Sulimn");
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) => ClosePage("");

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page</summary>
        /// <param name="text">Text to be passed back to the Bank Page</param>
        private void ClosePage(string text)
        {
            _actionText = text;
            if (_actionText.Length > 0)
                RefToBankPage.AddTextToTextBox(_actionText);
            RefToBankPage.CheckButtons();
            GameState.GoBack();
        }

        public BankDialogPage()
        {
            InitializeComponent();
            TxtBank.Focus();
            DataContext = this;
        }

        private void TxtBank_GotFocus(object sender, RoutedEventArgs e) => Functions.TextBoxGotFocus(sender);

        private void TxtBank_PreviewKeyDown(object sender, KeyEventArgs e) => Functions.PreviewKeyDown(e, KeyType.Integers);

        private void TxtBank_TextChanged(object sender, TextChangedEventArgs e)
        {
            Functions.TextBoxTextChanged(sender, KeyType.Integers);
            BtnAction.IsEnabled = TxtBank.Text.Length > 0;
        }

        #endregion Page-Manipulation Methods

        private void BankDialogPage_OnLoaded(object sender, RoutedEventArgs e) => GameState.CalculateScale(Grid);
    }
}