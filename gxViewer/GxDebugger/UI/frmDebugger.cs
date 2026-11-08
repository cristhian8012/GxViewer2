using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using gxViewer.GxObject;
using FastColoredTextBoxNS;
using gxViewer.Gx18;
using gxViewer.GxObject.SDT;
using ICSharpCode.SharpZipLib.GZip;
using System.Xml.Serialization;
using Variable = gxViewer.GxObject.Variable;
using gxViewer.GxDebugger.Helpers;

namespace gxViewer
{
    public partial class frmDebugger : System.Windows.Forms.Form
    {
        private GXObj _GXObj;
        private DataTable _dtvars;
        

                public frmDebugger(GXObj pGxObject)
        {
            _GXObj = pGxObject;
            InitializeComponent();
            this.Text = String.Format("GxDebugger {0} | {1}: {2} ", Application.ProductVersion, _GXObj.Tipo, _GXObj.GetName());
        }

        public frmDebugger()
        {
            InitializeComponent();
            this.Text = String.Format("GxDebugger {0} ", Application.ProductVersion);
        }


        private void acercaDeXCAREditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAboutBox _frm = new frmAboutBox();
            _frm.ShowDialog(this);
        }


        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!txtCode.IsChanged)
            { this.Close(); }
            else
            {
                if (MessageBox.Show("¿Esta seguro de cerrar sin guardar los cambios ?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Close();
                }
            }
        }


        private void buscarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtCode.ShowFindDialog();
        }

        private void irAToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtCode.ShowGoToDialog();
        }

        private void reemplazarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtCode.ShowReplaceDialog();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseObjects();
            this.Close();
        }

        private void CloseObjects()
        {
            
        }

        private void frmDebugger_Load(object sender, EventArgs e)
        {
            PrepareObject();
            this.WindowState = FormWindowState.Maximized;
        }


        #region SubRutinas

        #endregion


        private void PrepareObject()
        {
            if (_GXObj != null)
            {
                GxObject.GXObj _obj = (GxObject.GXObj)_GXObj;
                _GXObj = _obj;
                lblTipoObjeto.AutoSize = true;
                lblTipoObjeto.Text = _obj.Tipo;
                lblTipoObjeto.Refresh();
                lblNombreObjeto.Left = lblTipoObjeto.Right;
                lblNombreObjeto.Text = _obj.Info.Name;
                lblNombreObjeto.AutoSize = true;
                lblNombreObjeto.Refresh();
                lbldescrip.Text = _obj.Info.Description;
                lbldescrip.AutoSize = true;
                lbldescrip.Left = lblNombreObjeto.Right;


                txtCode.Text = _obj.Source;
                if (_obj.Subroutines != null)
                {
                    txtCode.AppendText(_obj.Subroutines);
                }

                //Evaluar Conditions y Rules
                //txtConditions.Text = _obj.Conditions;
                //txtRules.Text = _obj.Rules;

                if (_obj.Variables != null)
                {
                    CargarVariables(_obj);
                    txtFindVars.TextChanged += new EventHandler(txtFindVars_TextChanged);
                }

                //if (_obj.GXAtts != null)
                //{
                //    dgvAttrs.AutoGenerateColumns = false;
                //    dgvAttrs.DataSource = Functions.ConvertToDataTable(_obj.GXAtts);
                //    groupBoxAttrs.Text = String.Format("Listado de atributos ({0})", _obj.GXAtts.Length);
                //    txtFindAttr.TextChanged += new EventHandler(txtFindAttr_TextChanged);
                //}

                if (!String.IsNullOrEmpty( _obj.Rules ))
                {
                    CargarParameters(_obj);
                }




            }

        }

        void txtFindVars_TextChanged(object sender, EventArgs e)
        {
            if (_dtvars == null) { _dtvars = (DataTable)dgvVars.DataSource; }
            System.Data.DataView _dv = _dtvars.DefaultView;
            _dv.RowFilter = String.Format("Name LIKE '%{0}%'", txtFindVars.Text.Replace("'", String.Empty).Replace("\"", String.Empty));
            _dv.RowStateFilter = DataViewRowState.CurrentRows;
            DataTable _dtfiltrado = _dv.ToTable();
            dgvVars.DataSource = _dtfiltrado;

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
                //groupBoxVars.Text = String.Format("Listado de variables ({0})", _obj.Variables.Length);
            }
        }

        private void CargarParameters(GXObj _obj)
        {
            List<Variable> _lstVars = new List<Variable>();
            if (_obj.Variables != null)
            {
                dgvParms.AutoGenerateColumns = false;
                foreach (var item in ParameterHelper.GetParameters(_obj) )
                {
                    Variable v = null;
                    if (_obj.GXVariables.ContainsKey(item.Key)) 
                    {
                        v = _obj.GXVariables[item.Key];
                    }
                    if (v != null)
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
                    else
                    {
                        v = new Variable
                        {
                            Name = item.Key,
                            Type = "numeric", // Por defecto, se puede ajustar según sea necesario
                            Length = 3,
                            Decimals = 0,
                            Picture = String.Empty
                        };
                    }
                    v.Direction = item.Value.ToString();
                    _lstVars.Add(v);

                }
                DataTable _dtparms = Functions.ConvertToDataTable(_lstVars.ToArray());
                dgvParms.DataSource = _dtparms;
            }
        }


    }
}