using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Sulimn_WPF
{
    /// <summary>
    /// Interaction logic for ManageUserWindow.xaml
    /// </summary>
    public partial class ManageUserWindow : Window, INotifyPropertyChanged
    {
        private Hero modifyHero = new Hero();
        private Hero originalHero = new Hero();
        private List<Weapon> AllWeapons = new List<Weapon>(GameState.GetItemsOfType<Weapon>());
        private List<Armor> AllHeadArmor = new List<Armor>(GameState.GetItemsOfType<Armor>().Where(armr => armr.ArmorType == ArmorTypes.Head));
        private List<Armor> AllBodyArmor = new List<Armor>(GameState.GetItemsOfType<Armor>().Where(armr => armr.ArmorType == ArmorTypes.Body));
        private List<Armor> AllLegsArmor = new List<Armor>(GameState.GetItemsOfType<Armor>().Where(armr => armr.ArmorType == ArmorTypes.Legs));
        private List<Armor> AllFeetArmor = new List<Armor>(GameState.GetItemsOfType<Armor>().Where(armr => armr.ArmorType == ArmorTypes.Feet));
        private List<HeroClass> AllClasses = new List<HeroClass>(GameState.AllClasses);

        internal ManageUsersWindow RefToManageUsersWindow { get; set; }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Binds information to controls.
        /// </summary>
        private void BindControls()
        {
            cmbClass.ItemsSource = AllClasses;
            cmbWeapon.ItemsSource = AllWeapons;
            cmbHead.ItemsSource = AllHeadArmor;
            cmbBody.ItemsSource = AllBodyArmor;
            cmbLegs.ItemsSource = AllLegsArmor;
            cmbFeet.ItemsSource = AllFeetArmor;

            DisplayOriginalHero();
        }

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Display Manipulation

        /// <summary>
        /// Displays the Hero as it was when the Window was loaded.
        /// </summary>
        private void DisplayOriginalHero()
        {
            txtHeroName.Text = originalHero.Name;
            txtLevel.Text = originalHero.Level.ToString();
            txtExperience.Text = originalHero.Experience.ToString();
            txtSkillPoints.Text = originalHero.SkillPoints.ToString();
            txtStrength.Text = originalHero.Strength.ToString();
            txtVitality.Text = originalHero.Vitality.ToString();
            txtDexterity.Text = originalHero.Dexterity.ToString();
            txtWisdom.Text = originalHero.Wisdom.ToString();
            txtGold.Text = originalHero.Gold.ToString();
            txtCurrentHealth.Text = originalHero.CurrentHealth.ToString();
            txtMaximumHealth.Text = originalHero.MaximumHealth.ToString();
            txtCurrentMagic.Text = originalHero.CurrentMagic.ToString();
            txtMaximumMagic.Text = originalHero.MaximumMagic.ToString();
            cmbHead.SelectedValue = originalHero.Head;
            cmbBody.SelectedValue = originalHero.Body;
            cmbLegs.SelectedValue = originalHero.Legs;
            cmbFeet.SelectedValue = originalHero.Feet;
            cmbWeapon.SelectedValue = originalHero.Weapon;
            cmbClass.SelectedValue = originalHero.ClassName;
        }

        #endregion Display Manipulation

        #region Input Manipulation

        private void txtHeroName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;

            List<bool> keys = Functions.GetListOfKeys(Key.Back, Key.Delete, Key.Home, Key.End, Key.LeftShift, Key.RightShift, Key.Enter, Key.Tab, Key.LeftAlt, Key.RightAlt, Key.Left, Key.Right, Key.LeftCtrl, Key.RightCtrl);

            if (keys.Any(key => key == true) || Key.A <= k && k <= Key.Z)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;

            List<bool> keys = Functions.GetListOfKeys(Key.Back, Key.Delete, Key.Home, Key.End, Key.LeftShift, Key.RightShift, Key.Enter, Key.Tab, Key.LeftAlt, Key.RightAlt, Key.Left, Key.Right, Key.LeftCtrl, Key.RightCtrl);

            if (keys.Any(key => key == true) || (Key.D0 <= k && k <= Key.D9) || (Key.NumPad0 <= k && k <= Key.NumPad9))
                e.Handled = false;
            else
                e.Handled = true;
        }

        #endregion Input Manipulation

        #region Button-Click Methods

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            DisplayOriginalHero();
            modifyHero = new Hero(originalHero);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
        }

        internal void LoadWindow(Hero manageHero)
        {
            modifyHero = new Hero(manageHero);
            originalHero = new Hero(manageHero);
            BindControls();
        }

        public ManageUserWindow()
        {
            InitializeComponent();
        }

        private void windowManageUsers_Closing(object sender, CancelEventArgs e)
        {
            RefToManageUsersWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}