using Sulimn.Classes;
using Sulimn.Pages.Admin;
using System;
using System.Windows;

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

        internal void LoadAdmin()
        {
            MainFrame.Navigate(new AdminPage());
        }

        private void MnuAdmin_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdminPasswordPage());
        }

        private void MnuFileExit_Click(object sender, RoutedEventArgs e) => Close();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void WindowMain_Loaded(object sender, RoutedEventArgs e)
        {
            GameState.MainWindow = this;
            await GameState.LoadAll();
        }

        private void MnuOptionsChangeThemeDark_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.Source = new Uri("pack://application:,,,/Extensions;component/Dictionaries/Dark.xaml", UriKind.RelativeOrAbsolute);
        }

        private void MnuOptionsChangeThemeGrey_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.Source = new Uri("pack://application:,,,/Extensions;component/Dictionaries/Grey.xaml", UriKind.RelativeOrAbsolute);
        }

        private void MnuOptionsChangeThemeDefault_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.Source = new Uri("pack://application:,,,/Extensions;component/Dictionaries/Default.xaml", UriKind.RelativeOrAbsolute);
        }

        private void MainFrame_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            CalculateScale();
        }
    }
}