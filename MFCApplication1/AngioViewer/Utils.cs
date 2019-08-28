using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Windows;

namespace AngioViewer
{
    class Utils
    {
        public static void LoadJsonTo(String filePath, MeasurementData.Item measurementData)
        {
            // read file
            var contents = File.ReadAllText(filePath);
            dynamic array = JsonConvert.DeserializeObject(contents);

            // angiography list
            var arrAngiography = array["angiography_list"];
            List<MeasurementData.AngiographyItem> angiographyList = new List<MeasurementData.AngiographyItem>();
            foreach (var angiography in arrAngiography)
            {
                var newAngiography = MeasurementData.AngiographyItem.fromJson(angiography);
                angiographyList.Add(newAngiography);
            }
            measurementData.AngiographyItemList.Clear();
            measurementData.AngiographyItemList.AddRange(angiographyList);
            // exam info
            measurementData.ExamInfo = MeasurementData.ExamInfo.fromJson(array["exam_info"]);
            // patient info
            measurementData.PatientData = MeasurementData.PatientData.fromJson(array["patient_info"]);
        }
    }
}
