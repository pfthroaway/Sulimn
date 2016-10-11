using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn_WPF
{
    /// <summary>
    /// Interaction logic for BankDialogWindow.xaml
    /// </summary>
    public partial class BankDialogWindow : Window
    {
        private int _maximum = 0;
        private int textAmount = 0;
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
                    lblDialog.Text = "How much of your loan would you like to repay? You currently owe " + _maximum.ToString("N0") + " gold. You have " + GameState.currentHero.Gold.ToString("N0") + " with you.";
                    btnAction.Content = "_Repay";
                    break;

                case "Take Out Loan":
                    lblDialog.Text = "How much gold would you like to take out on loan? Your credit deems you worthy of receiving up to " + maximum.ToString("N0") + " gold. Remember, we have a 10% loan fee.";
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
            RefToBankWindow.GoldInBank += textAmount;
            GameState.currentHero.Gold -= textAmount;
            CloseWindow("You deposit " + textAmount.ToString("N0") + " gold.");
        }

        /// <summary>
        /// Repay the loan.
        /// </summary>
        private void RepayLoan()
        {
            RefToBankWindow.LoanTaken -= textAmount;
            RefToBankWindow.LoanAvailable += textAmount;
            GameState.currentHero.Gold -= textAmount;
            CloseWindow("You repay " + textAmount.ToString("N0") + " gold on your loan.");
        }

        /// <summary>
        /// Take out a loan.
        /// </summary>
        private void TakeOutLoan()
        {
            RefToBankWindow.LoanTaken += textAmount + (textAmount / 10);
            RefToBankWindow.LoanAvailable -= (textAmount + (textAmount / 10));
            GameState.currentHero.Gold += textAmount;
            CloseWindow("You take out a loan for " + textAmount.ToString("N0") + " gold.");
        }

        /// <summary>
        /// Withdraw money from the bank account.
        /// </summary>
        private void Withdrawal()
        {
            RefToBankWindow.GoldInBank -= textAmount;
            GameState.currentHero.Gold += textAmount;
            CloseWindow("You withdraw " + textAmount.ToString("N0") + " gold from your account.");
        }

        #endregion Transaction Methods

        #region Button-Click Methods

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            textAmount = Convert.ToInt32(txtBank.Text);

            if (textAmount <= _maximum)
            {
                switch (_type)
                {
                    case "Deposit":
                        if (textAmount <= GameState.currentHero.Gold)
                            Deposit();
                        else
                            MessageBox.Show("Please enter a value less than or equal to your current gold. You currently have " + GameState.currentHero.Gold + " gold.");
                        break;

                    case "Withdrawal":
                        Withdrawal();
                        break;

                    case "Repay Loan":
                        if (textAmount <= GameState.currentHero.Gold)
                        {
                            RepayLoan();
                        }
                        else
                            MessageBox.Show("Please enter a value less than or equal to your current gold. You currently have " + GameState.currentHero.Gold + " gold.");
                        break;

                    case "Take Out Loan":
                        TakeOutLoan();
                        break;
                }
            }
            else
                MessageBox.Show("Please enter a value less than or equal to your current gold. You currently have " + GameState.currentHero.Gold + " gold.");
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
        }

        private void txtBank_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void windowBankDialog_Closing(object sender, CancelEventArgs e)
        {
        }

        #endregion Window-Manipulation Methods
    }
}