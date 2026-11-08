using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace gxViewer.GxObject
{
    public class ChildProperties : HtmlTagForm
    {

        [XmlElement(ElementName = "Property")]
        public List<Property> Property { get; set; }
    }
}
