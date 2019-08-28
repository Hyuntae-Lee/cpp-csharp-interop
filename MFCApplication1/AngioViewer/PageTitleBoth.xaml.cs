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
    /// Interaction logic for PageTitleBoth.xaml
    /// </summary>
    public partial class PageTitleBoth : UserControl
    {
        public PageTitleBoth()
        {
            InitializeComponent();

            DataContext = this;
        }

        public String ExamDescription_OD { get; set; }
        public String ExamDescription_OS { get; set; }
    }
}
