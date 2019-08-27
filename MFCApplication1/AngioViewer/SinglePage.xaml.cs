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
    /// Interaction logic for SinglePage.xaml
    /// </summary>
    public partial class SinglePage
    {
        public SinglePage()
        {
            InitializeComponent();

            //
            DataContext = this;

            // angio graphy
            angiography_0.MouseLeftButtonDown += Angiography_MouseLeftButtonDown;
            angiography_1.MouseLeftButtonDown += Angiography_MouseLeftButtonDown;
            angiography_2.MouseLeftButtonDown += Angiography_MouseLeftButtonDown;
            angiography_3.MouseLeftButtonDown += Angiography_MouseLeftButtonDown;
            angiography_4.MouseLeftButtonDown += Angiography_MouseLeftButtonDown;

            angiography_0.guideLine.ScanIndexChanged += GuideLine_ScanIndexChanged;
            angiography_1.guideLine.ScanIndexChanged += GuideLine_ScanIndexChanged;
            angiography_2.guideLine.ScanIndexChanged += GuideLine_ScanIndexChanged;
            angiography_3.guideLine.ScanIndexChanged += GuideLine_ScanIndexChanged;
            angiography_4.guideLine.ScanIndexChanged += GuideLine_ScanIndexChanged;
            dataMap_0.guideLine.ScanIndexChanged += GuideLine_ScanIndexChanged;

            // bscan
            bscanViewer.LayerSettingsChanged += BscanViewer_LayerSettingsChanged;
            bscanViewer.BScanIndexChanged += BscanViewer_BScanIndexChanged;
        }

        private void GuideLine_ScanIndexChanged(int value, int maxValue)
        {
            BScanIndex = (int)((float)(MeasurementData.Ins.CurExamInfo.BScanNum - 1) / (float)maxValue * (float)value);
        }

        public void initPage()
        {
            selectAngiographyItem(angiography_0);
            BScanIndex = 0;
        }

        private void BscanViewer_BScanIndexChanged(int obj)
        {
            // inc
            if (obj > 0)
            {
                BScanIndex++;
            }
            // dec
            else
            {
                BScanIndex--;
            }
        }

        private void BscanViewer_LayerSettingsChanged(MeasurementData.BScanLayerItem arg1, int arg2, MeasurementData.BScanLayerItem arg3, int arg4)
        {
            // TODO : update angio graphy
            //throw new NotImplementedException();
        }

        private void selectAngiographyItem(AngioGraphyViewer viewer)
        {
            var item = viewer.getCurrentAngiographyItem();

            // hightlight item
            viewer.BorderBrush = new SolidColorBrush(Colors.Red);

            // data map
            dataMap_0.setItemList(item.DataMapList, 0, DataDir);

            // bscan
            bscanViewer.setLayerSettings(item.UpperLayer, item.UpperOffset, item.LowerLayer, item.LowerOffset);
        }

        private void Angiography_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // reset selection
            angiography_0.BorderBrush = new SolidColorBrush(Colors.Transparent);
            angiography_1.BorderBrush = new SolidColorBrush(Colors.Transparent);
            angiography_2.BorderBrush = new SolidColorBrush(Colors.Transparent);
            angiography_3.BorderBrush = new SolidColorBrush(Colors.Transparent);
            angiography_4.BorderBrush = new SolidColorBrush(Colors.Transparent);

            //
            selectAngiographyItem(sender as AngioGraphyViewer);
        }

        public void updateData(MeasurementData.ExamInfo examInfo, List<MeasurementData.AngiographyItem> angiographyList)
        {
            //
            pageTitle.ExamDescription = examInfo.toString();

            //
            DataDir = examInfo.DataDir;

            //
            angiography_0.setItemList(angiographyList, 0, DataDir);
            angiography_1.setItemList(angiographyList, 1, DataDir);
            angiography_2.setItemList(angiographyList, 2, DataDir);
            angiography_3.setItemList(angiographyList, 3, DataDir);
            angiography_4.setItemList(angiographyList, 4, DataDir);
        }

        private String DataDir { get; set; }
        private int BScanIndex
        {
            get
            {
                return m_bscanIndex;
            }

            set
            {
                if (!(value >= 0 && value < MeasurementData.Ins.CurExamInfo.BScanNum - 1))
                {
                    return;
                }

                m_bscanIndex = value;

                bscanViewer.updateBScanImage(m_bscanIndex);

                bool isVertical = !MeasurementData.Ins.CurExamInfo.Horizontal;
                int nMaxBScanIndex = MeasurementData.Ins.CurExamInfo.BScanNum - 1;

                angiography_0.setBScanIndex(m_bscanIndex, nMaxBScanIndex, isVertical);
                angiography_1.setBScanIndex(m_bscanIndex, nMaxBScanIndex, isVertical);
                angiography_2.setBScanIndex(m_bscanIndex, nMaxBScanIndex, isVertical);
                angiography_3.setBScanIndex(m_bscanIndex, nMaxBScanIndex, isVertical);
                angiography_4.setBScanIndex(m_bscanIndex, nMaxBScanIndex, isVertical);
                dataMap_0.setBScanIndex(m_bscanIndex, nMaxBScanIndex, isVertical);
            }
        }

        private int m_bscanIndex;
    }
}
