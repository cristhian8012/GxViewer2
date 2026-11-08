using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace JTextEditor
{
    public partial class frmTextPlainEditor : Form
    {
        FileInfo _ftxT = null;
        string _txtOriginal = String.Empty;
        bool _bConvert = false;
        DataTable _dtEncoders = new DataTable();
        //List<Encoding> ArrayEncoders = new List<Encoding>();
        Encoding _originalEncoding = null;

        public void SetTitle(string _Title)
        {
            this.Text = String.Format("TxtFileEditor | {0}", _Title);
        }

        public void LoadString(string _strXML)
        {
            txtXML.Text = _strXML;
        }

        public frmTextPlainEditor(string _FileName)
        {
            _ftxT = new FileInfo(_FileName);
            InitializeComponent();
        }

        public frmTextPlainEditor()
        {
            InitializeComponent();
            
        }

        private void frmTextPlainEditor_Load(object sender, EventArgs e)
        {
            //txtXML.ShowLineNumbers = false;
            this.LoadEncoders();

            if (_ftxT != null)
            {
                if (_ftxT.Exists)
                {
                    //string _AllTXT = System.IO.File.ReadAllText(_ftxT.FullName, Encoding.Default);
                    //txtXML.OpenFile(_ftxT.FullName, Encoding.Default);
                    LoadFile(_ftxT.FullName);
                }
            }
            else
            {
                cboEncoders.SelectedValue = Encoding.Default.CodePage;
            }

            verNumeroDeLineasToolStripMenuItem.Checked = txtXML.ShowLineNumbers;
            ajusteDeLineaToolStripMenuItem.Checked = txtXML.WordWrap;
        }

        private void LoadEncoders()
        {
            //tsddpEncoders.DropDownItems.Clear();
            //tsslEncoder.DropDownItems.Clear();
            cboEncoders.Items.Clear();
            //ArrayEncoders = Encoding.GetEncodings();
            //foreach (EncodingInfo ei in Encoding.GetEncodings())
            //{
            //    AddEncoder(ei.GetEncoding());
            //}
            //List<string> _lstencoders = new List<string>();
            DataTable _dtEncoders = new DataTable();
            _dtEncoders.Columns.Add("Index", typeof(int));
            _dtEncoders.Columns.Add("Nombre", typeof(string));
            _dtEncoders.Columns.Add("Orden", typeof(int));
            _dtEncoders.Columns.Add("Encoder", typeof(Encoding));

            EncodingInfo[] Lsteninfo = Encoding.GetEncodings();
            string _Filter = String.Empty;
            for (int i = 0; i < Lsteninfo.Length; i++)
            {
                Encoding _enc = Lsteninfo[i].GetEncoding();
                DataRow _r = _dtEncoders.NewRow();
                _r["Index"] = _enc.CodePage;
                if (_enc == Encoding.ASCII || _enc.CodePage == Encoding.Default.CodePage || _enc == Encoding.Unicode || _enc == Encoding.UTF32 || _enc == Encoding.UTF7 || _enc == Encoding.UTF8)
                {
                    if (_enc.CodePage == Encoding.Default.CodePage) { _r["Nombre"] = "ANSI [" + _enc.HeaderName.ToUpper() + "]"; _r["Orden"] = 1;  }
                    if (_enc == Encoding.ASCII) { _r["Nombre"] = "ASCII"; _r["Orden"] = 2; }
                    if (_enc == Encoding.UTF8) { _r["Nombre"] = "UTF-8"; _r["Orden"] = 3; }
                    if (_enc == Encoding.UTF7) { _r["Nombre"] = "UTF-7"; _r["Orden"] = 4;  }
                    if (_enc == Encoding.UTF32) { _r["Nombre"] = "UTF-32"; _r["Orden"] = 5; }
                    if (_enc == Encoding.Unicode) { _r["Nombre"] = "UTF-16(Unicode)"; _r["Orden"] = 6; }
                    _Filter = String.Format("{0},{1}", _Filter, _enc.CodePage);
                }
                else
                {
                    _r["Orden"] = 10;
                    _r["Nombre"] = _enc.EncodingName; 
                }
                _r["Encoder"] = _enc;
                _dtEncoders.Rows.Add(_r);
            }

            DataView _dv = _dtEncoders.DefaultView;
            _dv.Sort = "Orden asc, Nombre ASC";

            cboEncoders.DataSource = _dv.ToTable();
            cboEncoders.DisplayMember = "Nombre";
            cboEncoders.ValueMember = "Index";
            
            DataView _dv2 = _dtEncoders.DefaultView;
            _dv2.RowFilter = "Index in (" + _Filter.Substring(1) +  ")";
            _dv.Sort = "Orden asc, Nombre ASC";
            _dv2.RowStateFilter = DataViewRowState.CurrentRows;

            DataTable _dt2 = _dv2.ToTable();
            DataRow _rii = _dt2.NewRow();
            _rii["Index"] = -1;
            _rii["Nombre"] = "-- Automatico --";
            _dt2.Rows.InsertAt(_rii, 0);

            cboOpenEncoders.DataSource = _dt2;
            cboOpenEncoders.DisplayMember = "Nombre";
            cboOpenEncoders.ValueMember = "Index";
            cboOpenEncoders.SelectedIndex = 0;
            
            _bConvert = true;
        }

        //private void AddEncoder(string Name, Encoding enc)
        //{
        //    //ToolStripItem tsi = tsddpEncoders.DropDownItems.Add(enc.EncodingName);
        //    //ArrayEncoders.Add(enc);
        //    cboEncoders.Items.Add("[" + enc.CodePage.ToString().PadLeft(5,' ') + " - " + enc.WindowsCodePage.ToString().PadLeft(4,' ') + " ]" + enc.HeaderName.ToUpper());
        //    //ToolStripItem tsi = tsslEncoder.DropDownItems.Add(enc.EncodingName);
        //    //tsi.Tag = enc;
        //    //tsi.Click += Tsi_Click;
        //}

        //private void Tsi_Click(object sender, EventArgs e)
        //{
        //    string _initEncoder = tsslEncoder.Text;
        //    ToolStripItem tsi = (ToolStripItem)sender;
        //    Encoding enc = tsi.Tag as Encoding;

        //    if (!String.IsNullOrEmpty(_initEncoder))
        //    {
        //        Encoding encsourse = tsslEncoder.Tag as Encoding; 
        //        if (!String.IsNullOrEmpty( _txtOriginal))
        //        {
        //            txtXML.Text = ConvertEncoding(_txtOriginal, encsourse, enc);
        //        }
        //    }

        //    tsslEncoder.Text = enc.EncodingName;
        //    tsslEncoder.Tag = enc;
        //}

        private string ConvertEncoding(string _txt, Encoding encSource, Encoding encDest)
        { 
            // Convert the string into a byte array.
            byte[] souceBytes = encSource.GetBytes(_txt);
            
            // Perform the conversion from one encoding to the other.
            byte[] destBytes = Encoding.Convert(encSource, encDest, souceBytes);

            //Convert the new byte[] into a char[] and then into a string.
            char[] asciiChars = new char[encDest.GetCharCount(destBytes, 0, destBytes.Length)];
            encDest.GetChars(destBytes, 0, destBytes.Length, asciiChars, 0);
            string asciiString = new string(asciiChars);
            return asciiString;
        }

        private void irAToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void verLaAyudaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void acercaDeXCAREditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
           //frmAboutBox _frm = new frmAboutBox();
           // _frm.ShowDialog(this);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTextPlainEditor _xNewForm = new frmTextPlainEditor();
            _xNewForm.Show();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo Texto";
            ofd.Filter = Program.FilesGX.GetFillesPrmitid("xml,xsd,xlt,xhtml,html,htm,txt,*"); 
            ofd.FilterIndex = 0;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                LoadFile(ofd.FileName);
            }
            ofd.Dispose();
            
        }

        private void LoadFile(string pfilename)
        {
            _bConvert = false;
            //txtXML.Clear();
            _ftxT = new FileInfo(pfilename);
            Encoding _encOp = null;
            if (cboOpenEncoders.Items.Count > 0)
            {
                if (cboOpenEncoders.SelectedItem != null && cboOpenEncoders.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    DataRow r = (cboOpenEncoders.SelectedItem as DataRowView).Row;
                    if (!r["Index"].ToString().Equals("-1") )
                    {
                        _encOp = r["Encoder"] as Encoding;
                    }
                }
            }
            StreamReader _read = null;
            if (_encOp != null)
            {
                _read = new StreamReader(_ftxT.FullName, _encOp);
                //txtXML.OpenFile(_ftxT.FullName, _encOp);
            }
            else
            {
                _read = new StreamReader(_ftxT.FullName, true);
                //txtXML.OpenFile(_ftxT.FullName);
            }
            
            tsstCodeFile.Text = String.Format("Length: {3}  FileEncoding : {0} - Code Page: {1} -  Windows Code Page: {2}", _read.CurrentEncoding.EncodingName, _read.CurrentEncoding.CodePage, _read.CurrentEncoding.WindowsCodePage, _read.BaseStream.Length);
            cboEncoders.SelectedValue = _read.CurrentEncoding.CodePage;
            _originalEncoding = _read.CurrentEncoding;

            //StreamReader _read = new StreamReader(_ftxT.FullName, Encoding.Default);
            _txtOriginal = _read.ReadToEnd();
            _read.Dispose();
            _read = null;
            string _StringTXT = _txtOriginal;

            string _ext = _ftxT.Extension.ToLower();

            if (_ext.Contains("xml") || _ext.Contains("xsd") || _ext.Contains("xlt"))
            {
                System.Xml.Linq.XDocument _doc = System.Xml.Linq.XDocument.Parse(_StringTXT);
                _StringTXT = _doc.ToString();
                _doc = null;
            }
            else
            {
                if (_ext.Contains("htm"))
                {
                    txtXML.Language = FastColoredTextBoxNS.Language.HTML;
                }
            }

            //this.Refresh();
            this.SetTitle(_ftxT.FullName);
            this.LoadString(_StringTXT);
            //txtXML.IsChanged = false;
            //txtXML.Refresh();

            _bConvert = true;

        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (_ftxT == null)
            {
                guardarComoToolStripMenuItem_Click(null, null);
                return;
            }
            else
            {
                _ftxT = new FileInfo(_ftxT.FullName);
            }

            if (_ftxT.Exists)
            {
                _ftxT.Delete();
                Application.DoEvents();
            }
            this.SaveFileTxt(_ftxT.FullName);

            
        }


        private void SaveFileTxt(string _FileName)
        {
            
            
            Encoding enc = Encoding.Default;
            if (cboEncoders.Items.Count > 0)
            {
                if (cboEncoders.SelectedIndex >=0)
                {
                    DataRow r = (cboEncoders.SelectedItem as DataRowView).Row;
                    //enc = ArrayEncoders[cboEncoders.SelectedIndex];
                    enc = (Encoding)r["Encoder"];
                }
            }
            
            using (StreamWriter sw = new StreamWriter(_FileName, true, enc))
            {
                IList<string> _TextQ = new List<string>();
                _TextQ = txtXML.Lines;
                foreach (string sline in _TextQ)
                {
                    sw.WriteLine(sline);
                }
                sw.Close();
                txtXML.IsChanged = false;
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog _sfd = new SaveFileDialog();
            _sfd.Filter = Program.FilesGX.GetFillesPrmitid("xml,xsd,xlt,xhtml,html,htm,txt,*");
            _sfd.FilterIndex = 0;

            if (_sfd.ShowDialog(this)== DialogResult.OK)
            {
                _ftxT = new FileInfo(_sfd.FileName);

                if (_ftxT.Exists)
                {
                    
                    if (MessageBox.Show("El archivo ya existe, ¿ Desea reemplazarlo ?",this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)== DialogResult.OK)
                    {
                        _ftxT.Delete();
                    }
                    Application.DoEvents();
                }
                this.SaveFileTxt(_ftxT.FullName);
            }

        }

        private void configurarPaginaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                txtXML.Print(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!txtXML.IsChanged)
            { this.Close(); }
            else
            {
                if (MessageBox.Show("¿Esta seguro de cerrar sin guardar los cambios ?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Close();
                }
            }
        }

        private void colapsarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtXML.CollapseAllFoldingBlocks();
            
        }

        private void colapsarSeleccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtXML.CollapseBlock(txtXML.Selection.Start.iLine, txtXML.Selection.End.iLine);
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeSyntaxHighLight(FastColoredTextBoxNS.Language.XML);
        }

        private void hTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeSyntaxHighLight(FastColoredTextBoxNS.Language.HTML);
        }

        private void genexusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeSyntaxHighLight(FastColoredTextBoxNS.Language.Genexus);
        }

        private void rPGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeSyntaxHighLight(FastColoredTextBoxNS.Language.Lua); //Falta RPG
        }

        private void javaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeSyntaxHighLight(FastColoredTextBoxNS.Language.JS);
        }

        private void cToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.ChangeSyntaxHighLight(FastColoredTextBoxNS.Language.CSharp);
        }

        private void vBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeSyntaxHighLight(FastColoredTextBoxNS.Language.VB);
        }

        private void pHPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeSyntaxHighLight(FastColoredTextBoxNS.Language.PHP);
        }

        private void sQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeSyntaxHighLight(FastColoredTextBoxNS.Language.SQL);
        }

        private void ChangeSyntaxHighLight( FastColoredTextBoxNS.Language _lang)
        {
            FastColoredTextBoxNS.SyntaxHighlighter _syntax = new FastColoredTextBoxNS.SyntaxHighlighter();
            txtXML.Language = _lang;
            txtXML.ClearStylesBuffer();
            this.txtXML.OnSyntaxHighlight(new FastColoredTextBoxNS.TextChangedEventArgs(this.txtXML.Range));
            txtXML.Refresh();
        }


        private void buscarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtXML.ShowFindDialog();
        }

        private void irAToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtXML.ShowGoToDialog();
        }

        private void reemplazarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtXML.ShowReplaceDialog();
        }

        private void buscarSiguienteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void seleccionarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtXML.SelectAll();
        }

        private void fechaYHoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtXML.InsertText( DateTime.Now.ToString("dd/MM/yyyy hh:m1:ss"));
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void cboEncoders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_bConvert && cboEncoders.Items.Count > 0)
            {
                try
                {
                    if (_originalEncoding != null)
                    {
                        //Encoding encDest = ArrayEncoders[cboEncoders.SelectedIndex];
                        DataRow r = (cboEncoders.SelectedItem as DataRowView).Row;
                        //enc = ArrayEncoders[cboEncoders.SelectedIndex];
                        Encoding encDest = (Encoding)r["Encoder"];
                        if (!String.IsNullOrEmpty(_txtOriginal))
                        {
                            txtXML.Text = ConvertEncoding(_txtOriginal, _originalEncoding, encDest);
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cboOpenEncoders_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmTextPlainEditor_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void frmTextPlainEditor_DragDrop(object sender, DragEventArgs e)
        {
            // Comprobar si el tipo de datos es el que queremos
            if (e.Data.GetDataPresent("FileName", false))
            {
                string[] _FilesDrop = (string[])(e.Data.GetData("FileDrop", false));
                foreach (string sfile in _FilesDrop)
                {
                    LoadFile(sfile);
                    break;
                }
            }
        }

        private void ajusteDeLineaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtXML.WordWrap = !txtXML.WordWrap;
            ajusteDeLineaToolStripMenuItem.Checked = txtXML.WordWrap;
        }

        private void verNumeroDeLineasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtXML.ShowLineNumbers = !txtXML.ShowLineNumbers;
            verNumeroDeLineasToolStripMenuItem.Checked = txtXML.ShowLineNumbers;
        }

        private void txtXML_DragDrop(object sender, DragEventArgs e)
        {
            frmTextPlainEditor_DragDrop(sender, e);
        }

        private void txtXML_KeyUp(object sender, KeyEventArgs e)
        {
            ShowActiveLine();
        }

        private void txtXML_Click(object sender, EventArgs e)
        {
            ShowActiveLine();
        }

        private void ShowActiveLine()
        {
            int _Line = txtXML.Selection.FromLine;
            int _Col = txtXML.Selection.End.iChar;
            int _Selected = txtXML.SelectionLength;
            tsslStatusLine.Text = String.Format("Line : {0}   Col : {1}   Selected : {2} ", _Line+1, _Col+1, _Selected);
        }

    }
}