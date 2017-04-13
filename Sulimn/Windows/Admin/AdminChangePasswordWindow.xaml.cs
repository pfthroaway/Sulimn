using Extensions;
using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>Interaction logic for AdminChangePasswordWindow.xaml</summary>
    public partial class AdminChangePasswordWindow
    {
        internal AdminWindow RefToAdminWindow { private get; set; }

        #region Button-Click Methods

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (PswdCurrentPassword.Password.Length >= 1 && PswdNewPassword.Password.Length >= 1 &&
                PswdConfirmPassword.Password.Length >= 1)
            {
                if (PasswordHash.ValidatePassword(PswdCurrentPassword.Password, GameState.AdminPassword))
                    if (PswdNewPassword.Password == PswdConfirmPassword.Password)
                        if (PswdCurrentPassword.Password != PswdNewPassword.Password)
                        {
                            GameState.AdminPassword = PasswordHash.HashPassword(PswdNewPassword.Password);
                            GameState.DisplayNotification("Successfully changed administrator password.", "Sulimn",
                                NotificationButtons.OK, this);
                            CloseWindow();
                        }
                        else
                        {
                            GameState.DisplayNotification("The new password can't be the same as the current password.", "Sulimn",
                                NotificationButtons.OK, this);
                        }
                    else
                        GameState.DisplayNotification("Please ensure the new passwords match.", "Sulimn", NotificationButtons.OK,
                            this);
                else
                    GameState.DisplayNotification("Invalid current administrator password.", "Sulimn", NotificationButtons.OK, this)
                        ;
            }
            else
                GameState.DisplayNotification("The old and new passwords must be at least 4 characters in length.", "Sulimn", NotificationButtons.OK, this);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public AdminChangePasswordWindow()
        {
            InitializeComponent();
            PswdCurrentPassword.Focus();
        }

        private void PswdChanged(object sender, RoutedEventArgs e)
        {
            BtnSubmit.IsEnabled = PswdCurrentPassword.Password.Length >= 1 && PswdNewPassword.Password.Length >= 1 &&
                                  PswdConfirmPassword.Password.Length >= 1;
        }

        private void Pswd_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.PasswordBoxGotFocus(sender);
        }

        private void WindowAdminChangePassword_Closing(object sender, CancelEventArgs e)
        {
            RefToAdminWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}