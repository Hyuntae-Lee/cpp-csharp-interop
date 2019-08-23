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
    /// Interaction logic for ComboAngiographySelector.xaml
    /// </summary>
    public partial class ComboAngiographySelector : UserControl
    {
        public event Action<int> ItemSelected;

        public ComboAngiographySelector()
        {
            InitializeComponent();

            comboBox.SelectionChanged += ComboBox_SelectionChanged;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var objSender = sender as ComboBox;
            ItemSelected(objSender.SelectedIndex);
        }

        public void updateList(List<MeasurementData.AngiographyItem> itemList)
        {
            comboBox.Items.Clear();
            foreach (var item in itemList)
            {
                comboBox.Items.Add(item.Name);
            }
        }

        public void updateList(List<MeasurementData.DataMapItem> itemList)
        {
            comboBox.Items.Clear();
            foreach (var item in itemList)
            {
                comboBox.Items.Add(item.Name);
            }
        }

        public void changeCurrentSelection(int index)
        {
            comboBox.SelectedIndex = index;
        }
    }
}
