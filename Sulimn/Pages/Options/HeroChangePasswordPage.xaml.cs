using Extensions;
using Extensions.Encryption;
using Sulimn.Classes;
using System.Windows;

namespace Sulimn.Pages.Options
{
    /// <summary>Interaction logic for HeroChangePasswordPage.xaml</summary>
    public partial class HeroChangePasswordPage
    {
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
                            GameState.DisplayNotification("Successfully changed password.", "Sulimn");
                            ClosePage();
                        }
                        else
                            GameState.DisplayNotification("The new password can't be the same as the current password.", "Sulimn");
                    else
                        GameState.DisplayNotification("Please ensure the new passwords match.", "Sulimn");
                else
                    GameState.DisplayNotification("Your password must be at least 4 characters.", "Sulimn");
            else
                GameState.DisplayNotification("Invalid current password.", "Sulimn");
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
        }

        public HeroChangePasswordPage()
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

        #endregion Page-Manipulation Methods

        private void HeroChangePasswordPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameState.CalculateScale(Grid);
        }
    }
}