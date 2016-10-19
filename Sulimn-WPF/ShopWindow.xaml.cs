using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn_WPF
{
    /// <summary>
    /// Interaction logic for ShopWindow.xaml
    /// </summary>
    public partial class ShopWindow : Window
    {
        internal MarketWindow RefToMarketWindow { get; set; }
        internal TavernWindow RefToTavernWindow { get; set; }

        private string shopType;
        private string nl = Environment.NewLine;
        private List<Weapon> purchaseWeapons = new List<Weapon>();
        private List<Weapon> sellWeapons = new List<Weapon>();
        private List<Armor> purchaseArmor = new List<Armor>();
        private List<Armor> sellArmor = new List<Armor>();
        private List<Potion> purchasePotions = new List<Potion>();
        private List<Potion> sellPotions = new List<Potion>();
        private List<Spell> purchaseSpells = new List<Spell>();
        private List<Food> purchaseFood = new List<Food>();
        private List<Food> sellFood = new List<Food>();

        /// <summary>
        /// Sets the Shop type.
        /// </summary>
        /// <param name="typeOfShop">Shop type</param>
        internal void SetShopType(string typeOfShop)
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
                case "Weapon":
                    this.Title = "Weapons 'R' Us";
                    txtShop.Text = "You enter Weapons 'R' Us, the finest weaponsmith shop in the city of Sulimn. You approach the shopkeeper and he shows you his wares.";
                    break;

                case "Armor":
                    this.Title = "The Armoury";
                    txtShop.Text = "You enter The Armoury, an old, solid brick building filled with armor pieces of various shapes, sizes, and materials. The shopkeeper beckons you over to examine his wares.";
                    break;

                case "General":
                    this.Title = "The General Store";
                    txtShop.Text = "You enter The General Store, a solid wooden building near the center of the market. A beautiful young woman is standing behind a counter, smiling at you. You approach her and examine her wares.";
                    break;

                case "Food":
                    this.Title = "The Tavern";
                    txtShop.Text = "You approach the bar at The Tavern. The barkeeper asks you if you'd like a drink or a bite to eat.";
                    break;

                case "Magic":
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
                case "Weapon":
                    purchaseWeapons.Clear();
                    purchaseWeapons.AddRange(GameState.GetItemsOfType<Weapon>().Where(x => x.IsSold == true && x.Type == "Weapon"));
                    purchaseWeapons = purchaseWeapons.OrderBy(x => x.Value).ToList();
                    foreach (Weapon wpn in purchaseWeapons)
                        lstPurchase.Items.Add(wpn.Name);
                    break;

                case "Armor":
                    purchaseArmor.Clear();
                    purchaseArmor.AddRange(GameState.GetItemsOfType<Armor>().Where(x => x.IsSold == true));
                    purchaseArmor = purchaseArmor.OrderBy(x => x.Value).ToList();
                    foreach (Armor armr in purchaseArmor)
                        lstPurchase.Items.Add(armr.Name);
                    break;

                case "General":
                    purchasePotions.Clear();
                    purchasePotions.AddRange(GameState.GetItemsOfType<Potion>().Where(x => x.IsSold == true));
                    purchasePotions = purchasePotions.OrderBy(x => x.Value).ToList();
                    foreach (Potion potn in purchasePotions)
                        lstPurchase.Items.Add(potn.Name);
                    break;

                case "Food":
                    purchaseFood.Clear();
                    purchaseFood.AddRange(GameState.GetItemsOfType<Food>().Where(x => x.IsSold == true));
                    purchaseFood = purchaseFood.OrderBy(x => x.Value).ToList();
                    foreach (Food food in purchaseFood)
                        lstPurchase.Items.Add(food.Name);
                    break;

                case "Magic":
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
                                if (GameState.CurrentHero.ClassName == spl.RequiredClass)
                                    learnSpells.Add(spl);
                            }
                        }
                    }
                    purchaseSpells = learnSpells.OrderBy(x => x.Name).ToList();

                    foreach (Spell spl in purchaseSpells)
                        lstPurchase.Items.Add(spl.Name);
                    break;
            }

            lblGoldPurchase.Text = "Gold: " + GameState.CurrentHero.Gold.ToString("N0");
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
                case "Weapon":
                    sellWeapons = GameState.CurrentHero.Inventory.GetItemsOfType<Weapon>().OrderBy(x => x.Value).ToList();
                    foreach (Weapon wpn in sellWeapons)
                        lstSell.Items.Add(wpn.Name);
                    break;

                case "Armor":
                    sellArmor = GameState.CurrentHero.Inventory.GetItemsOfType<Armor>().OrderBy(x => x.Value).ToList();
                    foreach (Armor armr in sellArmor)
                        lstSell.Items.Add(armr.Name);
                    break;

                case "General":
                    sellPotions = GameState.CurrentHero.Inventory.GetItemsOfType<Potion>().OrderBy(x => x.Value).ToList();
                    foreach (Potion potn in sellPotions)
                        lstSell.Items.Add(potn.Name);
                    break;

                case "Food":
                    sellFood = GameState.CurrentHero.Inventory.GetItemsOfType<Food>().OrderBy(x => x.Value).ToList();
                    foreach (Food food in sellFood)
                        lstSell.Items.Add(food.Name);
                    break;

                case "Magic":
                    break;
            }

            lblGoldSell.Text = "Gold: " + GameState.CurrentHero.Gold.ToString("N0");
        }

        /// <summary>
        /// Displays information about the selected Item from the lstPurchase TextBox.
        /// </summary>
        private void DisplaySelectedPurchase()
        {
            switch (shopType)
            {
                case "Weapon":
                    Weapon wpn = purchaseWeapons[lstPurchase.SelectedIndex];
                    lblAmountPurchase.Text = "Damage: " + wpn.Damage.ToString("N0");
                    lblDescriptionPurchase.Text = wpn.Description;
                    lblSelectedNamePurchase.Text = wpn.Name;
                    lblValuePurchase.Text = "Value: " + wpn.Value.ToString("N0");
                    lblSelectedTypePurchase.Text = "Weapon: " + wpn.WeaponType;

                    if (wpn.Value <= GameState.CurrentHero.Gold)
                        btnPurchase.IsEnabled = true;
                    else
                        btnPurchase.IsEnabled = false;
                    break;

                case "Armor":
                    Armor armr = purchaseArmor[lstPurchase.SelectedIndex];
                    lblAmountPurchase.Text = "Defense: " + armr.Defense.ToString("N0");
                    lblDescriptionPurchase.Text = armr.Description;
                    lblSelectedNamePurchase.Text = armr.Name;
                    lblValuePurchase.Text = "Value: " + armr.Value.ToString("N0");
                    lblSelectedTypePurchase.Text = "Armor: " + armr.ArmorType;

                    if (armr.Value <= GameState.CurrentHero.Gold)
                        btnPurchase.IsEnabled = true;
                    else
                        btnPurchase.IsEnabled = false;
                    break;

                case "General":
                    Potion potn = purchasePotions[lstPurchase.SelectedIndex];
                    if (potn.PotionType == "Curing")
                        lblAmountPurchase.Text = "Curing";
                    else if (potn.PotionType == "Healing")
                        lblAmountPurchase.Text = "Healing: " + potn.Amount.ToString("N0");
                    else if (potn.PotionType == "Magic")
                        lblAmountPurchase.Text = "Magic: " + potn.Amount.ToString("N0");
                    //else FUTURE POTION TYPES

                    lblDescriptionPurchase.Text = potn.Description;
                    lblSelectedNamePurchase.Text = potn.Name;
                    lblValuePurchase.Text = "Value: " + potn.Value.ToString("N0");
                    lblSelectedTypePurchase.Text = "Potion: " + potn.PotionType;

                    if (potn.Value <= GameState.CurrentHero.Gold)
                        btnPurchase.IsEnabled = true;
                    else
                        btnPurchase.IsEnabled = false;
                    break;

                case "Food":
                    Food food = purchaseFood[lstPurchase.SelectedIndex];
                    if (food.FoodType == "Food")
                        lblAmountPurchase.Text = "Healing: " + food.Amount.ToString("N0");
                    else if (food.FoodType == "Drink")
                        lblAmountPurchase.Text = "Restore Magic: " + food.Amount.ToString("N0");
                    //else FUTURE FOOD TYPES

                    lblDescriptionPurchase.Text = food.Description;
                    lblSelectedNamePurchase.Text = food.Name;
                    lblValuePurchase.Text = "Value: " + food.Value.ToString("N0");
                    lblSelectedTypePurchase.Text = "Food: " + food.FoodType;

                    if (food.Value <= GameState.CurrentHero.Gold)
                        btnPurchase.IsEnabled = true;
                    else
                        btnPurchase.IsEnabled = false;
                    break;

                case "Magic":
                    Spell spl = purchaseSpells[lstPurchase.SelectedIndex];
                    int value = spl.RequiredLevel * 200;
                    lblAmountPurchase.Text = spl.Type + ": " + spl.Amount.ToString("N0");
                    //FUTURE SET SWITCH
                    lblDescriptionPurchase.Text = spl.Description;
                    lblSelectedNamePurchase.Text = spl.Name;
                    lblValuePurchase.Text = "Value: " + value.ToString("N0");
                    lblSelectedTypePurchase.Text = "Spell: " + spl.Type;

                    if (value <= GameState.CurrentHero.Gold)
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
                case "Weapon":
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

                case "Armor":
                    Armor armr = sellArmor[lstSell.SelectedIndex];
                    Decimal armorHalfValue = 0;
                    try
                    {
                        armorHalfValue = Decimal.Round(armr.Value / 2, 0);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Sulimn", MessageBoxButton.OK);
                    }
                    lblAmountSell.Text = "Defense: " + armr.Defense.ToString("N0");
                    lblDescriptionSell.Text = armr.Description;
                    lblSelectedNameSell.Text = armr.Name;
                    lblValueSell.Text = "Value: " + armorHalfValue.ToString("N0");
                    lblSelectedTypeSell.Text = "Armor: " + armr.ArmorType;

                    if (armr.CanSell)
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

                case "General":
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

                case "Food":
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
                    if (food.Type == "Food")
                        lblAmountSell.Text = "Healing: " + food.Amount.ToString("N0");
                    else if (food.Type == "Drink")
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

                case "Magic":
                    MessageBox.Show("How in the world did you get this error message? You can't sell Spells!", "Sulimn", MessageBoxButton.OK);
                    break;
            }
        }

        #endregion Load and Display Information

        #region Transaction Methods

        private string Purchase(Item itmPurchase)
        {
            GameState.CurrentHero.Gold -= itmPurchase.Value;
            GameState.CurrentHero.Inventory.AddItem(itmPurchase);
            return "You have purchased " + itmPurchase.Name + " for " + itmPurchase.Value + " gold.";
        }

        private string Purchase(Spell splPurchase)
        {
            GameState.CurrentHero.Gold -= splPurchase.RequiredLevel * 200;
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

            GameState.CurrentHero.Gold += Int32Helper.Parse(itemHalfValue);
            GameState.CurrentHero.Inventory.RemoveItem(itmSell);
            return "You have sold your " + itmSell.Name + " for " + itemHalfValue.ToString("N0") + " gold.";
        }

        #endregion Transaction Methods

        #region Button-Click Methods

        private void btnPurchase_Click(object sender, RoutedEventArgs e)
        {
            switch (shopType)
            {
                case "Weapon":
                    AddTextTT(Purchase(purchaseWeapons[lstPurchase.SelectedIndex]));
                    break;

                case "Armor":
                    AddTextTT(Purchase(purchaseArmor[lstPurchase.SelectedIndex]));
                    break;

                case "General":
                    AddTextTT(Purchase(purchasePotions[lstPurchase.SelectedIndex]));
                    break;

                case "Food":
                    AddTextTT(Purchase(purchaseFood[lstPurchase.SelectedIndex]));
                    break;

                case "Magic":
                    AddTextTT(Purchase(purchaseSpells[lstPurchase.SelectedIndex]));
                    break;
            }
            LoadAllSell();
            LoadAllPurchase();
            lblGoldPurchase.Text = "Gold: " + GameState.CurrentHero.Gold.ToString("N0");
            lblGoldSell.Text = "Gold: " + GameState.CurrentHero.Gold.ToString("N0");
            ClearPurchaseLabels();
        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            switch (shopType)
            {
                case "Weapon":
                    AddTextTT(Sell(sellWeapons[lstSell.SelectedIndex]));
                    break;

                case "Armor":
                    AddTextTT(Sell(sellArmor[lstSell.SelectedIndex]));
                    break;

                case "General":
                    AddTextTT(Sell(sellPotions[lstSell.SelectedIndex]));
                    break;

                case "Food":
                    AddTextTT(Sell(sellFood[lstSell.SelectedIndex]));
                    break;

                case "Magic":
                    MessageBox.Show("Seriously, how did you manage to sell a Spell without even selecting it?", "Sulimn", MessageBoxButton.OK);
                    break;
            }
            LoadAllSell();
            ClearSellLabels();

            lblGoldPurchase.Text = "Gold: " + GameState.CurrentHero.Gold.ToString("N0");
            lblGoldSell.Text = "Gold: " + GameState.CurrentHero.Gold.ToString("N0");
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

        private void windowShop_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (shopType != "Food")
                RefToMarketWindow.Show();
            else
                RefToTavernWindow.Show();

            GameState.SaveHero();
        }

        #endregion Window-Manipulation Methods
    }
}