using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Xml.Linq;
using System.Windows.Forms.VisualStyles;
using System.Dynamic;
using System.Security.Cryptography;


namespace GxKBBase18
{
    public static class GxComposition
    {

        private static string getValueProperty(List<GxObjectProperty> pProps, string pProp)
        {
            if (pProps == null || string.IsNullOrEmpty(pProp))
                return "";

            var prop = pProps.FirstOrDefault(p =>
                p.Name.Equals(pProp, StringComparison.OrdinalIgnoreCase));

            return prop?.Value;
        }

        public static dynamic getXMLFromObject(int pIdModel, int pEntityTypeId, int pEntityId, string pNameEntity, string pEntityType)
        {
            GxObjectMetadata _metadata = KnowledgeBase.KB.getMetadata(pIdModel, pEntityTypeId, pEntityId);
            List<GxObjectProperty> _lstPropeerties = KnowledgeBase.KB.getProperties(pIdModel, pEntityTypeId, pEntityId);
            List<GxObjectPart> _lstParts = KnowledgeBase.KB.getCompositionParts(pIdModel, pEntityTypeId, pEntityId);
            StringBuilder sb = new StringBuilder();

            dynamic _object = new ExpandoObject();
            _object.Name = _metadata.Name;
            _object.TypeName = pEntityType;
            _object.TypeGuid = TextoAGuid(pEntityType);
            _object.Meta = _metadata;
            _object.Parts = _lstParts;
            _object.XML = "";
            

            /*aca de debe crear las propiedasdes del objeto*/

            string _parentGuid = _metadata.ParentGuid;
            string _user = _metadata.User;
            string _versionDate = _metadata.VersionDate;
            string _lastUpdate = _metadata.LastUpdate;
            string _checksum = _metadata.Checksum;
            string _fullyQualifiedName = _metadata.FullyQualifiedName;
            string _moduleGuid = _metadata.ModuleGuid;
            string _guid = _metadata.Guid;
            string _name = _metadata.Name;
            string _type = _object.TypeGuid.ToString();// pEntityId.ToString(); //_metadata.TypeGuid;
            string _description = _metadata.Description; // "v4. Mantenimiento de Usua.x Conv.";
            string _parent = _metadata.ParentName;
            string _parentType = getValueProperty(_lstPropeerties, "FolderType"); 


            sb.AppendLine($"<Object parentGuid=\"{_parentGuid}\" user=\"{_user}\" versionDate=\"{_versionDate}\" lastUpdate=\"{_lastUpdate}\" checksum=\"{_checksum}\" fullyQualifiedName=\"{_fullyQualifiedName}\" moduleGuid=\"{_moduleGuid}\" guid=\"{_guid}\" name=\"{_name}\" type=\"{_type}\" description=\"{_description}\" parent=\"{_parent}\" parentType=\"{_parentType}\">");
            
            /*aca de debe crear las partes del objeto*/

            foreach (GxObjectPart gxPart in _lstParts)
            {
                StringBuilder _xmlTag = new StringBuilder();
               

                int EntityTypeId = gxPart.EntityTypeId; //Convert.ToInt32(dr["EntityTypeId"]);
                int EntityId = gxPart.EntityId; // Convert.ToInt32(dr["EntityId"]);

                string _PartName = gxPart.Name.Replace($"{pEntityType},","").Trim();// dr["EntityVersionName"].ToString();
                if (_PartName.Equals(pEntityType))
                {
                    if (pEntityType.Equals("Procedure") || pEntityType.Equals("Report"))
                    {
                        _PartName = "Source";
                    }
                    else
                    {
                        _PartName = "Events";
                    }
                }
                string _partGuid = TextoAGuid(_PartName).ToString();
                //string xmlChild = getXMLFromObject(pIdModel, EntityTypeId, EntityId);
                byte[] rawData = gxPart.RawData;  //(byte[])dr["EntityVersionData"];

                string xml = GxConvertVarbinary.DecompressAndFormatGzip(rawData);
                

                // 5. Convertir XML tokenizado a código humano
                string _strcode = "";
                switch (_PartName)
                {
                    case "Conditions":
                    case "Events":
                    case "Rules":
                    case "Source":
                        //case "Procedure":
                        _strcode = GxConvertVarbinary.ConvertXmlToCode(xml, _PartName);

                        _xmlTag.Append($"<Part type=\"{_partGuid}\">");
                        _xmlTag.Append($"<Source>");
                        _xmlTag.Append($"<![CDATA[{_strcode}]]>");
                        _xmlTag.Append($"</Source>");
                        //_xmlTag.Append(""); Aca van las propiedades
                        _xmlTag.Append($"</Part>");


                        break;

                    case "Documentation":
                        string _htmlDoc = WebUtility.HtmlDecode(xml);
                        if (!String.IsNullOrEmpty(_htmlDoc))
                        {
                            _strcode = GxConvertVarbinary.ExtractStorableContent(_htmlDoc, "StorableContent");
                        }
                        _xmlTag.AppendLine($"<Part type=\"{_partGuid}\">");
                        _xmlTag.AppendLine($"<InnerHtml>");
                        _xmlTag.AppendLine($"<![CDATA[{_strcode}]]>");
                        _xmlTag.AppendLine($"</InnerHtml>");
                        //_xmlTag.Append(""); Aca van las propiedades
                        _xmlTag.Append($"</Part>");
                        break;

                    case "Help":
                        _xmlTag.AppendLine( GxConvertVarbinary.ConvertHelpToPart(xml, _partGuid));
                        break;

                    case "Variables":
                        //Logica para las variables
                        break;

                    default:

                        if (_PartName.Equals(pEntityType))
                        {
                            _strcode = GxConvertVarbinary.ConvertXmlToCode(xml, "Events");

                            _xmlTag.AppendLine($"<Part type=\"{_partGuid}\">");
                            _xmlTag.AppendLine($"<Source>");
                            _xmlTag.AppendLine($"<![CDATA[{_strcode}]]>");
                            _xmlTag.AppendLine($"</Source>");
                            //_xmlTag.Append(""); Aca van las propiedades
                            _xmlTag.AppendLine($"</Part>");
                        }

                        _strcode = "";
                        break;
                }

                sb.AppendLine(_xmlTag.ToString());
            }


            /*Escribimos las propiedades*/
            /*
             * <Properties><Property><Name>Name</Name><Value>PLCCP58D</Value></Property>
             */
            sb.AppendLine("<Properties>");
            foreach (var prop in _lstPropeerties)
            {
                if (prop.Name!= "Signatures" && prop.Name != "PublicMethods")
                {
                    sb.Append($"<Property><Name>{prop.Name}</Name><Value>{prop.Value}</Value></Property>");
                }
            }
            sb.AppendLine("</Properties>");


            sb.AppendLine("</Object>");

            _object.XML = sb.ToString();


            return _object;//sb.ToString(); 
        }


