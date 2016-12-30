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

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (pswdCurrentPassword.Password.Length >= 1 && pswdNewPassword.Password.Length >= 1 &&
                pswdConfirmPassword.Password.Length >= 1)
            {
                if (PasswordHash.ValidatePassword(pswdCurrentPassword.Password, GameState.AdminPassword))
                    if (pswdNewPassword.Password == pswdConfirmPassword.Password)
                        if (pswdCurrentPassword.Password != pswdNewPassword.Password)
                        {
                            GameState.AdminPassword = PasswordHash.HashPassword(pswdNewPassword.Password);
                            new Notification("Successfully changed administrator password.", "Sulimn",
                                NotificationButtons.OK, this).ShowDialog();
                            CloseWindow();
                        }
                        else
                        {
                            new Notification("The new password can't be the same as the current password.", "Sulimn",
                                NotificationButtons.OK, this).ShowDialog();
                        }
                    else
                        new Notification("Please ensure the new passwords match.", "Sulimn", NotificationButtons.OK,
                            this).ShowDialog();
                else
                    new Notification("Invalid current administrator password.", "Sulimn", NotificationButtons.OK, this)
                        .ShowDialog();
            }
            else
                new Notification("The old and new passwords must be at least 4 characters in length.", "Sulimn", NotificationButtons.OK, this).ShowDialog();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
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
            pswdCurrentPassword.Focus();
        }

        private void PswdChanged(object sender, RoutedEventArgs e)
        {
            btnSubmit.IsEnabled = pswdCurrentPassword.Password.Length >= 1 && pswdNewPassword.Password.Length >= 1 &&
                                  pswdConfirmPassword.Password.Length >= 1;
        }

        private void pswd_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.PasswordBoxGotFocus(sender);
        }

        private void windowAdminChangePassword_Closing(object sender, CancelEventArgs e)
        {
            RefToAdminWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}