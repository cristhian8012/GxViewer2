using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GxKBBase18
{
    [XmlRoot("Object")]
    public class GxObjectMetadata
    {
        // ===== Identidad =====

        [XmlAttribute("guid")]
        public string Guid { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("fullyQualifiedName")]
        public string FullyQualifiedName { get; set; }

        [XmlAttribute("type")]
        public string TypeGuid { get; set; }

        // ===== Descripción =====

        [XmlAttribute("description")]
        public string Description { get; set; }

        // ===== Fechas / control =====

        [XmlAttribute("versionDate")]
        public string VersionDate { get; set; }

        [XmlAttribute("lastUpdate")]
        public string LastUpdate { get; set; }

        [XmlAttribute("checksum")]
        public string Checksum { get; set; }

        // ===== Seguridad =====

        [XmlAttribute("user")]
        public string User { get; set; }

        // ===== Jerarquía =====

        [XmlAttribute("parent")]
        public string ParentName { get; set; }

        [XmlAttribute("parentGuid")]
        public string ParentGuid { get; set; }

        [XmlAttribute("parentType")]
        public string ParentTypeGuid { get; set; }

        // ===== Módulo =====

        [XmlAttribute("moduleGuid")]
        public string ModuleGuid { get; set; }
    }
}
