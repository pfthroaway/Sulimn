using Sulimn.Classes;
using Sulimn.Pages.Admin;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn.Pages
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow
    {
        #region ScaleValue Depdency Property

        public static readonly DependencyProperty ScaleValueProperty = DependencyProperty.Register("ScaleValue", typeof(double), typeof(MainWindow), new UIPropertyMetadata(1.0, new PropertyChangedCallback(OnScaleValueChanged), new CoerceValueCallback(OnCoerceScaleValue)));

        private static object OnCoerceScaleValue(DependencyObject o, object value)
        {
            MainWindow mainWindow = o as MainWindow;
            return mainWindow?.OnCoerceScaleValue((double)value) ?? value;
        }

        private static void OnScaleValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            MainWindow mainWindow = o as MainWindow;
            mainWindow?.OnScaleValueChanged((double)e.OldValue, (double)e.NewValue);
        }

        protected virtual double OnCoerceScaleValue(double value)
        {
            if (double.IsNaN(value))
                return 1.0f;

            value = Math.Max(0.1, value);
            return value;
        }

        protected virtual void OnScaleValueChanged(double oldValue, double newValue)
        {
        }

        public double ScaleValue
        {
            get => (double)GetValue(ScaleValueProperty);
            set => SetValue(ScaleValueProperty, value);
        }

        #endregion ScaleValue Depdency Property

        /// <summary>Calculates the scale for the Window.</summary>
        internal void CalculateScale()
        {
            double yScale = ActualHeight / GameState.CurrentPageHeight;
            double xScale = ActualWidth / GameState.CurrentPageWidth;
            double value = Math.Min(xScale, yScale) * 0.8;
            if (value > 3)
                value = 3;
            else if (value < 1)
                value = 1;
            ScaleValue = (double)OnCoerceScaleValue(WindowMain, value);
        }

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
            UpdateTheme(await GameState.LoadTheme(), false);
            await GameState.LoadAll();
        }

        private void MainFrame_OnSizeChanged(object sender, SizeChangedEventArgs e) => CalculateScale();

        #endregion Window-Manipulation Methods
    }
}