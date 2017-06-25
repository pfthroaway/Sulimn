using Sulimn.Classes;
using System.ComponentModel;
using System.Windows;

namespace Sulimn.Windows.Battle
{
    /// <summary>Interaction logic for EnemyDetailsWindow.xaml</summary>
    public partial class EnemyDetailsWindow : INotifyPropertyChanged
    {
        internal BattleWindow RefToBattleWindow { private get; set; }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Binds information to Window.</summary>
        private void BindLabels()
        {
            DataContext = GameState.CurrentEnemy;
            LblStrength.DataContext = GameState.CurrentEnemy.Attributes;
            LblVitality.DataContext = GameState.CurrentEnemy.Attributes;
            LblDexterity.DataContext = GameState.CurrentEnemy.Attributes;
            LblHealth.DataContext = GameState.CurrentEnemy.Statistics;
            LblGold.DataContext = GameState.CurrentEnemy.Inventory;
            LblEquippedWeapon.DataContext = GameState.CurrentEnemy.Equipment.Weapon;
            LblEquippedWeaponDamage.DataContext = GameState.CurrentEnemy.Equipment.Weapon;
            LblEquippedHead.DataContext = GameState.CurrentEnemy.Equipment.Head;
            LblEquippedHeadDefense.DataContext = GameState.CurrentEnemy.Equipment.Head;
            LblEquippedBody.DataContext = GameState.CurrentEnemy.Equipment.Body;
            LblEquippedBodyDefense.DataContext = GameState.CurrentEnemy.Equipment.Body;
            LblEquippedLegs.DataContext = GameState.CurrentEnemy.Equipment.Legs;
            LblEquippedLegsDefense.DataContext = GameState.CurrentEnemy.Equipment.Legs;
            LblEquippedFeet.DataContext = GameState.CurrentEnemy.Equipment.Feet;
            LblEquippedFeetDefense.DataContext = GameState.CurrentEnemy.Equipment.Feet;
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        public EnemyDetailsWindow()
        {
            InitializeComponent();
            BindLabels();
        }

        private void WindowEnemyDetails_Closing(object sender, CancelEventArgs e)
        {
            RefToBattleWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}