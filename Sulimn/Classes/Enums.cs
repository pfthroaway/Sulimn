namespace Sulimn
{
    /// <summary>
    /// Represents an action performed at the Bank.
    /// </summary>
    internal enum BankAction
    {
        Deposit,
        Withdrawal,
        Borrow,
        Repay
    }

    /// <summary>
    /// Represents the suit of a playing card.
    /// </summary>
    internal enum CardSuit
    {
        Spades,
        Hearts,
        Clubs,
        Diamonds
    }

    /// <summary>
    /// Represents the types of Weapons which exist.
    /// </summary>
    internal enum WeaponTypes
    {
        Melee,
        Ranged
    }

    /// <summary>
    /// Represents the types of Items which exist.
    /// </summary>
    internal enum ItemTypes
    {
        Weapon,
        Head,
        Body,
        Hands,
        Legs,
        Feet,
        Ring,
        Potion,
        Food,
        Spell
    }

    /// <summary>
    /// Represents the types of Food which exist.
    /// </summary>
    internal enum FoodTypes
    {
        Food,
        Drink
    }

    /// <summary>
    /// Represents the types of Potions which exist.
    /// </summary>
    internal enum PotionTypes
    {
        Healing,
        Curing,
        Magic
    }

    /// <summary>
    /// Represents the types of Spells which exist.
    /// </summary>
    internal enum SpellTypes
    {
        Damage,
        Healing,
        Curing,
        Shield
    }

    /// <summary>
    /// Represents an entity's status.
    /// </summary>
    internal enum Status
    {
        Normal,
        Posioned,
        Paralyzed
    }
}