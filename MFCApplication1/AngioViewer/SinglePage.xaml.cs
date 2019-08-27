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

            m_bodyNormal = new SinglePageBodyNormal();
            m_bodyDetail = new SinglePageBodyDetail();

            pageTitle.pageModeToggle.Click += PageModeToggle_Click;
        }


        public void initPage()
        {
            m_bodyNormal.initPage();
            m_bodyDetail.initPage();

            updateBody();
        }
        
        public void updateData(MeasurementData.ExamInfo examInfo, List<MeasurementData.AngiographyItem> angiographyList)
        {
            pageTitle.ExamDescription = examInfo.toString();

            m_bodyNormal.updateData(examInfo, angiographyList);
            m_bodyDetail.updateData(examInfo, angiographyList);
        }

        public void updateBody()
        {
            if (pageTitle.pageModeToggle.IsChecked == true)
            {
                body.Content = m_bodyDetail;
            }
            else
            {
                body.Content = m_bodyNormal;
            }
        }

        private void PageModeToggle_Click(object sender, RoutedEventArgs e)
        {
            updateBody();
        }

        private SinglePageBodyNormal m_bodyNormal;
        private SinglePageBodyDetail m_bodyDetail;
    }
}
