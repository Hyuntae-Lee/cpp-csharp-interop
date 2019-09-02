using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace libAngioViewer
{
    public class Entry
    {
        public delegate int CbReqReCalc_t(String dataDir);

        public static void open_viewer(String data_dir, String db_file_path, CbReqReCalc_t cb_req_reCalc)
        {
            AngioViewer.MeasurementData.Ins.DataDirSelf = data_dir;
            AngioViewer.OctDBAccessor.Ins.DBFilePath = db_file_path;

            m_cb_req_reCalc_to = cb_req_reCalc;


            AngioViewer.CmdManager.Ins.ReqReCalc = req_reCalc;


            AngioViewer.MainWindow main = new AngioViewer.MainWindow();
            main.ShowDialog();
        }

        static private int req_reCalc(String dataDir)
        {
            if (m_cb_req_reCalc_to != null)
            {
                return m_cb_req_reCalc_to(dataDir);
            }

            return -1;
        }

        // mfc points
        static CbReqReCalc_t m_cb_req_reCalc_to;

        // c# points
        static AngioViewer.CmdManager.CbReqReCalc_t m_cb_req_reCalc_from;
    }
}
