using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class CodeBlock
    {
        private int _left;
        private int _top;
        private int _right;
        private int _bottom;
        private string _Brush;
        private string _TextMode;
        private ColorInfo _colorinfo;
        private object _WebProperties;
        private string _FromStyle;
        private string _FromDefault;
        private decimal _ForeColor;
        private decimal _BackColor;
        private int _BorderWidth;
        private string _Shape;
        private string _OutStyle;
        private int _GSGridWidth;
        private int _GSGridHeight;
        private int _DataLeft;
        private int _DataTop;
        private int _DataRight;
        private int _DataBottom;
        private string _Source;

        public int left
        {
            get { return _left; }
            set { _left = value; }
        }
        public int top
        {
            get { return _top; }
            set { _top = value; }
        }
        public int right
        {
            get { return _right; }
            set { _right = value; }
        }
        public int bottom
        {
            get { return _bottom; }
            set { _bottom = value; }
        }
        public string Brush
        {
            get { return _Brush; }
            set { _Brush = value; }
        }
        public string TextMode
        {
            get { return _TextMode; }
            set { _TextMode = value; }
        }
        public ColorInfo ColorInfo
        {
            get { return _colorinfo; }
            set { _colorinfo = value; }
        }
        public object WebProperties
        {
            get { return _WebProperties; }
            set { _WebProperties=value; }
        }
        public string FromStyle
        {
            get { return _FromStyle; }
            set { _FromStyle = value; }
        }
        public string FromDefault
        {
            get { return _FromDefault; }
            set { _FromDefault = value; }
        }
        public decimal ForeColor
        {
            get { return _ForeColor; }
            set { _ForeColor = value; }
        }
        public decimal BackColor
        {
            get { return _BackColor; }
            set { _BackColor = value; }
        }
        public int BorderWidth
        {
            get { return _BorderWidth; }
            set { _BorderWidth = value; }
        }
        public string Shape
        {
            get { return _Shape; }
            set { _Shape = value; }
        }
        public string OutStyle
        {
            get { return _OutStyle; }
            set { _OutStyle = value; }
        }
        public int GSGridWidth
        {
            get { return _GSGridWidth; }
            set { _GSGridWidth = value; }
        }
        public int GSGridHeight
        {
            get { return _GSGridHeight; }
            set { _GSGridHeight = value; }
        }
        public int DataLeft
        {
            get { return _DataLeft; }
            set { _DataLeft = value; }
        }
        public int DataTop
        {
            get { return _DataTop; }
            set { _DataTop = value; }
        }
        public int DataRight
        {
            get { return _DataRight; }
            set { _DataRight = value; }
        }
        public int DataBottom
        {
            get { return _DataBottom; }
            set { _DataBottom = value; }
        }
        public string Source
        {
            get { return _Source; }
            set { _Source=value; }
        }


    }
}

