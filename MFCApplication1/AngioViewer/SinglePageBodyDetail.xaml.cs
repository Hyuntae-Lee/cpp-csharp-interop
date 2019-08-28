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
    /// Interaction logic for SinglePageBodyDetail.xaml
    /// </summary>
    public partial class SinglePageBodyDetail : UserControl
    {
        const int kAngiographyViewSize = 420;
        const int kAngiographyImageSize = 360;

        public SinglePageBodyDetail()
        {
            InitializeComponent();

            angiography.Width = kAngiographyViewSize;
            angiography.Height = kAngiographyViewSize;
            angiography.itemImage.Width = kAngiographyImageSize;
            angiography.itemImage.Height = kAngiographyImageSize;
            angiography.guideLine.Width = kAngiographyImageSize;
            angiography.guideLine.Height = kAngiographyImageSize;
            dataMap.itemImage.Width = kAngiographyImageSize;
            dataMap.itemImage.Height = kAngiographyImageSize;
            dataMap.guideLine.Width = kAngiographyImageSize;
            dataMap.guideLine.Height = kAngiographyImageSize;
            
            //
            DataContext = this;

            // angio graphy
            angiography.guideLine.ScanIndexChanged += GuideLine_ScanIndexChanged;
            angiography.AngiographyItemSelectionChanged += Angiography_AngiographyItemSelectionChanged;

            dataMap.guideLine.ScanIndexChanged += GuideLine_ScanIndexChanged;

            // bscan
            bscanViewer.LayerSettingsChanged += BscanViewer_LayerSettingsChanged;
            bscanViewer.BScanIndexChanged += BscanViewer_BScanIndexChanged;
        }

        private void Angiography_AngiographyItemSelectionChanged(MeasurementData.AngiographyItem item)
        {
            // data map
            dataMap.setItemList(item.DataMapList, 0, MeasurementData.Ins.Self.ExamInfo.DataDir);

            // bscan
            bscanViewer.setLayerSettings(item.UpperLayer, item.UpperOffset, item.LowerLayer, item.LowerOffset);
        }

        public void initPage()
        {
            BScanIndex = 0;
        }

        public void updateData(MeasurementData.ExamInfo examInfo, List<MeasurementData.AngiographyItem> angiographyList)
        {
            angiography.setItemList(angiographyList, 0, examInfo.DataDir);
            var item = angiography.getCurrentAngiographyItem();

            // data map
            dataMap.setItemList(item.DataMapList, 0, examInfo.DataDir);

            // bscan
            bscanViewer.setLayerSettings(item.UpperLayer, item.UpperOffset, item.LowerLayer, item.LowerOffset);
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

        private void BscanViewer_LayerSettingsChanged(MeasurementData.BScanLayerItem upperLayer, int upperLayerOffset,
            MeasurementData.BScanLayerItem lowerLayer, int lowerLayerOffset)
        {
            // TODO : update angio graphy
            //throw new NotImplementedException();
        }

        private void GuideLine_ScanIndexChanged(int value, int maxValue)
        {
            BScanIndex = (int)((float)(MeasurementData.Ins.Self.ExamInfo.BScanNum - 1) / (float)maxValue * (float)value);
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

                angiography.setBScanIndex(m_bscanIndex, nMaxBScanIndex, isVertical);
                dataMap.setBScanIndex(m_bscanIndex, nMaxBScanIndex, isVertical);
            }
        }

        private int m_bscanIndex;
    }
}
