using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngioViewer
{
    public sealed class MeasurementData
    {
        private static volatile MeasurementData instance;
        private static object syncRoot = new Object();

        private MeasurementData() { }

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

        public String DataDir { get; set; }
    }
}
