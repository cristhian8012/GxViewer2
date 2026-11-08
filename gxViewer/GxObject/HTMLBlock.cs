using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace gxViewer.GxObject
{
    public class HTMLBlock
    {

        List<HtmlTagForm> _HtmlTagForm = new List<HtmlTagForm>();
        public void AddItemOrder(object pTag)
        {
            _HtmlTagForm.Add((HtmlTagForm)pTag);
        }
        public List<HtmlTagForm> getAllTags()
        {
            return _HtmlTagForm; 
        }

        [XmlElement(ElementName = "HTMLSpan")]
        public List<HTMLSpan> HTMLSpan { get; set; }

        [XmlElement(ElementName = "HTMLText")]
        public List<HTMLText> HTMLText { get; set; }

        [XmlElement(ElementName = "HTMLControl")]
        public List<HTMLControl> HTMLControl { get; set; }

        [XmlElement(ElementName = "HTMLTag")]
        public List<HTMLTag> HTMLTag { get; set; }
    }
}
