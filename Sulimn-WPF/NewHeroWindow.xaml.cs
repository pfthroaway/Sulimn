using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sulimn_WPF
{
    /// <summary>
    /// Interaction logic for NewPlayerWindow.xaml
    /// </summary>
    public partial class NewHeroWindow : Window
    {
        private List<HeroClass> classes = new List<HeroClass>();
        private HeroClass selectedClass = new HeroClass();
        private HeroClass compareClass = new HeroClass();
        private bool startGame = false;

        internal MainWindow RefToMainWindow { get; set; }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Attribute Modification

        /// <summary>
        /// Increases specified Attribute.
        /// </summary>
        /// <param name="attribute">Attribute to be increased.</param>
        /// <returns>Increased attribute</returns>
        private int IncreaseAttribute(int attribute)
        {
            selectedClass.SkillPoints--;
            CheckSkillPoints();
            return ++attribute;
        }

        /// <summary>
        /// Decreases specified Attribute.
        /// </summary>
        /// <param name="attribute">Attribute to be decreased.</param>
        /// <returns>Decreased attribute</returns>
        private int DecreaseAttribute(int attribute)
        {
            selectedClass.SkillPoints++;
            CheckSkillPoints();
            return --attribute;
        }

        #endregion Attribute Modification

        #region Display Manipulation

        /// <summary>
        /// Clears all text from the labels and resets the Window to default.
        /// </summary>
        private void Clear()
        {
            selectedClass = new HeroClass();
            compareClass = new HeroClass();
            lstClasses.UnselectAll();
            txtHeroName.Clear();
            txtHeroName.Focus();
            CheckSkillPoints();
            DisableMinus();
            DisablePlus();
        }

        #endregion Display Manipulation

        #region Enable/Disable Buttons

        /// <summary>
        /// Enables/disables buttons based on the Hero's available Skill Points.
        /// </summary>
        internal void CheckSkillPoints()
        {
            if (lstClasses.SelectedIndex >= 0 && selectedClass.SkillPoints > 0)
            {
                if (selectedClass.SkillPoints >= compareClass.SkillPoints)
                    DisableMinus();
                EnablePlus();
                btnCreate.IsEnabled = false;
            }
            else if (lstClasses.SelectedIndex >= 0 && selectedClass.SkillPoints == 0)
            {
                DisablePlus();
                if (txtHeroName.Text.Length > 0 && pswdPassword.Password.Length > 0 && pswdConfirm.Password.Length > 0)
                    btnCreate.IsEnabled = true;
                else
                    btnCreate.IsEnabled = false;
            }
            else if (lstClasses.SelectedIndex >= 0 && selectedClass.SkillPoints < 0)
            {
                MessageBox.Show("Somehow you have negative skill points. Please try creating your character again.", "Sulimn", MessageBoxButton.OK);
                Clear();
            }
        }

        /// <summary>
        /// Disables all Plus buttons.
        /// </summary>
        private void DisablePlus()
        {
            btnDexterityPlus.IsEnabled = false;
            btnStrengthPlus.IsEnabled = false;
            btnWisdomPlus.IsEnabled = false;
            btnVitalityPlus.IsEnabled = false;
        }

        /// <summary>
        /// Enables all Plus buttons.
        /// </summary>
        private void EnablePlus()
        {
            btnDexterityPlus.IsEnabled = true;
            btnStrengthPlus.IsEnabled = true;
            btnWisdomPlus.IsEnabled = true;
            btnVitalityPlus.IsEnabled = true;
        }

        /// <summary>
        /// Disables all Minus buttons.
        /// </summary>
        private void DisableMinus()
        {
            btnDexterityMinus.IsEnabled = false;
            btnStrengthMinus.IsEnabled = false;
            btnWisdomMinus.IsEnabled = false;
            btnVitalityMinus.IsEnabled = false;
        }

        /// <summary>
        /// Enables all Minus buttons.
        /// </summary>
        private void EnableMinus()
        {
            btnDexterityMinus.IsEnabled = true;
            btnStrengthMinus.IsEnabled = true;
            btnWisdomMinus.IsEnabled = true;
            btnVitalityMinus.IsEnabled = true;
        }

        #endregion Enable/Disable Buttons

        #region Button-Click Methods

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Hero createHero = new Hero();

            try
            {
                createHero = GameState.AllHeroes.Find(hero => hero.Name == txtHeroName.Text);
            }
            catch (Exception)
            {
            }
            if (createHero != null && !string.IsNullOrWhiteSpace(createHero.Name))
            {
                MessageBox.Show("This username has already been taken. Please choose another.", "Test", MessageBoxButton.OK);
                txtHeroName.SelectAll();
            }
            else
            {
                if (txtHeroName.Text.Length >= 4 && pswdPassword.Password.Length >= 4)
                {
                    if (pswdPassword.Password.Trim() == pswdConfirm.Password.Trim())
                    {
                        Hero newHero = new Hero(txtHeroName.Text.Trim(), PasswordHash.HashPassword(pswdPassword.Password.Trim()), selectedClass.Name, 1, 0, 0, selectedClass.Strength, selectedClass.Vitality, selectedClass.Dexterity, selectedClass.Wisdom, 250, selectedClass.CurrentHealth, selectedClass.MaximumHealth, selectedClass.CurrentMagic, selectedClass.MaximumMagic, new Armor(), new Armor(), new Armor(), new Armor(), new Weapon(), new Spellbook(), new Inventory());

                        if (await Functions.NewHero(newHero))
                        {
                            startGame = true;
                            GameState.CurrentHero = newHero;
                            CloseWindow();
                        }
                    }
                    else
                        MessageBox.Show("Please ensure that the passwords match.", "Sulimn", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("Names and passwords have to be at least 4 characters.", "Sulimn", MessageBoxButton.OK);
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        #endregion Button-Click Methods

        #region Plus/Minus Button Logic

        private void btnStrengthMinus_Click(object sender, RoutedEventArgs e)
        {
            selectedClass.Strength = DecreaseAttribute(selectedClass.Strength);
            if (selectedClass.Strength == compareClass.Strength)
                btnStrengthMinus.IsEnabled = false;
        }

        private void btnStrengthPlus_Click(object sender, RoutedEventArgs e)
        {
            selectedClass.Strength = IncreaseAttribute(selectedClass.Strength);
            btnStrengthMinus.IsEnabled = true;
        }

        private void btnVitalityMinus_Click(object sender, RoutedEventArgs e)
        {
            selectedClass.Vitality = DecreaseAttribute(selectedClass.Vitality);
            if (selectedClass.Vitality == compareClass.Vitality)
                btnVitalityMinus.IsEnabled = false;
        }

        private void btnVitalityPlus_Click(object sender, RoutedEventArgs e)
        {
            selectedClass.Vitality = IncreaseAttribute(selectedClass.Vitality);
            btnVitalityMinus.IsEnabled = true;
        }

        private void btnDexterityMinus_Click(object sender, RoutedEventArgs e)
        {
            selectedClass.Dexterity = DecreaseAttribute(selectedClass.Dexterity);
            if (selectedClass.Dexterity == compareClass.Dexterity)
                btnDexterityMinus.IsEnabled = false;
        }

        private void btnDexterityPlus_Click(object sender, RoutedEventArgs e)
        {
            selectedClass.Dexterity = IncreaseAttribute(selectedClass.Dexterity);
            btnDexterityMinus.IsEnabled = true;
        }

        private void btnWisdomMinus_Click(object sender, RoutedEventArgs e)
        {
            selectedClass.Wisdom = DecreaseAttribute(selectedClass.Wisdom);
            if (selectedClass.Wisdom == compareClass.Wisdom)
                btnWisdomMinus.IsEnabled = false;
        }

        private void btnWisdomPlus_Click(object sender, RoutedEventArgs e)
        {
            selectedClass.Wisdom = IncreaseAttribute(selectedClass.Wisdom);
            btnWisdomMinus.IsEnabled = true;
        }

        #endregion Plus/Minus Button Logic

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
        }

        public NewHeroWindow()
        {
            InitializeComponent();
            classes.AddRange(GameState.AllClasses);
            lstClasses.ItemsSource = classes;
            DataContext = selectedClass;
            txtHeroName.Focus();
        }

        private void txtName_Changed(object sender, TextChangedEventArgs e)
        {
            CheckSkillPoints();
        }

        private void txtHeroName_GotFocus(object sender, RoutedEventArgs e)
        {
            txtHeroName.SelectAll();
        }

        private void txtHeroName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;

            List<bool> keys = Functions.GetListOfKeys(Key.Back, Key.Delete, Key.Home, Key.End, Key.LeftShift, Key.RightShift, Key.Enter, Key.Tab, Key.LeftAlt, Key.RightAlt, Key.Left, Key.Right, Key.LeftCtrl, Key.RightCtrl);

            if (keys.Any(key => key == true) || Key.A <= k && k <= Key.Z)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void pswdConfirm_GotFocus(object sender, RoutedEventArgs e)
        {
            pswdConfirm.SelectAll();
        }

        private void pswdPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            pswdPassword.SelectAll();
        }

        private void pswd_Changed(object sender, RoutedEventArgs e)
        {
            CheckSkillPoints();
        }

        private void lstClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstClasses.SelectedIndex >= 0)
            {
                selectedClass = new HeroClass((HeroClass)lstClasses.SelectedValue);
                compareClass = new HeroClass((HeroClass)lstClasses.SelectedValue);
                CheckSkillPoints();
            }
            else
                Clear();

            lstClasses.ItemsSource = classes;
            DataContext = selectedClass;
        }

        private void windowNewHero_Closing(object sender, CancelEventArgs e)
        {
            if (startGame)
            {
                CityWindow cityWindow = new CityWindow();
                cityWindow.Show();
                cityWindow.RefToMainWindow = RefToMainWindow;
                cityWindow.EnterCity();
                this.Visibility = Visibility.Hidden;
            }
            else
                RefToMainWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}