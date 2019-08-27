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
    /// Interaction logic for AngioGraphyViewer.xaml
    /// </summary>
    public partial class AngioGraphyViewer : UserControl
    {
        public AngioGraphyViewer()
        {
            InitializeComponent();

            // cont
            itemSelector.ItemSelected += ItemSelector_ItemSelected;

            //
            m_angiographyItemList = new List<MeasurementData.AngiographyItem>();
            m_dataMapList = new List<MeasurementData.DataMapItem>();
        }

        public void setBScanIndex(int index, int maxValue, bool isVertical)
        {
            guideLine.IsVertical = isVertical;
            guideLine.setLineIndex(index, maxValue);
        }

        public void setItemList(List<MeasurementData.AngiographyItem> itemList, int defaultIndex, String dataPath)
        {
            if (m_angiographyItemList == null)
            {
                return;
            }

            clearItemList();

            // data path
            DataPath = dataPath;

            // item selector
            itemSelector.updateList(itemList);

            // items
            m_angiographyItemList.AddRange(itemList);

            // update control
            changeItem(defaultIndex);
        }

        public void setItemList(List<MeasurementData.DataMapItem> itemList, int defaultIndex, String dataPath)
        {
            if (m_dataMapList == null)
            {
                return;
            }

            clearItemList();

            // data path
            DataPath = dataPath;

            // item selector
            itemSelector.updateList(itemList);

            // items
            m_dataMapList.AddRange(itemList);

            // update control
            changeItem(defaultIndex);
        }

        public MeasurementData.AngiographyItem getCurrentAngiographyItem()
        {
            return m_angiographyItemList.ElementAt(itemSelector.comboBox.SelectedIndex);
        }

        private void clearItemList()
        {
            m_angiographyItemList.Clear();
            m_dataMapList.Clear();
        }

        private void changeItem(int index)
        {
            itemSelector.changeCurrentSelection(index);
        }

        private void ItemSelector_ItemSelected(int obj)
        {
            // angiography
            if (m_angiographyItemList.Count > 0)
            {
                
                var item = m_angiographyItemList.ElementAt(obj);
                var imagePath = DataPath + "/" + item.ImageName;
                itemImage.Source = new BitmapImage(new Uri(imagePath));
            }
            // data map
            else if (m_dataMapList.Count > 0)
            {
                var item = m_dataMapList.ElementAt(obj);
                var imagePath = DataPath + "/" + item.ImageName;
                itemImage.Source = new BitmapImage(new Uri(imagePath));
            }
        }

        private String DataPath { set; get; }
        private List<MeasurementData.AngiographyItem> m_angiographyItemList;
        private List<MeasurementData.DataMapItem> m_dataMapList;
    }
}
