using gxViewer.GxObject;
using log4net.Layout;
using mshtml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace gxViewer.Gx18
{
    public static class Gx18PropertyNames
    {
        public const string Transaction = "Transaction";
        public const string Structure = "Structure";
        public const string WebForm = "Web Form";
        public const string WinForm = "Win Form";
        public const string Rules = "Rules";
        public const string Events = "Events";
        public const string Variables = "Variables";
        public const string Help = "Help";
        public const string Documentation = "Documentation";
        public const string Table = "Table";
        public const string Indexes = "Indexes";
        public const string WorkPanel = "Work Panel";
        public const string WebPanel = "Web Panel";
        public const string Conditions = "Conditions";
        public const string Procedure = "Procedure";
        public const string Source = "Source";
        public const string Layout = "Layout";
        public const string Attribute = "Attribute";
    }

    public class Gx18Helper
    {
        private static Dictionary<string, Reference> _references = new Dictionary<string, Reference>();
        private static Dictionary<string, Attribute> _attributes = new Dictionary<string, Attribute>();
        private static Dictionary<string, string> _attributesIds = new Dictionary<string, string>();

        public static List<object> GetObjectsByXML(string strXML)
        {
            //List<gxViewer.GxObject.GXObj> _ret = new List<gxViewer.GxObject.GXObj>();
            List<object> _ret = new List<object>();

            XmlDocument _xmlMain = GxXML.GetDocument2(strXML);

            /* Convertir a Dependencies, para leer los tipos de objetos */
            XmlNode _xmlDependencies = GxXML.ReadElementSingleNode(_xmlMain, "/ExportFile/Dependencies");
            string _tmpXml = _xmlDependencies.OuterXml;
            Dependencies _dep = Functions.DeserializeXML_2<Dependencies>(_tmpXml);
            _references.Clear();
            foreach (var dep in _dep.Reference) 
            {
                Console.WriteLine($"Reference : {dep.Properties.Name}");
                _references.Add(dep.Id, dep);
            }

            /* Cargar Atributos */
            XmlNode _xmlattributes = GxXML.ReadElementSingleNode(_xmlMain, "/ExportFile/Attributes");
            if (_xmlattributes != null)
            {
                _tmpXml = _xmlattributes.OuterXml;
                Attributes _attrs = Functions.DeserializeXML_2<Attributes>(_tmpXml);
                _attributes.Clear();
                _attributesIds.Clear();
                foreach (var at in _attrs.Attribute)
                {
                    _attributes.Add(at.Guid, at);
                    _attributesIds.Add(at.Name, at.Guid);
                }
            }
            


            XmlNode _xmlnodesKNW = GxXML.ReadElement(_xmlMain, "KMW");
            GxObject.KMW _kmv = new gxViewer.GxObject.KMW();
            _kmv = Functions.DeserializeXML_2<gxViewer.GxObject.KMW>(_xmlnodesKNW.OuterXml); 

            XmlNode _xmlSource = GxXML.ReadElement(_xmlMain, "Source");
            Source _source  = Functions.DeserializeXML_2<Source>(_xmlSource.OuterXml);
            GxObject.Model _model = new gxViewer.GxObject.Model();
            _model.Name = _source.Version.Name;
            _model.Id = _source.Kb;
            _kmv.Path = _source.UNCPath;
            
            _model.Version = "Genexus 18";


            //Trabajamos con Objetos
            XmlNode _xmlObjects = GxXML.ReadElementSingleNode(_xmlMain, "/ExportFile/Objects");
            _tmpXml = _xmlObjects.OuterXml;
            Objects _objectss = Functions.DeserializeXML_2<Objects>(_tmpXml);

            try
            {

                foreach (var item in _objectss.Object)
                {
                    

                    GXObj _gxobj = new GXObj();
                    if (!_references.ContainsKey(item.Type))
                    {
                        Console.WriteLine(item.Name + " - " + item.Description + "-" + item.Type);
                        continue;
                    }
                    string _typeObject = _references[item.Type].Properties.Name;

                    switch (_typeObject)
                    {
                        case Gx18PropertyNames.WorkPanel:
                        case "WorkPanel":
                            _gxobj = new gxViewer.GxObject.WorkPanel();
                            break;

                        case Gx18PropertyNames.Procedure:
                            _gxobj = new gxViewer.GxObject.Procedure();
                            break;

                        case Gx18PropertyNames.Transaction:
                            _gxobj = new gxViewer.GxObject.Transaction();
                            break;

                        case Gx18PropertyNames.WebPanel:
                        case "WebPanel":
                            _gxobj = new gxViewer.GxObject.WebPanel();
                            break;
                        default:
                            continue;
                    }

                    gxViewer.GxObject.Info _inf = new GxObject.Info();
                    _inf.Name = item.Name.Substring(1);
                    _inf.Description = item.Description;
                    _inf.Folder = item.Parent;

                    _gxobj.Info = _inf;
                    _gxobj.CreationDate = item.VersionDate;
                    _gxobj.LastUpdate = item.LastUpdate;

                    _gxobj.Model = _model;

                    //Cargar Propiedades
                    foreach (var p in item.Properties.Property)
                    {
                        _gxobj.Propiedades.Add(p.Name, new GxObject.Property() { Name = p.Name, Value = p.Value });
                    }

                    foreach (var part in item.Part)
                    {
                        _typeObject = _references[part.Type].Properties.Name;

                        switch (_typeObject)
                        {
                            case Gx18PropertyNames.Documentation:
                                _gxobj.Documentation = new Documentation() { Source = part.InnerHtml };
                                break;

                            case Gx18PropertyNames.Variables:
                                //Cargar las variables
                                Dictionary<string, GxObject.Variable> _vars = new Dictionary<string, gxViewer.GxObject.Variable>();
                                Dictionary<string, string> _IdVars = new Dictionary<string, string>();
                                List<string> _listAttributesBasedOn = new List<string>();

                                foreach (var v in part.Variable)
                                {
                                    if (v.Properties == null)
                                    {
                                        continue;
                                    }

                                    gxViewer.GxObject.Variable _v = new GxObject.Variable();

                                    foreach (var p in v.Properties.Property)
                                    {
                                        string _valor = p.Value;
                                        var prop = _v.GetType().GetProperty(p.Name);
                                        if (p.Name.Equals("ATT_PICTURE") && prop == null)
                                        {
                                            prop = _v.GetType().GetProperty("Picture");
                                        }
                                        
                                        if (p.Name.Equals("ATTCUSTOMTYPE") && prop == null)
                                        {
                                            prop = _v.GetType().GetProperty("Type");
                                            _valor = p.Value.Replace("bas:", "");
                                        }

                                        if (p.Name.Equals("AttRows") && prop == null)
                                        {
                                            prop = _v.GetType().GetProperty("Rows");
                                        }

                                        if (p.Name.Equals("AttCols") && prop == null)
                                        {
                                            if (int.Parse(_valor) > 1)
                                            {
                                                prop = _v.GetType().GetProperty("Columns");
                                            }
                                        }


                                        //</Name><Value>1</Value></Property><Property><Name></Name><Value>1000<

                                        if (p.Name.Equals("idBasedOn") && prop == null)
                                        {
                                            prop = _v.GetType().GetProperty("BasedOn");
                                            if (p.Value.StartsWith("Att") && p.Value.Contains(":"))
                                            {
                                                string _attrBased = p.Value.Replace("Attribute:", "");
                                                if (!_listAttributesBasedOn.Contains(_attrBased))
                                                {
                                                    _listAttributesBasedOn.Add(_attrBased);
                                                }
                                            }
                                        }


                                        if (prop != null)
                                        {
                                            // Convertimos el valor al tipo adecuado si es necesario
                                            var convertedValue = Convert.ChangeType(_valor, prop.PropertyType);
                                            prop.SetValue(_v, convertedValue);
                                        }
                                    }

                                    if (!_vars.ContainsKey(_v.Name))
                                    {

                                        //Parche para colocar el DataType cuando es basado en algun tipo de dato
                                        if (!String.IsNullOrEmpty(_v.BasedOn) && !_v.BasedOn.ToLower().StartsWith("att"))
                                        {
                                            string[] _strBasedOn = _v.BasedOn.Split(':');
                                            if (_strBasedOn.Length > 1)
                                            {
                                                string _key = _strBasedOn[0].Trim();
                                                string _val = _strBasedOn[1].Trim();

                                                _v.Type = _val;
                                                if (_v.Length.Equals(0))
                                                {
                                                    switch (_val.ToLower())
                                                    {
                                                        case "date":
                                                            _v.Length = 8; break;

                                                        case "character":
                                                            _v.Length = 20; break;

                                                        case "numeric":
                                                            _v.Length = 3; break;
                                                    }
                                                }
                                            }
                                        }

                                        _vars.Add(_v.Name, _v);
                                        if (_v.Id != null)
                                        {
                                            _IdVars.Add(_v.Id, _v.Name);
                                        }
                                    }

                                }
                                _gxobj.Variables = new GxObject.Variable[_vars.Count];
                                _vars.Values.CopyTo(_gxobj.Variables, 0);
                                _gxobj.IdVariables = _IdVars;
                                _gxobj.GXVariables = _vars;

                                //Cargamos los Attributos de variables basadas EN..
                                if (_listAttributesBasedOn.Count > 0 && _attributesIds.Count > 0)
                                {
                                    Dictionary<string, GxObject.GXAtt> _attribs2 = new Dictionary<string, gxViewer.GxObject.GXAtt>();
                                    foreach (var vAttrBase in _listAttributesBasedOn)
                                    {
                                        gxViewer.GxObject.GXAtt _vAtt = new GxObject.GXAtt();
                                        gxViewer.GxObject.Attribute _xAtt = new GxObject.Attribute();
                                        //Attribute _gx18Attr = _attributes[_attributesIds[vAttrBase]];
                                        Attribute _gx18Attr = null;
                                        try
                                        {

                                            _gx18Attr = _attributes[_attributesIds[vAttrBase]];
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine($"Error al obtener el atributo {vAttrBase}: {ex.Message}");
                                            //throw;
                                            continue;
                                        }
                                        

                                        foreach (var p in _gx18Attr.Properties.Property)
                                        {
                                            if (!String.IsNullOrEmpty(p.Name))
                                            {
                                                var prop = _xAtt.GetType().GetProperty(p.Name);
                                                if (p.Name.Equals("Description") && prop == null)
                                                {
                                                    prop = _xAtt.GetType().GetProperty("Title");
                                                }

                                                
                                                if (p.Name.Equals("ATT_PICTURE") && prop == null)
                                                {
                                                    prop = _xAtt.GetType().GetProperty("Picture");
                                                }

                                                if (p.Name.Equals("ATTCUSTOMTYPE") && prop == null)
                                                {
                                                    prop = _xAtt.GetType().GetProperty("Type");
                                                    string _valor = p.Value.Replace("bas:", "");
                                                    var convertedValue = Convert.ChangeType(_valor, prop.PropertyType);
                                                    prop.SetValue(_xAtt, convertedValue);
                                                    prop = null;
                                                }


                                                if (prop != null)
                                                {
                                                    // Convertimos el valor al tipo adecuado si es necesario
                                                    var convertedValue = Convert.ChangeType(p.Value, prop.PropertyType);
                                                    prop.SetValue(_xAtt, convertedValue);
                                                }
                                            }
                                         }

                                        if (String.IsNullOrEmpty(_xAtt.Name))
                                        {
                                            _xAtt.Name = _gx18Attr.Name;
                                            _xAtt.Name = _gx18Attr.Name;
                                        }
                                        if (_xAtt.Length.Equals(0) && _xAtt.Type.ToLower().Equals("numeric"))
                                        {
                                            if (!String.IsNullOrEmpty(_xAtt.Picture))
                                            {
                                                _xAtt.Length = _xAtt.Picture.Trim().Length;
                                            }
                                        }

                                        _vAtt.Attribute = _xAtt;
                                        _attribs2.Add(_vAtt.Name, _vAtt);
                                    }
                                    _gxobj.GXAtts = new GxObject.GXAtt[_attribs2.Count];
                                    _attribs2.Values.CopyTo(_gxobj.GXAtts, 0);
                                    _gxobj.GXAtributos = _attribs2;

                                }



                                break;

                            case Gx18PropertyNames.Attribute:
                                //Cargar las variables


                                break;

                            case Gx18PropertyNames.Rules:
                                _gxobj.Rules = part.Source;
                                break;

                            case Gx18PropertyNames.Conditions:
                                _gxobj.Conditions = part.Source;
                                break;

                            case Gx18PropertyNames.Source:
                                _gxobj.Source = part.Source;
                                break;

                            case Gx18PropertyNames.WebForm:
                                if (_gxobj.GetType() == typeof(WebPanel))
                                {
                                    ((WebPanel)_gxobj).strHTMLForm += part.Source;
                                }
                                break;

                            case Gx18PropertyNames.Help:
                                _gxobj.Help = new GxObject.Help() { Source = part.Source };
                                break;

                            case Gx18PropertyNames.Events:
                                if (_gxobj.GetType() == typeof(WorkPanel))
                                {
                                    ((WorkPanel)_gxobj).Events += part.Source;
                                }

                                if (_gxobj.GetType() == typeof(Transaction))
                                {
                                    ((Transaction)_gxobj).Events += part.Source;
                                }

                                if (_gxobj.GetType() == typeof(WebPanel))
                                {
                                    ((WebPanel)_gxobj).Events += part.Source;
                                }

                                break;

                            case Gx18PropertyNames.Structure:

                                Dictionary<string, GxObject.GXAtt> _attribs = new Dictionary<string, gxViewer.GxObject.GXAtt>();

                                StringBuilder _strbStructure = new StringBuilder();

                                foreach (var a in part.Level.Attribute)
                                {
                                    gxViewer.GxObject.GXAtt _vAtt = new GxObject.GXAtt();
                                    gxViewer.GxObject.Attribute _xAtt = new GxObject.Attribute();

                                    Attribute _gx18Attr = _attributes[a.Guid];
                                    foreach (var p in _gx18Attr.Properties.Property)
                                    {
                                        if (!String.IsNullOrEmpty(p.Name))
                                        {
                                            var prop = _xAtt.GetType().GetProperty(p.Name);
                                            if (p.Name.Equals("Description") && prop == null)
                                            {
                                                prop = _xAtt.GetType().GetProperty("Title");
                                            }

                                            if (prop != null)
                                            {
                                                // Convertimos el valor al tipo adecuado si es necesario
                                                var convertedValue = Convert.ChangeType(p.Value, prop.PropertyType);
                                                prop.SetValue(_xAtt, convertedValue);
                                            }
                                        }
                                    }

                                    if (String.IsNullOrEmpty(_xAtt.Name))
                                    {
                                        _xAtt.Name = _gx18Attr.Name;
                                        _xAtt.Name = _gx18Attr.Name;
                                    }
                                    _vAtt.Attribute = _xAtt;

                                    _strbStructure.AppendLine($"{_vAtt.Name}{(a.Key.Equals("True") ? "*" : "")}");

                                    _attribs.Add(_vAtt.Name, _vAtt);

                                }

                                _gxobj.GXAtts = new GxObject.GXAtt[_attribs.Count];
                                _attribs.Values.CopyTo(_gxobj.GXAtts, 0);
                                _gxobj.GXAtributos = _attribs;


                                if (_gxobj.GetType() == typeof(Transaction))
                                {
                                    ((Transaction)_gxobj).Structure = new Structure() { Source = _strbStructure.ToString() };
                                }

                                break;

                            case Gx18PropertyNames.Layout:
                                if (_gxobj.GetType() == typeof(Procedure))
                                {
                                    if (part.Layout != null && part.Layout.Bands != null)
                                    {
                                        gxViewer.GxObject.Layout _layout = new gxViewer.GxObject.Layout();

                                        //Establecemos propiedades del Layout
                                        _layout.Paper = new gxViewer.GxObject.Paper();
                                        _layout.FormInfo = new gxViewer.GxObject.FormInfo();

                                        foreach (var p in part.Properties.Property)
                                        {
                                            switch (p.Name)
                                            {
                                                case "RPT_PAPER_SIZE":
                                                    _layout.Paper.PaperId = p.Value;
                                                    break;

                                                case "RPT_RIGHT_MARGIN":
                                                    _layout.Paper.SizeX = p.Value;
                                                    break;

                                                case "RPT_ATT_FONT":
                                                    string[] _attrfont = p.Value.Split(',');
                                                    int _size = 9;
                                                    int.TryParse(_attrfont[_attrfont.Length - 1].Replace("pt", ""), out _size);
                                                    _layout.FormInfo.Att_Font = new Att_Font() { Name = _attrfont[0], Size = _size };
                                                    break;

                                                case "RPT_TXT_FONT":
                                                    string[] _txtfont = p.Value.Split(',');
                                                    //int _fsize = 8;
                                                    //int.TryParse(_txtfont[1].Replace("pt", ""), out _fsize);
                                                    _layout.FormInfo.Txt_Font = new Txt_Font() { Bold = _txtfont[_txtfont.Length - 1].Replace("style=", "") };
                                                    break;

                                                default:
                                                    break;
                                            }
                                        }


                                        List<gxViewer.GxObject.Block> _listBlocks = new List<Block>();
                                        //Recorremos los bloques
                                        foreach (var pblock in part.Layout.Bands.PrintBlock)
                                        {
                                            gxViewer.GxObject.PrintBlock _pb = new gxViewer.GxObject.PrintBlock();
                                            gxViewer.GxObject.CodeBlock _cb = new gxViewer.GxObject.CodeBlock();

                                            foreach (var pPB in pblock.Properties.Property)
                                            {
                                                switch (pPB.Name)
                                                {
                                                    case "RPT_INTERNAL_NAME":
                                                        _pb.CtrlName = pPB.Value;
                                                        break;

                                                    case "RPT_X":
                                                        _pb.left = (int)Math.Round(int.Parse(pPB.Value) * 7.1, 0);
                                                        break;

                                                    case "RPT_Y":
                                                        _pb.top = (int)Math.Round(int.Parse(pPB.Value) * 12.4, 0);
                                                        break;

                                                    case "RPT_WIDTH":
                                                        _pb.right = (int)Math.Round(int.Parse(pPB.Value) * 8.569, 0);
                                                        break;

                                                    case "RPT_HEIGHT":
                                                        _pb.bottom = int.Parse(pPB.Value);
                                                        break;

                                                    default:
                                                        break;
                                                }
                                            }
                                            _pb.bottom = _pb.top + (int)(Math.Round(_pb.bottom * 16.85714286, 0));

                                            GxObject.Block _blk = new GxObject.Block() { PrintBlock = _pb, CodeBlock = _cb };

                                            if (pblock.Controls != null)
                                            {
                                                List<GxObject.Edit> _listEdits = new List<Edit>();
                                                foreach (var rpA in pblock.Controls.ReportAttributes)
                                                {
                                                    GxObject.Edit _edit = new Edit();
                                                    _edit.Font = new GxObject.Font() { Name= "Courier New", Size=10 };

                                                    foreach (var ppDE in rpA.Properties.Property)
                                                    {
                                                        switch (ppDE.Name)
                                                        {
                                                            case "RPT_ID":
                                                                _edit.Id = int.Parse(ppDE.Value);
                                                                break;

                                                            case "RowIndex":
                                                                _edit.Row = ppDE.Value;
                                                                break;

                                                            case "ColIndex":
                                                                _edit.Col = ppDE.Value;
                                                                break;

                                                            case "AttID":
                                                                _edit.Name = ppDE.Value.Replace("&", "");
                                                                _edit.Font.Underline = "Y";
                                                                break;

                                                            case "RPT_TEXT":
                                                                _edit.Text = ppDE.Value;
                                                                break;

                                                            case "RPT_X":
                                                                _edit.left = (int)Math.Round(int.Parse(ppDE.Value) * 7.859, 0);
                                                                break;

                                                            case "RPT_Y":
                                                                int _row = int.Parse(ppDE.Value);
                                                                double _factor = 14.6;

                                                                if (_row == 1)
                                                                {
                                                                    _edit.top = 0;
                                                                }
                                                                else
                                                                {
                                                                    _edit.top = (int)Math.Round(_row * _factor, 0);
                                                                }
                                                                _edit.bottom = (int)(_edit.top + _factor);
                                                                break;

                                                            default:
                                                                break;
                                                        }
                                                    }

                                                    _listEdits.Add(_edit);
                                                }
                                                _blk.Edits = _listEdits.ToArray();

                                                List<GxObject.Text> _listTexts = new List<GxObject.Text>();
                                                foreach (var rpL in pblock.Controls.ReportLabels)
                                                {
                                                    GxObject.Text _text = new GxObject.Text();
                                                    _text.Font = new GxObject.Font() { Name = "Courier New", Size = 10 };

                                                    foreach (var ppDTX in rpL.Properties.Property)
                                                    {
                                                        switch (ppDTX.Name)
                                                        {
                                                            case "RPT_ID":
                                                                _text.Id = int.Parse(ppDTX.Value);
                                                                break;

                                                            case "AttID":
                                                                _text.Name = ppDTX.Value.Replace("&", "");
                                                                break;

                                                            case "RPT_TEXT":
                                                                _text.text = ppDTX.Value;
                                                                break;

                                                            case "RPT_X":
                                                                _text.left = (int)Math.Round(int.Parse(ppDTX.Value) * 7.859, 0); //int.Parse(ppDE.Value) * 6;
                                                                break;

                                                            case "RPT_Y":
                                                                int _row = int.Parse(ppDTX.Value);
                                                                double _factor = 14.6;
                                                                
                                                                if (_row == 1)
                                                                {
                                                                    _text.top = 0;
                                                                }
                                                                else
                                                                {
                                                                    _text.top = (int)Math.Round(_row * _factor, 0);
                                                                }
                                                                _text.bottom = (int)(_text.top + _factor);
                                                                break;

                                                            default:
                                                                break;
                                                        }
                                                    }

                                                    _listTexts.Add(_text);
                                                }
                                                _blk.Texts = _listTexts.ToArray();
                                            }


                                            _listBlocks.Add(_blk);
                                        }

                                        _layout.Blocks = _listBlocks.ToArray();
                                        ((Procedure)_gxobj).Layout = _layout;
                                    }
                                }
                                break;

                            case Gx18PropertyNames.WinForm:
                                if (_gxobj.GetType() == typeof(WorkPanel))
                                {
                                    GxObject.Form _frm = new GxObject.Form();
                                    _frm.FormInfo = new FormInfo();
                                    if (part.Document == null)
                                    {
                                        continue;
                                    }
                                    _frm.FormInfo.FormClass = part.Document.Form.Class;
                                    _frm.Type = part.Document.Form.Type;
                                    _frm.Block = new Block();
                                    _frm.Block.Frame = new Frame();

                                    foreach (var pp in part.Document.Form.Canvas.Properties.Property)
                                    {

                                        var prop = _frm.Block.Frame.GetType().GetProperty(pp.Name);
                                        //if (p.Name.Equals("ATT_PICTURE") && prop == null)
                                        //{
                                        //    prop = _v.GetType().GetProperty("Picture");
                                        //}
                                        if (prop != null)
                                        {
                                            // Convertimos el valor al tipo adecuado si es necesario
                                            var convertedValue = Convert.ChangeType(pp.Value, prop.PropertyType);
                                            prop.SetValue(_frm.Block.Frame, convertedValue);
                                        }
                                    }
                                    if (_frm.Block.Frame.right == 0 || _frm.Block.Frame.left == 0)
                                    {
                                        _frm.Block.Frame.left = _frm.Block.Frame.DataLeft;
                                        _frm.Block.Frame.right = _frm.Block.Frame.DataRight * 2;
                                        _frm.Block.Frame.top = _frm.Block.Frame.DataTop;
                                        _frm.Block.Frame.bottom = _frm.Block.Frame.DataBottom * 3;
                                    }

                                    if (part.Document.Form.Canvas.Elements != null)
                                    {

                                        //EditText
                                        List<GxObject.Edit> _listEdits = new List<Edit>();
                                        foreach (var de in part.Document.Form.Canvas.Elements.DataEntry)
                                        {
                                            GxObject.Edit _edit = new Edit();
                                            _edit.Font = new GxObject.Font();
                                            foreach (var ppDE in de.Properties.Property)
                                            {
                                                switch (ppDE.Name)
                                                {
                                                    case "AttID":
                                                        _edit.Name = ppDE.Value.Replace("&", "");
                                                        break;

                                                    case "Left":
                                                        _edit.left = int.Parse(ppDE.Value) * 6;
                                                        break;

                                                    case "Top":
                                                        _edit.top = int.Parse(ppDE.Value) * 11;
                                                        break;

                                                    case "ForeColorText":
                                                        if (_edit.ColorInfo == null)
                                                        {
                                                            _edit.ColorInfo = new ColorInfo();
                                                        }
                                                        _edit.ColorInfo.Foreground = ppDE.Value;
                                                        break;

                                                    case "BackColorText":
                                                        if (_edit.ColorInfo == null)
                                                        {
                                                            _edit.ColorInfo = new ColorInfo();
                                                        }
                                                        _edit.ColorInfo.Background = ppDE.Value;
                                                        break;

                                                    case "Font":
                                                        string[] _attrfont = ppDE.Value.Split(',');
                                                        int _size = 9;
                                                        int.TryParse(_attrfont[_attrfont.Length - 1].Replace("pt", ""), out _size);
                                                        _edit.Font.Size = _size;
                                                        _edit.Font.Name = _attrfont[0];
                                                        break;


                                                    default:
                                                        break;
                                                }
                                            }
                                            _listEdits.Add(_edit);
                                        }
                                        _frm.Block.Edits = _listEdits.ToArray();

                                        //Label
                                        List<GxObject.Text> _listTexts = new List<GxObject.Text>();
                                        foreach (var la in part.Document.Form.Canvas.Elements.Label)
                                        {
                                            GxObject.Text _text = new GxObject.Text();
                                            _text.Font = new GxObject.Font();

                                            foreach (var ppDE in la.Properties.Property)
                                            {
                                                switch (ppDE.Name)
                                                {
                                                    case "Name":
                                                        _text.Name = ppDE.Value;
                                                        break;

                                                    case "CaptionExpression":
                                                        _text.text = ppDE.Value;
                                                        _text.Source = ppDE.Value;
                                                        break;

                                                    case "Left":
                                                        _text.left = int.Parse(ppDE.Value) * 6;
                                                        break;

                                                    case "Top":
                                                        _text.top = int.Parse(ppDE.Value) * 11;
                                                        break;

                                                    case "ForeColorText":
                                                        if (_text.ColorInfo == null)
                                                        {
                                                            _text.ColorInfo = new ColorInfo();
                                                        }
                                                        _text.ColorInfo.Foreground = ppDE.Value;
                                                        break;

                                                    case "BackColorText":
                                                        if (_text.ColorInfo == null)
                                                        {
                                                            _text.ColorInfo = new ColorInfo();
                                                        }
                                                        _text.ColorInfo.Background = ppDE.Value;
                                                        break;

                                                    case "Font":
                                                        string[] _attrfont = ppDE.Value.Split(',');
                                                        int _size = 9;
                                                        int.TryParse(_attrfont[_attrfont.Length - 1].Replace("pt", ""), out _size);
                                                        _text.Font.Size = _size;
                                                        _text.Font.Name = _attrfont[0];
                                                        break;



                                                    default:
                                                        break;
                                                }
                                            }


                                            _listTexts.Add(_text);
                                        }
                                        _frm.Block.Texts = _listTexts.ToArray();


                                        //SubFiles - Grids
                                        if (part.Document.Form.Canvas.Elements.Grid != null)
                                        {
                                            List<GxObject.SubFile> _lstSubfiles = new List<GxObject.SubFile>();
                                            foreach (var grd in part.Document.Form.Canvas.Elements.Grid)
                                            {
                                                GxObject.SubFile _sf = new GxObject.SubFile();

                                                //Establecemos las propiedades
                                                foreach (var ppGr in grd.Properties.Property)
                                                {
                                                    switch (ppGr.Name)
                                                    {
                                                        case "FromDefault":
                                                            _sf.FromDefault = ppGr.Value;
                                                            break;
                                                        case "FromStyle":
                                                            _sf.FromStyle = ppGr.Value;
                                                            break;
                                                        case "AutoResize":
                                                            _sf.AutoResize = ppGr.Value;
                                                            break;
                                                        case "BackColor":
                                                            _sf.BackColor = ppGr.Value;
                                                            break;
                                                        case "ForeColor":
                                                            _sf.ForeColor = ppGr.Value;
                                                            break;
                                                        //case "TitleFont":
                                                        //    _sf.TitleFont = ppGr.Value;
                                                        //    break;
                                                        //case "LinesFont":
                                                        //    _sf.LinesFont = ppGr.Value;
                                                        //    break;
                                                        case "Left":
                                                            _sf.left = int.Parse(ppGr.Value) * 6;
                                                            break;
                                                        case "Top":
                                                            _sf.top = int.Parse(ppGr.Value) * 11;
                                                            break;

                                                        case "DataLeft":
                                                            _sf.DataLeft = int.Parse(ppGr.Value);
                                                            break;

                                                        case "DataTop":
                                                            _sf.DataTop = int.Parse(ppGr.Value);
                                                            break;

                                                        case "DataRight":
                                                            _sf.DataRight = int.Parse(ppGr.Value);
                                                            break;

                                                        case "DataBottom":
                                                            _sf.DataBottom = int.Parse(ppGr.Value);
                                                            break;


                                                        case "Height":
                                                            _sf.bottom = int.Parse(ppGr.Value) * 3;
                                                            break;

                                                        default:
                                                            Console.WriteLine($"Unknown property {ppGr.Name}: {ppGr.Value}");
                                                            break;
                                                    }

                                                }

                                                //Agregar las Columnas de la Grila
                                                List<GxObject.Item> _listItems = new List<GxObject.Item>();
                                                foreach (var ppGr in grd.Elements.GridColumn)
                                                {
                                                    GxObject.Item _item = new GxObject.Item();
                                                    foreach (var ppCol in ppGr.Properties.Property)
                                                    {
                                                        switch (ppCol.Name)
                                                        {
                                                            case "AttID":
                                                                _item.Name = ppCol.Value.Replace("&", "");
                                                                break;

                                                            case "ColTitleExpression":
                                                                _item.Title = ppCol.Value;
                                                                break;

                                                            default:
                                                                break;
                                                        }
                                                    }

                                                    _listItems.Add(_item);

                                                }
                                                _sf.Items = _listItems.ToArray();


                                                if (_sf.top == 0 || _sf.left == 0)
                                                {
                                                    _sf.left = _sf.DataLeft;
                                                    _sf.right = _sf.DataRight * 2;
                                                    _sf.top = _sf.DataTop;
                                                    _sf.bottom = _sf.DataBottom * 3;
                                                }


                                                _lstSubfiles.Add(_sf);
                                            }


                                            _frm.Block.Subfiles = _lstSubfiles.ToArray();
                                        }

                                    }

                                    ((WorkPanel)_gxobj).Form = _frm;
                                }

                                break;

                            default:
                                continue;
                        }

                    }


                    _gxobj.KMW = _kmv;
                    _gxobj.XMLObj = "<GX18> CAR - GENEXUS 18 </GX18>";// _node.OuterXml;
                    _ret.Add(_gxobj);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }

            return _ret;

        }
    }
}
