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
    /// Interaction logic for BothPage.xaml
    /// </summary>
    public partial class BothPage : Page
    {
        public BothPage()
        {
            InitializeComponent();

            DataContext = this;

            // angio graphy
            angiography_od.AngiographyItemSelectionChanged += Angiography_od_AngiographyItemSelectionChanged;
            angiography_os.AngiographyItemSelectionChanged += Angiography_os_AngiographyItemSelectionChanged;

            angiography_od.guideLine.ScanIndexChanged += GuideLine_ScanIndexChanged_od;
            angiography_os.guideLine.ScanIndexChanged += GuideLine_ScanIndexChanged_os;
            dataMap_od.guideLine.ScanIndexChanged += GuideLine_ScanIndexChanged_od;
            dataMap_os.guideLine.ScanIndexChanged += GuideLine_ScanIndexChanged_os;

            // bscan
            bscanViewer_od.LayerSettingsChanged += BscanViewer_od_LayerSettingsChanged;
            bscanViewer_od.BScanIndexChanged += BscanViewer_od_BScanIndexChanged;
            bscanViewer_os.LayerSettingsChanged += BscanViewer_os_LayerSettingsChanged;
            bscanViewer_os.BScanIndexChanged += BscanViewer_os_BScanIndexChanged;
        }

        public void initPage()
        {
            BScanIndex_OD = 0;
            BScanIndex_OS = 0;
        }

        public void updateData()
        {
            // od
            {
                var measData = MeasurementData.Ins.OD;
                var angiographyView = angiography_od;
                var dataMapView = dataMap_od;
                var bscanView = bscanViewer_od;
                if (measData != null)
                {
                    angiographyView.setItemList(measData.AngiographyItemList, 0, measData.ExamInfo.DataDir);
                    var item = angiographyView.getCurrentAngiographyItem();

                    // data map
                    dataMapView.setItemList(item.DataMapList, 0, measData.ExamInfo.DataDir);

                    // bscan
                    bscanView.setLayerSettings(item.UpperLayer, item.UpperOffset, item.LowerLayer, item.LowerOffset);
                }
            }

            // os
            {
                var measData = MeasurementData.Ins.OS;
                var angiographyView = angiography_os;
                var dataMapView = dataMap_os;
                var bscanView = bscanViewer_os;
                if (measData != null)
                {
                    angiographyView.setItemList(measData.AngiographyItemList, 0, measData.ExamInfo.DataDir);
                    var item = angiographyView.getCurrentAngiographyItem();

                    // data map
                    dataMapView.setItemList(item.DataMapList, 0, measData.ExamInfo.DataDir);

                    // bscan
                    bscanView.setLayerSettings(item.UpperLayer, item.UpperOffset, item.LowerLayer, item.LowerOffset);
                }
            }
        }

        private void BscanViewer_os_BScanIndexChanged(int obj)
        {
            // inc
            if (obj > 0)
            {
                BScanIndex_OS++;
            }
            // dec
            else
            {
                BScanIndex_OS--;
            }
        }

        private void BscanViewer_os_LayerSettingsChanged(MeasurementData.BScanLayerItem arg1, int arg2, MeasurementData.BScanLayerItem arg3, int arg4)
        {
            // TODO
            //throw new NotImplementedException();
        }

        private void BscanViewer_od_BScanIndexChanged(int obj)
        {
            // inc
            if (obj > 0)
            {
                BScanIndex_OD++;
            }
            // dec
            else
            {
                BScanIndex_OD--;
            }
        }

        private void BscanViewer_od_LayerSettingsChanged(MeasurementData.BScanLayerItem arg1, int arg2, MeasurementData.BScanLayerItem arg3, int arg4)
        {
            // TODO
            //throw new NotImplementedException();
        }

        private void GuideLine_ScanIndexChanged_od(int value, int maxValue)
        {
            BScanIndex_OD = (int)((float)(MeasurementData.Ins.OD.ExamInfo.BScanNum - 1) / (float)maxValue * (float)value);
        }

        private void GuideLine_ScanIndexChanged_os(int value, int maxValue)
        {
            BScanIndex_OS = (int)((float)(MeasurementData.Ins.OS.ExamInfo.BScanNum - 1) / (float)maxValue * (float)value);
        }

        private void Angiography_os_AngiographyItemSelectionChanged(MeasurementData.AngiographyItem item)
        {
            // data map
            dataMap_os.setItemList(item.DataMapList, 0, MeasurementData.Ins.OS.ExamInfo.DataDir);

            // bscan
            bscanViewer_os.setLayerSettings(item.UpperLayer, item.UpperOffset, item.LowerLayer, item.LowerOffset);
        }

        private void Angiography_od_AngiographyItemSelectionChanged(MeasurementData.AngiographyItem item)
        {
            // data map
            dataMap_od.setItemList(item.DataMapList, 0, MeasurementData.Ins.OD.ExamInfo.DataDir);

            // bscan
            bscanViewer_od.setLayerSettings(item.UpperLayer, item.UpperOffset, item.LowerLayer, item.LowerOffset);
        }

        private int BScanIndex_OD
        {
            get
            {
                return m_bscanIndex_od;
            }

            set
            {
                if (!(value >= 0 && value < MeasurementData.Ins.OD.ExamInfo.BScanNum - 1))
                {
                    return;
                }

                m_bscanIndex_od = value;

                bscanViewer_od.updateBScanImage(MeasurementData.Ins.OD.ExamInfo.DataDir, m_bscanIndex_od);

                bool isVertical = !MeasurementData.Ins.OD.ExamInfo.Horizontal;
                int nMaxBScanIndex = MeasurementData.Ins.OD.ExamInfo.BScanNum - 1;

                angiography_od.setBScanIndex(m_bscanIndex_od, nMaxBScanIndex, isVertical);
                dataMap_od.setBScanIndex(m_bscanIndex_od, nMaxBScanIndex, isVertical);
            }
        }

        private int BScanIndex_OS
        {
            get
            {
                return m_bscanIndex_os;
            }

            set
            {
                if (!(value >= 0 && value < MeasurementData.Ins.OS.ExamInfo.BScanNum - 1))
                {
                    return;
                }

                m_bscanIndex_os = value;

                bscanViewer_os.updateBScanImage(MeasurementData.Ins.OS.ExamInfo.DataDir, m_bscanIndex_os);

                bool isVertical = !MeasurementData.Ins.OS.ExamInfo.Horizontal;
                int nMaxBScanIndex = MeasurementData.Ins.OS.ExamInfo.BScanNum - 1;

                angiography_os.setBScanIndex(m_bscanIndex_os, nMaxBScanIndex, isVertical);
                dataMap_os.setBScanIndex(m_bscanIndex_os, nMaxBScanIndex, isVertical);
            }
        }

        private int m_bscanIndex_od;
        private int m_bscanIndex_os;
    }
}
