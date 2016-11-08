using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sulimn_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Login

        /// <summary>
        /// Logs the Hero in.
        /// </summary>
        /// <param name="Hero">Current Hero</param>
        internal void Login()
        {
            txtHeroName.Text = "";
            pswdPassword.Password = "";
            txtHeroName.Focus();
            CityWindow cityWindow = new CityWindow();
            cityWindow.Show();
            cityWindow.RefToMainWindow = this;
            cityWindow.EnterCity();
            this.Visibility = Visibility.Hidden;
        }

        #endregion Login

        #region Button-Click Methods

        // This method opens the newPlayer form.
        private void btnNewHero_Click(object sender, RoutedEventArgs e)
        {
            NewHeroWindow newHeroWindow = new NewHeroWindow();
            newHeroWindow.Show();
            newHeroWindow.RefToMainWindow = this;
            this.Visibility = Visibility.Hidden;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CheckLogin(txtHeroName.Text.Trim(), pswdPassword.Password.Trim()))
                Login();
        }

        private void mnuAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminPasswordWindow adminPasswordWindow = new AdminPasswordWindow();
            adminPasswordWindow.Show();
            adminPasswordWindow.RefToMainWindow = this;
            this.Visibility = Visibility.Hidden;
        }

        private void mnuFileExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        public MainWindow()
        {
            InitializeComponent();
            txtHeroName.Focus();
        }

        private void txtHeroName_GotFocus(object sender, RoutedEventArgs e)
        {
            txtHeroName.SelectAll();
        }

        private void pswdPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pswdPassword.SelectAll();
        }

        private void txtHeroName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;

            List<bool> keys = Functions.GetListOfKeys(Key.Back, Key.Delete, Key.Home, Key.End, Key.LeftShift, Key.RightShift, Key.Enter, Key.Tab, Key.LeftAlt, Key.RightAlt, Key.Left, Key.Right, Key.LeftCtrl, Key.RightCtrl, Key.Escape);

            if (keys.Any(key => key == true) || Key.A <= k && k <= Key.Z)
                //&& !(Key.D0 <= k && k <= Key.D9) && !(Key.NumPad0 <= k && k <= Key.NumPad9))
                //|| k == Key.OemMinus || k == Key.Subtract || k == Key.Decimal || k == Key.OemPeriod)
                e.Handled = false;
            else
                e.Handled = true;

            //System.Media.SystemSound ss = System.Media.SystemSounds.Beep;
            //ss.Play();
        }

        /// <summary>
        /// Manages TextBox/PasswordBox text being changed.
        /// </summary>
        private void TextChanged()
        {
            if (txtHeroName.Text.Length > 0 && pswdPassword.Password.Length > 0)
                btnLogin.IsEnabled = true;
            else
                btnLogin.IsEnabled = false;
        }

        private void txtHeroName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged();
        }

        private void pswdPassword_TextChanged(object sender, RoutedEventArgs e)
        {
            TextChanged();
        }

        private async void windowMain_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Factory.StartNew(() => GameState.LoadAll());
        }

        #endregion Window-Manipulation Methods
    }
}