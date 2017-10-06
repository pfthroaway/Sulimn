using Sulimn.Classes.Items;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Sulimn.Classes.HeroParts
{
    /// <summary>Represents a Hero's inventory.</summary>
    internal class Inventory : IEnumerable<Item>, INotifyPropertyChanged
    {
        private List<Item> _items = new List<Item>();
        private int _gold;

        /// <summary>Gets all Items of specified Type.</summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Items of specified Type</returns>
        internal List<T> GetItemsOfType<T>() => Items.OfType<T>().ToList();

        #region Modifying Properties

        /// <summary>Amount of gold in the inventory.</summary>
        public int Gold
        {
            get => _gold;
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

        /// <summary>List of Items in the inventory.</summary>
        private ReadOnlyCollection<Item> Items => new ReadOnlyCollection<Item>(_items);

        /// <summary>Amount of gold in the inventory, with thousands separator.</summary>
        public string GoldToString => Gold.ToString("N0");

        /// <summary>Amount of gold in the inventory, with thousands separator and preceding text.</summary>
        public string GoldToStringWithText => $"Gold: {GoldToString}";

        #endregion Helper Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Inventory Management

        /// <summary>Adds an Item to the inventory.</summary>
        /// <param name="item">Item to be removed</param>
        internal void AddItem(Item item)
        {
            _items.Add(item);
            _items = Items.OrderBy(itm => itm.Name).ToList();
            OnPropertyChanged("Items");
        }

        /// <summary>Removes an Item from the inventory.</summary>
        /// <param name="item">Item to be removed</param>
        internal void RemoveItem(Item item)
        {
            _items.Remove(item);
            OnPropertyChanged("Items");
        }

        #endregion Inventory Management

        #region Enumerator

        public IEnumerator<Item> GetEnumerator() => Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion Enumerator

        #region Override Operators

        private static bool Equals(Inventory left, Inventory right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return left.Gold == right.Gold && !left.Items.Except(right.Items).Any(); ;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Inventory);

        public bool Equals(Inventory otherInventory) => Equals(this, otherInventory);

        public static bool operator ==(Inventory left, Inventory right) => Equals(left, right);

        public static bool operator !=(Inventory left, Inventory right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        public sealed override string ToString() => string.Join(",", Items);

        #endregion Override Operators

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

        /// <summary>Initializes an instance of Inventory by assigning the Inventory.</summary>
        /// <param name="itemList">List of Items in Inventory</param>
        /// <param name="gold">Gold</param>
        public Inventory(string itemList, int gold)
        {
            List<Item> newItems = new List<Item>();
            if (itemList.Length > 0)
            {
                string[] items = itemList.Split(',');
                newItems.AddRange(items.Select(str => GameState.AllItems.Find(item => item.Name == str.Trim())));
            }
            _items = newItems;
            Gold = gold;
        }

        /// <summary>Replaces this instance of Inventory with another instance.</summary>
        /// <param name="other">Instance of Inventory to replace this instance</param>
        public Inventory(Inventory other) : this(other.Items, other.Gold)
        {
        }

        #endregion Constructors
    }
}