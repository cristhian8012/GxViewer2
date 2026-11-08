using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;
using gxViewer.GxObject;
using System.Diagnostics;
using System.Xml;
using gxViewer.XPWConverter;
using mshtml;
using System.IO.Ports;

namespace gxViewer
{
    public class HTMLtoGxXML
    {
        private static string _gxType = String.Empty;
        

        private static void SetPropertyForHTML(ref GxObject.GXObj _gxObject, mshtml.IHTMLElement _elem)
        {
            Dictionary<string, string> _dProperties = new Dictionary<string, string>();
            _gxType = String.Empty;

            mshtml.IHTMLDocument2 _FirstDoc = (mshtml.IHTMLDocument2)(new mshtml.HTMLDocument());
            _FirstDoc.write(_elem.outerHTML);
            mshtml.HTMLDocument _FirstD = (mshtml.HTMLDocument)_FirstDoc;

            foreach (mshtml.IHTMLElement _tableHTML in _FirstD.getElementsByTagName("TABLE"))
            {
                mshtml.HTMLTableClass _tab = (mshtml.HTMLTableClass)_tableHTML;

                if (_tab.className != null)
                {
                    if (_tab.className.Equals("gxcontent"))
                    {
                        bool _indicadorContinue2 = true;
                        foreach (mshtml.IHTMLElement _row in _tab.rows)
                        {
                            mshtml.HTMLTableRowClass _r = (mshtml.HTMLTableRowClass)_row;

                            int _indexCol = 0;
                            string _key = String.Empty;
                            string _value = String.Empty;
                            foreach (mshtml.IHTMLElement _cell in _r.cells)
                            {
                                _indexCol++;
                                if (((mshtml.HTMLTableCellClass)_cell).colSpan > 1)
                                {
                                    _indicadorContinue2 = false;
                                    break;
                                }

                                switch (_indexCol)
                                {
                                    case 1: _key = _cell.innerText; break;
                                    case 2: _value = _cell.innerText;
                                        _dProperties.Add(_key, _value);
                                        break;
                                }

                                if (_key.Equals("Name") && !String.IsNullOrEmpty(_value)) //Obtengo el Tipo de Objeto
                                {
                                    int _posFin = _cell.innerHTML.IndexOf(".bmp");
                                    string _txtIMG = "";

                                    if (_posFin > 10)
                                    {
                                        _txtIMG = _cell.innerHTML.Substring(0, _posFin);
                                        string[] _typesN = _txtIMG.Split("/".ToCharArray());
                                        if (_typesN.Length > 0)
                                        {
                                            _gxType = _typesN[_typesN.Length - 1].Replace(".bmp","");
                                        }
                                    }
                                    else
                                    {
                                        int _posIni = _cell.innerHTML.IndexOf("/");
                                        if (_posFin > 0)
                                        {
                                            _gxType = _cell.innerHTML.Substring(_posIni + 1, (_posFin - (_posIni + 1)));
                                        }
                                    }


                                    
                                }


                            }

                            if (!_indicadorContinue2)
                            {
                                break;
                            }

                        }

                        break;
                    }
                }
            }

            object _objret = Activator.CreateInstance(System.Type.GetType(String.Format("gxViewer.GxObject.{0}", _gxType)));

            _gxObject = _objret as GxObject.GXObj;

            GxObject.Info _info = new gxViewer.GxObject.Info();
            GxObject.KMW _kmw = new gxViewer.GxObject.KMW();
            GxObject.Model _model = new gxViewer.GxObject.Model();

            _kmw.Path = "DLA"; //Para Identificar que de DLYA
            _model.Id = "2";
            _model.Name = "Bantotal RPG/400";
            _model.Type = "Production";

            foreach (KeyValuePair<string, string> _prop in _dProperties)
            {
                switch (_prop.Key)
                {
                    case "Name": _info.Name = _prop.Value.Trim(); break;
                    case "Description": _info.Description = _prop.Value.Trim(); break;
                    case "Folder": _info.Folder = _prop.Value; break;
                    //case "Id": _model.Id = _prop.Value; break;
                    //case "Created": _gxObject. = _prop.Value; break;
                    case "Updated": _gxObject.LastUpdate = _prop.Value; break;
                    //case "Consolidated": _gxObject. = _prop.Value; break;
                    default:
                        break;
                }

            }
            _gxObject.KMW = _kmw;
            _gxObject.Info = _info;
        }
        private static void SetPropertyForHTML2(ref GxObject.GXObj _gxObject, string _HtmlElem)
        {
            
            Dictionary<string, string> _dProperties = new Dictionary<string, string>();
            _gxType = String.Empty;

            mshtml.IHTMLDocument2 _FirstDoc = (mshtml.IHTMLDocument2)(new mshtml.HTMLDocument());
            _FirstDoc.write(_HtmlElem);

            mshtml.HTMLDocument _FirstD = (mshtml.HTMLDocument)_FirstDoc;

            foreach (mshtml.IHTMLElement _Div in _FirstD.getElementsByTagName("DIV"))
            {
                if (_Div.className.Contains("ibox-title"))
                {
                    string[] _titulo = _Div.innerText.Trim().Split(":".ToCharArray());
                    _dProperties.Add("Name", _titulo[_titulo.Length-1]);
                    continue;
                }

                if (_Div.className.Contains("ibox-content"))
                {
                    string[] _props = _Div.innerText.Split("\n".ToCharArray());
                    for (int i = 0; i < _props.Length; i++)
                    {
                        if (!String.IsNullOrEmpty(_props[i]))
                        {
                            string[] _prps = _props[i].Trim().Split(":".ToCharArray());
                            if (_prps.Length > 1)
                            {
                                _dProperties.Add(_prps[0].Trim(), _prps[1].Trim());
                            }
                            
                        }
                    }
                    break;
                }
            }

            SetValuesProperties(ref _gxObject, _dProperties);
        }

