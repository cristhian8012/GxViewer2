using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace gxViewer
{
    public partial class frmViewXML : Form
    {
        FileInfo _ftxT = null;
        private string _nameDefaultSave = "";

        public void SetTitle(string _Title)
        {
            this.Text = String.Format("Visor XML | {0}", _Title);
        }

        public void SetTitle2(string _Title)
        {
            this.Text = String.Format("Visor {0} | {1}", txtXML.Language.ToString(),  _Title);
        }


        public void SetFileNameDefault(string _nameFile)
        {
            _nameDefaultSave = _nameFile;
        }

        public void LoadString(string _strXML)
        {
            System.Xml.Linq.XDocument _doc = System.Xml.Linq.XDocument.Parse(_strXML);
            txtXML.ServiceColors = new FastColoredTextBoxNS.ServiceColors();
            txtXML.Language = FastColoredTextBoxNS.Language.XML;
            txtXML.Text = _doc.ToString(); ;
            SetCheckedLanguage();
        }

        public void LoadString(string _strXML, FastColoredTextBoxNS.Language _lang)
        {
            txtXML.ServiceColors = new FastColoredTextBoxNS.ServiceColors();
            txtXML.Language = _lang;
            txtXML.Text = _strXML;
            SetCheckedLanguage();
            
        }


        public frmViewXML(string _FileName)
        {
            _ftxT = new FileInfo(_FileName);
            InitializeComponent();
        }

        public frmViewXML()
        {
            InitializeComponent();
            
        }

        private void frmViewXML_Load(object sender, EventArgs e)
        {
            this.LoadEncoders();

            if (_ftxT != null)
            {
                if (_ftxT.Exists)
                {
                    //string _AllTXT = System.IO.File.ReadAllText(_ftxT.FullName, Encoding.Default);
                    string _ext = _ftxT.Extension;
                    FastColoredTextBoxNS.Language _lang = FastColoredTextBoxNS.Language.XML;
                    if (_ext.Contains("xml") || _ext.Contains("xsd") || _ext.Contains("xlt"))
                    {
                        _lang = FastColoredTextBoxNS.Language.XML;
                    }
                    else
                    {
                        if (_ext.Contains("cs"))
                        {
                            _lang = FastColoredTextBoxNS.Language.CSharp;
                        }

                        if (_ext.Contains("gx"))
                        {
                            _lang = FastColoredTextBoxNS.Language.Genexus;
                        }

                        if (_ext.Contains("js"))
                        {
                            _lang = FastColoredTextBoxNS.Language.JS;
                        }

                        if (_ext.Contains("php"))
                        {
                            _lang = FastColoredTextBoxNS.Language.PHP;
                        }

                        if (_ext.Contains("sql"))
                        {
                            _lang = FastColoredTextBoxNS.Language.SQL;
                        }

                        if (_ext.Contains("vb") || _ext.Contains("bas") || _ext.Contains("cls"))
                        {
                            _lang = FastColoredTextBoxNS.Language.VB;
                        }

                        if (_ext.Contains("lua"))
                        {
                            _lang = FastColoredTextBoxNS.Language.Lua;
                        }

                        if (_ext.Contains("htm"))
                        {
                            _lang = FastColoredTextBoxNS.Language.HTML;
                        }

                    }

                    txtXML.Language = _lang;
                    txtXML.OpenFile(_ftxT.FullName, Encoding.Default);
                    SetCheckedLanguage();

                }
            }
        }

        private void LoadEncoders()
        {
            tsddpEncoders.DropDownItems.Clear();
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                AddEncoder(ei.GetEncoding());
            }
        }

        private void AddEncoder(Encoding enc)
        {
            ToolStripItem tsi = tsddpEncoders.DropDownItems.Add(enc.EncodingName);
            tsi.Tag = enc;
            tsi.Click += Tsi_Click;
        }

        private void Tsi_Click(object sender, EventArgs e)
        {
            string _initEncoder = tsslEncoder.Text;
            ToolStripItem tsi = (ToolStripItem)sender;
            Encoding enc = tsi.Tag as Encoding;

            if (!String.IsNullOrEmpty(_initEncoder))
            {
                Encoding encsourse = Encoding.GetEncoding(_initEncoder);
                txtXML.Text = ConvertEncoding(txtXML.Text, encsourse, enc);
            }

            tsslEncoder.Text = enc.EncodingName;
        }

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
            frmAboutBox _frm = new frmAboutBox();
            _frm.ShowDialog(this);
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmViewXML _xNewForm = new frmViewXML();
            _xNewForm.Show();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo XML";
            ofd.Filter = FilesGX.GetFillesPrmitid("xml,xsd,xlt,xhtml,html,htm,txt,*"); 
            ofd.FilterIndex = 0;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                //txtXML.Clear();
                _ftxT = new FileInfo(ofd.FileName);
                StreamReader _read = new StreamReader(_ftxT.FullName, true);
                tsstCodeFile.Text = String.Format("Length: {3}  FileEncoding : {0} - Code Page: {1} -  Windows Code Page: {2}", _read.CurrentEncoding.EncodingName, _read.CurrentEncoding.CodePage, _read.CurrentEncoding.WindowsCodePage, _read.BaseStream.Length);
                tsslEncoder.Text = _read.CurrentEncoding.EncodingName;

                //StreamReader _read = new StreamReader(_ftxT.FullName, Encoding.Default);
                string _StringTXT = _read.ReadToEnd();
                _read.Dispose();
                _read = null;

                string _ext = _ftxT.Extension.ToLower();
                FastColoredTextBoxNS.Language _lang = FastColoredTextBoxNS.Language.XML;

                if (_ext.Contains("xml") || _ext.Contains("xsd") || _ext.Contains("xlt"))
                {
                    System.Xml.Linq.XDocument _doc = System.Xml.Linq.XDocument.Parse(_StringTXT);
                    _StringTXT = _doc.ToString();
                    _doc = null;
                }
                else
                {
                    if (_ext.Contains("cs"))
                    {
                        _lang = FastColoredTextBoxNS.Language.CSharp;
                    }
                    
                    if (_ext.Contains("gx"))
                    {
                        _lang = FastColoredTextBoxNS.Language.Genexus;
                    }

                    if (_ext.Contains("js"))
                    {
                        _lang = FastColoredTextBoxNS.Language.JS;
                    }

                    if (_ext.Contains("php"))
                    {
                        _lang = FastColoredTextBoxNS.Language.PHP;
                    }

                    if (_ext.Contains("sql"))
                    {
                        _lang = FastColoredTextBoxNS.Language.SQL;
                    }

                    if (_ext.Contains("vb") || _ext.Contains("bas") || _ext.Contains("cls"))
                    {
                        _lang = FastColoredTextBoxNS.Language.VB;
                    }

                    if (_ext.Contains("lua"))
                    {
                        _lang = FastColoredTextBoxNS.Language.Lua;
                    }

                    if (_ext.Contains("htm"))
                    {
                        _lang = FastColoredTextBoxNS.Language.HTML;
                    }   
                }

                this.Refresh();
                this.SetTitle(_ftxT.FullName);
                this.LoadString(_StringTXT);

                if (txtXML.Language != _lang )
                {
                    txtXML.Language = _lang;
                }

                txtXML.IsChanged = false;
                txtXML.Refresh();
                this.SetCheckedLanguage();

            }
            ofd.Dispose();



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
            string strenc = tsslEncoder.Text.Trim();
            Encoding enc = Encoding.Default;
            if (!String.IsNullOrEmpty( strenc))
            {
                enc = Encoding.GetEncoding(strenc);
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
            _sfd.Filter = FilesGX.GetFillesPrmitid("xml,xsd,xlt,xhtml,html,htm,txt,*");
            if (String.IsNullOrEmpty(this._nameDefaultSave))
            {
                _sfd.FileName = this._nameDefaultSave;
            }
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
            txtXML.ClearStylesBuffer();
            txtXML.Language = _lang;
            this.txtXML.OnSyntaxHighlight(new FastColoredTextBoxNS.TextChangedEventArgs(this.txtXML.Range));
            txtXML.Refresh();
            this.SetCheckedLanguage();


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
        private void SetCheckedLanguage()
        {
            cToolStripMenuItem1.Checked = false;
            genexusToolStripMenuItem.Checked = false;
            hTMLToolStripMenuItem.Checked = false;
            javaToolStripMenuItem.Checked = false;
            pHPToolStripMenuItem.Checked = false;
            sQLToolStripMenuItem.Checked = false;
            vBToolStripMenuItem.Checked = false;
            xMLToolStripMenuItem.Checked = false;

            switch (txtXML.Language)
            {
                case FastColoredTextBoxNS.Language.CSharp:  cToolStripMenuItem1.Checked = true; break;
                case FastColoredTextBoxNS.Language.Genexus: genexusToolStripMenuItem.Checked = true; break;
                case FastColoredTextBoxNS.Language.HTML: hTMLToolStripMenuItem.Checked = true; break;
                case FastColoredTextBoxNS.Language.JS: javaToolStripMenuItem.Checked = true; break;
                case FastColoredTextBoxNS.Language.PHP: pHPToolStripMenuItem.Checked = true; break;
                case FastColoredTextBoxNS.Language.SQL: sQLToolStripMenuItem.Checked = true; break;
                case FastColoredTextBoxNS.Language.VB: vBToolStripMenuItem.Checked = true; break;
                case FastColoredTextBoxNS.Language.XML: xMLToolStripMenuItem.Checked = true; break;
            }
            //txtXML.Refresh();
        }


    }
}