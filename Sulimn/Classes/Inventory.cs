using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Sulimn
{
    /// <summary>
    /// Represents a Hero's inventory.
    /// </summary>
    internal class Inventory : IEnumerable<Item>, INotifyPropertyChanged
    {
        private List<Item> _items = new List<Item>();
        private int _gold = 0;

        #region Modifying Properties

        internal ReadOnlyCollection<Item> Items
        {
            get { return new ReadOnlyCollection<Item>(_items); }
        }

        public int Gold
        {
            get { return _gold; }
            set { _gold = value; OnPropertyChanged("Gold"); OnPropertyChanged("GoldToString"); OnPropertyChanged("GoldToStringWithText"); }
        }

        #endregion Modifying Properties

        #region Helper Properties

        public string GoldToString
        {
            get { return Gold.ToString("N0"); }
        }

        public string GoldToStringWithText
        {
            get { return "Gold: " + GoldToString; }
        }

        #endregion Helper Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Inventory Management

        internal void AddItem(Item _item)
        {
            _items.Add(_item);
        }

        /// <summary>
        /// Removes an Item from Items.
        /// </summary>
        /// <param name="_item">Item to be removed</param>
        internal void RemoveItem(Item _item)
        {
            _items.Remove(_item);
        }

        /// <summary>
        /// Removes Inventory Item at specified index of Items List.
        /// </summary>
        /// <param name="index">Index of where to remove Item.</param>
        internal void RemoveItemAt(int index)
        {
            _items.RemoveAt(index);
        }

        #endregion Inventory Management

        /// <summary>Gets all Items of specified Type.</summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Items of Ty[e</returns>
        internal List<T> GetItemsOfType<T>()
        {
            return Items.OfType<T>().ToList<T>();
        }

        public sealed override string ToString()
        {
            string[] arrInventoryNames = new string[Items.Count];
            for (int i = 0; i < Items.Count; i++)
                arrInventoryNames[i] = Items[i].Name;

            return string.Join(",", arrInventoryNames);
        }

        #region Enumerator

        public IEnumerator<Item> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion Enumerator

        #region Constructors

        /// <summary>
        /// Initalizes a default instance of Inventory.
        /// </summary>
        public Inventory()
        {
        }

        /// <summary>
        /// Initializes an instance of Inventory by assigning the Inventory.
        /// </summary>
        /// <param name="itemList">List of Items in Inventory</param>
        /// <param name="gold">Gold</param>
        public Inventory(IEnumerable<Item> itemList, int gold)
        {
            List<Item> newItems = new List<Item>();
            newItems.AddRange(itemList);

            _items = newItems;
            Gold = gold;
        }

        /// <summary>
        /// Replaces this instance of Inventory with another instance.
        /// </summary>
        /// <param name="otherInventory">Instance of Inventory to replace this instance</param>
        public Inventory(Inventory otherInventory)
        {
            _items = new List<Item>(otherInventory.Items);
            Gold = otherInventory.Gold;
        }

        #endregion Constructors
    }
}