using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class AttInfo
    {
        private EditInfo _EditInfo;

        public EditInfo EditInfo
        {
            get { return _EditInfo; }
            set { _EditInfo = value; }
        }
    }
}
