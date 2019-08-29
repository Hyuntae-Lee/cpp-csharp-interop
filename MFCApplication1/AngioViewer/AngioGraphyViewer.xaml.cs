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
    /// Interaction logic for AngioGraphyViewer.xaml
    /// </summary>
    public partial class AngioGraphyViewer : UserControl
    {
        public event Action<MeasurementData.AngiographyItem> AngiographyItemSelectionChanged;

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

        private void ItemSelector_ItemSelected(int index)
        {
            // angiography
            if (m_angiographyItemList.Count > 0)
            {
                var item = m_angiographyItemList.ElementAt(index);
                var imagePath = DataPath + "/" + item.ImageName;
                itemImage.Source = new BitmapImage(new Uri(imagePath));

                AngiographyItemSelectionChanged(item);
            }
            // data map
            else if (m_dataMapList.Count > 0)
            {
                var item = m_dataMapList.ElementAt(index);

                if (item.U16Data != null)
                {
                    int width = item.U16Data[0].Count;
                    int height = item.U16Data.Count;

                    Bitmap bmp = new Bitmap(width, height);
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            bmp.SetPixel(x, y, Utils.getDepthCodeColor(item.U16Data[y][x]));
                        }
                    }

                    itemImage.Source = Utils.BitmapToImageSource(bmp);
                }
                else
                {
                    var imagePath = DataPath + "/" + item.ImageName;
                    itemImage.Source = new BitmapImage(new Uri(imagePath));
                }
            }
        }

        private String DataPath { set; get; }
        private List<MeasurementData.AngiographyItem> m_angiographyItemList;
        private List<MeasurementData.DataMapItem> m_dataMapList;
    }
}
