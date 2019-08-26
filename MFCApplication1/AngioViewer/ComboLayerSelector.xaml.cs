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
    /// Interaction logic for ComboLayerSelector.xaml
    /// </summary>
    public partial class ComboLayerSelector : UserControl
    {
        public event Action<MeasurementData.BScanLayerItem> ItemSelected;

        public ComboLayerSelector()
        {
            InitializeComponent();

            //DataContext = this.comboBox.SelectedItem as ComboItemAngioLayer;
        }

        public void composeItemList()
        {
            comboBox.Items.Clear();
            foreach (var layerItem in MeasurementData.Ins.kBScanLayers)
            {
                ComboItemAngioLayer comboBoxItem = new ComboItemAngioLayer(layerItem);
                comboBox.Items.Add(comboBoxItem);
                comboBox.SelectionChanged += ComboBox_SelectionChanged;
            }
        }

        public void selectLayer(MeasurementData.BScanLayerItem item)
        {
            // find target combobox item
            foreach (var comboItem in comboBox.Items)
            {
                var comboBoxAngioLayerItem = comboItem as ComboItemAngioLayer;
                if (comboBoxAngioLayerItem.LayerItem.Name == item.Name)
                {
                    comboBox.SelectedItem = comboBoxAngioLayerItem;
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = comboBox.SelectedItem as ComboItemAngioLayer;

            // update ui
            DataContext = selectedItem;

            // data
            ItemSelected(selectedItem.LayerItem);

            // stop propagation
            e.Handled = true;
        }

        // inner class
        public class ComboItemAngioLayer : ComboBoxItem
        {
            public ComboItemAngioLayer(MeasurementData.BScanLayerItem item)
            {
                LayerItem = item;

                DataContext = this;
            }

            public String LayerColor {
                get
                {
                    return ColorTranslator.ToHtml(LayerItem.Color);
                }
            }
            public String LayerName {
                get
                {
                    return LayerItem.Name;
                }
            }

            public MeasurementData.BScanLayerItem LayerItem { get; set; }
        }
    }
}
