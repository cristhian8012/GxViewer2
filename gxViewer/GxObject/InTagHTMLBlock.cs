using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace gxViewer.GxObject
{
    public class InTag_HTMLBlock
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

        [XmlElement(ElementName = "HTMLText")]
        public List<HTMLText> HTMLText { get; set; }

        [XmlElement(ElementName = "HTMLAttriValue")]
        public List<HTMLAttriValue> HTMLAttriValue { get; set; }
    }
}
