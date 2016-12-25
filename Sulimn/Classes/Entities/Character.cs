namespace Sulimn
{
    /// <summary>Represents living entities in Sulimn.</summary>
    internal abstract class Character
    {
        protected Attributes _attributes = new Attributes();
        protected Equipment _equipment = new Equipment();
        protected Inventory _inventory = new Inventory();
        protected int _level, _experience;
        protected string _name;
        protected Statistics _statistics = new Statistics();

        #region Properties

        public abstract string Name { get; set; }
        public abstract int Level { get; set; }
        public abstract int Experience { get; set; }
        public abstract Attributes Attributes { get; set; }
        public abstract Statistics Statistics { get; set; }
        public abstract Equipment Equipment { get; set; }
        public abstract Inventory Inventory { get; set; }

        #endregion Properties
    }
}