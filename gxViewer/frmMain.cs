using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Linq;
using System.Security.Cryptography;
using gxViewer.GxObject;
using gxViewer.AS400;
using gxViewer.Gx18;
using gxViewer.SGR.Models;
using gxViewer.SGR;
using Newtonsoft.Json;
using gxViewer.SGR.Services;

namespace gxViewer
{
    public partial class frmMain : System.Windows.Forms.Form
    {
        public static frmMain CurrentInstance;
        private List<FilesGXOpen> _FilesGXOpen = new List<FilesGXOpen>();
        private Point _imageLocation = new Point(15, 5);
        private Point _imgHitArea = new Point(13, 2);
        private bool _HaveArgs = false;
        private string[] _Arguments;
        private LoginResponse _loginData;

        public frmMain()
        {
            InitializeComponent();
            CurrentInstance = this;
            //this.tabContents.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
        }

        public frmMain(string[] args)
        {
            InitializeComponent();

            _HaveArgs = true;
            _Arguments = args;
            CurrentInstance = this;
        }


        #region Subrutinas a nivel de Formularios
        private void SetTitle()
        {
            string _CompileEnvironment = String.Empty;
            if (Environment.Is64BitProcess)
            {
                _CompileEnvironment = "x64";
            }
            else
            {
                _CompileEnvironment = "x86";
            }

            string _titulo = String.Format("GX Viewer ({2})   |   {0}\\{1}  | {3}", Environment.UserDomainName, Environment.UserName, String.Format("{0}", Application.ProductVersion), _CompileEnvironment);
            this.Text = _titulo;
        }
        
