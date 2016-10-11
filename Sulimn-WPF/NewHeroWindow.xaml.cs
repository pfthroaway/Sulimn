using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        internal MainWindow RefToMainWindow { get; set; }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        /// <summary>
        /// Loads all Classes from the database.
        /// </summary>
        private async void LoadClasses()
        {
            string sql = "SELECT * FROM Classes";
            string table = "Classes";
            DataSet ds = new DataSet();
            ds = await Functions.FillDataSet(sql, table);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                HeroClass newClass = new HeroClass();

                newClass.Name = ds.Tables[0].Rows[i]["ClassName"].ToString();
                newClass.Description = ds.Tables[0].Rows[i]["ClassDescription"].ToString();
                newClass.SkillPoints = Convert.ToInt32(ds.Tables[0].Rows[i]["SkillPoints"].ToString());
                newClass.Strength = Convert.ToInt32(ds.Tables[0].Rows[i]["Strength"].ToString());
                newClass.Vitality = Convert.ToInt32(ds.Tables[0].Rows[i]["Vitality"].ToString());
                newClass.Dexterity = Convert.ToInt32(ds.Tables[0].Rows[i]["Dexterity"].ToString());
                newClass.Wisdom = Convert.ToInt32(ds.Tables[0].Rows[i]["Wisdom"].ToString());

                classes.Add(newClass);
            }

            classes = classes.OrderBy(x => x.Name).ToList(); // sort classes alphabetically
            lstClasses.ItemsSource = classes;
        }

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
                if (txtHeroName.Text.Length >= 4)
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
            string cName = txtHeroName.Text;

            DataSet ds = new DataSet();
            string sql = "SELECT * FROM Players WHERE [CharacterName]='" + cName + "'";
            string table = "Player";
            ds = await Functions.FillDataSet(sql, table);

            if (ds.Tables[table].Rows.Count > 0)
            {
                MessageBox.Show("This username has already been taken. Please choose another.", "Test", MessageBoxButton.OK);
            }
            else
            {
                Hero newHero = new Hero(txtHeroName.Text, selectedClass.Name, 1, 0, 0, selectedClass.Strength, selectedClass.Vitality, selectedClass.Dexterity, selectedClass.Wisdom, 250, selectedClass.CurrentHealth, selectedClass.MaximumHealth, selectedClass.CurrentMagic, selectedClass.MaximumMagic, new Armor(), new Armor(), new Armor(), new Armor(), new Weapon(), new Spellbook(), new Inventory());

                Functions.NewHero(newHero);
                CloseWindow();
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
            LoadClasses();
            DataContext = selectedClass;
        }

        private void TxtName_Changed(object sender, TextChangedEventArgs e)
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
            DataContext = selectedClass;
        }

        private void windowNewHero_Closing(object sender, CancelEventArgs e)
        {
            RefToMainWindow.Visibility = Visibility.Visible;
            //CloseWindow();
        }

        #endregion Window-Manipulation Methods
    }
}