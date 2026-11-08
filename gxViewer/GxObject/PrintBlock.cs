using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class PrintBlock
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

        private string _Brush;
        public string Brush
        {
            get { return _Brush; }
            set { _Brush = value; }
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

        private object _WebProperties;
        public object WebProperties
        {
            get { return _WebProperties; }
            set { _WebProperties = value; }
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

        private string _ForeColor;
        public string ForeColor
        {
            get { return _ForeColor; }
            set { _ForeColor = value; }
        }

        private string _BackColor;
        public string BackColor
        {
            get { return _BackColor; }
            set { _BackColor = value; }
        }

        private string _Shape;
        public string Shape
        {
            get { return _Shape; }
            set { _Shape = value; }
        }

        private string _OutStyle;
        public string OutStyle
        {
            get { return _OutStyle; }
            set { _OutStyle = value; }
        }

        private int _GSGridWidth;
        public int GSGridWidth
        {
            get { return _GSGridWidth; }
            set { _GSGridWidth = value; }
        }

        private int _GSGridHeight;
        public int GSGridHeight
        {
            get { return _GSGridHeight; }
            set { _GSGridHeight = value; }
        }

        private int _DataLeft;
        public int DataLeft
        {
            get { return _DataLeft; }
            set { _DataLeft = value; }
        }

        private int _DataTop;
        public int DataTop
        {
            get { return _DataTop; }
            set { _DataTop = value; }
        }

        private int _DataRight;
        public int DataRight
        {
            get { return _DataRight; }
            set { _DataRight = value; }
        }

        private int _DataBottom;
        public int DataBottom
        {
            get { return _DataBottom; }
            set { _DataBottom = value; }
        }

        private string _CommandLine;
        public string CommandLine
        {
            get { return _CommandLine; }
            set { _CommandLine = value; }
        }

        private int _PBPos;
        public int PBPos
        {
            get { return _PBPos; }
            set { _PBPos = value; }
        }


        public string TextMode { get; set; }






    }
}
