using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Reflection;
using gxViewer.GxObject;
using System.IO;

namespace gxViewer.XPWConverter
{
    public class XPWReader
    {
        Dictionary<string, object> _listObjGX = new Dictionary<string, object>();
        private List<string> _ListpropsGX = new List<string>(); //Functions.getListObjectsGX();

        List<GxDefinitionClass> _listPropsGXobj = new List<GxDefinitionClass>();
        Dictionary<string, string> _listFixNoClosed = new Dictionary<string, string>();

        public XPWReader()
        {
            this.getObjects();
            _listFixNoClosed.Add("Bold", "Y");
            _listFixNoClosed.Add("FromDefault", "Y");
            _listFixNoClosed.Add("FromStyle", "Y");
            _listFixNoClosed.Add("Underline", "Y");
            _listFixNoClosed.Add("TextMode", "Y");
            //_listFixNoClosed.Add("Bold", "Y");

        }
        public bool ConvertToXML(string pFileNameInput, string pFileNameOutput)
        {
            string pStringXPW = System.IO.File.ReadAllText(pFileNameInput, Encoding.Default);
            StringBuilder _strb = new StringBuilder();

            /*Codigo para parchar el XPW al inicio*/

            List<GxDefinitionClass> _lstdefRep = new List<GxDefinitionClass>()
            {
                new GxDefinitionClass(){ IniBracket=":FormInfo\r\n", EndBracket=":eFormInfo\r\n", 
                                            ListProperties=new List<GxDefinitionClassProperties>()
                                            {
                                                new GxDefinitionClassProperties(){ IniBracket=":Att:Font\r\n", EndBracket=":eFont\r\n", ReplaceWith="Att_Font" },
                                                new GxDefinitionClassProperties(){ IniBracket=":Txt:Font\r\n", EndBracket=":eFont\r\n", ReplaceWith="Txt_Font" },
                                                new GxDefinitionClassProperties(){ IniBracket=":Tit:Font\r\n", EndBracket=":eFont\r\n", ReplaceWith="Tit_Font" }
                                            }
                                        },

                new GxDefinitionClass(){ IniBracket=":SubFile\r\n", EndBracket=":eSubFile\r\n",
                                            ListProperties=new List<GxDefinitionClassProperties>()
                                            {
                                                new GxDefinitionClassProperties(){ IniBracket=":Att:Font\r\n", EndBracket=":eFont\r\n", ReplaceWith="Att_Font" },
                                                new GxDefinitionClassProperties(){ IniBracket=":Txt:Font\r\n", EndBracket=":eFont\r\n", ReplaceWith="Txt_Font" },
                                                new GxDefinitionClassProperties(){ IniBracket=":Tit:Font\r\n", EndBracket=":eFont\r\n", ReplaceWith="Tit_Font" }
                                            }
                                        },

                new GxDefinitionClass(){ IniBracket=":Item\r\n", EndBracket=":eItem\r\n",
                                            ListProperties=new List<GxDefinitionClassProperties>()
                                            {
                                                new GxDefinitionClassProperties(){ IniBracket=":Title:ColorInfo\r\n", EndBracket=":eColorInfo\r\n", ReplaceWith="Title_ColorInfo" },
                                                new GxDefinitionClassProperties(){ IniBracket=":Rows:ColorInfo\r\n", EndBracket=":eColorInfo\r\n", ReplaceWith="Rows_ColorInfo" }
                                            }
                                        }
            };
            foreach (GxDefinitionClass _def in _lstdefRep)
            {
                foreach (xPosition xpos in Functions.GetPositionsByString(pStringXPW, _def.IniBracket, _def.EndBracket, true))
                {
                    string _strobjtxt = xpos.ContentText;
                    foreach (GxDefinitionClassProperties _defprop in _def.ListProperties)
                    {
                        foreach (xPosition xpos2 in Functions.GetPositionsByString(_strobjtxt, _defprop.IniBracket, _defprop.EndBracket, true))
                        {
                            string _propRep = xpos2.ContentText.Replace(_defprop.IniBracket, _defprop.getRepIniBracket()).Replace(_defprop.EndBracket, _defprop.getRepEndBracket());
                            _strobjtxt = _strobjtxt.Replace(xpos2.ContentText, _propRep);
                        }
                    }
                    pStringXPW = pStringXPW.Replace(xpos.ContentText, _strobjtxt);
                }
            }


            string _ini , _fin , _iniXML, _finXML ;

            foreach (GxDefinitionClass _gxdc in _listPropsGXobj)
            {
                string _strProp = _gxdc.Name;

                //if (_strProp.Equals("Bold"))  //Solo para DEBUG
                {
                    Console.WriteLine("Propery: " + _strProp);
                }

                if (_gxdc.IsMainObject)
                {
                    _ini = String.Format(":{0}\r\n", _strProp);
                    _fin = String.Format(":e{0}\r\n", _strProp);
                    _iniXML = String.Format("<{0}>\r\n", _strProp);
                    _finXML = String.Format("</{0}>\r\n", _strProp);
                }
                else
                {
                    _ini = String.Format(" :{0}\r\n", _strProp);
                    _fin = String.Format(" :e{0}\r\n", _strProp);
                    _iniXML = String.Format(" <{0}>\r\n", _strProp);
                    _finXML = String.Format(" </{0}>\r\n", _strProp);
                }
                
                bool _changeTXTCDATA = false;

                if (_strProp.Equals("Source") || _strProp.Equals("Rules") || _strProp.Equals("GxCondition") || _strProp.Equals("GxCondOrder") || _strProp.Equals("Events") || _strProp.Equals("Subroutines") || _strProp.Equals("WebProperties") || _strProp.Equals("Conditions"))
                {
                    _iniXML = String.Format("<{0}><![CDATA[", _strProp);
                    _finXML = String.Format("]]></{0}>\r\n", _strProp);
                    _changeTXTCDATA = true;
                }

                string _strtmp = pStringXPW.Replace(_ini, _iniXML).Replace(_fin, _finXML);

                if (!pStringXPW.Equals(_strtmp))
                {
                    _gxdc.ChangeStringDef = (_changeTXTCDATA) ? false : true;
                }

                pStringXPW = _strtmp;
            }

            string _kmw = String.Empty;
            string _model = String.Empty;

            //Ahora recorremos los atributos clases, para cambiarlos
            foreach (GxDefinitionClass _gxdc in _listPropsGXobj)
            //foreach (string _propChanged in _listPropsGXobj)
            {
                //Ahora vamos por las propiedades
                if (_gxdc.ChangeStringDef)
                {
                    _ini = String.Format("<{0}>", _gxdc.Name);
                    _fin = String.Format("</{0}>", _gxdc.Name);

                    List<xPosition> _lsgxPos = Functions.GetPositionsByString(pStringXPW, _ini, _fin, true);
                    foreach (xPosition xpos in _lsgxPos)
                    {
                        string _txtObject = xpos.ContentText;
                        foreach (GxDefinitionClassProperties _gxprop in _gxdc.ListProperties)
                        {
                            if (!_gxprop.IsPropGXObject)
                            {
                                _ini = String.Format(" {0}=", _gxprop.PropName);
                                _fin = "\r\n";

                                //string _blockTXT = xpos.ContentText;
                                List<xPosition> _litemgxPos = Functions.GetPositionsByString(_txtObject, _ini, _fin, true);

                                if (_litemgxPos.Count.Equals(0))
                                {
                                    _ini = String.Format("{0}=", _gxprop.PropName);
                                    _litemgxPos = Functions.GetPositionsByString(_txtObject, _ini, _fin, true);


                                    if (_litemgxPos.Count.Equals(0))
                                    {
                                        if (_listFixNoClosed.ContainsKey(_gxprop.PropName))
                                        {
                                            _ini = String.Format("{0}", _gxprop.PropName);
                                            _litemgxPos = Functions.GetPositionsByString(_txtObject, _ini, _fin, true);
                                            Console.Write("ERROR => " + $"{_gxdc.Name} - {_gxdc.TypeName} | {_gxprop.PropName}");
                                        }
                                        
                                    }
                                }

                                if (_gxprop.PropName.Equals("Source") || _gxprop.PropName.Equals("Text") || _gxprop.PropName.Equals("Doc") )
                                {
                                    _iniXML = String.Format(" <{0}><![CDATA[", _gxprop.PropName);
                                    _finXML = String.Format("]]></{0}>\r\n", _gxprop.PropName);

                                }
                                else
                                {
                                    _iniXML = String.Format(" <{0}>", _gxprop.PropName);
                                    _finXML = String.Format("</{0}>\r\n", _gxprop.PropName);
                                }

                                foreach (xPosition itemxPos in _litemgxPos)
                                {

                                    string _strtmpprop = "";
                                    if (itemxPos.LengthTxt.Equals(0) && _listFixNoClosed.ContainsKey(_gxprop.PropName))
                                    {
                                        _strtmpprop = $"{_iniXML}{_listFixNoClosed[_gxprop.PropName]}{_finXML}"; 
                                    }
                                    else
                                    {
                                        _strtmpprop = itemxPos.ContentText.Replace(_ini, _iniXML).Replace(_fin, _finXML);
                                    }

                                    if (_gxprop.PropName.Equals("Row"))
                                    {
                                        if (!_strtmpprop.Contains("&amp;"))
                                        {
                                            _strtmpprop = _strtmpprop.Replace("&", "&amp;");
                                        }
                                    }

                                    _txtObject = _txtObject.Replace(itemxPos.ContentText, _strtmpprop);
                                }
                            }
                        }
                        pStringXPW = pStringXPW.Replace(xpos.ContentText, _txtObject);

                        if (_gxdc.Name.Equals("KMW"))
                        {
                            _kmw = _txtObject;
                        }

                        if (_gxdc.Name.Equals("Model"))
                        {
                            _model = _txtObject;
                        }

                    }
                    _lsgxPos = null;
                }
            }


            if (!pStringXPW.Contains("<ExportFile>"))
            {
                if (!pStringXPW.Contains("<?xml version="))
                {
                    _strb.AppendLine("<?xml version='1.0' encoding='iso-8859-1'?>");
                }

                KMW _kb = new KMW() {  MajorVersion=1, MinorVersion=0, Path="" };
                Model _mdl = new Model() {Id="2", Name="Genexus"};

                string _xmlKMV = Functions.Serialize<KMW>(_kb, true);
                string _xmlMdl = Functions.Serialize<Model>(_mdl, true);
                
                _strb.AppendLine("<ExportFile>");

                if (pStringXPW.Contains("<KMW>"))
                {
                    _strb.AppendLine(_kmw);
                    pStringXPW = pStringXPW.Replace(_kmw, String.Empty);
                }
                else
                {
                    _strb.AppendLine(_xmlKMV);
                }

                if (pStringXPW.Contains("<Model>"))
                {
                    _strb.AppendLine(_model);
                    pStringXPW = pStringXPW.Replace(_model, String.Empty);
                }
                else
                {
                    _strb.AppendLine(_xmlMdl);
                }

                _strb.AppendLine("<GXObject>");
                _strb.AppendLine(pStringXPW);
                _strb.AppendLine("</GXObject>");
                _strb.AppendLine("</ExportFile>");
            }
            else
            {
                _strb.Append(pStringXPW);
            }

            using (StreamWriter sw = new StreamWriter(pFileNameOutput, true, Encoding.Default))
            {
                {
                    sw.Write(_strb.ToString());
                }
                sw.Close();
            }

            return true;
        }
        
