﻿using System;
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
    /// Interaction logic for PatientInfo.xaml
    /// </summary>
    public partial class PatientInfo : UserControl
    {
        public PatientInfo()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public void refreshData()
        {
            PatientInfoString = MeasurementData.Ins.Self.PatientData.toString();
        }

        public String PatientInfoString { set; get; }
    }
}
