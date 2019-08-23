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
            //String smapleFilePath = "D:/HCT_DATA/20190614/000002/OCT_190614_165757_OD/" + Defs.kAngioGraphyInfoFileName;
            //var contents = File.ReadAllText(smapleFilePath);

            var contents = File.ReadAllText(filePath);


            dynamic array = JsonConvert.DeserializeObject(contents);

            // angiography list
            var arrAngioGraphy = array["angiography_list"];
            List<MeasurementData.AngiographyItem> angiographyList = new List<MeasurementData.AngiographyItem>();
            foreach (var item in arrAngioGraphy)
            {
                var newItem = parseAngiographyItem(item);
                angiographyList.Add(newItem);
            }
            measurementData.CurAngiographyItemList.Clear();
            measurementData.CurAngiographyItemList.AddRange(angiographyList);
            // exam info
            measurementData.CurExamInfo = parseExamInfo(array["exam_info"]);
            // patient info
            measurementData.CurPatientData = parsePatientData(array["patient_info"]);
        }

        static MeasurementData.PatientData parsePatientData(dynamic array)
        {
            var newItem = new MeasurementData.PatientData();

            newItem.BirthDate = array["birth_date"];
            newItem.ID = array["id"];
            newItem.Name = array["name"];

            return newItem;
        }

        static MeasurementData.ExamInfo parseExamInfo(dynamic array)
        {
            var newItem = new MeasurementData.ExamInfo();

            newItem.AScanNum = array["ascan_num"];
            newItem.BScanNum = array["bscan_num"];
            newItem.DataDir = array["data_dir"];
            newItem.ExamDate = array["exam_date"];
            newItem.ExamTime = array["exam_time"];
            newItem.Horizontal = array["horizontal"];
            newItem.Overlap = array["overlap"];
            newItem.RangeX = array["range_x"];
            newItem.RangeY = array["range_y"];
            newItem.Side = array["side"] == "OD" ? MeasurementData.EyeSide.OD : MeasurementData.EyeSide.OS;
            newItem.Ssi = array["ssi"];

            return newItem;
        }

        static MeasurementData.AngiographyItem parseAngiographyItem(dynamic array)
        {
            var newItem = new MeasurementData.AngiographyItem();

            // name
            newItem.ImageName = array["image_name"];
            newItem.Name = array["name"];
            // upper layer
            newItem.UpperLayer = MeasurementData.Ins.findBScanLayerByName(array["upper_layer"].Value);
            newItem.UpperOffset = array["upper_offset"];
            // lower layer
            newItem.LowerLayer = MeasurementData.Ins.findBScanLayerByName(array["lower_layer"].Value);
            newItem.UpperOffset = array["lower_offset"];

            // data map : TODO

            return newItem;
        }
    }
}
