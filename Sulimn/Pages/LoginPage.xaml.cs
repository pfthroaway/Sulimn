using Extensions;
using Extensions.Enums;
using Sulimn.Classes;
using Sulimn.Pages.Characters;
using Sulimn.Pages.Exploration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sulimn.Pages
{
    /// <summary>Interaction logic for LoginPage.xaml</summary>
    public partial class LoginPage
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
            GameState.Navigate(new CityPage());
        }

        #endregion Login

        #region Button-Click Methods

        private void BtnNewHero_Click(object sender, RoutedEventArgs e)
        {
            GameState.Navigate(new NewHeroPage { PreviousPage = this });
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CheckLogin(TxtHeroName.Text.Trim(), PswdPassword.Password.Trim()))
                Login();
        }

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

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

        #endregion Page-Manipulation Methods

        public LoginPage()
        {
            InitializeComponent();
            TxtHeroName.Focus();
        }

        private void LoginPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameState.CalculateScale(Grid);
        }
    }
}