using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace gxViewer.GxObject
{
    public class HTMLControl : HtmlTagForm
    {
        [XmlElement(ElementName = "InTag_HTMLBlock")]
        public InTag_HTMLBlock InTag_HTMLBlock { get; set; }

        [XmlElement(ElementName = "ControlIdType")]
        public string ControlIdType { get; set; }

        [XmlElement(ElementName = "Property")]
        public List<Property> Property { get; set; }

        public List<ChildProperties> ChildProperties { get; set; }

    }
}
