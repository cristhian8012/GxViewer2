using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace gxViewer.GxObject
{
    public class InText_HTMLBlock
    {

        [XmlElement(ElementName = "HTMLText")]
        public HTMLText HTMLText { get; set; }
    }
}
