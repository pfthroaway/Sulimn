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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sulimn.Pages.Exploration
{
    /// <summary>Interaction logic for CastlePage.xaml</summary>
    public partial class CastlePage
    {
        internal ExplorePage RefToExplorePage { get; set; }

        #region Click Methods

        private void BtnCourtyard_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnBattlements_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnArmoury_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnSpire_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnThroneRoom_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
        }

        #endregion Click Methods

        #region Window-Manipulation Methods

        public CastlePage() => InitializeComponent();

        private void CastlePage_OnLoadedPage_OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        #endregion Window-Manipulation Methods
    }
}