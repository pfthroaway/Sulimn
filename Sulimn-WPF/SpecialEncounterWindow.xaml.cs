using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        private void windowSpecialEncounter_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}