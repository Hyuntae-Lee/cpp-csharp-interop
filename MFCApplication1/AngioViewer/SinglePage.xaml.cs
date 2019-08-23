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
            this.DataContext = this;
        }

        public void updateData(MeasurementData.ExamInfo examInfo, List<MeasurementData.AngiographyItem> itemList)
        {
            //
            pageTitle.ExamDescription = examInfo.toString();

            //
            angiography_0.updateAngiographyItemList(itemList, 0, examInfo.DataDir);
            angiography_1.updateAngiographyItemList(itemList, 1, examInfo.DataDir);
            angiography_2.updateAngiographyItemList(itemList, 2, examInfo.DataDir);
            angiography_3.updateAngiographyItemList(itemList, 3, examInfo.DataDir);
            angiography_4.updateAngiographyItemList(itemList, 4, examInfo.DataDir);
            // dataMap_0 : TODO
        }
    }
}
