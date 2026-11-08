using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Layout
    {
        private int _NextControlID;
        public int NextControlID
        {
            get { return _NextControlID; }
            set { _NextControlID = value; }
        }

        private FormInfo _FormInfo;
        public FormInfo FormInfo
        {
            get { return _FormInfo; }
            set { _FormInfo = value; }
        }

        private Block[] _Blocks;
        public Block[] Blocks
        {
            get { return _Blocks; }
            set { _Blocks = value; }
        }

        private Paper _Paper;
        public Paper Paper
        {
            get { return _Paper; }
            set { _Paper = value; }
        }
	
	
	


    }
}
