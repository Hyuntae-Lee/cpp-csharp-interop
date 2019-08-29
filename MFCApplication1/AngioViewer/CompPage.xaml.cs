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
    /// Interaction logic for CompPage.xaml
    /// </summary>
    public partial class CompPage : Page
    {
        public CompPage()
        {
            InitializeComponent();

            DataContext = this;

            // self
            angiography_self.AngiographyItemSelectionChanged += Angiography_self_AngiographyItemSelectionChanged;
            angiography_self.guideLine.ScanIndexChanged += GuideLine_ScanIndexChanged_Self;
            dataMap_self.guideLine.ScanIndexChanged += GuideLine_ScanIndexChanged_Self;
            bscanViewer_self.LayerSettingsChanged += BscanViewer_self_LayerSettingsChanged;
            bscanViewer_self.BScanIndexChanged += BscanViewer_self_BScanIndexChanged;

            // target
            angiography_target.AngiographyItemSelectionChanged += Angiography_target_AngiographyItemSelectionChanged;
            angiography_target.guideLine.ScanIndexChanged += GuideLine_ScanIndexChanged_Target;
            dataMap_target.guideLine.ScanIndexChanged += GuideLine_ScanIndexChanged_Target;
            bscanViewer_target.LayerSettingsChanged += BscanViewer_target_LayerSettingsChanged;
            bscanViewer_target.BScanIndexChanged += BscanViewer_target_BScanIndexChanged;
        }

        public void initPage()
        {
            BScanIndex_Self = 0;
            BScanIndex_Target = 0;
            m_targetIndex = 0;
        }

        public void updateData()
        {
            // self
            {
                var measData = MeasurementData.Ins.Self;
                var angiographyView = angiography_self;
                var dataMapView = dataMap_self;
                var bscanView = bscanViewer_self;
                if (measData != null)
                {
                    pageTitle.ExamDescription_OD = measData.ExamInfo.toString();

                    angiographyView.setItemList(measData.AngiographyItemList, 0, measData.ExamInfo.DataDir);
                    var item = angiographyView.getCurrentAngiographyItem();

                    // data map
                    dataMapView.setItemList(item.DataMapList, 0, measData.ExamInfo.DataDir);

                    // bscan
                    bscanView.setLayerSettings(item.UpperLayer, item.UpperOffset, item.LowerLayer, item.LowerOffset);
                }
            }

            // target
            {
                var measData = Target;
                var angiographyView = angiography_target;
                var dataMapView = dataMap_target;
                var bscanView = bscanViewer_target;
                if (measData != null)
                {
                    pageTitle.ExamDescription_OS = measData.ExamInfo.toString();

                    angiographyView.setItemList(measData.AngiographyItemList, 0, measData.ExamInfo.DataDir);
                    var item = angiographyView.getCurrentAngiographyItem();

                    // data map
                    dataMapView.setItemList(item.DataMapList, 0, measData.ExamInfo.DataDir);

                    // bscan
                    bscanView.setLayerSettings(item.UpperLayer, item.UpperOffset, item.LowerLayer, item.LowerOffset);
                }
            }
        }

        private void BscanViewer_target_BScanIndexChanged(int obj)
        {
            // inc
            if (obj > 0)
            {
                BScanIndex_Target++;
            }
            // dec
            else
            {
                BScanIndex_Target--;
            }
        }

        private void BscanViewer_target_LayerSettingsChanged(MeasurementData.BScanLayerItem arg1, int arg2, MeasurementData.BScanLayerItem arg3, int arg4)
        {
            // TODO
            //throw new NotImplementedException();
        }

        private void GuideLine_ScanIndexChanged_Target(int value, int maxValue)
        {
            if (Target == null)
            {
                return;
            }

            BScanIndex_Target = (int)((float)(Target.ExamInfo.BScanNum - 1) / (float)maxValue * (float)value);
        }

        private void Angiography_target_AngiographyItemSelectionChanged(MeasurementData.AngiographyItem item)
        {
            if (Target == null)
            {
                return;
            }

            // data map
            dataMap_target.setItemList(item.DataMapList, 0, Target.ExamInfo.DataDir);

            // bscan
            bscanViewer_target.setLayerSettings(item.UpperLayer, item.UpperOffset, item.LowerLayer, item.LowerOffset);
        }

        private void BscanViewer_self_BScanIndexChanged(int obj)
        {
            // inc
            if (obj > 0)
            {
                BScanIndex_Self++;
            }
            // dec
            else
            {
                BScanIndex_Self--;
            }
        }

        private void BscanViewer_self_LayerSettingsChanged(MeasurementData.BScanLayerItem arg1, int arg2, MeasurementData.BScanLayerItem arg3, int arg4)
        {
            // TODO
            //throw new NotImplementedException();
        }

        private void GuideLine_ScanIndexChanged_Self(int value, int maxValue)
        {
            BScanIndex_Self = (int)((float)(MeasurementData.Ins.Self.ExamInfo.BScanNum - 1) / (float)maxValue * (float)value);
        }

        private void Angiography_self_AngiographyItemSelectionChanged(MeasurementData.AngiographyItem item)
        {
            // data map
            dataMap_self.setItemList(item.DataMapList, 0, MeasurementData.Ins.Self.ExamInfo.DataDir);

            // bscan
            bscanViewer_self.setLayerSettings(item.UpperLayer, item.UpperOffset, item.LowerLayer, item.LowerOffset);
        }

        private int BScanIndex_Self
        {
            get
            {
                return m_bscanIndex_self;
            }

            set
            {
                if (!(value >= 0 && value < MeasurementData.Ins.Self.ExamInfo.BScanNum - 1))
                {
                    return;
                }

                m_bscanIndex_self = value;

                bscanViewer_self.updateBScanImage(MeasurementData.Ins.Self.ExamInfo.DataDir, m_bscanIndex_self);

                bool isVertical = !MeasurementData.Ins.Self.ExamInfo.Horizontal;
                int nMaxBScanIndex = MeasurementData.Ins.Self.ExamInfo.BScanNum - 1;

                angiography_self.setBScanIndex(m_bscanIndex_self, nMaxBScanIndex, isVertical);
                dataMap_self.setBScanIndex(m_bscanIndex_self, nMaxBScanIndex, isVertical);
            }
        }

        private int BScanIndex_Target
        {
            get
            {
                return m_bscanIndex_target;
            }

            set
            {
                if (Target == null)
                {
                    return;
                }

                if (!(value >= 0 && value < Target.ExamInfo.BScanNum - 1))
                {
                    return;
                }

                m_bscanIndex_target = value;

                bscanViewer_target.updateBScanImage(Target.ExamInfo.DataDir, m_bscanIndex_target);

                bool isVertical = !Target.ExamInfo.Horizontal;
                int nMaxBScanIndex = Target.ExamInfo.BScanNum - 1;

                angiography_target.setBScanIndex(m_bscanIndex_target, nMaxBScanIndex, isVertical);
                dataMap_target.setBScanIndex(m_bscanIndex_target, nMaxBScanIndex, isVertical);
            }
        }

        private MeasurementData.Item Target
        {
            get
            {
                if (MeasurementData.Ins.CompList.Count == 0)
                {
                    return null;
                }
                else
                {
                    return MeasurementData.Ins.CompList.ElementAt(m_targetIndex);
                }
            }
        }

        private int m_bscanIndex_self;
        private int m_bscanIndex_target;
        private int m_targetIndex;
    }
}
