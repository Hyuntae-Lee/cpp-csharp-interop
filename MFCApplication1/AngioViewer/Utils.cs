using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace AngioViewer
{
    class Utils
    {
        private const UInt16 kDepthCodeValueMax = 40;
        private const UInt16 kDepthCodeValueMin = 10;

        public static void LoadJsonTo(String dataDir, String fileName, MeasurementData.Item measurementData)
        {
            String filePath = dataDir + "/" + fileName;

            // read file
            var contents = File.ReadAllText(filePath);
            dynamic array = JsonConvert.DeserializeObject(contents);

            // angiography list
            var arrAngiography = array["angiography_list"];
            List<MeasurementData.AngiographyItem> angiographyList = new List<MeasurementData.AngiographyItem>();
            foreach (var angiography in arrAngiography)
            {
                var newAngiography = MeasurementData.AngiographyItem.fromJson(angiography, dataDir);
                angiographyList.Add(newAngiography);
            }
            measurementData.AngiographyItemList.Clear();
            measurementData.AngiographyItemList.AddRange(angiographyList);
            // exam info
            measurementData.ExamInfo = MeasurementData.ExamInfo.fromJson(array["exam_info"]);
            // patient info
            measurementData.PatientData = MeasurementData.PatientData.fromJson(array["patient_info"]);
        }

        public static Color getDepthCodeColor(UInt16 value)
        {
            if (value < 10)
            {
                return Color.Black;
            }
            else if (value < 20)
            {
                return Color.FromKnownColor(KnownColor.Green);
            }
            else if (value < 30)
            {
                return Color.FromKnownColor(KnownColor.Yellow);
            }
            else if (value < 30)
            {
                return Color.FromKnownColor(KnownColor.Blue);
            }
            else if (value < 40)
            {
                return Color.FromKnownColor(KnownColor.Red);
            }
            else
            {
                return Color.White;
            }

            //if (value < kDepthCodeValueMin)
            //{
            //    return Color.Black;
            //}

            //if (value > kDepthCodeValueMax)
            //{
            //    value = kDepthCodeValueMax;
            //}

            //double hueValue = kDepthCodeValueMax - value;

            //return HSVtoRGB(hueValue / (double)kDepthCodeValueMax * 240.0, 1.0, 1.0);
        }

        private static Color HSVtoRGB(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private static double mix(double a, double b, double v)
        {
            return (1 - v) * a + v * b;
        }
    }
}
