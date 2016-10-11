using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulimn_WPF
{
    /// <summary>
    /// Base class of living entities in Sulimn.
    /// </summary>
    internal abstract class Character
    {
        #region Properties

        abstract public string Name { get; set; }
        abstract public int Level { get; set; }
        abstract public int Experience { get; set; }
        abstract public int Strength { get; set; }
        abstract public int Vitality { get; set; }
        abstract public int Dexterity { get; set; }
        abstract public int Wisdom { get; set; }
        abstract public int Gold { get; set; }
        abstract public int CurrentHealth { get; set; }
        abstract public int MaximumHealth { get; set; }
        abstract public int CurrentMagic { get; set; }
        abstract public int MaximumMagic { get; set; }
        abstract public Weapon Weapon { get; set; }
        abstract public Armor Head { get; set; }
        abstract public Armor Body { get; set; }
        abstract public Armor Legs { get; set; }
        abstract public Armor Feet { get; set; }

        #endregion Properties

        abstract internal string TakeDamage(int damage);

        abstract internal string Heal(int healAmount);
    }
}