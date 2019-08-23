using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngioViewer
{
    public sealed class MeasurementData
    {
        private static volatile MeasurementData instance;
        private static object syncRoot = new Object();

        public static readonly BScanLayerItem kLayerILM = new BScanLayerItem(BScanLayerIndex.ILM, "ILM", Color.FromArgb(234, 232,  42));
        public static readonly BScanLayerItem kLayerNFL = new BScanLayerItem(BScanLayerIndex.NFL, "NFL", Color.FromArgb( 42, 234,  62));
        public static readonly BScanLayerItem kLayerIPL = new BScanLayerItem(BScanLayerIndex.IPL, "IPL", Color.FromArgb( 42, 196, 234));
        public static readonly BScanLayerItem kLayerOPL = new BScanLayerItem(BScanLayerIndex.OPL, "OPL", Color.FromArgb(255,  49, 106));
        public static readonly BScanLayerItem kLayerIOS = new BScanLayerItem(BScanLayerIndex.IOS, "IOS", Color.FromArgb(  0, 255, 164));
        public static readonly BScanLayerItem kLayerRPE = new BScanLayerItem(BScanLayerIndex.RPE, "RPE", Color.FromArgb(255, 120,   0));
        public static readonly BScanLayerItem kLayerBRM = new BScanLayerItem(BScanLayerIndex.BRM, "BRM", Color.FromArgb(144,   0, 255));

        private MeasurementData()
        {
            CurAngiographyItemList = new List<AngiographyItem>();
        }

        public static MeasurementData Ins
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MeasurementData();
                    }
                }

                return instance;
            }
        }

        public BScanLayerItem findBScanLayerByName(String name)
        {
            foreach (var item in kBScanLayers)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }

            return kBScanLayers[0];
        }

        public String DataDir { get; set; }
        public readonly BScanLayerItem[] kBScanLayers = { kLayerILM, kLayerNFL, kLayerIPL, kLayerOPL, kLayerIOS, kLayerRPE, kLayerBRM };
        public List<AngiographyItem> CurAngiographyItemList;
        public PatientData CurPatientData { get; set; }
        public ExamInfo CurExamInfo { get; set; }

        // inner classes
        public class PatientData
        {
            public String BirthDate { get; set; }
            public String ID { get; set; }
            public String Name { get; set; }

            public String toString()
            {
                return ID + " / " + Name;
            }

            public static PatientData fromJson(dynamic array)
            {
                var newItem = new PatientData();

                newItem.BirthDate = array["birth_date"];
                newItem.ID = array["id"];
                newItem.Name = array["name"];

                return newItem;
            }
        }

        public class ExamInfo
        {
            public int AScanNum { get; set; }
            public int BScanNum { get; set; }
            public String DataDir { get; set; }
            public String ExamDate { get; set; }
            public String ExamTime { get; set; }
            public bool Horizontal { get; set; }
            public int Overlap { get; set; }
            public int RangeX { get; set; }
            public int RangeY { get; set; }
            public EyeSide Side { get; set; }
            public int Ssi { get; set; }

            public String toString()
            {
                return ExamDate + " / " + ExamTime + " / " + BScanNum + "x" + AScanNum + " / " + RangeX + "x" + RangeY + "mm / SSI: " + Ssi;
            }

            public static ExamInfo fromJson(dynamic array)
            {
                var newItem = new ExamInfo();

                newItem.AScanNum = array["ascan_num"];
                newItem.BScanNum = array["bscan_num"];
                newItem.DataDir = array["data_dir"];
                newItem.ExamDate = array["exam_date"];
                newItem.ExamTime = array["exam_time"];
                newItem.Horizontal = array["horizontal"];
                newItem.Overlap = array["overlap"];
                newItem.RangeX = array["range_x"];
                newItem.RangeY = array["range_y"];
                newItem.Side = array["side"] == "OD" ? EyeSide.OD : EyeSide.OS;
                newItem.Ssi = array["ssi"];

                return newItem;
            }
        }

        public class AngiographyItem
        {
            public AngiographyItem()
            {
                DataMapList = new List<DataMapItem>();
            }

            public String ImageName { get; set; }
            public String Name { get; set; }
            public int UpperOffset { get; set; }
            public int LowerOffset { get; set; }
            public float CenterX { get; set; }
            public float CenterY { get; set; }
            public float ChartThresh { get; set; }
            public BScanLayerItem UpperLayer { get; set; }
            public BScanLayerItem LowerLayer { get; set; }
            public List<DataMapItem> DataMapList { get; set; }
            public AngioFlows Flows { get; set; }
            public AngioDensity Density { get; set; }

            public static AngiographyItem fromJson(dynamic array)
            {
                var newItem = new AngiographyItem();

                // name
                newItem.ImageName = array["image_name"];
                newItem.Name = array["name"];
                // upper layer
                newItem.UpperLayer = MeasurementData.Ins.findBScanLayerByName(array["upper_layer"].Value);
                newItem.UpperOffset = array["upper_offset"];
                // lower layer
                newItem.LowerLayer = MeasurementData.Ins.findBScanLayerByName(array["lower_layer"].Value);
                newItem.UpperOffset = array["lower_offset"];

                // data map list
                var arrDataMap = array["data_map_list"];
                List<DataMapItem> dataMapList = new List<DataMapItem>();
                foreach (var item in arrDataMap)
                {
                    var dataMapItem = DataMapItem.fromJson(item);
                    dataMapList.Add(dataMapItem);
                }
                newItem.DataMapList.Clear();
                newItem.DataMapList.AddRange(dataMapList);

                return newItem;
            }
        }

        public enum EyeSide
        {
            OD = 0,
            OS,
        }

        public enum BScanLayerIndex
        {
            ILM = 0,
            NFL,
            IPL,
            OPL,
            IOS,
            RPE,
            BRM,
            NUM,
        }

        public class BScanLayerItem
        {
            public BScanLayerItem()
            {

            }

            public BScanLayerItem(BScanLayerIndex index, String name, Color color)
            {
                Idx = index;
                Name = name;
                Color = color;
            }

            public BScanLayerIndex Idx { get; set; }
            public String Name { get; set; }
            public Color Color { get; set; }
        }

        public class DataMapItem
        {
            public String ImageName { get; set; }
            public String Name { get; set; }

            public static DataMapItem fromJson(dynamic array)
            {
                var newItem = new DataMapItem();

                // name
                newItem.ImageName = array["image_name"];
                newItem.Name = array["name"];

                return newItem;
            }
        }

        public struct AngioFlows
        {
            int Center;
            int Total;
            int Superior;
            int Inferior;
            int Section_0;
            int Section_1;
            int Section_2;
            int Section_3;
            int Section_4;
        }

        public struct AngioDensity
        {
            int Center;
            int Total;
            int Superior;
            int Inferior;
            int Section_0;
            int Section_1;
            int Section_2;
            int Section_3;
            int Section_4;
        }
    }


}
