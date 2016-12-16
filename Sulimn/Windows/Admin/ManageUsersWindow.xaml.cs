using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for ManageUsersWindow.xaml
    /// </summary>
    public partial class ManageUsersWindow : INotifyPropertyChanged
    {
        private readonly List<Hero> AllHeroes = new List<Hero>(GameState.AllHeroes);
        private Hero selectedHero = new Hero();
        internal AdminWindow RefToAdminWindow { get; set; }

        #region Data Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data Binding

        #region Button Manipulation

        /// <summary>Enables certain buttons.</summary>
        private void EnableButtons()
        {
            btnManageUser.IsEnabled = true;
            btnDeleteUser.IsEnabled = true;
        }

        /// <summary>Disables certain buttons.</summary>
        private void DisableButtons()
        {
            btnManageUser.IsEnabled = false;
            btnDeleteUser.IsEnabled = false;
        }

        #endregion Button Manipulation

        #region Button-Click Methods

        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnManageUser_Click(object sender, RoutedEventArgs e)
        {
            ManageUserWindow manageUserWindow = new ManageUserWindow { RefToManageUsersWindow = this };
            manageUserWindow.LoadWindow((Hero)lstUsers.SelectedValue);
            manageUserWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public ManageUsersWindow()
        {
            InitializeComponent();
            lstUsers.ItemsSource = AllHeroes;
        }

        private void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUsers.SelectedIndex >= 0)
                EnableButtons();
            else
                DisableButtons();
        }

        private void windowManageUsers_Closing(object sender, CancelEventArgs e)
        {
            RefToAdminWindow.Show();
        }

        #endregion Window Manipulation Methods
    }
}