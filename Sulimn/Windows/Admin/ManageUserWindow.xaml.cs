using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for ManageUserWindow.xaml
    /// </summary>
    public partial class ManageUserWindow : INotifyPropertyChanged
    {
        private readonly List<BodyArmor> AllBodyArmor = GameState.GetItemsOfType<BodyArmor>();
        private readonly List<HeroClass> AllClasses = new List<HeroClass>(GameState.AllClasses);
        private readonly List<FeetArmor> AllFeetArmor = GameState.GetItemsOfType<FeetArmor>();
        private readonly List<HeadArmor> AllHeadArmor = GameState.GetItemsOfType<HeadArmor>();
        private readonly List<LegArmor> AllLegsArmor = GameState.GetItemsOfType<LegArmor>();
        private readonly List<Weapon> AllWeapons = new List<Weapon>(GameState.GetItemsOfType<Weapon>());
        private Hero modifyHero = new Hero();
        private Hero originalHero = new Hero();

        internal ManageUsersWindow RefToManageUsersWindow { get; set; }

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
            txtStrength.Text = originalHero.Attributes.Strength.ToString();
            txtVitality.Text = originalHero.Attributes.Vitality.ToString();
            txtDexterity.Text = originalHero.Attributes.Dexterity.ToString();
            txtWisdom.Text = originalHero.Attributes.Wisdom.ToString();
            txtGold.Text = originalHero.Inventory.Gold.ToString();
            txtCurrentHealth.Text = originalHero.Statistics.CurrentHealth.ToString();
            txtMaximumHealth.Text = originalHero.Statistics.MaximumHealth.ToString();
            txtCurrentMagic.Text = originalHero.Statistics.CurrentMagic.ToString();
            txtMaximumMagic.Text = originalHero.Statistics.MaximumMagic.ToString();
            cmbHead.SelectedValue = originalHero.Equipment.Head;
            cmbBody.SelectedValue = originalHero.Equipment.Body;
            cmbLegs.SelectedValue = originalHero.Equipment.Legs;
            cmbFeet.SelectedValue = originalHero.Equipment.Feet;
            cmbWeapon.SelectedValue = originalHero.Equipment.Weapon;
            cmbClass.SelectedValue = originalHero.Class;
        }

        #endregion Display Manipulation

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

        #region Input Manipulation

        private void txtHeroName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;

            List<bool> keys = Functions.GetListOfKeys(Key.Back, Key.Delete, Key.Home, Key.End, Key.LeftShift, Key.RightShift,
            Key.Enter, Key.Tab, Key.LeftAlt, Key.RightAlt, Key.Left, Key.Right, Key.LeftCtrl, Key.RightCtrl,
            Key.Escape);

            if (keys.Any(key => key) || Key.A <= k && k <= Key.Z)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;

            List<bool> keys = Functions.GetListOfKeys(Key.Back, Key.Delete, Key.Home, Key.End, Key.LeftShift, Key.RightShift,
            Key.Enter, Key.Tab, Key.LeftAlt, Key.RightAlt, Key.Left, Key.Right, Key.LeftCtrl, Key.RightCtrl,
            Key.Escape);

            if (keys.Any(key => key) || Key.D0 <= k && k <= Key.D9 || Key.NumPad0 <= k && k <= Key.NumPad9)
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
            Close();
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

        private void txtHeroName_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtHeroName.Text = new string((from c in txtHeroName.Text
                                           where char.IsLetter(c)
                                           select c).ToArray());
            txtHeroName.CaretIndex = txtHeroName.Text.Length;
        }

        private void windowManageUsers_Closing(object sender, CancelEventArgs e)
        {
            RefToManageUsersWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}