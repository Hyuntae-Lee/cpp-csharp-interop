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
        public static void LoadJsonTo(String filePath, MeasurementData measurementData)
        {
            // read file
            var contents = File.ReadAllText(filePath);
            dynamic array = JsonConvert.DeserializeObject(contents);

            // angiography list
            var arrAngioGraphy = array["angiography_list"];
            List<MeasurementData.AngiographyItem> angiographyList = new List<MeasurementData.AngiographyItem>();
            foreach (var item in arrAngioGraphy)
            {
                var newItem = MeasurementData.AngiographyItem.fromJson(item);
                angiographyList.Add(newItem);
            }
            measurementData.CurAngiographyItemList.Clear();
            measurementData.CurAngiographyItemList.AddRange(angiographyList);
            // exam info
            measurementData.CurExamInfo = MeasurementData.ExamInfo.fromJson(array["exam_info"]);
            // patient info
            measurementData.CurPatientData = MeasurementData.PatientData.fromJson(array["patient_info"]);
        }
    }
}
