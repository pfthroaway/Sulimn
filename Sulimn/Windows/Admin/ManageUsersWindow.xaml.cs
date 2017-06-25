using Sulimn.Classes;
using Sulimn.Classes.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn.Windows.Admin
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
            BtnManageUser.IsEnabled = enabled;
            BtnDeleteUser.IsEnabled = enabled;
        }

        #endregion Button Manipulation

        #region Button-Click Methods

        private void BtnNewUser_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnManageUser_Click(object sender, RoutedEventArgs e)
        {
            ManageUserWindow manageUserWindow = new ManageUserWindow { RefToManageUsersWindow = this };
            manageUserWindow.LoadWindow((Hero)LstUsers.SelectedValue);
            manageUserWindow.Show();
            Visibility = Visibility.Hidden;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
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
            LstUsers.ItemsSource = _allHeroes;
        }

        private void LstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToggleButtons(LstUsers.SelectedIndex >= 0);
        }

        private void WindowManageUsers_Closing(object sender, CancelEventArgs e)
        {
            RefToAdminWindow.Show();
        }

        #endregion Window Manipulation Methods
    }
}