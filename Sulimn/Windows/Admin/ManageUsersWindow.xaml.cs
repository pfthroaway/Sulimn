using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>Interaction logic for ManageUsersWindow.xaml</summary>
    public partial class ManageUsersWindow : INotifyPropertyChanged
    {
        private readonly List<Hero> _allHeroes = new List<Hero>(GameState.AllHeroes);
        private Hero _selectedHero = new Hero();
        internal AdminWindow RefToAdminWindow { private get; set; }

        #region Data Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data Binding

        #region Button Manipulation

        /// <summary>Toggles certain buttons.</summary>
        private void ToggleButtons(bool enabled)
        {
            btnManageUser.IsEnabled = enabled;
            btnDeleteUser.IsEnabled = enabled;
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
            lstUsers.ItemsSource = _allHeroes;
        }

        private void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToggleButtons(lstUsers.SelectedIndex >= 0);
        }

        private void windowManageUsers_Closing(object sender, CancelEventArgs e)
        {
            RefToAdminWindow.Show();
        }

        #endregion Window Manipulation Methods
    }
}