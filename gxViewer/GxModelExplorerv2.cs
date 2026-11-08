using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Security.AccessControl;
using System.Windows.Forms;
using System.Xml;
using GxKBBase18;

namespace gxViewer
{
    public partial class GxModelExplorerv2 : UserControl
    {
        public class DistribuidosArgs2 : EventArgs
        {
            public XmlDocument XMLDoc { get; set; }
            public string Modelo { get; set; }
        }

        public event EventHandler<DistribuidosArgs2> OnGetObjects;

        private bool _LoadingModel = true;
        Dictionary<int, string> _DTipos = new Dictionary<int, string>();
        Dictionary<int, string> _DTiposAtt = new Dictionary<int, string>();
        Dictionary<int, string> _TiposObjectsAbrev = new Dictionary<int, string>();
        DataTable _dtFolders = new DataTable();
        System.IO.DirectoryInfo _diKB = null;

        public GxModelExplorerv2()
        {
            InitializeComponent();
            this.Disposed += OnDispose;

            this.LoadtypeObjects();
            this.LoadFilterType();
            this.LoadtiposAbrev();
            this.LoadtypeAtt();
            
        }

        private void LoadtiposAbrev()
        {
            //{ trn | att | dom | prc | rpt | mnu | wkp | dv | strn | sprc | srpt | swkp | pmt | spmt | wbp | swbp | sdt }
            _TiposObjectsAbrev.Add((int)GXClass.Transaction, "trn");
            _TiposObjectsAbrev.Add((int)GXClass.Procedure, "prc");
            _TiposObjectsAbrev.Add((int)GXClass.Report, "rpt");
            _TiposObjectsAbrev.Add((int)GXClass.Menu, "mnu");
            _TiposObjectsAbrev.Add((int)GXClass.WorkPanel, "wkp");
            _TiposObjectsAbrev.Add((int)GXClass.DataView, "dv");
            //_TiposObjectsAbrev.Add((int)GXClass.Table, "TBL");
            //_TiposObjectsAbrev.Add((int)GXClass.Folder, "FLD");
            //_TiposObjectsAbrev.Add((int)GXClass.Prompt, "pmt");
            _TiposObjectsAbrev.Add((int)GXClass.WebPanel, "wbp");
            //_TiposObjectsAbrev.Add((int)GXClass.ExternalProgram, "XPG");
            //_TiposObjectsAbrev.Add((int)GXClass.MenuBar, "MBR");
            //_TiposObjectsAbrev.Add((int)GXClass.StyleTransaction, "strn");
            //_TiposObjectsAbrev.Add((int)GXClass.StyleProcedure, "sprc");
            //_TiposObjectsAbrev.Add((int)GXClass.StyleReport, "srpt");
            //_TiposObjectsAbrev.Add((int)GXClass.StyleWorkPanel, "swkp");
            //_TiposObjectsAbrev.Add((int)GXClass.StylePrompt, "spmt");
            //_TiposObjectsAbrev.Add((int)GXClass.StyleWebPanel, "swbp");
            //_TiposObjectsAbrev.Add((int)GXClass.StyleMenuBar, "SMBR");
            //_TiposObjectsAbrev.Add((int)GXClass.Theme, "THM");
            _TiposObjectsAbrev.Add((int)GXClass.SDT, "sdt");

            //_TiposObjectsAbrev.Add((int)GXClass.Attribute, "att");
            _TiposObjectsAbrev.Add((int)GXClass.Domain, "dom");

        }

