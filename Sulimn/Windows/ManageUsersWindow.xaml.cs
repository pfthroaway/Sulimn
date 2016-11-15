﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for ManageUsersWindow.xaml
    /// </summary>
    public partial class ManageUsersWindow : Window, INotifyPropertyChanged
    {
        private Hero selectedHero = new Hero();

        internal AdminWindow RefToAdminWindow { get; set; }

        #region Data Binding

        internal void BindLabels()
        {
            DataContext = selectedHero;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data Binding

        #region Button-Click Methods

        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnManageUser_Click(object sender, RoutedEventArgs e)
        {
            ManageUserWindow manageUserWindow = new ManageUserWindow();
            manageUserWindow.RefToManageUsersWindow = this;
            manageUserWindow.LoadWindow((Hero)lstUsers.SelectedValue);
            manageUserWindow.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
        }

        public ManageUsersWindow()
        {
            InitializeComponent();
        }

        private void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void windowManageUsers_Closing(object sender, CancelEventArgs e)
        {
            RefToAdminWindow.Show();
        }

        #endregion Window Manipulation Methods
    }
}