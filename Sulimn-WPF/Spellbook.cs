using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sulimn_WPF
{
    internal class Spellbook
    {
        private List<Spell> _spells = new List<Spell>();

        internal ReadOnlyCollection<Spell> Spells
        {
            get { return new ReadOnlyCollection<Spell>(_spells); }
        }

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

        public override string ToString()
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

        #endregion Constructors
    }
}