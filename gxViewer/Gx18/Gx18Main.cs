using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using gxViewer.GxObject;
using System.Windows.Forms;
namespace gxViewer.Gx18
{

    [XmlRoot(ElementName = "ExportFile")]
    public class ExportFile
    {
        [XmlElement(ElementName = "KMW")]
        public KMW KMW { get; set; }
        [XmlElement(ElementName = "Source")]
        public Source Source { get; set; }
        [XmlElement(ElementName = "Objects")]
        public Objects Objects { get; set; }
        [XmlElement(ElementName = "Attributes")]
        public Attributes Attributes { get; set; }
        [XmlElement(ElementName = "Dependencies")]
        public Dependencies Dependencies { get; set; }
        [XmlElement(ElementName = "ObjectsIdentityMapping")]
        public ObjectsIdentityMapping ObjectsIdentityMapping { get; set; }
    }


    [XmlRoot(ElementName = "Version")]
    public class Version
    {
        [XmlAttribute(AttributeName = "guid")]
        public string Guid { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "Source")]
    public class Source
    {
        [XmlElement(ElementName = "Version")]
        public Version Version { get; set; }
        [XmlAttribute(AttributeName = "kb")]
        public string Kb { get; set; }
        [XmlAttribute(AttributeName = "username")]
        public string Username { get; set; }
        [XmlAttribute(AttributeName = "UNCPath")]
        public string UNCPath { get; set; }
    }

    [XmlRoot(ElementName = "Attributes")]
    public class Attributes
    {
        [XmlElement(ElementName = "Attribute")]
        public List<Attribute> Attribute { get; set; }
    }

    [XmlRoot(ElementName = "Dependencies")]
    public class Dependencies
    {
        [XmlElement(ElementName = "Reference")]
        public List<Reference> Reference { get; set; }
    }