        private static void SetValuesProperties(ref GxObject.GXObj _gxObject, Dictionary<string, string> _dProperties)
        {
            string _Name = "_GX";
            if (_dProperties.ContainsKey("Name"))
            {
                _Name = _dProperties["Name"].Trim();
            }
            string _strSubtype = _Name.Substring(0, 1);
            switch (_strSubtype.ToUpper())
            {
                case "P": _gxType = "Procedure"; break;
                case "W": _gxType = "WorkPanel"; break;
                case "R": _gxType = "Report"; break;
                case "H": _gxType = "WebPanel"; break;
                default:
                    _gxType = "GXObj";
                    break;
            }

            object _objret = Activator.CreateInstance(System.Type.GetType(String.Format("gxViewer.GxObject.{0}", _gxType)));

            _gxObject = _objret as GxObject.GXObj;

            GxObject.Info _info = new gxViewer.GxObject.Info();
            GxObject.KMW _kmw = new gxViewer.GxObject.KMW();
            GxObject.Model _model = new gxViewer.GxObject.Model();

            _kmw.Path = "DLA"; //Para Identificar que de DLYA
            _model.Id = "2";
            if (_dProperties.ContainsKey("Modelo"))
            {
                _model.Name = _dProperties["Modelo"];
                _kmw.Path = _model.Name;
            }
            else
            {
                _model.Name = "Bantotal RPG/400";
            }
            _model.Type = "Production";

            foreach (KeyValuePair<string, string> _prop in _dProperties)
            {
                switch (_prop.Key)
                {
                    case "Name": _info.Name = _prop.Value.Trim().Substring(1); break;
                    case "Descripción": _info.Description = _prop.Value.Trim(); break;
                    //case "Id": _model.Id = _prop.Value; break;
                    //case "Fecha de Alta": _gxObject. = _prop.Value; break;
                    case "Última fecha de modificación": _gxObject.LastUpdate = _prop.Value; break;
                    case "Fecha de Alta": _gxObject.CreationDate = _prop.Value; break;
                    //case "Consolidated": _gxObject. = _prop.Value; break;
                    default:
                        break;
                }

            }
            _gxObject.KMW = _kmw;
            _gxObject.Model = _model;
            _gxObject.Info = _info;
        }



