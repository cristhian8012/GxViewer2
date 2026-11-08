using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Index
    {
        private int _AttriId;   
        public int AttriId
        {
            get { return _AttriId; }
            set { _AttriId = value; }
        }

        private string _Name;   
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _TDefault;
        public string TDefault
        {
            get { return _TDefault; }
            set { _TDefault = value; }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private int _Width;
        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        private object _Title_ColorInfo;
        public object Title_ColorInfo
        {
            get { return _Title_ColorInfo; }
            set { _Title_ColorInfo = value; }
        }

        private object _Rows_ColorInfo;
        public object Rows_ColorInfo
        {
            get { return _Rows_ColorInfo; }
            set { _Rows_ColorInfo = value; }
        }
	
    }
}
