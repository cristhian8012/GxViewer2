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
using gxViewer.AS400;

namespace gxViewer
{
    public partial class AS400Editor : UserControl
    {
        private string _PathSaveCode = String.Empty;
        private int _CurLineCode = 0;

        private As400Object _objeto;
        private string _text;
        private DataTable _dtvars;
        private DataTable _dtattrs;
        private Dictionary<string, string> _TiposObjectsAbrev = new Dictionary<string, string>();
        public string TextDocumentation = "";
        private bool _procRoutines = false;


        public override string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public As400Object objeto
        {
            get { return _objeto; }
            set { _objeto = value; }
        }


        public AS400Editor()
        {
            InitializeComponent();
            this.Load += new EventHandler(AS400Editor_Load);
        }


        void AS400Editor_Load(object sender, EventArgs e)
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
            if (_objeto != null)
            {
                lblTipoObjeto.AutoSize = true;
                lblTipoObjeto.Text = _objeto.GetNameType() + " : ";
                lblTipoObjeto.Refresh();
                lblNombreObjeto.Left = lblTipoObjeto.Right;
                lblNombreObjeto.Text = _objeto.Name.ToUpper();
                lblNombreObjeto.AutoSize = true;
                lblNombreObjeto.Refresh();
                if (_objeto.Filename.Length > 60)
                {
                    lbldescrip.Text = _objeto.Filename.Substring(0, 60);
                }
                else
                {
                    lbldescrip.Text = _objeto.Filename;
                }
                
                lbldescrip.AutoSize = true;
                lbldescrip.Left = lblNombreObjeto.Right;

                if (!String.IsNullOrEmpty(_objeto.Ext))
                {
                    FileInfo _infoFileDefSyx = new FileInfo(Path.Combine(Application.StartupPath, $"{_objeto.Ext}_desc.xml"));
                    if (_infoFileDefSyx.Exists)
                    {
                        txtCode.Language = Language.Custom;
                        txtCode.DescriptionFile = _infoFileDefSyx.FullName;
                    }
                    else
                    {
                        if (_objeto.Type == AS400_Types.CL)
                        {
                            txtCode.Language = Language.Genexus;
                        }
                        else
                        {
                            if (_objeto.Type == AS400_Types.PF || _objeto.Type == AS400_Types.LF || _objeto.Type == AS400_Types.SQL)
                            {
                                txtCode.Language = Language.SQL;
                            }
                            else
                            {
                                txtCode.Language = Language.VB;
                            }
                        }
                    }

                    if (_objeto.Type == AS400_Types.COBOL || _objeto.Type == AS400_Types.RPG || _objeto.Type == AS400_Types.CL || _objeto.Type == AS400_Types.JCL)
                    {
                        txtCode.BackColor = Color.Black;
                        txtCode.ForeColor = Color.Lime;
                        txtCode.SelectionColor = Color.White;
                    }
                   
                    txtCode.Refresh();
                }

                txtCode.Text = _objeto.Source;

                if (_objeto.Variables != null)
                {
                    CargarVariables(_objeto);
                    txtFindVars.TextChanged += new EventHandler(txtFindVars_TextChanged);
                }

                //if (_obj.GXAtts != null)
                //{
                //    dgvAttrs.AutoGenerateColumns = false;
                //    dgvAttrs.DataSource = Functions.ConvertToDataTable(_obj.GXAtts);
                //    groupBoxAttrs.Text = String.Format("Listado de atributos ({0})", _obj.GXAtts.Length);
                //    txtFindAttr.TextChanged += new EventHandler(txtFindAttr_TextChanged);
                //}

            }
            
        }

        private void CargarVariables(As400Object _obj)
        {
            if (_obj.Variables != null)
            {
                dgvVars.AutoGenerateColumns = false;
                

                DataTable _dtpvars = Functions.ConvertToDataTable(_obj.Variables.ToArray());
                
                dgvVars.DataSource = _dtpvars;
                groupBoxVars.Text = String.Format("Listado de variables ({0})", _obj.Variables.Count);

            }
        }

        public DataTable getVarsTable()
        {
            DataTable _ret = (DataTable)dgvVars.DataSource;
            return _ret;
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

                _objeto.Source = txtCode.Text;
                if (_objeto.Type == AS400_Types.PF || _objeto.Type == AS400_Types.LF)
                {
                    _objeto.Documentation = AS400_Helper.getDocumentationPF(_objeto.Source);
                }
                else
                {
                    if (_objeto.Type == AS400_Types.CL || _objeto.Type == AS400_Types.JCL || _objeto.Type == AS400_Types.RPG || _objeto.Type == AS400_Types.COBOL)
                    {
                        _objeto.Variables = AS400_Helper.getVariablesCL(_objeto.Source);
                        _objeto.Documentation = AS400_Helper.getDocumentationCL(_objeto.Source);
                    }
                    
                }
                _objeto.setLinesCode(txtCode.Lines);

                GxValidador.ValidatorAS400 _objValidator = new GxValidador.ValidatorAS400(_objeto);
                GxValidador.frmAS400Validator _fVal = new GxValidador.frmAS400Validator(_objValidator);
                _fVal.Show(this);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            if (_objeto != null)
            {

                if (String.IsNullOrEmpty(_PathSaveCode))
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "GXViewer files (*.GXV)|*.GXV";
                    sfd.FilterIndex = 0;
                    sfd.RestoreDirectory = true;
                    sfd.AddExtension = true;
                    //sfd.CheckFileExists = true;
                    sfd.FileName = this.Name;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        _PathSaveCode = sfd.FileName;
                    }
                    else
                    {
                        return;
                    }
                }
                
