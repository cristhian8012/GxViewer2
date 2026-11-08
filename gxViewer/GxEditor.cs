using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using gxViewer.GxObject;
using System.Xml;
using static gxViewer.GxModelExplorer;
using FastColoredTextBoxNS;
using static System.Windows.Forms.LinkLabel;
using gxViewer.GxObject.SDT;
using System.IO;
using System.Xml.Serialization;
using Ionic.Zip;
using ICSharpCode.SharpZipLib.GZip;
using System.Text.RegularExpressions;
using System.Runtime.Remoting.Messaging;
using Genexus_Definition_HtmlBuilder;
using System.Web.UI.WebControls;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace gxViewer
{
    public partial class GxEditor : UserControl
    {
        private string _PathSaveCode = String.Empty;
        private string _XMLSTR = String.Empty;
        private string _xmlSpec = String.Empty;
        private int _CurLineCode = 0;

        private object _objetoGX;
        private string _text;
        private DataTable _dtvars;
        private DataTable _dtattrs;
        private Dictionary<string, string> _TiposObjectsAbrev = new Dictionary<string, string>();
        public string TextDocumentation = "";
        private string _strLastNavigationPath = String.Empty;
        private bool _procRoutines = false;

        private string _HTMLSpec = String.Empty;
        private GXObj _ObjectGXEditor = null;

        private void LoadTypesObjects()
        {
            _TiposObjectsAbrev.Add("Transaction", "trn");
            _TiposObjectsAbrev.Add("Procedure", "prc");
            _TiposObjectsAbrev.Add("Report", "rpt");
            _TiposObjectsAbrev.Add("Menu", "mnu");
            _TiposObjectsAbrev.Add("WorkPanel", "wkp");
            _TiposObjectsAbrev.Add("DataView", "dv");
            
            _TiposObjectsAbrev.Add("Prompt", "pmt");
            _TiposObjectsAbrev.Add("WebPanel", "wbp");
            _TiposObjectsAbrev.Add("ExternalProgram", "XPG");
            
            _TiposObjectsAbrev.Add("StyleTransaction", "strn");
            _TiposObjectsAbrev.Add("StyleProcedure", "sprc");
            _TiposObjectsAbrev.Add("StyleReport", "srpt");
            _TiposObjectsAbrev.Add("StyleWorkPanel", "swkp");
            _TiposObjectsAbrev.Add("StylePrompt", "spmt");
            _TiposObjectsAbrev.Add("StyleWebPanel", "swbp");

            _TiposObjectsAbrev.Add("SDT", "sdt");

            _TiposObjectsAbrev.Add("Attribute", "att");
            _TiposObjectsAbrev.Add("Domain", "dom");
        }


        public override string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public object objetoGX
        {
            get { return _objetoGX; }
            set { _objetoGX = value; }
        }

        public GXObj GXObj
        {
            get { return (GXObj)_objetoGX; }
        }

        public GxEditor(string pFilenameGXV)
        {
            InitializeComponent();
            try
            {
                _PathSaveCode = pFilenameGXV;
                if (_objetoGX == null)
                {
                    string _base64Object = System.IO.File.ReadAllText(pFilenameGXV, Encoding.Default);
                    _objetoGX = Functions.String64ToObject(_base64Object);
                    PrepareObject();
                }
                tbObjeto.SelectedTab = tabCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }


        public GxEditor(string _XMLpath, object _tipo)
        {
            _XMLSTR = _XMLpath;
            InitializeComponent();

            //
            System.Threading.Thread _LeeObj = new System.Threading.Thread(PrepareObject);

            try
            {
                _LeeObj.IsBackground = true;
                _LeeObj.Priority = System.Threading.ThreadPriority.Highest;
                _LeeObj.Start();
            }
            catch (Exception ex)
            {
                _LeeObj.Abort();
                _LeeObj = null;
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public GxEditor()
        {
            InitializeComponent();
            FastColoredTextBoxNS.SyntaxHighlighter _syntax = new FastColoredTextBoxNS.SyntaxHighlighter();

            this.txtRules.Language = FastColoredTextBoxNS.Language.Genexus;
            this.txtRules.SyntaxHighlighter.IgnoreIFScafold = true;
            this.txtRules.OnSyntaxHighlight(new FastColoredTextBoxNS.TextChangedEventArgs(this.txtRules.Range));

            this.txtConditions.SyntaxHighlighter.IgnoreIFScafold = true;
            this.txtConditions.Language = FastColoredTextBoxNS.Language.Genexus;
            this.txtConditions.OnSyntaxHighlight(new FastColoredTextBoxNS.TextChangedEventArgs(this.txtConditions.Range));

            //_syntax.HighlightSyntax( 

            this.Load += new EventHandler(GxEditor_Load);
        }

       

        void GxEditor_Load(object sender, EventArgs e)
        {
            this.PrepareObject();
            if (tbObjeto.TabPages.ContainsKey("tabCode"))
            {
                tbObjeto.SelectedTab = tabCode;
            }
            
        }

        private void paneltop_Paint(object sender, PaintEventArgs e)
        {

        }


        private void PrepareObject()
        {
            if (_objetoGX != null)
            {
                GxObject.GXObj _obj = (GxObject.GXObj)_objetoGX;
                _ObjectGXEditor = _obj;
                lblTipoObjeto.AutoSize = true;
                lblTipoObjeto.Text = _obj.Tipo.Replace("gxViewer.GxObject.GXObj", "Objeto");
                lblTipoObjeto.Refresh();
                lblNombreObjeto.Left = lblTipoObjeto.Right;
                lblNombreObjeto.Text = _obj.Info.Name;
                lblNombreObjeto.AutoSize = true;
                lblNombreObjeto.Refresh();
                lbldescrip.Text = _obj.Info.Description;
                lbldescrip.AutoSize = true;
                lbldescrip.Left = lblNombreObjeto.Right;

                //Que vaya cargando el LastNavigation
                try
                {
                    System.Threading.Thread backgroundThread = new System.Threading.Thread(GetXMLSpec2);
                    backgroundThread.IsBackground = true;
                    backgroundThread.Start();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.Write(ex.InnerException.Message);
                    }
                }

                if (_obj.Tipo == "StructureDataType")
                {
                    //tbObjeto.TabPages.RemoveByKey("tabLayout");
                    tbObjeto.TabPages.RemoveByKey("tabConditions");
                    tbObjeto.TabPages.RemoveByKey("tabCode");
                    tbObjeto.TabPages.RemoveByKey("tabRules");
                    tbObjeto.TabPages.RemoveByKey("tabStructure");
                    tbObjeto.TabPages.RemoveByKey("tabHelp");

                    this.treeListView1.CanExpandGetter = delegate (Object x)
                    {
                        return (x is SdtMainTreeview) || (x is SdtChildTreeview);
                    };

                    this.treeListView1.ChildrenGetter = delegate (Object x)
                    {
                        if (x is SdtMainTreeview)
                            return ((SdtMainTreeview)x).ChildItems;
                        if (x is SdtChildTreeview)
                            return ((SdtChildTreeview)x).ChildItems;

                        throw new ArgumentException("Error al seleccionar Items");
                    };

                    XmlSerializer serializer = new XmlSerializer(typeof(Types));
                    string xml = (_obj as StructureDataType).SDTXml.Source;

                    using (StringReader reader = new StringReader(xml))
                    {
                        var test = (Types)serializer.Deserialize(reader);
                        List<SdtMainTreeview> _lista = HelperSDT.getTreeview(test);
                        this.treeListView1.SetObjects(_lista);
                        this.treeListView1.ExpandAll();
                    }

                }
                else
                {
                    tbObjeto.TabPages.RemoveByKey("tabStructureSDT");

                    if (_obj.Tipo == "Transaction")
                    {
                        tbObjeto.TabPages.RemoveByKey("tabLayout");
                        Transaction _trn = ((Transaction)_obj);
                        wbStructure.DocumentText = GxEditorHelper.Transaction.GetHTMLStruct(_trn, imageList1);
                    }
                    else
                    {
                        tbObjeto.TabPages.RemoveByKey("tabStructure");
                        string _strHtml = @"<BR><BR><div>No se ha trabajado esta opcion</div>";


                        switch (_obj.Tipo)
                        {
                            case "WorkPanel":
                                WorkPanel _wpn = ((WorkPanel)_obj);
                                if (_wpn.Form == null)
                                {
                                    GxEditorHelper.WorkPanel.PrepareLayoutXML(_wpn);
                                }
                                _strHtml = GxEditorHelper.WorkPanel.GetHTMLDesign(_wpn, imageList1);
                                break;

                            case "WebPanel":
                                WebPanel _webpn = ((WebPanel)_obj);
                                if (_webpn.HTMLForm == null)
                                {
                                    GxEditorHelper.WebPanel.PrepareLayoutXML(_webpn);
                                }
                                 _strHtml = GxEditorHelper.WebPanel.GetHTMLDesign(_webpn, imageList1);

                                if (_webpn.Propiedades.ContainsKey("USER_METADATA"))
                                {
                                    Property _PropUserMetadata = _webpn.Propiedades["USER_METADATA"];
                                    XmlTextReader reader = new XmlTextReader(new StringReader(_PropUserMetadata.Value));
                                    reader.MoveToContent();
                                    string str2 = "";
                                    switch (reader.GetAttribute("format"))
                                    {
                                        case "escaped":
                                            str2 = Functions.FromEscapedString(reader.ReadInnerXml());
                                            break;

                                        case "compressed64":
                                            {
                                                GZipInputStream stream = new GZipInputStream(new MemoryStream(Convert.FromBase64String(reader.ReadInnerXml())));
                                                str2 = new StreamReader(stream).ReadToEnd();
                                                break;
                                            }

                                        case "xml":
                                            str2 = reader.ReadInnerXml();
                                            break;
                                    }
                                    txtDesignDefinitions.Text = Functions.BeautifyXML("<root>" + str2.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;","&") + "</root>").Replace("&amp;", "&");
                                    txtLayoutSource.Language = Language.XML;
                                    reader.Close();
                                    reader = null;
                                }

                                break;

                            case "Procedure":
                                Procedure _prc = ((Procedure)_obj);
                                _strHtml = GxEditorHelper.Procedure.GetHTMLDesign(_prc, imageList1);
                                break;


                            case "Report":
                                Report _rpt = ((Report)_obj);
                                _strHtml = GxEditorHelper.Report.GetHTMLDesign(_rpt, imageList1);
                                break;

                            default:
                                break;
                        }

                        

                        if (!String.IsNullOrEmpty(txtDesignDefinitions.Text))
                        {
                            var parser = new GenexusXmlParser();
                            WebUiDefinition def = parser.Parse(txtDesignDefinitions.Text);
                            var generator = new HtmlGenerator();
                            string html = generator.Generate(def, _obj);

                            webDesign.DocumentText = html;
                        }
                        else
                        {
                            webDesign.DocumentText = _strHtml;
                        }

                        webDesign.Visible = true;
                         
                        //if (!String.IsNullOrEmpty(_strHtml))
                        //{
                        //    txtLayoutSource.Text = _strHtml;
                        //    txtLayoutSource.Language = Language.HTML;
                        //}
                        

                    }

                }

                if (String.IsNullOrEmpty(txtDesignDefinitions.Text))
                {
                    tabControlLayout.TabPages.Remove(tabLayoutDefnition);
                }

                if (String.IsNullOrEmpty(txtLayoutSource.Text))
                {
                    tabControlLayout.TabPages.Remove(tabLayoutSource);
                }

                if (_obj.Documentation != null)
                {
                    string _source = _obj.Documentation.Source;
                    if (!String.IsNullOrEmpty(_source))
                    {
                        _source = _source.Replace("\n\r", "<br>").Replace("\r\n", "<br>");
                        wbdoc.DocumentText = GxEditorHelper.GetHTMLDocumentation(_source);
                    }
                }

                if (_obj.Help != null)
                {
                    string _sourceH = _obj.Help.Source;
                    if (!String.IsNullOrEmpty(_sourceH))
                    {
                        _sourceH = _sourceH.Replace("\n\r", "<br>").Replace("\r\n", "<br>");
                        wbhelp.DocumentText = GxEditorHelper.GetHTMLDocumentation(_sourceH);
                    }
                }


                txtRules.Text = _obj.Rules;

                txtCode.Text = _obj.Source;
                if (_obj.Subroutines != null)
                {
                    txtCode.AppendText(_obj.Subroutines);
                }

                txtConditions.Text = _obj.Conditions;

                if (_obj.Variables != null)
                {
                    CargarVariables(_obj);
                    txtFindVars.TextChanged += new EventHandler(txtFindVars_TextChanged);
                }

                if (_obj.GXAtts != null)
                {
                    dgvAttrs.AutoGenerateColumns = false;
                    dgvAttrs.DataSource = Functions.ConvertToDataTable(_obj.GXAtts);
                    groupBoxAttrs.Text = String.Format("Listado de atributos ({0})", _obj.GXAtts.Length);
                    txtFindAttr.TextChanged += new EventHandler(txtFindAttr_TextChanged);
                }

                PropertyGrid propertyGrid = new PropertyGrid();
                propertyGrid.CommandsVisibleIfAvailable = true;
                propertyGrid.LargeButtons = false;
                propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
                propertyGrid.Location = new System.Drawing.Point(12, 12);
                propertyGrid.Name = String.Format("propertyGrid1{0}", lblNombreObjeto.Text);
                propertyGrid.SelectedObject = _obj;
                propertyGrid.Dock = DockStyle.Fill;
                propertyGrid.Text = "Propiedades";
                tabProp.ImageIndex = 8;
                tabProp.Controls.Add(propertyGrid);
            }
            else
            {
                //tbObjeto.TabPages.RemoveByKey("tabLayout");
                tbObjeto.TabPages.RemoveByKey("tabStructure");
                tbObjeto.TabPages.RemoveByKey("tabStructureSDT");
                tbObjeto.TabPages.RemoveByKey("tabHelp");
                tbObjeto.TabPages.RemoveByKey("tabDocument");

            }
 
        }

        private void CargarVariables(GXObj _obj)
        {
            if (_obj.Variables != null)
            {
                dgvVars.AutoGenerateColumns = false;
                foreach (Variable v in _obj.Variables)
                {
                    if (!String.IsNullOrEmpty(v.BasedOn) && v.Length <= 0)
                    {
                        if (!v.BasedOn.Contains("Domain:"))
                        {
                            if (_obj.GXAtts != null)
                            {
                                if (_obj.GXAtributos.Count > 0)
                                {

                                    GxObject.GXAtt _att = _obj.GXAtributos[v.BasedOn.Replace("Attribute:", "")];
                                    if (_att != null)
                                    {
                                        v.Length = _att.Tamanio;
                                        v.Decimals = _att.Decimals;
                                        if (String.IsNullOrEmpty(v.Picture))
                                        {
                                            v.Picture = _att.Picture;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }



                DataTable _dtpvars = Functions.ConvertToDataTable(_obj.Variables);
                //_dtpvars.Columns.Add("NombreVar", typeof(string), "IIF(Rows > 0, ( IIF(Columns > 0, Name + ' (' + Rows + ',' + Columns + ')'  , Name + ' (' + Rows + ')' )  )  ,Name)");
                //_dtpvars.Columns.Add("NombreType", typeof(string), "IIF(Decimales > 0, Type + ' (' + Length + '.' + Decimales + ')'  , Type + ' (' + Length + ')' )");

                dgvVars.DataSource = _dtpvars;
                groupBoxVars.Text = String.Format("Listado de variables ({0})", _obj.Variables.Length);
            }
        }

        public DataTable getVarsTable()
        {
            DataTable _ret = (DataTable)dgvVars.DataSource;
            if (_ret != null)
            {
                System.Data.DataView _dv = _ret.DefaultView;
                _dv.Sort = "Name ASC";
                _ret = _dv.ToTable();
            }
            
            return _ret;
        }
        public string getLayoutString()
        {
            string _retLayout = String.Empty; // "(version peliminar...)";
            WebBrowser _wb = null;
            if (tbObjeto.TabPages.ContainsKey("tabLayout"))
            {
                _wb = webDesign;
            }
            else
            {
                if (tbObjeto.TabPages.ContainsKey("tabStructure"))
                {
                    _wb = wbStructure;
                }
            }


            //_retLayout = _retLayout + "\r\n" + _wb.Document.Body.InnerText;
            _retLayout = _retLayout + getStringFromDocument(_wb.Document, "");

            return _retLayout;
        }


        public string getDocumentationString()
        {
            return getStringFromDocument(wbdoc.Document, "SIN DOCUMENTACIÓN");

            //string _retdocumentation = String.Empty;
            //StringBuilder _strDocum = new StringBuilder();
            //var pattern = String.Format("(</?(?!{0})[^<>]*(?<!{0})>)", "B");

            //string _strDocument = "SIN DOCUMENTACIÓN";
            //try
            //{
            //    _strDocument = (wbdoc.Document == null ? "": wbdoc.Document.Body.InnerHtml);
            //}
            //catch (Exception ex)
            //{
            //    _strDocument = "";
            //}
            //string[] _linesdocum = _strDocument.Split( new string[] { "<br>", "</br>", "<BR>", "</BR>" }, StringSplitOptions.RemoveEmptyEntries );
            //if (_linesdocum.Length > 0)
            //{
            //    for (int i = 0; i < _linesdocum.Length; i++)
            //    {
            //        var result = Regex.Replace(_linesdocum[i].Replace("&nbsp;", " "), pattern, "");
            //        _strDocum.AppendLine(  result );
            //    }
                
            //}

            //if (_strDocum.Length > 0)
            //{
            //    _retdocumentation = _strDocum.ToString();
            //}
            //else
            //{
            //    try
            //    {
            //        _retdocumentation = _retdocumentation + "\r\n" + wbdoc.Document.Body.InnerText;
            //    }
            //    catch (Exception ex)
            //    {
            //        _retdocumentation = _strDocument;
            //    }
            //}

            //return _retdocumentation;
        }


        public string getStringFromDocument(HtmlDocument pDoc, string pDefault)
        {
            string _retstringHtmlFixed = String.Empty;
            StringBuilder _strDocum = new StringBuilder();
            var pattern = String.Format("(</?(?!{0})[^<>]*(?<!{0})>)", "B");

            string _strDocument = pDefault;//"SIN DOCUMENTACIÓN";
            try
            {
                _strDocument = (pDoc == null ? "" : pDoc.Body.InnerHtml);
            }
            catch (Exception ex)
            {
                _strDocument = "";
            }
            string[] _linesdocum = _strDocument.Split(new string[] { "<br>", "</br>", "<BR>", "</BR>" }, StringSplitOptions.RemoveEmptyEntries);
            if (_linesdocum.Length > 0)
            {
                for (int i = 0; i < _linesdocum.Length; i++)
                {
                    var result = Regex.Replace(_linesdocum[i].Replace("&nbsp;", " ").Replace("&amp;","&"), pattern, "");
                    _strDocum.AppendLine(result);
                }

            }

            if (_strDocum.Length > 0)
            {
                _retstringHtmlFixed = _strDocum.ToString();
            }
            else
            {
                try
                {
                    _retstringHtmlFixed = _retstringHtmlFixed + "\r\n" + pDoc.Body.InnerText;
                }
                catch (Exception ex)
                {
                    _retstringHtmlFixed = _strDocument;
                }
            }

            return _retstringHtmlFixed;
        }




        void txtFindAttr_TextChanged(object sender, EventArgs e)
        {
            if (_dtattrs == null) { _dtattrs = (DataTable)dgvAttrs.DataSource; }
            System.Data.DataView _dv = _dtattrs.DefaultView;
            _dv.RowFilter = String.Format("Name LIKE '%{0}%'", txtFindAttr.Text.Replace("'", String.Empty).Replace("\"", String.Empty));
            _dv.RowStateFilter = DataViewRowState.CurrentRows;
            DataTable _dtfiltrado = _dv.ToTable();
            dgvAttrs.DataSource = _dtfiltrado;
            groupBoxAttrs.Text = String.Format("Listado de atributos ({0})", _dtfiltrado.Rows.Count);
        }

        void txtFindVars_TextChanged(object sender, EventArgs e)
        {
            if (_dtvars == null) { _dtvars = (DataTable)dgvVars.DataSource; }
            System.Data.DataView _dv = _dtvars.DefaultView;
            _dv.RowFilter = String.Format("Name LIKE '%{0}%'", txtFindVars.Text.Replace("'",String.Empty).Replace("\"",String.Empty));
            _dv.RowStateFilter = DataViewRowState.CurrentRows;
            DataTable _dtfiltrado = _dv.ToTable();
            dgvVars.DataSource = _dtfiltrado;
            groupBoxVars.Text = String.Format("Listado de variables ({0})", _dtfiltrado.Rows.Count);

        }
        

        private void tsbClose_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage _tab = (TabPage)this.Parent;
                //if (MessageBox.Show( String.Format("¿Esta seguro de Cerrar el Objeto \"{0}\" ?",_tab.Text), "GxViewer",  MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                TabControl _tabMain = (TabControl)_tab.Parent;
                _tabMain.TabPages.Remove(_tab);
                _tabMain.SelectedIndex = _tabMain.TabPages.Count - 1;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsbValidadorGX_Click(object sender, EventArgs e)
        {
            try
            {
                GXObj _objGX = (GXObj)_objetoGX;
                //_objGX.setLayoutText(this.getLayoutString());
                _objGX.setDocumentationText(this.getDocumentationString());
                _objGX.SetLinesCode(txtCode.Lines);
                _objGX.SetLinesRule(txtRules.Lines);

                GxValidador.ValidatorGX _objValidator = new GxValidador.ValidatorGX(_objGX);
                GxValidador.frmValidator _fVal = new GxValidador.frmValidator(_objValidator);

                _fVal.Show(this);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }   

            
            
        }

        private void verificarVariablesNoUtilizadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<Variable> _varsNoUsed = GxValidador.ValidatorGX.VariablesNoUsadas(this.GXObj);
                if (_varsNoUsed != null)
                {
                    List<Variable> _lstVars = new List<Variable>();
                    _lstVars.AddRange(this.GXObj.Variables);
                    XmlDocument _docXml = new XmlDocument();
                    _docXml.LoadXml(this.GXObj.XMLObj);

                    XmlNodeList _xmlvariables = GxXML.ReadElements(_docXml, "Variable");

                    foreach (Variable v in _varsNoUsed)
                    {

                        foreach (XmlNode _v in _xmlvariables)
                        {
                            Variable vXML = (Variable)GxXML.XMLNodeToObject(_v);
                            if (vXML.Name == v.Name)
                            {
                                _v.ParentNode.RemoveChild(_v);
                                //_docXml.RemoveChild(_v);
                                break;
                            }
                        }
                        _lstVars.Remove(v);
                    }

                    this.GXObj.XMLObj = _docXml.OuterXml;
                    _docXml = null;
                    this.GXObj.Variables = _lstVars.ToArray();
                    string _mensaje = String.Format("Se eliminaron {0} variables del objeto {1}:{2}", _varsNoUsed.Count.ToString(),  this.GXObj.Tipo, this.GXObj.Name);
                    MessageBox.Show(_mensaje, "Eliminar Variables no utilizadas",  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarVariables(this.GXObj);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eliminar Variables no utilizadas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void verXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ( _objetoGX != null)
            {
                GXObj _obj = ((GXObj)_objetoGX);
                frmViewXML _fXML = new frmViewXML();
                
                string _titulo = String.Format("{0} : {1} ({2})",lblTipoObjeto.Text,lblNombreObjeto.Text,lbldescrip.Text);
                _fXML.SetTitle(_titulo);

                string _XMLObject = gextXMLFormattExport();

                if (_obj.IsXML)
                {
                    _fXML.LoadString(_XMLObject, Language.XML);
                }
                else
                {
                    _fXML.LoadString(_XMLObject, Language.HTML);
                }


                try
                {
                    string _filename = String.Format("GXV_Exp_{0}." + (_obj.IsXML ? "xml" : "html"), _obj.GetName());
                    _fXML.SetFileNameDefault(_filename);
                    _fXML.Show(this);
                    //_fXML.ShowDialog();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                

            }
        }

        public string gextXMLFormattExport()
        {
            if (_objetoGX != null)
            {
                GXObj _obj = ((GXObj)_objetoGX);
                string _XMLObject = String.Empty;

                if (_obj.IsXML)
                {
                    string _xmlKMV = Functions.Serialize<KMW>(_obj.KMW, true);
                    string _xmlModel = Functions.Serialize<Model>(_obj.Model, true);

                    StringBuilder _strbXML = new StringBuilder();
                    _strbXML.AppendLine("<ExportFile>");
                    _strbXML.AppendLine(_xmlModel);
                    _strbXML.AppendLine(_xmlKMV);
                    _strbXML.AppendLine("<GXObject>");
                    _strbXML.AppendLine(_obj.XMLObj);
                    _strbXML.AppendLine("</GXObject>");
                    _strbXML.AppendLine("</ExportFile>");
                    _XMLObject = _strbXML.ToString();
                    _strbXML = null;
                    System.Xml.Linq.XDocument _doc = System.Xml.Linq.XDocument.Parse(_XMLObject);
                    _XMLObject = "<?xml version='1.0' encoding='iso-8859-1'?>\r\n" + _doc.ToString();
                }
                else
                {
                    _XMLObject = _obj.XMLObj;
                }

                return _XMLObject;
            }
            else
            {
                return String.Empty;
            }
        }

        private FastColoredTextBoxNS.FastColoredTextBox GetMainTXT(object sender)
        {
            ToolStripItem item = (ToolStripItem)sender;
            FastColoredTextBoxNS.FastColoredTextBox _txtret = null;

            if (item != null) 
            { 
                ContextMenuStrip owner = item.Owner as ContextMenuStrip; 
                if (owner != null) 
                {
                    _txtret = (FastColoredTextBoxNS.FastColoredTextBox)owner.SourceControl ;
                } 
            }
            return _txtret;
        }


        private void colapsarSeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FastColoredTextBoxNS.FastColoredTextBox _txt = GetMainTXT(sender);
            if (_txt != null)
            {
                _txt.CollapseBlock(_txt.Selection.Start.iLine, _txt.Selection.End.iLine);
            }
        }

        private void colapsarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FastColoredTextBoxNS.FastColoredTextBox _txt = GetMainTXT(sender);
            if (_txt != null)
            {
                _txt.CollapseAllFoldingBlocks();
            }
        }

        private void asigmarMarcadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FastColoredTextBoxNS.FastColoredTextBox _txt = GetMainTXT(sender);
            if (_txt != null)
            {
                _txt.BookmarkLine(_txt.Selection.Start.iLine);
            }
        }

        private void desAsignarMarcadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FastColoredTextBoxNS.FastColoredTextBox _txt = GetMainTXT(sender);
            if (_txt != null)
            {
                _txt.UnbookmarkLine(_txt.Selection.Start.iLine);
            }
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FastColoredTextBoxNS.FastColoredTextBox _txt = GetMainTXT(sender);
            if (_txt != null)
            {
                _txt.ShowFindDialog();
            }
        }

        private void reemplazarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FastColoredTextBoxNS.FastColoredTextBox _txt = GetMainTXT(sender);
            if (_txt != null)
            {
                _txt.ShowReplaceDialog();
            }
        }

        private void irALineaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FastColoredTextBoxNS.FastColoredTextBox _txt = GetMainTXT(sender);
            if (_txt != null)
            {
                _txt.ShowGoToDialog();
            }
        }

        private void seleccionarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FastColoredTextBoxNS.FastColoredTextBox _txt = GetMainTXT(sender);
            if (_txt != null)
            {
                _txt.SelectAll();
            }
        }


        private void buscarObjetoEnElModeloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> _ListObjects = new List<string>() { String.Format("{0}:{1}", _TiposObjectsAbrev[this.GXObj.Tipo], this.GXObj.Name) };
                int _IdModel = 2;
                if (this.GXObj.Model != null)
                {
                    if (this.GXObj.Model.Id != null)
                    {
                        _IdModel = int.Parse(this.GXObj.Model.Id);
                    }
                }

                GXBaseKB.KnowledgeBase.KB.Open2(this.GXObj.KMW.Path);
                GXBaseKB.KnowledgeBase.KB.ModelID = _IdModel ; 
                XmlDocument _xdoc = GXBaseKB.KnowledgeBase.KB.DistributeObjects(_ListObjects.ToArray());
                string _ModeloName = (new System.IO.DirectoryInfo(this.GXObj.KMW.Path)).Name;

                string _Name = String.Format("{0}-{1}", _ModeloName, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                bool _ret = frmMain.CurrentInstance.OpenXMLString(_xdoc.OuterXml, _Name);
                if (_ret)
                {
                    frmMain.CurrentInstance.InvokeOpenGXObjectSelected();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Distribuir Objetos GX", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Control)
            {
                if ( e.KeyCode == Keys.S)
                {
                    SaveCurrentObject();
                }
            }


        }

        private void SaveCurrentObject()
        {
            if (_objetoGX != null)
            {

                if (String.IsNullOrEmpty(_PathSaveCode))
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "GXViewer files (*.GXV)|*.GXV";
                    sfd.FilterIndex = 0;
                    sfd.RestoreDirectory = true;
                    sfd.AddExtension = true;
                    //sfd.CheckFileExists = true;
                    sfd.FileName = this.GXObj.GetName();
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        _PathSaveCode = sfd.FileName;
                    }
                    else
                    {
                        return;
                    }
                }

                GXObj _gx = (GXObj)_objetoGX;
                _gx.Source = txtCode.Text;
                _gx.Rules = txtRules.Text;
                if (wbdoc.Document != null)
                {
                    _gx.Documentation.Source = wbdoc.Document.Body.OuterHtml;
                }
                _gx.Conditions = txtConditions.Text;

                //string _text = Functions.Serialize<GXObj>(_gx);
                string _text = Functions.ObjectToString64(_gx);
                System.IO.File.WriteAllText(_PathSaveCode, _text, Encoding.Default);
            }
            else
            {
                if (String.IsNullOrEmpty(_PathSaveCode))
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "GXViewer files (*.GXV)|*.GXV";
                    sfd.FilterIndex = 0;
                    sfd.RestoreDirectory = true;
                    sfd.AddExtension = true;
                    sfd.FileName = "ObjectsGX";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        _PathSaveCode = sfd.FileName;
                        FileInfo _fi = new FileInfo(_PathSaveCode);

                        GXObj _gx = new GXObj();
                        //_gx.Name = _fi.Name.Replace(_fi.Extension, "");

                        GxObject.Info _info = new gxViewer.GxObject.Info();
                        _info.Name = _fi.Name.Replace(_fi.Extension, "");
                        _info.Description = $"Objeto GX {_info.Name} creado desde GXViewer";
                        _gx.LastUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        _gx.CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        GxObject.KMW _kmw = new gxViewer.GxObject.KMW();
                        _kmw.Path = _fi.DirectoryName;

                        GxObject.Model _model = new gxViewer.GxObject.Model();
                        _model.Id = "1";
                        _model.Type = "TestGxViewer";

                        _gx.KMW = _kmw;
                        _gx.Model = _model;
                        _gx.Info = _info;

                        _gx.Source = txtCode.Text;
                        _gx.Rules = txtRules.Text;
                        _gx.Conditions = txtConditions.Text;
                        string _text = Functions.ObjectToString64(_gx);
                        System.IO.File.WriteAllText(_PathSaveCode, _text, Encoding.Default);

                    }
                    else
                    {
                        return;
                    }
                }

            }
            
        }

        private void GxEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.S)
                {
                    SaveCurrentObject();
                }
            }
        }

        private void dgvVars_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control)
            {
                if (e.KeyCode == Keys.V)
                {
                    //System.IO.MemoryStream _me = Clipboard.GetData( DataFormats.Locale) as System.IO.MemoryStream;
                    //string _data = Encoding.Default.GetString(_me.ToArray());
                    //MessageBox.Show(_data);


                }
            }
        }

        private void guardarObjetoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCurrentObject();
        }

        private void compararEnBeyondCompareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage _tab = (TabPage)this.Parent;
                frmSelectionToCompare _frm = new frmSelectionToCompare((TabControl)_tab.Parent);
                _frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void wbdoc_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                if (wbdoc.Document.Body != null)
                {
                    this.TextDocumentation = wbdoc.Document.Body.OuterText;
                }
            }
            catch (Exception)
            {
                this.TextDocumentation = String.Empty;
            }
        }

        private void lastNavigationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Threading.Thread _LeeObj = new System.Threading.Thread(LoadLastNavigationSubProcess);
            string _text = lastNavigationToolStripMenuItem.Text;
            try
            {
                if (tbObjeto.TabPages.ContainsKey("tabSpec"))
                {
                    tbObjeto.SelectedTab = tbObjeto.TabPages["tabSpec"];
                    return;
                }

                lastNavigationToolStripMenuItem.Enabled = false;
                _LeeObj.IsBackground = true;                
                _LeeObj.Priority = System.Threading.ThreadPriority.Highest;
                _LeeObj.Start();

                while (_LeeObj.IsAlive)
                {
                    Random _rand = new Random(5);
                    string _xxx = " ".PadLeft(_rand.Next(1,5)).Replace(" ", ".") ;
                    lastNavigationToolStripMenuItem.Text = _text + " (Cargando"+ _xxx + ")";
                    Application.DoEvents();
                    //this.Refresh();
                }

                if (!String.IsNullOrEmpty(_HTMLSpec))
                {
                    WebBrowser _wbEspec = new WebBrowser();
                    _wbEspec.DocumentText = _HTMLSpec;
                    _wbEspec.ScriptErrorsSuppressed = true;
                    _wbEspec.ScrollBarsEnabled = true;
                    AddTabMain("tabSpec", "Especificación", _wbEspec, "spec");
                }


            }
            catch (Exception ex)
            {
                _LeeObj.Abort();
                _LeeObj = null;
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lastNavigationToolStripMenuItem.Enabled = true;
                lastNavigationToolStripMenuItem.Text = _text;
            }
            //LoadLastNavigationSubProcess();
        }

        private void LoadLastNavigationSubProcess()
        {
            try
            {
                if (!tbObjeto.TabPages.ContainsKey("tabSpec"))
                {
                    string _xmlSpx = GetXMLSpec();
                    string _tmpmodelpath = String.Empty;
                    if (String.IsNullOrEmpty(_xmlSpec))
                    {
                        if (GXObj.KMW != null)
                        {
                            if (GXObj.KMW.Path != null)
                            {
                                _tmpmodelpath = GXObj.KMW.Path;
                            }
                        }
                        if (String.IsNullOrEmpty(_tmpmodelpath))
                        {
                            if (MessageBox.Show("No se ha encontrado la ruta de la KB\n¿Deseas Ingresar el Modelo?", "Ubicar Ruta KB", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                            {
                                _tmpmodelpath = Functions.InputBox("Ingrese Nombre del Modelo KB", "Nombre o Ruta KB", String.Empty);
                                _xmlSpx = GetXMLSpec(_tmpmodelpath);
                            }
                        }
                    }

                    if (!String.IsNullOrEmpty(_xmlSpx))
                    {
                        string _atributtes = String.Empty;
                        string _xslTemplate = Functions.getStringFile(Application.StartupPath + "\\gxxml", "spec.xsl", ref _atributtes);
                        if (!String.IsNullOrEmpty(_xslTemplate))
                        {
                            _HTMLSpec = Functions.TransformXMLToHTML(_xmlSpx, _xslTemplate);
                        }
                        else
                        {
                            MessageBox.Show("No se encuentra la plantilla: gxxml\\spec.xsl", "Especificar objeto GX", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se ha encontrado el archivo de especificación para \nel objeto: " + GXObj.GetName() + "\nKB: " + _tmpmodelpath, "Especificar objeto GX", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    tbObjeto.SelectedTab = tbObjeto.TabPages["tabSpec"];
                }
            }
            catch (Exception ex)
            {
                string _msg = ex.Message;
                if (ex.InnerException != null) { _msg = _msg + "\n" + ex.InnerException.Message; }
                MessageBox.Show(_msg, "Especificar objeto GX", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private TabPage AddTabMain(string _tabName, string _tabText, Control c, string _KeyImage)
        {
            TabPage _newtab = null;
            if (!tbObjeto.TabPages.ContainsKey(_tabName))
            {
                c.Dock = DockStyle.Fill;
                _newtab = new TabPage(_tabText);
                _newtab.Name = _tabName;
                _newtab.ImageIndex = imageList1.Images.IndexOfKey(_KeyImage);

                _newtab.Controls.Add(c);
                tbObjeto.TabPages.Add(_newtab);
                tbObjeto.SelectedTab = _newtab;
            }
            else
            {
                int _idx = tbObjeto.TabPages.IndexOfKey(_tabName);
                _newtab = tbObjeto.TabPages[_idx];
                tbObjeto.SelectedTab = _newtab;
            }
            return _newtab;
        }

        public void GetXMLSpec2()
        {
            try
            {
                lastNavigationToolStripMenuItem.Enabled = false;
                GetPathXMLSpec();
                lastNavigationToolStripMenuItem.Enabled = true;
            }
            catch { }
        }

        private void GetPathXMLSpec(string pModelPath = "")
        {
            string _model = (String.IsNullOrEmpty(pModelPath) ? GXObj.KMW.Path : pModelPath);
            string _pathModel = String.Empty;
            if (Functions.getModelPath(_model, out _pathModel))
            {
                string _pattern = String.Format("{0}.xml", GXObj.GetName());
                string _pathFind = _pathModel + "\\GXSPC*";
                _strLastNavigationPath =  Functions.getStringPathFile(_pathFind, _pattern);
            }
        }

        private string GetXMLSpec(string pModelPath="")
        {
            if (String.IsNullOrEmpty(_strLastNavigationPath))
            {
                GetPathXMLSpec(pModelPath);
            }

            if (String.IsNullOrEmpty(_strLastNavigationPath))
            {
                MessageBox.Show("Error al buscar el XML de Especificacion del objeto: " + GXObj.GetName());
                return String.Empty;
            }

            if (String.IsNullOrEmpty(_xmlSpec) )
            {
                System.IO.FileInfo _fi = new System.IO.FileInfo(_strLastNavigationPath);
                string _attributes = "GET";
                string _tmpxml = Functions.getStringFile(_fi.Directory.FullName, _fi.Name, ref _attributes);

                if (!String.IsNullOrEmpty(_tmpxml))
                {
                    Console.WriteLine("Archivo OK");
                    string[] _strAttr = _attributes.Split("|".ToCharArray());
                    string _date = _strAttr[0].Replace("Fecha=", String.Empty);
                    string _UserName = _strAttr[1].Replace("Usuario=", String.Empty);

                    string _xmlSpectmp = "<?xml version='1.0'?>\r\n\t<ObjSpecs>\r\n\t<gxpath><![CDATA[{0}]]>\\</gxpath>\r\n\t<gxdate><![CDATA[{2}]]></gxdate>\r\n\t<gxowner><![CDATA[{3}]]></gxowner>{1}</ObjSpecs>";
                    _xmlSpec = String.Format(_xmlSpectmp, Application.StartupPath.Replace("\\", "/"), _tmpxml, _date, _UserName);
                }
                else
                {
                    Console.WriteLine("No encontró Archivo XML de Especificación");
                }
            }
            return _xmlSpec;
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetMainTXT(sender).Copy();
        }

        private void cortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetMainTXT(sender).Cut();
        }

        private void txtCode_TextChangedDelayed(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            try
            {
                _procRoutines = false;
                LoadEventsAndRoutines();
                if (txtCode.DelayedTextChangedInterval.Equals(300))
                {
                    txtCode.DelayedTextChangedInterval = 3000;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadEventsAndRoutines()
        {
            Dictionary<int, string> _listEvents = new Dictionary<int, string>();
            Dictionary<int, string> _listRoutines = new Dictionary<int, string>();

            bool _inBlockComment = false;

            for (int i = 0; i < txtCode.Lines.Count; i++)
            {
                string _line = txtCode.Lines[i].Trim();

                // --- 1. Manejo de comentarios de bloque /* ... */ ---
                if (_inBlockComment)
                {
                    if (_line.Contains("*/"))
                    {
                        _inBlockComment = false;
                        _line = _line.Substring(_line.IndexOf("*/") + 2).Trim();
                    }
                    if (_inBlockComment || string.IsNullOrEmpty(_line)) continue;
                }

                if (_line.StartsWith("//")) continue;

                if (_line.Contains("/*"))
                {
                    int closeIdx = _line.IndexOf("*/");
                    if (closeIdx > -1) _line = _line.Substring(closeIdx + 2).Trim();
                    else { _inBlockComment = true; continue; }
                }

                if (string.IsNullOrEmpty(_line)) continue;

                // --- 2. Identificar si es Evento o Subrutina ---
                bool isEvent = false;
                bool isSub = false;
                string _tmpName = "";

                if (_line.StartsWith("Event", StringComparison.OrdinalIgnoreCase))
                {
                    isEvent = true;
                    _tmpName = _line.Substring(5).TrimStart(); // Quita "Event"
                }
                else if (_line.StartsWith("Sub", StringComparison.OrdinalIgnoreCase))
                {
                    isSub = true;
                    _tmpName = _line.Substring(3).TrimStart(); // Quita "Sub"
                }

                if (!isEvent && !isSub) continue;

                // --- 3. Extraer el nombre y validar si hay tecla de función (Solo Eventos) ---
                string _cleanName = "";
                string _functionKey = "";
                bool _hasQuotes = false;

                if (_tmpName.Length > 0 && (_tmpName[0] == '\'' || _tmpName[0] == '"'))
                {
                    _hasQuotes = true;
                    char quoteChar = _tmpName[0];
                    int endQuoteIdx = _tmpName.IndexOf(quoteChar, 1);

                    if (endQuoteIdx > -1)
                    {
                        _cleanName = _tmpName.Substring(1, endQuoteIdx - 1).Trim();

                        // Si es un evento y hay texto después de la comilla de cierre, buscamos si es un número (Tecla de función)
                        if (isEvent && _tmpName.Length > endQuoteIdx + 1)
                        {
                            string _afterQuote = _tmpName.Substring(endQuoteIdx + 1).Trim();

                            // Si el siguiente caracter es un número (ej: 'Ver Familiares PEP' 8)
                            if (_afterQuote.Length > 0 && char.IsDigit(_afterQuote[0]))
                            {
                                // Extraemos el número completo (por si fuera F10, F11, etc.)
                                int j = 0;
                                while (j < _afterQuote.Length && char.IsDigit(_afterQuote[j]))
                                {
                                    j++;
                                }
                                string numStr = _afterQuote.Substring(0, j);

                                // Formateamos como F + número
                                _functionKey = " [F" + numStr + "]";
                            }
                        }
                    }
                    else
                    {
                        // No hay comilla de cierre, tomamos todo (caso raro)
                        _cleanName = _tmpName.Substring(1).Trim('\'', '"').Trim();
                    }
                }
                else
                {
                    // No tiene comillas (ej: Event Start // $...)
                    // Buscamos el primer comentario o espacio para cortar
                    int cutIndex = -1;
                    int idxSpace = _tmpName.IndexOf(' ');
                    int idxComilla = _tmpName.IndexOf("'");
                    int idxSlashSlash = _tmpName.IndexOf("//");
                    int idxBlock = _tmpName.IndexOf("/*");

                    if (idxSpace > -1) cutIndex = idxSpace;
                    if (idxComilla > -1 && (cutIndex == -1 || idxComilla < cutIndex)) cutIndex = idxComilla;
                    if (idxSlashSlash > -1 && (cutIndex == -1 || idxSlashSlash < cutIndex)) cutIndex = idxSlashSlash;
                    if (idxBlock > -1 && (cutIndex == -1 || idxBlock < cutIndex)) cutIndex = idxBlock;

                    if (cutIndex > -1)
                        _cleanName = _tmpName.Substring(0, cutIndex).Trim();
                    else
                        _cleanName = _tmpName.Trim();
                }

                if (string.IsNullOrEmpty(_cleanName)) continue;

                // --- 4. Agregar a las listas ---
                if (isEvent)
                    _listEvents.Add(i + 1, _functionKey + _cleanName);
                else if (isSub)
                    _listRoutines.Add(i + 1, _cleanName);
            }

            // --- ASIGNAR A LOS COMBOBOX ---

            // EVENTOS
            if (_listEvents.Count > 0)
            {
                cboEvents.Enabled = true;
                BindingSource _bdiEvt = new BindingSource(_listEvents, null);
                _bdiEvt.Insert(0, new KeyValuePair<int, string>(0, "(General)"));

                if (cboEvents.DataSource == null || ((BindingSource)cboEvents.DataSource).Count != _bdiEvt.Count)
                {
                    cboEvents.DataSource = _bdiEvt;
                    cboEvents.DisplayMember = "Value";
                    cboEvents.ValueMember = "Key";
                    cboEvents.SelectedIndex = 0;
                }
            }
            else
            {
                cboEvents.Enabled = false;
                cboEvents.DataSource = null;
            }

            // SUBRUTINAS
            if (_listRoutines.Count > 0)
            {
                cboRoutines.Enabled = true;
                BindingSource _bdiRoutines = new BindingSource(_listRoutines, null);
                _bdiRoutines.Insert(0, new KeyValuePair<int, string>(0, "(General)"));

                if (cboRoutines.DataSource == null || ((BindingSource)cboRoutines.DataSource).Count != _bdiRoutines.Count)
                {
                    cboRoutines.DataSource = _bdiRoutines;
                    cboRoutines.DisplayMember = "Value";
                    cboRoutines.ValueMember = "Key";
                    cboRoutines.SelectedIndex = 0;
                }
            }
            else
            {
                cboRoutines.Enabled = false;
                cboRoutines.DataSource = null;
            }

            // Mostrar u ocultar el panel
            panelEvents.Visible = cboRoutines.Enabled || cboEvents.Enabled;
        }

        private void cboEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboEvents.SelectedValue==null || !_procRoutines)
                {
                    return;
                }

                int _val;
                if (int.TryParse(cboEvents.SelectedValue.ToString(), out _val))
                {
                    _val = _val - 1;
                    if (_val.Equals(-1)) { _val = 0; }
                    if (cboRoutines.DataSource != null && cboRoutines.Enabled)
                    {
                        cboRoutines.SelectedIndex = 0;
                    }
                    
                    int _len = txtCode.Lines[_val].Length;

                    txtCode.Selection = new FastColoredTextBoxNS.Range(txtCode, 0, _val, 0, _val);
                    txtCode.DoSelectionVisible();
                    // 2. Ahora aplicamos el resaltado hasta el final
                    txtCode.Selection = new FastColoredTextBoxNS.Range(txtCode, 0, _val, _len, _val);

                    txtCode.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboRoutines_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboRoutines.SelectedValue == null || !_procRoutines)
                {
                    return;
                }

                int _val;
                if (int.TryParse(cboRoutines.SelectedValue.ToString(), out _val))
                {
                    _val = _val - 1;

                    if (_val.Equals(-1)) { _val = 0; }
                    if (cboEvents.DataSource != null && cboEvents.Enabled)
                    {
                        cboEvents.SelectedIndex = 0;
                    }
                    int _len = txtCode.Lines[_val].Length;
                    //txtCode.Selection = new FastColoredTextBoxNS.Range(txtCode, 0, _val, _len, _val);
                    txtCode.Selection = new FastColoredTextBoxNS.Range(txtCode, 0, _val, 0, _val);
                    txtCode.DoSelectionVisible();
                    // 2. Ahora aplicamos el resaltado hasta el final
                    txtCode.Selection = new FastColoredTextBoxNS.Range(txtCode, 0, _val, _len, _val);

                    txtCode.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void cboEvents_MouseClick(object sender, MouseEventArgs e)
        {
            _procRoutines = true;
        }

        private void cboRoutines_MouseClick(object sender, MouseEventArgs e)
        {
            _procRoutines = true;
        }

        private void compilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //txtConditions.Text = getLayoutString();
        }

        private void txtCode_ToolTipNeeded(object sender, FastColoredTextBoxNS.ToolTipNeededEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.HoveredWord))
            {
                _CurLineCode = e.Place.iLine;
                if (_ObjectGXEditor != null)
                {
                    if (_ObjectGXEditor.Variables != null)
                    {
                        var r = new Range((FastColoredTextBox)sender, e.Place, e.Place);
                        string hoveredWord = r.GetFragment("[a-zA-Z0-9&_]").Text;

                        string _nameVar = hoveredWord; //e.HoveredWord;
                        //Console.WriteLine(_nameVar);
                        if (_nameVar.StartsWith("&"))
                        {
                            Variable _varGx = _ObjectGXEditor.getVarByName(_nameVar.Substring(1));
                            if (_varGx != null)
                            {
                                e.ToolTipTitle = $"    &{_varGx.NombreVar}\n     ";
                                e.ToolTipIcon = ToolTipIcon.Info;
                                e.ToolTipText = _varGx.getToolTipVar();
                            }
                        }


                        
                    }
                }
            }
        }

        private void txtRules_ToolTipNeeded(object sender, ToolTipNeededEventArgs e)
        {
            txtCode_ToolTipNeeded(sender, e);
        }

        private void txtCode_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                FastColoredTextBox _txt = sender as FastColoredTextBox;
                if (_txt.Selection.Start.iLine != 0 || _txt.Selection.GoRightThroughFolded())
                {
                    if (_txt.Selection.Start.iLine > 0)
                    {
                        _CurLineCode = _txt.Selection.Start.iLine;
                        if (_CurLineCode > 0)
                        {
                            string _linea = _txt.GetLineText(_CurLineCode);

                            if (_linea.Trim().StartsWith("//"))
                            {
                                return;
                            }

                            int _strPosComment = _linea.IndexOf('/');
                            if (_strPosComment > 0)
                            {
                                _linea = _linea.Substring(0, _strPosComment);
                            }

                            _linea = _linea.Trim();
                            if (!_linea.ToUpper().StartsWith("DO"))
                            {
                                return;
                            }

                            _linea = _linea.ToUpper().Replace("DO ", String.Empty).Replace("\"", String.Empty).Replace("\'", String.Empty).Trim();

                            if (cboRoutines.DataSource != null)
                            {
                                BindingSource _bdiRoutines = cboRoutines.DataSource as BindingSource;
                                if (_bdiRoutines != null)
                                {
                                    Dictionary<int, string> _listRoutines = _bdiRoutines.DataSource as Dictionary<int, string>;
                                    //for (int i = 0; i < _listRoutines.Count; i++)
                                    foreach (KeyValuePair<int, string> _routine in _listRoutines)
                                    {
                                        string _lineaRuotine = _routine.Value;
                                        if (_lineaRuotine.ToUpper().Equals(_linea))
                                        {
                                            //MessageBox.Show(_linea + "\n" + _routine.Key);
                                            //cboRoutines.SelectedValue = _routine.Key;
                                            _txt[_CurLineCode].LastVisit = DateTime.Now;
                                            //_txt.Navigate(_routine.Key-1);
                                            int _len = _txt[_routine.Key-1].Text.Length;
                                            _txt.Selection = new Range(_txt, 0, _routine.Key-1, _len, _routine.Key-1);
                                            _txt.DoSelectionVisible();
                                            _txt.Focus();
                                            Refresh();
                                            break;
                                        }
                                    }

                                }
                            }


                        }
                    }
                        
                }

                
            }
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            txtCode.NavigateBackward();
            txtCode.Focus();
        }

        private void btnFoward_Click(object sender, EventArgs e)
        {
            txtCode.NavigateForward();
            txtCode.Focus();
        }

        private void irAMetodoF12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtCode_KeyDown_1(txtCode, new KeyEventArgs(Keys.F12));
        }

        private void mnuForEachToSQL_Click(object sender, EventArgs e)
        {
            FastColoredTextBox _txt = GetMainTXT(sender);
            if (_txt.SelectedText != null)
            {
                string _select = _txt.SelectedText.ToUpper();
                if (_select.Contains("FOR ") && _select.Contains("ENDFOR"))
                {
                    string strResult = GxUtil.GenerateSQL_From_ForEach(_select);
                    if (!String.IsNullOrEmpty(strResult))
                    {
                        frmViewXML _fXML = new frmViewXML();
                        _fXML.LoadString(strResult, Language.SQL);
                        _fXML.SetTitle2("SQL Generado");
                        _fXML.ShowDialog(this);
                    }

                }
            }
        }

        private void cmsOpcionesTXT_Opening(object sender, CancelEventArgs e)
        {
            mnuForEachToSQL.Enabled = false;

            Control _parent = cmsOpcionesTXT.SourceControl;
            if (_parent.GetType() == typeof(FastColoredTextBox))
            {
                FastColoredTextBox _txt = (FastColoredTextBox)_parent;
                if (_txt.SelectedText != null)
                {
                    string _select = _txt.SelectedText.ToUpper();
                    if (_select.Contains("FOR ") && _select.Contains("ENDFOR"))
                    {
                        mnuForEachToSQL.Enabled = true;
                    }
                }
            }

            
        }
    }
}
