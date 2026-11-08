using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Txt_Font
    {
        private string _bold;

        public string Bold
        {
            get { return _bold; }
            set { _bold = value; }
        }
	
    }
}
