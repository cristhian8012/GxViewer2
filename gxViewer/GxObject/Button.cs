using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Button
    {
        private int _left;
        public int left
        {
            get { return _left; }
            set { _left = value; }
        }

        private int _top;
        public int top
        {
            get { return _top; }
            set { _top = value; }
        }

        private int _right;
        public int right
        {
            get { return _right; }
            set { _right = value; }
        }

        private int _bottom;
        public int bottom
        {
            get { return _bottom; }
            set { _bottom = value; }
        }

        private string _CtrlName;
        public string CtrlName
        {
            get { return _CtrlName; }
            set { _CtrlName = value; }
        }

        private ColorInfo _ColorInfo;   
        public ColorInfo ColorInfo
        {
            get { return _ColorInfo; }
            set { _ColorInfo = value; }
        }

        private string _FromStyle;
        public string FromStyle
        {
            get { return _FromStyle; }
            set { _FromStyle = value; }
        }

        private string _FromDefault;
        public string FromDefault
        {
            get { return _FromDefault; }
            set { _FromDefault = value; }
        }

        private string _Event;
        public string Event
        {
            get { return _Event; }
            set { _Event = value; }
        }

        private string _Type;
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private string _Caption;
        public string Caption
        {
            get { return _Caption; }
            set { _Caption = value; }
        }      

        private string _BitmapFile;
        public string BitmapFile
        {
            get { return _BitmapFile; }
            set { _BitmapFile = value; }
        }
	
	

	

    }
}
