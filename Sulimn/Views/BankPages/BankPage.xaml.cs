using Extensions;
using Sulimn.Classes;
using Sulimn.Classes.Enums;
using Sulimn.Classes.HeroParts;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace Sulimn.Views.BankPages
{
    /// <summary>Interaction logic for BankPage.xaml</summary>
    public partial class BankPage : INotifyPropertyChanged
    {
        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Binds text to the labels.</summary>
        private void BindLabels()
        {
            DataContext = GameState.CurrentHero.Bank;
            LblGoldOnHand.DataContext = GameState.CurrentHero;
        }

        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Display Manipulation

        /// <summary>Adds text to the TxtBank Textbox.</summary>
        /// <param name="newText">Text to be added</param>
        internal void AddTextToTextBox(string newText) => Functions.AddTextToTextBox(TxtBank, newText);

        /// <summary>Checks what buttons should be enabled.</summary>
        internal void CheckButtons()
        {
            BtnDeposit.IsEnabled = GameState.CurrentHero.Gold > 0;
            BtnWithdraw.IsEnabled = GameState.CurrentHero.Bank.GoldInBank > 0;
            BtnTakeLoan.IsEnabled = GameState.CurrentHero.Bank.LoanAvailable > 0;
            BtnRepayLoan.IsEnabled = GameState.CurrentHero.Bank.LoanTaken > 0;
        }

        /// <summary>Displays the Bank Dialog Page.</summary>
        /// <param name="maximum">Maximum amount of Gold permitted</param>
        /// <param name="type">Type of Page information to be displayed</param>
        private void DisplayBankDialog(int maximum, BankAction type)
        {
            BankDialogPage bankDialogPage = new BankDialogPage();
            bankDialogPage.LoadPage(maximum, type);
            bankDialogPage.RefToBankPage = this;
            GameState.Navigate(bankDialogPage);
        }

        /// <summary>Loads the initial Bank state and Hero's Bank information..</summary>
        internal void LoadBank()
        {
            TxtBank.Text =
            "You enter the Bank. A teller beckons to you and you approach him. You tell him your name, and he rummages through a few papers. He finds one, and pulls it out.\n\n" +
            $"You have {GameState.CurrentHero.Bank.GoldInBankToString} gold available to withdraw. You also have an open credit line of { GameState.CurrentHero.Bank.LoanAvailableToString} gold.";
            BindLabels();
            CheckButtons();
        }

        #endregion Display Manipulation

        #region Button-Click Methods

        private void BtnBack_Click(object sender, RoutedEventArgs e) => ClosePage();

        private void BtnDeposit_Click(object sender, RoutedEventArgs e) => DisplayBankDialog(GameState.CurrentHero.Gold, BankAction.Deposit);

        private void BtnRepayLoan_Click(object sender, RoutedEventArgs e) => DisplayBankDialog(GameState.CurrentHero.Bank.LoanTaken, BankAction.Repay);

        private void BtnTakeLoan_Click(object sender, RoutedEventArgs e) => DisplayBankDialog(GameState.CurrentHero.Bank.LoanAvailable, BankAction.Borrow);

        private void BtnWithdraw_Click(object sender, RoutedEventArgs e) => DisplayBankDialog(GameState.CurrentHero.Bank.GoldInBank, BankAction.Withdrawal);

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private async void ClosePage()
        {
            await Task.Run(() => GameState.SaveHero(GameState.CurrentHero));
            GameState.GoBack();
        }

        public BankPage() => InitializeComponent();

        #endregion Page-Manipulation Methods
    }
}