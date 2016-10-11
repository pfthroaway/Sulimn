using System.ComponentModel;
using System.Windows;

namespace Sulimn_WPF
{
    /// <summary>
    /// Interaction logic for EnemyDetailsWindow.xaml
    /// </summary>
    public partial class EnemyDetailsWindow : Window, INotifyPropertyChanged
    {
        internal BattleWindow RefToBattleWindow { get; set; }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Binds information to Window.
        /// </summary>
        private void BindLabels()
        {
            DataContext = GameState.currentEnemy;
            lblEquippedWeapon.DataContext = GameState.currentEnemy.Weapon;
            lblEquippedWeaponDamage.DataContext = GameState.currentEnemy.Weapon;
            lblEquippedHead.DataContext = GameState.currentEnemy.Head;
            lblEquippedHeadDefense.DataContext = GameState.currentEnemy.Head;
            lblEquippedBody.DataContext = GameState.currentEnemy.Body;
            lblEquippedBodyDefense.DataContext = GameState.currentEnemy.Body;
            lblEquippedLegs.DataContext = GameState.currentEnemy.Legs;
            lblEquippedLegsDefense.DataContext = GameState.currentEnemy.Legs;
            lblEquippedFeet.DataContext = GameState.currentEnemy.Feet;
            lblEquippedFeetDefense.DataContext = GameState.currentEnemy.Feet;
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
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