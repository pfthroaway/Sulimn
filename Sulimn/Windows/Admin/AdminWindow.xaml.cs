using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>Interaction logic for AdminWindow.xaml</summary>
    public partial class AdminWindow
    {
        internal MainWindow RefToMainWindow { private get; set; }

        #region Button-Click Methods

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void BtnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            ManageUsersWindow manageUsersWindow = new ManageUsersWindow { RefToAdminWindow = this };
            manageUsersWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnChangeAdminPassword_Click(object sender, RoutedEventArgs e)
        {
            AdminChangePasswordWindow adminChangePasswordWindow = new AdminChangePasswordWindow
            {
                RefToAdminWindow = this
            };
            adminChangePasswordWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnManageArmor_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnManageEnemies_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnManageFood_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnManageHeroClass_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnManagePotion_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnManageWeapons_Click(object sender, RoutedEventArgs e)
        {
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public AdminWindow()
        {
            InitializeComponent();
        }

        private void WindowAdmin_Closing(object sender, CancelEventArgs e)
        {
            RefToMainWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}