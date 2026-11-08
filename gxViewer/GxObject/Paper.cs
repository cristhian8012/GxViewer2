using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Paper
    {
        private string _PaperId;
        public string PaperId
        {
            get { return _PaperId; }
            set { _PaperId = value; }
        }

        private string _SizeX;
        public string SizeX
        {
            get { return _SizeX; }
            set { _SizeX = value; }
        }

        private string _SizeY;
        public string SizeY
        {
            get { return _SizeY; }
            set { _SizeY = value; }
        }

        private string _RightIndent;
        public string RightIndent
        {
            get { return _RightIndent; }
            set { _RightIndent = value; }
        }

    }
}
