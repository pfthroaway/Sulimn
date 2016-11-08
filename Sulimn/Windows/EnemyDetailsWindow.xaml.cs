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
            DataContext = GameState.CurrentEnemy;
            lblEquippedWeapon.DataContext = GameState.CurrentEnemy.Weapon;
            lblEquippedWeaponDamage.DataContext = GameState.CurrentEnemy.Weapon;
            lblEquippedHead.DataContext = GameState.CurrentEnemy.Head;
            lblEquippedHeadDefense.DataContext = GameState.CurrentEnemy.Head;
            lblEquippedBody.DataContext = GameState.CurrentEnemy.Body;
            lblEquippedBodyDefense.DataContext = GameState.CurrentEnemy.Body;
            lblEquippedLegs.DataContext = GameState.CurrentEnemy.Legs;
            lblEquippedLegsDefense.DataContext = GameState.CurrentEnemy.Legs;
            lblEquippedFeet.DataContext = GameState.CurrentEnemy.Feet;
            lblEquippedFeetDefense.DataContext = GameState.CurrentEnemy.Feet;
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