        private void OnDispose(object sender, EventArgs e)
        {
            // do stuff on dispose
            try
            {
                KnowledgeBase.KB.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


       

        //private void LoadModels()
        //{
        //    Dictionary<string, List<GXModel>> _listaModelTarget = new Dictionary<string, List<GXModel>>();
        //    GXObjectList _lst = KnowledgeBase.KB.GetModels();
           

        //    List<string> _ListTypes = new List<string>();
        //    //List<GXModel> _lstModel = new List<GXModel>();
        //    foreach (var _mdl in _lst)
        //    {
        //        GXModel mdl = (GXModel)_mdl;
        //        string _Type = mdl.Type.ToString();

        //        if (!_listaModelTarget.ContainsKey(_Type))
        //        {
        //            _listaModelTarget.Add(_Type, new List<GXModel>());
        //        }
        //        _listaModelTarget[_Type].Add(mdl);
        //    }

        //    _LoadingModel = true;

        //    cboTarget.DataSource = new BindingSource(_listaModelTarget, null);
        //    cboTarget.DisplayMember = "Key";
        //    cboTarget.ValueMember = "Key";
        //    _LoadingModel = false;
        //    if (_listaModelTarget.ContainsKey(GXModel.ModelType.Production.ToString()))
        //    {
        //        cboTarget.SelectedValue = GXModel.ModelType.Production.ToString();
        //    }
        //    else
        //    {
        //        cboTarget.SelectedValue = 0;
        //    }
        //    cboTarget_SelectedIndexChanged(cboTarget, null);
        //}


        private void LoadtypeObjects()
        {
            Array _Tipos = Enum.GetValues(typeof(GXClass));
            _DTipos.Add(-1, "*ALL");
            for (int i = 0; i < _Tipos.Length; i++)
            {
                GXClass _xx = (GXClass)_Tipos.GetValue(i);
                _DTipos.Add( (int)_xx, _xx.ToString());
            }
            
            cboType.DataSource = new BindingSource(_DTipos, null);
            cboType.DisplayMember = "Value";
            cboType.ValueMember = "Key";
            cboType.SelectedValue = -1;
        }

        private void LoadtypeAtt()
        {
            Array _Tipos = Enum.GetValues(typeof(DataType));
            for (int i = 0; i < _Tipos.Length; i++)
            {
                DataType _xx = (DataType)_Tipos.GetValue(i);
                _DTiposAtt.Add((int)_xx, _xx.ToString());
            }
        }

        private void LoadFilterType()
        {
            Dictionary<int, string> _tipoFilter = new Dictionary<int, string>();
            _tipoFilter.Add(1, "Objetos");
            _tipoFilter.Add(2, "Tablas");
            _tipoFilter.Add(3, "Atributos");
            _tipoFilter.Add(4, "Dominios");
            _tipoFilter.Add(5, "Formulas");
            _tipoFilter.Add(6, "Variables");
            
            cboTipoBusqueda.DataSource = new BindingSource(_tipoFilter, null);
            cboTipoBusqueda.DisplayMember = "Value";
            cboTipoBusqueda.ValueMember = "Key";
            cboTipoBusqueda.SelectedValue = 1;
        }


        //private void LoadFolders()
        //{
        //    int _IdModel = (cboModel.SelectedValue as GXModel).Id;
        //    int _TipoObject = (int)GXClass.Folder;
        //    string _queryStr = String.Format("SELECT * FROM Object WHERE MdlId={0} AND ObjCls={1}", _IdModel, _TipoObject);
        //    KnowledgeBaseQuery _KQry = KnowledgeBase.KB.ExecuteQuery(_queryStr);
        //    if (_KQry.Read())
        //    {
        //        _dtFolders = (DataTable)(typeof(KnowledgeBaseQuery)).GetField("m_DataTable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_KQry);
        //    }

        //    DataRow _r = _dtFolders.NewRow();
        //    _r["ObjId"] = -1;
        //    _r["ObjNam"] = "*ALL";
        //    _dtFolders.Rows.InsertAt(_r,0);

        //    cboFolder.DataSource = _dtFolders;
        //    cboFolder.DisplayMember = "ObjNam";
        //    cboFolder.ValueMember = "ObjId";
        //    cboFolder.SelectedValue = -1;
        //}

        private void cboTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    try
        //    {
        //        if (_LoadingModel) { return; }

        //        if (cboTarget.SelectedItem != null)
        //        {
        //            KeyValuePair<string, List<GXModel>> item = (KeyValuePair<string, List<GXModel>>)cboTarget.SelectedItem;
        //            cboModel.DataSource = item.Value;
        //            cboModel.DisplayMember = "Name";
        //            cboModel.ValueMember = "Id";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        //MessageBox.Show(ex.Message);   
        //    }
        }

        private void cboModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (cboModel.SelectedItem != null)
            //    {
            //        int _IdModel = Convert.ToInt32(cboModel.SelectedValue);
            //        KnowledgeBase.KB.ModelID = _IdModel;
            //        LoadFolders();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    //MessageBox.Show(ex.Message);
            //}
        }

