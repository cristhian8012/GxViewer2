using System;
using System.Collections.Generic;
using System.Text;
using Ionic.Zlib;
using Ionic.Zip;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace gxViewer
{
    public static class ZipGX
    {

        public static List<string> ExtraerArchivos(string filename)
        {
            System.IO.DirectoryInfo _TargetDir = new System.IO.DirectoryInfo(String.Format("{0}\\tempzip\\", System.Windows.Forms.Application.StartupPath));
            List<string> _ret = new List<string>();

            if (ZipFile.IsZipFile(filename))
            {
                if (_TargetDir.Exists)
                {
                    System.IO.Directory.Delete(_TargetDir.FullName, true);
                }

                ZipFile _zfile = new ZipFile(filename);
                _zfile.ExtractAll(_TargetDir.FullName, ExtractExistingFileAction.OverwriteSilently);

                _TargetDir.Refresh();
            }
            else
            {
                _ret.Add(filename);
                return _ret;
            }

            if (_TargetDir.Exists)
            {
                foreach (System.IO.FileInfo f in _TargetDir.GetFiles("*.xml"))
                {
                    _ret.Add(f.FullName);
                }
            }

            return _ret;
        }

    }
}
