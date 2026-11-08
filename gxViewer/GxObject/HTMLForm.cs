using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace gxViewer.GxObject
{
    [Serializable]
    public class HTMLForm
    {

        [XmlElement(ElementName = "ElemAtts")]
        public ElemAtts ElemAtts { get; set; }

        [XmlElement(ElementName = "HTMLBlock")]
        public HTMLBlock HTMLBlock { get; set; }
    }
}
