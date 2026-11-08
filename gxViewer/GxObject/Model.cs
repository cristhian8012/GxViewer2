using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Model
    {
        private string _id;
        private string _Name;
        private string _Version = "";
        private string _Type;
        private int _AttLen;
        private int _TblLen;
        private int _ObjLen;
        
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string Version
        {
            get { return _Version; }
            set { _Version = value; }
        }

        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public int AttLen
        {
            get { return _AttLen; }
            set { _AttLen = value; }
        }
        public int TblLen
        {
            get { return _TblLen; }
            set { _TblLen = value; }
        }
        public int ObjLen
        {
            get { return _ObjLen; }
            set { _ObjLen = value; }
        }

        public string NullsBehavior { get; set; }
        public string EmptyAsNullBehavior { get; set; }

    }
}
