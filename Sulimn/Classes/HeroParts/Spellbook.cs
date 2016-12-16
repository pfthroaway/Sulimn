using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sulimn
{
    /// <summary>
    /// Represents a collection of Spells a Hero can cast.
    /// </summary>
    internal class Spellbook
    {
        private readonly List<Spell> _spells = new List<Spell>();

        internal ReadOnlyCollection<Spell> Spells => new ReadOnlyCollection<Spell>(_spells);

        /// <summary>
        /// Teaches a Hero a Spell.
        /// </summary>
        /// <param name="newSpell">Spell to be learned</param>
        /// <returns>String saying Hero learned the spell</returns>
        internal string LearnSpell(Spell newSpell)
        {
            _spells.Add(newSpell);
            return "You learn " + newSpell.Name + ".";
        }

        public sealed override string ToString()
        {
            string[] arrSpellNames = new string[Spells.Count];
            for (int i = 0; i < Spells.Count; i++)
                arrSpellNames[i] = Spells[i].Name;

            return string.Join(",", arrSpellNames);
        }

        #region Constructors

        /// <summary>
        /// Initializes a default instance of Spellbook.
        /// </summary>
        public Spellbook()
        {
        }

        /// <summary>
        /// Initializes a new instance of Spellbook by assigning known spells.
        /// </summary>
        /// <param name="spellList">List of known spells</param>
        public Spellbook(IEnumerable<Spell> spellList)
        {
            List<Spell> newSpells = new List<Spell>();
            newSpells.AddRange(spellList);
            _spells = newSpells;
        }

        /// <summary>
        /// Replaces this instance of Spellbook with another instance.
        /// </summary>
        /// <param name="otherSpellbook">Instance of Spellbook to replace this instance</param>
        public Spellbook(Spellbook otherSpellbook)
        {
            _spells = new List<Spell>(otherSpellbook.Spells);
        }

        #endregion Constructors
    }
}