        private void BuscarObjetos()
        {
            int _TipoBusqueda = int.Parse(cboTipoBusqueda.SelectedValue.ToString());
            int _IdModel = int.Parse(cboModel.SelectedValue.ToString());
            int _TipoObject = int.Parse(cboType.SelectedValue.ToString());
            string _Name = txtFind.Text.ToUpper();
            int _Folder = int.Parse(cboFolder.SelectedValue.ToString());

            Dictionary<string, GridColumnInfo> _listaCampos = new Dictionary<string, GridColumnInfo>();
            DataTable _dtResult = null;

            string _queryStr = String.Empty;

            switch (_TipoBusqueda)
            {
                case 1: //Objetos

                    _dtResult = KnowledgeBase.KB.FindObjects(_IdModel, _TipoObject, _Folder, _Name);

                    _listaCampos.Add("ObjectName", new GridColumnInfo { Field= "ObjectName", Header= "Nombre Objeto", Width=150 } );
                    _listaCampos.Add("ObjectType", new GridColumnInfo { Field = "ObjectType", Header = "Tipo Objeto", Width = 150 });
                    _listaCampos.Add("ObjectDecription", new GridColumnInfo { Field = "ObjectDecription", Header = "Descripción", Width = 350 } );
                    _listaCampos.Add("FolderName", new GridColumnInfo { Field = "FolderName", Header = "Folder", Width = 150 });
                    //_listaCampos.Add("ObjCreTS", "F.Creación ");
                    //_listaCampos.Add("ObjUpdTS", "F.Modificación");
                    //_listaCampos.Add("ObjSpcTS", "F.Especificación ");
                    //_listaCampos.Add("ObjConsTS", "F.Consolidación");
                    //_listaCampos.Add("ObjTyp", "(DW)");

                    break;
                    
                case 2: //Tablas
                    _dtResult = KnowledgeBase.KB.FindTables(_IdModel, _Name);

                    _listaCampos.Add("table_id", new GridColumnInfo { Field = "table_id", Header = "Id", Width = 80 });
                    _listaCampos.Add("table_name", new GridColumnInfo { Field = "table_name", Header = "Nombre Tabla", Width = 150 });
                    _listaCampos.Add("obj_title", new GridColumnInfo { Field = "obj_title", Header = "Descripción Tabla", Width = 400 });
                    _listaCampos.Add("ObjectType", new GridColumnInfo { Field = "ObjectType", Header = "Table", Width = 100 });
                    break;

                case 3: //Atributos
                    _queryStr = String.Format("SELECT * FROM Attribute WHERE MdlId={0} ", _IdModel);

                    _listaCampos.Add("AttNam", new GridColumnInfo { Field = "AttNam", Header = "Nombre Atributo", Width = 150 });
                    _listaCampos.Add("AttDsc", new GridColumnInfo { Field = "AttDsc", Header = "Descripción", Width = 150 });
                    _listaCampos.Add("AttTyp", new GridColumnInfo { Field = "AttTyp", Header = "Tipo Dato", Width = 150 });
                    _listaCampos.Add("AttLen", new GridColumnInfo { Field = "AttLen", Header = "Tamaño", Width = 150 });
                    _listaCampos.Add("AttDec", new GridColumnInfo { Field = "AttDec", Header = "N° Dec.", Width = 150 });
                    _listaCampos.Add("AttSign", new GridColumnInfo { Field = "AttSign", Header = "Signo", Width = 150 });
                    _listaCampos.Add("AttPic", new GridColumnInfo { Field = "AttPic", Header = "Picture", Width = 150 });
                    _listaCampos.Add("AttId", new GridColumnInfo { Field = "AttId", Header = "IdAtributo", Width = 150 });
                    //
                    break;

                //case 4: //Dominios
                //    _queryStr = String.Format("SELECT * FROM Domain WHERE MdlId={0} ", _IdModel);

                //    _listaCampos.Add("DomNam", "Nombre Dominio");
                //    _listaCampos.Add("DomDsc", "Descripción");
                //    _listaCampos.Add("DomTyp", "Tipo Dato");
                //    _listaCampos.Add("DomLen", "Tamaño");
                //    _listaCampos.Add("DomDec", "N° Dec.");
                //    _listaCampos.Add("DomRng", "Rango");
                //    _listaCampos.Add("DomPic", "Picture");
                //    _listaCampos.Add("DomUpdTS", "F.Modificación"); 
                //    //
                //    break;

                //case 5: //Formulas
                //    _queryStr = String.Format("SELECT * FROM Formula WHERE MdlId={0} ", _IdModel);

                //    _listaCampos.Add("FmlPos", "Posición del token");
                //    _listaCampos.Add("FmlTknTyp", "Tipo de token");
                //    _listaCampos.Add("FmlTknVal", "Valor del token");
                //    _listaCampos.Add("AttId", "IdAtributo");
                //    //
                //    break;

                //case 6: //Variables

                //    if (_TipoObject.Equals(-1))
                //    {
                //        MessageBox.Show("Seleccione Tipo de Objeto", "Buscar Objetos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //    }

                //    _queryStr = String.Format("SELECT * FROM Variable WHERE MdlId={0} AND ObjCls={1} AND VarId < -7 ", _IdModel, _TipoObject);

                //    if (_Name.Length > 3)
                //    {
                //        _queryStr += String.Format(" AND VarNam=\"{0}\" ", _Name);
                //    }

                //    _listaCampos.Add("VarNam", "Nombre Variable");
                //    _listaCampos.Add("VarDsc", "Descripción");
                //    _listaCampos.Add("VarTyp", "Tipo Dato");
                //    _listaCampos.Add("VarLen", "Tamaño");
                //    _listaCampos.Add("VarDec", "N° Dec.");
                //    _listaCampos.Add("VarSign", "Signo");
                //    _listaCampos.Add("VarPic", "Picture");
                //    //_listaCampos.Add("Dimension", "Dimension");
                //    _listaCampos.Add("DimRows", "Rows");
                //    _listaCampos.Add("DimCols", "Cols");
                //    _listaCampos.Add("IdTypeObject", "Tipo Objeto");
                //    _listaCampos.Add("IdObject", "Nombre Objeto");
                //    break;

                default:
                    break;
            }

            dgvObjetos.DataSource = null;
            dgvObjetos.Columns.Clear();

            if (_listaCampos.Count > 0)
            {
                dgvObjetos.AutoGenerateColumns = false;
                foreach (KeyValuePair<string, GridColumnInfo> _col in _listaCampos)
                {
                    int _ColAdd = -1;
                    
                    switch (_col.Key)
                    {
                        case "AttTyp":
                        case "DomTyp":
                        case "VarTyp":
                            DataGridViewComboBoxColumn _cboAtt = new DataGridViewComboBoxColumn();
                            _cboAtt.DataSource = new BindingSource(_DTiposAtt, null);
                            _cboAtt.ValueMember = "Key";
                            _cboAtt.DisplayMember = "Value";
                            _cboAtt.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                            _ColAdd = dgvObjetos.Columns.Add(_cboAtt);
                            break;

                        default:
                            DataGridViewColumn _dcol = new DataGridViewColumn();
                            _dcol.CellTemplate = new DataGridViewTextBoxCell();
                            _dcol.SortMode = DataGridViewColumnSortMode.Automatic;
                            //_dcol.Width = 100;
                            _ColAdd = dgvObjetos.Columns.Add(_dcol);

                            break;
                    }
                    
                    dgvObjetos.Columns[_ColAdd].Name = String.Format("Col_{0}", _col.Key);
                    dgvObjetos.Columns[_ColAdd].DataPropertyName = _col.Key;
                    dgvObjetos.Columns[_ColAdd].HeaderText = _col.Value.Header;
                    if (_col.Value != null &&  _col.Value.Align > 0 )
                    {
                        dgvObjetos.Columns[_ColAdd].DefaultCellStyle.Alignment = _col.Value.Align;
                    }
                    if (_col.Value != null && _col.Value.Format != null)
                    {
                        dgvObjetos.Columns[_ColAdd].DefaultCellStyle.Format = _col.Value.Format;
                    }

                    if (_col.Value != null && _col.Value.Width > 0)
                    {
                        dgvObjetos.Columns[_ColAdd].Width = _col.Value.Width;
                    }
                    

                    //dgvObjetos.Columns.Add(_dcol);
                }
            }
            else
            {
                dgvObjetos.AutoGenerateColumns = true;
            }


            dgvObjetos.DataSource = _dtResult;
            dgvObjetos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //dgvObjetos.RowHeadersWidth = 25;

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarObjetos();
        }

