using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using gxViewer.GxObject;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using mshtml;
using System.Diagnostics;
using gxViewer.GxObject.SDT;
using static System.Net.WebRequestMethods;
using System.ComponentModel.Design;
using static System.Net.Mime.MediaTypeNames;
using gxViewer.Gx18;
using System.IO;

namespace gxViewer
{
    public class GxObjectsHelper
    {
        public static List<object> GetObjectsByXML(string strXML)
        {
            XmlDocument _xmlMain = GxXML.GetDocument2(strXML);
            //Validamos la version de GX
            try
            {
                XmlNode _xmlSource = GxXML.ReadElement(_xmlMain, "Source");
                if (_xmlSource != null)
                {
                    Gx18.Source _source = Functions.DeserializeXML_2<Gx18.Source>(_xmlSource.OuterXml);
                    if (_source != null && _source.Version != null && _source.Kb != null)
                    {
                        _xmlMain = null;
                        return Gx18Helper.GetObjectsByXML(strXML);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puede abrir con GX18, se continuará con la versión anterior\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //Console.WriteLine(ex.Message);
            }




            //List<gxViewer.GxObject.GXObj> _ret = new List<gxViewer.GxObject.GXObj>();
            List<object> _ret = new List<object>();


            XmlNodeList _xmlnodes = GxXML.ReadElements(_xmlMain, "GXObject");
            XmlNodeList _xmlnodesKNW = GxXML.ReadElements(_xmlMain, "KMW");
            XmlNodeList _xmlnodesModel = GxXML.ReadElements(_xmlMain, "Model");

            //Recupero el KMW correspondiente al archivo Distribuido
            GxObject.KMW _kmv = new gxViewer.GxObject.KMW();
            foreach (XmlNode _node01 in _xmlnodesKNW)
            {
                if (_node01 != null)
                {
                    _kmv = GxXML.XMLNodeToObject<GxObject.KMW>(_node01);
                }
            }

            //Recupero el Modelo correspondiente al archivo Distribuido
            GxObject.Model _mdl = new gxViewer.GxObject.Model();
            foreach (XmlNode _nodeMdl in _xmlnodesModel)
            {
                if (_nodeMdl != null)
                {
                    _mdl = GxXML.XMLNodeToObject<GxObject.Model>(_nodeMdl);
                }
            }
            _mdl.Version = "Genexus 9";


            foreach (XmlNode _xobj in _xmlnodes)
            {
                XmlNode _node = _xobj.FirstChild;
                if (_node != null)
                {
                    Console.WriteLine(_node.Name);
                    if (FilesGX.ObjectIsValid(_node.Name))  //(_node.Name == "WorkPanel" || _node.Name == "Procedure" || _node.Name == "Transaction" || _node.Name == "DataView")
                    {
                        object _obj = GxXML.XMLNodeToObject(_node);

                        GxObject.GXObj _gxobj = _obj as GxObject.GXObj;
                        Console.WriteLine(_gxobj.Info.Name);

                        //-->Obtenemos las variables
                        if (_gxobj != null)
                        {
                            //List<GxObject.Variable> _vars = new List<GxObject.Variable>();
                            Dictionary<string, GxObject.Variable> _vars = new Dictionary<string, gxViewer.GxObject.Variable>();
                            Dictionary<string, string> _IdVars = new Dictionary<string, string>();
                            Dictionary<string, GxObject.GXAtt> _attrs = new Dictionary<string, gxViewer.GxObject.GXAtt>();
                            //List<GxObject.GXAtt> _attrs = new List<GxObject.GXAtt>();


                            //GxObject.Variable

                            XmlDocument _docXml = GxXML.GetDocument2(_node.OuterXml);
                            XmlNodeList _xmlvariables = GxXML.ReadElements(_docXml, "Variable");



                            foreach (XmlNode _v in _xmlvariables)
                            {
                                GxObject.Variable _var = GxXML.XMLNodeToObject<GxObject.Variable>(_v);
                                //Console.WriteLine(_var.Name + "-- " + _var.Name2);
                                if (!_vars.ContainsKey(_var.Name))
                                {
                                    _vars.Add(_var.Name, _var);
                                    if (_var.Id != null)
                                    {
                                        _IdVars.Add(_var.Id, _var.Name);
                                    }
                                }
                            }
                            _gxobj.Variables = new GxObject.Variable[_vars.Count];
                            _vars.Values.CopyTo(_gxobj.Variables, 0);
                            _gxobj.IdVariables = _IdVars;
                            _gxobj.GXVariables = _vars;

                            //GxObject.Attributes
                            XmlNode _nodeAttrs = _node.ParentNode.NextSibling;
                            if (_nodeAttrs != null)
                            {
                                if (_nodeAttrs.Name == "Attributes")
                                {
                                    _docXml = GxXML.GetDocument2(_nodeAttrs.OuterXml);
                                    XmlNodeList _xmlattrs = GxXML.ReadElements(_docXml, "GXAtt");
                                    foreach (XmlNode _atr in _xmlattrs)
                                    {
                                        GxObject.GXAtt _a = GxXML.XMLNodeToObject<GxObject.GXAtt>(_atr);
                                        _attrs.Add(_a.Name, _a);
                                    }
                                    _gxobj.GXAtts = new GxObject.GXAtt[_attrs.Count];
                                    _attrs.Values.CopyTo(_gxobj.GXAtts, 0);
                                    _gxobj.GXAtributos = _attrs;
                                }
                            }

                            //Puede ser que no hayan atributos por version anterior de Genexus
                            if (_gxobj.GXAtts == null)
                            {
                                bool _batrib = false;

                                XmlNode _nAttr = _node.ParentNode.NextSibling;
                                if (_nAttr != null)
                                {
                                    if (_nAttr.Name == "GXAtt") { _batrib = true; } //Inicia Carga de Atributos

                                    while (_batrib)
                                    {
                                        GxObject.GXAtt _a = GxXML.XMLNodeToObject<GxObject.GXAtt>(_nAttr);
                                        _attrs.Add(_a.Name, _a);

                                        _nAttr = _nAttr.NextSibling;
                                        if (_nAttr != null)
                                        {
                                            if (_nAttr.Name == "GXAtt") { _batrib = true; } else { _batrib = false; }
                                        }
                                        else
                                        {
                                            _batrib = false;
                                        }


                                    }

                                    if (_attrs.Count > 0)
                                    {
                                        _gxobj.GXAtts = new GxObject.GXAtt[_attrs.Count];
                                        _attrs.Values.CopyTo(_gxobj.GXAtts, 0);
                                        _gxobj.GXAtributos = _attrs;
                                    }
                                }
                            }

                            //Obtenemos Propiedades del Objeto
                            Dictionary<string, GxObject.Property> _props = new Dictionary<string, gxViewer.GxObject.Property>();
                            _docXml = GxXML.GetDocument2(_node.OuterXml);
                            XmlNodeList _xmlproperties = GxXML.ReadElements(_docXml, "Property");

                            foreach (XmlNode _p in _xmlproperties)
                            {

                                if (_p.ParentNode.Name == _gxobj.Tipo)
                                {
                                    GxObject.Property _prp = GxXML.XMLNodeToObject<GxObject.Property>(_p);
                                    _props.Add(_prp.Name, _prp);
                                    //Console.WriteLine(_prp.Name + "-->" + _p.ParentNode.Name);
                                }

                            }
                            _gxobj.Propiedades = _props;

                        }

                        _gxobj.KMW = _kmv;
                        _gxobj.Model = _mdl;
                        _gxobj.XMLObj = _node.OuterXml;
                        _ret.Add(_gxobj);

                    }
                }
            }

            return _ret;

        }

        //public static FilesGXOpen getFilesGXOpen(FileInfo _info)
        //{
        //    FilesGXOpen _gxFile = new FilesGXOpen();
        //    _gxFile.IsGxViewerFile = false;
        //    _gxFile.Name = _info.Name;
        //    _gxFile.Path = _info.FullName;

        //    string _xmlstr = String.Empty;
        //    string _extension = _info.Extension.ToUpper();

        //    switch (_extension)
        //    {
        //        case ".XPW":
        //            //XPWREADERLib.XPWtoXMLClass _xpw = new XPWREADERLib.XPWtoXMLClass();
        //            XPWConverter.XPWReader _xpw = new XPWConverter.XPWReader();
        //            string _newxmlfilename = String.Format("{0}\\temp.xml", System.Windows.Forms.Application.StartupPath);
        //            _xpw.ConvertToXML(_info.FullName, _newxmlfilename);
        //            //_xmlstr = _xpw.GetXML(_info.FullName);
        //            _xpw = null;
        //            FileInfo _infotmp = new FileInfo(_newxmlfilename);
        //            _xmlstr = System.IO.File.ReadAllText(_infotmp.FullName, Encoding.Default);
        //            _infotmp.Delete();
        //            break;

        //        case ".XPZ":
        //            List<string> _files = ZipGX.ExtraerArchivos(_info.FullName);
        //            foreach (string _sfile in _files)
        //            {
        //                _xmlstr = System.IO.File.ReadAllText(_sfile, Encoding.Default);
        //            }
        //            break;

        //        case ".XML":
        //            _xmlstr = System.IO.File.ReadAllText(_info.FullName, Encoding.Default);
        //            break;

        //        case ".PRC":
        //        case ".WPN":
        //        case ".TRN":
        //        case ".RPT":
        //            try
        //            {
        //                string _strtmpxml = System.IO.File.ReadAllText(_info.FullName, Encoding.Default);
        //                XmlDocument _xmltmp = null;
        //                try
        //                {
        //                    _xmltmp = GxXML.GetDocument2(_strtmpxml);
        //                    _xmlstr = _xmltmp.OuterXml;
        //                    break;
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.Message);
        //                    _xmltmp = null;
        //                }

        //                if (_xmltmp == null) //si no esta bien formado el XML- vamos por el xpw
        //                {
        //                    //XPWREADERLib.XPWtoXMLClass _xpw2 = new XPWREADERLib.XPWtoXMLClass();
        //                    XPWConverter.XPWReader _xpw2 = new XPWConverter.XPWReader();

        //                    string _xpwxml = String.Format("{0}\\temp.xml", System.Windows.Forms.Application.StartupPath);
        //                    try
        //                    {
        //                        _xpw2.ConvertToXML(_info.FullName, _xpwxml);
        //                        _xpw2 = null;
        //                        FileInfo _infotmpxml = new FileInfo(_xpwxml);
        //                        _xmlstr = System.IO.File.ReadAllText(_infotmpxml.FullName, Encoding.Default);
        //                        _infotmpxml.Delete();
        //                        break;
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        MessageBox.Show("El archivo no es compatible con GXViewer\n" + ex.Message);
        //                        return null;
        //                    }
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message);
        //                return null;
        //            }
        //            break;

        //        case ".GXV":
        //            _gxFile.IsGxViewerFile = true;
        //            _xmlstr = System.IO.File.ReadAllText(_info.FullName, Encoding.Default);
        //            break;

        //        default:
        //            MessageBox.Show("Tipo de Archivo no esta permitido");
        //            return null;
        //    }

        //    //_gxFile.Objetos2 = GetObjectsByXML(_xmlstr);

        //    if (!String.IsNullOrEmpty(_xmlstr))
        //    {
        //        if (_gxFile.IsGxViewerFile)
        //        {
        //            GxObject.GXObj _gxObj = (GxObject.GXObj)Functions.String64ToObject(_xmlstr);
        //            _gxFile.XMLText = _gxObj.XMLObj; //_xmlstr;
        //            _gxFile.Objetos2 = new List<object> { _gxObj };
        //        }
        //        else
        //        {
        //            _gxFile.XMLText = _xmlstr;
        //            _gxFile.Objetos2 = GxObjectsHelper.GetObjectsByXML(_xmlstr);
        //        }

        //    }



        //    return _gxFile;

        //}

    }
}
