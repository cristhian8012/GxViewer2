using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using GXBaseKB;
using System.Xml;

namespace gxViewer
{
    public partial class GxModelExplorer : UserControl
    {
        public class DistribuidosArgs : EventArgs
        {
            public XmlDocument XMLDoc { get; set; }
            public string Modelo { get; set; }
        }

        public event EventHandler<DistribuidosArgs> OnGetObjects;

        private bool _LoadingModel = true;
        Dictionary<int, string> _DTipos = new Dictionary<int, string>();
        Dictionary<int, string> _DTiposAtt = new Dictionary<int, string>();
        Dictionary<int, string> _TiposObjectsAbrev = new Dictionary<int, string>();
        DataTable _dtFolders = new DataTable();
        System.IO.DirectoryInfo _diKB = null;
        
        public GxModelExplorer()
        {
            InitializeComponent();
            this.Disposed += OnDispose;

            this.LoadtypeObjects();
            this.LoadFilterType();
            this.LoadtiposAbrev();
            this.LoadtypeAtt();
            txtPathModel.Text = getLastModelPath();

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
            _TiposObjectsAbrev.Add((int)GXClass.Prompt, "pmt");
            _TiposObjectsAbrev.Add((int)GXClass.WebPanel, "wbp");
            _TiposObjectsAbrev.Add((int)GXClass.ExternalProgram, "XPG");
            //_TiposObjectsAbrev.Add((int)GXClass.MenuBar, "MBR");
            _TiposObjectsAbrev.Add((int)GXClass.StyleTransaction, "strn");
            _TiposObjectsAbrev.Add((int)GXClass.StyleProcedure, "sprc");
            _TiposObjectsAbrev.Add((int)GXClass.StyleReport, "srpt");
            _TiposObjectsAbrev.Add((int)GXClass.StyleWorkPanel, "swkp");
            _TiposObjectsAbrev.Add((int)GXClass.StylePrompt, "spmt");
            _TiposObjectsAbrev.Add((int)GXClass.StyleWebPanel, "swbp");
            //_TiposObjectsAbrev.Add((int)GXClass.StyleMenuBar, "SMBR");
            //_TiposObjectsAbrev.Add((int)GXClass.Theme, "THM");
            _TiposObjectsAbrev.Add((int)GXClass.SDT, "sdt");

            _TiposObjectsAbrev.Add((int)GXClass.Attribute, "att");
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

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                KnowledgeBase.KB.Close();
                dgvObjetos.DataSource = null;

                string _PathKB = txtPathModel.Text;
                _diKB = new System.IO.DirectoryInfo(_PathKB);
                if (_diKB.Exists)
                {
                    KnowledgeBase.KB.Open(_PathKB);
                    this.LoadModels();
                    SaveLastModelPath(_PathKB);
                }
                else
                {
                    MessageBox.Show("La ruta :" + _diKB.FullName + " No existe", "Open KB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

       

        private void LoadModels()
        {
            Dictionary<string, List<GXModel>> _listaModelTarget = new Dictionary<string, List<GXModel>>();
            GXObjectList _lst = KnowledgeBase.KB.GetModels();
           

            List<string> _ListTypes = new List<string>();
            //List<GXModel> _lstModel = new List<GXModel>();
            foreach (var _mdl in _lst)
            {
                GXModel mdl = (GXModel)_mdl;
                string _Type = mdl.Type.ToString();

                if (!_listaModelTarget.ContainsKey(_Type))
                {
                    _listaModelTarget.Add(_Type, new List<GXModel>());
                }
                _listaModelTarget[_Type].Add(mdl);
            }

            _LoadingModel = true;

            cboTarget.DataSource = new BindingSource(_listaModelTarget, null);
            cboTarget.DisplayMember = "Key";
            cboTarget.ValueMember = "Key";
            _LoadingModel = false;
            if (_listaModelTarget.ContainsKey(GXModel.ModelType.Production.ToString()))
            {
                cboTarget.SelectedValue = GXModel.ModelType.Production.ToString();
            }
            else
            {
                cboTarget.SelectedValue = 0;
            }
            cboTarget_SelectedIndexChanged(cboTarget, null);
        }
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


        private void LoadFolders()
        {
            int _IdModel = (cboModel.SelectedValue as GXModel).Id;
            int _TipoObject = (int)GXClass.Folder;
            string _queryStr = String.Format("SELECT * FROM Object WHERE MdlId={0} AND ObjCls={1}", _IdModel, _TipoObject);
            KnowledgeBaseQuery _KQry = KnowledgeBase.KB.ExecuteQuery(_queryStr);
            if (_KQry.Read())
            {
                _dtFolders = (DataTable)(typeof(KnowledgeBaseQuery)).GetField("m_DataTable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_KQry);
            }

            DataRow _r = _dtFolders.NewRow();
            _r["ObjId"] = -1;
            _r["ObjNam"] = "*ALL";
            _dtFolders.Rows.InsertAt(_r,0);

            cboFolder.DataSource = _dtFolders;
            cboFolder.DisplayMember = "ObjNam";
            cboFolder.ValueMember = "ObjId";
            cboFolder.SelectedValue = -1;
        }

        private void btnSearchFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog _fdb = new FolderBrowserDialog();
            _fdb.Description = "Ubicar la ruta de la KB Genexus";
            if (_fdb.ShowDialog(this) == DialogResult.OK)
            {
                txtPathModel.Text = _fdb.SelectedPath;
            }
        }

        private void cboTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_LoadingModel) { return; }

                if (cboTarget.SelectedItem != null)
                {
                    KeyValuePair<string, List<GXModel>> item = (KeyValuePair<string, List<GXModel>>)cboTarget.SelectedItem;
                    cboModel.DataSource = item.Value;
                    cboModel.DisplayMember = "Name";
                    cboModel.ValueMember = "Id";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show(ex.Message);   
            }
        }

        private void cboModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboModel.SelectedItem != null)
                {
                    int _IdModel = (cboModel.SelectedItem as GXModel).Id;
                    KnowledgeBase.KB.ModelID = _IdModel;
                    LoadFolders();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show(ex.Message);
            }
        }