                //GXObj _gx = (GXObj)_objetoGX;
                //_gx.Source = txtCode.Text;
                //_gx.Rules = txtRules.Text;
                //if (wbdoc.Document != null)
                //{
                //    _gx.Documentation.Source = wbdoc.Document.Body.OuterHtml;
                //}
                //_gx.Conditions = txtConditions.Text;

                //string _text = Functions.Serialize<GXObj>(_gx);
                //string _text = Functions.ObjectToString64(_gx);
                //System.IO.File.WriteAllText(_PathSaveCode, _text, Encoding.Default);                
            }
            
        }

        private void AS400Editor_KeyDown(object sender, KeyEventArgs e)
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
                frmSelectionToCompareAs400 _frm = new frmSelectionToCompareAs400((TabControl)_tab.Parent);
                _frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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

        public string getCurrentSource()
        {
            return txtCode.Text;
        }

        private void LoadEventsAndRoutines()
        {
            Dictionary<int, string> _listEvents = new Dictionary<int, string>();
            
            List<int> _linesEvents =  txtCode.FindLines("^(Event)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            foreach (int idx in _linesEvents)
            {
                string _line = txtCode.Lines[idx].Trim();
                if (!_line.StartsWith("//"))
                {
                    string _tmpEventName = _line.Replace("Event", String.Empty);
                    int _postComment = _tmpEventName.IndexOf("//");
                    if (_postComment.Equals( -1)) { _postComment = _tmpEventName.IndexOf("/*"); }

                    if (_postComment > -1)
                    {
                        _tmpEventName.Remove(_postComment);
                    }
                    
                    string[] strsplit = _tmpEventName.Split("'".ToCharArray());
                    if (strsplit.Length > 1)
                    {
                        _listEvents.Add(idx+1, String.Format("[F{0}] - {1}", strsplit[2].Trim(), strsplit[1].Trim()));
                    }
                    else
                    {
                        _listEvents.Add(idx+1, _tmpEventName.Trim());
                    }
                }
            }
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
            }
            

            Dictionary<int, string> _listRoutines = new Dictionary<int, string>();
            //cboRoutines.DataSource = null;
            
            List<int> _linesRoutines = txtCode.FindLines("^(Sub)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            foreach (int idx in _linesRoutines)
            {
                string _line = txtCode.Lines[idx].Trim();
                if (!_line.StartsWith("//"))
                {
                    string[] strsplit = _line.Split("'".ToCharArray());
                    if (strsplit.Length > 1)
                    {
                        _listRoutines.Add(idx+1, strsplit[1].Trim());
                    }
                }
            }
            if (_listRoutines.Count > 0)
            {
                BindingSource _bdiRoutines = new BindingSource(_listRoutines, null);
                _bdiRoutines.Insert(0, new KeyValuePair<int, string>(0, "(General)"));

                cboRoutines.Enabled = true;
                //_listRoutines.Add(-1, "(General)");

                if (cboRoutines.DataSource==null || ((BindingSource)cboRoutines.DataSource).Count != _bdiRoutines.Count)
                {
                    cboRoutines.DataSource = _bdiRoutines; //new BindingSource(_listRoutines, null);
                    cboRoutines.DisplayMember = "Value";
                    cboRoutines.ValueMember = "Key";
                    cboRoutines.SelectedIndex = 0;
                }
            }
            else
            {
                cboRoutines.Enabled = false;
            }

            if (!cboRoutines.Enabled && !cboEvents.Enabled)
            {
                panelEvents.Visible = false;
            }
            else
            {
                panelEvents.Visible = true;
            }

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
                    txtCode.Selection = new FastColoredTextBoxNS.Range(txtCode, 0, _val, _len, _val);
                    txtCode.DoSelectionVisible();
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
                    txtCode.Selection = new FastColoredTextBoxNS.Range(txtCode, 0, _val, _len, _val);
                    txtCode.DoSelectionVisible();
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
                //_CurLineCode = e.Place.iLine;
                //if (_ObjectAS400Editor != null)
                //{
                //    if (_ObjectAS400Editor.Variables != null)
                //    {
                //        var r = new Range((FastColoredTextBox)sender, e.Place, e.Place);
                //        string hoveredWord = r.GetFragment("[a-zA-Z0-9&_]").Text;

                //        string _nameVar = hoveredWord; //e.HoveredWord;
                //        //Console.WriteLine(_nameVar);
                //        if (_nameVar.StartsWith("&"))
                //        {
                //            Variable _varGx = _ObjectAS400Editor.getVarByName(_nameVar.Substring(1));
                //            if (_varGx != null)
                //            {
                //                e.ToolTipTitle = $"    &{_varGx.NombreVar}\n     ";
                //                e.ToolTipIcon = ToolTipIcon.Info;
                //                e.ToolTipText = _varGx.getToolTipVar();
                //            }
                //        }


                        
                //    }
                //}
            }
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
    }
}
