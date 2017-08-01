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
    /// <summary>Interaction logic for AdminNewUserPage.xaml</summary>
    public partial class AdminNewUserPage : INotifyPropertyChanged
    {
        internal ManageUsersPage PreviousPage { get; set; }

        #region Display Manipulation

        /// <summary>Determines if buttons should be enabled.</summary>
        private void CheckInput()
        {
            bool enabled = TxtHeroName.Text.Length > 0 && CmbClass.SelectedIndex >= 0 && TxtLevel.Text.Length > 0 &&
                           TxtExperience.Text.Length > 0 && TxtSkillPoints.Text.Length > 0 &&
                           TxtStrength.Text.Length > 0 && TxtVitality.Text.Length > 0 && TxtDexterity.Text.Length > 0 &&
                           TxtWisdom.Text.Length > 0 && TxtCurrentHealth.Text.Length > 0 &&
                           TxtMaximumHealth.Text.Length > 0 && TxtCurrentMagic.Text.Length > 0 &&
                           TxtMaximumMagic.Text.Length > 0 && TxtGold.Text.Length > 0 && CmbWeapon.SelectedIndex >= 0 &&
                           CmbHead.SelectedIndex >= 0 && CmbBody.SelectedIndex >= 0 && CmbHands.SelectedIndex >= 0 &&
                           CmbLegs.SelectedIndex >= 0 && CmbFeet.SelectedIndex >= 0;
            BtnSave.IsEnabled = enabled;
        }

        /// <summary>Displays the Hero as it was when the Page was loaded.</summary>
        private void DisplayOriginalHero()
        {
            TxtHeroName.Text = "";
            ChkHardcore.IsChecked = false;
            TxtLevel.Text = "";
            TxtExperience.Text = "";
            TxtSkillPoints.Text = "";
            TxtStrength.Text = "";
            TxtVitality.Text = "";
            TxtDexterity.Text = "";
            TxtWisdom.Text = "";
            TxtGold.Text = "";
            TxtInventory.Text = "";
            TxtCurrentHealth.Text = "";
            TxtMaximumHealth.Text = "";
            TxtCurrentMagic.Text = "";
            TxtMaximumMagic.Text = "";
            CmbHead.SelectedIndex = -1;
            CmbBody.SelectedIndex = -1;
            CmbHands.SelectedIndex = -1;
            CmbLegs.SelectedIndex = -1;
            CmbFeet.SelectedIndex = -1;
            CmbLeftRing.SelectedIndex = -1;
            CmbRightRing.SelectedIndex = -1;
            CmbWeapon.SelectedIndex = -1;
            CmbClass.SelectedIndex = -1;
        }

        private void Reset()
        {
            DisplayOriginalHero();
            BindControls();
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

            DisplayOriginalHero();
        }

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Input Manipulation

        private void Txt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Functions.PreviewKeyDown(e, KeyType.Letters);
        }

        private void TxtNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Functions.PreviewKeyDown(e, KeyType.Integers);
        }

        private void Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Functions.TextBoxTextChanged(sender, KeyType.Letters);
            CheckInput();
        }

        private void Integers_TextChanged(object sender, TextChangedEventArgs e)
        {
            Functions.TextBoxTextChanged(sender, KeyType.Integers);
            CheckInput();
        }

        private void Txt_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.TextBoxGotFocus(sender);
        }

        private void Pswd_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.PasswordBoxGotFocus(sender);
        }

        private void TxtInventory_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Functions.PreviewKeyDown(e, KeyType.LettersSpaceComma);
        }

        private void TxtInventory_TextChanged(object sender, TextChangedEventArgs e)
        {
            Functions.TextBoxTextChanged(sender, KeyType.LettersSpaceComma);
            CheckInput();
        }

        #endregion Input Manipulation

        #region Button-Click Methods

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (TxtHeroName.Text.Length >= 4 && PswdPassword.Password.Length >= 4 && PswdConfirm.Password.Length >= 4 &&
                 PswdPassword.Password == PswdConfirm.Password)
            {
                Hero checkHero = GameState.AllHeroes.Find(hero => hero.Name == TxtHeroName.Text);
                if (checkHero == null || checkHero == new Hero())
                {
                    await GameState.NewHero(new Hero(TxtHeroName.Text, Argon2.HashPassword(PswdPassword.Password),
                        (HeroClass)CmbClass.SelectedItem,
                        Int32Helper.Parse(TxtLevel.Text), Int32Helper.Parse(TxtExperience.Text),
                        Int32Helper.Parse(TxtSkillPoints.Text),
                        new Attributes(Int32Helper.Parse(TxtStrength.Text), Int32Helper.Parse(TxtVitality.Text),
                            Int32Helper.Parse(TxtDexterity.Text), Int32Helper.Parse(TxtWisdom.Text)),
                        new Statistics(Int32Helper.Parse(TxtCurrentHealth.Text),
                            Int32Helper.Parse(TxtMaximumHealth.Text),
                            Int32Helper.Parse(TxtCurrentMagic.Text), Int32Helper.Parse(TxtMaximumMagic.Text)),
                        new Equipment((Weapon)CmbWeapon.SelectedItem, (HeadArmor)CmbHead.SelectedItem,
                            (BodyArmor)CmbBody.SelectedItem, (HandArmor)CmbHands.SelectedItem,
                            (LegArmor)CmbLegs.SelectedItem, (FeetArmor)CmbFeet.SelectedItem,
                            CmbLeftRing.SelectedIndex >= 0 ? (Ring)CmbLeftRing.SelectedItem : new Ring(),
                            CmbRightRing.SelectedIndex >= 0 ? (Ring)CmbRightRing.SelectedItem : new Ring()),
                        new Spellbook(), new Inventory(TxtInventory.Text, Int32Helper.Parse(TxtGold.Text)),
                        ChkHardcore.IsChecked ?? false));
                    ClosePage();
                }
                else
                {
                    GameState.DisplayNotification("This username has already been taken. Please try another.", "Sulimn");
                    TxtHeroName.Focus();
                }
            }
            else if (PswdPassword.Password.Length < 4 && PswdConfirm.Password.Length < 4)
            {
                GameState.DisplayNotification("Please ensure the new password is 4 or more characters in length.", "Sulimn");
                PswdPassword.Focus();
            }
            else if (PswdPassword.Password != PswdConfirm.Password)
            {
                GameState.DisplayNotification("Please ensure the passwords match.", "Sulimn");
                PswdPassword.Focus();
            }
            else if (TxtHeroName.Text.Length < 4)
            {
                GameState.DisplayNotification("Please ensure the hero name and password are at least 4 characters long.", "Sulimn");
                TxtHeroName.Focus();
            }
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage()
        {
            GameState.GoBack();
        }

        #endregion Page-Manipulation Methods

        public AdminNewUserPage()
        {
            InitializeComponent();
            TxtHeroName.Focus();
        }

        private void AdminNewUserPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameState.CalculateScale(Grid);
            Reset();
        }
    }
}