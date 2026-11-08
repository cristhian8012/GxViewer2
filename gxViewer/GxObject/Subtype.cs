using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Subtype
    {
        private string _Name;
        private string _Supertype;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string Supertype
        {
            get { return _Supertype; }
            set { _Supertype = value; }
        }
    }
}
