using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sulimn
{
    /// <summary>Interaction logic for NewPlayerWindow.xaml</summary>
    public partial class NewHeroWindow
    {
        private readonly List<HeroClass> _classes = new List<HeroClass>();
        private HeroClass _compareClass = new HeroClass();
        private HeroClass _selectedClass = new HeroClass();
        private bool _startGame;

        internal MainWindow RefToMainWindow { private get; set; }

        #region Display Manipulation

        /// <summary>Clears all text from the labels and resets the Window to default.</summary>
        private void Clear()
        {
            _selectedClass = new HeroClass();
            _compareClass = new HeroClass();
            lstClasses.UnselectAll();
            txtHeroName.Clear();
            txtHeroName.Focus();
            CheckSkillPoints();
            DisableMinus();
            TogglePlus(false);
        }

        #endregion Display Manipulation

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Attribute Modification

        /// <summary>Increases specified Attribute.</summary>
        /// <param name="attribute">Attribute to be increased.</param>
        /// <returns>Increased attribute</returns>
        private int IncreaseAttribute(int attribute)
        {
            _selectedClass.SkillPoints--;
            CheckSkillPoints();
            return ++attribute;
        }

        /// <summary>Decreases specified Attribute.</summary>
        /// <param name="attribute">Attribute to be decreased.</param>
        /// <returns>Decreased attribute</returns>
        private int DecreaseAttribute(int attribute)
        {
            _selectedClass.SkillPoints++;
            CheckSkillPoints();
            return --attribute;
        }

        #endregion Attribute Modification

        #region Enable/Disable Buttons

        /// <summary>Enables/disables buttons based on the Hero's available Skill Points.</summary>
        private void CheckSkillPoints()
        {
            if (lstClasses.SelectedIndex >= 0 && _selectedClass.SkillPoints > 0)
            {
                if (_selectedClass.SkillPoints >= _compareClass.SkillPoints)
                    DisableMinus();
                TogglePlus(true);
                btnCreate.IsEnabled = false;
            }
            else if (lstClasses.SelectedIndex >= 0 && _selectedClass.SkillPoints == 0)
            {
                TogglePlus(false);
                btnCreate.IsEnabled = txtHeroName.Text.Length > 0 && pswdPassword.Password.Length > 0 &&
                                      pswdConfirm.Password.Length > 0;
            }
            else if (lstClasses.SelectedIndex >= 0 && _selectedClass.SkillPoints < 0)
            {
                new Notification("Somehow you have negative skill points. Please try creating your character again.",
                "Sulimn", NotificationButtons.OK, this).ShowDialog();
                Clear();
            }
        }

        #region Toggle Buttons

        /// <summary>Toggles the IsEnabled Property of the Plus Buttons.</summary>
        /// <param name="enabled">Should the Buttons be enabled?</param>
        private void TogglePlus(bool enabled)
        {
            btnDexterityPlus.IsEnabled = enabled;
            btnStrengthPlus.IsEnabled = enabled;
            btnWisdomPlus.IsEnabled = enabled;
            btnVitalityPlus.IsEnabled = enabled;
        }

        private void DisableMinus()
        {
            btnDexterityMinus.IsEnabled = false;
            btnStrengthMinus.IsEnabled = false;
            btnWisdomMinus.IsEnabled = false;
            btnVitalityMinus.IsEnabled = false;
        }

        #endregion Toggle Buttons

        #endregion Enable/Disable Buttons

        #region Button-Click Methods

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Hero createHero = new Hero();

            try
            {
                createHero = GameState.AllHeroes.Find(hero => hero.Name == txtHeroName.Text);
            }
            catch (ArgumentNullException)
            {
            }
            if (!string.IsNullOrWhiteSpace(createHero?.Name))
            {
                new Notification("This username has already been taken. Please choose another.", "Test",
                NotificationButtons.OK, this).ShowDialog();
                txtHeroName.SelectAll();
            }
            else
            {
                if (txtHeroName.Text.Length >= 4 && pswdPassword.Password.Length >= 4)
                    if (pswdPassword.Password.Trim() == pswdConfirm.Password.Trim())
                    {
                        Hero newHero = new Hero(
                        txtHeroName.Text.Trim(),
                        PasswordHash.HashPassword(pswdPassword.Password.Trim()),
                        _selectedClass,
                        1,
                        0,
                        0,
                        new Attributes(
                        _selectedClass.Strength,
                        _selectedClass.Vitality,
                        _selectedClass.Dexterity,
                        _selectedClass.Wisdom),
                        new Statistics(
                        _selectedClass.CurrentHealth,
                        _selectedClass.MaximumHealth,
                        _selectedClass.CurrentMagic,
                        _selectedClass.MaximumMagic),
                        new Equipment(
                        new Weapon(),
                        new HeadArmor(),
                        new BodyArmor(),
                        new HandArmor(),
                        new LegArmor(),
                        new FeetArmor(),
                        new Ring(),
                        new Ring()),
                        new Spellbook(),
                        new Inventory(new List<Item>(), 250));

                        if (await GameState.NewHero(newHero))
                        {
                            _startGame = true;
                            GameState.CurrentHero = GameState.AllHeroes.Find(hero => hero.Name == newHero.Name);
                            CloseWindow();
                        }
                    }
                    else
                    {
                        new Notification("Please ensure that the passwords match.", "Sulimn", NotificationButtons.OK, this).ShowDialog();
                    }
                else
                    new Notification("Names and passwords have to be at least 4 characters.", "Sulimn",
                    NotificationButtons.OK, this).ShowDialog();
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
            _selectedClass.Strength = DecreaseAttribute(_selectedClass.Strength);
            btnStrengthMinus.IsEnabled = _selectedClass.Strength == _compareClass.Strength;
        }

        private void btnStrengthPlus_Click(object sender, RoutedEventArgs e)
        {
            _selectedClass.Strength = IncreaseAttribute(_selectedClass.Strength);
            btnStrengthMinus.IsEnabled = true;
        }

        private void btnVitalityMinus_Click(object sender, RoutedEventArgs e)
        {
            _selectedClass.Vitality = DecreaseAttribute(_selectedClass.Vitality);
            btnVitalityMinus.IsEnabled = _selectedClass.Vitality == _compareClass.Vitality;
        }

        private void btnVitalityPlus_Click(object sender, RoutedEventArgs e)
        {
            _selectedClass.Vitality = IncreaseAttribute(_selectedClass.Vitality);
            btnVitalityMinus.IsEnabled = true;
        }

        private void btnDexterityMinus_Click(object sender, RoutedEventArgs e)
        {
            _selectedClass.Dexterity = DecreaseAttribute(_selectedClass.Dexterity);
            btnDexterityMinus.IsEnabled = _selectedClass.Dexterity == _compareClass.Dexterity;
        }

        private void btnDexterityPlus_Click(object sender, RoutedEventArgs e)
        {
            _selectedClass.Dexterity = IncreaseAttribute(_selectedClass.Dexterity);
            btnDexterityMinus.IsEnabled = true;
        }

        private void btnWisdomMinus_Click(object sender, RoutedEventArgs e)
        {
            _selectedClass.Wisdom = DecreaseAttribute(_selectedClass.Wisdom);
            btnWisdomMinus.IsEnabled = _selectedClass.Wisdom == _compareClass.Wisdom;
        }

        private void btnWisdomPlus_Click(object sender, RoutedEventArgs e)
        {
            _selectedClass.Wisdom = IncreaseAttribute(_selectedClass.Wisdom);
            btnWisdomMinus.IsEnabled = true;
        }

        #endregion Plus/Minus Button Logic

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public NewHeroWindow()
        {
            InitializeComponent();
            _classes.AddRange(GameState.AllClasses);
            lstClasses.ItemsSource = _classes;
            DataContext = _selectedClass;
            txtHeroName.Focus();
        }

        private void txtHeroName_Changed(object sender, TextChangedEventArgs e)
        {
            txtHeroName.Text = new string((from c in txtHeroName.Text
                                           where char.IsLetter(c)
                                           select c).ToArray());
            txtHeroName.CaretIndex = txtHeroName.Text.Length;
            CheckSkillPoints();
        }

        private void txtHeroName_GotFocus(object sender, RoutedEventArgs e)
        {
            txtHeroName.SelectAll();
        }

        private void txtHeroName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;

            List<bool> keys = Functions.GetListOfKeys(Key.Back, Key.Delete, Key.Home, Key.End, Key.LeftShift, Key.RightShift,
            Key.Enter, Key.Tab, Key.LeftAlt, Key.RightAlt, Key.Left, Key.Right, Key.LeftCtrl, Key.RightCtrl,
            Key.Escape);

            e.Handled = !keys.Any(key => key) && (Key.A > k || k > Key.Z);
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
                _selectedClass = new HeroClass((HeroClass)lstClasses.SelectedValue);
                _compareClass = new HeroClass((HeroClass)lstClasses.SelectedValue);
                CheckSkillPoints();
            }
            else
                Clear();

            lstClasses.ItemsSource = _classes;
            DataContext = _selectedClass;
        }

        private void windowNewHero_Closing(object sender, CancelEventArgs e)
        {
            if (_startGame)
            {
                CityWindow cityWindow = new CityWindow { RefToMainWindow = RefToMainWindow };
                cityWindow.Show();
                Visibility = Visibility.Hidden;
            }
            else
                RefToMainWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}