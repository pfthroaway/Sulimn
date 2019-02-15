using Sulimn.Classes;
using Sulimn.Classes.Database;
using Sulimn.Views.Admin;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn.Views
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow
    {
        /// <summary>Updates the current theme.</summary>
        /// <param name="theme">Theme name</param>
        /// <param name="update">Write to database?</param>
        private async void UpdateTheme(string theme, bool update = true)
        {
            Application.Current.Resources.Source =
                new Uri($"pack://application:,,,/Extensions;component/Dictionaries/{theme}.xaml",
                    UriKind.RelativeOrAbsolute);
            MainFrame.Style = (Style)FindResource(typeof(Frame));
            Page newPage = MainFrame.Content as Page;
            if (newPage != null)
                newPage.Style = (Style)FindResource("PageStyle");
            Style = (Style)FindResource("WindowStyle");
            Menu.Style = (Style)FindResource(typeof(Menu));
            switch (theme)
            {
                case "Dark":
                    MnuOptionsChangeThemeDark.IsChecked = true;
                    MnuOptionsChangeThemeDefault.IsChecked = false;
                    MnuOptionsChangeThemeGrey.IsChecked = false;
                    break;

                case "Grey":
                    MnuOptionsChangeThemeDark.IsChecked = false;
                    MnuOptionsChangeThemeDefault.IsChecked = false;
                    MnuOptionsChangeThemeGrey.IsChecked = true;
                    break;

                case "Default":
                    MnuOptionsChangeThemeDark.IsChecked = false;
                    MnuOptionsChangeThemeDefault.IsChecked = true;
                    MnuOptionsChangeThemeGrey.IsChecked = false;
                    break;

                default:

                    MnuOptionsChangeThemeDark.IsChecked = false;
                    MnuOptionsChangeThemeDefault.IsChecked = false;
                    MnuOptionsChangeThemeGrey.IsChecked = false;
                    break;
            }

            if (update)
                await GameState.ChangeTheme(theme);
        }

        #region Menu Click

        private void MnuAdmin_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdminPasswordPage());
            MnuAdmin.IsEnabled = false;
        }

        private void MnuFileExit_Click(object sender, RoutedEventArgs e) => Close();

        private void MnuOptionsChangeThemeDark_Click(object sender, RoutedEventArgs e) => UpdateTheme("Dark");

        private void MnuOptionsChangeThemeGrey_Click(object sender, RoutedEventArgs e) => UpdateTheme("Grey");

        private void MnuOptionsChangeThemeDefault_Click(object sender, RoutedEventArgs e) => UpdateTheme("Default");

        #endregion Menu Click

        #region Window-Manipulation Methods

        public MainWindow() => InitializeComponent();

        private async void WindowMain_Loaded(object sender, RoutedEventArgs e)
        {
            GameState.MainWindow = this;
            GameState.VerifyFolders();
            //await GameState.LoadAll();
            //XMLInteraction.WriteAll();
            UpdateTheme(await GameState.LoadTheme(), false);
        }

        #endregion Window-Manipulation Methods
    }
}