using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for ShopWindow.xaml
    /// </summary>
    public partial class ShopWindow : Window
    {
        internal MarketWindow RefToMarketWindow { get; set; }
        internal TavernWindow RefToTavernWindow { get; set; }
        internal ArmoryWindow RefToArmoryWindow { get; set; }

        private ItemTypes shopType;
        private string nl = Environment.NewLine;
        private List<Weapon> purchaseWeapons = new List<Weapon>();
        private List<Weapon> sellWeapons = new List<Weapon>();
        private List<HeadArmor> purchaseHead = new List<HeadArmor>();
        private List<HeadArmor> sellHead = new List<HeadArmor>();
        private List<BodyArmor> purchaseBody = new List<BodyArmor>();
        private List<BodyArmor> sellBody = new List<BodyArmor>();
        private List<LegArmor> purchaseLegs = new List<LegArmor>();
        private List<LegArmor> sellLegs = new List<LegArmor>();
        private List<FeetArmor> purchaseFeet = new List<FeetArmor>();
        private List<FeetArmor> sellFeet = new List<FeetArmor>();
        private List<Potion> purchasePotions = new List<Potion>();
        private List<Potion> sellPotions = new List<Potion>();
        private List<Spell> purchaseSpells = new List<Spell>();
        private List<Food> purchaseFood = new List<Food>();
        private List<Food> sellFood = new List<Food>();

        /// <summary>
        /// Sets the Shop type.
        /// </summary>
        /// <param name="typeOfShop">Shop type</param>
        internal void SetShopType(ItemTypes typeOfShop)
        {
            shopType = typeOfShop;
        }

        /// <summary>
        /// Adds text to the txtShop TextBox.
        /// </summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtShop.Text += nl + nl + newText;
            txtShop.Focus();
            txtShop.CaretIndex = txtShop.Text.Length;
            txtShop.ScrollToEnd();
        }

        #region Load and Display Information

        /// <summary>
        /// Clears text from the labels on the Purchase tab.
        /// </summary>
        private void ClearPurchaseLabels()
        {
            lblAmountPurchase.Text = "";
            lblDescriptionPurchase.Text = "";
            lblSelectedNamePurchase.Text = "";
            lblSelectedTypePurchase.Text = "";
            lblSellablePurchase.Text = "";
            lblValuePurchase.Text = "";
            lstPurchase.UnselectAll();
        }

        /// <summary>
        /// Clears text from the labels on the Sell tab.
        /// </summary>
        private void ClearSellLabels()
        {
            lblAmountSell.Text = "";
            lblDescriptionSell.Text = "";
            lblSelectedNameSell.Text = "";
            lblSelectedTypeSell.Text = "";
            lblSellableSell.Text = "";
            lblValueSell.Text = "";
            lstSell.UnselectAll();
        }

        /// <summary>
        /// Loads all information into the appropriate List and displays all items in the appropriate ListBoxes.
        /// </summary>
        internal void LoadAll()
        {
            switch (shopType)
            {
                case ItemTypes.Weapon:
                    this.Title = "Weapons 'R' Us";
                    txtShop.Text = "You enter Weapons 'R' Us, the finest weaponsmith shop in the city of Sulimn. You approach the shopkeeper and he shows you his wares.";
                    break;

                case ItemTypes.Head:
                    this.Title = "The Armoury";
                    txtShop.Text = "You enter The Armoury, an old, solid brick building filled with armor pieces of various shapes, sizes, and materials. The shopkeeper beckons you over to examine his wares.";
                    break;

                case ItemTypes.Potion:
                    this.Title = "The General Store";
                    txtShop.Text = "You enter The General Store, a solid wooden building near the center of the market. A beautiful young woman is standing behind a counter, smiling at you. You approach her and examine her wares.";
                    break;

                case ItemTypes.Food:
                    this.Title = "The Tavern";
                    txtShop.Text = "You approach the bar at The Tavern. The barkeeper asks you if you'd like a drink or a bite to eat.";
                    break;

                case ItemTypes.Spell:
                    this.Title = "Ye Olde Magick Shoppe";
                    txtShop.Text = "You enter Ye Olde Magick Shoppe, a hut of a building. Inside there is a woman facing away from you, stirring a mixture in a cauldron. Sensing your presence, she turns to you, her face hideous and covered in boils." + nl + nl + "\"Would you like to learn some spells, " + GameState.CurrentHero.Name + "?\" she asks. How she knows your name is beyond you.";
                    break;

                default:
                    break;
            }

            LoadAllPurchase();
            LoadAllSell();
        }

        /// <summary>
        /// Loads the appropriate List and displays its contents in the lstPurchase ListBox.
        /// </summary>
        private void LoadAllPurchase()
        {
            lstPurchase.Items.Clear();
            btnPurchase.IsEnabled = false;
            switch (shopType)
            {
                case ItemTypes.Weapon:
                    purchaseWeapons.Clear();
                    purchaseWeapons.AddRange(GameState.GetItemsOfType<Weapon>().Where(x => x.IsSold == true));
                    purchaseWeapons = purchaseWeapons.OrderBy(x => x.Value).ToList();
                    foreach (Weapon wpn in purchaseWeapons)
                        lstPurchase.Items.Add(wpn.Name);
                    break;

                case ItemTypes.Head:
                    purchaseHead.Clear();
                    purchaseHead.AddRange(GameState.GetItemsOfType<HeadArmor>().Where(x => x.IsSold == true));
                    purchaseHead = purchaseHead.OrderBy(x => x.Value).ToList();
                    foreach (Item itm in purchaseHead)
                        lstPurchase.Items.Add(itm.Name);
                    break;

                case ItemTypes.Body:
                    purchaseBody.Clear();
                    purchaseBody.AddRange(GameState.GetItemsOfType<BodyArmor>().Where(x => x.IsSold == true));
                    purchaseBody = purchaseBody.OrderBy(x => x.Value).ToList();
                    foreach (Item itm in purchaseBody)
                        lstPurchase.Items.Add(itm.Name);
                    break;

                case ItemTypes.Legs:
                    purchaseLegs.Clear();
                    purchaseLegs.AddRange(GameState.GetItemsOfType<LegArmor>().Where(x => x.IsSold == true));
                    purchaseLegs = purchaseLegs.OrderBy(x => x.Value).ToList();
                    foreach (Item itm in purchaseLegs)
                        lstPurchase.Items.Add(itm.Name);
                    break;

                case ItemTypes.Feet:
                    purchaseFeet.Clear();
                    purchaseFeet.AddRange(GameState.GetItemsOfType<FeetArmor>().Where(x => x.IsSold == true));
                    purchaseFeet = purchaseFeet.OrderBy(x => x.Value).ToList();
                    foreach (Item itm in purchaseFeet)
                        lstPurchase.Items.Add(itm.Name);
                    break;

                case ItemTypes.Potion:
                    purchasePotions.Clear();
                    purchasePotions.AddRange(GameState.GetItemsOfType<Potion>().Where(x => x.IsSold == true));
                    purchasePotions = purchasePotions.OrderBy(x => x.Value).ToList();
                    foreach (Potion potn in purchasePotions)
                        lstPurchase.Items.Add(potn.Name);
                    break;

                case ItemTypes.Food:
                    purchaseFood.Clear();
                    purchaseFood.AddRange(GameState.GetItemsOfType<Food>().Where(x => x.IsSold == true));
                    purchaseFood = purchaseFood.OrderBy(x => x.Value).ToList();
                    foreach (Food food in purchaseFood)
                        lstPurchase.Items.Add(food.Name);
                    break;

                case ItemTypes.Spell:
                    purchaseSpells.Clear();
                    purchaseSpells.AddRange(GameState.AllSpells);
                    List<Spell> learnSpells = new List<Spell>();
                    foreach (Spell spl in purchaseSpells)
                    {
                        if (GameState.CurrentHero.Level >= spl.RequiredLevel && !GameState.CurrentHero.Spellbook.Spells.Contains(spl))
                        {
                            if (spl.RequiredClass.Length == 0)
                                learnSpells.Add(spl);
                            else
                            {
                                if (GameState.CurrentHero.Class.Name == spl.RequiredClass)
                                    learnSpells.Add(spl);
                            }
                        }
                    }
                    purchaseSpells = learnSpells.OrderBy(x => x.Name).ToList();

                    foreach (Spell spl in purchaseSpells)
                        lstPurchase.Items.Add(spl.Name);
                    break;
            }

            lblGoldPurchase.Text = "Gold: " + GameState.CurrentHero.Inventory.Gold.ToString("N0");
        }

        /// <summary>
        /// Loads the Hero's inventory into the list and displays its contents in the lstSell TextBox.
        /// </summary>
        private void LoadAllSell()
        {
            lstSell.Items.Clear();
            btnSell.IsEnabled = false;
            switch (shopType)
            {
                case ItemTypes.Weapon:
                    sellWeapons = GameState.CurrentHero.Inventory.GetItemsOfType<Weapon>().OrderBy(x => x.Value).ToList();
                    foreach (Weapon wpn in sellWeapons)
                        lstSell.Items.Add(wpn.Name);
                    break;

                case ItemTypes.Head:
                    sellHead.Clear();
                    sellHead.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<HeadArmor>().OrderBy(x => x.Value).ToList());
                    foreach (Item itm in sellHead)
                        lstSell.Items.Add(itm.Name);
                    break;

                case ItemTypes.Body:
                    sellBody.Clear();
                    sellBody.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<BodyArmor>().OrderBy(x => x.Value).ToList());
                    foreach (Item itm in sellBody)
                        lstSell.Items.Add(itm.Name);
                    break;

                case ItemTypes.Legs:
                    sellLegs.Clear();
                    sellLegs.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<LegArmor>().OrderBy(x => x.Value).ToList());
                    foreach (Item itm in sellLegs)
                        lstSell.Items.Add(itm.Name);
                    break;

                case ItemTypes.Feet:
                    sellFeet.Clear();
                    sellFeet.AddRange(GameState.CurrentHero.Inventory.GetItemsOfType<FeetArmor>().OrderBy(x => x.Value).ToList());
                    foreach (Item itm in sellFeet)
                        lstSell.Items.Add(itm.Name);
                    break;

                case ItemTypes.Potion:
                    sellPotions = GameState.CurrentHero.Inventory.GetItemsOfType<Potion>().OrderBy(x => x.Value).ToList();
                    foreach (Potion potn in sellPotions)
                        lstSell.Items.Add(potn.Name);
                    break;

                case ItemTypes.Food:
                    sellFood = GameState.CurrentHero.Inventory.GetItemsOfType<Food>().OrderBy(x => x.Value).ToList();
                    foreach (Food food in sellFood)
                        lstSell.Items.Add(food.Name);
                    break;

                case ItemTypes.Spell:
                    break;
            }

            lblGoldSell.Text = "Gold: " + GameState.CurrentHero.Inventory.Gold.ToString("N0");
        }

        /// <summary>
        /// Displays information about the selected Item from the lstPurchase TextBox.
        /// </summary>
        private void DisplaySelectedPurchase()
        {
            switch (shopType)
            {
                case ItemTypes.Weapon:
                    Weapon wpn = purchaseWeapons[lstPurchase.SelectedIndex];
                    lblAmountPurchase.Text = "Damage: " + wpn.Damage.ToString("N0");
                    lblDescriptionPurchase.Text = wpn.Description;
                    lblSelectedNamePurchase.Text = wpn.Name;
                    lblValuePurchase.Text = "Value: " + wpn.Value.ToString("N0");
                    lblSelectedTypePurchase.Text = "Weapon: " + wpn.WeaponType;

                    if (wpn.Value <= GameState.CurrentHero.Inventory.Gold)
                        btnPurchase.IsEnabled = true;
                    else
                        btnPurchase.IsEnabled = false;
                    break;

                case ItemTypes.Head:

                    HeadArmor headArmor = new HeadArmor(purchaseHead[lstPurchase.SelectedIndex]);
                    lblAmountPurchase.Text = "Defense: " + headArmor.Defense.ToString("N0");
                    lblDescriptionPurchase.Text = headArmor.Description;
                    lblSelectedNamePurchase.Text = headArmor.Name;
                    lblValuePurchase.Text = "Value: " + headArmor.Value.ToString("N0");
                    lblSelectedTypePurchase.Text = "Armor: " + headArmor.Type;

                    if (headArmor.Value <= GameState.CurrentHero.Inventory.Gold)
                        btnPurchase.IsEnabled = true;
                    else
                        btnPurchase.IsEnabled = false;
                    break;

                case ItemTypes.Body:

                    BodyArmor bodyArmor = new BodyArmor(purchaseBody[lstPurchase.SelectedIndex]);
                    lblAmountPurchase.Text = "Defense: " + bodyArmor.Defense.ToString("N0");
                    lblDescriptionPurchase.Text = bodyArmor.Description;
                    lblSelectedNamePurchase.Text = bodyArmor.Name;
                    lblValuePurchase.Text = "Value: " + bodyArmor.Value.ToString("N0");
                    lblSelectedTypePurchase.Text = "Armor: " + bodyArmor.Type;

                    if (bodyArmor.Value <= GameState.CurrentHero.Inventory.Gold)
                        btnPurchase.IsEnabled = true;
                    else
                        btnPurchase.IsEnabled = false;
                    break;

                case ItemTypes.Legs:

                    LegArmor legArmor = new LegArmor(purchaseLegs[lstPurchase.SelectedIndex]);
                    lblAmountPurchase.Text = "Defense: " + legArmor.Defense.ToString("N0");
                    lblDescriptionPurchase.Text = legArmor.Description;
                    lblSelectedNamePurchase.Text = legArmor.Name;
                    lblValuePurchase.Text = "Value: " + legArmor.Value.ToString("N0");
                    lblSelectedTypePurchase.Text = "Armor: " + legArmor.Type;

                    if (legArmor.Value <= GameState.CurrentHero.Inventory.Gold)
                        btnPurchase.IsEnabled = true;
                    else
                        btnPurchase.IsEnabled = false;
                    break;

                case ItemTypes.Feet:

                    FeetArmor feetArmor = new FeetArmor(purchaseFeet[lstPurchase.SelectedIndex]);
                    lblAmountPurchase.Text = "Defense: " + feetArmor.Defense.ToString("N0");
                    lblDescriptionPurchase.Text = feetArmor.Description;
                    lblSelectedNamePurchase.Text = feetArmor.Name;
                    lblValuePurchase.Text = "Value: " + feetArmor.Value.ToString("N0");
                    lblSelectedTypePurchase.Text = "Armor: " + feetArmor.Type;

                    if (feetArmor.Value <= GameState.CurrentHero.Inventory.Gold)
                        btnPurchase.IsEnabled = true;
                    else
                        btnPurchase.IsEnabled = false;
                    break;

                case ItemTypes.Potion:
                    Potion potn = purchasePotions[lstPurchase.SelectedIndex];
                    if (potn.PotionType == PotionTypes.Curing)
                        lblAmountPurchase.Text = "Curing";
                    else if (potn.PotionType == PotionTypes.Healing)
                        lblAmountPurchase.Text = "Healing: " + potn.Amount.ToString("N0");
                    else if (potn.PotionType == PotionTypes.Magic)
                        lblAmountPurchase.Text = "Magic: " + potn.Amount.ToString("N0");
                    //else FUTURE POTION TYPES

                    lblDescriptionPurchase.Text = potn.Description;
                    lblSelectedNamePurchase.Text = potn.Name;
                    lblValuePurchase.Text = "Value: " + potn.Value.ToString("N0");
                    lblSelectedTypePurchase.Text = "Potion: " + potn.PotionType;

                    if (potn.Value <= GameState.CurrentHero.Inventory.Gold)
                        btnPurchase.IsEnabled = true;
                    else
                        btnPurchase.IsEnabled = false;
                    break;

                case ItemTypes.Food:
                    Food food = purchaseFood[lstPurchase.SelectedIndex];
                    if (food.FoodType == FoodTypes.Food)
                        lblAmountPurchase.Text = "Healing: " + food.Amount.ToString("N0");
                    else if (food.FoodType == FoodTypes.Drink)
                        lblAmountPurchase.Text = "Restore Magic: " + food.Amount.ToString("N0");
                    //else FUTURE FOOD TYPES

                    lblDescriptionPurchase.Text = food.Description;
                    lblSelectedNamePurchase.Text = food.Name;
                    lblValuePurchase.Text = "Value: " + food.Value.ToString("N0");
                    lblSelectedTypePurchase.Text = "Food: " + food.FoodType;

                    if (food.Value <= GameState.CurrentHero.Inventory.Gold)
                        btnPurchase.IsEnabled = true;
                    else
                        btnPurchase.IsEnabled = false;
                    break;

                case ItemTypes.Spell:
                    Spell spl = purchaseSpells[lstPurchase.SelectedIndex];
                    int value = spl.RequiredLevel * 200;
                    lblAmountPurchase.Text = spl.Type + ": " + spl.Amount.ToString("N0");
                    //FUTURE SET SWITCH
                    lblDescriptionPurchase.Text = spl.Description;
                    lblSelectedNamePurchase.Text = spl.Name;
                    lblValuePurchase.Text = "Value: " + value.ToString("N0");
                    lblSelectedTypePurchase.Text = "Spell: " + spl.Type;
                    lblRequiredLevel.Text = spl.RequiredLevelToString;
                    if (value <= GameState.CurrentHero.Inventory.Gold)
                        btnPurchase.IsEnabled = true;
                    else
                        btnPurchase.IsEnabled = false;
                    break;
            }
        }

        /// <summary>
        /// Displays information about the selected Item from the lstSell TextBox.
        /// </summary>
        private void DisplaySelectedSell()
        {
            switch (shopType)
            {
                case ItemTypes.Weapon:
                    Weapon wpn = sellWeapons[lstSell.SelectedIndex];
                    Decimal weaponHalfValue = 0;
                    try
                    {
                        weaponHalfValue = Decimal.Round(wpn.Value / 2, 0);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Sulimn", MessageBoxButton.OK);
                    }
                    lblAmountSell.Text = "Damage: " + wpn.Damage.ToString("N0");
                    lblDescriptionSell.Text = wpn.Description;
                    lblSelectedNameSell.Text = wpn.Name;
                    lblValueSell.Text = "Value: " + weaponHalfValue.ToString("N0");
                    lblSelectedTypeSell.Text = "Weapon: " + wpn.WeaponType;

                    if (wpn.CanSell)
                    {
                        lblSellableSell.Text = "Sellable";
                        btnSell.IsEnabled = true;
                    }
                    else
                    {
                        lblSellableSell.Text = "Not Sellable";
                        btnSell.IsEnabled = false;
                    }
                    break;

                case ItemTypes.Head:
                    HeadArmor headArmor = new HeadArmor(sellHead[lstSell.SelectedIndex]);
                    Decimal headHalfValue = 0;
                    try
                    {
                        headHalfValue = Decimal.Round(headArmor.Value / 2, 0);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Sulimn", MessageBoxButton.OK);
                    }
                    lblAmountSell.Text = "Defense: " + headArmor.Defense.ToString("N0");
                    lblDescriptionSell.Text = headArmor.Description;
                    lblSelectedNameSell.Text = headArmor.Name;
                    lblValueSell.Text = "Value: " + headHalfValue.ToString("N0");
                    lblSelectedTypeSell.Text = "Armor: " + headArmor.Type;

                    if (headArmor.CanSell)
                    {
                        lblSellableSell.Text = "Sellable";
                        btnSell.IsEnabled = true;
                    }
                    else
                    {
                        lblSellableSell.Text = "Not Sellable";
                        btnSell.IsEnabled = false;
                    }
                    break;

                case ItemTypes.Body:
                    BodyArmor bodyArmor = new BodyArmor(sellBody[lstSell.SelectedIndex]);
                    Decimal bodyHalfValue = 0;
                    try
                    {
                        headHalfValue = Decimal.Round(bodyArmor.Value / 2, 0);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Sulimn", MessageBoxButton.OK);
                    }
                    lblAmountSell.Text = "Defense: " + bodyArmor.Defense.ToString("N0");
                    lblDescriptionSell.Text = bodyArmor.Description;
                    lblSelectedNameSell.Text = bodyArmor.Name;
                    lblValueSell.Text = "Value: " + bodyHalfValue.ToString("N0");
                    lblSelectedTypeSell.Text = "Armor: " + bodyArmor.Type;

                    if (bodyArmor.CanSell)
                    {
                        lblSellableSell.Text = "Sellable";
                        btnSell.IsEnabled = true;
                    }
                    else
                    {
                        lblSellableSell.Text = "Not Sellable";
                        btnSell.IsEnabled = false;
                    }
                    break;

                case ItemTypes.Legs:
                    LegArmor legArmor = new LegArmor(sellLegs[lstSell.SelectedIndex]);
                    Decimal legHalfValue = 0;
                    try
                    {
                        headHalfValue = Decimal.Round(legArmor.Value / 2, 0);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Sulimn", MessageBoxButton.OK);
                    }
                    lblAmountSell.Text = "Defense: " + legArmor.Defense.ToString("N0");
                    lblDescriptionSell.Text = legArmor.Description;
                    lblSelectedNameSell.Text = legArmor.Name;
                    lblValueSell.Text = "Value: " + legHalfValue.ToString("N0");
                    lblSelectedTypeSell.Text = "Armor: " + legArmor.Type;

                    if (legArmor.CanSell)
                    {
                        lblSellableSell.Text = "Sellable";
                        btnSell.IsEnabled = true;
                    }
                    else
                    {
                        lblSellableSell.Text = "Not Sellable";
                        btnSell.IsEnabled = false;
                    }
                    break;

                case ItemTypes.Feet:
                    FeetArmor feetArmor = new FeetArmor(sellFeet[lstSell.SelectedIndex]);
                    Decimal feetHalfValue = 0;
                    try
                    {
                        headHalfValue = Decimal.Round(feetArmor.Value / 2, 0);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Sulimn", MessageBoxButton.OK);
                    }
                    lblAmountSell.Text = "Defense: " + feetArmor.Defense.ToString("N0");
                    lblDescriptionSell.Text = feetArmor.Description;
                    lblSelectedNameSell.Text = feetArmor.Name;
                    lblValueSell.Text = "Value: " + feetHalfValue.ToString("N0");
                    lblSelectedTypeSell.Text = "Armor: " + feetArmor.Type;

                    if (feetArmor.CanSell)
                    {
                        lblSellableSell.Text = "Sellable";
                        btnSell.IsEnabled = true;
                    }
                    else
                    {
                        lblSellableSell.Text = "Not Sellable";
                        btnSell.IsEnabled = false;
                    }
                    break;

                case ItemTypes.Potion:
                    Potion potn = sellPotions[lstSell.SelectedIndex];
                    Decimal potionHalfValue = 0;
                    try
                    {
                        potionHalfValue = Decimal.Round(potn.Value / 2, 0);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Sulimn", MessageBoxButton.OK);
                    }
                    //FUTURE SET SWITCH
                    lblAmountSell.Text = potn.PotionType + ": " + potn.Amount.ToString("N0");
                    lblDescriptionSell.Text = potn.Description;
                    lblSelectedNameSell.Text = potn.Name;
                    lblValueSell.Text = "Value: " + potionHalfValue.ToString("N0");
                    lblSelectedTypeSell.Text = "Potion: " + potn.PotionType;

                    if (potn.CanSell)
                    {
                        lblSellableSell.Text = "Sellable";
                        btnSell.IsEnabled = true;
                    }
                    else
                    {
                        lblSellableSell.Text = "Not Sellable";
                        btnSell.IsEnabled = false;
                    }
                    break;

                case ItemTypes.Food:
                    Food food = sellFood[lstSell.SelectedIndex];
                    Decimal foodHalfValue = 0;
                    try
                    {
                        foodHalfValue = Decimal.Round(food.Value / 2, 0);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Sulimn", MessageBoxButton.OK);
                    }
                    //FUTURE SET SWITCH
                    if (food.FoodType == FoodTypes.Food)
                        lblAmountSell.Text = "Healing: " + food.Amount.ToString("N0");
                    else if (food.FoodType == FoodTypes.Drink)
                        lblAmountSell.Text = "Restore Magic: " + food.Amount.ToString("N0");
                    lblDescriptionSell.Text = food.Description;
                    lblSelectedNameSell.Text = food.Name;
                    lblValueSell.Text = "Value: " + foodHalfValue.ToString("N0");
                    lblSelectedTypeSell.Text = "Food: " + food.FoodType;

                    if (food.CanSell)
                    {
                        lblSellableSell.Text = "Sellable";
                        btnSell.IsEnabled = true;
                    }
                    else
                    {
                        lblSellableSell.Text = "Not Sellable";
                        btnSell.IsEnabled = false;
                    }
                    break;

                case ItemTypes.Spell:
                    MessageBox.Show("How in the world did you get this error message? You can't sell Spells!", "Sulimn", MessageBoxButton.OK);
                    break;
            }
        }

        #endregion Load and Display Information

        #region Transaction Methods

        private string Purchase(Item itmPurchase)
        {
            GameState.CurrentHero.Inventory.Gold -= itmPurchase.Value;
            GameState.CurrentHero.Inventory.AddItem(itmPurchase);
            return "You have purchased " + itmPurchase.Name + " for " + itmPurchase.Value + " gold.";
        }

        private string Purchase(Spell splPurchase)
        {
            GameState.CurrentHero.Inventory.Gold -= splPurchase.RequiredLevel * 200;
            return GameState.CurrentHero.Spellbook.LearnSpell(splPurchase) + " It cost " + splPurchase.RequiredLevel * 200 + " gold.";
        }

        private string Sell(Item itmSell)
        {
            Decimal itemHalfValue = 0;

            try
            {
                itemHalfValue = Decimal.Round(itmSell.Value / 2, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Converting Decimal", MessageBoxButton.OK);
            }

            GameState.CurrentHero.Inventory.Gold += Int32Helper.Parse(itemHalfValue);
            GameState.CurrentHero.Inventory.RemoveItem(itmSell);
            return "You have sold your " + itmSell.Name + " for " + itemHalfValue.ToString("N0") + " gold.";
        }

        #endregion Transaction Methods

        #region Button-Click Methods

        private void btnPurchase_Click(object sender, RoutedEventArgs e)
        {
            switch (shopType)
            {
                case ItemTypes.Weapon:
                    AddTextTT(Purchase(purchaseWeapons[lstPurchase.SelectedIndex]));
                    break;

                case ItemTypes.Head:
                    AddTextTT(Purchase(purchaseHead[lstPurchase.SelectedIndex]));
                    break;

                case ItemTypes.Body:
                    AddTextTT(Purchase(purchaseBody[lstPurchase.SelectedIndex]));
                    break;

                case ItemTypes.Legs:
                    AddTextTT(Purchase(purchaseLegs[lstPurchase.SelectedIndex]));
                    break;

                case ItemTypes.Feet:
                    AddTextTT(Purchase(purchaseFeet[lstPurchase.SelectedIndex]));
                    break;

                case ItemTypes.Potion:
                    AddTextTT(Purchase(purchasePotions[lstPurchase.SelectedIndex]));
                    break;

                case ItemTypes.Food:
                    AddTextTT(Purchase(purchaseFood[lstPurchase.SelectedIndex]));
                    break;

                case ItemTypes.Spell:
                    AddTextTT(Purchase(purchaseSpells[lstPurchase.SelectedIndex]));
                    break;
            }
            LoadAllSell();
            LoadAllPurchase();
            lblGoldPurchase.Text = GameState.CurrentHero.Inventory.GoldToStringWithText; ;
            lblGoldSell.Text = GameState.CurrentHero.Inventory.GoldToStringWithText;
            ClearPurchaseLabels();
        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            switch (shopType)
            {
                case ItemTypes.Weapon:
                    AddTextTT(Sell(sellWeapons[lstSell.SelectedIndex]));
                    break;

                case ItemTypes.Head:
                    AddTextTT(Sell(sellHead[lstSell.SelectedIndex]));
                    break;

                case ItemTypes.Body:
                    AddTextTT(Sell(sellBody[lstSell.SelectedIndex]));
                    break;

                case ItemTypes.Legs:
                    AddTextTT(Sell(sellLegs[lstSell.SelectedIndex]));
                    break;

                case ItemTypes.Feet:
                    AddTextTT(Sell(sellFeet[lstSell.SelectedIndex]));
                    break;

                case ItemTypes.Potion:
                    AddTextTT(Sell(sellPotions[lstSell.SelectedIndex]));
                    break;

                case ItemTypes.Food:
                    AddTextTT(Sell(sellFood[lstSell.SelectedIndex]));
                    break;

                case ItemTypes.Spell:
                    MessageBox.Show("Seriously, how did you manage to sell a Spell without even selecting it?", "Sulimn", MessageBoxButton.OK);
                    break;
            }
            LoadAllSell();
            ClearSellLabels();

            lblGoldPurchase.Text = GameState.CurrentHero.Inventory.GoldToStringWithText;
            lblGoldSell.Text = GameState.CurrentHero.Inventory.GoldToStringWithText;
        }

        private void btnCharacter_Click(object sender, RoutedEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow();
            characterWindow.RefToShopWindow = this;
            characterWindow.Show();
            characterWindow.SetupChar();
            characterWindow.SetPreviousWindow("Shop");
            characterWindow.BindLabels();
            this.Visibility = Visibility.Hidden;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
        }

        private void lstSell_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstSell.SelectedIndex >= 0)
            {
                DisplaySelectedSell();
            }
        }

        private void lstPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstPurchase.SelectedIndex >= 0)
            {
                DisplaySelectedPurchase();
            }
        }

        public ShopWindow()
        {
            InitializeComponent();
        }

        private void windowShop_Closing(object sender, CancelEventArgs e)
        {
            if (shopType == ItemTypes.Food)
                RefToTavernWindow.Show();
            else if (shopType == ItemTypes.Body || shopType == ItemTypes.Head || shopType == ItemTypes.Legs || shopType == ItemTypes.Feet)
                RefToArmoryWindow.Show();
            else
                RefToMarketWindow.Show();

            GameState.SaveHero(GameState.CurrentHero);
        }

        #endregion Window-Manipulation Methods
    }
}