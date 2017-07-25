using Extensions.Encryption;
using Sulimn.Classes;
using System.Windows;

namespace Sulimn.Pages.Admin
{
    /// <summary>Interaction logic for AdminPasswordPage.xaml</summary>
    public partial class AdminPasswordPage
    {
        private bool _admin;

        #region Button-Click Methods

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (Argon2.ValidatePassword(GameState.AdminPassword, PswdAdmin.Password))
            {
                _admin = true;
                ClosePage();
            }
            else
            {
                GameState.DisplayNotification("Invalid login.", "Sulimn");
                PswdAdmin.SelectAll();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage()
        {
            GameState.GoBack();
            if (_admin)
                GameState.Navigate(new AdminPage());
            else
                GameState.MainWindow.MnuAdmin.IsEnabled = true;
        }

        public AdminPasswordPage()
        {
            InitializeComponent();
            PswdAdmin.Focus();
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            BtnSubmit.IsEnabled = PswdAdmin.Password.Length > 0;
        }

        #endregion Page-Manipulation Methods

        private void AdminPasswordPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameState.CalculateScale(Grid);
        }
    }
}