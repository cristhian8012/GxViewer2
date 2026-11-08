using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace gxViewer.GxObject
{
    public class HTMLAttriValue: HtmlTagForm
    {

        [XmlElement(ElementName = "Attribute")]
        public string Attribute { get; set; }

        [XmlElement(ElementName = "Value")]
        public string Value { get; set; }

        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }
    }
}
