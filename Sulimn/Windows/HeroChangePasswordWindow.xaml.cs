﻿using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>Interaction logic for HeroChangePasswordWindow.xaml</summary>
    public partial class HeroChangePasswordWindow
    {
        internal CityWindow RefToCityWindow { private get; set; }

        #region Button-Click Methods

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordHash.ValidatePassword(pswdCurrentPassword.Password, GameState.CurrentHero.Password))
                if (pswdNewPassword.Password.Length >= 4 && pswdConfirmPassword.Password.Length >= 4)
                    if (pswdNewPassword.Password == pswdConfirmPassword.Password)
                        if (pswdCurrentPassword.Password != pswdNewPassword.Password)
                        {
                            GameState.CurrentHero.Password = PasswordHash.HashPassword(pswdNewPassword.Password);
                            GameState.SaveHeroPassword(GameState.CurrentHero);
                            new Notification("Successfully changed password.", "Sulimn", NotificationButtons.OK, this).ShowDialog();
                            CloseWindow();
                        }
                        else
                        {
                            new Notification("The new password can't be the same as the current password.", "Sulimn",
                            NotificationButtons.OK, this).ShowDialog();
                        }
                    else
                        new Notification("Please ensure the new passwords match.", "Sulimn", NotificationButtons.OK, this).ShowDialog();
                else
                    new Notification("Your password must be at least 4 characters.", "Sulimn", NotificationButtons.OK, this).ShowDialog();
            else
                new Notification("Invalid current password.", "Sulimn", NotificationButtons.OK, this).ShowDialog();
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

        public HeroChangePasswordWindow()
        {
            InitializeComponent();
            pswdCurrentPassword.Focus();
        }

        private void PswdChanged(object sender, RoutedEventArgs e)
        {
            btnSubmit.IsEnabled = pswdCurrentPassword.Password.Length > 0 && pswdNewPassword.Password.Length > 0 &&
                                  pswdConfirmPassword.Password.Length > 0;
        }

        private void pswd_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.PasswordBoxGotFocus(sender);
        }

        private void windowHeroChangePassword_Closing(object sender, CancelEventArgs e)
        {
            RefToCityWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}