    [XmlRoot(ElementName = "Reference")]
    public class Reference
    {
        [XmlElement(ElementName = "Properties")]
        public Properties Properties { get; set; }
        [XmlAttribute(AttributeName = "Package")]
        public string Package { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "ObjectIdentity")]
    public class ObjectIdentity
    {
        [XmlElement(ElementName = "Guid")]
        public string Guid { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "parent")]
        public string Parent { get; set; }
    }

    [XmlRoot(ElementName = "ObjectsIdentityMapping")]
    public class ObjectsIdentityMapping
    {
        [XmlElement(ElementName = "ObjectIdentity")]
        public List<ObjectIdentity> ObjectIdentity { get; set; }
    }


    [XmlRoot(ElementName = "Attribute")]
    //public class Attribute
    //{
    //    [XmlAttribute(AttributeName = "key")]
    //    public string Key { get; set; }
    //    [XmlAttribute(AttributeName = "guid")]
    //    public string Guid { get; set; }
    //    [XmlText]
    //    public string Text { get; set; }

    //    public List<Part> Part { get; set; }

    //    public Properties Properties { get; set; }
    //}

    public class Attribute
    {
        [XmlAttribute("parentGuid")]
        public string ParentGuid { get; set; }

        [XmlAttribute("user")]
        public string User { get; set; }

        [XmlAttribute("versionDate")]
        public DateTime VersionDate { get; set; }

        [XmlAttribute("lastUpdate")]
        public DateTime LastUpdate { get; set; }

        [XmlAttribute("checksum")]
        public string Checksum { get; set; }

        [XmlAttribute("fullyQualifiedName")]
        public string FullyQualifiedName { get; set; }

        [XmlAttribute("moduleGuid")]
        public string ModuleGuid { get; set; }

        [XmlAttribute("guid")]
        public string Guid { get; set; }

        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("description")]
        public string Description { get; set; }

        [XmlElement("Part")]
        public List<Part> Parts { get; set; }

        [XmlElement("Properties")]
        public Properties Properties { get; set; }
    }


    [XmlRoot(ElementName = "Level")]
    public class Level
    {
        [XmlElement(ElementName = "Properties")]
        public string Properties { get; set; }
        [XmlElement(ElementName = "Attribute")]
        public List<Attribute> Attribute { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "Description")]
        public string Description { get; set; }
        [XmlAttribute(AttributeName = "Guid")]
        public string Guid { get; set; }
    }

    [XmlRoot(ElementName = "Property")]
    public class Property
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Value")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "Properties")]
    public class Properties
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "PackageName")]
        public string PackageName { get; set; }

        [XmlElement(ElementName = "Property")]
        public List<Property> Property { get; set; }
    }

    [XmlRoot(ElementName = "Part")]
    public class Part
    {
        [XmlElement(ElementName = "Level")]
        public Level Level { get; set; }
        [XmlElement(ElementName = "Properties")]
        public Properties Properties { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "Source")]
        public string Source { get; set; }
        [XmlElement(ElementName = "Document")]
        public Document Document { get; set; }
        [XmlElement(ElementName = "Help")]
        public Help Help { get; set; }
        [XmlElement(ElementName = "InnerHtml")]
        public string InnerHtml { get; set; }
        [XmlElement(ElementName = "Key")]
        public Key Key { get; set; }
        [XmlElement(ElementName = "Indexes")]
        public Indexes Indexes { get; set; }
        [XmlElement(ElementName = "Variable")]
        public List<Variable> Variable { get; set; }
        [XmlElement(ElementName = "Layout")]
        public Layout Layout { get; set; }
    }

    [XmlRoot(ElementName = "Info")]
    public class Info
    {
        [XmlElement(ElementName = "Properties")]
        public Properties Properties { get; set; }
    }

    [XmlRoot(ElementName = "DataEntry")]
    public class DataEntry
    {
        [XmlElement(ElementName = "Properties")]
        public Properties Properties { get; set; }
    }

    [XmlRoot(ElementName = "Label")]
    public class Label
    {
        [XmlElement(ElementName = "Properties")]
        public Properties Properties { get; set; }
    }

    [XmlRoot(ElementName = "Grid")]
    public class Grid
    {
        [XmlElement(ElementName = "Elements")]
        public ElementsGrid Elements { get; set; }

        [XmlElement("Properties")]
        public Properties Properties { get; set; }
    }

    [XmlRoot(ElementName = "GridColumn")]
    public class GridColumn
    {
        [XmlElement("Properties")]
        public PropertiesColumns Properties { get; set; }
    }


    [XmlRoot(ElementName = "Properties")]
    public class PropertiesColumns
    {
        [XmlElement(ElementName = "Property")]
        public List<PropertyColum> Property { get; set; }
    }

    public class PropertyColum
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Value")]
        public string Value { get; set; }
    }



    [XmlRoot(ElementName = "Elements")]
    public class Elements
    {
        [XmlElement(ElementName = "DataEntry")]
        public List<DataEntry> DataEntry { get; set; }
        [XmlElement(ElementName = "Label")]
        public List<Label> Label { get; set; }


        [XmlElement(ElementName = "Grid")]
        public List<Grid> Grid { get; set; }
       

    }

    public class ElementsGrid
    {
        [XmlElement("GridColumn")]
        public List<GridColumn> GridColumn { get; set; }

    }


    [XmlRoot(ElementName = "Canvas")]
    public class Canvas
    {
        [XmlElement(ElementName = "Elements")]
        public Elements Elements { get; set; }
        [XmlElement(ElementName = "Properties")]
        public Properties Properties { get; set; }
    }

    [XmlRoot(ElementName = "Form")]
    public class Form
    {
        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "Class")]
        public string Class { get; set; }
        [XmlElement(ElementName = "ClassGuid")]
        public string ClassGuid { get; set; }
        [XmlElement(ElementName = "SizeUnit")]
        public string SizeUnit { get; set; }
        [XmlElement(ElementName = "Info")]
        public Info Info { get; set; }
        [XmlElement(ElementName = "Canvas")]
        public Canvas Canvas { get; set; }
    }

    [XmlRoot(ElementName = "Document")]
    public class Document
    {
        [XmlElement(ElementName = "Form")]
        public Form Form { get; set; }
    }

    [XmlRoot(ElementName = "HelpItem")]
    public class HelpItem
    {
        [XmlElement(ElementName = "Language")]
        public string Language { get; set; }
        [XmlElement(ElementName = "Content")]
        public string Content { get; set; }
    }

    [XmlRoot(ElementName = "Help")]
    public class Help
    {
        [XmlElement(ElementName = "HelpItem")]
        public List<HelpItem> HelpItem { get; set; }
    }

    [XmlRoot(ElementName = "Object")]
    public class Object
    {
        [XmlElement(ElementName = "Part")]
        public List<Part> Part { get; set; }
        [XmlElement(ElementName = "Properties")]
        public Properties Properties { get; set; }
        [XmlAttribute(AttributeName = "parentGuid")]
        public string ParentGuid { get; set; }
        [XmlAttribute(AttributeName = "user")]
        public string User { get; set; }
        [XmlAttribute(AttributeName = "versionDate")]
        public string VersionDate { get; set; }
        [XmlAttribute(AttributeName = "lastUpdate")]
        public string LastUpdate { get; set; }
        [XmlAttribute(AttributeName = "checksum")]
        public string Checksum { get; set; }
        [XmlAttribute(AttributeName = "fullyQualifiedName")]
        public string FullyQualifiedName { get; set; }
        [XmlAttribute(AttributeName = "moduleGuid")]
        public string ModuleGuid { get; set; }
        [XmlAttribute(AttributeName = "guid")]
        public string Guid { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }
        [XmlAttribute(AttributeName = "parent")]
        public string Parent { get; set; }
        [XmlAttribute(AttributeName = "parentType")]
        public string ParentType { get; set; }
    }

    [XmlRoot(ElementName = "Item")]
    public class Item
    {
        [XmlAttribute(AttributeName = "guid")]
        public string Guid { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Key")]
    public class Key
    {
        [XmlElement(ElementName = "Item")]
        public List<Item> Item { get; set; }
    }

    [XmlRoot(ElementName = "Member")]
    public class Member
    {
        [XmlAttribute(AttributeName = "Order")]
        public string Order { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Members")]
    public class Members
    {
        [XmlElement(ElementName = "Member")]
        public List<Member> Member { get; set; }
    }

    [XmlRoot(ElementName = "Index")]
    public class Index
    {
        [XmlElement(ElementName = "Part")]
        public Part Part { get; set; }
        [XmlElement(ElementName = "Properties")]
        public Properties Properties { get; set; }
        [XmlElement(ElementName = "Type")]
        public List<string> Type { get; set; }
        [XmlAttribute(AttributeName = "Source")]
        public string Source { get; set; }
        [XmlAttribute(AttributeName = "parentGuid")]
        public string ParentGuid { get; set; }
        [XmlAttribute(AttributeName = "user")]
        public string User { get; set; }
        [XmlAttribute(AttributeName = "versionDate")]
        public string VersionDate { get; set; }
        [XmlAttribute(AttributeName = "lastUpdate")]
        public string LastUpdate { get; set; }
        [XmlAttribute(AttributeName = "checksum")]
        public string Checksum { get; set; }
        [XmlAttribute(AttributeName = "fullyQualifiedName")]
        public string FullyQualifiedName { get; set; }
        [XmlAttribute(AttributeName = "moduleGuid")]
        public string ModuleGuid { get; set; }
        [XmlAttribute(AttributeName = "guid")]
        public string Guid { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }
    }

    [XmlRoot(ElementName = "TableIndex")]
    public class TableIndex
    {
        [XmlElement(ElementName = "Index")]
        public Index Index { get; set; }
    }

    [XmlRoot(ElementName = "Indexes")]
    public class Indexes
    {
        [XmlElement(ElementName = "TableIndex")]
        public List<TableIndex> TableIndex { get; set; }
    }

    [XmlRoot(ElementName = "Variable")]
    public class Variable
    {
        [XmlElement(ElementName = "Documentation")]
        public string Documentation { get; set; }
        [XmlElement(ElementName = "Properties")]
        public Properties Properties { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "PrintBlock")]
    public class PrintBlock
    {
        [XmlElement("Controls")]
        public Controls Controls { get; set; }

        [XmlElement("Properties")]
        public Properties Properties { get; set; }
    }

    [XmlRoot(ElementName = "Controls")]
    public class Controls
    {
        [XmlElement("ReportAttribute")]
        public List<ReportAttribute> ReportAttributes { get; set; }

        [XmlElement("ReportLabel")]
        public List<ReportLabel> ReportLabels { get; set; }
    }

    public class ReportAttribute
    {
        [XmlElement("Properties")]
        public Properties Properties { get; set; }
    }

    public class ReportLabel
    {
        [XmlElement("Properties")]
        public Properties Properties { get; set; }
    }


    [XmlRoot(ElementName = "Bands")]
    public class Bands
    {
        [XmlElement(ElementName = "PrintBlock")]
        public List<PrintBlock> PrintBlock { get; set; }
    }

    [XmlRoot(ElementName = "Layout")]
    public class Layout
    {
        [XmlElement(ElementName = "Bands")]
        public Bands Bands { get; set; }
        [XmlElement(ElementName = "Properties")]
        public Properties Properties { get; set; }
    }

    [XmlRoot(ElementName = "Objects")]
    public class Objects
    {
        [XmlElement(ElementName = "Object")]
        public List<Object> Object { get; set; }
    }






}
