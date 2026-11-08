using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Group
    {
        private Subtype[] _Subtype;
        private Info _Info;

        public Info Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public Subtype[] Subtypes
        {
            get { return _Subtype; }
            set { _Subtype = value; }
        }

    }
}
