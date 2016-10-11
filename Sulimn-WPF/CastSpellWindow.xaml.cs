using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Sulimn_WPF
{
    /// <summary>
    /// Interaction logic for CastSpellWindow.xaml
    /// </summary>
    public partial class CastSpellWindow : Window, INotifyPropertyChanged
    {
        private BindingList<Spell> availableSpells = new BindingList<Spell>();
        private Spell selectedSpell = new Spell();

        internal BattleWindow RefToBattleWindow { get; set; }
        internal CharacterWindow RefToCharacterWindow { get; set; }

        private string PreviousWindow;

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Binds text to the labels.
        /// </summary>
        internal void BindLabels()
        {
            lstSpells.DataContext = availableSpells;
            DataContext = selectedSpell;
            lblMagic.DataContext = GameState.currentHero;
        }

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

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
                    RefToBattleWindow.CastSpell(spell);
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
                    availableSpells = new BindingList<Spell>(GameState.currentHero.Spellbook.Spells);
                    break;

                case "Character":
                    availableSpells = new BindingList<Spell>(GameState.currentHero.Spellbook.Spells.Where(spl => spl.Type == "Healing").ToList());
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
        /// Closes the form.
        /// </summary>
        private void CloseWindow()
        {
            this.Close();
        }

        private void lstSpells_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstSpells.SelectedIndex >= 0)
            {
                selectedSpell = GameState.currentHero.Spellbook.Spells.Find(spl => spl.Name == lstSpells.SelectedItem.ToString());

                if (selectedSpell.MagicCost <= GameState.currentHero.CurrentMagic)
                {
                    btnCastSpell.IsEnabled = true;
                }
                else
                    btnCastSpell.IsEnabled = false;
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