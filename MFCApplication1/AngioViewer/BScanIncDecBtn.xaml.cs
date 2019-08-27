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
    /// Interaction logic for BScanIncDecBtn.xaml
    /// </summary>
    public partial class BScanIncDecBtn : UserControl
    {
        public event Action<int> ButtonClicked;

        public BScanIncDecBtn()
        {
            InitializeComponent();

            btnDec.Click += BtnDec_Click;
            btnInc.Click += BtnInc_Click;
        }

        private void BtnInc_Click(object sender, RoutedEventArgs e)
        {
            ButtonClicked(1);
        }

        private void BtnDec_Click(object sender, RoutedEventArgs e)
        {
            ButtonClicked(-1);
        }
    }
}