        private static void SetRulesForHTML(ref GxObject.GXObj _gxObject, mshtml.IHTMLElement _elem)
        {
            string _Rules = _elem.innerText; //_xDoc.documentElement.innerText;
            _gxObject.Rules = _Rules;
        }

        private static void SetRulesForHTML2(ref GxObject.GXObj _gxObject, mshtml.IHTMLElement _elem)
        {
            mshtml.IHTMLDocument2 _xDocHTML = (mshtml.IHTMLDocument2)(new mshtml.HTMLDocument());
            _xDocHTML.write(_elem.outerHTML);
            mshtml.HTMLDocument _FirstD = (mshtml.HTMLDocument)_xDocHTML;
            string _htmlRules = _elem.innerText;

            foreach (mshtml.IHTMLElement _Div in _FirstD.getElementsByTagName("DIV"))
            {
                var _attrCargado = _Div.getAttribute("ng-show");

                if (_attrCargado!=null)
                {
                    if (_attrCargado.ToString().Equals("codigoCargado"))
                    {
                        _htmlRules = _Div.innerText;

                        mshtml.IHTMLDocument2 _xDocHTMLX = (mshtml.IHTMLDocument2)(new mshtml.HTMLDocument());
                        _xDocHTMLX.write(_Div.outerHTML);
                        mshtml.HTMLDocument _FirstDx = (mshtml.HTMLDocument)_xDocHTMLX;
                        foreach (mshtml.IHTMLElement _spanHTML in _FirstD.getElementsByTagName("span"))
                        {
                            if (_spanHTML.innerText != null)
                            {
                                _htmlRules = _htmlRules.Replace(_spanHTML.innerText, String.Empty);
                            }
                        }
                        _htmlRules = _htmlRules.Replace("\r\n", "");


                        _htmlRules = _htmlRules.Replace(";", ";\r\n");

                    }
                }
                
            }

            _gxObject.Rules = _htmlRules;
        }

        private static void SetEventsForHTML(ref GxObject.GXObj _gxObject, mshtml.IHTMLElement _elem)
        {
            string _Events = _elem.innerText;
            if (_gxObject.Source != null)
            {
                if (_gxObject.Source.Length > 0)
                {
                    _gxObject.Source += "\n" + _Events;
                }
                else
                {
                    _gxObject.Source = _Events;
                }
            }
            else
            {
                _gxObject.Source = _Events;
            }
            

        }

        public static mshtml.IHTMLElementCollection getElementsChildren(mshtml.IHTMLElement pElem, string pTagname)
        {
            //mshtml.IHTMLDocument2 _xNEWHTML = (mshtml.IHTMLDocument2)(new mshtml.HTMLDocument());
            //_xNEWHTML.write(pElem.innerHTML);
            mshtml.HTMLDocument _docHtm = getDocument(pElem.innerHTML);
            if (_docHtm != null)
            {
                if (string.IsNullOrEmpty(pTagname))
                {
                    return _docHtm.all;
                }
                else
                {
                    return _docHtm.getElementsByTagName(pTagname);
                }
            }
            else
            {
                return null;
            }
        }


        public static mshtml.HTMLDocument getDocument(string _html)
        {
            mshtml.IHTMLDocument2 _xNEWHTML = (mshtml.IHTMLDocument2)(new mshtml.HTMLDocument());
            _xNEWHTML.write(_html);
            mshtml.HTMLDocument _docHtm = (mshtml.HTMLDocument)_xNEWHTML;
            if (_docHtm != null)
            {
                return _docHtm;
            }
            else
            {
                return null;
            }
        }




