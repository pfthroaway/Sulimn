using System.ComponentModel;

namespace Sulimn.Windows.Exploration
{
    /// <summary>Interaction logic for SpecialEncounterWindow.xaml</summary>
    public partial class SpecialEncounterWindow
    {
        public SpecialEncounterWindow()
        {
            InitializeComponent();
        }

        internal ForestWindow RefToForestWindow { get; set; }

        private void WindowSpecialEncounter_Closing(object sender, CancelEventArgs e)
        {
        }
    }
}