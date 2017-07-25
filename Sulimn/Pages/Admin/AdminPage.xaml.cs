using Sulimn.Classes;
using System.Windows;

namespace Sulimn.Pages.Admin
{
    /// <summary>Interaction logic for AdminPage.xaml</summary>
    public partial class AdminPage
    {
        #region Button-Click Methods

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }

        private void BtnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            ManageUsersPage manageUsersPage = new ManageUsersPage();
            manageUsersPage.RefreshItemsSource();
            GameState.Navigate(manageUsersPage);
        }

        private void BtnChangeAdminPassword_Click(object sender, RoutedEventArgs e)
        {
            GameState.Navigate(new AdminChangePasswordPage());
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

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private static void ClosePage()
        {
            GameState.MainWindow.MainFrame.RemoveBackEntry();
            GameState.GoBack();
            GameState.MainWindow.MnuAdmin.IsEnabled = true;
        }

        public AdminPage()
        {
            InitializeComponent();
        }

        #endregion Page-Manipulation Methods

        private void AdminPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameState.CalculateScale(Grid);
        }
    }
}