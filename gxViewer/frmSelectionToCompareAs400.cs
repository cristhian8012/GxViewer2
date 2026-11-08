using gxViewer.GxObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gxViewer
{
    public partial class frmSelectionToCompareAs400 : System.Windows.Forms.Form
    {
        private TabControl _tabMain = null;
        private object _TypeEditor;
        private Dictionary<string, string> _listObjetcs = new Dictionary<string, string>();
        public frmSelectionToCompareAs400()
        {
            InitializeComponent();
        }

        public frmSelectionToCompareAs400( TabControl tbMain )
        {
            InitializeComponent();
            _tabMain = tbMain;


        }

        private void frmSelectionToCompareAs400_Load(object sender, EventArgs e)
        {
            if (_tabMain != null)
            {
                LoadCombos();
                txtBeyondCompare.Text = getPathBeyond();
            }
            
        }

        private void LoadCombos()
        {
            foreach (TabPage t in _tabMain.TabPages)
            {
                if (!t.Name.Equals("tpIndex"))
                {
                    foreach (Control c in t.Controls)
                    {
                        if (c.GetType() == typeof(AS400Editor))
                        {
                            _listObjetcs.Add(t.Name, t.ToolTipText);
                            break;
                        }
                    }
                }
            }

            cbo01.DataSource = new BindingSource(_listObjetcs, null);
            cbo01.DisplayMember = "value";
            cbo01.ValueMember = "key";

            cbo02.DataSource = new BindingSource(_listObjetcs, null);
            cbo02.DisplayMember = "value";
            cbo02.ValueMember = "key";

        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            string _key01 = cbo01.SelectedValue.ToString();
            string _key02 = cbo02.SelectedValue.ToString();

            if (_key02.Equals(_key01))
            {
                MessageBox.Show("Seleccione 2 objetos diferentes a comparar", "Comparar Objetos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (String.IsNullOrEmpty( _key01))
            {
                MessageBox.Show("Seleccione 1 objetos ", "Comparar Objetos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbo01.Focus();
                return;
            }

            if (String.IsNullOrEmpty(_key02))
            {
                MessageBox.Show("Seleccione 1 objetos ", "Comparar Objetos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbo02.Focus();
                return;
            }

            if (!String.IsNullOrEmpty(txtBeyondCompare.Text))
            {
                System.IO.FileInfo _fi = new FileInfo(txtBeyondCompare.Text);
                if (_fi.Exists)
                {
                    if (!_fi.Extension.ToLower().Equals(".exe"))
                    {
                        MessageBox.Show("El archivo seleccionado no es correcto", "Comparar Objetos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBeyondCompare.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("El archivo ejecutable no existe", "Comparar Objetos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBeyondCompare.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Ubique la ruta del BeyondCompare ", "Comparar Objetos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBeyondCompare.Focus();
                return;
            }



            string _NameObj01="", _NameObj02="";
            
            string _txtSource01 = GetSourceFromTab(_key01, out _NameObj01);
            string _txtSource02 = GetSourceFromTab(_key02, out _NameObj02);

            if (!String.IsNullOrEmpty(_NameObj01) && !String.IsNullOrEmpty(_NameObj02))
            {
                string _path01 = SaveStringToTemp(_txtSource01, _NameObj01);
                string _path02 = SaveStringToTemp(_txtSource02, _NameObj02);

                if (!String.IsNullOrEmpty(_path01) && !String.IsNullOrEmpty(_path02))
                {
                    string _exe = txtBeyondCompare.Text;
                    string _Params = String.Format("{0} {1}", _path01, _path02);

                    Process.Start(@_exe, @_Params);
                }

            }


        }

        private string GetSourceFromTab(string pKeyTab, out string pNameObject)
        {
            string _ret = "";
            pNameObject = "";

            TabPage t = _tabMain.TabPages[pKeyTab];
            AS400Editor _gxE = null;
            StringBuilder _strb = new StringBuilder();
            foreach (Control c in t.Controls)
            {
                if (c.GetType() == typeof(AS400Editor))
                {
                    _gxE = (AS400Editor)c;
                    break;
                }
            }

            if (_gxE.objeto != null)
            {

                pNameObject = _gxE.Tag.ToString() + "." + _gxE.objeto.Name;
                _ret = _gxE.getCurrentSource();
            }



            return _ret;
        }

        private string SaveStringToTemp( string pObjectTxt, string pObjName )
        {
            //string _file = Path.Combine( System.IO.Path.GetTempPath(), pObjName + ".txt");
            string _file = Path.Combine(System.IO.Path.GetTempPath(), pObjName.Replace("(", "_").Replace(")", "_").Replace(" ", "-") + ".txt");
            try
            {
                if (File.Exists(_file))
                {
                    File.Delete(_file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("El archivo:\n" + _file + "\nEsta siendo usado por otro programa.", "Comparar");
                return "";
            }
            
            using (StreamWriter sw = new StreamWriter(_file, false, Encoding.Default))
            {
                IList<string> _TextQ = new List<string>();
                sw.Write(pObjectTxt);
                sw.Flush();
                sw.Close();
            }

            return _file;
        }

        private string getPathBeyond()
        {
            string _exename = "";
            try
            {
                string _KeyPath = Functions.GetPathRegEdit("RUTAS");
                string _keyName = "PathBeyond";
                _exename = Functions.GetValueRegEdit(_KeyPath, _keyName);
            }
            catch (Exception)
            {
                _exename = "";
            }
            return _exename ;
        }

        private void SaveRegKey()
        {
            string _KeyPath = Functions.GetPathRegEdit("RUTAS");
            string _keyName = "PathBeyond";
            string _pathValue = txtBeyondCompare.Text;
            Functions.SetValueRegEdit(_KeyPath, _keyName, _pathValue);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo Beyond Compare";
            ofd.Filter = FilesGX.GetFillesPrmitid("exe"); 
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo _info = new FileInfo(ofd.FileName);
                if (_info.Exists)
                {
                    txtBeyondCompare.Text = _info.FullName;
                }
                _info = null;
            }
            ofd.Dispose();
        }

        private void frmSelectionToCompareAs400_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!String.IsNullOrEmpty( txtBeyondCompare.Text))
            {
                System.IO.FileInfo _fi = new FileInfo(txtBeyondCompare.Text);
                if (_fi.Exists)
                {
                    this.SaveRegKey();
                }
            }
        }

    }
}
