using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class DataSource
    {
        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private Property[] _Propertys;

        public Property[] Propertys
        {
            get { return _Propertys; }
            set { _Propertys = value; }
        }

	
	
    }
}
