using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for CastSpellWindow.xaml
    /// </summary>
    public partial class CastSpellWindow : INotifyPropertyChanged
    {
        private BindingList<Spell> availableSpells = new BindingList<Spell>();

        private string PreviousWindow;
        private Spell selectedSpell = new Spell();

        internal BattleWindow RefToBattleWindow { get; set; }
        internal CharacterWindow RefToCharacterWindow { get; set; }

        /// <summary>
        /// Casts spell.
        /// </summary>
        /// <param name="spell">Spell to be cast</param>
        private void CastSpell(Spell spell)
        {
            CloseWindow();

            switch (PreviousWindow)
            {
                case "Battle":
                    RefToBattleWindow.SetSpell(spell);
                    break;

                case "Character":
                    RefToCharacterWindow.CastSpell(spell);
                    break;
            }
        }

        /// <summary>
        /// Loads the Window.
        /// </summary>
        internal void LoadWindow(string prevWindow)
        {
            PreviousWindow = prevWindow;
            DisplayKnownSpells();
            BindLabels();
            ClearSpellInfo();
        }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Binds text to the labels.
        /// </summary>
        internal void BindLabels()
        {
            lstSpells.DataContext = availableSpells;
            DataContext = selectedSpell;
            lblHealth.DataContext = GameState.CurrentHero.Statistics;
            lblMagic.DataContext = GameState.CurrentHero.Statistics;
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Display Manipulation

        /// <summary>
        /// Clears all text from all spell Labels.
        /// </summary>
        private void ClearSpellInfo()
        {
            lstSpells.UnselectAll();
            btnCastSpell.IsEnabled = false;
        }

        /// <summary>
        /// Displays list of Hero's known Spells.
        /// </summary>
        private void DisplayKnownSpells()
        {
            switch (PreviousWindow)
            {
                case "Battle":
                    availableSpells = new BindingList<Spell>(GameState.CurrentHero.Spellbook.Spells);
                    break;

                case "Character":
                    availableSpells =
                    new BindingList<Spell>(
                    GameState.CurrentHero.Spellbook.Spells.Where(spl => spl.Type == SpellTypes.Healing).ToList());
                    break;
            }
        }

        #endregion Display Manipulation

        #region Button-Click Methods

        private void btnCastSpell_Click(object sender, RoutedEventArgs e)
        {
            CastSpell(selectedSpell);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
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
                selectedSpell = spells.Find(spl => spl.Name == lstSpells.SelectedItem.ToString());

                btnCastSpell.IsEnabled = selectedSpell.MagicCost <= GameState.CurrentHero.Statistics.CurrentMagic;
            }
            else
            {
                selectedSpell = new Spell();
                btnCastSpell.IsEnabled = false;
            }

            DataContext = selectedSpell;
        }

        public CastSpellWindow()
        {
            InitializeComponent();
        }

        private void windowCastSpell_Closing(object sender, CancelEventArgs e)
        {
            switch (PreviousWindow)
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