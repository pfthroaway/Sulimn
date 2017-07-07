using Extensions;
using Extensions.Enums;
using Sulimn.Classes;
using Sulimn.Classes.Entities;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sulimn.Windows.Admin
{
    /// <summary>
    /// Interaction logic for ManageUserWindow.xaml
    /// </summary>
    public partial class ManageUserWindow : INotifyPropertyChanged
    {
        private readonly List<BodyArmor> _allBodyArmor = GameState.GetItemsOfType<BodyArmor>();
        private readonly List<HeroClass> _allClasses = new List<HeroClass>(GameState.AllClasses);
        private readonly List<FeetArmor> _allFeetArmor = GameState.GetItemsOfType<FeetArmor>();
        private readonly List<HeadArmor> _allHeadArmor = GameState.GetItemsOfType<HeadArmor>();
        private readonly List<LegArmor> _allLegsArmor = GameState.GetItemsOfType<LegArmor>();
        private readonly List<Weapon> _allWeapons = new List<Weapon>(GameState.GetItemsOfType<Weapon>());
        private Hero _modifyHero = new Hero();
        private Hero _originalHero = new Hero();

        internal ManageUsersWindow RefToManageUsersWindow { private get; set; }

        #region Display Manipulation

        /// <summary>
        /// Displays the Hero as it was when the Window was loaded.
        /// </summary>
        private void DisplayOriginalHero()
        {
            TxtHeroName.Text = _originalHero.Name;
            TxtLevel.Text = _originalHero.Level.ToString();
            TxtExperience.Text = _originalHero.Experience.ToString();
            TxtSkillPoints.Text = _originalHero.SkillPoints.ToString();
            TxtStrength.Text = _originalHero.Attributes.Strength.ToString();
            TxtVitality.Text = _originalHero.Attributes.Vitality.ToString();
            TxtDexterity.Text = _originalHero.Attributes.Dexterity.ToString();
            TxtWisdom.Text = _originalHero.Attributes.Wisdom.ToString();
            TxtGold.Text = _originalHero.Inventory.Gold.ToString();
            TxtCurrentHealth.Text = _originalHero.Statistics.CurrentHealth.ToString();
            TxtMaximumHealth.Text = _originalHero.Statistics.MaximumHealth.ToString();
            TxtCurrentMagic.Text = _originalHero.Statistics.CurrentMagic.ToString();
            TxtMaximumMagic.Text = _originalHero.Statistics.MaximumMagic.ToString();
            CmbHead.SelectedValue = _originalHero.Equipment.Head;
            CmbBody.SelectedValue = _originalHero.Equipment.Body;
            CmbLegs.SelectedValue = _originalHero.Equipment.Legs;
            CmbFeet.SelectedValue = _originalHero.Equipment.Feet;
            CmbWeapon.SelectedValue = _originalHero.Equipment.Weapon;
            CmbClass.SelectedValue = _originalHero.Class;
        }

        #endregion Display Manipulation

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Binds information to controls.
        /// </summary>
        private void BindControls()
        {
            CmbClass.ItemsSource = _allClasses;
            CmbWeapon.ItemsSource = _allWeapons;
            CmbHead.ItemsSource = _allHeadArmor;
            CmbBody.ItemsSource = _allBodyArmor;
            CmbLegs.ItemsSource = _allLegsArmor;
            CmbFeet.ItemsSource = _allFeetArmor;

            DisplayOriginalHero();
        }

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Input Manipulation

        private void TxtHeroName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Functions.PreviewKeyDown(e, KeyType.Letters);
        }

        private void TxtNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Functions.PreviewKeyDown(e, KeyType.Integers);
        }

        #endregion Input Manipulation

        #region Button-Click Methods

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            DisplayOriginalHero();
            _modifyHero = new Hero(_originalHero);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
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
            _modifyHero = new Hero(manageHero);
            _originalHero = new Hero(manageHero);
            BindControls();
        }

        public ManageUserWindow()
        {
            InitializeComponent();
        }

        private void TxtHeroName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Functions.TextBoxTextChanged(sender, KeyType.Letters);
            TxtHeroName.CaretIndex = TxtHeroName.Text.Length;
        }

        private void WindowManageUsers_Closing(object sender, CancelEventArgs e)
        {
            RefToManageUsersWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}