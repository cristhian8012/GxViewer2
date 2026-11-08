using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Folder
    {
        private ObjInfo _ObjInfo;
        private Info _Info;
        private string _LastUpdate;

        public Info Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public ObjInfo ObjInfo
        {
            get { return _ObjInfo; }
            set { _ObjInfo = value; }
        }

        public string LastUpdate
        {
            get { return _LastUpdate; }
            set { _LastUpdate = value; }
        }

    }
}
