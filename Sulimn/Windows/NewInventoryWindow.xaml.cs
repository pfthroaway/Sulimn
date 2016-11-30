using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for NewInventoryWindow.xaml
    /// </summary>
    public partial class NewInventoryWindow : Window, INotifyPropertyChanged
    {
        private List<Weapon> inventoryWeapon = new List<Weapon>();
        private List<HeadArmor> inventoryHeadArmor = new List<HeadArmor>();
        private List<BodyArmor> inventoryBodyArmor = new List<BodyArmor>();
        private List<HandArmor> inventoryHandsArmor = new List<HandArmor>();
        private List<LegArmor> inventoryLegsArmor = new List<LegArmor>();
        private List<FeetArmor> inventoryFeetArmor = new List<FeetArmor>();
        private List<Ring> inventoryRing = new List<Ring>();

        private Weapon selectedWeapon = new Weapon();
        private HeadArmor selectedHead = new HeadArmor();
        private BodyArmor selectedBody = new BodyArmor();
        private HandArmor selectedHands = new HandArmor();
        private LegArmor selectedLegs = new LegArmor();
        private FeetArmor selectedFeet = new FeetArmor();
        private Ring selectedRing = new Ring();

        internal CharacterWindow RefToCharacterWindow { get; set; }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindLabels()
        {
            lblGold.DataContext = GameState.CurrentHero.Inventory;
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            this.Close();
        }

        public NewInventoryWindow()
        {
            InitializeComponent();
        }

        private async void windowInventory_Closing(object sender, CancelEventArgs e)
        {
            RefToCharacterWindow.Show();
            await GameState.SaveHero(GameState.CurrentHero);
        }

        #endregion Window-Manipulation Methods

        private void lstWeaponInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnUnequipWeapon_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnEquipSelectedWeapon_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnDropSelectedWeapon_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnCharacter_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}