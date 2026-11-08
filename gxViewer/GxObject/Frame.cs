using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Frame
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

        private string _LBColor;
        public string LBColor
        {
            get { return _LBColor; }
            set { _LBColor = value; }
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

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
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


        public string TextMode { get; set; }










    }
}
