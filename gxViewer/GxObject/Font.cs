using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Font
    {
        private string _Name;
        private int _Size;
        private string _Underline;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public int Size
        {
            get { return _Size; }
            set { _Size = value; }
        }

        public string Underline
        {
            get { return _Underline; }
            set { _Underline = value; }
        }

    }
}
