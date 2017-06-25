using Extensions;
using Extensions.Encryption;
using Sulimn.Classes;
using System.ComponentModel;
using System.Windows;

namespace Sulimn.Windows.Options
{
    /// <summary>Interaction logic for HeroChangePasswordWindow.xaml</summary>
    public partial class HeroChangePasswordWindow
    {
        internal Exploration.CityWindow RefToCityWindow { private get; set; }

        #region Button-Click Methods

        private async void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (Argon2.ValidatePassword(GameState.CurrentHero.Password, PswdCurrentPassword.Password))
                if (PswdNewPassword.Password.Length >= 4 && PswdConfirmPassword.Password.Length >= 4)
                    if (PswdNewPassword.Password == PswdConfirmPassword.Password)
                        if (PswdCurrentPassword.Password != PswdNewPassword.Password)
                        {
                            GameState.CurrentHero.Password = Argon2.HashPassword(PswdNewPassword.Password);
                            await GameState.SaveHeroPassword(GameState.CurrentHero);
                            GameState.DisplayNotification("Successfully changed password.", "Sulimn", this);
                            CloseWindow();
                        }
                        else
                        {
                            GameState.DisplayNotification("The new password can't be the same as the current password.", "Sulimn",
                            this);
                        }
                    else
                        GameState.DisplayNotification("Please ensure the new passwords match.", "Sulimn", this);
                else
                    GameState.DisplayNotification("Your password must be at least 4 characters.", "Sulimn", this);
            else
                GameState.DisplayNotification("Invalid current password.", "Sulimn", this);
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

        public HeroChangePasswordWindow()
        {
            InitializeComponent();
            PswdCurrentPassword.Focus();
        }

        private void PswdChanged(object sender, RoutedEventArgs e)
        {
            BtnSubmit.IsEnabled = PswdCurrentPassword.Password.Length > 0 && PswdNewPassword.Password.Length > 0 &&
            PswdConfirmPassword.Password.Length > 0;
        }

        private void Pswd_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.PasswordBoxGotFocus(sender);
        }

        private void WindowHeroChangePassword_Closing(object sender, CancelEventArgs e)
        {
            RefToCityWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}