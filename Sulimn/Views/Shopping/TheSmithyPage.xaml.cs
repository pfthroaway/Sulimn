using Sulimn.Classes;
using Sulimn.Classes.Items;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn.Views.Shopping
{
    /// <summary>Interaction logic for TheSmithyPage.xaml</summary>
    public partial class TheSmithyPage : Page, INotifyPropertyChanged
    {
        private Item _selectedItem = new Item();
        private List<Item> _repairItems = new List<Item>();

        /// <summary>Currently selected <see cref="Item"/> in the LstRepair ListBox.</summary>
        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged(nameof(SelectedItem));
            }
        }

        /// <summary>List of available <see cref="Item"/>s to be repaired.</summary>
        public List<Item> RepairItems
        {
            get => _repairItems;
            set
            {
                _repairItems = value;
                NotifyPropertyChanged(nameof(RepairItems));
            }
        }

        #region INotifyPropertyChanged Members

        /// <summary>The event that is raised when a property that calls the NotifyPropertyChanged method is changed.</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Raises the PropertyChanged event alerting the WPF Framework to update the UI.</summary>
        /// <param name="propertyNames">The names of the properties to update in the UI.</param>
        protected void NotifyPropertyChanged(params string[] propertyNames)
        {
            if (PropertyChanged != null)
            {
                foreach (string propertyName in propertyNames)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        /// <summary>Raises the PropertyChanged event alerting the WPF Framework to update the UI.</summary>
        /// <param name="propertyName">The optional name of the property to update in the UI. If this is left blank, the name will be taken from the calling member via the CallerMemberName attribute.</param>
        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged Members

        private void UpdateSource()
        {
            RepairItems.Clear();
            LstRepair.UnselectAll();
            RepairItems.AddRange(GameState.CurrentHero.Inventory.Where(itm => itm.CurrentDurability != itm.MaximumDurability));
            RepairItems.AddRange(GameState.CurrentHero.Equipment.AllEquipment.Where(itm => itm.CurrentDurability != itm.MaximumDurability));
            RepairItems = RepairItems.OrderBy(itm => itm.Name).ToList();
            LstRepair.ItemsSource = RepairItems;
            LstRepair.Items.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            RepairItems.AddRange(GameState.CurrentHero.Inventory.Where(itm => itm.DurabilityRatio != 1m));
            BtnRepairAll.IsEnabled = GameState.CurrentHero.Gold >= GameState.CurrentHero.TotalRepairCost;
            LblRepairAll.Text = GameState.CurrentHero.TotalRepairCostToStringWithText;
            DataContext = GameState.CurrentHero;
            BtnRepairAll.IsEnabled = RepairItems.Count > 0;
        }

        #region Click

        private void BtnBack_Click(object sender, RoutedEventArgs e) => GameState.GoBack();

        private void BtnRepairAll_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.Gold -= GameState.CurrentHero.TotalRepairCost;
            GameState.CurrentHero.Inventory.ForEach(itm => itm.CurrentDurability = itm.MaximumDurability);
            GameState.CurrentHero.Equipment.AllEquipment.ForEach(itm => itm.CurrentDurability = itm.MaximumDurability);
            BtnRepairAll.IsEnabled = false;
            UpdateSource();
        }

        private void BtnRepair_Click(object sender, RoutedEventArgs e)
        {
            GameState.CurrentHero.Gold -= SelectedItem.RepairCost;
            foreach (Item itm in GameState.CurrentHero.Inventory.Where(i => i == SelectedItem))
                itm.CurrentDurability = itm.MaximumDurability;
            foreach (Item itm in GameState.CurrentHero.Equipment.AllEquipment.Where(i => i == SelectedItem))
                itm.CurrentDurability = itm.MaximumDurability;
            SelectedItem = new Item();
            BtnRepair.IsEnabled = false;
            UpdateSource();
        }

        private void LstRepair_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstRepair.SelectedIndex >= 0)
                SelectedItem = RepairItems[LstRepair.SelectedIndex];
            BtnRepair.IsEnabled = LstRepair.SelectedIndex >= 0 && GameState.CurrentHero.Gold >= SelectedItem.RepairCost;
            LblRepair.DataContext = SelectedItem;
        }

        #endregion Click

        #region Page-Manipulation Methods

        public TheSmithyPage() => InitializeComponent();

        private void Page_Loaded(object sender, RoutedEventArgs e) => UpdateSource();

        #endregion Page-Manipulation Methods
    }
}