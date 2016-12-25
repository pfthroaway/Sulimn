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

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordHash.ValidatePassword(pswdAdmin.Password, GameState.AdminPassword))
            {
                _admin = true;
                CloseWindow();
            }
            else
            {
                new Notification("Invalid login.", "Sulimn", NotificationButtons.OK, this).ShowDialog();
                pswdAdmin.SelectAll();
            }
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

        public AdminPasswordWindow()
        {
            InitializeComponent();
            pswdAdmin.Focus();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            btnSubmit.IsEnabled = pswdAdmin.Password.Length > 0;
        }

        private void windowAdminPassword_Closing(object sender, CancelEventArgs e)
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