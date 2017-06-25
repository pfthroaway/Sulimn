using Extensions;
using Sulimn.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sulimn.Windows
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow
    {
        #region Login

        /// <summary>Clears the input boxes.</summary>
        internal void ClearInput()
        {
            TxtHeroName.Text = "";
            PswdPassword.Password = "";
            TxtHeroName.Focus();
        }

        /// <summary>Logs the Hero in.</summary>
        private void Login()
        {
            ClearInput();
            Exploration.CityWindow cityWindow = new Exploration.CityWindow { RefToMainWindow = this };
            cityWindow.Show();
            Visibility = Visibility.Hidden;
        }

        #endregion Login

        #region Button-Click Methods

        private void BtnNewHero_Click(object sender, RoutedEventArgs e)
        {
            Characters.NewHeroWindow newHeroWindow = new Characters.NewHeroWindow { RefToMainWindow = this };
            newHeroWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CheckLogin(TxtHeroName.Text.Trim(), PswdPassword.Password.Trim()))
                Login();
        }

        private void MnuAdmin_Click(object sender, RoutedEventArgs e)
        {
            Admin.AdminPasswordWindow adminPasswordWindow = new Admin.AdminPasswordWindow { RefToMainWindow = this };
            adminPasswordWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void MnuFileExit_Click(object sender, RoutedEventArgs e) => Close();

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        public MainWindow()
        {
            InitializeComponent();
            TxtHeroName.Focus();
        }

        private void TxtHeroName_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.TextBoxGotFocus(sender);
        }

        private void PswdPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.PasswordBoxGotFocus(sender);
        }

        private void TxtHeroName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Functions.PreviewKeyDown(e, KeyType.Letters);
        }

        /// <summary>Manages TextBox/PasswordBox text being changed.</summary>
        private void TextChanged()
        {
            BtnLogin.IsEnabled = TxtHeroName.Text.Length > 0 && PswdPassword.Password.Length > 0;
        }

        private void TxtHeroName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Functions.TextBoxTextChanged(sender, KeyType.Letters);
            TextChanged();
        }

        private void PswdPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            TextChanged();
        }

        private async void WindowMain_Loaded(object sender, RoutedEventArgs e)
        {
            await GameState.LoadAll();
        }

        #endregion Window-Manipulation Methods
    }
}