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
        public static void open_viewer(String data_dir)
        {
            AngioViewer.MainWindow main = new AngioViewer.MainWindow();
            main.DataDirSelf = data_dir;
            main.ShowDialog();
        }
    }
}