        private static void SetEventsForHTML2(ref GxObject.GXObj _gxObject, mshtml.IHTMLElement _elem)
        {
            string _Events = _elem.innerText;

            mshtml.IHTMLDocument2 _xDocHTML = (mshtml.IHTMLDocument2)(new mshtml.HTMLDocument());
            _xDocHTML.write(_elem.outerHTML);
            mshtml.HTMLDocument _FirstD = (mshtml.HTMLDocument)_xDocHTML;

            foreach (mshtml.IHTMLElement _Div in _FirstD.getElementsByTagName("DIV"))
            {
                var _attrCargado = _Div.getAttribute("ng-show");

                if (_attrCargado != null)
                {
                    if (_attrCargado.ToString().Equals("codigoCargado"))
                    {
                        _Events = _Div.innerText;
                        break;
                        //mshtml.IHTMLDocument2 _xDocHTMLX = (mshtml.IHTMLDocument2)(new mshtml.HTMLDocument());
                        //_xDocHTMLX.write(_Div.outerHTML);
                        //mshtml.HTMLDocument _FirstDx = (mshtml.HTMLDocument)_xDocHTMLX;
                        //foreach (mshtml.IHTMLElement _spanHTML in _FirstD.getElementsByTagName("span"))
                        //{
                        //    _Events = _Events.Replace(_spanHTML.innerText, String.Empty);
                        //}
                        ////_htmlRules = _htmlRules.Replace("\r\n", "");
                    }
                }

            }

            

            if (_gxObject.Source != null)
            {
                if (_gxObject.Source.Length > 0)
                {
                    _gxObject.Source += "\n" + _Events;
                }
                else
                {
                    _gxObject.Source = _Events;
                }
            }
            else
            {
                _gxObject.Source = _Events;
            }


        }

        private static void SetSubRoutinesForHTML(ref GxObject.GXObj _gxObject, mshtml.IHTMLElement _elem)
        {
            string _SubRoutines = _elem.innerText;
            _gxObject.Subroutines = _SubRoutines;
        }
        private static void SetConditionsForHTML(ref GxObject.GXObj _gxObject, mshtml.IHTMLElement _elem)
        {
            string _Conditions = _elem.innerText;
            _gxObject.Conditions = _Conditions;

        }
        private static void SetDocumForHTML(ref GxObject.GXObj _gxObject, mshtml.IHTMLElement _elem)
        {
            string _strDocum = _elem.innerHTML;
            GxObject.Documentation _doc = new gxViewer.GxObject.Documentation();
            _doc.Source = _strDocum;
            _gxObject.Documentation = _doc;

        }
        private static void SetVariablesForHTML(ref GxObject.GXObj _gxObject, mshtml.IHTMLElement _elem)
        {
            mshtml.IHTMLDocument2 _dh = (mshtml.IHTMLDocument2)(new mshtml.HTMLDocument());
            _dh.write(_elem.outerHTML);
            mshtml.HTMLDocument _d = (mshtml.HTMLDocument)_dh;

            mshtml.IHTMLElementCollection _tbls = _d.getElementsByTagName("TABLE");
            
            List<GxObject.Variable> _vars = new List<gxViewer.GxObject.Variable>();


            DataTable _dtVars = new DataTable("variables");
            _dtVars.Columns.Add("Name");
            _dtVars.Columns.Add("Description");
            _dtVars.Columns.Add("Type");
            _dtVars.Columns.Add("Length");
            _dtVars.Columns.Add("Decimals");
            _dtVars.Columns.Add("Picture");

            foreach (mshtml.IHTMLElement _t in _tbls)
            {
                mshtml.HTMLTableClass _table = (mshtml.HTMLTableClass)_t;

                int _indexrow = 0;
                
                foreach (mshtml.IHTMLElement _row in _table.rows)
                {
                    mshtml.HTMLTableRowClass _r = (mshtml.HTMLTableRowClass)_row;
                    if (_indexrow > 0) //cabecera
                    {
                        int _col = 0;
                        DataRow _rVar = _dtVars.NewRow();
                        foreach (mshtml.IHTMLElement _c  in _r.cells)
                        {
                            _rVar[_col] = _c.innerText;
                            _col++;
                        }
                        _dtVars.Rows.Add(_rVar);

                    }
                    _indexrow++;
                }                
            }


            foreach (DataRow _r in _dtVars.Rows)
            {
                GxObject.Variable _v = new gxViewer.GxObject.Variable();
                _v.Name = _r["Name"].ToString();
                _v.Title = _r["Description"].ToString();
                _v.Type = _r["Type"].ToString();
                _v.Length = int.Parse( _r["Length"].ToString());
                _v.Decimals = ( String.IsNullOrEmpty( _r["Decimals"].ToString() ) ? 0 : int.Parse(_r["Decimals"].ToString()) );
                _v.Picture = _r["Picture"].ToString();

                _vars.Add(_v);
            }
            _gxObject.Variables = _vars.ToArray();

        }

