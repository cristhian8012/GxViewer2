using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Documentation
    {
        private string _source;

        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }
    }
}
