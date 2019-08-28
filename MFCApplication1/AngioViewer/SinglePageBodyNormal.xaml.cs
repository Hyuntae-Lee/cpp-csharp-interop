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
    /// Interaction logic for SinglePageBodyNormal.xaml
    /// </summary>
    public partial class SinglePageBodyNormal : UserControl
    {
        SolidColorBrush kAngiographyViewBrushNormal = new SolidColorBrush(Colors.Transparent);
        SolidColorBrush kAngiographyViewBrushSelected = new SolidColorBrush(Colors.Red);

        public SinglePageBodyNormal()
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

            angiography_0.AngiographyItemSelectionChanged += Angiography_0_AngiographyItemSelectionChanged;
            angiography_1.AngiographyItemSelectionChanged += Angiography_1_AngiographyItemSelectionChanged;
            angiography_2.AngiographyItemSelectionChanged += Angiography_2_AngiographyItemSelectionChanged;
            angiography_3.AngiographyItemSelectionChanged += Angiography_3_AngiographyItemSelectionChanged;
            angiography_4.AngiographyItemSelectionChanged += Angiography_4_AngiographyItemSelectionChanged;

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

        private void Angiography_0_AngiographyItemSelectionChanged(MeasurementData.AngiographyItem angiographyItem)
        {
            selectAngiographyItem(angiography_0 as AngioGraphyViewer);
        }

        private void Angiography_1_AngiographyItemSelectionChanged(MeasurementData.AngiographyItem angiographyItem)
        {
            selectAngiographyItem(angiography_1 as AngioGraphyViewer);
        }

        private void Angiography_2_AngiographyItemSelectionChanged(MeasurementData.AngiographyItem angiographyItem)
        {
            selectAngiographyItem(angiography_2 as AngioGraphyViewer);
        }

        private void Angiography_3_AngiographyItemSelectionChanged(MeasurementData.AngiographyItem angiographyItem)
        {
            selectAngiographyItem(angiography_3 as AngioGraphyViewer);
        }

        private void Angiography_4_AngiographyItemSelectionChanged(MeasurementData.AngiographyItem angiographyItem)
        {
            selectAngiographyItem(angiography_4 as AngioGraphyViewer);
        }

        private void GuideLine_ScanIndexChanged(int value, int maxValue)
        {
            BScanIndex = (int)((float)(MeasurementData.Ins.Self.ExamInfo.BScanNum - 1) / (float)maxValue * (float)value);
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
            // reset ui
            angiography_0.BorderBrush = kAngiographyViewBrushNormal;
            angiography_1.BorderBrush = kAngiographyViewBrushNormal;
            angiography_2.BorderBrush = kAngiographyViewBrushNormal;
            angiography_3.BorderBrush = kAngiographyViewBrushNormal;
            angiography_4.BorderBrush = kAngiographyViewBrushNormal;

            var item = viewer.getCurrentAngiographyItem();

            // hightlight item
            viewer.BorderBrush = kAngiographyViewBrushSelected;

            // data map
            dataMap_0.setItemList(item.DataMapList, 0, MeasurementData.Ins.Self.ExamInfo.DataDir);

            // bscan
            bscanViewer.setLayerSettings(item.UpperLayer, item.UpperOffset, item.LowerLayer, item.LowerOffset);
        }

        private void Angiography_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //
            selectAngiographyItem(sender as AngioGraphyViewer);
        }

        public void updateData(MeasurementData.ExamInfo examInfo, List<MeasurementData.AngiographyItem> angiographyList)
        {
            //
            angiography_0.setItemList(angiographyList, 0, examInfo.DataDir);
            angiography_1.setItemList(angiographyList, 1, examInfo.DataDir);
            angiography_2.setItemList(angiographyList, 2, examInfo.DataDir);
            angiography_3.setItemList(angiographyList, 3, examInfo.DataDir);
            angiography_4.setItemList(angiographyList, 4, examInfo.DataDir);
        }

        private int BScanIndex
        {
            get
            {
                return m_bscanIndex;
            }

            set
            {
                if (!(value >= 0 && value < MeasurementData.Ins.Self.ExamInfo.BScanNum - 1))
                {
                    return;
                }

                m_bscanIndex = value;

                bscanViewer.updateBScanImage(m_bscanIndex);

                bool isVertical = !MeasurementData.Ins.Self.ExamInfo.Horizontal;
                int nMaxBScanIndex = MeasurementData.Ins.Self.ExamInfo.BScanNum - 1;

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
