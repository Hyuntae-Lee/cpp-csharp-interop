using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        public void updateData()
        {
            if (MeasurementData.Ins.Self.isEmpty())
            {
                btnSingle.IsEnabled = false;
                btnBoth.IsEnabled = false;
                btnComp.IsEnabled = false;
                btnProg.IsEnabled = false;
            }
            else
            {
                btnSingle.IsEnabled = true;
                btnBoth.IsEnabled = !MeasurementData.Ins.OtherSide.isEmpty();
                btnComp.IsEnabled = MeasurementData.Ins.CompList.Count > 0;
                btnProg.IsEnabled = MeasurementData.Ins.CompList.Count > 0;
            }
        }

        private void OnSingleButtonClick(object sender, RoutedEventArgs e)
        {
            resetBtnsStates();
            PageButtonClicked(Defs.kPageNameSingle);
            (sender as ToggleButton).IsChecked = true;
        }

        private void OnBothButtonClick(object sender, RoutedEventArgs e)
        {
            resetBtnsStates();
            PageButtonClicked(Defs.kPageNameBoth);
            (sender as ToggleButton).IsChecked = true;
        }

        private void OnCompareButtonClick(object sender, RoutedEventArgs e)
        {
            resetBtnsStates();
            PageButtonClicked(Defs.kPageNameCompare);
            (sender as ToggleButton).IsChecked = true;
        }

        private void OnProgressionButtonClick(object sender, RoutedEventArgs e)
        {
            resetBtnsStates();
            PageButtonClicked(Defs.kPageNameProgression);
            (sender as ToggleButton).IsChecked = true;
        }

        private void resetBtnsStates()
        {
            btnSingle.IsChecked = false;
            btnBoth.IsChecked = false;
            btnComp.IsChecked = false;
            btnProg.IsChecked = false;
        }
    }
}
