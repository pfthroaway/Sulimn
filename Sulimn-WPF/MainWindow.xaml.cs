using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows;

namespace Sulimn_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Hero Management

        /// <summary>
        /// Sets the Hero's inventory.
        /// </summary>
        /// <param name="inventory">Inventory to be converted</param>
        /// <returns>Inventory List</returns>
        private Inventory SetInventory(string inventory)
        {
            List<Item> itemList = new List<Item>();
            string[] arrInventory = inventory.Split(',');

            foreach (string str in arrInventory)
            {
                string type = GameState.AllItems.Find(x => x.Name == (str.Trim())).Type;
                itemList.Add(GameState.AllItems.Find(x => x.Name == str.Trim()));
            }
            return new Inventory(itemList);
        }

        /// <summary>
        /// Sets the list of the Hero's known spells.
        /// </summary>
        /// <param name="spells">String list of spells</param>
        /// <returns>List of known Spells</returns>
        private Spellbook SetSpellbook(string spells)
        {
            List<Spell> spellList = new List<Spell>();
            string[] arrSpell = spells.Split(',');

            foreach (string str in arrSpell)
                spellList.Add(GameState.AllSpells.Find(x => x.Name == str.Trim()));
            return new Spellbook(spellList);
        }

        /// <summary>
        /// Assign all attributes to the Hero.
        /// </summary>
        /// <param name="ds">DataSet</param>
        private void AssignHero(DataSet ds)
        {
            string spells, weapon, head, body, legs, feet, inventory;
            GameState.CurrentHero = new Hero();

            GameState.CurrentHero.Name = ds.Tables[0].Rows[0]["CharacterName"].ToString();
            GameState.CurrentHero.ClassName = ds.Tables[0].Rows[0]["Class"].ToString();
            GameState.CurrentHero.Level = Convert.ToInt32(ds.Tables[0].Rows[0]["Level"]);
            GameState.CurrentHero.Experience = Convert.ToInt32(ds.Tables[0].Rows[0]["Experience"]);
            GameState.CurrentHero.SkillPoints = Convert.ToInt32(ds.Tables[0].Rows[0]["SkillPoints"]);
            GameState.CurrentHero.Strength = Convert.ToInt32(ds.Tables[0].Rows[0]["Strength"]);
            GameState.CurrentHero.Vitality = Convert.ToInt32(ds.Tables[0].Rows[0]["Vitality"]);
            GameState.CurrentHero.Dexterity = Convert.ToInt32(ds.Tables[0].Rows[0]["Dexterity"]);
            GameState.CurrentHero.Wisdom = Convert.ToInt32(ds.Tables[0].Rows[0]["Wisdom"]);
            GameState.CurrentHero.Gold = Convert.ToInt32(ds.Tables[0].Rows[0]["Gold"]);
            GameState.CurrentHero.CurrentHealth = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrHealth"]);
            GameState.CurrentHero.MaximumHealth = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxHealth"]);
            GameState.CurrentHero.CurrentMagic = Convert.ToInt32(ds.Tables[0].Rows[0]["CurrMagic"]);
            GameState.CurrentHero.MaximumMagic = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxMagic"]);
            spells = ds.Tables[0].Rows[0]["KnownSpells"].ToString();
            weapon = ds.Tables[0].Rows[0]["Weapon"].ToString();
            head = ds.Tables[0].Rows[0]["Head"].ToString();
            body = ds.Tables[0].Rows[0]["Body"].ToString();
            legs = ds.Tables[0].Rows[0]["Legs"].ToString();
            feet = ds.Tables[0].Rows[0]["Feet"].ToString();
            inventory = ds.Tables[0].Rows[0]["Inventory"].ToString();

            if (spells.Length > 0)
            {
                GameState.CurrentHero.Spellbook = SetSpellbook(spells);
            }

            GameState.CurrentHero.Weapon = (Weapon)GameState.AllItems.Find(x => x.Name == weapon);

            if (inventory.Length > 0)
            {
                GameState.CurrentHero.Inventory = SetInventory(inventory);
            }

            GameState.CurrentHero.Head = (Armor)GameState.AllItems.Find(x => x.Name == head);
            GameState.CurrentHero.Body = (Armor)GameState.AllItems.Find(x => x.Name == body);
            GameState.CurrentHero.Legs = (Armor)GameState.AllItems.Find(x => x.Name == legs);
            GameState.CurrentHero.Feet = (Armor)GameState.AllItems.Find(x => x.Name == feet);

            Login(GameState.CurrentHero);
        }

        #endregion Hero Management

        #region Login

        /// <summary>
        /// Checks the validity of the typed login.
        /// </summary>
        internal async void CheckLogin()
        {
            string sql = "SELECT * FROM Players WHERE [CharacterName]='" + txtHeroName.Text + "'";
            string table = "Player";
            DataSet ds = await Functions.FillDataSet(sql, table);

            if (ds.Tables[0].Rows.Count > 0)
            {
                AssignHero(ds);
            }
            else
            {
                MessageBox.Show("Invalid login.", "Sulimn", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Logs the Hero in.
        /// </summary>
        /// <param name="Hero">Current Hero</param>
        internal void Login(Hero Hero)
        {
            txtHeroName.Text = "";
            CityWindow cityWindow = new CityWindow();
            cityWindow.Show();
            cityWindow.RefToMainWindow = this;
            cityWindow.EnterCity();
            this.Visibility = Visibility.Hidden;
        }

        #endregion Login

        #region Button-Click Methods

        // This method opens the newPlayer form.
        private void btnNewHero_Click(object sender, RoutedEventArgs e)
        {
            NewHeroWindow newHeroWindow = new NewHeroWindow();
            newHeroWindow.Show();
            newHeroWindow.RefToMainWindow = this;
            this.Visibility = Visibility.Hidden;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            CheckLogin();
        }

        private void mnuAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminPasswordWindow adminPasswordWindow = new AdminPasswordWindow();
            adminPasswordWindow.Show();
            adminPasswordWindow.RefToMainWindow = this;
            this.Visibility = Visibility.Hidden;
        }

        #endregion Button-Click Methods

        public MainWindow()
        {
            InitializeComponent();
            txtHeroName.Focus();
        }

        private async void windowMain_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Factory.StartNew(() => GameState.LoadAll());
        }
    }
}