using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class SubFile
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

        private string _AutoResize;
        public string AutoResize
        {
            get { return _AutoResize; }
            set { _AutoResize = value; }
        }

        private string _HorzScroll;
        public string HorzScroll
        {
            get { return _HorzScroll; }
            set { _HorzScroll = value; }
        }

        private Item[] _Items;
        public Item[] Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        public string Pen { get; set; }
        public string TextMode { get; set; }

        public string ForeColor { get; set; }
        public string BackColor { get; set; }

        public int DataLeft { get; set; }
        public int DataRight { get; set; }
        public int DataTop { get; set; }
        public int DataBottom { get; set; }

    }
}
