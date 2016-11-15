﻿using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for AdminChangePasswordWindow.xaml
    /// </summary>
    public partial class AdminChangePasswordWindow : Window
    {
        internal AdminWindow RefToAdminWindow { get; set; }

        #region Button-Click Methods

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordHash.ValidatePassword(pswdCurrentPassword.Password, GameState.AdminPassword))
            {
                if (pswdNewPassword.Password == pswdConfirmPassword.Password)
                {
                    if (pswdCurrentPassword.Password != pswdNewPassword.Password)
                    {
                        GameState.AdminPassword = PasswordHash.HashPassword(pswdNewPassword.Password);
                        MessageBox.Show("Successfully changed administrator password.", "Sulimn", MessageBoxButton.OK);
                        CloseWindow();
                    }
                    else
                        MessageBox.Show("The new password can't be the same as the current password.", "Sulimn", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("Please ensure the new passwords match.", "Sulimn", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Invalid current administrator password.", "Sulimn", MessageBoxButton.OK);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
        }

        public AdminChangePasswordWindow()
        {
            InitializeComponent();
            pswdCurrentPassword.Focus();
        }

        private void pswdChanged(object sender, RoutedEventArgs e)
        {
            if (pswdCurrentPassword.Password.Length >= 4 && pswdNewPassword.Password.Length >= 4 && pswdConfirmPassword.Password.Length >= 4)
                btnSubmit.IsEnabled = true;
            else
                btnSubmit.IsEnabled = false;
        }

        private void pswdCurrentPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pswdCurrentPassword.SelectAll();
        }

        private void pswdNewPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pswdNewPassword.SelectAll();
        }

        private void pswdConfirmPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pswdConfirmPassword.SelectAll();
        }

        private void windowAdminChangePassword_Closing(object sender, CancelEventArgs e)
        {
            RefToAdminWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}