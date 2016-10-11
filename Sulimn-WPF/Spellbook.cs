﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulimn_WPF
{
    class Spellbook
    {
        List<Spell> _spells = new List<Spell>();

        internal List<Spell> Spells
        {
            get { return _spells; }
            set { _spells = value; }
        }

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

            return String.Join(",", arrSpellNames);
        }

        #region Constructors
        public Spellbook()
        {

        }

        public Spellbook(List<Spell> spellList)
        {
            Spells = spellList;
        }
        #endregion
    }
}
