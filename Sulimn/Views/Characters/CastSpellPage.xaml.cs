using Sulimn.Classes;
using Sulimn.Classes.Enums;
using Sulimn.Classes.HeroParts;
using Sulimn.Views.Battle;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn.Views.Characters
{
    /// <summary>Interaction logic for CastSpellPage.xaml</summary>
    public partial class CastSpellPage : INotifyPropertyChanged
    {
        private BindingList<Spell> _availableSpells = new BindingList<Spell>();
        private string _previousPage;
        private Spell _selectedSpell = new Spell();

        internal BattlePage RefToBattlePage { private get; set; }
        internal CharacterPage RefToCharacterPage { private get; set; }

        /// <summary>Casts spell.</summary>
        /// <param name="spell">Spell to be cast</param>
        private void CastSpell(Spell spell)
        {
            GameState.GoBack();

            switch (_previousPage)
            {
                case "Battle":
                    GameState.CurrentHero.CurrentSpell = spell;
                    RefToBattlePage.LblSpell.DataContext = GameState.CurrentHero.CurrentSpell;
                    if (spell != null && spell != new Spell() && GameState.CurrentHero.Statistics.CurrentMagic >= spell.MagicCost)
                        RefToBattlePage.BtnCastSpell.IsEnabled = true;
                    break;

                case "Character":
                    RefToCharacterPage.CastSpell(spell);
                    break;
            }
        }

        /// <summary>Loads the Page.</summary>
        /// <param name="prevPage">Previous Page</param>
        internal void LoadPage(string prevPage)
        {
            _previousPage = prevPage;
            DisplayKnownSpells();
            BindLabels();
        }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Binds text to the labels.</summary>
        private void BindLabels()
        {
            LstSpells.DataContext = _availableSpells;
            DataContext = _selectedSpell;
            LblHealth.DataContext = GameState.CurrentHero.Statistics;
            LblMagic.DataContext = GameState.CurrentHero.Statistics;
        }

        protected void NotifyPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Display Manipulation

        /// <summary>Displays list of Hero's known Spells.</summary>
        private void DisplayKnownSpells()
        {
            switch (_previousPage)
            {
                case "Battle":
                    _availableSpells = new BindingList<Spell>(GameState.CurrentHero.Spellbook.Spells);
                    break;

                case "Character":
                    _availableSpells =
                        new BindingList<Spell>(
                            GameState.CurrentHero.Spellbook.Spells.Where(spl => spl.Type == SpellType.Healing).ToList());
                    break;
            }
        }

        #endregion Display Manipulation

        #region Button-Click Methods

        private void BtnCastSpell_Click(object sender, RoutedEventArgs e) => CastSpell(_selectedSpell);

        private void BtnBack_Click(object sender, RoutedEventArgs e) => GameState.GoBack();

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>

        private void LstSpells_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstSpells.SelectedIndex >= 0)
            {
                List<Spell> spells = new List<Spell>();
                spells.AddRange(GameState.CurrentHero.Spellbook.Spells);
                _selectedSpell = spells.Find(spl => spl.Name == LstSpells.SelectedItem.ToString());
            }
            else
                _selectedSpell = new Spell();

            BtnCastSpell.IsEnabled = LstSpells.SelectedIndex >= 0 && _selectedSpell.MagicCost <= GameState.CurrentHero.Statistics.CurrentMagic;
            DataContext = _selectedSpell;
        }

        public CastSpellPage() => InitializeComponent();

        #endregion Page-Manipulation Methods
    }
}