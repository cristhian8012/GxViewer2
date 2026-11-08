using System;

namespace GxKBBase18
{
    public class GxDependencyReference
    {
        /// <summary>
        /// Package GUID (normalmente el KB)
        /// </summary>
        public string PackageGuId { get; set; }

        /// <summary>
        /// Object | Part
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// GUID del objeto o parte
        /// </summary>
        public string Id { get; set; }

        public GxDependencyProperties Properties { get; set; } = new GxDependencyProperties();
    }
    public class GxDependencyProperties
    {
        /// <summary>
        /// Nombre funcional (Work Panel, Events, Rules, Attribute, etc.)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Paquete lógico (GenexusBL)
        /// </summary>
        public string PackageName { get; set; }
    }
}