        private void getObjects()
        {
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (t.IsClass && (t.Namespace!=null && t.Namespace.Contains("GxObject")) && !t.IsAbstract)
                {
                    GxDefinitionClass _obj = new GxDefinitionClass();
                    _obj.Name = t.Name;

                    if (_obj.Name.ToUpper().Equals("TEXT"))
                    {
                        Console.WriteLine(_obj.Name);
                    }
                    _obj.TypeName = t.GetType().Name;
                    _obj.IsMainObject = t.IsSubclassOf(typeof(GXObj));
                    if (t.Name.Equals("Attribute") || t.Name.Equals("AttInfo") || t.Name.Equals("Folder") || t.Name.Equals("Doc") || t.Name.Equals("Model") || t.Name.Equals("KMW") || t.Name.Equals("Table"))
                    {
                        _obj.IsMainObject = true;
                    }

                    try
                    {
                        object _t = Activator.CreateInstance(t);
                        LoadProperties(ref _obj, Activator.CreateInstance(t));
                    }
                    catch (Exception ex )
                    {
                        Console.WriteLine(ex.Message);                        
                    }
                    _listPropsGXobj.Add(_obj);
                }                
            }
        }

        private void LoadProperties(ref GxDefinitionClass pObj, object pInstance)
        {
            pObj.ListProperties = new List<GxDefinitionClassProperties>();
            foreach (PropertyInfo prop in pInstance.GetType().GetProperties())
            {
                Type _t = prop.PropertyType;

                GxDefinitionClassProperties _propGXdef = new GxDefinitionClassProperties();
                _propGXdef.PropName = prop.Name;
                _propGXdef.PropTypeName = _t.Name;
                _propGXdef.IsArray = prop.PropertyType.IsArray;

                if (prop.PropertyType.GetElementType() != null)
                {
                    _t = prop.PropertyType.GetElementType();
                    _propGXdef.PropTypeName = _t.Name;
                }
                else
                {
                    if (_propGXdef.PropTypeName.Contains("Dictionary"))
                    {
                        PropertyInfo _mix = (PropertyInfo)prop.PropertyType.GetDefaultMembers()[0];  //((System.Reflection.RuntimePropertyInfo)).PropertyType.FullName
                        _t = _mix.PropertyType;
                        _propGXdef.PropTypeName = _t.Name;
                        _propGXdef.IsDictionary = true;
                    }
                }

                //Esto para salvar lso Text.text (Objeto PLC4P12)
                if (pInstance.GetType().Name.ToUpper() == _propGXdef.PropName.ToUpper())
                {
                    _propGXdef.PropName = pInstance.GetType().Name;
                }

                _propGXdef.IsPropGXObject = _t.Namespace.Contains("GxObject");

                pObj.ListProperties.Add(_propGXdef);

            }
        }


    }
}
