using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class FVAttri
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _ExtName;

        public string ExtName
        {
            get { return _ExtName; }
            set { _ExtName = value; }
        }
	
	
        
    }
}
