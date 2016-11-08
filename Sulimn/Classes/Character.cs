namespace Sulimn_WPF
{
    /// <summary>
    /// Represents living entities in Sulimn.
    /// </summary>
    internal abstract class Character
    {
        #region Properties

        public abstract string Name { get; set; }
        public abstract int Level { get; set; }
        public abstract int Experience { get; set; }
        public abstract int Strength { get; set; }
        public abstract int Vitality { get; set; }
        public abstract int Dexterity { get; set; }
        public abstract int Wisdom { get; set; }
        public abstract int Gold { get; set; }
        public abstract int CurrentHealth { get; set; }
        public abstract int MaximumHealth { get; set; }
        public abstract int CurrentMagic { get; set; }
        public abstract int MaximumMagic { get; set; }
        public abstract Weapon Weapon { get; set; }
        public abstract Armor Head { get; set; }
        public abstract Armor Body { get; set; }
        public abstract Armor Legs { get; set; }
        public abstract Armor Feet { get; set; }

        #endregion Properties

        internal abstract string TakeDamage(int damage);

        internal abstract string Heal(int healAmount);
    }
}