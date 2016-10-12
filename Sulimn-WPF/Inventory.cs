using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sulimn_WPF
{
    internal class Inventory : IEnumerable<Item>
    {
        private List<Item> _items = new List<Item>();

        internal List<Item> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        #region Inventory Management

        internal void AddItem(Item _item)
        {
            Items.Add(_item);
        }

        /// <summary>
        /// Removes an Item from Items.
        /// </summary>
        /// <param name="_item">Item to be removed</param>
        internal void RemoveItem(Item _item)
        {
            Items.Remove(_item);
        }

        /// <summary>
        /// Removes Inventory Item at specified index of Items List.
        /// </summary>
        /// <param name="index">Index of where to remove Item.</param>
        internal void RemoveItemAt(int index)
        {
            Items.RemoveAt(index);
        }

        #endregion Inventory Management

        internal List<T> GetItemsOfType<T>()
        {
            return Items.OfType<T>().ToList<T>();
        }

        public override string ToString()
        {
            string[] arrInventoryNames = new string[Items.Count];
            for (int i = 0; i < Items.Count; i++)
                arrInventoryNames[i] = Items[i].Name;

            return String.Join(",", arrInventoryNames);
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

        public Inventory()
        {
        }

        public Inventory(List<Item> itemList)
        {
            Items = itemList;
        }

        #endregion Constructors
    }
}