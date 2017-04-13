using Extensions;
using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>Interaction logic for AdminPasswordWindow.xaml</summary>
    public partial class AdminPasswordWindow
    {
        private bool _admin;
        internal MainWindow RefToMainWindow { private get; set; }

        #region Button-Click Methods

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordHash.ValidatePassword(PswdAdmin.Password, GameState.AdminPassword))
            {
                _admin = true;
                CloseWindow();
            }
            else
            {
                GameState.DisplayNotification("Invalid login.", "Sulimn", NotificationButtons.OK, this);
                PswdAdmin.SelectAll();
            }
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

        public AdminPasswordWindow()
        {
            InitializeComponent();
            PswdAdmin.Focus();
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            BtnSubmit.IsEnabled = PswdAdmin.Password.Length > 0;
        }

        private void WindowAdminPassword_Closing(object sender, CancelEventArgs e)
        {
            if (!_admin)
                RefToMainWindow.Show();
            else
            {
                AdminWindow adminWindow = new AdminWindow { RefToMainWindow = RefToMainWindow };
                adminWindow.Show();
            }
        }

        #endregion Window-Manipulation Methods
    }
}