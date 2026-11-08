using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Info
    {
        private string _Name;
        private string _Description;
        private string _Folder;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public string Folder
        {
            get { return _Folder; }
            set { _Folder = value; }
        }

        public string DWHTrn { get; set; }
    }
}
