using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Sulimn
{
    /// <summary>Represents a Hero's inventory.</summary>
    internal class Inventory : IEnumerable<Item>, INotifyPropertyChanged
    {
        private readonly List<Item> _items = new List<Item>();
        private int _gold;

        /// <summary>Gets all Items of specified Type.</summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Items of specified Type</returns>
        internal List<T> GetItemsOfType<T>()
        {
            return Items.OfType<T>().ToList();
        }

        public sealed override string ToString()
        {
            string[] arrInventoryNames = new string[Items.Count];
            for (int i = 0; i < Items.Count; i++)
                arrInventoryNames[i] = Items[i].Name;

            return string.Join(",", arrInventoryNames);
        }

        #region Modifying Properties

        /// <summary>List of Items in the inventory.</summary>
        internal ReadOnlyCollection<Item> Items => new ReadOnlyCollection<Item>(_items);

        /// <summary>Amount of gold in the inventory.</summary>
        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged("Gold");
                OnPropertyChanged("GoldToString");
                OnPropertyChanged("GoldToStringWithText");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Amount of gold in the inventory, with thousands separator.</summary>
        public string GoldToString => Gold.ToString("N0");

        /// <summary>Amount of gold in the inventory, with thousands separator and preceding text.</summary>
        public string GoldToStringWithText => "Gold: " + GoldToString;

        #endregion Helper Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Inventory Management

        /// <summary>Adds an Item to the inventory.</summary>
        /// <param name="item">Item to be removed</param>
        internal void AddItem(Item item)
        {
            _items.Add(item);
        }

        /// <summary>Removes an Item from the inventory.</summary>
        /// <param name="item">Item to be removed</param>
        internal void RemoveItem(Item item)
        {
            _items.Remove(item);
        }

        #endregion Inventory Management

        #region Enumerator

        public IEnumerator<Item> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion Enumerator

        #region Constructors

        /// <summary>Initalizes a default instance of Inventory.</summary>
        public Inventory()
        {
        }

        /// <summary>Initializes an instance of Inventory by assigning the Inventory.</summary>
        /// <param name="itemList">List of Items in Inventory</param>
        /// <param name="gold">Gold</param>
        public Inventory(IEnumerable<Item> itemList, int gold)
        {
            List<Item> newItems = new List<Item>();
            newItems.AddRange(itemList);

            _items = newItems;
            Gold = gold;
        }

        /// <summary>Replaces this instance of Inventory with another instance.</summary>
        /// <param name="otherInventory">Instance of Inventory to replace this instance</param>
        public Inventory(Inventory otherInventory)
        {
            _items = new List<Item>(otherInventory.Items);
            Gold = otherInventory.Gold;
        }

        #endregion Constructors
    }
}