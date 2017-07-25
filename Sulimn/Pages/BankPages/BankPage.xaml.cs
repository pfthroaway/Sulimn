using Extensions;
using Sulimn.Classes;
using Sulimn.Classes.Enums;
using Sulimn.Classes.HeroParts;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace Sulimn.Pages.BankPages
{
    /// <summary>Interaction logic for BankPage.xaml</summary>
    public partial class BankPage : INotifyPropertyChanged
    {
        internal Bank HeroBank = new Bank();

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Binds text to the labels.</summary>
        private void BindLabels()
        {
            DataContext = HeroBank;
            LblGoldOnHand.DataContext = GameState.CurrentHero.Inventory;
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Display Manipulation

        /// <summary>Adds text to the TxtBank Textbox.</summary>
        /// <param name="newText">Text to be added</param>
        internal void AddTextToTextBox(string newText)
        {
            Functions.AddTextToTextBox(TxtBank, newText);
        }

        /// <summary>Checks what buttons should be enabled.</summary>
        internal void CheckButtons()
        {
            BtnDeposit.IsEnabled = GameState.CurrentHero.Inventory.Gold > 0;
            BtnWithdraw.IsEnabled = HeroBank.GoldInBank > 0;
            BtnTakeLoan.IsEnabled = HeroBank.LoanAvailable > 0;
            BtnRepayLoan.IsEnabled = HeroBank.LoanTaken > 0;
        }

        /// <summary>Displays the Bank Dialog Page.</summary>
        /// <param name="maximum">Maximum amount of gold permitted</param>
        /// <param name="type">Type of Page information to be displayed</param>
        private void DisplayBankDialog(int maximum, BankAction type)
        {
            BankDialogPage bankDialogPage = new BankDialogPage();
            bankDialogPage.LoadPage(maximum, type);
            bankDialogPage.RefToBankPage = this;
            GameState.Navigate(bankDialogPage);
        }

        /// <summary>Loads the initial Bank state and Hero's Bank information..</summary>
        internal async void LoadBank()
        {
            HeroBank = await GameState.LoadHeroBank(GameState.CurrentHero);

            TxtBank.Text =
            "You enter the Bank. A teller beckons to you and you approach him. You tell him your name, and he rummages through a few papers. He finds one, and pulls it out.\n\n" +
            $"You have {HeroBank.GoldInBankToString} gold available to withdraw. You also have an open credit line of { HeroBank.LoanAvailableToString} gold.";
            BindLabels();
            CheckButtons();
        }

        #endregion Display Manipulation

        #region Button-Click Methods

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }

        private void BtnDeposit_Click(object sender, RoutedEventArgs e)
        {
            DisplayBankDialog(GameState.CurrentHero.Inventory.Gold, BankAction.Deposit);
        }

        private void BtnRepayLoan_Click(object sender, RoutedEventArgs e)
        {
            DisplayBankDialog(HeroBank.LoanTaken, BankAction.Repay);
        }

        private void BtnTakeLoan_Click(object sender, RoutedEventArgs e)
        {
            DisplayBankDialog(HeroBank.LoanAvailable, BankAction.Borrow);
        }

        private void BtnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            DisplayBankDialog(HeroBank.GoldInBank, BankAction.Withdrawal);
        }

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private async void ClosePage()
        {
            await Task.Run(() => GameState.SaveHero(GameState.CurrentHero));
            await Task.Run(() => GameState.SaveHeroBank(HeroBank.GoldInBank, HeroBank.LoanTaken));
            GameState.GoBack();
        }

        public BankPage()
        {
            InitializeComponent();
        }

        #endregion Page-Manipulation Methods

        private void BankPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameState.CalculateScale(Grid);
        }
    }
}