        private void dgvObjetos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void btnObtener_Click(object sender, EventArgs e)
        {
           
            XmlDocument _xdoc = this.Distribuir();
            DistribuidosArgs2 _args = new DistribuidosArgs2();
            _args.XMLDoc = _xdoc;
            _args.Modelo = lblNameModel.Text;
            OnGetObjects(this, _args);
            
        }

        private XmlDocument Distribuir()
        {
            if (dgvObjetos.Rows.Count <= 0)
            {
                MessageBox.Show("No hay ningun objeto seleccionado", "Obtener Objetos GX", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            try
            {
                int pModelId = int.Parse(cboModel.SelectedValue.ToString());
                List<string> _ListObjects = new List<string>();
                foreach (DataGridViewRow _grow in dgvObjetos.SelectedRows)
                {
                    DataRow _r = (_grow.DataBoundItem as DataRowView).Row;

                    if (!_r.Table.Columns.Contains("ObjectType"))
                    {
                        MessageBox.Show("El objeto : " + _r[ dgvObjetos.Columns[0].DataPropertyName].ToString() + " no se puede Distribuir", "Distribuir Objeto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    

                    int _TipoObject = int.Parse(_r["EntityTypeId"].ToString());
                    int _IdObject = int.Parse(_r["EntityId"].ToString());
                    string _nameObject = _r["ObjectName"].ToString();
                    string _objectType = _r["ObjectType"].ToString();

                    if (_TiposObjectsAbrev.ContainsKey(_TipoObject))
                    {
                        string _object = String.Format("{0}:{1}:{2}:{3}", _TipoObject, _IdObject, _nameObject, _objectType);
                        _ListObjects.Add(_object);
                    }
                    else
                    {
                        MessageBox.Show("El objeto : " + _r["ObjNam"].ToString() + " no se puede Distribuir", "Distribuir Objeto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }                    
                }


                if (_ListObjects.Count <= 0)
                {
                    MessageBox.Show("Debe seleccionar al menos 1 objeto", "Obtener Objetos GX", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                XmlDocument _xdoc = KnowledgeBase.KB.DistributeObjects(pModelId,_ListObjects);
                return _xdoc;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Obtener Objetos GX", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            
        }

        private void btnDistribuir_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "XML files (*.xml)|*.xml";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.AddExtension = true;
            //saveFileDialog1.CheckFileExists = true;
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                XmlDocument _xdoc = this.Distribuir();
                _xdoc.Save(saveFileDialog1.FileName);
                _xdoc = null;
            }
        }


        private void btnKBBD_Click(object sender, EventArgs e)
        {
            bool _conenct = KnowledgeBase.KB.ModelConnect(this.ParentForm);

            if (_conenct)
            {
                btnKBBD.BackColor = Color.Lime;
                lblNameModel.Text = KnowledgeBase.KB.CurrentDatabase();
                this.LoadModels();
                this.LoadFolders();
                txtFind.Focus();
            }
            else
            {
                btnKBBD.BackColor = Color.MistyRose;
            }

        }

        #region Private Methods
        private void LoadModels()
        {
            cboModel.DataSource = KnowledgeBase.KB.getModels();
            cboModel.DisplayMember = "ModelName2";
            cboModel.ValueMember = "ModelId";
        }
        private void LoadFolders()
        {
            cboFolder.DataSource = KnowledgeBase.KB.getFolders();
            cboFolder.DisplayMember = "FolderName";
            cboFolder.ValueMember = "FolderId";
        }

        
        #endregion
    }
    class GridColumnInfo
    {
        public string Header { get; set; }
        public string Field { get; set; }
        public int Width { get; set; }
        public DataGridViewContentAlignment Align { get; set; }
        public string Format { get; set; }
        public bool Visible { get; set; } = true;
    }

}
