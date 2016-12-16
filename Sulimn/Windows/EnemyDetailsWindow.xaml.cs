using System.ComponentModel;
using System.Windows;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for EnemyDetailsWindow.xaml
    /// </summary>
    public partial class EnemyDetailsWindow : INotifyPropertyChanged
    {
        internal BattleWindow RefToBattleWindow { get; set; }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Binds information to Window.
        /// </summary>
        private void BindLabels()
        {
            DataContext = GameState.CurrentEnemy;
            lblStrength.DataContext = GameState.CurrentEnemy.Attributes;
            lblVitality.DataContext = GameState.CurrentEnemy.Attributes;
            lblDexterity.DataContext = GameState.CurrentEnemy.Attributes;
            lblHealth.DataContext = GameState.CurrentEnemy.Statistics;
            lblGold.DataContext = GameState.CurrentEnemy.Inventory;
            lblEquippedWeapon.DataContext = GameState.CurrentEnemy.Equipment.Weapon;
            lblEquippedWeaponDamage.DataContext = GameState.CurrentEnemy.Equipment.Weapon;
            lblEquippedHead.DataContext = GameState.CurrentEnemy.Equipment.Head;
            lblEquippedHeadDefense.DataContext = GameState.CurrentEnemy.Equipment.Head;
            lblEquippedBody.DataContext = GameState.CurrentEnemy.Equipment.Body;
            lblEquippedBodyDefense.DataContext = GameState.CurrentEnemy.Equipment.Body;
            lblEquippedLegs.DataContext = GameState.CurrentEnemy.Equipment.Legs;
            lblEquippedLegsDefense.DataContext = GameState.CurrentEnemy.Equipment.Legs;
            lblEquippedFeet.DataContext = GameState.CurrentEnemy.Equipment.Feet;
            lblEquippedFeetDefense.DataContext = GameState.CurrentEnemy.Equipment.Feet;
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            Close();
        }

        public EnemyDetailsWindow()
        {
            InitializeComponent();
            BindLabels();
        }

        private void windowEnemyDetails_Closing(object sender, CancelEventArgs e)
        {
            RefToBattleWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}