        private void OpenFileGX()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo Distribuido de Genexus";
            ofd.Filter = FilesGX.GetFillesPrmitid(); //"Archivos XML (*.xml)|*.xml|Archivos XPW (*.xpw)|*.xpw";
            ofd.FilterIndex = 0;
            ofd.Multiselect = true;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string sfile in ofd.FileNames)
                {
                    OpenFileOK(new FileInfo(sfile));
                }
                //FileInfo _info = new FileInfo(ofd.FileName);
                //OpenFileOK(_info)
            }
            ofd.Dispose();
        }

        private void OpenFileOK(FileInfo _info)
        {
            //txtPath.Text = _info.FullName;
            string _xmlstr = String.Empty;
            bool _GxViewFile = false;

            string _extension = _info.Extension.ToUpper();

            switch ( _extension )
            {
                case ".XPW" :
                    //XPWREADERLib.XPWtoXMLClass _xpw = new XPWREADERLib.XPWtoXMLClass();
                    XPWConverter.XPWReader _xpw = new XPWConverter.XPWReader();
                    string _newxmlfilename = String.Format("{0}\\temp.xml", Application.StartupPath);
                    _xpw.ConvertToXML(_info.FullName, _newxmlfilename);
                    //_xmlstr = _xpw.GetXML(_info.FullName);
                    _xpw = null;
                    FileInfo _infotmp = new FileInfo(_newxmlfilename);
                    _xmlstr = System.IO.File.ReadAllText(_infotmp.FullName, Encoding.Default);
                    _infotmp.Delete();
                    break;
                
                case ".XPZ":
                    List<string> _files = ZipGX.ExtraerArchivos(_info.FullName);
                    foreach (string _sfile in _files)
                    {
                        _xmlstr = System.IO.File.ReadAllText(_sfile, Encoding.Default);
                    }
                    break;

                case ".XML" :
                    _xmlstr = System.IO.File.ReadAllText(_info.FullName, Encoding.Default);
                    break;

                case ".PRC":
                case ".WPN":
                case ".TRN":
                case ".RPT":                
                    try
                    {
                        string _strtmpxml = System.IO.File.ReadAllText(_info.FullName, Encoding.Default);
                        XmlDocument _xmltmp = null;
                        try
                        {
                            _xmltmp = GxXML.GetDocument2(_strtmpxml);
                            _xmlstr = _xmltmp.OuterXml;
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            _xmltmp = null;
                        }
                        
                        if (_xmltmp == null) //si no esta bien formado el XML- vamos por el xpw
                        {
                            //XPWREADERLib.XPWtoXMLClass _xpw2 = new XPWREADERLib.XPWtoXMLClass();
                            XPWConverter.XPWReader _xpw2 = new XPWConverter.XPWReader();

                            string _xpwxml = String.Format("{0}\\temp.xml", Application.StartupPath);
                            try
                            {
                                _xpw2.ConvertToXML(_info.FullName, _xpwxml);
                                _xpw2 = null;
                                FileInfo _infotmpxml = new FileInfo(_xpwxml);
                                _xmlstr = System.IO.File.ReadAllText(_infotmpxml.FullName, Encoding.Default);
                                _infotmpxml.Delete();
                                break;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("El archivo no es compatible con GXViewer\n" + ex.Message);
                                return;
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                    break;

                case ".GXV":
                    _GxViewFile = true;
                    _xmlstr = System.IO.File.ReadAllText(_info.FullName, Encoding.Default);
                    break;

                case ".TXT":
                case ".CLP":
                case ".CLLE":
                case ".CLE":
                case ".RPG":
                case ".RPGLE":
                case ".CBL":
                case ".COBOL":
                case ".SQLRPG":
                case ".SQL":
                case ".TBT":
                case ".PF":
                    _GxViewFile = false;
                    OpenTEXTString(_info);
                    return;
                    //break;

                default:
                    MessageBox.Show("Tipo de Archivo no esta permitido");
                    return;
            }


            //SI CARGO BIEN EL ARCHIVO GX, entonces AGREGO A LA LISTA DE OBJETOS ABIERTOS
            FilesGXOpen _gxFile = new FilesGXOpen();
            _gxFile.IsGxViewerFile = _GxViewFile;
            _gxFile.Name = _info.Name;
            _gxFile.Path = _info.FullName;

            if (!String.IsNullOrEmpty(_xmlstr))
            {
                if (_gxFile.IsGxViewerFile)
                {
                    GxObject.GXObj _gxObj = (GxObject.GXObj)Functions.String64ToObject(_xmlstr);
                    _gxFile.XMLText = _gxObj.XMLObj; //_xmlstr;
                    _gxFile.Objetos2 = new  List<object> { _gxObj } ;
                }
                else
                {
                    _gxFile.XMLText = _xmlstr;
                    _gxFile.Objetos2 = GxObjectsHelper.GetObjectsByXML(_xmlstr); 
                }
               
            }
            
            _FilesGXOpen.Add(_gxFile);

            //ReloadTreeview();
            TreeNode _node = AddFileInTreeview(_gxFile);
            _node.Expand();
            tvObjetos.SelectedNode = _node;
        }


        public void Open_HTML_DLYA(string _HTML)
        {
            string path = Functions.GetDownloadFolderPath(); //Environment.SpecialFolder.UserProfile + @"\Downloads";
            DirectoryInfo dinfo = new DirectoryInfo(path);

            if (dinfo.Exists)
            {
                string _nameFile = $"ExpDLYA_{DateTime.Now.ToString("yyyyMMdd_HHmiss")}.gxhtml";
                FileInfo _fi = new FileInfo(Path.Combine(dinfo.FullName, _nameFile));
                if (_fi.Exists)
                {
                    _fi.Delete();
                }
                
                File.WriteAllText(_fi.FullName, _HTML, Encoding.UTF8);

                _fi = new FileInfo(_fi.FullName);
                if (_fi.Exists)
                {
                    string _type = String.Empty;
                    GxObject.GXObj _gxObj = HTMLtoGxXML.ConvertXML(_fi.FullName);
                    FileInfo _fi2 = null;
                    if (_gxObj != null && _gxObj.Name != null && !String.IsNullOrEmpty(_gxObj.Name))
                    {
                        string _newName = $"{_gxObj.GetName()}.html";
                        _fi2 = new FileInfo(Path.Combine(dinfo.FullName, _newName));
                        if (_fi2.Exists)
                        {
                            _fi2.Delete();
                        }
                        File.Move(_fi.FullName, _fi2.FullName);
                    }
                    else
                    {
                        MessageBox.Show("Esta pagina, no es el fuente de un objeto DL&A");
                        _fi.Delete();
                        return;
                    }

                    if (_fi2 != null)
                    {
                        FilesGXOpen _gxFile = new FilesGXOpen();
                        _gxFile.Name = _fi2.Name;
                        _gxFile.Path = _fi2.FullName;
                        _gxFile.XMLText = _gxObj.XMLObj;

                        List<object> _ret = new List<object>();
                        _ret.Add(_gxObj);

                        _gxFile.Objetos2 = _ret;

                        _FilesGXOpen.Add(_gxFile);

                        ReloadTreeview();
                    }
                    

                }


            }

        }
        private void OpenFileOK_HTML(FileInfo _info)
        {
            string _type = String.Empty;
            GxObject.GXObj _gxObj = HTMLtoGxXML.ConvertXML(_info.FullName);

            FilesGXOpen _gxFile = new FilesGXOpen();
            _gxFile.Name = _info.Name;
            _gxFile.Path = _info.FullName;
            _gxFile.XMLText = _gxObj.XMLObj;

            List<object> _ret = new List<object>();
            _ret.Add(_gxObj);

            _gxFile.Objetos2 = _ret;

            _FilesGXOpen.Add(_gxFile);

            ReloadTreeview();

            _info = null;

        }

        private void OpenFileOK_JSON(FileInfo _info)
        {
            //string _type = String.Empty;
            string _json = System.IO.File.ReadAllText(_info.FullName, Encoding.UTF8);
            GxResponse resp = JsonConvert.DeserializeObject<GxResponse>(_json);
            if (resp == null)
                throw new Exception("Error al cargar el JSON ");

            //GxResponse resp =  arr[0];
            string source = GxCrypto.DescifrarSource(resp.pSource, resp.iv, resp.ivServidor);

            GxObject.GXObj _gxObj = JsonToGxObj.Convert(resp, source);

            FilesGXOpen _gxFile = new FilesGXOpen();
            _gxFile.Name = _info.Name;
            _gxFile.Path = _info.FullName;
            _gxFile.XMLText = _gxObj.XMLObj;

            List<object> _ret = new List<object>();
            _ret.Add(_gxObj);

            _gxFile.Objetos2 = _ret;

            _FilesGXOpen.Add(_gxFile);

            ReloadTreeview();

            _info = null;

        }


        public bool OpenXMLString(string _XMLString, string NameInTree)
        {
            try
            {
                FilesGXOpen _gxFile = new FilesGXOpen();
                _gxFile.Name = NameInTree;
                _gxFile.Path = NameInTree;
                _gxFile.XMLText = _XMLString;

                _gxFile.Objetos2 = GxObjectsHelper.GetObjectsByXML(_XMLString); //new List<gxViewer.GxObject.GXObj>() ;

                _FilesGXOpen.Add(_gxFile);

                TreeNode _node = AddFileInTreeview(_gxFile);
                _node.Expand();
                tvObjetos.SelectedNode = _node;

                //ReloadTreeview();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
            
        }


        public bool OpenTEXTString(FileInfo pFileInfo)
        {
            try
            {
               
                string _ext = pFileInfo.Extension.Replace(".", "");


                TreeNode _tvMain = tvObjetos.Nodes.Add(pFileInfo.Name);
                _tvMain.ToolTipText = pFileInfo.FullName;
                _tvMain.ImageIndex = imageList1.Images.IndexOfKey("folder");
                _tvMain.SelectedImageIndex = _tvMain.ImageIndex;
                //_tvMain.Tag = pFileInfo;

                string _nameChild = pFileInfo.Name.Replace(pFileInfo.Extension, String.Empty);
                TreeNode _tvHijo = _tvMain.Nodes.Add(_nameChild);

                _tvHijo.Name = _nameChild;//_obj.Tipo.Substring(0, 1) + _obj.Info.Name;
                _tvHijo.Tag =  AS400_Helper.Get400Object(pFileInfo);
                if (!imageList1.Images.ContainsKey(_ext))
                {
                    _ext = "txt";
                }
                _tvHijo.ImageIndex = imageList1.Images.IndexOfKey(_ext);
                //_tvHijo.ImageKey = _ext;
                _tvHijo.SelectedImageIndex = _tvHijo.ImageIndex;

                _tvMain.Expand();


                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //private List<object> GetObjectsByXML(string strXML)
        //{
        //    XmlDocument _xmlMain = GxXML.GetDocument2(strXML);
        //    //Validamos la version de GX
        //    try
        //    {
        //        XmlNode _xmlSource = GxXML.ReadElement(_xmlMain, "Source");
        //        if (_xmlSource != null)
        //        {
        //            Gx18.Source _source = Functions.DeserializeXML_2<Gx18.Source>(_xmlSource.OuterXml);
        //            if (_source != null && _source.Version != null && _source.Kb != null)
        //            {
        //                _xmlMain = null;
        //                return Gx18Helper.GetObjectsByXML(strXML);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("No se puede abrir con GX18, se continuará con la versión anterior\n"+ ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //Console.WriteLine(ex.Message);
        //    }
            
            

        //    //List<gxViewer.GxObject.GXObj> _ret = new List<gxViewer.GxObject.GXObj>();
        //    List<object> _ret = new List<object>();

            
        //    XmlNodeList _xmlnodes = GxXML.ReadElements(_xmlMain, "GXObject");
        //    XmlNodeList _xmlnodesKNW = GxXML.ReadElements(_xmlMain, "KMW");
        //    XmlNodeList _xmlnodesModel = GxXML.ReadElements(_xmlMain, "Model");

        //    //Recupero el KMW correspondiente al archivo Distribuido
        //    GxObject.KMW _kmv = new gxViewer.GxObject.KMW();
        //    foreach (XmlNode _node01 in _xmlnodesKNW)
        //    {
        //        if (_node01 != null)
        //        {
        //            _kmv = GxXML.XMLNodeToObject<GxObject.KMW>(_node01);
        //        }
        //    }

        //    //Recupero el Modelo correspondiente al archivo Distribuido
        //    GxObject.Model _mdl = new gxViewer.GxObject.Model();
        //    foreach (XmlNode _nodeMdl in _xmlnodesModel)
        //    {
        //        if (_nodeMdl != null)
        //        {
        //            _mdl = GxXML.XMLNodeToObject<GxObject.Model>(_nodeMdl);
        //        }
        //    }
        //    _mdl.Version = "Genexus 9";


        //    foreach (XmlNode _xobj in _xmlnodes)
        //    {
        //        XmlNode _node = _xobj.FirstChild;
        //        if(_node!=null)
        //        {
        //            Console.WriteLine(_node.Name);
        //            if (FilesGX.ObjectIsValid(_node.Name))  //(_node.Name == "WorkPanel" || _node.Name == "Procedure" || _node.Name == "Transaction" || _node.Name == "DataView")
        //            {
        //                object _obj = GxXML.XMLNodeToObject(_node);

        //                GxObject.GXObj _gxobj = _obj as GxObject.GXObj;
        //                Console.WriteLine(_gxobj.Info.Name);

        //                //-->Obtenemos las variables
        //                if (_gxobj != null)
        //                {
        //                    //List<GxObject.Variable> _vars = new List<GxObject.Variable>();
        //                    Dictionary<string, GxObject.Variable> _vars = new Dictionary<string, gxViewer.GxObject.Variable>();
        //                    Dictionary<string, string> _IdVars = new Dictionary<string, string>();
        //                    Dictionary<string, GxObject.GXAtt> _attrs = new Dictionary<string, gxViewer.GxObject.GXAtt>();
        //                    //List<GxObject.GXAtt> _attrs = new List<GxObject.GXAtt>();
                            

        //                    //GxObject.Variable

        //                    XmlDocument _docXml = GxXML.GetDocument2(_node.OuterXml);
        //                    XmlNodeList _xmlvariables = GxXML.ReadElements(_docXml, "Variable");

                            

        //                    foreach (XmlNode _v in _xmlvariables)
        //                    {
        //                        GxObject.Variable _var = GxXML.XMLNodeToObject<GxObject.Variable>(_v);
        //                        //Console.WriteLine(_var.Name + "-- " + _var.Name2);
        //                        if (!_vars.ContainsKey(_var.Name))
        //                        {
        //                            _vars.Add(_var.Name, _var);
        //                            if (_var.Id != null)
        //                            {
        //                                _IdVars.Add(_var.Id, _var.Name);
        //                            }
        //                        }
        //                    }
        //                    _gxobj.Variables = new GxObject.Variable[_vars.Count];
        //                    _vars.Values.CopyTo(_gxobj.Variables, 0);
        //                    _gxobj.IdVariables = _IdVars;
        //                    _gxobj.GXVariables = _vars;

        //                    //GxObject.Attributes
        //                    XmlNode _nodeAttrs = _node.ParentNode.NextSibling;
        //                    if (_nodeAttrs != null)
        //                    {
        //                        if (_nodeAttrs.Name == "Attributes")
        //                        {
        //                            _docXml = GxXML.GetDocument2(_nodeAttrs.OuterXml);
        //                            XmlNodeList _xmlattrs = GxXML.ReadElements(_docXml, "GXAtt");
        //                            foreach (XmlNode _atr in _xmlattrs)
        //                            {
        //                                GxObject.GXAtt _a = GxXML.XMLNodeToObject<GxObject.GXAtt>(_atr);
        //                                _attrs.Add(_a.Name, _a);
        //                            }
        //                            _gxobj.GXAtts = new GxObject.GXAtt[_attrs.Count];
        //                            _attrs.Values.CopyTo(_gxobj.GXAtts, 0);
        //                            _gxobj.GXAtributos = _attrs;
        //                        }
        //                    }

        //                    //Puede ser que no hayan atributos por version anterior de Genexus
        //                    if (_gxobj.GXAtts == null)
        //                    {
        //                        bool _batrib = false;
                                
        //                        XmlNode _nAttr = _node.ParentNode.NextSibling;
        //                        if (_nAttr != null)
        //                        {
        //                            if (_nAttr.Name == "GXAtt") { _batrib = true; } //Inicia Carga de Atributos

        //                            while (_batrib)
        //                            {
        //                                GxObject.GXAtt _a = GxXML.XMLNodeToObject<GxObject.GXAtt>(_nAttr);
        //                                _attrs.Add(_a.Name, _a);

        //                                _nAttr = _nAttr.NextSibling;
        //                                if (_nAttr != null)
        //                                {
        //                                    if (_nAttr.Name == "GXAtt") { _batrib = true; } else { _batrib = false; }
        //                                }
        //                                else
        //                                {
        //                                    _batrib = false;
        //                                }
                                        

        //                            }

        //                            if (_attrs.Count > 0)
        //                            {
        //                                _gxobj.GXAtts = new GxObject.GXAtt[_attrs.Count];
        //                                _attrs.Values.CopyTo(_gxobj.GXAtts, 0);
        //                                _gxobj.GXAtributos = _attrs;
        //                            }
        //                        }
        //                    }

        //                    //Obtenemos Propiedades del Objeto
        //                    Dictionary<string, GxObject.Property> _props = new Dictionary<string, gxViewer.GxObject.Property>();
        //                     _docXml = GxXML.GetDocument2(_node.OuterXml);
        //                    XmlNodeList _xmlproperties = GxXML.ReadElements(_docXml, "Property");

        //                    foreach (XmlNode _p in _xmlproperties)
        //                    {

        //                        if (_p.ParentNode.Name == _gxobj.Tipo)
        //                        {
        //                            GxObject.Property _prp = GxXML.XMLNodeToObject<GxObject.Property>(_p);
        //                            _props.Add(_prp.Name, _prp);
        //                            //Console.WriteLine(_prp.Name + "-->" + _p.ParentNode.Name);
        //                        } 

        //                    }
        //                    _gxobj.Propiedades = _props;

        //                }

        //                _gxobj.KMW = _kmv;
        //                _gxobj.Model = _mdl;
        //                _gxobj.XMLObj = _node.OuterXml;
        //                _ret.Add(_gxobj);

        //            }
        //        }
        //    }

        //    return _ret;

        //}

        private void ReloadTreeview()
        {
            tvObjetos.ImageList = imageList1;
            //tvObjetos.Nodes.Clear();

            FilesGXOpen _Lastgxf =_FilesGXOpen.LastOrDefault();
            if (_Lastgxf != null)
            {
                TreeNode _node = AddFileInTreeview(_Lastgxf);
                _node.ExpandAll();
            }
            tvObjetos.Refresh();

            //foreach (FilesGXOpen _gxf in _FilesGXOpen)
            //{    
            //    /*
            //    TreeNode _tvMain = tvObjetos.Nodes.Add(_gxf.Name);
            //    _tvMain.ToolTipText = _gxf.Path;
            //    _tvMain.ImageIndex = imageList1.Images.IndexOfKey("folder");
            //    _tvMain.SelectedImageIndex = _tvMain.ImageIndex;
            //    //_tvMain.Tag = _gxf.pa;

            //    foreach (GxObject.GXObj _obj in _gxf.Objetos2)
            //    {
            //        string _strTextTreeview = _obj.Info.Name;
            //        if (_obj.Tipo == "Procedure" || _obj.Tipo == "WorkPanel" || _obj.Tipo == "Report")
            //        {
            //            _strTextTreeview = string.Format("{0}{1}", _obj.Tipo.Substring(0, 1), _obj.Info.Name);
            //        }

            //        TreeNode _tvHijo = _tvMain.Nodes.Add(_strTextTreeview);
            //        _tvHijo.ToolTipText = _obj.LastUpdate.ToString();
            //        _tvHijo.Name = _obj.Tipo.Substring(0,1) + _obj.Info.Name;
            //        _tvHijo.Tag = _obj;
            //        _tvHijo.ImageIndex = imageList1.Images.IndexOfKey(_obj.Tipo.ToLower());
            //        _tvHijo.SelectedImageIndex = _tvHijo.ImageIndex;
            //    }
            //    */
            //    AddFileInTreeview(_gxf);
            //}

        }


        private TreeNode AddFileInTreeview(FilesGXOpen _gxf)
        {
            TreeNode _tvMain = tvObjetos.Nodes.Add(_gxf.Name);
            _tvMain.ToolTipText = _gxf.Path;
            _tvMain.ImageIndex = imageList1.Images.IndexOfKey("folder");
            _tvMain.SelectedImageIndex = _tvMain.ImageIndex;
            _tvMain.Tag = _gxf.IsGxViewerFile;
            
            //_tvMain.Tag = _gxf.pa;
            
            foreach (GxObject.GXObj _obj in _gxf.Objetos2)
            {
                if (_obj.Info != null)
                {
                    string _strTextTreeview = _obj.Info.Name;
                    if (_obj.Tipo == "Procedure" || _obj.Tipo == "WorkPanel" || _obj.Tipo == "Report")
                    {
                        _strTextTreeview = string.Format("{0}{1}", _obj.Tipo.Substring(0, 1), _obj.Info.Name);
                    }
                    else
                    {
                        if (_obj.Tipo == "WebPanel")
                        {
                            _strTextTreeview = string.Format("H{0}", _obj.Info.Name);
                        }
                    }

                    //Asignar el icono de Genexus 18
                    if (!String.IsNullOrEmpty(_obj.Version_Genexus))
                    {
                        if (_obj.Version_Genexus.Contains("18"))
                        {
                            _tvMain.ImageIndex = imageList1.Images.IndexOfKey("Foldergx18");  
                        }
                        else
                        {
                            _tvMain.ImageIndex = imageList1.Images.IndexOfKey("Foldergx08");
                        }
                        _tvMain.SelectedImageIndex = _tvMain.ImageIndex;
                    }


                    TreeNode _tvHijo = _tvMain.Nodes.Add(_strTextTreeview);
                    if (_obj.LastUpdate != null)
                    {
                        _tvHijo.ToolTipText = _obj.LastUpdate.ToString();
                    }

                    _tvHijo.Name = _strTextTreeview;//_obj.Tipo.Substring(0, 1) + _obj.Info.Name;
                    _tvHijo.Tag = _obj;
                    _tvHijo.ImageIndex = imageList1.Images.IndexOfKey(_obj.Tipo.ToLower());
                    _tvHijo.SelectedImageIndex = _tvHijo.ImageIndex;
                }
            }
            //_tvMain.ExpandAll();
            return _tvMain;
        }



        private TabPage AddTabMain( Control c, string _KeyImage )
        {
            TabPage _newtab = null;
            string _tabName = String.Format("tb{0}-{1}", c.Name, c.Tag.ToString().Replace(".",""));
            if (!tabContents.TabPages.ContainsKey(_tabName))
            {
                c.Dock = DockStyle.Fill;
                _newtab = new TabPage(c.Text);
                _newtab.Name = _tabName ;
                _newtab.ToolTipText = String.Format("{0} -> {1}", c.Tag.ToString(), c.Name );
                _newtab.ImageIndex = imageList1.Images.IndexOfKey(_KeyImage);
                //_newtab.ImageIndex = 0;
                
                _newtab.Controls.Add(c);
                tabContents.TabPages.Add(_newtab);
                tabContents.SelectedTab = _newtab;
            }
            else
            {
                int _idx = tabContents.TabPages.IndexOfKey(_tabName);
                _newtab = tabContents.TabPages[_idx];
                tabContents.SelectedTab = _newtab;
            }
            return _newtab;
        }


        #endregion

        #region Eventos

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.SetTitle();

            if (_HaveArgs)
            {

                //_Arguments = args;
            }

            tvObjetos.ImageList = imageList1;
        }


        

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            this.OpenFileGX();
        }

        public void InvokeOpenGXObjectSelected()
        {
            if (tvObjetos.SelectedNode != null)
            {
                if (tvObjetos.SelectedNode.Nodes.Count > 0)
                {
                    TreeNode _node = tvObjetos.SelectedNode.Nodes[0];
                    TreeNodeMouseClickEventArgs _arge = new TreeNodeMouseClickEventArgs(_node, MouseButtons.Left, 2, 1, 1); ;
                    tvObjetos_NodeMouseDoubleClick(tvObjetos, _arge);
                }
            }
        }

        private void tvObjetos_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null) //Esto indica que se tiene un objeto asociado
            {
                bool IsGxVFile = false;

                if (e.Node.Tag.GetType().BaseType == typeof(GxObject.GXObj) || e.Node.Tag.GetType() == typeof(GxObject.GXObj))
                {
                    GxEditor _gxObject = null;
                    if (typeof(Boolean) == e.Node.Parent.Tag.GetType())
                    {
                        IsGxVFile = (bool)e.Node.Parent.Tag;
                    }

                    if (IsGxVFile)
                    {
                        _gxObject = new GxEditor(e.Node.Parent.ToolTipText);
                        _gxObject.Name = e.Node.Name;
                        _gxObject.Text = e.Node.Parent.Text;
                        _gxObject.Tag = e.Node.Parent.Text;
                        //_gxObject.objetoGX = null;
                    }
                    else
                    {
                        _gxObject = new GxEditor();
                        _gxObject.Name = e.Node.Name;
                        _gxObject.Text = e.Node.Text;
                        _gxObject.objetoGX = e.Node.Tag;
                        _gxObject.Tag = e.Node.Parent.Text;
                    }

                    string _keyimage = String.Empty;
                    if (_gxObject.objetoGX != null)
                    {
                        _keyimage = ((GxObject.GXObj)_gxObject.objetoGX).Tipo;
                    }

                    TabPage _pag = this.AddTabMain(_gxObject, _keyimage.ToLower());
                    if (IsGxVFile)
                    {
                        _pag.ToolTipText = e.Node.Parent.ToolTipText;
                    }
                }
                else
                {
                    if (e.Node.Tag.GetType() == typeof(As400Object))
                    {
                        As400Object _fi = e.Node.Tag as As400Object;
                        string _image = _fi.Ext;
                        if (!imageList1.Images.ContainsKey(_image))
                        {
                            _image = "txt";
                        }

                        AS400Editor _as400Editor = new AS400Editor();
                        _as400Editor.Name = e.Node.Name;
                        _as400Editor.Text = e.Node.Text;
                        _as400Editor.objeto = _fi;
                        _as400Editor.Tag = e.Node.Parent.Text;
                        TabPage _pag = this.AddTabMain(_as400Editor, _image);
                    }
                }


            }
            
        }

        #endregion

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            frmAboutBox _frm = new frmAboutBox();
            _frm.ShowDialog(this);
        }

        private void txtFindTreeview_TextChanged(object sender, EventArgs e)
        {
            if (_FilesGXOpen.Count > 0)
            {
                this.BuscarTreeview();
            }
        }


        private void BuscarTreeview()
        {
            string _strABuscar = txtFindTreeview.Text.ToUpper();
            int _len = _strABuscar.Length;
            //int _indexOf = 0;

            if(txtFindTreeview.Text.Length <= 0)
            {
                foreach (TreeNode t1 in tvObjetos.Nodes)
                {
                    t1.BackColor = Color.White;
                    foreach (TreeNode t2 in t1.Nodes)
                    {
                        t2.BackColor = Color.White;
                    }
                }
                return;
            }


            foreach (TreeNode t1 in tvObjetos.Nodes)
            {
                t1.BackColor = Color.White;
                if (t1.Text.Length >= _len)
                {
                    if (t1.Text.Contains( _strABuscar))
                    {
                        t1.BackColor = Color.Yellow;
                    }
                }

                foreach (TreeNode t2 in t1.Nodes)
                {
                    t2.BackColor = Color.White;
                    if (t2.Text.Length >= _len)
                    {
                        if (t2.Text.Contains(_strABuscar))
                        {
                            t2.BackColor = Color.Yellow;
                        }
                    }
                }
            }
            this.Refresh();

        }

        private void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            // Comprobar si el tipo de datos es el que queremos
            if (e.Data.GetDataPresent("FileName", false))
            {
                string[] _FilesDrop = (string[])(e.Data.GetData("FileDrop", false));
                foreach (string sfile in _FilesDrop)
                {
                    FileInfo _info = new FileInfo(sfile);
                    if (_info.Extension.ToUpper().Contains("HTM"))
                    {
                        OpenFileOK_HTML(_info);
                    }
                    else if (_info.Extension.ToUpper().Contains("JSON"))
                    {
                        OpenFileOK_JSON(_info);
                    }
                    else
                    {
                        OpenFileOK(_info);
                    }
                }
            }
        }

        private void frmMain_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void tvObjetos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (tvObjetos.Nodes.Count == 0) { return; }
                if (MessageBox.Show( String.Format("Estas seguro de Eliminar el Archivo cargado {0}", tvObjetos.SelectedNode.Text),this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    try
                    {
                        TreeNode _tvDel = tvObjetos.SelectedNode;

                        foreach (FilesGXOpen _gxf in _FilesGXOpen)
                        {
                            if (_gxf.Name == _tvDel.Text)
                            {
                                _FilesGXOpen.Remove(_gxf);
                                break;
                            }
                        }
                        tvObjetos.Nodes.Remove(_tvDel);
                        tvObjetos.Refresh();
                        //ReloadTreeview();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show( String.Format("Seleccione un nodo Superior \n{0}", ex.Message), this.Text,  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
                    }
                    
                }

                
            }
        }

        private void abrirArchivoGXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenFileGX();
        }

        private void nuevoArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GxEditor _gxObject = new GxEditor();
            _gxObject.Name = "NewObj-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            _gxObject.Tag = "NewOBJ";
            _gxObject.Text = _gxObject.Name;
            _gxObject.objetoGX = null;
            string _keyimage = "procedure";
            this.AddTabMain(_gxObject, _keyimage.ToLower());
        }

        public void OpenFileForArguments(string _filename)
        {
            try
            {
                FileInfo _info = new FileInfo(_filename);


                if (_info.Extension.ToUpper().Contains("HTM"))
                {
                    OpenFileOK_HTML(_info);
                }
                else
                {
                    OpenFileOK(_info);
                }
                tvObjetos.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }
        
        private void convertirHtmlDLAEnXPWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo HTML DLA";
            ofd.Filter = FilesGX.GetFillesPrmitid("htm,html,xhtml"); //"Archivos HTML (*.htm)|*.htm|Archivos HTML (*.html)|*.html";
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo _info = new FileInfo(ofd.FileName);
                OpenFileOK_HTML(_info );
            }
            ofd.Dispose();
        }

        private void convertirXPWEnXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Abrir archivo Distribuido de Genexus";
                ofd.Filter = FilesGX.GetFillesPrmitid("xpw,xpz,prc,wpn,rpt,trn");
                ofd.FilterIndex = 0;
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    FileInfo _info = new FileInfo(ofd.FileName);
                    string _xmlstr = String.Empty;

                    switch (_info.Extension.ToUpper())
                    {
                        case ".XPW":
                        case ".PRC":
                        case ".WPN":
                        case ".TRN":
                        case ".RPT":
                            //XPWREADERLib.XPWtoXMLClass _xpw = new XPWREADERLib.XPWtoXMLClass();
                            string _strtmpxml = System.IO.File.ReadAllText(_info.FullName, Encoding.Default);
                            XmlDocument _xmltmp = null;
                            try
                            {
                                _xmltmp = GxXML.GetDocument2(_strtmpxml);
                                _xmlstr = _xmltmp.OuterXml;
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                _xmltmp = null;
                            }

                            try
                            {
                                XPWConverter.XPWReader _xpw = new XPWConverter.XPWReader();
                                string _newxmlfilename = String.Format("{0}\\temp.xml", Application.StartupPath);
                                _xpw.ConvertToXML(_info.FullName, _newxmlfilename);
                                _xpw = null;
                                FileInfo _infotmp = new FileInfo(_newxmlfilename);
                                _xmlstr = System.IO.File.ReadAllText(_infotmp.FullName, Encoding.Default);

                                _infotmp.Delete();
                                break;
                            }
                            catch (Exception ex )
                            {
                                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                throw;
                            }
                            

                        case ".XPZ":
                            List<string> _files = ZipGX.ExtraerArchivos(_info.FullName);
                            foreach (string _sfile in _files)
                            {
                                _xmlstr = System.IO.File.ReadAllText(_sfile, Encoding.Default);
                            }
                            break;

                        default:
                            MessageBox.Show("Tipo de Archivo no esta permitido");
                            return;
                    }

                    if (_xmlstr != String.Empty)
                    {

                        frmViewXML _fXML = new frmViewXML();
                        string _titulo = String.Format("{0})", _info.Name);
                        _fXML.SetTitle(_titulo);

                        //System.Xml.Linq.XDocument _doc = System.Xml.Linq.XDocument.Parse(_xmlstr);
                        //_xmlstr = _doc.ToString( );
                        //_doc = null;

                        _fXML.LoadString(_xmlstr);
                        _fXML.Show(this);
                    }


                }
                ofd.Dispose();
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message, "Comunicarse con John Aima", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            
        }

        private void buscarObjetosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGxPublic _frm = new frmGxPublic();
            _frm.Owner = this;
            _frm.Show(this);
        }

        private void obtenerXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Esta opción no esta implementada en esta versión \n Comuniquese con el administrador del aplicativo C.A.R.");
        }

        private void TSMIguardarArchivosAbiertos_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog ofd = new FolderBrowserDialog();
            ofd.SelectedPath = Application.StartupPath;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                                
                foreach (FilesGXOpen _gxf in _FilesGXOpen)
                {
                    FileInfo _xiFile = new FileInfo(_gxf.Path);
                    string _extension = _xiFile.Extension;

                    FileInfo _info = null;
                    string _path = String.Empty;
                    
                    _path = Path.Combine(ofd.SelectedPath, _gxf.Name) ;

                    _info = new FileInfo(_path);
                    if (_info.Exists)
                    {
                        _path = _path.Replace(_extension, "_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + _extension);
                    }

                    _info = new FileInfo(_path);
                    File.AppendAllText(_info.FullName, _gxf.XMLText, Encoding.UTF8);
                }
            }
            ofd.Dispose();

        }

        private void newEditorHighlighToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmViewXML _fXML = new frmViewXML();
            string _titulo = String.Format("{0})", "Nuevo Archivo");
            _fXML.SetTitle(_titulo);
            _fXML.Show(this);
        }

        private void convertirAStringBase64ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileToStrBase64.frmConvertToBase64 frm = new FileToStrBase64.frmConvertToBase64();
            frm.Show();
        }

        private void convertirDeBase64AFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileToStrBase64.frmConvertToFile frm = new FileToStrBase64.frmConvertToFile();
            frm.Show();
        }

        private void desdeElSGRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmViewBrowser frm = new frmViewBrowser("https://support.dlya.com.uy/SGR/#/index/HUB/BantotalView");
            frm.Show(this);
        }

        private void desdeArchivoHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo JSON/HTML DLA";
            ofd.Filter = FilesGX.GetFillesPrmitid("htm,html,xhtml,json"); //"Archivos HTML (*.htm)|*.htm|Archivos HTML (*.html)|*.html";
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo _info = new FileInfo(ofd.FileName);
                if (_info.Extension.ToUpper().Contains("JSON"))
                {
                    OpenFileOK_JSON(_info);
                }
                else
                {
                    OpenFileOK_HTML(_info);
                }
            }
            ofd.Dispose();
        }
    
        /*A PARTIR DE AQUI TODO APLICA SOBRE GX 18, VAMOS A CONVERTIR A GX 8 PARA PODER MOSTRAR EN GXVIEWER*/
        private void abrirArchivoGX18ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo Distribuido de Genexus 18";
            ofd.Filter = FilesGX.GetFillesPrmitid_Gx18(); 
            ofd.FilterIndex = 0;
            ofd.Multiselect = true;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string sfile in ofd.FileNames)
                {
                    OpenFileOK_GX18(new FileInfo(sfile));
                }
            }
            ofd.Dispose();
        }

        private void OpenFileOK_GX18(FileInfo _info)
        {
            string _xmlstr = String.Empty;
            string _extension = _info.Extension.ToUpper();

            switch (_extension)
            {
                case ".XPZ":
                    List<string> _files = ZipGX.ExtraerArchivos(_info.FullName);
                    foreach (string _sfile in _files)
                    {
                        _xmlstr = System.IO.File.ReadAllText(_sfile, Encoding.Default);
                    }
                    break;

                case ".PRC":
                case ".WPN":
                case ".TRN":
                case ".RPT":
                case ".HPN":
                case ".XML":
                    _xmlstr = System.IO.File.ReadAllText(_info.FullName, Encoding.Default);
                    break;

                default:
                    MessageBox.Show("Tipo de Archivo no esta permitido");
                    return;
            }

            //SI CARGO BIEN EL ARCHIVO GX, entonces AGREGO A LA LISTA DE OBJETOS ABIERTOS
            FilesGXOpen _gxFile = new FilesGXOpen();
            _gxFile.Name = _info.Name;
            _gxFile.Path = _info.FullName;

            if (!String.IsNullOrEmpty(_xmlstr))
            {
                _gxFile.XMLText = _xmlstr;
                _gxFile.Objetos2 = Gx18Helper.GetObjectsByXML(_xmlstr);
            }

            _FilesGXOpen.Add(_gxFile);

            //ReloadTreeview();
            TreeNode _node = AddFileInTreeview(_gxFile);
            _node.Expand();
            tvObjetos.SelectedNode = _node;
        }

        private void buscarObjetosV151618ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGxPublicV2 _frm = new frmGxPublicV2();
            _frm.Owner = this;
            _frm.Show(this);
        }

        private void desdeSGRV2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmViewBrowser frm = new frmViewBrowser("https://support.dlya.com.uy/SGR/#/index/HUB/BantotalView");
            //frm.Show(this);

            frmBantotalView frm = new frmBantotalView();
            frm.Show(this);

            // Cuando el usuario pulse "Obtener en GXViewer", el formulario
            // se cerrará con DialogResult.OK y aquí lo recogemos.
            // Pero como usamos Show (no ShowDialog), necesitamos manejar el FormClosed:
            frm.FormClosed += (s, ev) =>
            {
                if (frm.DialogResult == DialogResult.OK && frm.ObjetoResultado != null)
                {
                    Open_GXObj_DLYA(frm.ObjetoResultado);
                }
            };

        }

        public void Open_GXObj_DLYA(GxObject.GXObj _gxObj)
        {
            if (_gxObj == null || string.IsNullOrEmpty(_gxObj.Name))
            {
                MessageBox.Show("Esta pagina, no es el fuente de un objeto DL&A");
                return;
            }

            string path = Functions.GetDownloadFolderPath();
            DirectoryInfo dinfo = new DirectoryInfo(path);
            if (!dinfo.Exists) return;

            // Nombre físico final, igual que antes
            string _newName = $"{_gxObj.GetName()}.json";
            FileInfo _fi2 = new FileInfo(Path.Combine(dinfo.FullName, _newName));
            if (_fi2.Exists)
            {
                _fi2.Delete();
            }

            // Escribimos el JSON crudo (trazabilidad). Si NO quieres tocar disco,
            // borra estas dos líneas y deja _fi2 apuntando a un path ficticio.
            File.WriteAllText(_fi2.FullName, _gxObj.XMLObj ?? "", Encoding.UTF8);

            FilesGXOpen _gxFile = new FilesGXOpen();
            _gxFile.Name = _fi2.Name;
            _gxFile.Path = _fi2.FullName;
            _gxFile.XMLText = _gxObj.XMLObj;

            List<object> _ret = new List<object>();
            _ret.Add(_gxObj);
            _gxFile.Objetos2 = _ret;

            _FilesGXOpen.Add(_gxFile);

            ReloadTreeview();
        }
    }
}