        public static GxObject.GXObj ConvertXML(string _RutaHTML)
        {
           //    Dim xmldoc As New Xml.XmlDocumentxmldoc.Load("your filename here")
            string _html = System.IO.File.ReadAllText(_RutaHTML, Encoding.Default);
            if (_html.IndexOf("ng-app") > 0)
            {
                if (_html.IndexOf("<code id=") > 0)
                {
                    return ConvertXML3(_RutaHTML);
                }
                else 
                {
                    return ConvertXML2(_RutaHTML);
                }    
            }
            
            mshtml.IHTMLDocument2 _docxHtml = (mshtml.IHTMLDocument2)(new mshtml.HTMLDocument());
            _docxHtml.clear();
            _docxHtml.write(_html);
            mshtml.HTMLDocument _doc = (mshtml.HTMLDocument)_docxHtml;

            GxObject.GXObj _gxObject = new gxViewer.GxObject.GXObj();
            Dictionary<string, string> _dProperties = new Dictionary<string, string>();

            _gxObject.XMLObj = _html;

            mshtml.IHTMLElementCollection _Divs = _doc.getElementsByTagName("DIV");
            string _NameObject = String.Empty;
            int _IndicadorContinue = 0;
            foreach (mshtml.IHTMLElement _elem in _Divs)
            {
                bool _continue = false;

                if (_elem.id != null )
                {
                    if (String.IsNullOrEmpty(_NameObject))
                    {
                        _NameObject = _elem.id;
                        _continue = false;
                    }

                    switch (_elem.id)
                    {
                        case "wholeNugget":
                        case "formPart":
                        case "layoutPart":
                        case "htmlPart":
                            _continue = false;
                            break;
                        case "content":
                            _continue = true;
                            _IndicadorContinue++;
                            break;

                    }
                    
                }

                if (_continue)
                {

                    if (_IndicadorContinue == 1) //Solo la primera vez
                    {
                        SetPropertyForHTML(ref _gxObject, _elem);
                        _IndicadorContinue++;
                    }
                    else
                    {

                        //cargamos las Reglas
                        //parent rule
                        switch (_elem.parentElement.id)
                        {
                            case "formPart":   //FORM
                            case "layoutPart": //LAYOUT
                                SetEventsForHTML(ref _gxObject, _elem);
                                break;

                            case "htmlPart":   //WEB FORM
                                break;

                            case "rule":   //Reglas
                                SetRulesForHTML(ref _gxObject, _elem);
                                break;

                            case "evt":  //Eventos
                                SetEventsForHTML(ref _gxObject, _elem);
                                break;

                            case "subs":  //Subrutinas
                                SetSubRoutinesForHTML(ref _gxObject, _elem);
                                break;

                            case "cond":  //Condiciones
                                SetConditionsForHTML(ref _gxObject, _elem);
                                break;
                            
                            case "var":  //Variables
                                SetVariablesForHTML(ref _gxObject, _elem);
                                break;

                            case "doc":  //Documentacion
                                SetDocumForHTML(ref _gxObject, _elem);
                                break;
                            
                            default:
                                break;
                        }


                       
                        
                    }
                }

            }

            _gxObject.IsXML = false;
            _gxObject.XMLObj = _html;

            return _gxObject;

        
        }

