using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Att_Font
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _size;
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }
	
	

    }
}
