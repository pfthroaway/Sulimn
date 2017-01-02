using Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sulimn
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow
    {
        #region Login

        /// <summary>Logs the Hero in.</summary>
        private void Login()
        {
            txtHeroName.Text = "";
            pswdPassword.Password = "";
            txtHeroName.Focus();
            CityWindow cityWindow = new CityWindow { RefToMainWindow = this };
            cityWindow.Show();
            Visibility = Visibility.Hidden;
        }

        #endregion Login

        #region Button-Click Methods

        private void btnNewHero_Click(object sender, RoutedEventArgs e)
        {
            NewHeroWindow newHeroWindow = new NewHeroWindow { RefToMainWindow = this };
            newHeroWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CheckLogin(txtHeroName.Text.Trim(), pswdPassword.Password.Trim()))
                Login();
        }

        private void mnuAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminPasswordWindow adminPasswordWindow = new AdminPasswordWindow { RefToMainWindow = this };
            adminPasswordWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void mnuFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        public MainWindow()
        {
            InitializeComponent();
            txtHeroName.Focus();
        }

        private void txtHeroName_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.TextBoxGotFocus(sender);
        }

        private void pswdPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.PasswordBoxGotFocus(sender);
        }

        private void txtHeroName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Functions.PreviewKeyDown(e, KeyType.Letters);
        }

        /// <summary>Manages TextBox/PasswordBox text being changed.</summary>
        private void TextChanged()
        {
            btnLogin.IsEnabled = txtHeroName.Text.Length > 0 && pswdPassword.Password.Length > 0;
        }

        private void txtHeroName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Functions.TextBoxTextChanged(sender, KeyType.Letters);
            TextChanged();
        }

        private void pswdPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            TextChanged();
        }

        private async void windowMain_Loaded(object sender, RoutedEventArgs e)
        {
            await GameState.LoadAll();
        }

        #endregion Window-Manipulation Methods
    }
}