        public static GxObject.GXObj ConvertXML2(string PathHTML)
        {
            string _html = System.IO.File.ReadAllText(PathHTML, Encoding.UTF8);
            
            mshtml.IHTMLDocument2 _docxHtml = (mshtml.IHTMLDocument2)(new mshtml.HTMLDocument());
            _docxHtml.clear();
            _docxHtml.write(_html);
            mshtml.HTMLDocument _doc = (mshtml.HTMLDocument)_docxHtml;

            GxObject.GXObj _gxObject = new gxViewer.GxObject.GXObj();
            Dictionary<string, string> _dProperties = new Dictionary<string, string>();
            
            mshtml.IHTMLElementCollection _Divs = _doc.getElementsByTagName("DIV");
            string _NameObject = String.Empty;
            int _IndicadorContinue = 0;

            mshtml.IHTMLElement _LastElem = null;
            int _count = 0;
            int _lascount = 0;
            string _bloque = String.Empty;

            foreach (mshtml.IHTMLElement _elem in _Divs)
            {
                Console.WriteLine($"Name:{_elem.id} - Class:{_elem.className}");
                bool _continue = false;

                if (_elem.className != null)
                {
                    _count++;

                    if (_elem.className.IndexOf("ibox-title") >= 0)
                    {
                        _LastElem = _elem;
                        _lascount = _count;
                    }
                    
                    if (_elem.className.IndexOf("ibox-content") >= 0)
                    {
                        if (_LastElem != null && (_lascount+1) == _count)
                        {
                            _IndicadorContinue = 1;
                            _continue = true;
                            _bloque = "Header";
                        }

                        string attr = _elem.getAttribute("ng-class").ToString().Trim();
                        if (attr.Contains("Rules"))
                        {
                            _bloque = "rule";
                            _continue = true;
                        }
                        else
                        {
                            if (_elem.innerText != null)
                            {
                                if (_elem.innerText.Trim().Length > 0)
                                {
                                    _bloque = "evt";
                                    _continue = true;
                                }
                            }
                            
                        }

                        
                    }
                    
                }

                if (_continue)
                {

                    if (_IndicadorContinue == 1) //Solo la primera vez
                    {
                        string _htmlElement = _LastElem.outerHTML + _elem.outerHTML;
                        SetPropertyForHTML2(ref _gxObject, _htmlElement);
                        _LastElem = null;
                        _IndicadorContinue++;
                    }
                    else
                    {
                        switch (_bloque)
                        {
                            case "layoutPart": //LAYOUT
                                SetEventsForHTML2(ref _gxObject, _elem);
                                break;

                            case "rule":   //Reglas
                                SetRulesForHTML2(ref _gxObject, _elem);
                                break;

                            case "evt":  //Eventos
                                SetEventsForHTML2(ref _gxObject, _elem);
                                break;

                            case "subs":  //Subrutinas
                                SetSubRoutinesForHTML(ref _gxObject, _elem);
                                break;

                            case "cond":  //Condiciones
                                SetConditionsForHTML(ref _gxObject, _elem);
                                break;

                            case "var":  //Variables
                                SetVariablesForHTML(ref _gxObject, _elem);
                                break;

                            case "doc":  //Documentacion
                                SetDocumForHTML(ref _gxObject, _elem);
                                break;

                            default:
                                break;
                        }
                        
                    }
                }

            }
            _gxObject.IsXML = false;
            _gxObject.XMLObj = _html;
            return _gxObject;
        }

