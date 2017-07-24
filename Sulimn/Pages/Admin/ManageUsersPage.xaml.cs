using Sulimn.Classes;
using Sulimn.Classes.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn.Pages.Admin
{
    /// <summary>Interaction logic for ManageUsersPage.xaml</summary>
    public partial class ManageUsersPage : INotifyPropertyChanged
    {
        private readonly List<Hero> _allHeroes = new List<Hero>(GameState.AllHeroes);
        private Hero _selectedHero = new Hero();

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

        internal void RefreshItemsSource()
        {
            LstUsers.ItemsSource = _allHeroes;
            LstUsers.Items.Refresh();
        }

        #region Button-Click Methods

        private void BtnNewUser_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnManageUser_Click(object sender, RoutedEventArgs e)
        {
            ManageUserPage manageUserPage = new ManageUserPage { PreviousPage = this };
            manageUserPage.LoadPage((Hero)LstUsers.SelectedValue);
            GameState.MainWindow.MainFrame.Navigate(manageUserPage);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }

        #endregion Button-Click Methods

        #region Page Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage()
        {
            GameState.MainWindow.MainFrame.GoBack();
        }

        public ManageUsersPage()
        {
            InitializeComponent();
        }

        private void LstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToggleButtons(LstUsers.SelectedIndex >= 0);
        }

        #endregion Page Manipulation Methods

        private void ManageUsersPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameState.CalculateScale(Grid);
        }
    }
}