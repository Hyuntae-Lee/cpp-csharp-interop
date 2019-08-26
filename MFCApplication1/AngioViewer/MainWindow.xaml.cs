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

            // init. controls
            m_singlePage = new SinglePage();
            m_bothPage = new BothPage();
            m_compPage = new CompPage();
            m_progPage = new ProgPage();

            // TTT
            MeasurementData.Ins.DataDir = "D:/HCT_DATA/20190614/000002/OCT_190614_165757_OD";

            // load data
            String infoFilePath = MeasurementData.Ins.DataDir + "/" + Defs.kAngioGraphyInfoFileName;
            Utils.LoadJsonTo(infoFilePath, MeasurementData.Ins);

            // patient info. bar
            patientInfo.refreshData();

            // page
            updatePageData();

            // tab
            this.tabBar.PageButtonClicked += TabBar_PageButtonClicked;

            // default page
            SwitchPage(m_singlePage);

            m_singlePage.initPage();
        }

        private void updatePageData()
        {
            m_singlePage.updateData(MeasurementData.Ins.CurExamInfo, MeasurementData.Ins.CurAngiographyItemList);

            //m_bothPage;
            //m_compPage;
            //m_progPage;
        }

        private void SwitchPage(Page page)
        {
            this.page.Content = page;
        }

        private void TabBar_PageButtonClicked(string obj)
        {
            if (obj == Defs.kPageNameSingle)
            {
                SwitchPage(m_singlePage);
            }
            else if (obj == Defs.kPageNameBoth)
            {
                SwitchPage(m_bothPage);
            }
            else if (obj == Defs.kPageNameCompare)
            {
                SwitchPage(m_compPage);
            }
            else if (obj == Defs.kPageNameProgression)
            {
                SwitchPage(m_progPage);
            }
        }

        // pages
         SinglePage m_singlePage;
         BothPage m_bothPage;
         CompPage m_compPage;
         ProgPage m_progPage;
    }
}
