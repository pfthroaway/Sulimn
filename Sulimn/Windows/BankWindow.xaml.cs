using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Extensions;

namespace Sulimn
{
    /// <summary>Interaction logic for BankWindow.xaml</summary>
    public partial class BankWindow : INotifyPropertyChanged
    {
        internal Bank HeroBank = new Bank();

        internal CityWindow RefToCityWindow { private get; set; }

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

        /// <summary>Displays the Bank Dialog Window.</summary>
        /// <param name="maximum">Maximum amount of gold permitted</param>
        /// <param name="type">Type of Window information to be displayed</param>
        private void DisplayBankDialog(int maximum, BankAction type)
        {
            BankDialogWindow bankDialogWindow = new BankDialogWindow();
            bankDialogWindow.LoadWindow(maximum, type);
            bankDialogWindow.RefToBankWindow = this;
            bankDialogWindow.Show();
            Visibility = Visibility.Hidden;
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
            CloseWindow();
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

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public BankWindow()
        {
            InitializeComponent();
        }

        private async void WindowBank_Closing(object sender, CancelEventArgs e)
        {
            RefToCityWindow.Show();
            await Task.Run(() => GameState.SaveHero(GameState.CurrentHero));
            await Task.Run(() => GameState.SaveHeroBank(HeroBank.GoldInBank, HeroBank.LoanTaken));
        }

        #endregion Window-Manipulation Methods
    }
}