        public static string getXmlAttributes(int pIdModel, List<string> pListaObjects)
        {
            //List<GxObjectPart> _lstParts = KnowledgeBase.KB.getCompositionParts(pIdModel, pEntityTypeId, pEntityId);
            //string[] parts = objeto.Split('|');
            //int pEntityTypeId = Convert.ToInt32(parts[0]);
            //int pEntityId = Convert.ToInt32(parts[1]);
            return "";
            //throw new NotImplementedException();
        }

        internal static string getXmlDependencies(int pIdModel, List<string> pListaObjects)
        {
            StringBuilder _strb = new StringBuilder();
            foreach (string part in pListaObjects)
            {
                string[] parts = part.Split(':');
                int pEntityTypeId = Convert.ToInt32(parts[0]);
                int pEntityId = Convert.ToInt32(parts[1]);

                GxDependencyReference gxDep = KnowledgeBase.KB.getDependencies(pIdModel, pEntityTypeId, pEntityId);
                /*
                    <Reference Package="3ea7e1c6-b849-4df9-931a-070171a8a2f0" Type="Object" Id="198e8ea4-1d49-4c9c-8a9a-417024baa9d1">
                    <Properties Name="Work Panel" PackageName="GenexusBL" />
                </Reference>
                    */
                _strb.AppendLine($"<Reference Package=\"{gxDep.PackageGuId}\" Type=\"{gxDep.Type}\" Id=\"{gxDep.Id}\">");
                _strb.AppendLine($"<Properties Name=\"{gxDep.Properties.Name}\" PackageName=\"{gxDep.Properties.PackageName}\" />");
                _strb.AppendLine("</Reference>");

            }




            return _strb.ToString();
        }

        internal static string getXmlMappings(int pIdModel, List<string> pListaObjects)
        {
            return string.Empty;
        }

        internal static string getXmlKBInformation(int pIdModel)
        {
            StringBuilder _strb = new StringBuilder();
            _strb.AppendLine("<KMW>");
            _strb.AppendLine("<MajorVersion>4</MajorVersion>");
            _strb.AppendLine("<MinorVersion>0</MinorVersion>");
            _strb.AppendLine("<Build>173650</Build>");
            _strb.AppendLine("</KMW>");

            return _strb.ToString();

        }

        internal static string getXmlSourceInformation(int pIdModel)
        {
            StringBuilder _strb = new StringBuilder();
            _strb.AppendLine("<Source kb=\"a8a173a1-7739-442f-9d4e-51e4e5e046e8\" username=\"PER\\s7378701\" UNCPath=\"\\\\PERL9APF47CDDX\\C$\\Modelos\\G18BTTW\">");
            _strb.AppendLine("<Version guid=\"7b9f3617-319e-4bbb-a489-12b974c42e02\" name=\"G18BTTW\" />");
            _strb.AppendLine("</Source>");
            return _strb.ToString();

        }

        public static Guid TextoAGuid(string texto)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(texto));
                return new Guid(hash);
            }
        }
    }
}
