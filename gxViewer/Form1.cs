using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using gxViewer.GxObject;

namespace gxViewer
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            AbrirArchivo();
        }

        #region Methods

        private void AbrirArchivo()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo Distribuido de Genexus";
            ofd.Filter = FilesGX.GetFillesPrmitid(); //"Archivos XML (*.xml)|*.xml|Archivos XPW (*.xpw)|*.xpw";
            ofd.FilterIndex = 0;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                System.IO.FileInfo _info = new FileInfo(ofd.FileName);
                txtPath.Text = _info.FullName;

                if (_info.Extension.ToUpper().Equals(".XPW"))
                {
                    //XPWREADERLib.XPWtoXMLClass _xpw = new XPWREADERLib.XPWtoXMLClass();
                    XPWConverter.XPWReader _xpw = new XPWConverter.XPWReader();
                    string _newxmlfilename = String.Format("{0}\\temp.xml", _info.Directory.FullName);
                    _xpw.ConvertToXML(ofd.FileName, _newxmlfilename);
                    _xpw = null;

                    LeerArchivo(_newxmlfilename);

                    _info = new FileInfo(_newxmlfilename);
                    _info.Delete();
                }
                else
                {
                    LeerArchivo(_info.FullName);
                }
                
            }
            ofd.Dispose();
        }

        private void LeerArchivo(string pathname)
        {
            FileInfo _finfo = new FileInfo(pathname);
            if (_finfo.Exists)
            {
                
                //ExportFile _obj = Functions.Deserialize<ExportFile>(_finfo.FullName);
                //txtCode.AppendText(_obj.Model.Name);
                XmlDocument _xmldoc = GxXML.GetDocument(pathname);
                
                LoadXMLInObject<WorkPanel>(_xmldoc);
                LoadXMLInObject<Procedure>(_xmldoc);

                /*
                XmlNode _elem = GxXML.ReadElement(_xmldoc, "Procedure");
                
                //GXObject.WorkPanel _wpn = new GXObject.WorkPanel();
                GXObject.WorkPanel _wpn = Functions.DeserializeXML<GXObject.WorkPanel>(_elem.OuterXml);
                //txtDocumentation.Text = _wpn.Documentation.Source;
                txtRules.Text = _wpn.Rules;
                txtCode.Text = _wpn.Events;
                
                //HtmlDocument _htmdoc = new HtmlDocument();
                //_htmdoc.Body.InnerHtml = _wpn.Documentation.Source;

                //HtmlDocument _htmhelp = new HtmlDocument();
                //_htmhelp.Body.InnerHtml = _wpn.Help.Source;
                wbhelp.DocumentText = _wpn.Help.Source;
                wbdoc.DocumentText = _wpn.Documentation.Source;
                */



            }
        }


        private void LoadXMLInObject<T>(XmlDocument _xmldoc)
        {
            T _obj = Activator.CreateInstance<T>();
            string[] _names = _obj.GetType().ToString().Split(".".ToCharArray());
            string _nameObject = _names[_names.Length-1];


            XmlNode _elem = GxXML.ReadElement(_xmldoc, _nameObject);

            if (_elem != null)
            {
                _obj = Functions.DeserializeXML<T>(_elem.OuterXml);
                //string xxx = (_obj as GXObj).Source;

                txtRules.Text = (_obj as GXObj).Rules;
                txtCode.Text = (_obj as GXObj).Source;


                wbhelp.DocumentText = (_obj as GXObj).Help.Source;
                wbdoc.DocumentText = (_obj as GXObj).Documentation.Source;
                Application.DoEvents();
                /* Cargamos las variables */
                XmlNodeList _vars = GxXML.ReadElements(_elem, "Variable");
                List<Variable> _lstvars = new List<Variable>();
                foreach (XmlNode n in _vars)
                {
                    Variable _v = GxXML.XMLNodeToObject<Variable>(n);
                    _lstvars.Add(_v);
                }

                (_obj as GXObj).Variables = _lstvars.ToArray();

                
            }


            

 
        }



        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo Distribuido de Genexus";
            ofd.Filter = "Archivos XPZ (*.xpz)|*.xpz";
            ofd.FilterIndex = 0;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                List<string> _files = ZipGX.ExtraerArchivos(ofd.FileName);

            }
            ofd.Dispose();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo Distribuido de Genexus";
            ofd.Filter = "Archivos XPW (*.xpw)|*.xpw";
            ofd.FilterIndex = 0;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {

                string _xpwstr = System.IO.File.ReadAllText(ofd.FileName, Encoding.Default);
                txtRules.Text = _xpwstr;

                List<string> _tags = GXConstants.GetTags();
                string _ret = _xpwstr;

                //StringBuilder _strb = new StringBuilder();
                

                foreach (string s in _tags)
                {
                    string _oldstrtag = String.Format(":{0}",s);
                    string _newstrtag = String.Format("<{0}>",s);

                    string _oldendtag = String.Format(":e{0}",s);
                    string _newendtag = String.Format("</{0}>",s);

                    _ret = _ret.Replace(_oldstrtag, _newstrtag);
                    _ret = _ret.Replace(_oldendtag, _newendtag);
                    
                }


                txtConditions.Text = String.Format("<ExportFile>\n{0}</ExportFile>", _ret);
                XmlDocument _xdoc = GxXML.GetDocument2(txtConditions.Text);
                




            }
            ofd.Dispose();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}