using Extensions;
using Sulimn.Classes;
using Sulimn.Classes.Enums;
using Sulimn.Classes.Items;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn.Views.Characters
{
    /// <summary>Interaction logic for InventoryPage.xaml</summary>
    public partial class InventoryPage : INotifyPropertyChanged
    {
        private List<Item> _inventoryBody = new List<Item>();
        private List<Item> _inventoryFeet = new List<Item>();
        private List<Item> _inventoryFood = new List<Item>();
        private List<Item> _inventoryDrinks = new List<Item>();
        private List<Item> _inventoryHands = new List<Item>();
        private List<Item> _inventoryHead = new List<Item>();
        private List<Item> _inventoryLegs = new List<Item>();
        private List<Item> _inventoryPotion = new List<Item>();
        private List<Item> _inventoryRing = new List<Item>();
        private List<Item> _inventoryWeapon = new List<Item>();
        private Item _selectedBody = new Item();
        private Item _selectedFeet = new Item();
        private Item _selectedFood = new Item();
        private Item _selectedDrink = new Item();
        private Item _selectedHands = new Item();
        private Item _selectedHead = new Item();
        private Item _selectedLegs = new Item();
        private Item _selectedPotion = new Item();
        private Item _selectedRing = new Item();
        private Item _selectedWeapon = new Item();

        /// <summary>Determines if the Hero really wants to drop an Item.</summary>
        /// <param name="dropItem">Item to be dropped</param>
        /// <returns>Returns true if the Item is dropped</returns>
        private bool DropItem(Item dropItem)
        {
            if (GameState.YesNoNotification($"Are you sure you really want to drop {dropItem.Name}? You won't be able to get it back.", "Sulimn"))
            {
                GameState.CurrentHero.RemoveItem(dropItem);
                Functions.AddTextToTextBox(TxtInventory, $"You drop the {dropItem.Name}.");
                return true;
            }
            return false;
        }

        #region Page Button-Click Methods

        private void BtnBack_Click(object sender, RoutedEventArgs e) => ClosePage();

        #endregion Page Button-Click Methods

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void BindWeapon(bool reload = true)
        {
            if (reload)
            {
                _inventoryWeapon.Clear();
                _inventoryWeapon.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.MeleeWeapon));
                _inventoryWeapon.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.RangedWeapon));
                _inventoryWeapon = _inventoryWeapon.OrderBy(weapon => weapon.Value).ToList();
                LstWeaponInventory.UnselectAll();
                LstWeaponInventory.ItemsSource = _inventoryWeapon;
                LstWeaponInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                LstWeaponInventory.Items.Refresh();
                BtnUnequipWeapon.IsEnabled = GameState.CurrentHero.Equipment.Weapon != GameState.DefaultWeapon;
            }
            LblEquippedWeapon.DataContext = GameState.CurrentHero.Equipment.Weapon;
            LblEquippedWeaponDamage.DataContext = GameState.CurrentHero.Equipment.Weapon;
            LblEquippedWeaponValue.DataContext = GameState.CurrentHero.Equipment.Weapon;
            LblEquippedWeaponType.DataContext = GameState.CurrentHero.Equipment.Weapon;
            LblEquippedWeaponSellable.DataContext = GameState.CurrentHero.Equipment.Weapon;
            LblEquippedWeaponDescription.DataContext = GameState.CurrentHero.Equipment.Weapon;
            LblSelectedWeapon.DataContext = _selectedWeapon;
            LblSelectedWeaponDamage.DataContext = _selectedWeapon;
            LblSelectedWeaponValue.DataContext = _selectedWeapon;
            LblSelectedWeaponType.DataContext = _selectedWeapon;
            LblSelectedWeaponSellable.DataContext = _selectedWeapon;
            LblSelectedWeaponDescription.DataContext = _selectedWeapon;
        }

        private void BindHead(bool reload = true)
        {
            if (reload)
            {
                _inventoryHead.Clear();
                _inventoryHead.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.HeadArmor));
                _inventoryHead = _inventoryHead.OrderBy(armor => armor.Value).ToList();
                LstHeadInventory.UnselectAll();
                LstHeadInventory.ItemsSource = _inventoryHead;
                LstHeadInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                LstHeadInventory.Items.Refresh();
                BtnUnequipHead.IsEnabled = GameState.CurrentHero.Equipment.Head != GameState.DefaultHead;
            }
            LblEquippedHead.DataContext = GameState.CurrentHero.Equipment.Head;
            LblEquippedHeadDefense.DataContext = GameState.CurrentHero.Equipment.Head;
            LblEquippedHeadValue.DataContext = GameState.CurrentHero.Equipment.Head;
            LblEquippedHeadSellable.DataContext = GameState.CurrentHero.Equipment.Head;
            LblEquippedHeadDescription.DataContext = GameState.CurrentHero.Equipment.Head;
            LblSelectedHead.DataContext = _selectedHead;
            LblSelectedHeadDefense.DataContext = _selectedHead;
            LblSelectedHeadValue.DataContext = _selectedHead;
            LblSelectedHeadSellable.DataContext = _selectedHead;
            LblSelectedHeadDescription.DataContext = _selectedHead;
        }

        private void BindBody(bool reload = true)
        {
            if (reload)
            {
                _inventoryBody.Clear();
                _inventoryBody.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.BodyArmor));
                _inventoryBody = _inventoryBody.OrderBy(armor => armor.Value).ToList();
                LstBodyInventory.UnselectAll();
                LstBodyInventory.ItemsSource = _inventoryBody;
                LstBodyInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                LstBodyInventory.Items.Refresh();
                BtnUnequipBody.IsEnabled = GameState.CurrentHero.Equipment.Body != GameState.DefaultBody;
            }
            LblEquippedBody.DataContext = GameState.CurrentHero.Equipment.Body;
            LblEquippedBodyDefense.DataContext = GameState.CurrentHero.Equipment.Body;
            LblEquippedBodyValue.DataContext = GameState.CurrentHero.Equipment.Body;
            LblEquippedBodySellable.DataContext = GameState.CurrentHero.Equipment.Body;
            LblEquippedBodyDescription.DataContext = GameState.CurrentHero.Equipment.Body;
            LblSelectedBody.DataContext = _selectedBody;
            LblSelectedBodyDefense.DataContext = _selectedBody;
            LblSelectedBodyValue.DataContext = _selectedBody;
            LblSelectedBodySellable.DataContext = _selectedBody;
            LblSelectedBodyDescription.DataContext = _selectedBody;
        }

        private void BindHands(bool reload = true)
        {
            if (reload)
            {
                _inventoryHands.Clear();
                _inventoryHands.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.HandArmor));
                _inventoryHands = _inventoryHands.OrderBy(armor => armor.Value).ToList();
                LstHandsInventory.UnselectAll();
                LstHandsInventory.ItemsSource = _inventoryHands;
                LstHandsInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                LstHandsInventory.Items.Refresh();
                BtnUnequipHands.IsEnabled = GameState.CurrentHero.Equipment.Hands != GameState.DefaultHands;
            }
            LblEquippedHands.DataContext = GameState.CurrentHero.Equipment.Hands;
            LblEquippedHandsDefense.DataContext = GameState.CurrentHero.Equipment.Hands;
            LblEquippedHandsValue.DataContext = GameState.CurrentHero.Equipment.Hands;
            LblEquippedHandsSellable.DataContext = GameState.CurrentHero.Equipment.Hands;
            LblEquippedHandsDescription.DataContext = GameState.CurrentHero.Equipment.Hands;
            LblSelectedHands.DataContext = _selectedHands;
            LblSelectedHandsDefense.DataContext = _selectedHands;
            LblSelectedHandsValue.DataContext = _selectedHands;
            LblSelectedHandsSellable.DataContext = _selectedHands;
            LblSelectedHandsDescription.DataContext = _selectedHands;
        }

        private void BindLegs(bool reload = true)
        {
            if (reload)
            {
                _inventoryLegs.Clear();
                _inventoryLegs.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.LegArmor));
                _inventoryLegs = _inventoryLegs.OrderBy(armor => armor.Value).ToList();
                LstLegsInventory.UnselectAll();
                LstLegsInventory.ItemsSource = _inventoryLegs;
                LstLegsInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                LstLegsInventory.Items.Refresh();
                BtnUnequipLegs.IsEnabled = GameState.CurrentHero.Equipment.Legs != GameState.DefaultLegs;
            }
            LblEquippedLegs.DataContext = GameState.CurrentHero.Equipment.Legs;
            LblEquippedLegsDefense.DataContext = GameState.CurrentHero.Equipment.Legs;
            LblEquippedLegsValue.DataContext = GameState.CurrentHero.Equipment.Legs;
            LblEquippedLegsSellable.DataContext = GameState.CurrentHero.Equipment.Legs;
            LblEquippedLegsDescription.DataContext = GameState.CurrentHero.Equipment.Legs;
            LblSelectedLegs.DataContext = _selectedLegs;
            LblSelectedLegsDefense.DataContext = _selectedLegs;
            LblSelectedLegsValue.DataContext = _selectedLegs;
            LblSelectedLegsSellable.DataContext = _selectedLegs;
            LblSelectedLegsDescription.DataContext = _selectedLegs;
        }

        private void BindFeet(bool reload = true)
        {
            if (reload)
            {
                _inventoryFeet.Clear();
                _inventoryFeet.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.FeetArmor));
                _inventoryFeet = _inventoryFeet.OrderBy(armor => armor.Value).ToList();
                LstFeetInventory.UnselectAll();
                LstFeetInventory.ItemsSource = _inventoryFeet;
                LstFeetInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                LstFeetInventory.Items.Refresh();
                BtnUnequipFeet.IsEnabled = GameState.CurrentHero.Equipment.Feet != GameState.DefaultFeet;
            }
            LblEquippedFeet.DataContext = GameState.CurrentHero.Equipment.Feet;
            LblEquippedFeetDefense.DataContext = GameState.CurrentHero.Equipment.Feet;
            LblEquippedFeetValue.DataContext = GameState.CurrentHero.Equipment.Feet;
            LblEquippedFeetSellable.DataContext = GameState.CurrentHero.Equipment.Feet;
            LblEquippedFeetDescription.DataContext = GameState.CurrentHero.Equipment.Feet;
            LblSelectedFeet.DataContext = _selectedFeet;
            LblSelectedFeetDefense.DataContext = _selectedFeet;
            LblSelectedFeetValue.DataContext = _selectedFeet;
            LblSelectedFeetSellable.DataContext = _selectedFeet;
            LblSelectedFeetDescription.DataContext = _selectedFeet;
        }

        private void BindRing(bool reload = true)
        {
            if (reload)
            {
                _inventoryRing.Clear();
                _inventoryRing.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.Ring));
                _inventoryRing = _inventoryRing.OrderBy(ring => ring.Value).ToList();
                LstRingInventory.UnselectAll();
                LstRingInventory.ItemsSource = _inventoryRing;
                LstRingInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                LstRingInventory.Items.Refresh();
                BtnUnequipLeftRing.IsEnabled = GameState.CurrentHero.Equipment.LeftRing != new Item();
                BtnUnequipRightRing.IsEnabled = GameState.CurrentHero.Equipment.RightRing != new Item();
            }
            LblEquippedLeftRing.DataContext = GameState.CurrentHero.Equipment.LeftRing;
            LblEquippedLeftRingBonus.DataContext = GameState.CurrentHero.Equipment.LeftRing;
            LblEquippedLeftRingValue.DataContext = GameState.CurrentHero.Equipment.LeftRing;
            LblEquippedRightRing.DataContext = GameState.CurrentHero.Equipment.RightRing;
            LblEquippedRightRingBonus.DataContext = GameState.CurrentHero.Equipment.RightRing;
            LblEquippedRightRingValue.DataContext = GameState.CurrentHero.Equipment.RightRing;
            LblSelectedRing.DataContext = _selectedRing;
            LblSelectedRingBonus.DataContext = _selectedRing;
            LblSelectedRingValue.DataContext = _selectedRing;
            LblSelectedRingSellable.DataContext = _selectedRing;
            LblSelectedRingDescription.DataContext = _selectedRing;
        }

        private void BindPotion(bool reload = true)
        {
            if (reload)
            {
                _inventoryPotion.Clear();
                _inventoryPotion.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.Potion));
                _inventoryPotion = _inventoryPotion.OrderBy(potion => potion.Value).ToList();
                LstPotionInventory.UnselectAll();
                LstPotionInventory.ItemsSource = _inventoryPotion;
                LstPotionInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                LstPotionInventory.Items.Refresh();
            }
            LblSelectedPotion.DataContext = _selectedPotion;
            LblSelectedPotionEffects.DataContext = _selectedPotion;
            LblSelectedPotionValue.DataContext = _selectedPotion;
            LblSelectedPotionSellable.DataContext = _selectedPotion;
            LblSelectedPotionDescription.DataContext = _selectedPotion;
        }

        private void BindFood(bool reload = true)
        {
            if (reload)
            {
                _inventoryFood.Clear();
                _inventoryFood.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.Food));
                _inventoryFood = _inventoryFood.OrderBy(food => food.Value).ToList();
                LstFoodInventory.UnselectAll();
                LstFoodInventory.ItemsSource = _inventoryFood;
                LstFoodInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                LstFoodInventory.Items.Refresh();
            }
            LblSelectedFood.DataContext = _selectedFood;
            LblSelectedFoodEffects.DataContext = _selectedFood;
            LblSelectedFoodValue.DataContext = _selectedFood;
            LblSelectedFoodSellable.DataContext = _selectedFood;
            LblSelectedFoodDescription.DataContext = _selectedFood;
        }

        private void BindDrink(bool reload = true)
        {
            if (reload)
            {
                _inventoryDrinks.Clear();
                _inventoryDrinks.AddRange(GameState.CurrentHero.GetItemsOfType(ItemType.Drink));
                _inventoryDrinks = _inventoryDrinks.OrderBy(drink => drink.Value).ToList();
                LstDrinkInventory.UnselectAll();
                LstDrinkInventory.ItemsSource = _inventoryDrinks;
                LstDrinkInventory.Items.SortDescriptions.Add(new SortDescription("Value", ListSortDirection.Ascending));
                LstDrinkInventory.Items.Refresh();
            }
            LblSelectedDrink.DataContext = _selectedDrink;
            LblSelectedDrinkEffects.DataContext = _selectedDrink;
            LblSelectedDrinkValue.DataContext = _selectedDrink;
            LblSelectedDrinkSellable.DataContext = _selectedDrink;
            LblSelectedDrinkDescription.DataContext = _selectedDrink;
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
            BindDrink();

            DataContext = GameState.CurrentHero;
            LblHealth.DataContext = GameState.CurrentHero.Statistics;
            LblMagic.DataContext = GameState.CurrentHero.Statistics;
        }

        protected void NotifyPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Weapon-Click

        private void BtnUnequipWeapon_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You unequip your {GameState.CurrentHero.Equipment.Weapon.Name}.");
            GameState.CurrentHero.Unequip(GameState.CurrentHero.Equipment.Weapon);
            BindWeapon();
        }

        private void BtnEquipSelectedWeapon_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You equip your {_selectedWeapon.Name}.");
            GameState.CurrentHero.Equip(_selectedWeapon);
            BindWeapon();
        }

        private void BtnDropSelectedWeapon_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedWeapon))
                BindWeapon();
        }

        private void LstWeaponInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedWeapon = LstWeaponInventory.SelectedIndex >= 0 ? new Item((Item)LstWeaponInventory.SelectedValue) : new Item();
            BtnEquipSelectedWeapon.IsEnabled = LstWeaponInventory.SelectedIndex >= 0;
            BtnDropSelectedWeapon.IsEnabled = LstWeaponInventory.SelectedIndex >= 0;

            BindWeapon(false);
        }

        #endregion Weapon-Click

        #region Head-Click

        private void BtnUnequipHead_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You unequip your {GameState.CurrentHero.Equipment.Head.Name}.");
            GameState.CurrentHero.Unequip(GameState.CurrentHero.Equipment.Head);
            BindHead();
        }

        private void BtnEquipSelectedHead_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You equip your {_selectedHead.Name}.");
            GameState.CurrentHero.Equip(_selectedHead);
            BindHead();
        }

        private void BtnDropSelectedHead_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedHead))
                BindHead();
        }

        private void LstHeadInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedHead = LstHeadInventory.SelectedIndex >= 0 ? new Item((Item)LstHeadInventory.SelectedValue) : new Item();
            BtnEquipSelectedHead.IsEnabled = LstHeadInventory.SelectedIndex >= 0;
            BtnDropSelectedHead.IsEnabled = LstHeadInventory.SelectedIndex >= 0;

            BindHead(false);
        }

        #endregion Head-Click

        #region Body-Click

        private void BtnUnequipBody_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You unequip your {GameState.CurrentHero.Equipment.Body.Name}.");
            GameState.CurrentHero.Unequip(GameState.CurrentHero.Equipment.Body);
            BindBody();
        }

        private void BtnEquipSelectedBody_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You equip your {_selectedBody.Name}.");
            GameState.CurrentHero.RemoveItem(_selectedBody);
            GameState.CurrentHero.Equip(_selectedBody);
            BindBody();
        }

        private void BtnDropSelectedBody_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedBody))
                BindBody();
        }

        private void LstBodyInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedBody = LstBodyInventory.SelectedIndex >= 0 ? new Item((Item)LstBodyInventory.SelectedValue) : new Item();
            BtnEquipSelectedBody.IsEnabled = LstBodyInventory.SelectedIndex >= 0;
            BtnDropSelectedBody.IsEnabled = LstBodyInventory.SelectedIndex >= 0;

            BindBody(false);
        }

        #endregion Body-Click

        #region Hands-Click

        private void BtnUnequipHands_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You unequip your {GameState.CurrentHero.Equipment.Hands.Name}.");
            GameState.CurrentHero.Unequip(GameState.CurrentHero.Equipment.Hands);
            BindHands();
        }

        private void BtnEquipSelectedHands_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You equip your {_selectedHands.Name}.");
            GameState.CurrentHero.Equip(_selectedHands);
            BindHands();
        }

        private void BtnDropSelectedHands_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedHands))
                BindHands();
        }

        private void LstHandsInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedHands = LstHandsInventory.SelectedIndex >= 0 ? new Item((Item)LstHandsInventory.SelectedValue) : new Item();
            BtnEquipSelectedHands.IsEnabled = LstHandsInventory.SelectedIndex >= 0;
            BtnDropSelectedHands.IsEnabled = LstHandsInventory.SelectedIndex >= 0;

            BindHands(false);
        }

        #endregion Hands-Click

        #region Legs-Click

        private void BtnUnequipLegs_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You unequip your {GameState.CurrentHero.Equipment.Legs.Name}.");
            GameState.CurrentHero.Unequip(GameState.CurrentHero.Equipment.Legs);
            BindLegs();
        }

        private void BtnEquipSelectedLegs_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You equip your {_selectedLegs.Name}.");
            GameState.CurrentHero.Equip(_selectedLegs);
            BindLegs();
        }

        private void BtnDropSelectedLegs_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedLegs))
                BindLegs();
        }

        private void LstLegsInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedLegs = LstLegsInventory.SelectedIndex >= 0 ? new Item((Item)LstLegsInventory.SelectedValue) : new Item();
            BtnEquipSelectedLegs.IsEnabled = LstLegsInventory.SelectedIndex >= 0;
            BtnDropSelectedLegs.IsEnabled = LstLegsInventory.SelectedIndex >= 0;

            BindLegs(false);
        }

        #endregion Legs-Click

        #region Feet-Click

        private void BtnUnequipFeet_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You unequip your {GameState.CurrentHero.Equipment.Feet.Name}.");
            GameState.CurrentHero.Unequip(GameState.CurrentHero.Equipment.Feet);
            BindFeet();
        }

        private void BtnEquipSelectedFeet_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You equip your {_selectedFeet.Name}.");
            GameState.CurrentHero.Equip(_selectedFeet);
            BindFeet();
        }

        private void BtnDropSelectedFeet_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedFeet))
                BindFeet();
        }

        private void LstFeetInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedFeet = LstFeetInventory.SelectedIndex >= 0 ? new Item((Item)LstFeetInventory.SelectedValue) : new Item();
            BtnEquipSelectedFeet.IsEnabled = LstFeetInventory.SelectedIndex >= 0;
            BtnDropSelectedFeet.IsEnabled = LstFeetInventory.SelectedIndex >= 0;

            BindFeet(false);
        }

        #endregion Feet-Click

        #region Rings-Click

        private void BtnUnequipLeftRing_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You unequip your {GameState.CurrentHero.Equipment.LeftRing.Name}.");
            GameState.CurrentHero.Unequip(GameState.CurrentHero.Equipment.LeftRing, RingHand.Left);
            GameState.CurrentHero.UpdateStatistics();
            BindRing();
        }

        private void BtnUnequipRightRing_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You unequip your {GameState.CurrentHero.Equipment.RightRing.Name}.");
            GameState.CurrentHero.Unequip(GameState.CurrentHero.Equipment.RightRing, RingHand.Right);
            GameState.CurrentHero.UpdateStatistics();
            BindRing();
        }

        private void BtnEquipSelectedRingLeft_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You equip your {_selectedRing.Name}.");
            GameState.CurrentHero.Equip(_selectedRing, RingHand.Left);
            BtnEquipSelectedRingLeft.IsEnabled = false;
            GameState.CurrentHero.UpdateStatistics();
            BindRing();
        }

        private void BtnEquipSelectedRingRight_Click(object sender, RoutedEventArgs e)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You equip your {_selectedRing.Name}.");
            GameState.CurrentHero.Equip(_selectedRing, RingHand.Right);
            BtnEquipSelectedRingRight.IsEnabled = false;
            GameState.CurrentHero.UpdateStatistics();
            BindRing();
        }

        private void BtnDropSelectedRing_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedRing))
                BindRing();
        }

        private void LstRingInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedRing = LstRingInventory.SelectedIndex >= 0 ? new Item((Item)LstRingInventory.SelectedValue) : new Item();
            BtnEquipSelectedRingLeft.IsEnabled = LstRingInventory.SelectedIndex >= 0;
            BtnEquipSelectedRingRight.IsEnabled = LstRingInventory.SelectedIndex >= 0;
            BtnDropSelectedRing.IsEnabled = LstRingInventory.SelectedIndex >= 0;

            BindRing(false);
        }

        #endregion Rings-Click

        //TODO Make it to where attacking with a weapon chooses a random body part, then deducts damage from that spot's defense.

        #region Potion-Click

        private void Consume(Item selectedConsumable)
        {
            Functions.AddTextToTextBox(TxtInventory, $"You consume the {selectedConsumable.Name}.");

            if (selectedConsumable.RestoreHealth > 0)
                Functions.AddTextToTextBox(TxtInventory, GameState.CurrentHero.Heal(selectedConsumable.RestoreHealth));

            if (selectedConsumable.RestoreMagic > 0)
                Functions.AddTextToTextBox(TxtInventory, GameState.CurrentHero.Statistics.RestoreMagic(selectedConsumable.RestoreMagic));

            if (selectedConsumable.Cures)
                Functions.AddTextToTextBox(TxtInventory, "You are now free of any ailments.");
            //TODO Implement Curing, Status Effects

            GameState.CurrentHero.RemoveItem(selectedConsumable);
        }

        private void BtnConsumeSelectedPotion_Click(object sender, RoutedEventArgs e)
        {
            Consume(_selectedPotion);
            BindPotion();
        }

        private void BtnDropSelectedPotion_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedPotion))
                BindPotion();
        }

        private void LstPotionInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedPotion = LstPotionInventory.SelectedIndex >= 0 ? new Item((Item)LstPotionInventory.SelectedValue) : new Item();
            BtnConsumeSelectedPotion.IsEnabled = LstPotionInventory.SelectedIndex >= 0;
            BtnDropSelectedPotion.IsEnabled = LstPotionInventory.SelectedIndex >= 0;

            BindPotion(false);
        }

        #endregion Potion-Click

        #region Food-Click

        private void BtnConsumeSelectedFood_Click(object sender, RoutedEventArgs e)
        {
            Consume(_selectedFood);
            BindFood();
        }

        private void BtnDropSelectedFood_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedFood))
                BindFood();
        }

        private void LstFoodInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedFood = LstFoodInventory.SelectedIndex >= 0 ? new Item((Item)LstFoodInventory.SelectedValue) : new Item();
            BtnConsumeSelectedFood.IsEnabled = LstFoodInventory.SelectedIndex >= 0;
            BtnDropSelectedFood.IsEnabled = LstFoodInventory.SelectedIndex >= 0;

            BindFood(false);
        }

        #endregion Food-Click

        #region Drink-Click

        private void BtnConsumeSelectedDrink_Click(object sender, RoutedEventArgs e)
        {
            Consume(_selectedDrink);
            BindDrink();
        }

        private void BtnDropSelectedDrink_Click(object sender, RoutedEventArgs e)
        {
            if (DropItem(_selectedDrink))
                BindDrink();
        }

        private void LstDrinkInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDrink = LstDrinkInventory.SelectedIndex >= 0 ? new Item((Item)LstDrinkInventory.SelectedValue) : new Item();
            BtnConsumeSelectedDrink.IsEnabled = LstDrinkInventory.SelectedIndex >= 0;
            BtnDropSelectedDrink.IsEnabled = LstDrinkInventory.SelectedIndex >= 0;

            BindDrink(false);
        }

        #endregion Drink-Click

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private void ClosePage()
        {
            GameState.SaveHero(GameState.CurrentHero);
            GameState.GoBack();
        }

        public InventoryPage()
        {
            InitializeComponent();
            BindLabels();
        }

        #endregion Page-Manipulation Methods
    }
}