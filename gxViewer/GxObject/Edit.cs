using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Edit
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
        private int _Id;
        private string _Name;
        private int _Align;
        //private EditInfo _EditInfo;
        private Font _Font;

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
            set { _WebProperties = value; }
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
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public int Align
        {
            get { return _Align; }
            set { _Align = value; }
        }

        private string _Text;
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        private string _Source;
        public string Source
        {
            get { return _Source; }
            set { _Source = value; }
        }

        public GxObject.Font Font
        {
            get { return _Font; }
            set { _Font = value; }
        }

        public string Dimension { get; set; }

        public string Row { get; set; }
        public string Col { get; set; }
        public string CtrlName { get; set; }

    }
 }
