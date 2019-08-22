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

namespace AngioViewer
{
    /// <summary>
    /// Interaction logic for TabBar.xaml
    /// </summary>
    public partial class TabBar : UserControl
    {
        public event Action<string> PageButtonClicked;

        public TabBar()
        {
            InitializeComponent();
        }

        private void OnSingleButtonClick(object sender, RoutedEventArgs e)
        {
            PageButtonClicked(Defs.PageName_Single);
        }

        private void OnBothButtonClick(object sender, RoutedEventArgs e)
        {
            PageButtonClicked(Defs.PageName_Both);
        }

        private void OnCompareButtonClick(object sender, RoutedEventArgs e)
        {
            PageButtonClicked(Defs.PageName_Compare);
        }

        private void OnProgressionButtonClick(object sender, RoutedEventArgs e)
        {
            PageButtonClicked(Defs.PageName_Progression);
        }
    }
}
