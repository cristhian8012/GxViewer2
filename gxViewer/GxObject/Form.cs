using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Form
    {
        private string _NextControlID;
        public string NextControlID
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

        private Block _Block;
        public Block Block
        {
            get { return _Block; }
            set { _Block = value; }
        }

        public string Type { get; set; }



    }
}
