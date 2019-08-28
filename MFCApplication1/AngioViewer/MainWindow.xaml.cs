﻿using System;
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
        // parameters : store parameters as member variable for debugging
        public String DataDirSelf { get; set; }
        public String DBFilePath { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // init. controls
            m_singlePage = new SinglePage();
            m_bothPage = new BothPage();
            m_compPage = new CompPage();
            m_progPage = new ProgPage();

            // TTT
            DataDirSelf = "D:/HCT_DATA/20190614/000002/OCT_190614_165757_OD";
            DBFilePath = "D:/projects/etc/cpp-csharp-interop/MFCApplication1/MFCApplication1/HCT_DB.db";

            //
            OctDBAccessor.Ins.DBFilePath = DBFilePath;
            String infoFilePath = DataDirSelf + "/" + Defs.kAngioGraphyInfoFileName;

            // load data
            // - self
            Utils.LoadJsonTo(infoFilePath, MeasurementData.Ins.Self);
            // - other side
            var dirForOtherSide = OctDBAccessor.Ins.getOtherSide(DataDirSelf, MeasurementData.Ins.Self.ExamInfo.Side);
            if (dirForOtherSide != null)
            {
                Utils.LoadJsonTo(dirForOtherSide + "/" + Defs.kAngioGraphyInfoFileName,
                    MeasurementData.Ins.OtherSide);
            }

            // patient info. bar
            patientInfo.refreshData();

            // tab
            this.tabBar.PageButtonClicked += TabBar_PageButtonClicked;

            // default page
            switchToSinglePage();
        }

        private void switchToSinglePage()
        {
            page.Content = m_singlePage;
            m_singlePage.updateData();
            m_singlePage.initPage();
        }

        private void switchToBothPage()
        {
            page.Content = m_bothPage;
            m_bothPage.updateData();
            m_bothPage.initPage();
        }

        private void switchToCompPage()
        {
            page.Content = m_compPage;
        }

        private void switchToProgPage()
        {
            page.Content = m_progPage;
        }

        private void TabBar_PageButtonClicked(string obj)
        {
            if (obj == Defs.kPageNameSingle)
            {
                switchToSinglePage();
            }
            else if (obj == Defs.kPageNameBoth)
            {
                switchToBothPage();
            }
            else if (obj == Defs.kPageNameCompare)
            {
                switchToCompPage();
            }
            else if (obj == Defs.kPageNameProgression)
            {
                switchToProgPage();
            }
        }

        // pages
        SinglePage m_singlePage;
        BothPage m_bothPage;
        CompPage m_compPage;
        ProgPage m_progPage;
    }
}
