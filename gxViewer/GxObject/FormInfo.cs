using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class FormInfo
    {
        private int _FormId;
        public int FormId
        {
            get { return _FormId; }
            set { _FormId = value; }
        }

        private string _FormClass;
        public string FormClass
        {
            get { return _FormClass; }
            set { _FormClass = value; }
        }

        private string _IsDefault;
        public string IsDefault
        {
            get { return _IsDefault; }
            set { _IsDefault = value; }
        }

        private string _TextGrid;
        public string TextGrid
        {
            get { return _TextGrid; }
            set { _TextGrid = value; }
        }

        private Att_Font _Att_Font;
        public Att_Font Att_Font
        {
            get { return _Att_Font; }
            set { _Att_Font = value; }
        }

        private Txt_Font _Txt_Font;
        public Txt_Font Txt_Font
        {
            get { return _Txt_Font; }
            set { _Txt_Font = value; }
        }

        private int _GridWidth;
        public int GridWidth
        {
            get { return _GridWidth; }
            set { _GridWidth = value; }
        }

        private int _GridHeight;
        public int GridHeight
        {
            get { return _GridHeight; }
            set { _GridHeight = value; }
        }

        private string _TemplateBased;
        public string TemplateBased
        {
            get { return _TemplateBased; }
            set { _TemplateBased = value; }
        }

        public string IsGraphic { get; set; }

        public int OffsetX { get; set; }

        public int OffsetY { get; set; }


    }
}
