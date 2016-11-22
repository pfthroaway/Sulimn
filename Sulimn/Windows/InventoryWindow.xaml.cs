using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for InventoryWindow.xaml
    /// </summary>
    public partial class InventoryWindow : Window, INotifyPropertyChanged
    {
        internal CharacterWindow RefToCharacterWindow { get; set; }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Binds information to controls.
        /// </summary>
        private void BindLabels()
        {
            lblEquippedWeapon.DataContext = GameState.CurrentHero.Equipment.Weapon;
            lblEquippedWeaponDamage.DataContext = GameState.CurrentHero.Equipment.Weapon;
            lblEquippedHead.DataContext = GameState.CurrentHero.Equipment.Head;
            lblEquippedHeadDefense.DataContext = GameState.CurrentHero.Equipment.Head;
            lblEquippedBody.DataContext = GameState.CurrentHero.Equipment.Body;
            lblEquippedBodyDefense.DataContext = GameState.CurrentHero.Equipment.Body;
            lblEquippedLegs.DataContext = GameState.CurrentHero.Equipment.Legs;
            lblEquippedLegsDefense.DataContext = GameState.CurrentHero.Equipment.Legs;
            lblEquippedFeet.DataContext = GameState.CurrentHero.Equipment.Feet;
            lblEquippedFeetDefense.DataContext = GameState.CurrentHero.Equipment.Feet;
            DataContext = GameState.CurrentHero;
            lstInventory.ItemsSource = GameState.CurrentHero.Inventory;
        }

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Display Manipulation

        /// <summary>
        /// Clears all text from the Selected Item labels.
        /// </summary>
        private void ClearSelectedInfo()
        {
            lblSelectedName.Text = "";
            lblSelectedType.Text = "";
            lblDescription.Text = "";
            lblValue.Text = "";
            lblAmount.Text = "";
            lblSellable.Text = "";
        }

        /// <summary>
        /// Displays all relevant information.
        /// </summary>
        internal void DisplayAllInfo()
        {
            lstInventory.Items.Refresh();
            CheckUnequipButtons();
            DisplayInventoryList();
        }

        /// <summary>
        /// Displays information about the currently equipped Weapon and Armors.
        /// </summary>
        private void CheckUnequipButtons()
        {
            if (GameState.CurrentHero.Equipment.Weapon.Name == "Fists")
                btnUnequipWeapon.IsEnabled = false;
            else
                btnUnequipWeapon.IsEnabled = true;

            if (GameState.CurrentHero.Equipment.Head.Name == "Cloth Helmet")
                btnUnequipHead.IsEnabled = false;
            else
                btnUnequipHead.IsEnabled = true;

            if (GameState.CurrentHero.Equipment.Body.Name == "Cloth Shirt")
                btnUnequipBody.IsEnabled = false;
            else
                btnUnequipBody.IsEnabled = true;

            if (GameState.CurrentHero.Equipment.Legs.Name == "Cloth Pants")
                btnUnequipLegs.IsEnabled = false;
            else
                btnUnequipLegs.IsEnabled = true;

            if (GameState.CurrentHero.Equipment.Feet.Name == "Cloth Shoes")
                btnUnequipFeet.IsEnabled = false;
            else
                btnUnequipFeet.IsEnabled = true;
        }

        /// <summary>
        /// Displays the current Hero's information to the lstInventory ListBox.
        /// </summary>
        private void DisplayInventoryList()
        {
            ClearSelectedInfo();
            btnEquip.IsEnabled = false;
            btnConsume.IsEnabled = false;
            btnDrop.IsEnabled = false;
        }

        /// <summary>
        /// Display information about the currently selected Item in the lstInventory ListBox.
        /// </summary>
        private void DisplaySelectedInfo()
        {
            if (lstInventory.SelectedIndex >= 0)
            {
                Item selectedItem = GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex];

                switch (selectedItem.Type)
                {
                    case ItemTypes.Weapon:
                        btnEquip.IsEnabled = true;
                        btnConsume.IsEnabled = false;
                        btnDrop.IsEnabled = true;

                        Weapon selectedWeapon = (Weapon)GameState.AllItems.Find(x => x.Name == selectedItem.Name);

                        lblSelectedName.Text = selectedWeapon.Name;
                        lblSelectedType.Text = "Weapon: " + selectedWeapon.WeaponType;
                        lblDescription.Text = selectedWeapon.Description;
                        lblValue.Text = "Value: " + selectedWeapon.Value.ToString("N0");
                        lblAmount.Text = "Damage: " + selectedWeapon.Damage.ToString("N0");
                        if (selectedWeapon.CanSell)
                            lblSellable.Text = "Sellable";
                        else
                            lblSellable.Text = "Not Sellable";
                        break;

                    case ItemTypes.Head:
                        btnEquip.IsEnabled = true;
                        btnConsume.IsEnabled = false;
                        btnDrop.IsEnabled = true;

                        HeadArmor selectedHead = GameState.AllHeadArmor.Find(x => x.Name == selectedItem.Name);

                        lblSelectedName.Text = selectedHead.Name;
                        lblSelectedType.Text = "Armor: " + selectedHead.Type;
                        lblDescription.Text = selectedHead.Description;
                        lblValue.Text = "Value: " + selectedHead.Value.ToString("N0");
                        lblAmount.Text = "Defense: " + selectedHead.Defense.ToString("N0");
                        if (selectedHead.CanSell)
                            lblSellable.Text = "Sellable";
                        else
                            lblSellable.Text = "Not Sellable";
                        break;

                    case ItemTypes.Body:
                        btnEquip.IsEnabled = true;
                        btnConsume.IsEnabled = false;
                        btnDrop.IsEnabled = true;

                        BodyArmor selectedBody = GameState.AllBodyArmor.Find(x => x.Name == selectedItem.Name);

                        lblSelectedName.Text = selectedBody.Name;
                        lblSelectedType.Text = "Armor: " + selectedBody.Type;
                        lblDescription.Text = selectedBody.Description;
                        lblValue.Text = "Value: " + selectedBody.Value.ToString("N0");
                        lblAmount.Text = "Defense: " + selectedBody.Defense.ToString("N0");
                        if (selectedBody.CanSell)
                            lblSellable.Text = "Sellable";
                        else
                            lblSellable.Text = "Not Sellable";
                        break;

                    case ItemTypes.Legs:
                        btnEquip.IsEnabled = true;
                        btnConsume.IsEnabled = false;
                        btnDrop.IsEnabled = true;

                        LegArmor selectedLegs = GameState.AllLegArmor.Find(x => x.Name == selectedItem.Name);

                        lblSelectedName.Text = selectedLegs.Name;
                        lblSelectedType.Text = "Armor: " + selectedLegs.Type;
                        lblDescription.Text = selectedLegs.Description;
                        lblValue.Text = "Value: " + selectedLegs.Value.ToString("N0");
                        lblAmount.Text = "Defense: " + selectedLegs.Defense.ToString("N0");
                        if (selectedLegs.CanSell)
                            lblSellable.Text = "Sellable";
                        else
                            lblSellable.Text = "Not Sellable";
                        break;

                    case ItemTypes.Feet:
                        btnEquip.IsEnabled = true;
                        btnConsume.IsEnabled = false;
                        btnDrop.IsEnabled = true;

                        FeetArmor selectedFeet = GameState.AllFeetArmor.Find(x => x.Name == selectedItem.Name);

                        lblSelectedName.Text = selectedFeet.Name;
                        lblSelectedType.Text = "Armor: " + selectedFeet.Type;
                        lblDescription.Text = selectedFeet.Description;
                        lblValue.Text = "Value: " + selectedFeet.Value.ToString("N0");
                        lblAmount.Text = "Defense: " + selectedFeet.Defense.ToString("N0");
                        if (selectedFeet.CanSell)
                            lblSellable.Text = "Sellable";
                        else
                            lblSellable.Text = "Not Sellable";
                        break;

                    case ItemTypes.Potion:
                        btnEquip.IsEnabled = false;
                        btnConsume.IsEnabled = true;
                        btnDrop.IsEnabled = true;

                        Potion selectedPotion = (Potion)GameState.AllItems.Find(x => x.Name == selectedItem.Name);

                        lblSelectedName.Text = selectedPotion.Name;
                        lblSelectedType.Text = "Potion: " + selectedPotion.PotionType;
                        lblDescription.Text = selectedPotion.Description;
                        lblValue.Text = "Value: " + selectedPotion.Value.ToString("N0");
                        lblAmount.Text = selectedPotion.PotionType + ": " + selectedPotion.Amount.ToString("N0");
                        //FUTURE SET SWITCH FOR POTION TYPES
                        if (selectedPotion.CanSell)
                            lblSellable.Text = "Sellable";
                        else
                            lblSellable.Text = "Not Sellable";
                        break;

                    case ItemTypes.Food:
                        btnEquip.IsEnabled = false;
                        btnConsume.IsEnabled = true;
                        btnDrop.IsEnabled = true;

                        Food selectedFood = (Food)GameState.AllItems.Find(x => x.Name == selectedItem.Name);

                        lblSelectedName.Text = selectedFood.Name;
                        lblSelectedType.Text = "Food: " + selectedFood.FoodType;
                        lblDescription.Text = selectedFood.Description;
                        lblValue.Text = "Value: " + selectedFood.Value.ToString("N0");
                        lblAmount.Text = selectedFood.FoodType + ": " + selectedFood.Amount.ToString("N0");
                        //FUTURE SET SWITCH FOR POTION TYPES
                        if (selectedFood.CanSell)
                            lblSellable.Text = "Sellable";
                        else
                            lblSellable.Text = "Not Sellable";
                        break;
                    //FUTURE MORE ITEM TYPES
                    default:
                        MessageBox.Show("Somehow you managed to screw up and find an item type named '" + selectedItem.Type + "'.", "Sulimn", MessageBoxButton.OK);
                        break;
                }
            }
        }

        #endregion Display Manipulation

        #region Button-Click Methods

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void btnDrop_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dropItem = MessageBox.Show("Are you sure you really want to drop this item? You won't be able to get it back.", "Sulimn", MessageBoxButton.YesNo);

            if (dropItem == MessageBoxResult.Yes)
            {
                GameState.CurrentHero.Inventory.RemoveItemAt(lstInventory.SelectedIndex);
                lstInventory.UnselectAll();
                lstInventory.Items.Refresh();
            }

            DisplayInventoryList();
        }

        private void btnEquip_Click(object sender, RoutedEventArgs e)
        {
            switch (GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex].Type)
            {
                case ItemTypes.Weapon:
                    Weapon selectedWeapon = (Weapon)(GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex]);
                    GameState.CurrentHero.Inventory.RemoveItem(selectedWeapon);
                    if (GameState.CurrentHero.Equipment.Weapon.Name != "Fists")
                    {
                        GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Weapon);
                        lstInventory.ItemsSource = GameState.CurrentHero.Inventory;
                    }
                    GameState.CurrentHero.Equipment.Weapon = selectedWeapon;
                    lblEquippedWeapon.DataContext = GameState.CurrentHero.Equipment.Weapon;
                    lblEquippedWeaponDamage.DataContext = GameState.CurrentHero.Equipment.Weapon;
                    break;

                case ItemTypes.Head:
                    HeadArmor selectedHead = new HeadArmor((HeadArmor)(GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex]));
                    GameState.CurrentHero.Inventory.RemoveItem(selectedHead);

                    if (GameState.CurrentHero.Equipment.Head.Name != "Cloth Helmet")
                        GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Head);

                    GameState.CurrentHero.Equipment.Head = selectedHead;
                    lblEquippedHead.DataContext = GameState.CurrentHero.Equipment.Head;
                    lblEquippedHeadDefense.DataContext = GameState.CurrentHero.Equipment.Head;
                    break;

                case ItemTypes.Body:
                    BodyArmor selectedBody = new BodyArmor((BodyArmor)(GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex]));
                    GameState.CurrentHero.Inventory.RemoveItem(selectedBody);

                    if (GameState.CurrentHero.Equipment.Body.Name != "Cloth Shirt")
                        GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Body);

                    GameState.CurrentHero.Equipment.Body = selectedBody;
                    lblEquippedBody.DataContext = GameState.CurrentHero.Equipment.Body;
                    lblEquippedBodyDefense.DataContext = GameState.CurrentHero.Equipment.Body;
                    break;

                case ItemTypes.Legs:
                    LegArmor selectedLegs = new LegArmor((LegArmor)(GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex]));
                    GameState.CurrentHero.Inventory.RemoveItem(selectedLegs);

                    if (GameState.CurrentHero.Equipment.Legs.Name != "Cloth Pants")
                        GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Legs);

                    GameState.CurrentHero.Equipment.Legs = selectedLegs;
                    lblEquippedLegs.DataContext = GameState.CurrentHero.Equipment.Legs;
                    lblEquippedLegsDefense.DataContext = GameState.CurrentHero.Equipment.Legs;
                    break;

                case ItemTypes.Feet:
                    FeetArmor selectedFeet = new FeetArmor((FeetArmor)(GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex]));
                    GameState.CurrentHero.Inventory.RemoveItem(selectedFeet);

                    if (GameState.CurrentHero.Equipment.Feet.Name != "Cloth Shoes")
                        GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Feet);

                    GameState.CurrentHero.Equipment.Feet = selectedFeet;
                    lblEquippedFeet.DataContext = GameState.CurrentHero.Equipment.Feet;
                    lblEquippedFeetDefense.DataContext = GameState.CurrentHero.Equipment.Feet;
                    break;

                default:
                    MessageBox.Show("Somehow you managed to screw up and find an item type named '" + GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex].Type + "'.", "Sulimn", MessageBoxButton.OK);
                    break;
            }
            //FUTURE MORE ITEM TYPES

            DisplayAllInfo();
            btnDrop.IsEnabled = false;
            btnConsume.IsEnabled = false;
            btnEquip.IsEnabled = false;
        }

        private void btnConsume_Click(object sender, RoutedEventArgs e)
        {
            switch (GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex].Type)
            {
                case ItemTypes.Potion:
                    Potion selectedPotion = (Potion)GameState.AllItems.Find(x => x.Name == GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex].Name);
                    GameState.CurrentHero.Inventory.RemoveItem(selectedPotion);

                    switch (selectedPotion.PotionType)
                    {
                        case PotionTypes.Healing:
                            GameState.CurrentHero.Heal(selectedPotion.Amount);
                            break;

                        case PotionTypes.Magic:
                            GameState.CurrentHero.Statistics.RestoreMagic(selectedPotion.Amount);
                            break;

                        case PotionTypes.Curing:
                            break;

                        default:
                            //FUTURE POTION TYPES
                            break;
                    }
                    break;

                case ItemTypes.Food:
                    Food selectedFood = (Food)GameState.AllItems.Find(x => x.Name == GameState.CurrentHero.Inventory.Items[lstInventory.SelectedIndex].Name);
                    GameState.CurrentHero.Inventory.RemoveItem(selectedFood);

                    switch (selectedFood.FoodType)
                    {
                        case FoodTypes.Food:
                            GameState.CurrentHero.Heal(selectedFood.Amount);
                            break;

                        case FoodTypes.Drink:
                            GameState.CurrentHero.Statistics.RestoreMagic(selectedFood.Amount);
                            break;
                    }
                    break;
            }

            DisplayAllInfo();
        }

        #endregion Button-Click Methods

        #region Unequip Button Clicks

        private void btnUnequipHead_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Equipment.Head.Name != "Cloth Helmet")
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Head);
            GameState.CurrentHero.Equipment.Head = (HeadArmor)GameState.AllItems.Find(x => x.Name == "Cloth Helmet");
            lblEquippedHead.DataContext = GameState.CurrentHero.Equipment.Head;
            lblEquippedHeadDefense.DataContext = GameState.CurrentHero.Equipment.Head;
            DisplayAllInfo();
        }

        private void btnUnequipBody_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Equipment.Body.Name != "Cloth Shirt")
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Body);
            GameState.CurrentHero.Equipment.Body = (BodyArmor)GameState.AllItems.Find(x => x.Name == "Cloth Shirt");
            lblEquippedBody.DataContext = GameState.CurrentHero.Equipment.Body;
            lblEquippedBodyDefense.DataContext = GameState.CurrentHero.Equipment.Body;
            DisplayAllInfo();
        }

        private void btnUnequipLegs_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Equipment.Legs.Name != "Cloth Pants")
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Legs);
            GameState.CurrentHero.Equipment.Legs = (LegArmor)GameState.AllItems.Find(x => x.Name == "Cloth Pants");
            lblEquippedLegs.DataContext = GameState.CurrentHero.Equipment.Legs;
            lblEquippedLegsDefense.DataContext = GameState.CurrentHero.Equipment.Legs;
            DisplayAllInfo();
        }

        private void btnUnequipFeet_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Equipment.Feet.Name != "Cloth Shoes")
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Feet);
            GameState.CurrentHero.Equipment.Feet = (FeetArmor)GameState.AllItems.Find(x => x.Name == "Cloth Shoes");
            lblEquippedFeet.DataContext = GameState.CurrentHero.Equipment.Feet;
            lblEquippedFeetDefense.DataContext = GameState.CurrentHero.Equipment.Feet;
            DisplayAllInfo();
        }

        private void btnUnequipWeapon_Click(object sender, RoutedEventArgs e)
        {
            if (GameState.CurrentHero.Equipment.Weapon.Name != "Fists")
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Weapon);
            GameState.CurrentHero.Equipment.Weapon = (Weapon)GameState.AllItems.Find(x => x.Name == "Fists");
            lblEquippedWeapon.DataContext = GameState.CurrentHero.Equipment.Weapon;
            lblEquippedWeaponDamage.DataContext = GameState.CurrentHero.Equipment.Weapon;
            DisplayAllInfo();
        }

        #endregion Unequip Button Clicks

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
        }

        public InventoryWindow()
        {
            InitializeComponent();
            BindLabels();
        }

        private void lstInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplaySelectedInfo();
        }

        private void windowInventory_Closing(object sender, CancelEventArgs e)
        {
            GameState.SaveHero(GameState.CurrentHero);
            RefToCharacterWindow.Show();
        }

        #endregion Window-Manipulation Methods
    }
}