        public static GxObject.GXObj ConvertXML3(string PathHTML)
        {
            string _html = System.IO.File.ReadAllText(PathHTML, Encoding.UTF8);

            mshtml.IHTMLDocument2 _docxHtml = (mshtml.IHTMLDocument2)(new mshtml.HTMLDocument());
            _docxHtml.clear();
            _docxHtml.write(_html);
            mshtml.HTMLDocument _doc = (mshtml.HTMLDocument)_docxHtml;

            GxObject.GXObj _gxObject = new gxViewer.GxObject.GXObj();
            Dictionary<string, string> _dProperties = new Dictionary<string, string>();

            mshtml.IHTMLElementCollection _Divs = _doc.getElementsByTagName("code");
            
            string _bloque = String.Empty;


            /*Obtener nombre del Objeto*/
            mshtml.IHTMLElementCollection _HXs = _doc.getElementsByTagName("h3");
            foreach (mshtml.IHTMLElement _elem in _HXs)
            {
                if (_elem.innerHTML.Contains("ng-binding"))
                {
                    string _texto = _elem.innerText.Replace("\n","").Replace("\r","").Trim();
                    string[] _texts = _texto.Split(' ');
                    string _NameObject = _texts[_texts.Length - 1];
                    _dProperties.Add("Name", _NameObject);
                    break;
                }
            }
            if (!_dProperties.ContainsKey("Name"))
            {
                _HXs = _doc.getElementsByTagName("h2");
                foreach (mshtml.IHTMLElement _elem in _HXs)
                {
                    if (_elem.innerHTML.Contains("ng-binding"))
                    {
                        string _texto = _elem.innerText.Replace("\n", "").Replace("\r", "").Trim();
                        string[] _texts = _texto.Split(' ');
                        string _NameObject = _texts[_texts.Length - 1];
                        _dProperties.Add("Name", _NameObject.Trim());
                        break;
                    }
                }
            }


            /*Obtener Propiedades*/
            _HXs = _doc.getElementsByTagName("h5");
            foreach (mshtml.IHTMLElement _elem in _HXs)
            {
                if (_elem.outerHTML.Contains("ng-binding"))
                {
                    string _texto = _elem.innerText.Replace("\n", "").Replace("\r", "").Trim();
                    string[] _texts = _texto.Split(':');
                    if (_texts.Length == 2)
                    {
                        _dProperties.Add(_texts[0], _texts[1].Trim());
                    }
                    if (_texts.Length == 3)
                    {
                        _dProperties.Add(_texts[0], _texts[1].Trim() + ":" + _texts[2].Trim());
                    }
                }
            }
            if (_dProperties.Count > 0)
            {
                SetValuesProperties(ref _gxObject, _dProperties);
            }
            else
            {
                //Colocar Modelo y Valores por default
                System.IO.FileInfo _finfo = new System.IO.FileInfo(PathHTML);
                _gxObject.Model = new Model() { Name = "DLYA" };
                _gxObject.Info = new GxObject.Info() {  Name= _finfo.Name, Description= _finfo .FullName} ;
            }


            _Divs = _doc.getElementsByTagName("code");
            List<GxObject.Variable> _vars = new List<gxViewer.GxObject.Variable>();
            List<GxObject.GXAtt> _atributtes = new List<gxViewer.GxObject.GXAtt>();

            foreach (mshtml.IHTMLElement _elem in _Divs)
            {
                /*REGLAS*/
                if (_elem.id != null && _elem.id.StartsWith("parametros"))
                {
                    string _rulesBlock = String.Empty;
                    

                    if (_elem.outerText != null)
                    {
                        string _htmlRules = _elem.outerHTML;

                        GxDefinitionClass _def = new GxDefinitionClass() { IniBracket = "<span", EndBracket = "</span>" };
                        foreach (xPosition xpos in Functions.GetPositionsByString(_htmlRules, _def.IniBracket, _def.EndBracket, true))
                        {
                            Console.WriteLine(xpos.ContentText);
                            _htmlRules = _htmlRules.Replace(xpos.ContentText, "");
                        }

                        mshtml.HTMLDocument _docRules = getDocument(_htmlRules);
                        mshtml.IHTMLElementCollection _TagCodes = _docRules.getElementsByTagName("code");
                        
                        Console.WriteLine(_TagCodes.length);

                        StringBuilder sb = new StringBuilder();
                        string _Code_and_comments = String.Empty;
                        List<string> _Listcomments = new List<string>();
                        int _index = 0;

                        foreach (mshtml.IHTMLElement _xcode in _TagCodes)
                        {
                            if (_xcode.innerText != null)
                            {
                                if (_index.Equals(0))
                                {
                                    _Code_and_comments = _xcode.innerText;
                                    _index++;
                                    continue;
                                }

                                if (_xcode.className != null && _xcode.className.Contains("special-comment"))
                                {
                                    _Listcomments.Add(_xcode.innerText);
                                }
                            }
                        }

                        try
                        {
                            /*Acomodamos las lineas de codigo con los comentarios*/
                            string _splitChars = "//";
                            foreach (string l in _Listcomments)
                            {
                                string[] _comen = l.Split(_splitChars.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                string _replcomm = (_Code_and_comments.StartsWith(_splitChars) ? "" : "\n") + _splitChars + String.Join("\n" + _splitChars, _comen) + "\n";
                                _Code_and_comments = _Code_and_comments.Replace(l, _replcomm);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);

                        }
                        finally
                        {
                            sb.Append(_Code_and_comments);
                        }
                        
                        
                        _rulesBlock = sb.ToString();

                    }

                    _gxObject.Rules = _rulesBlock;
                }


                /*SOURCE*/
                //disrep
                if (_elem.id == null && _elem.parentElement.id!= null && !_elem.parentElement.id.Equals("parametros"))
                {
                    string _codigo = "";
                    if (_elem.parentElement != null && _elem.parentElement.id.Equals("codigo"))
                    {
                        if (_elem.parentElement.parentElement != null)
                        {
                            _codigo = _elem.parentElement.parentElement.outerText;
                        }
                        else
                        {
                            _codigo = _elem.parentElement.outerText;
                        }
                    }

                    if (!String.IsNullOrEmpty(_codigo))
                    {
                        _gxObject.Source = _codigo;
                        break;
                    }
                }

                //Chuchaaa... no trae Source, Aplicamos otra tecnica
                if (String.IsNullOrEmpty(_gxObject.Source))
                {
                    _Divs = _doc.getElementsByTagName("pre");
                    foreach (mshtml.IHTMLElement _pre in _Divs)
                    {
                        if (_pre.id != null && _pre.id.Equals("codigo") && _pre.className.Contains("genexusCode"))
                        {
                            _gxObject.Source = _pre.outerText;
                            break;
                        }
                    }
                }



                Console.WriteLine($"Name:{_elem.id} - Class:{_elem.className}  => \n{_elem.parentElement.id}\n\n");

                /*Variables*/
                var _vars_tags = getElementsChildren(_elem, "span");
                foreach (mshtml.IHTMLElement _xel in _vars_tags)
                {
                    if (_xel.className != null)
                    {
                        string _classNameVar = _xel.className;
                        if (_classNameVar.Contains("tooltipvar"))
                        {
                            Variable _var = new Variable();
                            _var.Name = _classNameVar.Replace("tooltipCustom","").Replace("tooltipvar","").Replace(" top ","").Trim();
                            if (_vars.Find( v => v.Name == _var.Name) != null)
                            {
                                continue;
                            }

                            if (_xel.innerText == null)
                            {
                                continue;
                            }

                            string[] _str_props = _xel.innerText.Trim().Split('\n');
                            foreach (var strProp in _str_props)
                            {
                                string[] _texts = strProp.Split(':');
                                string _key="", _value = "";
                                if (_texts.Length >= 2)
                                {
                                    _key = _texts[0].Trim();
                                    _value = _texts[1].Trim();                                    
                                }

                                if (!String.IsNullOrEmpty(_key))
                                {
                                    switch (_key)
                                    {
                                        case "Descripción": _var.Title = _value; break;
                                        case "Tipo": _var.Type = _value; break;
                                        case "Picture": _var.Picture = _value; break;
                                        case "Basada en": _var.BasedOn = _value; break;
                                        case "Largo": _var.Length = int.Parse(_value); break;
                                        case "Decimales": 
                                        case "Decimals": 
                                            _var.Decimals = int.Parse(_value); 
                                            break;
                                        default:
                                            break;
                                    }
                                }

                                //Console.WriteLine(strProp);
                            }

                            if (!String.IsNullOrEmpty( _var.BasedOn))
                            {
                                _atributtes.Add(new GXAtt() 
                                  { Attribute = new GxObject.Attribute() { Name= _var.BasedOn, 
                                                                           Title=_var.Title, 
                                                                           Length=_var.Length 
                                                                         } , 
                                  });
                            }

                            _vars.Add(_var);

                        }
                        
                    }

                }

            }


            if (_vars.Count > 0)
            {
                _gxObject.Variables = _vars.ToArray();
            }

            if (_atributtes.Count > 0)
            {
                _gxObject.GXAtts = _atributtes.ToArray();
            }

            _gxObject.IsXML = false;
            _gxObject.XMLObj = _html ;
            return _gxObject;
        }

    }
}
