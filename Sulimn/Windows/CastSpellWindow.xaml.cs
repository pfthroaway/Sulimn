using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>Interaction logic for CastSpellWindow.xaml</summary>
    public partial class CastSpellWindow : INotifyPropertyChanged
    {
        private BindingList<Spell> _availableSpells = new BindingList<Spell>();
        private string _previousWindow;
        private Spell _selectedSpell = new Spell();

        internal BattleWindow RefToBattleWindow { private get; set; }
        internal CharacterWindow RefToCharacterWindow { private get; set; }

        /// <summary>Casts spell.</summary>
        /// <param name="spell">Spell to be cast</param>
        private void CastSpell(Spell spell)
        {
            CloseWindow();

            switch (_previousWindow)
            {
                case "Battle":
                    RefToBattleWindow.SetSpell(spell);
                    break;

                case "Character":
                    CharacterWindow.CastSpell(spell);
                    break;
            }
        }

        /// <summary>Loads the Window.</summary>
        internal void LoadWindow(string prevWindow)
        {
            _previousWindow = prevWindow;
            DisplayKnownSpells();
            BindLabels();
        }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Binds text to the labels.</summary>
        private void BindLabels()
        {
            lstSpells.DataContext = _availableSpells;
            DataContext = _selectedSpell;
            lblHealth.DataContext = GameState.CurrentHero.Statistics;
            lblMagic.DataContext = GameState.CurrentHero.Statistics;
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Display Manipulation

        /// <summary>Displays list of Hero's known Spells.</summary>
        private void DisplayKnownSpells()
        {
            switch (_previousWindow)
            {
                case "Battle":
                    _availableSpells = new BindingList<Spell>(GameState.CurrentHero.Spellbook.Spells);
                    break;

                case "Character":
                    _availableSpells =
                    new BindingList<Spell>(
                    GameState.CurrentHero.Spellbook.Spells.Where(spl => spl.Type == SpellTypes.Healing).ToList());
                    break;
            }
        }

        #endregion Display Manipulation

        #region Button-Click Methods

        private void btnCastSpell_Click(object sender, RoutedEventArgs e)
        {
            CastSpell(_selectedSpell);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            Close();
        }

        private void lstSpells_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstSpells.SelectedIndex >= 0)
            {
                List<Spell> spells = new List<Spell>();
                spells.AddRange(GameState.CurrentHero.Spellbook.Spells);
                _selectedSpell = spells.Find(spl => spl.Name == lstSpells.SelectedItem.ToString());
            }
            else
                _selectedSpell = new Spell();

            btnCastSpell.IsEnabled = lstSpells.SelectedIndex >= 0 && _selectedSpell.MagicCost <= GameState.CurrentHero.Statistics.CurrentMagic;
            DataContext = _selectedSpell;
        }

        public CastSpellWindow()
        {
            InitializeComponent();
        }

        private void windowCastSpell_Closing(object sender, CancelEventArgs e)
        {
            switch (_previousWindow)
            {
                case "Battle":
                    RefToBattleWindow.Show();
                    break;

                case "Character":
                    RefToCharacterWindow.Show();
                    break;
            }

            #endregion Window-Manipulation Methods
        }
    }
}