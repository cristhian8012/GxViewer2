using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using mshtml;

namespace gxViewer.GxObject
{
    [Serializable]
    public class StructureDataType : GXObj
    {
        
        private SDTXml _SDTXml;
        public SDTXml SDTXml
        {
            get { return _SDTXml; }
            set
            {
                _SDTXml = value;
            }
        }

        public override string Tipo
        {
            get
            {
                return "StructureDataType";
            }
        }
    }
}
