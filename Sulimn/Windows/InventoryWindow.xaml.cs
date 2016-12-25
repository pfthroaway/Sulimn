using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>Interaction logic for NewInventoryWindow.xaml</summary>
    public partial class InventoryWindow : INotifyPropertyChanged
    {
        private readonly string _nl = Environment.NewLine;
        private List<BodyArmor> _inventoryBody = new List<BodyArmor>();
        private List<FeetArmor> _inventoryFeet = new List<FeetArmor>();
        private List<Food> _inventoryFood = new List<Food>();
        private List<HandArmor> _inventoryHands = new List<HandArmor>();
        private List<HeadArmor> _inventoryHead = new List<HeadArmor>();
        private List<LegArmor> _inventoryLegs = new List<LegArmor>();
        private List<Potion> _inventoryPotion = new List<Potion>();
        private List<Ring> _inventoryRing = new List<Ring>();
        private List<Weapon> _inventoryWeapon = new List<Weapon>();
        private BodyArmor _selectedBody = new BodyArmor();
        private FeetArmor _selectedFeet = new FeetArmor();
        private Food _selectedFood = new Food();
        private HandArmor _selectedHands = new HandArmor();
        private HeadArmor _selectedHead = new HeadArmor();
        private LegArmor _selectedLegs = new LegArmor();
        private Potion _selectedPotion = new Potion();
        private Ring _selectedRing = new Ring();
        private Weapon _selectedWeapon = new Weapon();
        internal CharacterWindow RefToCharacterWindow { private get; set; }

        /// <summary>Adds text to the txtInventory Textbox.</summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            if (txtInventory.Text.Length > 0)
                txtInventory.Text += _nl + _nl + newText;
            else
                txtInventory.Text = newText;
            txtInventory.Focus();
            txtInventory.CaretIndex = txtInventory.Text.Length;
            txtInventory.ScrollToEnd();
        }

        /// <summary>Determines if the Hero really wants to drop an Item.</summary>
        /// <param name="dropItem">Item to be dropped</param>
        /// <returns>Returns true if the Item is dropped</returns>
        private bool DropItem(Item dropItem)
        {
            if (new Notification("Are you sure you really want to drop this " + dropItem.Name + "? You won't be able to get it back.",
              "Sulimn", NotificationButtons.YesNo, this).ShowDialog() == true)
            {
                GameState.CurrentHero.Inventory.RemoveItem(dropItem);
                AddTextTT("You drop the " + dropItem.Name + ".");
                return true;
            }
            return false;
        }

        #region Window Button-Click Methods

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Window Button-Click Methods

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindWeapon(bool reload = true)
        {
            if (reload)
            {
                _inventoryWeapon.Clear();
                _inventoryWeapon.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<Weapon>());
                _inventoryWeapon = _inventoryWeapon.OrderBy(weapon => weapon.Value).ToList();
                lstWeaponInventory.UnselectAll();
                lstWeaponInventory.ItemsSource = _inventoryWeapon;
                lstWeaponInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstWeaponInventory.Items.Refresh();
                btnUnequipWeapon.IsEnabled = GameState.CurrentHero.Equipment.Weapon != GameState.DefaultWeapon;
            }
            lblEquippedWeapon.DataContext = GameState.CurrentHero.Equipment.Weapon;
            lblEquippedWeaponDamage.DataContext = GameState.CurrentHero.Equipment.Weapon;
            lblEquippedWeaponValue.DataContext = GameState.CurrentHero.Equipment.Weapon;
            lblEquippedWeaponType.DataContext = GameState.CurrentHero.Equipment.Weapon;
            lblEquippedWeaponSellable.DataContext = GameState.CurrentHero.Equipment.Weapon;
            lblEquippedWeaponDescription.DataContext = GameState.CurrentHero.Equipment.Weapon;
            lblSelectedWeapon.DataContext = _selectedWeapon;
            lblSelectedWeaponDamage.DataContext = _selectedWeapon;
            lblSelectedWeaponValue.DataContext = _selectedWeapon;
            lblSelectedWeaponType.DataContext = _selectedWeapon;
            lblSelectedWeaponSellable.DataContext = _selectedWeapon;
            lblSelectedWeaponDescription.DataContext = _selectedWeapon;
        }

        private void BindHead(bool reload = true)
        {
            if (reload)
            {
                _inventoryHead.Clear();
                _inventoryHead.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<HeadArmor>());
                _inventoryHead = _inventoryHead.OrderBy(armor => armor.Value).ToList();
                lstHeadInventory.UnselectAll();
                lstHeadInventory.ItemsSource = _inventoryHead;
                lstHeadInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstHeadInventory.Items.Refresh();
                btnUnequipHead.IsEnabled = GameState.CurrentHero.Equipment.Head != GameState.DefaultHead;
            }
            lblEquippedHead.DataContext = GameState.CurrentHero.Equipment.Head;
            lblEquippedHeadDefense.DataContext = GameState.CurrentHero.Equipment.Head;
            lblEquippedHeadValue.DataContext = GameState.CurrentHero.Equipment.Head;
            lblEquippedHeadSellable.DataContext = GameState.CurrentHero.Equipment.Head;
            lblEquippedHeadDescription.DataContext = GameState.CurrentHero.Equipment.Head;
            lblSelectedHead.DataContext = _selectedHead;
            lblSelectedHeadDefense.DataContext = _selectedHead;
            lblSelectedHeadValue.DataContext = _selectedHead;
            lblSelectedHeadSellable.DataContext = _selectedHead;
            lblSelectedHeadDescription.DataContext = _selectedHead;
        }

        private void BindBody(bool reload = true)
        {
            if (reload)
            {
                _inventoryBody.Clear();
                _inventoryBody.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<BodyArmor>());
                _inventoryBody = _inventoryBody.OrderBy(armor => armor.Value).ToList();
                lstBodyInventory.UnselectAll();
                lstBodyInventory.ItemsSource = _inventoryBody;
                lstBodyInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstBodyInventory.Items.Refresh();
                btnUnequipBody.IsEnabled = GameState.CurrentHero.Equipment.Body != GameState.DefaultBody;
            }
            lblEquippedBody.DataContext = GameState.CurrentHero.Equipment.Body;
            lblEquippedBodyDefense.DataContext = GameState.CurrentHero.Equipment.Body;
            lblEquippedBodyValue.DataContext = GameState.CurrentHero.Equipment.Body;
            lblEquippedBodySellable.DataContext = GameState.CurrentHero.Equipment.Body;
            lblEquippedBodyDescription.DataContext = GameState.CurrentHero.Equipment.Body;
            lblSelectedBody.DataContext = _selectedBody;
            lblSelectedBodyDefense.DataContext = _selectedBody;
            lblSelectedBodyValue.DataContext = _selectedBody;
            lblSelectedBodySellable.DataContext = _selectedBody;
            lblSelectedBodyDescription.DataContext = _selectedBody;
        }

        private void BindHands(bool reload = true)
        {
            if (reload)
            {
                _inventoryHands.Clear();
                _inventoryHands.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<HandArmor>());
                _inventoryHands = _inventoryHands.OrderBy(armor => armor.Value).ToList();
                lstHandsInventory.UnselectAll();
                lstHandsInventory.ItemsSource = _inventoryHands;
                lstHandsInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstHandsInventory.Items.Refresh();
                btnUnequipHands.IsEnabled = GameState.CurrentHero.Equipment.Hands != GameState.DefaultHands;
            }
            lblEquippedHands.DataContext = GameState.CurrentHero.Equipment.Hands;
            lblEquippedHandsDefense.DataContext = GameState.CurrentHero.Equipment.Hands;
            lblEquippedHandsValue.DataContext = GameState.CurrentHero.Equipment.Hands;
            lblEquippedHandsSellable.DataContext = GameState.CurrentHero.Equipment.Hands;
            lblEquippedHandsDescription.DataContext = GameState.CurrentHero.Equipment.Hands;
            lblSelectedHands.DataContext = _selectedHands;
            lblSelectedHandsDefense.DataContext = _selectedHands;
            lblSelectedHandsValue.DataContext = _selectedHands;
            lblSelectedHandsSellable.DataContext = _selectedHands;
            lblSelectedHandsDescription.DataContext = _selectedHands;
        }

        private void BindLegs(bool reload = true)
        {
            if (reload)
            {
                _inventoryLegs.Clear();
                _inventoryLegs.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<LegArmor>());
                _inventoryLegs = _inventoryLegs.OrderBy(armor => armor.Value).ToList();
                lstLegsInventory.UnselectAll();
                lstLegsInventory.ItemsSource = _inventoryLegs;
                lstLegsInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstLegsInventory.Items.Refresh();
                btnUnequipLegs.IsEnabled = GameState.CurrentHero.Equipment.Legs != GameState.DefaultLegs;
            }
            lblEquippedLegs.DataContext = GameState.CurrentHero.Equipment.Legs;
            lblEquippedLegsDefense.DataContext = GameState.CurrentHero.Equipment.Legs;
            lblEquippedLegsValue.DataContext = GameState.CurrentHero.Equipment.Legs;
            lblEquippedLegsSellable.DataContext = GameState.CurrentHero.Equipment.Legs;
            lblEquippedLegsDescription.DataContext = GameState.CurrentHero.Equipment.Legs;
            lblSelectedLegs.DataContext = _selectedLegs;
            lblSelectedLegsDefense.DataContext = _selectedLegs;
            lblSelectedLegsValue.DataContext = _selectedLegs;
            lblSelectedLegsSellable.DataContext = _selectedLegs;
            lblSelectedLegsDescription.DataContext = _selectedLegs;
        }

        private void BindFeet(bool reload = true)
        {
            if (reload)
            {
                _inventoryFeet.Clear();
                _inventoryFeet.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<FeetArmor>());
                _inventoryFeet = _inventoryFeet.OrderBy(armor => armor.Value).ToList();
                lstFeetInventory.UnselectAll();
                lstFeetInventory.ItemsSource = _inventoryFeet;
                lstFeetInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstFeetInventory.Items.Refresh();
                btnUnequipFeet.IsEnabled = GameState.CurrentHero.Equipment.Feet != GameState.DefaultFeet;
            }
            lblEquippedFeet.DataContext = GameState.CurrentHero.Equipment.Feet;
            lblEquippedFeetDefense.DataContext = GameState.CurrentHero.Equipment.Feet;
            lblEquippedFeetValue.DataContext = GameState.CurrentHero.Equipment.Feet;
            lblEquippedFeetSellable.DataContext = GameState.CurrentHero.Equipment.Feet;
            lblEquippedFeetDescription.DataContext = GameState.CurrentHero.Equipment.Feet;
            lblSelectedFeet.DataContext = _selectedFeet;
            lblSelectedFeetDefense.DataContext = _selectedFeet;
            lblSelectedFeetValue.DataContext = _selectedFeet;
            lblSelectedFeetSellable.DataContext = _selectedFeet;
            lblSelectedFeetDescription.DataContext = _selectedFeet;
        }

        private void BindRing(bool reload = true)
        {
            if (reload)
            {
                _inventoryRing.Clear();
                _inventoryRing.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<Ring>());
                _inventoryRing = _inventoryRing.OrderBy(ring => ring.Value).ToList();
                lstRingInventory.UnselectAll();
                lstRingInventory.ItemsSource = _inventoryRing;
                lstRingInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstRingInventory.Items.Refresh();
                btnUnequipLeftRing.IsEnabled = GameState.CurrentHero.Equipment.LeftRing != new Ring();
                btnUnequipRightRing.IsEnabled = GameState.CurrentHero.Equipment.RightRing != new Ring();
            }
            lblEquippedLeftRing.DataContext = GameState.CurrentHero.Equipment.LeftRing;
            lblEquippedLeftRingBonus.DataContext = GameState.CurrentHero.Equipment.LeftRing;
            lblEquippedLeftRingValue.DataContext = GameState.CurrentHero.Equipment.LeftRing;
            lblEquippedRightRing.DataContext = GameState.CurrentHero.Equipment.RightRing;
            lblEquippedRightRingBonus.DataContext = GameState.CurrentHero.Equipment.RightRing;
            lblEquippedRightRingValue.DataContext = GameState.CurrentHero.Equipment.RightRing;
            lblSelectedRing.DataContext = _selectedRing;
            lblSelectedRingBonus.DataContext = _selectedRing;
            lblSelectedRingValue.DataContext = _selectedRing;
            lblSelectedRingSellable.DataContext = _selectedRing;
            lblSelectedRingDescription.DataContext = _selectedRing;
        }

        private void BindPotion(bool reload = true)
        {
            if (reload)
            {
                _inventoryPotion.Clear();
                _inventoryPotion.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<Potion>());
                _inventoryPotion = _inventoryPotion.OrderBy(potion => potion.Value).ToList();
                lstPotionInventory.UnselectAll();
                lstPotionInventory.ItemsSource = _inventoryPotion;
                lstPotionInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstPotionInventory.Items.Refresh();
            }
            lblSelectedPotion.DataContext = _selectedPotion;
            lblSelectedPotionTypeAmount.DataContext = _selectedPotion;
            lblSelectedPotionValue.DataContext = _selectedPotion;
            lblSelectedPotionSellable.DataContext = _selectedPotion;
            lblSelectedPotionDescription.DataContext = _selectedPotion;
        }

        private void BindFood(bool reload = true)
        {
            if (reload)
            {
                _inventoryFood.Clear();
                _inventoryFood.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<Food>());
                _inventoryFood = _inventoryFood.OrderBy(food => food.Value).ToList();
                lstFoodInventory.UnselectAll();
                lstFoodInventory.ItemsSource = _inventoryFood;
                lstFoodInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                lstFoodInventory.Items.Refresh();
            }
            lblSelectedFood.DataContext = _selectedFood;
            lblSelectedFoodTypeAmount.DataContext = _selectedFood;
            lblSelectedFoodValue.DataContext = _selectedFood;
            lblSelectedFoodSellable.DataContext = _selectedFood;
            lblSelectedFoodDescription.DataContext = _selectedFood;
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

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Weapon-Click

        private void btnUnequipWeapon_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You unequip your " + GameState.CurrentHero.Equipment.Weapon.Name + ".");
            if (GameState.CurrentHero.Equipment.Weapon != GameState.DefaultWeapon)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Weapon);
            GameState.CurrentHero.Equipment.Weapon =
            (Weapon)GameState.AllItems.Find(x => x.Name == GameState.DefaultWeapon.Name);
            BindWeapon();
        }

        private void btnEquipSelectedWeapon_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You equip your " + _selectedWeapon.Name + ".");
            GameState.CurrentHero.Inventory.RemoveItem(_selectedWeapon);
            if (GameState.CurrentHero.Equipment.Weapon != GameState.DefaultWeapon)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Weapon);
            GameState.CurrentHero.Equipment.Weapon = _selectedWeapon;
            BindWeapon();
        }

        private void btnDropSelectedWeapon_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedWeapon))
                BindWeapon();
        }

        private void lstWeaponInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedWeapon = lstWeaponInventory.SelectedIndex >= 0 ? new Weapon((Weapon)lstWeaponInventory.SelectedValue) : new Weapon();
            btnEquipSelectedWeapon.IsEnabled = lstWeaponInventory.SelectedIndex >= 0;
            btnDropSelectedWeapon.IsEnabled = lstWeaponInventory.SelectedIndex >= 0;

            BindWeapon(false);
        }

        #endregion Weapon-Click

        #region Head-Click

        private void btnUnequipHead_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You unequip your " + GameState.CurrentHero.Equipment.Head.Name + ".");
            if (GameState.CurrentHero.Equipment.Head != GameState.DefaultHead)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Head);
            GameState.CurrentHero.Equipment.Head =
            (HeadArmor)GameState.AllItems.Find(x => x.Name == GameState.DefaultHead.Name);
            BindHead();
        }

        private void btnEquipSelectedHead_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You equip your " + _selectedHead.Name + ".");
            GameState.CurrentHero.Inventory.RemoveItem(_selectedHead);
            if (GameState.CurrentHero.Equipment.Head != GameState.DefaultHead)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Head);
            GameState.CurrentHero.Equipment.Head = _selectedHead;
            BindHead();
        }

        private void btnDropSelectedHead_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedHead))
                BindHead();
        }

        private void lstHeadInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedHead = lstHeadInventory.SelectedIndex >= 0 ? new HeadArmor((HeadArmor)lstHeadInventory.SelectedValue) : new HeadArmor();
            btnEquipSelectedHead.IsEnabled = lstHeadInventory.SelectedIndex >= 0;
            btnDropSelectedHead.IsEnabled = lstHeadInventory.SelectedIndex >= 0;

            BindHead(false);
        }

        #endregion Head-Click

        #region Body-Click

        private void btnUnequipBody_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You unequip your " + GameState.CurrentHero.Equipment.Body.Name + ".");
            if (GameState.CurrentHero.Equipment.Body != GameState.DefaultBody)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Body);
            GameState.CurrentHero.Equipment.Body =
            (BodyArmor)GameState.AllItems.Find(x => x.Name == GameState.DefaultBody.Name);
            BindBody();
        }

        private void btnEquipSelectedBody_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You equip your " + _selectedBody.Name + ".");
            GameState.CurrentHero.Inventory.RemoveItem(_selectedBody);
            if (GameState.CurrentHero.Equipment.Body != GameState.DefaultBody)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Body);
            GameState.CurrentHero.Equipment.Body = _selectedBody;
            BindBody();
        }

        private void btnDropSelectedBody_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedBody))
                BindBody();
        }

        private void lstBodyInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedBody = lstBodyInventory.SelectedIndex >= 0 ? new BodyArmor((BodyArmor)lstBodyInventory.SelectedValue) : new BodyArmor();
            btnEquipSelectedBody.IsEnabled = lstBodyInventory.SelectedIndex >= 0;
            btnDropSelectedBody.IsEnabled = lstBodyInventory.SelectedIndex >= 0;

            BindBody(false);
        }

        #endregion Body-Click

        #region Hands-Click

        private void btnUnequipHands_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You unequip your " + GameState.CurrentHero.Equipment.Hands.Name + ".");
            if (GameState.CurrentHero.Equipment.Hands != GameState.DefaultHands)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Hands);
            GameState.CurrentHero.Equipment.Hands =
            (HandArmor)GameState.AllItems.Find(x => x.Name == GameState.DefaultHands.Name);
            BindHands();
        }

        private void btnEquipSelectedHands_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You equip your " + _selectedHands.Name + ".");
            GameState.CurrentHero.Inventory.RemoveItem(_selectedHands);
            if (GameState.CurrentHero.Equipment.Hands != GameState.DefaultHands)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Hands);
            GameState.CurrentHero.Equipment.Hands = _selectedHands;
            BindHands();
        }

        private void btnDropSelectedHands_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedHands))
                BindHands();
        }

        private void lstHandsInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedHands = lstHandsInventory.SelectedIndex >= 0 ? new HandArmor((HandArmor)lstHandsInventory.SelectedValue) : new HandArmor();
            btnEquipSelectedHands.IsEnabled = lstHandsInventory.SelectedIndex >= 0;
            btnDropSelectedHands.IsEnabled = lstHandsInventory.SelectedIndex >= 0;

            BindHands(false);
        }

        #endregion Hands-Click

        #region Legs-Click

        private void btnUnequipLegs_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You unequip your " + GameState.CurrentHero.Equipment.Legs.Name + ".");
            if (GameState.CurrentHero.Equipment.Legs != GameState.DefaultLegs)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Legs);
            GameState.CurrentHero.Equipment.Legs =
            (LegArmor)GameState.AllItems.Find(x => x.Name == GameState.DefaultLegs.Name);
            BindLegs();
        }

        private void btnEquipSelectedLegs_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You equip your " + _selectedLegs.Name + ".");
            GameState.CurrentHero.Inventory.RemoveItem(_selectedLegs);
            if (GameState.CurrentHero.Equipment.Legs != GameState.DefaultLegs)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Legs);
            GameState.CurrentHero.Equipment.Legs = _selectedLegs;
            BindLegs();
        }

        private void btnDropSelectedLegs_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedLegs))
                BindLegs();
        }

        private void lstLegsInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedLegs = lstLegsInventory.SelectedIndex >= 0 ? new LegArmor((LegArmor)lstLegsInventory.SelectedValue) : new LegArmor();
            btnEquipSelectedLegs.IsEnabled = lstLegsInventory.SelectedIndex >= 0;
            btnDropSelectedLegs.IsEnabled = lstLegsInventory.SelectedIndex >= 0;

            BindLegs(false);
        }

        #endregion Legs-Click

        #region Feet-Click

        private void btnUnequipFeet_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You unequip your " + GameState.CurrentHero.Equipment.Feet.Name + ".");
            if (GameState.CurrentHero.Equipment.Feet != GameState.DefaultFeet)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Feet);
            GameState.CurrentHero.Equipment.Feet =
            (FeetArmor)GameState.AllItems.Find(x => x.Name == GameState.DefaultFeet.Name);
            BindFeet();
        }

        private void btnEquipSelectedFeet_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You equip your " + _selectedFeet.Name + ".");
            GameState.CurrentHero.Inventory.RemoveItem(_selectedFeet);
            if (GameState.CurrentHero.Equipment.Feet != GameState.DefaultFeet)
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.Feet);
            GameState.CurrentHero.Equipment.Feet = _selectedFeet;
            BindFeet();
        }

        private void btnDropSelectedFeet_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedFeet))
                BindFeet();
        }

        private void lstFeetInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedFeet = lstFeetInventory.SelectedIndex >= 0 ? new FeetArmor((FeetArmor)lstFeetInventory.SelectedValue) : new FeetArmor();
            btnEquipSelectedFeet.IsEnabled = lstFeetInventory.SelectedIndex >= 0;
            btnDropSelectedFeet.IsEnabled = lstFeetInventory.SelectedIndex >= 0;

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
            GameState.CurrentHero.UpdateStatistics();
            BindRing();
        }

        private void btnUnequipRightRing_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You unequip your " + GameState.CurrentHero.Equipment.RightRing.Name + ".");
            if (GameState.CurrentHero.Equipment.RightRing != new Ring())
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.RightRing);
            GameState.CurrentHero.Equipment.RightRing = new Ring();
            GameState.CurrentHero.UpdateStatistics();
            BindRing();
        }

        private void btnEquipSelectedRingLeft_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You equip your " + _selectedRing.Name + ".");
            GameState.CurrentHero.Inventory.RemoveItem(_selectedRing);
            if (GameState.CurrentHero.Equipment.LeftRing != new Ring())
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.LeftRing);
            GameState.CurrentHero.Equipment.LeftRing = _selectedRing;
            btnEquipSelectedRingLeft.IsEnabled = false;
            GameState.CurrentHero.UpdateStatistics();
            BindRing();
        }

        private void btnEquipSelectedRingRight_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You equip your " + _selectedRing.Name + ".");
            GameState.CurrentHero.Inventory.RemoveItem(_selectedRing);
            if (GameState.CurrentHero.Equipment.RightRing != new Ring())
                GameState.CurrentHero.Inventory.AddItem(GameState.CurrentHero.Equipment.RightRing);
            GameState.CurrentHero.Equipment.RightRing = _selectedRing;
            btnEquipSelectedRingRight.IsEnabled = false;
            GameState.CurrentHero.UpdateStatistics();
            BindRing();
        }

        private void btnDropSelectedRing_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedRing))
                BindRing();
        }

        private void lstRingInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedRing = lstRingInventory.SelectedIndex >= 0 ? new Ring((Ring)lstRingInventory.SelectedValue) : new Ring();
            btnEquipSelectedRingLeft.IsEnabled = lstRingInventory.SelectedIndex >= 0;
            btnEquipSelectedRingRight.IsEnabled = lstRingInventory.SelectedIndex >= 0;
            btnDropSelectedRing.IsEnabled = lstRingInventory.SelectedIndex >= 0;

            BindRing(false);
        }

        #endregion Rings-Click

        #region Potion-Click

        private void btnConsumeSelectedPotion_Click(object sender, RoutedEventArgs e)
        {
            AddTextTT("You consume the " + _selectedPotion.Name + ".");
            switch (_selectedPotion.PotionType)
            {
                case PotionTypes.Healing:
                    AddTextTT(GameState.CurrentHero.Heal(_selectedPotion.Amount));
                    break;

                case PotionTypes.Magic:
                    AddTextTT(GameState.CurrentHero.Statistics.RestoreMagic(_selectedPotion.Amount));
                    break;

                case PotionTypes.Curing:
                    AddTextTT("You are now free of any ailments.");
                    break;
            }

            GameState.CurrentHero.Inventory.RemoveItem(_selectedPotion);
            BindPotion();
        }

        private void btnDropSelectedPotion_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedPotion))
                BindPotion();
        }

        private void lstPotionInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedPotion = lstPotionInventory.SelectedIndex >= 0 ? new Potion((Potion)lstPotionInventory.SelectedValue) : new Potion();
            btnConsumeSelectedPotion.IsEnabled = lstPotionInventory.SelectedIndex >= 0;
            btnDropSelectedPotion.IsEnabled = lstPotionInventory.SelectedIndex >= 0;

            BindPotion(false);
        }

        #endregion Potion-Click

        #region Food-Click

        private void btnConsumeSelectedFood_Click(object sender, RoutedEventArgs e)
        {
            switch (_selectedFood.FoodType)
            {
                case FoodTypes.Food:
                    AddTextTT("You eat the " + _selectedFood.Name + "." + _nl +
                    GameState.CurrentHero.Heal(_selectedFood.Amount));
                    break;

                case FoodTypes.Drink:
                    AddTextTT("You drink the " + _selectedFood.Name + "." + _nl +
                    GameState.CurrentHero.Statistics.RestoreMagic(_selectedFood.Amount));
                    break;
            }

            GameState.CurrentHero.Inventory.RemoveItem(_selectedPotion);
            BindFood();
        }

        private void btnDropSelectedFood_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedFood))
                BindFood();
        }

        private void lstFoodInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedPotion = lstPotionInventory.SelectedIndex >= 0 ? new Potion((Potion)lstPotionInventory.SelectedValue) : new Potion();
            btnConsumeSelectedPotion.IsEnabled = lstPotionInventory.SelectedIndex >= 0;
            btnDropSelectedPotion.IsEnabled = lstPotionInventory.SelectedIndex >= 0;

            BindFood(false);
        }

        #endregion Food-Click

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
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