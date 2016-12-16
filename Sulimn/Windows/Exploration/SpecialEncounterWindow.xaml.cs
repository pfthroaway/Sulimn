using System.ComponentModel;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for SpecialEncounterWindow.xaml
    /// </summary>
    public partial class SpecialEncounterWindow
    {
        public SpecialEncounterWindow()
        {
            InitializeComponent();
        }

        internal ForestWindow RefToForestWindow { get; set; }

        private void windowSpecialEncounter_Closing(object sender, CancelEventArgs e)
        {
        }
    }
}