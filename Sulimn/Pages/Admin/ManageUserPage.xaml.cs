using Extensions;
using Extensions.DataTypeHelpers;
using Extensions.Encryption;
using Extensions.Enums;
using Sulimn.Classes;
using Sulimn.Classes.Entities;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sulimn.Pages.Admin
{
    /// <summary>Interaction logic for ManageUserPage.xaml</summary>
    public partial class ManageUserPage : INotifyPropertyChanged
    {
        private Hero _originalHero = new Hero();

        internal ManageUsersPage PreviousPage { get; set; }

        #region Display Manipulation

        /// <summary>Determines if buttons should be enabled.</summary>
        private void CheckInput()
        {
            //bool enabled = TxtHeroName.Text.Length > 0 && CmbClass.SelectedIndex >= 0 && TxtLevel.Text.Length > 0 &&
            //               TxtExperience.Text.Length > 0 && TxtSkillPoints.Text.Length > 0 &&
            //               TxtStrength.Text.Length > 0 && TxtVitality.Text.Length > 0 && TxtDexterity.Text.Length > 0 &&
            //               TxtWisdom.Text.Length > 0 && TxtCurrentHealth.Text.Length > 0 &&
            //               TxtMaximumHealth.Text.Length > 0 && TxtCurrentMagic.Text.Length > 0 &&
            //               TxtMaximumMagic.Text.Length > 0 && TxtGold.Text.Length > 0 && CmbWeapon.SelectedIndex >= 0 &&
            //               CmbHead.SelectedIndex >= 0 && CmbBody.SelectedIndex >= 0 && CmbHands.SelectedIndex >= 0 &&
            //               CmbLegs.SelectedIndex >= 0 && CmbFeet.SelectedIndex >= 0 &&
            //               TxtHeroName.Text != _originalHero.Name |
            //               (HeroClass)CmbClass.SelectedValue != _originalHero.Class |
            //               TxtLevel.Text != _originalHero.Level.ToString() |
            //               TxtExperience.Text != _originalHero.Experience.ToString() |
            //               TxtSkillPoints.Text != _originalHero.SkillPoints.ToString() |
            //               TxtStrength.Text != _originalHero.TotalStrength.ToString() |
            //               TxtVitality.Text != _originalHero.TotalVitality.ToString() |
            //               TxtDexterity.Text != _originalHero.TotalDexterity.ToString() |
            //               TxtWisdom.Text != _originalHero.TotalWisdom.ToString() |
            //               TxtCurrentHealth.Text != _originalHero.Statistics.CurrentHealth.ToString() |
            //               TxtCurrentMagic.Text != _originalHero.Statistics.CurrentMagic.ToString() |
            //               TxtMaximumHealth.Text != _originalHero.Statistics.MaximumHealth.ToString() |
            //               TxtMaximumMagic.Text != _originalHero.Statistics.MaximumMagic.ToString() |
            //               TxtGold.Text != _originalHero.Inventory.Gold.ToString() |
            //               TxtInventory.Text != _originalHero.Inventory.ToString() |
            //               (Weapon)CmbWeapon.SelectedValue != _originalHero.Equipment.Weapon |
            //               (HeadArmor)CmbHead.SelectedValue != _originalHero.Equipment.Head |
            //               (BodyArmor)CmbBody.SelectedValue != _originalHero.Equipment.Body |
            //               (HandArmor)CmbHands.SelectedValue != _originalHero.Equipment.Hands |
            //               (LegArmor)CmbLegs.SelectedValue != _originalHero.Equipment.Legs |
            //               (FeetArmor)CmbFeet.SelectedValue != _originalHero.Equipment.Feet |
            //               (CmbLeftRing.SelectedIndex >= 0 && (Ring)CmbLeftRing.SelectedValue !=
            //                _originalHero.Equipment.LeftRing) |
            //               (CmbRightRing.SelectedIndex >= 0 && (Ring)CmbRightRing.SelectedValue !=
            //                _originalHero.Equipment.RightRing) |
            //               (PswdPassword.Password.Length > 0 && PswdConfirm.Password.Length > 0);
            //BtnSave.IsEnabled = enabled;
            //BtnReset.IsEnabled = enabled;
        }

        /// <summary>Resets input to default values.</summary>
        private void Reset()
        {
            TxtHeroName.Text = _originalHero.Name;
            ChkHardcore.IsChecked = _originalHero.Hardcore;
            TxtLevel.Text = _originalHero.Level.ToString();
            TxtExperience.Text = _originalHero.Experience.ToString();
            TxtSkillPoints.Text = _originalHero.SkillPoints.ToString();
            TxtStrength.Text = _originalHero.TotalStrength.ToString();
            TxtVitality.Text = _originalHero.TotalVitality.ToString();
            TxtDexterity.Text = _originalHero.TotalDexterity.ToString();
            TxtWisdom.Text = _originalHero.TotalWisdom.ToString();
            TxtGold.Text = _originalHero.Inventory.Gold.ToString();
            TxtInventory.Text = _originalHero.Inventory.ToString();
            TxtCurrentHealth.Text = _originalHero.Statistics.CurrentHealth.ToString();
            TxtMaximumHealth.Text = _originalHero.Statistics.MaximumHealth.ToString();
            TxtCurrentMagic.Text = _originalHero.Statistics.CurrentMagic.ToString();
            TxtMaximumMagic.Text = _originalHero.Statistics.MaximumMagic.ToString();
            CmbHead.SelectedValue = _originalHero.Equipment.Head;
            CmbBody.SelectedValue = _originalHero.Equipment.Body;
            CmbHands.SelectedValue = _originalHero.Equipment.Hands;
            CmbLegs.SelectedValue = _originalHero.Equipment.Legs;
            CmbFeet.SelectedValue = _originalHero.Equipment.Feet;
            CmbLeftRing.SelectedValue = _originalHero.Equipment.LeftRing;
            CmbRightRing.SelectedValue = _originalHero.Equipment.RightRing;
            CmbWeapon.SelectedValue = _originalHero.Equipment.Weapon;
            CmbClass.SelectedValue = _originalHero.Class;
        }

        #endregion Display Manipulation

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Binds information to controls.</summary>
        private void BindControls()
        {
            CmbClass.ItemsSource = GameState.AllClasses;
            CmbWeapon.ItemsSource = GameState.AllWeapons;
            CmbHead.ItemsSource = GameState.AllHeadArmor;
            CmbBody.ItemsSource = GameState.AllBodyArmor;
            CmbHands.ItemsSource = GameState.AllHandArmor;
            CmbLegs.ItemsSource = GameState.AllLegArmor;
            CmbFeet.ItemsSource = GameState.AllFeetArmor;
            CmbLeftRing.ItemsSource = GameState.AllRings;
            CmbRightRing.ItemsSource = GameState.AllRings;

            Reset();
        }

        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Input Manipulation

        private void Txt_PreviewKeyDown(object sender, KeyEventArgs e) => Functions.PreviewKeyDown(e, KeyType.Letters);

        private void TxtNumbers_PreviewKeyDown(object sender, KeyEventArgs e) => Functions.PreviewKeyDown(e, KeyType.Integers);

        private void Txt_TextChanged(object sender, TextChangedEventArgs e) => Functions.TextBoxTextChanged(sender, KeyType.Letters);

        private void Integers_TextChanged(object sender, TextChangedEventArgs e) => Functions.TextBoxTextChanged(sender, KeyType.Integers);

        private void Txt_GotFocus(object sender, RoutedEventArgs e) => Functions.TextBoxGotFocus(sender);

        private void Pswd_GotFocus(object sender, RoutedEventArgs e) => Functions.PasswordBoxGotFocus(sender);

        private void TxtInventory_PreviewKeyDown(object sender, KeyEventArgs e) => Functions.PreviewKeyDown(e, KeyType.LettersSpaceComma);

        private void TxtInventory_TextChanged(object sender, TextChangedEventArgs e) => Functions.TextBoxTextChanged(sender, KeyType.LettersSpaceComma);

        #endregion Input Manipulation

        #region Button-Click Methods

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (TxtHeroName.Text.Length >= 4 &&
                (PswdPassword.Password.Length == 0 && PswdConfirm.Password.Length == 0 ||
                 PswdPassword.Password.Length >= 4 && PswdConfirm.Password.Length >= 4) &&
                PswdPassword.Password == PswdConfirm.Password)
            {
                string password = PswdPassword.Password.Length >= 0
                    ? Argon2.HashPassword(PswdPassword.Password)
                    : _originalHero.Password;

                if (TxtHeroName.Text != _originalHero.Name || PswdPassword.Password.Length >= 4)
                    await GameState.ChangeHeroDetails(_originalHero,
                        new Hero { Name = TxtHeroName.Text, Password = password });
                Hero newHero = new Hero(TxtHeroName.Text, password, (HeroClass)CmbClass.SelectedItem,
                    Int32Helper.Parse(TxtLevel.Text), Int32Helper.Parse(TxtExperience.Text),
                    Int32Helper.Parse(TxtSkillPoints.Text),
                    new Attributes(Int32Helper.Parse(TxtStrength.Text), Int32Helper.Parse(TxtVitality.Text),
                        Int32Helper.Parse(TxtDexterity.Text), Int32Helper.Parse(TxtWisdom.Text)),
                    new Statistics(Int32Helper.Parse(TxtCurrentHealth.Text), Int32Helper.Parse(TxtMaximumHealth.Text),
                        Int32Helper.Parse(TxtCurrentMagic.Text), Int32Helper.Parse(TxtMaximumMagic.Text)), new Equipment((Weapon)CmbWeapon.SelectedItem, (HeadArmor)CmbHead.SelectedItem, (BodyArmor)CmbBody.SelectedItem, (HandArmor)CmbHands.SelectedItem, (LegArmor)CmbLegs.SelectedItem, (FeetArmor)CmbFeet.SelectedItem, CmbLeftRing.SelectedIndex >= 0 ? (Ring)CmbLeftRing.SelectedItem : new Ring(), CmbRightRing.SelectedIndex >= 0 ? (Ring)CmbRightRing.SelectedItem : new Ring()),
                    new Spellbook(_originalHero.Spellbook), new Inventory(TxtInventory.Text, Int32Helper.Parse(TxtGold.Text)), new Progression(_originalHero.Progression), ChkHardcore.IsChecked ?? false);

                if (await GameState.SaveHero(newHero))
                    ClosePage();
            }
            else if (PswdPassword.Password.Length != 0 && PswdConfirm.Password.Length != 0 &&
                     PswdPassword.Password.Length < 4 && PswdConfirm.Password.Length < 4)
                GameState.DisplayNotification("Please ensure the new password is 4 or more characters in length.", "Sulimn");
            else if (PswdPassword.Password != PswdConfirm.Password)
                GameState.DisplayNotification("Please ensure the passwords match.", "Sulimn");
            else if (TxtHeroName.Text.Length < 4)
                GameState.DisplayNotification("Please ensure the hero name and password are at least 4 characters long.", "Sulimn");
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e) => Reset();

        private void BtnCancel_Click(object sender, RoutedEventArgs e) => ClosePage();

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage()
        {
            PreviousPage.RefreshItemsSource();
            GameState.GoBack();
        }

        internal void LoadPage(Hero manageHero)
        {
            _originalHero = new Hero(manageHero);
            BindControls();
        }

        public ManageUserPage() => InitializeComponent();

        private void ManageUserPage_OnLoaded(object sender, RoutedEventArgs e) => GameState.CalculateScale(Grid);

        #endregion Page-Manipulation Methods
    }
}