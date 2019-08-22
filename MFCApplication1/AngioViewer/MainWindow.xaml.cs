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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //
            m_singlePage = new SinglePage();
            m_bothPage = new BothPage();
            m_compPage = new CompPage();
            m_progPage = new ProgPage();

            //
            this.tabBar.PageButtonClicked += TabBar_PageButtonClicked;

            // default page
            SwitchPage(m_singlePage);
        }

        private void SwitchPage(Page page)
        {
            this.page.Content = page;
        }

        private void TabBar_PageButtonClicked(string obj)
        {
            if (obj == Defs.PageName_Single)
            {
                SwitchPage(m_singlePage);
            }
            else if (obj == Defs.PageName_Both)
            {
                SwitchPage(m_bothPage);
            }
            else if (obj == Defs.PageName_Compare)
            {
                SwitchPage(m_compPage);
            }
            else if (obj == Defs.PageName_Progression)
            {
                SwitchPage(m_progPage);
            }
        }

        // pages
        Page m_singlePage;
        Page m_bothPage;
        Page m_compPage;
        Page m_progPage;
    }
}