        private void BuscarObjetos()
        {
            int _TipoBusqueda = int.Parse(cboTipoBusqueda.SelectedValue.ToString());
            int _IdModel = (cboModel.SelectedItem as GXModel).Id;
            int _TipoObject = int.Parse(cboType.SelectedValue.ToString());
            string _Name = txtFind.Text.ToUpper();
            int _Folder = int.Parse(cboFolder.SelectedValue.ToString());

            Dictionary<string, string> _listaCampos = new Dictionary<string, string>();


            string _queryStr = String.Empty;
           
            switch (_TipoBusqueda)
            {
                case 1: //Objetos
                    _queryStr = String.Format("SELECT * FROM Object WHERE MdlId={0} ", _IdModel);

                    if (!_TipoObject.Equals(-1))
                    {
                        _queryStr += String.Format(" AND ObjCls={0}", _TipoObject);
                    }

                    if (!_Folder.Equals(-1))
                    {
                        _queryStr += String.Format(" AND ObjOwner={0}", _Folder);
                    }
                    
                    _listaCampos.Add("ObjNam", "Nombre Objeto");
                    _listaCampos.Add("ObjCls", "Tipo Objeto");
                    _listaCampos.Add("ObjDsc", "Descripción");
                    _listaCampos.Add("ObjOwner", "Folder");
                    _listaCampos.Add("ObjCreTS", "F.Creación ");
                    _listaCampos.Add("ObjUpdTS", "F.Modificación");
                    _listaCampos.Add("ObjSpcTS", "F.Especificación ");
                    _listaCampos.Add("ObjConsTS", "F.Consolidación");
                    _listaCampos.Add("ObjTyp", "(DW)");

                    break;
                    
                case 2: //Tablas
                    _queryStr = String.Format("SELECT * FROM Table WHERE MdlId={0} ", _IdModel);
                    
                    _listaCampos.Add("TblNam", "Nombre Tabla");
                    _listaCampos.Add("TblDsc", "Descripción");
                    _listaCampos.Add("TblId", "IdTabla");
                    break;

                case 3: //Atributos
                    _queryStr = String.Format("SELECT * FROM Attribute WHERE MdlId={0} ", _IdModel);

                    _listaCampos.Add("AttNam", "Nombre Atributo");
                    _listaCampos.Add("AttDsc", "Descripción");
                    _listaCampos.Add("AttTyp", "Tipo Dato");
                    _listaCampos.Add("AttLen", "Tamaño");
                    _listaCampos.Add("AttDec", "N° Dec.");
                    _listaCampos.Add("AttSign", "Signo");
                    _listaCampos.Add("AttPic", "Picture");
                    _listaCampos.Add("AttId", "IdAtributo");
                    //
                    break;

                case 4: //Dominios
                    _queryStr = String.Format("SELECT * FROM Domain WHERE MdlId={0} ", _IdModel);

                    _listaCampos.Add("DomNam", "Nombre Dominio");
                    _listaCampos.Add("DomDsc", "Descripción");
                    _listaCampos.Add("DomTyp", "Tipo Dato");
                    _listaCampos.Add("DomLen", "Tamaño");
                    _listaCampos.Add("DomDec", "N° Dec.");
                    _listaCampos.Add("DomRng", "Rango");
                    _listaCampos.Add("DomPic", "Picture");
                    _listaCampos.Add("DomUpdTS", "F.Modificación"); 
                    //
                    break;

                case 5: //Formulas
                    _queryStr = String.Format("SELECT * FROM Formula WHERE MdlId={0} ", _IdModel);

                    _listaCampos.Add("FmlPos", "Posición del token");
                    _listaCampos.Add("FmlTknTyp", "Tipo de token");
                    _listaCampos.Add("FmlTknVal", "Valor del token");
                    _listaCampos.Add("AttId", "IdAtributo");
                    //
                    break;

                case 6: //Variables

                    if (_TipoObject.Equals(-1))
                    {
                        MessageBox.Show("Seleccione Tipo de Objeto", "Buscar Objetos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    _queryStr = String.Format("SELECT * FROM Variable WHERE MdlId={0} AND ObjCls={1} AND VarId < -7 ", _IdModel, _TipoObject);

                    if (_Name.Length > 3)
                    {
                        _queryStr += String.Format(" AND VarNam=\"{0}\" ", _Name);
                    }

                    _listaCampos.Add("VarNam", "Nombre Variable");
                    _listaCampos.Add("VarDsc", "Descripción");
                    _listaCampos.Add("VarTyp", "Tipo Dato");
                    _listaCampos.Add("VarLen", "Tamaño");
                    _listaCampos.Add("VarDec", "N° Dec.");
                    _listaCampos.Add("VarSign", "Signo");
                    _listaCampos.Add("VarPic", "Picture");
                    //_listaCampos.Add("Dimension", "Dimension");
                    _listaCampos.Add("DimRows", "Rows");
                    _listaCampos.Add("DimCols", "Cols");
                    _listaCampos.Add("IdTypeObject", "Tipo Objeto");
                    _listaCampos.Add("IdObject", "Nombre Objeto");
                    break;

                default:
                    break;
            }

            dgvObjetos.DataSource = null;
            DataTable _dtObjects = new DataTable();

            if (!String.IsNullOrEmpty(_queryStr))
            {
                try
                {
                    KnowledgeBaseQuery _KQry = KnowledgeBase.KB.ExecuteQuery(_queryStr);
                    //if (_KQry.Read())
                    {
                        _dtObjects = (DataTable)(typeof(KnowledgeBaseQuery)).GetField("m_DataTable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_KQry);
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Buscar Objetos en KB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                

                string _Field = "";
                switch (_TipoBusqueda)
                {
                    case 1: _Field = "ObjNam"; break;
                    case 2: _Field = "TblNam"; break;
                    case 3:
                        _Field = "AttNam";
                        _dtObjects.Columns.Add("ObjCls", typeof(int)).DefaultValue = (int)GXClass.Attribute;
                        _dtObjects.Columns.Add("ObjNam", typeof(string), "AttNam");
                        break;
                    case 4:
                        _Field = "DomNam";
                        _dtObjects.Columns.Add("ObjCls", typeof(int)).DefaultValue = (int)GXClass.Domain;
                        _dtObjects.Columns.Add("ObjNam", typeof(string), "DomNam");
                        break;
                    case 5: _Field = "FmlTknVal"; break;
                    case 6: _Field = "VarNam";
                        _dtObjects.Columns["ObjCls"].ColumnName = "IdTypeObject";
                        _dtObjects.Columns["ObjId"].ColumnName = "IdObject";
                        break;
                    default:
                        break;
                }

                if (!String.IsNullOrEmpty(_Name))
                {
                    _queryStr = String.Format(" {1} Like '%{0}%' ", _Name, _Field);
                    DataView _dv = _dtObjects.DefaultView;
                    _dv.RowFilter = _queryStr;
                    _dv.RowStateFilter = DataViewRowState.CurrentRows;
                    _dtObjects = _dv.ToTable();
                }

            }

            dgvObjetos.Columns.Clear();
            if (_listaCampos.Count > 0)
            {
                dgvObjetos.AutoGenerateColumns = false;
                foreach (KeyValuePair<string, string> _col in _listaCampos)
                {
                    int _ColAdd = -1;
                    
                    switch (_col.Key)
                    {
                        case "ObjCls":
                        case "IdTypeObject":
                            DataGridViewComboBoxColumn _cbo = new DataGridViewComboBoxColumn();
                            _cbo.DataSource = new BindingSource(_DTipos, null);
                            _cbo.ValueMember = "Key";
                            _cbo.DisplayMember = "Value";
                            //_cbo.Width = 100;
                            _cbo.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                            _ColAdd = dgvObjetos.Columns.Add(_cbo);
                            break;

                        case "ObjOwner":
                            DataGridViewComboBoxColumn _cbo2 = new DataGridViewComboBoxColumn();
                            _cbo2.DataSource = _dtFolders;
                            _cbo2.ValueMember = "ObjId";
                            _cbo2.DisplayMember = "ObjNam";
                            //_cbo2.Width = 100;
                            _cbo2.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                            _ColAdd = dgvObjetos.Columns.Add(_cbo2);
                            break;

                        case "IdObject":
                            DataGridViewComboBoxColumn _cboObj = new DataGridViewComboBoxColumn();
                            DataTable _dtX = new DataTable();
                            string _qX = "";
                            _qX = String.Format("SELECT ObjId, ObjNam FROM Object WHERE MdlId={0} ", _IdModel);
                            if (!_TipoObject.Equals(-1))
                            {
                                _queryStr += String.Format(" AND ObjCls={0}", _TipoObject);
                            }
                            KnowledgeBaseQuery _KQry2 = KnowledgeBase.KB.ExecuteQuery(_qX);
                            if (_KQry2.Read())
                            {
                                _dtX = (DataTable)(typeof(KnowledgeBaseQuery)).GetField("m_DataTable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_KQry2);
                            }
                            _cboObj.DataSource = _dtX;
                            _cboObj.ValueMember = "ObjId";
                            _cboObj.DisplayMember = "ObjNam";
                            _cboObj.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                            _ColAdd = dgvObjetos.Columns.Add(_cboObj);
                            break;

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
                    dgvObjetos.Columns[_ColAdd].HeaderText = _col.Value;
                    
                    //dgvObjetos.Columns.Add(_dcol);
                }
            }
            else
            {
                dgvObjetos.AutoGenerateColumns = true;
            }


            dgvObjetos.DataSource = _dtObjects;
            dgvObjetos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
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
            DistribuidosArgs _args = new DistribuidosArgs();
            _args.XMLDoc = _xdoc;
            _args.Modelo = _diKB.Name;
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
                List<string> _ListObjects = new List<string>();
                foreach (DataGridViewRow _grow in dgvObjetos.SelectedRows)
                {
                    DataRow _r = (_grow.DataBoundItem as DataRowView).Row;

                    if (!_r.Table.Columns.Contains("ObjCls"))
                    {
                        MessageBox.Show("El objeto : " + _r[ dgvObjetos.Columns[0].DataPropertyName].ToString() + " no se puede Distribuir", "Distribuir Objeto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    

                    int _TipoObject = int.Parse(_r["ObjCls"].ToString());

                    if (_TiposObjectsAbrev.ContainsKey(_TipoObject))
                    {
                        string _object = String.Format("{0}:{1}", _TiposObjectsAbrev[_TipoObject], _r["ObjNam"]);
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

                XmlDocument _xdoc = KnowledgeBase.KB.DistributeObjects(_ListObjects.ToArray());
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

        private string getLastModelPath()
        {
            string _ret = "";
            
//#if (DEBUG)
            //_ret = @"D:\Models\G80btCc";
            _ret = @"\\SP-GENEXUS\modelos\\G80BTTW";
//#else
            string _KeyPath = Functions.GetPathRegEdit("RUTAS");
            string _keyName = "PathLastModel";
            _ret = Functions.GetValueRegEdit(_KeyPath, _keyName);
//#endif
            if (String.IsNullOrEmpty(_ret))
            {
                _ret = @"\\SP-GENEXUS\modelos\\G80BTTW";
            }
            return _ret;
        }

        private void SaveLastModelPath(string pathModel)
        {
            string _KeyPath = Functions.GetPathRegEdit("RUTAS");
            string _keyName = "PathLastModel";
            Functions.SetValueRegEdit(_KeyPath, _keyName, pathModel);
        }

    }
}
