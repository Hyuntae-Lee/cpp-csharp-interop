using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AngioViewer
{
    class Utils
    {
        public static void LoadJson(String filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var item in array)
                {
                    Console.WriteLine("{0} {1}", item.temp, item.vcc);
                }
            }
        }
    }
}
