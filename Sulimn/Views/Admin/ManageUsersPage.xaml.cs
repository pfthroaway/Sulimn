using Sulimn.Classes;
using Sulimn.Classes.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn.Views.Admin
{
    /// <summary>Interaction logic for ManageUsersPage.xaml</summary>
    public partial class ManageUsersPage : INotifyPropertyChanged
    {
        private readonly List<Hero> _allHeroes = new List<Hero>(GameState.AllHeroes);

        #region Data Binding

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(property));

        #endregion Data Binding

        #region Button Manipulation

        /// <summary>Toggles certain buttons.</summary>
        /// <param name="enabled"></param>
        private void ToggleButtons(bool enabled)
        {
            BtnManageUser.IsEnabled = enabled;
            BtnDeleteUser.IsEnabled = enabled;
        }

        #endregion Button Manipulation

        /// <summary>Refreshed the LstUsers ItemSource.</summary>
        internal void RefreshItemsSource()
        {
            LstUsers.ItemsSource = _allHeroes;
            LstUsers.Items.Refresh();
        }

        #region Button-Click Methods

        private void BtnNewUser_Click(object sender, RoutedEventArgs e) => GameState.Navigate(new AdminNewUserPage());

        private async void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            Hero selectedHero = (Hero)LstUsers.SelectedItem;
            if (GameState.YesNoNotification(
                $"Are you sure you want to delete {selectedHero.Name}? This action cannot be undone.", "Sulimn"))
                if (await GameState.DeleteHero(selectedHero))
                    RefreshItemsSource();
        }

        private void BtnManageUser_Click(object sender, RoutedEventArgs e)
        {
            ManageUserPage manageUserPage = new ManageUserPage { PreviousPage = this };
            manageUserPage.LoadPage((Hero)LstUsers.SelectedItem);
            GameState.Navigate(manageUserPage);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => ClosePage();

        #endregion Button-Click Methods

        #region Page Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage() => GameState.GoBack();

        public ManageUsersPage() => InitializeComponent();

        private void ManageUsersPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            
            RefreshItemsSource();
        }

        private void LstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e) => ToggleButtons(
            LstUsers.SelectedIndex >= 0);

        #endregion Page Manipulation Methods
    }
}