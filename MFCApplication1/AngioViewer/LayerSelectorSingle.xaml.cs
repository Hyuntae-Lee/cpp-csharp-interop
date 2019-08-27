using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for LayerSelectorSingle.xaml
    /// </summary>
    public partial class LayerSelectorSingle : UserControl
    {
        public event Action<MeasurementData.BScanLayerItem, int> ItemSettingChanged;

        public LayerSelectorSingle()
        {
            InitializeComponent();

            layerSelector.composeItemList();
            layerSelector.ItemSelected += LayerSelector_ItemSelected;
        }

        public void setLayerSettings(MeasurementData.BScanLayerItem layer, int offset)
        {
            layerSelector.selectLayer(layer);
            layerOffset.Text = offset.ToString();
        }

        private void LayerSelector_ItemSelected(MeasurementData.BScanLayerItem obj)
        {
            ItemSettingChanged(obj, 0);
        }
    }
}
