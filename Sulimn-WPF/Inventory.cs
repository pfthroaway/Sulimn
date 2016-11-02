using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Sulimn_WPF
{
    /// <summary>
    /// Represents a Hero's inventory.
    /// </summary>
    internal class Inventory : IEnumerable<Item>
    {
        private List<Item> _items = new List<Item>();

        internal ReadOnlyCollection<Item> Items
        {
            get { return new ReadOnlyCollection<Item>(_items); }
        }

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

        /// <summary>
        /// Gets all Items of specified Type.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Items of Ty[e</returns>
        internal List<T> GetItemsOfType<T>()
        {
            return Items.OfType<T>().ToList<T>();
        }

        public override string ToString()
        {
            string[] arrInventoryNames = new string[Items.Count];
            for (int i = 0; i < Items.Count; i++)
                arrInventoryNames[i] = Items[i].Name;

            return string.Join(",", arrInventoryNames);
        }

        public IEnumerator<Item> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

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
        public Inventory(IEnumerable<Item> itemList)
        {
            List<Item> newItems = new List<Item>();
            newItems.AddRange(itemList);

            _items = newItems;
        }

        #endregion Constructors
    }
}