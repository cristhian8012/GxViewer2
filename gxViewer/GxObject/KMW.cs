using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class KMW
    {
        private int _MajorVersion;
        private int _MinorVersion;
        private string _Path;
        private int _MaxGxBuildSaved;

        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }

        public int MajorVersion
        {
            get { return _MajorVersion; }
            set { _MajorVersion = value; }
        }
        public int MinorVersion
        {
            get { return _MinorVersion; }
            set { _MinorVersion = value; }
        }
        public int MaxGxBuildSaved
        {
            get { return _MaxGxBuildSaved; }
            set { _MaxGxBuildSaved = value; }
        }

        public int Build
        {
            get { return _MaxGxBuildSaved; }
            set { _MaxGxBuildSaved = value; }
        }

    }
}
