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
    /// Interaction logic for BScanViewerSingle.xaml
    /// </summary>
    public partial class BScanViewerSingle : UserControl
    {
        public event Action<MeasurementData.BScanLayerItem, int, MeasurementData.BScanLayerItem, int> LayerSettingsChanged;
        public event Action<int> BScanIndexChanged;

        public BScanViewerSingle()
        {
            InitializeComponent();

            layerSelectorUpper.ItemSettingChanged += LayerSelectorUpper_ItemSettingChanged;
            layerSelectorLower.ItemSettingChanged += LayerSelectorLower_ItemSettingChanged;

            bscanChangeBtn.ButtonClicked += BscanChangeBtn_ButtonClicked;
        }

        private void BscanChangeBtn_ButtonClicked(int obj)
        {
            BScanIndexChanged(obj);
        }

        public void setLayerSettings(MeasurementData.BScanLayerItem upperLayer, int upperOffset, MeasurementData.BScanLayerItem lowerLayer, int lowerOffset)
        {
            layerSelectorUpper.setLayerSettings(upperLayer, upperOffset);
            layerSelectorLower.setLayerSettings(lowerLayer, lowerOffset);
        }

        public void updateBScanImage(int index)
        {
            String imagePath = MeasurementData.Ins.Self.ExamInfo.DataDir + "/" + index.ToString("000") + ".jpg";
            itemImage.Source = new BitmapImage(new Uri(imagePath));
        }

        private void notifyChanges()
        {
            MeasurementData.BScanLayerItem upperLayer = MeasurementData.kLayerILM;
            MeasurementData.BScanLayerItem lowerLayer = MeasurementData.kLayerILM;
            int upperOffset = 0;
            int lowerOffset = 0;

            if (layerSelectorUpper.layerSelector.comboBox.SelectedItem != null)
            {
                upperLayer = (layerSelectorUpper.layerSelector.comboBox.SelectedItem as ComboLayerSelector.ComboItemAngioLayer).LayerItem;
                upperOffset = Int32.Parse(layerSelectorUpper.layerOffset.Text);
            }

            if (layerSelectorLower.layerSelector.comboBox.SelectedItem != null)
            {
                lowerLayer = (layerSelectorLower.layerSelector.comboBox.SelectedItem as ComboLayerSelector.ComboItemAngioLayer).LayerItem;
                lowerOffset = Int32.Parse(layerSelectorLower.layerOffset.Text);
            }

            LayerSettingsChanged(upperLayer, upperOffset, lowerLayer, lowerOffset);
        }

        private void LayerSelectorLower_ItemSettingChanged(MeasurementData.BScanLayerItem layerItem, int offset)
        {
            notifyChanges();
        }

        private void LayerSelectorUpper_ItemSettingChanged(MeasurementData.BScanLayerItem layerItem, int offset)
        {
            notifyChanges();
        }
    }
}
