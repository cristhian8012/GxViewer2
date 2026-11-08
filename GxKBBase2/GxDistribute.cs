using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GxKBBase18
{
    public static class GxDistribute
    {
        public static string DistributeXML(int pIdModel, List<string> pListaObjects)
        {
            StringBuilder sb = new StringBuilder();
            List<dynamic> _objects = new List<dynamic>();
            
            /*aca de debe crear las propiedasdes del Modelo*/

            sb.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            sb.AppendLine(@"<ExportFile>");

            /*aqui van los datos de la KB*/
            sb.AppendLine(GxComposition.getXmlKBInformation(pIdModel));

            /*aqui van los datos de origen de la KB*/
            sb.AppendLine(GxComposition.getXmlSourceInformation(pIdModel));


            /*aqui van los objetos*/
            sb.AppendLine("<Objects>");
            foreach (string objeto in pListaObjects)
            {
                string[] parts = objeto.Split(':');
                int pEntityTypeId = int.Parse(parts[0]);
                int pEntityId = int.Parse(parts[1]);
                string pEntityName = parts[2];
                string pEntityType = parts[3];

                /*
                 * dynamic _object = new
                    {
                        Name = _metadata.Name,
                        Meta = _metadata,
                        Parts = _lstParts,
                        XML = ""
                    };
                 */
                dynamic _Object = GxComposition.getXMLFromObject(pIdModel, pEntityTypeId, pEntityId, pEntityName, pEntityType);
                _objects.Add(_Object);

                sb.AppendLine(_Object.XML);
            }

            sb.AppendLine("</Objects>");


            /*aqui van los atributos*/
            sb.AppendLine("<Attributes>");
            string _xmlAttr = GxComposition.getXmlAttributes(pIdModel, pListaObjects);
            sb.AppendLine("</Attributes>");

            /*aqui van las dependecnias*/
            Dictionary<string, string> _KeysRef = new Dictionary<string, string>();
            sb.AppendLine("<Dependencies>");
            foreach (var _obj in _objects)
            {
                string pEntityType = _obj.TypeName;

                if (!_KeysRef.ContainsKey(pEntityType))
                {
                    string _xmlDep = $@"
<Reference Package=""3ea7e1c6-b849-4df9-931a-070171a8a2f0"" Type=""Object"" Id=""{_obj.TypeGuid}"">
    <Properties Name=""{pEntityType}"" PackageName=""GenexusBL"" />
</Reference>"; //GxComposition.getXmlDependencies(pIdModel, pListaObjects);
                    sb.AppendLine(_xmlDep);
                    _KeysRef.Add(pEntityType, _obj.Name);
                }

                foreach (GxObjectPart _Parts in _obj.Parts)
                {
                    string _PartName = _Parts.Name.Replace($"{pEntityType},", "").Trim();
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


                    string _partGuid = GxComposition.TextoAGuid(_PartName).ToString();
                    if (!_KeysRef.ContainsKey(_PartName))
                    {
                        string _xmlDepart = $@"
<Reference Package=""3ea7e1c6-b849-4df9-931a-070171a8a2f0"" Type=""Part"" Id=""{_partGuid}"">
    <Properties Name=""{_PartName}"" PackageName=""GenexusBL"" />
</Reference>"; //GxComposition.getXmlDependencies(pIdModel, pListaObjects);
                        sb.AppendLine(_xmlDepart);
                        _KeysRef.Add(_PartName, _obj.Name);
                    }
                }

            }
            
            sb.AppendLine("</Dependencies>");

            /*aqui van las dependecnias*/
            sb.AppendLine("<ObjectsIdentityMapping>");
            string _xmlMap = GxComposition.getXmlMappings(pIdModel, pListaObjects);
            sb.AppendLine(_xmlMap);
            sb.AppendLine("</ObjectsIdentityMapping>");

            sb.AppendLine("</ExportFile>");

            return sb.ToString();
        }

        public static GXClass? GetGXClassByAbrev(string abrev)
        {
            if (string.IsNullOrWhiteSpace(abrev))
                return null;

            switch (abrev.ToLower())
            {
                case "trn": return GXClass.Transaction;
                case "att": return GXClass.Attribute;
                case "dom": return GXClass.Domain;
                case "prc": return GXClass.Procedure;
                case "rpt": return GXClass.Report;
                case "mnu": return GXClass.Menu;
                case "wkp": return GXClass.WorkPanel;
                case "dv": return GXClass.DataView;
                //case "pmt": return GXClass.Prompt;
                case "wbp": return GXClass.WebPanel;

                case "tbl": return GXClass.Table;
                //case "fld": return GXClass.Folder;
                //case "xpg": return GXClass.ExternalProgram;
                //case "mbr": return GXClass.MenuBar;

                //case "strn": return GXClass.StyleTransaction;
                //case "sprc": return GXClass.StyleProcedure;
                //case "srpt": return GXClass.StyleReport;
                //case "swkp": return GXClass.StyleWorkPanel;
                //case "spmt": return GXClass.StylePrompt;
                //case "swbp": return GXClass.StyleWebPanel;
                //case "smbr": return GXClass.StyleMenuBar;

                case "thm": return GXClass.Theme;
                case "sdt": return GXClass.SDT;

                default:
                    return null; // abreviatura no reconocida
            }
        }


    }
}
