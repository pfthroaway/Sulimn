using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Sulimn.Classes.HeroParts
{
    /// <summary>Represents a collection of Spells a Hero can cast.</summary>
    internal class Spellbook : INotifyPropertyChanged
    {
        private readonly List<Spell> _spells = new List<Spell>();

        /// <summary>List of known Spells.</summary>
        internal ReadOnlyCollection<Spell> Spells => new ReadOnlyCollection<Spell>(_spells);

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        /// <summary>Teaches a Hero a Spell.</summary>
        /// <param name="newSpell">Spell to be learned</param>
        /// <returns>String saying Hero learned the spell</returns>
        internal string LearnSpell(Spell newSpell)
        {
            _spells.Add(newSpell);
            OnPropertyChanged("Spells");
            return $"You learn {newSpell.Name}.";
        }

        #region Override Operators

        private static bool Equals(Spellbook left, Spellbook right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left) ^ ReferenceEquals(null, right)) return false;
            return !left.Spells.Except(right.Spells).Any();
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Spellbook);

        public bool Equals(Spellbook otherSpellbook) => Equals(this, otherSpellbook);

        public static bool operator ==(Spellbook left, Spellbook right) => Equals(left, right);

        public static bool operator !=(Spellbook left, Spellbook right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        public sealed override string ToString() => string.Join(",", Spells);

        #endregion Override Operators

        #region Constructors

        /// <summary>Initializes a default instance of Spellbook.</summary>
        public Spellbook()
        {
        }

        /// <summary>Initializes a new instance of Spellbook by assigning known spells.</summary>
        /// <param name="spellList">List of known spells</param>
        public Spellbook(IEnumerable<Spell> spellList)
        {
            List<Spell> newSpells = new List<Spell>();
            newSpells.AddRange(spellList);
            _spells = newSpells;
        }

        /// <summary>Replaces this instance of Spellbook with another instance.</summary>
        /// <param name="other">Instance of Spellbook to replace this instance</param>
        public Spellbook(Spellbook other) : this(new List<Spell>(other.Spells))
        {
        }

        #endregion Constructors
    }
}