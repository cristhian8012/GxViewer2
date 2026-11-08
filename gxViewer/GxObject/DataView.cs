using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class DataView : GXObj
    {

        private GxObject.FVAttri[] _FVAttris ;
        public GxObject.FVAttri[] FVAttris
        {
            get { return _FVAttris; }
            set { _FVAttris = value; }
        }

        private DataSource _datasource;
        public DataSource DataSource
        {   
            get { return _datasource; }
            set { _datasource = value; }
        }

        private string _Table;
        public string Table
        {
            get { return _Table; }
            set { _Table = value; }
        }

        public override string Tipo
        {
            get
            {
                return "DataView";
            }
        }
    }
}
