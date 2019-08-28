using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace AngioViewer
{
    public sealed class OctDBAccessor
    {
        private static volatile OctDBAccessor instance;
        private static object syncRoot = new Object();

        private SQLiteConnection Connection;

        private String m_dbFilePath;

        public String DBFilePath
        {
            get
            {
                return m_dbFilePath;
            }
            set
            {
                m_dbFilePath = value;
                Connection.ConnectionString = String.Format("Data Source={0};Version=3;", m_dbFilePath); ;
            }
        }

        private OctDBAccessor()
        {
            Connection = new SQLiteConnection();
        }

        public static OctDBAccessor Ins
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new OctDBAccessor();
                    }
                }

                return instance;
            }
        }

        public String getOtherSide(String dataDir, MeasurementData.EyeSide side)
        {
            if (Connection == null)
            {
                return null;
            }

            int otherSide = side == MeasurementData.EyeSide.OD ? 2 : 1;

            // self
            var pathPair = parseDataDir(dataDir);
            var examPath = pathPair.Item1;
            var scanItemPath = pathPair.Item2;
            var examId = getExamIdByFilePath(examPath);
            var scanInfo = getScanInfoByFilePath(examId, scanItemPath);
            var candidateList = getScanItemListByData(scanInfo.examId, scanInfo.ascans,
                scanInfo.bscans, scanInfo.scan_w, scanInfo.scan_h, scanInfo.scan_direction,
                scanInfo.patternIdx, otherSide);

            if (candidateList.Count <= 0)
            {
                return null;
            }

            // other
            return Path.Combine(examPath, candidateList.ElementAt(0).file_path);
        }

        private List<ScanInfo> getScanItemListByData(int examId, int ascans, int bscans, double scan_w, double scan_h, int scan_direction, int patternIdx, int eye_side)
        {
            Connection.Open();

            String sql = String.Format("select * from ScanResultTbl where Exam_ID={0} and A_scans={1} and B_Scans={2} and Scan_Width={3} and Scan_Height={4} and Scan_Direction={5} and Pattern_ID={6} and Eye_Side={7}",
                examId, ascans, bscans, scan_w, scan_h, scan_direction, patternIdx, eye_side);

            SQLiteCommand cmd = new SQLiteCommand(sql, Connection);
            SQLiteDataReader rdr = cmd.ExecuteReader();

            List<ScanInfo> itemList = new List<ScanInfo>();
            while (rdr.Read())
            {
                ScanInfo item = new ScanInfo();

                item.examId = examId;
                item.patternIdx = rdr.GetInt32(rdr.GetOrdinal("Pattern_ID"));
                item.ascans = (int)rdr.GetDouble(rdr.GetOrdinal("A_scans"));
                item.bscans = (int)rdr.GetDouble(rdr.GetOrdinal("B_Scans"));
                item.scan_w = rdr.GetDouble(rdr.GetOrdinal("Scan_Width"));
                item.scan_h = rdr.GetDouble(rdr.GetOrdinal("Scan_Height"));
                item.scan_direction = rdr.GetInt32(rdr.GetOrdinal("Scan_Direction"));
                item.eye_side = rdr.GetInt32(rdr.GetOrdinal("Eye_Side"));
                item.meas_time = rdr.GetDateTime(rdr.GetOrdinal("Measure_Time"));
                item.file_path = rdr["File_Path"] as String;

                itemList.Add(item);
            }

            rdr.Close();

            Connection.Close();

            return itemList;
        }

        private ScanInfo getScanInfoByFilePath(int examId, string filePath)
        {
            Connection.Open();

            String sql = String.Format("select * from ScanResultTbl where File_Path=\'{0}\'", filePath);

            SQLiteCommand cmd = new SQLiteCommand(sql, Connection);
            SQLiteDataReader rdr = cmd.ExecuteReader();

            ScanInfo scanInfo = new ScanInfo();
            scanInfo.examId = -1;
            while (rdr.Read())
            {
                scanInfo.examId = examId;
                scanInfo.patternIdx = rdr.GetInt32(rdr.GetOrdinal("Pattern_ID"));
                scanInfo.ascans = (int)rdr.GetDouble(rdr.GetOrdinal("A_scans"));
                scanInfo.bscans = (int)rdr.GetDouble(rdr.GetOrdinal("B_Scans"));
                scanInfo.scan_w = rdr.GetDouble(rdr.GetOrdinal("Scan_Width"));
                scanInfo.scan_h = rdr.GetDouble(rdr.GetOrdinal("Scan_Height"));
                scanInfo.scan_direction = rdr.GetInt32(rdr.GetOrdinal("Scan_Direction"));
                scanInfo.eye_side = rdr.GetInt32(rdr.GetOrdinal("Eye_Side"));
                scanInfo.meas_time = rdr.GetDateTime(rdr.GetOrdinal("Measure_Time"));
                scanInfo.file_path = rdr["File_Path"] as String;
                break;
            }

            rdr.Close();

            Connection.Close();

            return scanInfo;
        }

        private int getExamIdByFilePath(String filePath)
        {
            Connection.Open();

            String sql = String.Format("select * from ExamTbl where File_Path=\'{0}\'", filePath);

            SQLiteCommand cmd = new SQLiteCommand(sql, Connection);
            SQLiteDataReader rdr = cmd.ExecuteReader();

            int examId = -1;
            while (rdr.Read())
            {
                examId = rdr.GetInt32(rdr.GetOrdinal("Serial_ID"));
                break;
            }

            rdr.Close();
            
            Connection.Close();

            return examId;
        }

        private Tuple<String, String> parseDataDir(String dataDir)
        {
            var dirName = Path.GetDirectoryName(dataDir);
            var fileName = Path.GetFileNameWithoutExtension(dataDir);

            return new Tuple<string, string>(dirName, fileName);
        }

        public struct ScanInfo
        {
            public int examId;
            public int patternIdx;
            public int ascans;
            public int bscans;
            public double scan_w;
            public double scan_h;
            public int scan_direction;
            public int eye_side;
            public DateTime meas_time;
            public String file_path;
        }
    }
}
