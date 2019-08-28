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
            dataMap_od.AngiographyItemSelectionChanged += DataMap_od_AngiographyItemSelectionChanged;
            dataMap_os.AngiographyItemSelectionChanged += DataMap_os_AngiographyItemSelectionChanged;

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

        public void updateData(MeasurementData.ExamInfo examInfo_od, List<MeasurementData.AngiographyItem> angiographyList_od,
            MeasurementData.ExamInfo examInfo_os, List<MeasurementData.AngiographyItem> angiographyList_os)
        {
            angiography_od.setItemList(angiographyList_od, 0, examInfo_od.DataDir);
            var item_od = angiography_od.getCurrentAngiographyItem();

            // data map
            dataMap_od.setItemList(item_od.DataMapList, 0, examInfo_od.DataDir);

            // bscan
            bscanViewer_od.setLayerSettings(item_od.UpperLayer, item_od.UpperOffset, item_od.LowerLayer, item_od.LowerOffset);
        }

        private void BscanViewer_os_BScanIndexChanged(int obj)
        {
            throw new NotImplementedException();
        }

        private void BscanViewer_os_LayerSettingsChanged(MeasurementData.BScanLayerItem arg1, int arg2, MeasurementData.BScanLayerItem arg3, int arg4)
        {
            throw new NotImplementedException();
        }

        private void BscanViewer_od_BScanIndexChanged(int obj)
        {
            throw new NotImplementedException();
        }

        private void BscanViewer_od_LayerSettingsChanged(MeasurementData.BScanLayerItem arg1, int arg2, MeasurementData.BScanLayerItem arg3, int arg4)
        {
            throw new NotImplementedException();
        }

        private void GuideLine_ScanIndexChanged_od(int arg1, int arg2)
        {
            throw new NotImplementedException();
        }

        private void GuideLine_ScanIndexChanged_os(int arg1, int arg2)
        {
            throw new NotImplementedException();
        }

        private void DataMap_os_AngiographyItemSelectionChanged(MeasurementData.AngiographyItem obj)
        {
            throw new NotImplementedException();
        }

        private void DataMap_od_AngiographyItemSelectionChanged(MeasurementData.AngiographyItem obj)
        {
            throw new NotImplementedException();
        }

        private void Angiography_os_AngiographyItemSelectionChanged(MeasurementData.AngiographyItem obj)
        {
            throw new NotImplementedException();
        }

        private void Angiography_od_AngiographyItemSelectionChanged(MeasurementData.AngiographyItem obj)
        {
            throw new NotImplementedException();
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
