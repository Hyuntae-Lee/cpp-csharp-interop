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
    /// Interaction logic for SinglePage.xaml
    /// </summary>
    public partial class SinglePage
    {
        public SinglePage()
        {
            InitializeComponent();

            //
            this.DataContext = this;

            //
            angiography_0.MouseLeftButtonDown += Angiography_MouseLeftButtonDown;
            angiography_1.MouseLeftButtonDown += Angiography_MouseLeftButtonDown;
            angiography_2.MouseLeftButtonDown += Angiography_MouseLeftButtonDown;
            angiography_3.MouseLeftButtonDown += Angiography_MouseLeftButtonDown;
            angiography_4.MouseLeftButtonDown += Angiography_MouseLeftButtonDown;
        }

        private void selectAngiographyItem(MeasurementData.AngiographyItem item)
        {
            // data map
            dataMap_0.setItemList(item.DataMapList, 0, DataDir);

            // bscan
        }

        private void Angiography_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedViewer = sender as AngioGraphyViewer;

            // hightlight item
            angiography_0.BorderBrush = new SolidColorBrush(Colors.Transparent);
            angiography_1.BorderBrush = new SolidColorBrush(Colors.Transparent);
            angiography_2.BorderBrush = new SolidColorBrush(Colors.Transparent);
            angiography_3.BorderBrush = new SolidColorBrush(Colors.Transparent);
            angiography_4.BorderBrush = new SolidColorBrush(Colors.Transparent);
            selectedViewer.BorderBrush = new SolidColorBrush(Colors.Red);

            //
            var item = selectedViewer.getCurrentAngiographyItem();
            selectAngiographyItem(item);
        }

        public void updateData(MeasurementData.ExamInfo examInfo, List<MeasurementData.AngiographyItem> angiographyList)
        {
            //
            pageTitle.ExamDescription = examInfo.toString();

            //
            DataDir = examInfo.DataDir;

            //
            angiography_0.setItemList(angiographyList, 0, DataDir);
            angiography_1.setItemList(angiographyList, 1, DataDir);
            angiography_2.setItemList(angiographyList, 2, DataDir);
            angiography_3.setItemList(angiographyList, 3, DataDir);
            angiography_4.setItemList(angiographyList, 4, DataDir);
        }

        private String DataDir { get; set; }
    }
}
