using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class ColorInfo
    {
        private string _Foreground;
        private string _Default;

        public string Foreground
        {
            get { return _Foreground; }
            set { _Foreground = value; }
        }

        public string Default
        {
            get { return _Default; }
            set { _Default = value; }
        }

        public string Background { get; set; }
    }
}
