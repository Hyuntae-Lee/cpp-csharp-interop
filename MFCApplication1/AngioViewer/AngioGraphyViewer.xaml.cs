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
        }

        public void updateAngiographyItemList(List<MeasurementData.AngiographyItem> itemList, int defaultIndex, String dataPath)
        {
            if (m_angiographyItemList == null)
            {
                return;
            }

            // data path
            DataPath = dataPath;

            // item selector
            itemSelector.updateList(itemList);

            // angiography items
            m_angiographyItemList.Clear();
            m_angiographyItemList.AddRange(itemList);

            // update control
            changeItem(defaultIndex);
        }

        private void changeItem(int index)
        {
            itemSelector.changeCurrentSelection(index);
        }

        private void ItemSelector_ItemSelected(int obj)
        {
            // angiography update
            var item = m_angiographyItemList.ElementAt(obj);
            var imagePath = DataPath + "/" + item.ImageName;
            itemImage.Source = new BitmapImage(new Uri(imagePath));
        }

        private String DataPath { set; get; }
        private List<MeasurementData.AngiographyItem> m_angiographyItemList;
    }
}
