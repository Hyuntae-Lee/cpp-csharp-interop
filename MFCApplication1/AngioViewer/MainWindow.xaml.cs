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
            this.FontFamily = new FontFamily("Noto Sans CJK KR Medium");
            this.FontSize = 16;

            // init. controls
            m_singlePage = new SinglePage();
            m_bothPage = new BothPage();
            m_compPage = new CompPage();
            m_progPage = new ProgPage();

            // TTT
            MeasurementData.Ins.DataDirSelf = "D:/HCT_DATA/20190614/000002/OCT_190614_165757_OD";
            OctDBAccessor.Ins.DBFilePath = "D:/projects/etc/cpp-csharp-interop/MFCApplication1/MFCApplication1/HCT_DB.db";

            // load data
            // - self
            Utils.LoadJsonTo(MeasurementData.Ins.DataDirSelf, Defs.kAngioGraphyInfoFileName, MeasurementData.Ins.Self);
            // - other side
            var dirForOtherSide = OctDBAccessor.Ins.getOtherSide(MeasurementData.Ins.DataDirSelf, MeasurementData.Ins.Self.ExamInfo.Side);
            if (dirForOtherSide != null)
            {
                Utils.LoadJsonTo(dirForOtherSide, Defs.kAngioGraphyInfoFileName, MeasurementData.Ins.OtherSide);
            }
            // - comparables
            var dirListForComp = OctDBAccessor.Ins.getCompables(MeasurementData.Ins.DataDirSelf, MeasurementData.Ins.Self.ExamInfo.Side);
            if (dirListForComp.Count > 0)
            {
                var compItem = new MeasurementData.Item();
                Utils.LoadJsonTo(dirForOtherSide, Defs.kAngioGraphyInfoFileName, compItem);
                MeasurementData.Ins.CompList.Add(compItem);
            }

            // patient info. bar
            patientInfo.refreshData();
            patientInfo.btnReCalc.Click += BtnReCalc_Click;

            updateData();

            // tab
            tabBar.updateData();
            tabBar.PageButtonClicked += TabBar_PageButtonClicked;

            // default page
            switchPage(m_singlePage);
        }

        private void BtnReCalc_Click(object sender, RoutedEventArgs e)
        {
            CmdManager.Ins.ReqReCalc(MeasurementData.Ins.Self.ExamInfo.DataDir);
        }

        private void updateData()
        {
            m_singlePage.updateData();
            m_singlePage.initPage();

            m_bothPage.updateData();
            m_bothPage.initPage();

            m_compPage.updateData();
            m_compPage.initPage();
        }

        private void switchPage(Page targetPage)
        {
            page.Content = targetPage;
        }

        private void TabBar_PageButtonClicked(string obj)
        {
            if (obj == Defs.kPageNameSingle)
            {
                switchPage(m_singlePage);
            }
            else if (obj == Defs.kPageNameBoth)
            {
                switchPage(m_bothPage);
            }
            else if (obj == Defs.kPageNameCompare)
            {
                switchPage(m_compPage);
            }
            else if (obj == Defs.kPageNameProgression)
            {
                switchPage(m_progPage);
            }
        }

        // pages
        SinglePage m_singlePage;
        BothPage m_bothPage;
        CompPage m_compPage;
        ProgPage m_progPage;
    }
}
