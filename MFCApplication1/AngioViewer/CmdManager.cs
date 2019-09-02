using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngioViewer
{
    public sealed class CmdManager
    {
        public delegate int CbReqReCalc_t(String dataDir);

        private static volatile CmdManager instance;
        private static object syncRoot = new Object();

        private CmdManager()
        {
        }

        public static CmdManager Ins
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new CmdManager();
                    }
                }

                return instance;
            }
        }

        public CbReqReCalc_t ReqReCalc { set; get; }
    }
}
