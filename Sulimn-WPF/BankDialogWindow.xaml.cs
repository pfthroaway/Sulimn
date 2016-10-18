using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sulimn_WPF
{
    /// <summary>
    /// Interaction logic for BankDialogWindow.xaml
    /// </summary>
    public partial class BankDialogWindow : Window
    {
        private int _maximum = 0;
        private int _textAmount = 0;
        private string _type = "";

        internal BankWindow RefToBankWindow { get; set; }

        /// <summary>
        /// Load the necessary data for the form.
        /// </summary>
        /// <param name="maximum">Maximum amount of gold to be used.</param>
        /// <param name="type">What type of transaction is taking place.</param>
        internal void LoadWindow(int maximum, string type)
        {
            _maximum = maximum;
            _type = type;

            switch (_type)
            {
                case "Deposit":
                    lblDialog.Text = "How much gold would you like to deposit? You have " + _maximum.ToString("N0") + " gold with you.";
                    btnAction.Content = "_Deposit";
                    break;

                case "Withdrawal":
                    lblDialog.Text = "How much gold would you like to withdraw? You have " + _maximum.ToString("N0") + " gold in your account.";
                    btnAction.Content = "_Withdraw";
                    break;

                case "Repay Loan":
                    lblDialog.Text = "How much of your loan would you like to repay? You currently owe " + _maximum.ToString("N0") + " gold. You have " + GameState.CurrentHero.Gold.ToString("N0") + " with you.";
                    btnAction.Content = "_Repay";
                    break;

                case "Take Out Loan":
                    lblDialog.Text = "How much gold would you like to take out on loan? Your credit deems you worthy of receiving up to " + maximum.ToString("N0") + " gold. Remember, we have a 5% loan fee.";
                    btnAction.Content = "_Borrow";
                    break;
            }
        }

        #region Transaction Methods

        /// <summary>
        /// Deposit money into the bank.
        /// </summary>
        private void Deposit()
        {
            RefToBankWindow.GoldInBank += _textAmount;
            GameState.CurrentHero.Gold -= _textAmount;
            CloseWindow("You deposit " + _textAmount.ToString("N0") + " gold.");
        }

        /// <summary>
        /// Repay the loan.
        /// </summary>
        private void RepayLoan()
        {
            RefToBankWindow.LoanTaken -= _textAmount;
            RefToBankWindow.LoanAvailable += _textAmount;
            GameState.CurrentHero.Gold -= _textAmount;
            CloseWindow("You repay " + _textAmount.ToString("N0") + " gold on your loan.");
        }

        /// <summary>
        /// Take out a loan.
        /// </summary>
        private void TakeOutLoan()
        {
            RefToBankWindow.LoanTaken += _textAmount + (_textAmount / 20);
            RefToBankWindow.LoanAvailable -= (_textAmount + (_textAmount / 20));
            GameState.CurrentHero.Gold += _textAmount;
            CloseWindow("You take out a loan for " + _textAmount.ToString("N0") + " gold.");
        }

        /// <summary>
        /// Withdraw money from the bank account.
        /// </summary>
        private void Withdrawal()
        {
            RefToBankWindow.GoldInBank -= _textAmount;
            GameState.CurrentHero.Gold += _textAmount;
            CloseWindow("You withdraw " + _textAmount.ToString("N0") + " gold from your account.");
        }

        #endregion Transaction Methods

        #region Button-Click Methods

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(txtBank.Text, out _textAmount);

            if (_textAmount <= _maximum && _textAmount > 0)
            {
                switch (_type)
                {
                    case "Deposit":
                        if (_textAmount <= GameState.CurrentHero.Gold)
                            Deposit();
                        else
                            MessageBox.Show("Please enter a value less than or equal to your current gold. You currently have " + GameState.CurrentHero.GoldToString + " gold.");
                        break;

                    case "Withdrawal":
                        Withdrawal();
                        break;

                    case "Repay Loan":
                        if (_textAmount <= GameState.CurrentHero.Gold)
                        {
                            RepayLoan();
                        }
                        else
                            MessageBox.Show("Please enter a value less than or equal to your current gold. You currently have " + GameState.CurrentHero.GoldToString + " gold.");
                        break;

                    case "Take Out Loan":
                        TakeOutLoan();
                        break;
                }
            }
            else
                MessageBox.Show("Please enter a positive value less than or equal to your current gold. You currently have " + GameState.CurrentHero.GoldToString + " gold.");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow("");
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window
        /// </summary>
        /// <param name="text">Text to be passed back to the Bank Window</param>
        private void CloseWindow(string text)
        {
            RefToBankWindow.Show();
            if (text.Length > 0)
                RefToBankWindow.AddTextTT(text);
            RefToBankWindow.CheckButtons();
            this.Close();
        }

        public BankDialogWindow()
        {
            InitializeComponent();
            txtBank.Focus();
        }

        private void txtBet_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;

            bool controlKeyIsDown = Keyboard.IsKeyDown(Key.Back);

            if (controlKeyIsDown || (Key.D0 <= k && k <= Key.D9) || (Key.NumPad0 <= k && k <= Key.NumPad9))
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
        }

        private void windowBankDialog_Closing(object sender, CancelEventArgs e)
        {
            RefToBankWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}