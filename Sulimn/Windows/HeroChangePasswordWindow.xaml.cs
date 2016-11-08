using System.ComponentModel;
using System.Windows;

namespace Sulimn_WPF
{
    /// <summary>
    /// Interaction logic for HeroChangePasswordWindow.xaml
    /// </summary>
    public partial class HeroChangePasswordWindow : Window
    {
        internal CityWindow RefToCityWindow { get; set; }

        #region Button-Click Methods

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordHash.ValidatePassword(pswdCurrentPassword.Password, GameState.CurrentHero.Password))
            {
                if (pswdNewPassword.Password.Length >= 4 && pswdConfirmPassword.Password.Length >= 4)
                {
                    if (pswdNewPassword.Password == pswdConfirmPassword.Password)
                    {
                        if (pswdCurrentPassword.Password != pswdNewPassword.Password)
                        {
                            GameState.CurrentHero.Password = PasswordHash.HashPassword(pswdNewPassword.Password);
                            GameState.SaveHeroPassword(GameState.CurrentHero);
                            MessageBox.Show("Successfully changed password.", "Sulimn", MessageBoxButton.OK);
                            CloseWindow();
                        }
                        else
                            MessageBox.Show("The new password can't be the same as the current password.", "Sulimn", MessageBoxButton.OK);
                    }
                    else
                        MessageBox.Show("Please ensure the new passwords match.", "Sulimn", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("Your password must be at least 4 characters.", "Sulimn", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Invalid current password.", "Sulimn", MessageBoxButton.OK);
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

        public HeroChangePasswordWindow()
        {
            InitializeComponent();
            pswdCurrentPassword.Focus();
        }

        private void pswdChanged(object sender, RoutedEventArgs e)
        {
            if (pswdCurrentPassword.Password.Length >= 1 && pswdNewPassword.Password.Length >= 1 && pswdConfirmPassword.Password.Length >= 1)
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

        private void windowHeroChangePassword_Closing(object sender, CancelEventArgs e)
        {
            RefToCityWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}