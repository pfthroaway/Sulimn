using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for NewInventoryWindow.xaml
    /// </summary>
    public partial class InventoryWindow : Window, INotifyPropertyChanged
    {
        private List<Weapon> inventoryWeapon = new List<Weapon>();
        private List<HeadArmor> inventoryHead = new List<HeadArmor>();
        private List<BodyArmor> inventoryBody = new List<BodyArmor>();
        private List<HandArmor> inventoryHands = new List<HandArmor>();
        private List<LegArmor> inventoryLegs = new List<LegArmor>();
        private List<FeetArmor> inventoryFeet = new List<FeetArmor>();
        private List<Ring> inventoryRing = new List<Ring>();
        private List<Potion> inventoryPotion = new List<Potion>();
        private List<Food> inventoryFood = new List<Food>();

        private Weapon selectedWeapon = new Weapon();
        private HeadArmor selectedHead = new HeadArmor();
        private BodyArmor selectedBody = new BodyArmor();
        private HandArmor selectedHands = new HandArmor();
        private LegArmor selectedLegs = new LegArmor();
        private FeetArmor selectedFeet = new FeetArmor();
        private Ring selectedRing = new Ring();
        private Potion selectedPotion = new Potion();
        private Food selectedFood = new Food();

        private string nl = Environment.NewLine;
        internal CharacterWindow RefToCharacterWindow { get; set; }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindWeapon(bool reload = true)
        {
            if (reload)
            {
                inventoryWeapon.Clear();
                inventoryWeapon.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<Weapon>());
                inventoryWeapon = inventoryWeapon.OrderBy(weapon => weapon.Value).ToList();
                lstWeaponInventory.ItemsSource = inventoryWeapon;
                lstWeaponInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstWeaponInventory.Items.Refresh();

                if (GameState.CurrentHero.Equipment.Weapon != GameState.DefaultWeapon)
                    btnUnequipWeapon.IsEnabled = true;
                else
                    btnUnequipWeapon.IsEnabled = false;
            }
            lblEquippedWeapon.DataContext = GameState.CurrentHero.Equipment.Weapon;
            lblEquippedWeaponDamage.DataContext = GameState.CurrentHero.Equipment.Weapon;
            lblEquippedWeaponValue.DataContext = GameState.CurrentHero.Equipment.Weapon;
            lblEquippedWeaponType.DataContext = GameState.CurrentHero.Equipment.Weapon;
            lblEquippedWeaponSellable.DataContext = GameState.CurrentHero.Equipment.Weapon;
            lblEquippedWeaponDescription.DataContext = GameState.CurrentHero.Equipment.Weapon;
            lblSelectedWeapon.DataContext = selectedWeapon;
            lblSelectedWeaponDamage.DataContext = selectedWeapon;
            lblSelectedWeaponValue.DataContext = selectedWeapon;
            lblSelectedWeaponType.DataContext = selectedWeapon;
            lblSelectedWeaponSellable.DataContext = selectedWeapon;
            lblSelectedWeaponDescription.DataContext = selectedWeapon;
        }

        private void BindHead(bool reload = true)
        {
            if (reload)
            {
                inventoryHead.Clear();
                inventoryHead.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<HeadArmor>());
                inventoryHead = inventoryHead.OrderBy(armor => armor.Value).ToList();
                lstHeadInventory.ItemsSource = inventoryHead;
                lstHeadInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstHeadInventory.Items.Refresh();

                if (GameState.CurrentHero.Equipment.Head != GameState.DefaultHead)
                    btnUnequipHead.IsEnabled = true;
                else
                    btnUnequipHead.IsEnabled = false;
            }
            lblEquippedHead.DataContext = GameState.CurrentHero.Equipment.Head;
            lblEquippedHeadDefense.DataContext = GameState.CurrentHero.Equipment.Head;
            lblEquippedHeadValue.DataContext = GameState.CurrentHero.Equipment.Head;
            lblEquippedHeadSellable.DataContext = GameState.CurrentHero.Equipment.Head;
            lblEquippedHeadDescription.DataContext = GameState.CurrentHero.Equipment.Head;
            lblSelectedHead.DataContext = selectedHead;
            lblSelectedHeadDefense.DataContext = selectedHead;
            lblSelectedHeadValue.DataContext = selectedHead;
            lblSelectedHeadSellable.DataContext = selectedHead;
            lblSelectedHeadDescription.DataContext = selectedHead;
        }

        private void BindBody(bool reload = true)
        {
            if (reload)
            {
                inventoryBody.Clear();
                inventoryBody.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<BodyArmor>());
                inventoryBody = inventoryBody.OrderBy(armor => armor.Value).ToList();
                lstBodyInventory.ItemsSource = inventoryBody;
                lstBodyInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstBodyInventory.Items.Refresh();

                if (GameState.CurrentHero.Equipment.Body != GameState.DefaultBody)
                    btnUnequipBody.IsEnabled = true;
                else
                    btnUnequipBody.IsEnabled = false;
            }
            lblEquippedBody.DataContext = GameState.CurrentHero.Equipment.Body;
            lblEquippedBodyDefense.DataContext = GameState.CurrentHero.Equipment.Body;
            lblEquippedBodyValue.DataContext = GameState.CurrentHero.Equipment.Body;
            lblEquippedBodySellable.DataContext = GameState.CurrentHero.Equipment.Body;
            lblEquippedBodyDescription.DataContext = GameState.CurrentHero.Equipment.Body;
            lblSelectedBody.DataContext = selectedBody;
            lblSelectedBodyDefense.DataContext = selectedBody;
            lblSelectedBodyValue.DataContext = selectedBody;
            lblSelectedBodySellable.DataContext = selectedBody;
            lblSelectedBodyDescription.DataContext = selectedBody;
        }

        private void BindHands(bool reload = true)
        {
            if (reload)
            {
                inventoryHands.Clear();
                inventoryHands.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<HandArmor>());
                inventoryHands = inventoryHands.OrderBy(armor => armor.Value).ToList();
                lstHandsInventory.ItemsSource = inventoryHands;
                lstHandsInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstHandsInventory.Items.Refresh();

                if (GameState.CurrentHero.Equipment.Hands != GameState.DefaultHands)
                    btnUnequipHands.IsEnabled = true;
                else
                    btnUnequipHands.IsEnabled = false;
            }
            lblEquippedHands.DataContext = GameState.CurrentHero.Equipment.Hands;
            lblEquippedHandsDefense.DataContext = GameState.CurrentHero.Equipment.Hands;
            lblEquippedHandsValue.DataContext = GameState.CurrentHero.Equipment.Hands;
            lblEquippedHandsSellable.DataContext = GameState.CurrentHero.Equipment.Hands;
            lblEquippedHandsDescription.DataContext = GameState.CurrentHero.Equipment.Hands;
            lblSelectedHands.DataContext = selectedHands;
            lblSelectedHandsDefense.DataContext = selectedHands;
            lblSelectedHandsValue.DataContext = selectedHands;
            lblSelectedHandsSellable.DataContext = selectedHands;
            lblSelectedHandsDescription.DataContext = selectedHands;
        }

        private void BindLegs(bool reload = true)
        {
            if (reload)
            {
                inventoryLegs.Clear();
                inventoryLegs.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<LegArmor>());
                inventoryLegs = inventoryLegs.OrderBy(armor => armor.Value).ToList();
                lstLegsInventory.ItemsSource = inventoryLegs;
                lstLegsInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstLegsInventory.Items.Refresh();

                if (GameState.CurrentHero.Equipment.Legs != GameState.DefaultLegs)
                    btnUnequipLegs.IsEnabled = true;
                else
                    btnUnequipLegs.IsEnabled = false;
            }
            lblEquippedLegs.DataContext = GameState.CurrentHero.Equipment.Legs;
            lblEquippedLegsDefense.DataContext = GameState.CurrentHero.Equipment.Legs;
            lblEquippedLegsValue.DataContext = GameState.CurrentHero.Equipment.Legs;
            lblEquippedLegsSellable.DataContext = GameState.CurrentHero.Equipment.Legs;
            lblEquippedLegsDescription.DataContext = GameState.CurrentHero.Equipment.Legs;
            lblSelectedLegs.DataContext = selectedLegs;
            lblSelectedLegsDefense.DataContext = selectedLegs;
            lblSelectedLegsValue.DataContext = selectedLegs;
            lblSelectedLegsSellable.DataContext = selectedLegs;
            lblSelectedLegsDescription.DataContext = selectedLegs;
        }

        private void BindFeet(bool reload = true)
        {
            if (reload)
            {
                inventoryFeet.Clear();
                inventoryFeet.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<FeetArmor>());
                inventoryFeet = inventoryFeet.OrderBy(armor => armor.Value).ToList();
                lstFeetInventory.ItemsSource = inventoryFeet;
                lstFeetInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstFeetInventory.Items.Refresh();

                if (GameState.CurrentHero.Equipment.Feet != GameState.DefaultFeet)
                    btnUnequipFeet.IsEnabled = true;
                else
                    btnUnequipFeet.IsEnabled = false;
            }
            lblEquippedFeet.DataContext = GameState.CurrentHero.Equipment.Feet;
            lblEquippedFeetDefense.DataContext = GameState.CurrentHero.Equipment.Feet;
            lblEquippedFeetValue.DataContext = GameState.CurrentHero.Equipment.Feet;
            lblEquippedFeetSellable.DataContext = GameState.CurrentHero.Equipment.Feet;
            lblEquippedFeetDescription.DataContext = GameState.CurrentHero.Equipment.Feet;
            lblSelectedFeet.DataContext = selectedFeet;
            lblSelectedFeetDefense.DataContext = selectedFeet;
            lblSelectedFeetValue.DataContext = selectedFeet;
            lblSelectedFeetSellable.DataContext = selectedFeet;
            lblSelectedFeetDescription.DataContext = selectedFeet;
        }

        private void BindRing(bool reload = true)
        {
            if (reload)
            {
                inventoryRing.Clear();
                inventoryRing.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<Ring>());
                inventoryRing = inventoryRing.OrderBy(ring => ring.Value).ToList();
                lstRingInventory.ItemsSource = inventoryRing;
                lstRingInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstRingInventory.Items.Refresh();

                if (GameState.CurrentHero.Equipment.LeftRing != new Ring())
                    btnUnequipLeftRing.IsEnabled = true;
                else
                    btnUnequipLeftRing.IsEnabled = false;

                if (GameState.CurrentHero.Equipment.RightRing != new Ring())
                    btnUnequipRightRing.IsEnabled = true;
                else
                    btnUnequipRightRing.IsEnabled = false;
            }
            lblEquippedLeftRing.DataContext = GameState.CurrentHero.Equipment.LeftRing;
            lblEquippedLeftRingBonus.DataContext = GameState.CurrentHero.Equipment.LeftRing;
            lblEquippedLeftRingValue.DataContext = GameState.CurrentHero.Equipment.LeftRing;
            lblEquippedRightRing.DataContext = GameState.CurrentHero.Equipment.RightRing;
            lblEquippedRightRingBonus.DataContext = GameState.CurrentHero.Equipment.RightRing;
            lblEquippedRightRingValue.DataContext = GameState.CurrentHero.Equipment.RightRing;
            lblSelectedRing.DataContext = selectedRing;
            lblSelectedRingBonus.DataContext = selectedRing;
            lblSelectedRingValue.DataContext = selectedRing;
            lblSelectedRingSellable.DataContext = selectedRing;
            lblSelectedRingDescription.DataContext = selectedRing;
        }

        private void BindPotion(bool reload = true)
        {
            if (reload)
            {
                inventoryPotion.Clear();
                inventoryPotion.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<Potion>());
                inventoryPotion = inventoryPotion.OrderBy(potion => potion.Value).ToList();
                lstPotionInventory.ItemsSource = inventoryPotion;
                lstPotionInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstPotionInventory.Items.Refresh();
            }
            lblSelectedPotion.DataContext = selectedPotion;
            lblSelectedPotionTypeAmount.DataContext = selectedPotion;
            lblSelectedPotionValue.DataContext = selectedPotion;
            lblSelectedPotionSellable.DataContext = selectedPotion;
            lblSelectedPotionDescription.DataContext = selectedPotion;
        }

        private void BindFood(bool reload = true)
        {
            if (reload)
            {
                inventoryFood.Clear();
                inventoryFood.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<Food>());
                inventoryFood = inventoryFood.OrderBy(food => food.Value).ToList();
                lstFoodInventory.ItemsSource = inventoryFood;
                lstFoodInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstFoodInventory.Items.Refresh();
            }
            lblSelectedFood.DataContext = selectedFood;
            lblSelectedFoodTypeAmount.DataContext = selectedFood;
            lblSelectedFoodValue.DataContext = selectedFood;
            lblSelectedFoodSellable.DataContext = selectedFood;
            lblSelectedFoodDescription.DataContext = selectedFood;
        }

        private void BindLabels()
        {
            BindWeapon();
            BindHead();
            BindBody();
            BindHands();
            BindLegs();
            BindFeet();
            BindRing();
            BindPotion();
            BindFood();

            DataContext = GameState.CurrentHero;
            lblHealth.DataContext = GameState.CurrentHero.Statistics;
            lblMagic.DataContext = GameState.CurrentHero.Statistics;
            lblGold.DataContext = GameState.CurrentHero.Inventory;
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        /// <summary>
        /// Adds text to the txtInventory Textbox.
        /// </summary>
        /// <param name="newText">Text to be added</param>
        internal void AddTextTT(string newText)
        {
            if (txtInventory.Text.Length > 0)
                txtInventory.Text += nl + nl + newText;
            else
                txtInventory.Text = newText;
            txtInventory.Focus();
            txtInventory.CaretIndex = txtInventory.Text.Length;
            txtInventory.ScrollToEnd();
        }

        #region Weapon-Click

        private void btnUnequipWeapon_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You unequip your " + GameState.CurrentHero.Equipment.Weapon.Name + ".");
            if (GameState.CurrentHero.Equipment.Weapon != GameState.DefaultWeapon)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Weapon);
            GameState.CurrentHero.Equipment.Weapon = (Weapon)GameState.AllItems.Find(x => x.Name == GameState.DefaultWeapon.Name);
            BindWeapon();
        }

        private void btnEquipSelectedWeapon_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You equip your " + selectedWeapon.Name + ".");
            GameState.CurrentHero.Inventory.RemoveItem(selectedWeapon);
            if (GameState.CurrentHero.Equipment.Weapon != GameState.DefaultWeapon)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Weapon);
            GameState.CurrentHero.Equipment.Weapon = selectedWeapon;
            BindWeapon();
        }

        private void btnDropSelectedWeapon_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dropItem = MessageBox.Show("Are you sure you really want to drop this item? You won't be able to get it back.", "Sulimn", MessageBoxButton.YesNo);

            if (dropItem == MessageBoxResult.Yes)
            {
                GameState.CurrentHero.Inventory.RemoveItem(selectedWeapon);
                AddTextTT("You drop the " + selectedWeapon.Name + ".");
                BindWeapon();
            }
        }

        private void lstWeaponInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstWeaponInventory.SelectedIndex >= 0)
            {
                selectedWeapon = new Weapon((Weapon)lstWeaponInventory.SelectedValue);
                btnEquipSelectedWeapon.IsEnabled = true;
                btnDropSelectedWeapon.IsEnabled = true;
            }
            else
            {
                selectedWeapon = new Weapon();
                btnEquipSelectedWeapon.IsEnabled = false;
                btnDropSelectedWeapon.IsEnabled = false;
            }

            BindWeapon(false);
        }

        #endregion Weapon-Click

        #region Head-Click

        private void btnUnequipHead_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You unequip your " + GameState.CurrentHero.Equipment.Head.Name + ".");
            if (GameState.CurrentHero.Equipment.Head != GameState.DefaultHead)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Head);
            GameState.CurrentHero.Equipment.Head = (HeadArmor)GameState.AllItems.Find(x => x.Name == GameState.DefaultHead.Name);
            BindHead();
        }

        private void btnEquipSelectedHead_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You equip your " + selectedHead.Name + ".");
            GameState.CurrentHero.Inventory.RemoveItem(selectedHead);
            if (GameState.CurrentHero.Equipment.Head != GameState.DefaultHead)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Head);
            GameState.CurrentHero.Equipment.Head = selectedHead;
            BindHead();
        }

        private void btnDropSelectedHead_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dropItem = MessageBox.Show("Are you sure you really want to drop this item? You won't be able to get it back.", "Sulimn", MessageBoxButton.YesNo);

            if (dropItem == MessageBoxResult.Yes)
            {
                GameState.CurrentHero.Inventory.RemoveItem(selectedHead);
                AddTextTT("You drop the " + selectedHead.Name + ".");
                BindHead();
            }
        }

        private void lstHeadInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstHeadInventory.SelectedIndex >= 0)
            {
                selectedHead = new HeadArmor((HeadArmor)lstHeadInventory.SelectedValue);
                btnEquipSelectedHead.IsEnabled = true;
                btnDropSelectedHead.IsEnabled = true;
            }
            else
            {
                selectedHead = new HeadArmor();
                btnEquipSelectedHead.IsEnabled = false;
                btnDropSelectedHead.IsEnabled = false;
            }

            BindHead(false);
        }

        #endregion Head-Click

        #region Body-Click

        private void btnUnequipBody_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You unequip your " + GameState.CurrentHero.Equipment.Body.Name + ".");
            if (GameState.CurrentHero.Equipment.Body != GameState.DefaultBody)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Body);
            GameState.CurrentHero.Equipment.Body = (BodyArmor)GameState.AllItems.Find(x => x.Name == GameState.DefaultBody.Name);
            BindBody();
        }

        private void btnEquipSelectedBody_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You equip your " + selectedBody.Name + ".");
            GameState.CurrentHero.Inventory.RemoveItem(selectedBody);
            if (GameState.CurrentHero.Equipment.Body != GameState.DefaultBody)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Body);
            GameState.CurrentHero.Equipment.Body = selectedBody;
            BindBody();
        }

        private void btnDropSelectedBody_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dropItem = MessageBox.Show("Are you sure you really want to drop this item? You won't be able to get it back.", "Sulimn", MessageBoxButton.YesNo);

            if (dropItem == MessageBoxResult.Yes)
            {
                GameState.CurrentHero.Inventory.RemoveItem(selectedBody);
                AddTextTT("You drop the " + selectedBody.Name + ".");
                BindBody();
            }
        }

        private void lstBodyInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBodyInventory.SelectedIndex >= 0)
            {
                selectedBody = new BodyArmor((BodyArmor)lstBodyInventory.SelectedValue);
                btnEquipSelectedBody.IsEnabled = true;
                btnDropSelectedBody.IsEnabled = true;
            }
            else
            {
                selectedBody = new BodyArmor();
                btnEquipSelectedBody.IsEnabled = false;
                btnDropSelectedBody.IsEnabled = false;
            }

            BindBody(false);
        }

        #endregion Body-Click

        #region Hands-Click

        private void btnUnequipHands_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You unequip your " + GameState.CurrentHero.Equipment.Hands.Name + ".");
            if (GameState.CurrentHero.Equipment.Hands != GameState.DefaultHands)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Hands);
            GameState.CurrentHero.Equipment.Hands = (HandArmor)GameState.AllItems.Find(x => x.Name == GameState.DefaultHands.Name);
            BindHands();
        }

        private void btnEquipSelectedHands_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You equip your " + selectedHands.Name + ".");
            GameState.CurrentHero.Inventory.RemoveItem(selectedHands);
            if (GameState.CurrentHero.Equipment.Hands != GameState.DefaultHands)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Hands);
            GameState.CurrentHero.Equipment.Hands = selectedHands;
            BindHands();
        }

        private void btnDropSelectedHands_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dropItem = MessageBox.Show("Are you sure you really want to drop this item? You won't be able to get it back.", "Sulimn", MessageBoxButton.YesNo);

            if (dropItem == MessageBoxResult.Yes)
            {
                GameState.CurrentHero.Inventory.RemoveItem(selectedHands);
                AddTextTT("You drop the " + selectedHands.Name + ".");
                BindHands();
            }
        }

        private void lstHandsInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstHandsInventory.SelectedIndex >= 0)
            {
                selectedHands = new HandArmor((HandArmor)lstHandsInventory.SelectedValue);
                btnEquipSelectedHands.IsEnabled = true;
                btnDropSelectedHands.IsEnabled = true;
            }
            else
            {
                selectedHands = new HandArmor();
                btnEquipSelectedHands.IsEnabled = false;
                btnDropSelectedHands.IsEnabled = false;
            }

            BindHands(false);
        }

        #endregion Hands-Click

        #region Legs-Click

        private void btnUnequipLegs_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You unequip your " + GameState.CurrentHero.Equipment.Legs.Name + ".");
            if (GameState.CurrentHero.Equipment.Legs != GameState.DefaultLegs)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Legs);
            GameState.CurrentHero.Equipment.Legs = (LegArmor)GameState.AllItems.Find(x => x.Name == GameState.DefaultLegs.Name);
            BindLegs();
        }

        private void btnEquipSelectedLegs_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You equip your " + selectedLegs.Name + ".");
            GameState.CurrentHero.Inventory.RemoveItem(selectedLegs);
            if (GameState.CurrentHero.Equipment.Legs != GameState.DefaultLegs)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Legs);
            GameState.CurrentHero.Equipment.Legs = selectedLegs;
            BindLegs();
        }

        private void btnDropSelectedLegs_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dropItem = MessageBox.Show("Are you sure you really want to drop this item? You won't be able to get it back.", "Sulimn", MessageBoxButton.YesNo);

            if (dropItem == MessageBoxResult.Yes)
            {
                GameState.CurrentHero.Inventory.RemoveItem(selectedLegs);
                AddTextTT("You drop the " + selectedLegs.Name + ".");
                BindLegs();
            }
        }

        private void lstLegsInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstLegsInventory.SelectedIndex >= 0)
            {
                selectedLegs = new LegArmor((LegArmor)lstLegsInventory.SelectedValue);
                btnEquipSelectedLegs.IsEnabled = true;
                btnDropSelectedLegs.IsEnabled = true;
            }
            else
            {
                selectedLegs = new LegArmor();
                btnEquipSelectedLegs.IsEnabled = false;
                btnDropSelectedLegs.IsEnabled = false;
            }

            BindLegs(false);
        }

        #endregion Legs-Click

        #region Feet-Click

        private void btnUnequipFeet_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You unequip your " + GameState.CurrentHero.Equipment.Feet.Name + ".");
            if (GameState.CurrentHero.Equipment.Feet != GameState.DefaultFeet)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Feet);
            GameState.CurrentHero.Equipment.Feet = (FeetArmor)GameState.AllItems.Find(x => x.Name == GameState.DefaultFeet.Name);
            BindFeet();
        }

        private void btnEquipSelectedFeet_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You equip your " + selectedFeet.Name + ".");
            GameState.CurrentHero.Inventory.RemoveItem(selectedFeet);
            if (GameState.CurrentHero.Equipment.Feet != GameState.DefaultFeet)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Feet);
            GameState.CurrentHero.Equipment.Feet = selectedFeet;
            BindFeet();
        }

        private void btnDropSelectedFeet_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dropItem = MessageBox.Show("Are you sure you really want to drop this item? You won't be able to get it back.", "Sulimn", MessageBoxButton.YesNo);

            if (dropItem == MessageBoxResult.Yes)
            {
                GameState.CurrentHero.Inventory.RemoveItem(selectedFeet);
                AddTextTT("You drop the " + selectedFeet.Name + ".");
                BindFeet();
            }
        }

        private void lstFeetInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFeetInventory.SelectedIndex >= 0)
            {
                selectedFeet = new FeetArmor((FeetArmor)lstFeetInventory.SelectedValue);
                btnEquipSelectedFeet.IsEnabled = true;
                btnDropSelectedFeet.IsEnabled = true;
            }
            else
            {
                selectedFeet = new FeetArmor();
                btnEquipSelectedFeet.IsEnabled = false;
                btnDropSelectedFeet.IsEnabled = false;
            }

            BindFeet(false);
        }

        #endregion Feet-Click

        #region Rings-Click

        private void btnUnequipLeftRing_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You unequip your " + GameState.CurrentHero.Equipment.LeftRing.Name + ".");
            if (GameState.CurrentHero.Equipment.LeftRing != new Ring())
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.LeftRing);
            GameState.CurrentHero.Equipment.LeftRing = new Ring();
            BindRing();
        }

        private void btnUnequipRightRing_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You unequip your " + GameState.CurrentHero.Equipment.RightRing.Name + ".");
            if (GameState.CurrentHero.Equipment.RightRing != new Ring())
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.RightRing);
            GameState.CurrentHero.Equipment.RightRing = new Ring();
            BindRing();
        }

        private void btnEquipSelectedRingLeft_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You equip your " + selectedRing.Name + ".");
            GameState.CurrentHero.Inventory.RemoveItem(selectedRing);
            if (GameState.CurrentHero.Equipment.LeftRing != new Ring())
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.LeftRing);
            GameState.CurrentHero.Equipment.LeftRing = selectedRing;
            btnEquipSelectedRingLeft.IsEnabled = false;
            BindRing();
        }

        private void btnEquipSelectedRingRight_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You equip your " + selectedRing.Name + ".");
            GameState.CurrentHero.Inventory.RemoveItem(selectedRing);
            if (GameState.CurrentHero.Equipment.RightRing != new Ring())
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.RightRing);
            GameState.CurrentHero.Equipment.RightRing = selectedRing;
            btnEquipSelectedRingRight.IsEnabled = false;
            BindRing();
        }

        private void btnDropSelectedRing_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dropItem = MessageBox.Show("Are you sure you really want to drop this item? You won't be able to get it back.", "Sulimn", MessageBoxButton.YesNo);

            if (dropItem == MessageBoxResult.Yes)
            {
                GameState.CurrentHero.Inventory.RemoveItem(selectedRing);
                AddTextTT("You drop the " + selectedRing.Name + ".");
                BindRing();
            }
        }

        private void lstRingInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstRingInventory.SelectedIndex >= 0)
            {
                selectedRing = new Ring((Ring)lstRingInventory.SelectedValue);
                btnEquipSelectedRingLeft.IsEnabled = true;
                btnEquipSelectedRingRight.IsEnabled = true;
                btnDropSelectedRing.IsEnabled = true;
            }
            else
            {
                selectedRing = new Ring();
                btnEquipSelectedRingLeft.IsEnabled = false;
                btnEquipSelectedRingRight.IsEnabled = false;
                btnDropSelectedRing.IsEnabled = false;
            }

            BindRing(false);
        }

        #endregion Rings-Click

        #region Potion-Click

        private void btnConsumeSelectedPotion_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You consume the " + selectedPotion.Name + ".");
            switch (selectedPotion.PotionType)
            {
                case PotionTypes.Healing:
                    AddText(GameState.CurrentHero.Heal(selectedPotion.Amount));
                    break;

                case PotionTypes.Magic:
                    AddText(GameState.CurrentHero.Statistics.RestoreMagic(selectedPotion.Amount));
                    break;

                case PotionTypes.Curing:
                    AddText("You are now free of any ailments.");
                    break;
            }

            GameState.CurrentHero.Inventory.RemoveItem(selectedPotion);
            BindPotion();
        }

        private void btnDropSelectedPotion_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dropItem = MessageBox.Show("Are you sure you really want to drop this item? You won't be able to get it back.", "Sulimn", MessageBoxButton.YesNo);

            if (dropItem == MessageBoxResult.Yes)
            {
                GameState.CurrentHero.Inventory.RemoveItem(selectedPotion);
                AddTextTT("You drop the " + selectedPotion.Name + ".");
                BindPotion();
            }
        }

        private void lstPotionInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstPotionInventory.SelectedIndex >= 0)
            {
                selectedPotion = new Potion((Potion)lstPotionInventory.SelectedValue);
                btnConsumeSelectedPotion.IsEnabled = true;
                btnDropSelectedPotion.IsEnabled = true;
            }
            else
            {
                selectedPotion = new Potion();
                btnConsumeSelectedPotion.IsEnabled = false;
                btnDropSelectedPotion.IsEnabled = false;
            }

            BindPotion(false);
        }

        #endregion Potion-Click

        #region Food-Click

        private void btnConsumeSelectedFood_Click(object sender, RoutedEventArgs e)
        {
            switch (selectedFood.FoodType)
            {
                case FoodTypes.Food:
                    AddTextTT("You eat the " + selectedFood.Name + "." + nl + GameState.CurrentHero.Heal(selectedFood.Amount));
                    break;

                case FoodTypes.Drink:
                    AddTextTT("You drink the " + selectedFood.Name + "." + nl + GameState.CurrentHero.Statistics.RestoreMagic(selectedFood.Amount));
                    break;
            }

            GameState.CurrentHero.Inventory.RemoveItem(selectedPotion);
            BindFood();
        }

        private void btnDropSelectedFood_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dropItem = MessageBox.Show("Are you sure you really want to drop this item? You won't be able to get it back.", "Sulimn", MessageBoxButton.YesNo);

            if (dropItem == MessageBoxResult.Yes)
            {
                GameState.CurrentHero.Inventory.RemoveItem(selectedFood);
                AddTextTT("You drop the " + selectedFood.Name + ".");
                BindFood();
            }
        }

        private void lstFoodInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFoodInventory.SelectedIndex >= 0)
            {
                selectedFood = new Food((Food)lstFoodInventory.SelectedValue);
                btnConsumeSelectedFood.IsEnabled = true;
                btnDropSelectedFood.IsEnabled = true;
            }
            else
            {
                selectedFood = new Food();
                btnConsumeSelectedFood.IsEnabled = false;
                btnDropSelectedFood.IsEnabled = false;
            }

            BindFood(false);
        }

        #endregion Food-Click

        #region Window Button-Click Methods

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Window Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            this.Close();
        }

        public InventoryWindow()
        {
            InitializeComponent();
            BindLabels();
        }

        private async void windowInventory_Closing(object sender, CancelEventArgs e)
        {
            RefToCharacterWindow.Show();
            await GameState.SaveHero(GameState.CurrentHero);
        }

        #endregion Window-Manipulation Methods
    }
}