using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace gxViewer.GxValidador
{

    [XmlRoot(ElementName = "words")]
    public class Words
    {

        [XmlElement(ElementName = "word")]
        public List<string> Word { get; set; }
    }

    [XmlRoot(ElementName = "group")]
    public class Group
    {

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "required")]
        public bool Required { get; set; }

        [XmlElement(ElementName = "words")]
        public Words Words { get; set; }

        [XmlElement(ElementName = "pattern")]
        public string Pattern { get; set; } = "";

        [XmlElement(ElementName = "pattern_min")]
        public string Pattern_Min { get; set; } = "";


        [XmlElement(ElementName = "inline")]
        public bool Inline { get; set; }

        public bool IsValid { get; set; }=false;
        public string TextContent { get; set; } = "";

    }

    [XmlRoot(ElementName = "groups")]
    public class Groups
    {

        [XmlElement(ElementName = "group")]
        public List<Group> Group { get; set; }

        public void resetValidGroups()
        {
            foreach (var g in Group)
            {
                g.IsValid = false;
                g.TextContent = "";
            }
        }

        public List<string> getAllWords()
        {
            List<string> _retList = new List<string>();
            foreach (var g in Group)
            {
                foreach (var w in g.Words.Word)
                {
                    if (!_retList.Contains(w))
                    {
                        _retList.Add(w);
                    }
                }
            }
            return _retList;
        }
    }

}
