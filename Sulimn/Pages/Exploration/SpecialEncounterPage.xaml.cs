using System.ComponentModel;

namespace Sulimn.Pages.Exploration
{
    /// <summary>Interaction logic for SpecialEncounterPage.xaml</summary>
    public partial class SpecialEncounterPage
    {
        public SpecialEncounterPage()
        {
            InitializeComponent();
        }

        internal ForestPage RefToForestPage { get; set; }

        private void PageSpecialEncounter_Closing(object sender, CancelEventArgs e)
        {
        }
    }
}