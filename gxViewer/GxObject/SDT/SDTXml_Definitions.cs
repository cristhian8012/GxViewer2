
using System.Collections.Generic;
using System.Xml.Serialization;

namespace gxViewer.GxObject.SDT
{

    [XmlRoot(ElementName = "Values")]
    public class Values
    {

        [XmlElement(ElementName = "Value")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "CModifierDefinition")]
    public class CModifierDefinition
    {

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Values")]
        public Values Values { get; set; }
    }

    [XmlRoot(ElementName = "Modifiers")]
    public class Modifiers
    {

        [XmlElement(ElementName = "CModifierDefinition")]
        public List<CModifierDefinition> CModifierDefinition { get; set; }
    }

    [XmlRoot(ElementName = "Type")]
    public class Type
    {

        [XmlAttribute(AttributeName = "obj")]
        public string Obj { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "CGxExpandPropertyDef")]
    public class CGxExpandPropertyDef
    {

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Type")]
        public Type Type { get; set; }

        [XmlElement(ElementName = "namespace")]
        public object Namespace { get; set; }

        [XmlElement(ElementName = "friendlynamespace")]
        public object Friendlynamespace { get; set; }

        [XmlElement(ElementName = "Modifiers")]
        public Modifiers Modifiers { get; set; }
    }

    [XmlRoot(ElementName = "Properties")]
    public class Properties
    {

        [XmlElement(ElementName = "CGxExpandPropertyDef")]
        public List<CGxExpandPropertyDef> CGxExpandPropertyDef { get; set; }
    }

    [XmlRoot(ElementName = "CGxExpandClassDef")]
    public class CGxExpandClassDef
    {

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Type")]
        public int Type { get; set; }

        [XmlElement(ElementName = "namespace")]
        public string Namespace { get; set; }

        [XmlElement(ElementName = "friendlynamespace")]
        public string Friendlynamespace { get; set; }

        [XmlElement(ElementName = "Modifiers")]
        public Modifiers Modifiers { get; set; }

        [XmlElement(ElementName = "Properties")]
        public Properties Properties { get; set; }
    }

    [XmlRoot(ElementName = "types")]
    public class Types
    {

        [XmlElement(ElementName = "CGxExpandClassDef")]
        public CGxExpandClassDef CGxExpandClassDef { get; set; }
    }
}