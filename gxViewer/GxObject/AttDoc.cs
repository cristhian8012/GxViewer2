using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class AttDoc
    {
        private Help _Help;

        public Help Help
        {
            get { return _Help; }
            set { _Help = value; }
        }
    }
}
