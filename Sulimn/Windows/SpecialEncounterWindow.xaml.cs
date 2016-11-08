using System.ComponentModel;
using System.Windows;

namespace Sulimn_WPF
{
    /// <summary>
    /// Interaction logic for SpecialEncounterWindow.xaml
    /// </summary>
    public partial class SpecialEncounterWindow : Window
    {
        internal ForestWindow RefToForestWindow { get; set; }

        public SpecialEncounterWindow()
        {
            InitializeComponent();
        }

        private void windowSpecialEncounter_Closing(object sender, CancelEventArgs e)
        {
        }
    }
}