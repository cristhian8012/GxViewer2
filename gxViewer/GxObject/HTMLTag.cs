using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace gxViewer.GxObject
{
    public class HTMLTag : HtmlTagForm
    {

        [XmlElement(ElementName = "InTag_HTMLBlock")]
        public InTag_HTMLBlock InTag_HTMLBlock { get; set; }

        [XmlElement(ElementName = "InText_HTMLBlock")]
        public InText_HTMLBlock InText_HTMLBlock { get; set; }

        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
    }
}
