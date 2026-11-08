using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GxKBBase18
{
    public partial class frmConexionMdl : Form
    {
        private bool _isValid = false;
        private DbConnection _connection;
        private DBOFactory _factory;
        private bool _simulator = false;

        public frmConexionMdl()
        {
            InitializeComponent();
        }
        public DBOFactory getFactory()
        {
            return _factory;
        }

        public void SimularConexionSQlite()
        {
            _simulator = true;
            string modoDebug = ConfigurationManager.AppSettings["ModoDebug"];
            if (modoDebug.Equals("S"))
            {
                FileInfo _fi = new FileInfo(ConfigurationManager.AppSettings["DemoInitialSQLite"]);
                if (_fi.Exists)
                {
                    txtDataSourceSQLITE.Text = _fi.FullName;
                    btnValidarSQLITE_Click(btnValidarSQLITE, null);
                    btnConectarModelSQLITE_Click(btnConectarModelSQLITE, null);
                }
            }

        }

        private void frmConexionMdl_Load(object sender, EventArgs e)
        {
            cboDBMS.Items.Clear();
            cboDBMS.Items.AddRange(new string[] { "SQL Server", "MySQL", "Oracle", "SQLite" });

            this.Width = 391;
            this.Height = 333;

            cboDBMS.SelectedIndex = 0;
            txtConnectionStringSQL.Visible = false;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboDBMS.SelectedItem == null) return;

                ShowDatabaseParameters(cboDBMS.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void ShowDatabaseParameters(string v)
        {
            _isValid = false;
            grpSQL.Visible = false;
            gbSQLite.Visible = false;

            switch (v)
            {
                case "SQL Server":
                    grpSQL.Visible = true;
                    grpSQL.Location = new Point(12, 49);
                    break;

                case "SQLite":
                    gbSQLite.Visible = true;
                    gbSQLite.Location = new Point(12, 49);
                    break;

                case "MySQL":
                    // Implement MySQL parameters visibility here
                    MessageBox.Show("MySQL parameters not implemented yet.");
                    break;
                case "Oracle":
                    MessageBox.Show("MySQL parameters not implemented yet.");
                    break;

                default:
                    break;
            }


        }


        private void btnConectarModelSQL_Click(object sender, EventArgs e)
        {
            if (!_isValid)
            {
                MessageBox.Show("Validar Conexión", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cboDatabaseSQL.DataSource == null)
            {
                MessageBox.Show("Seleccione la Base de Datos", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (cboDatabaseSQL.SelectedValue == null || String.IsNullOrEmpty(cboDatabaseSQL.Text))
            {
                MessageBox.Show("Seleccione la Base de Datos", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            _factory.ReConnection(ChangeDatabaseInConnectionString(_factory.GetCurrentConnection().ConnectionString, cboDatabaseSQL.Text));
            _factory.CurrentDatabase = cboDatabaseSQL.Text;
            GXDB.dboFactory = _factory;
            int _nroModels = GXDB.ChekModels();
            if (_nroModels <= 0)
            {
                MessageBox.Show("La Base de Datos seleccionada no contiene Modelos GX", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {

                this.DialogResult = DialogResult.OK;
                this.Hide();
            }

        }

        private string ChangeDatabaseInConnectionString(string connectionString, string newDbName)
        {
            var builder = new DbConnectionStringBuilder { ConnectionString = connectionString };
            if (builder.ContainsKey("Initial Catalog"))
                builder["Initial Catalog"] = newDbName;
            else if (builder.ContainsKey("Database"))
                builder["Database"] = newDbName;
            else
                builder.Add("Initial Catalog", newDbName);

            return builder.ConnectionString;
        }

        private void btnConectarModelSQLITE_Click(object sender, EventArgs e)
        {
            if (!_isValid)
            {
                MessageBox.Show("Validar Conexión", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            GXDB.dboFactory = _factory;
            int _nroModels = GXDB.ChekModels();
            if (_nroModels <= 0)
            {
                MessageBox.Show("La Base de Datos seleccionada no contiene Modelos GX", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                _factory.CurrentDatabase = Path.GetFileName(txtDataSourceSQLITE.Text);
                this.DialogResult = DialogResult.OK;
                this.Hide();
            }

        }

        private void btnValidarSQL_Click(object sender, EventArgs e)
        {
            string _sqlCnnStr = "";
            if (!chkUseCnnSQL.Checked)
            {
                if (txtServerSQL.Text == "")
                {
                    txtServerSQL.Focus();
                    return;
                }

                if (!chkWinAuthSQL.Checked)
                {

                    if (txtUserSQL.Text == "")
                    {
                        txtUserSQL.Focus();
                        return;
                    }
                }
                _sqlCnnStr = "Data Source=" + txtServerSQL.Text + ";";
                if (chkWinAuthSQL.Checked)
                {
                    _sqlCnnStr += "Integrated Security=True;";
                }
                else
                {
                    _sqlCnnStr += "User ID=" + txtUserSQL.Text + ";Password=" + txtPasswordSQL.Text + ";";
                }

                txtConnectionStringSQL.Text = _sqlCnnStr;
            }
            else
            {
                _sqlCnnStr = txtConnectionStringSQL.Text;
            }

            try
            {
                DBOFactory _sqlServerDB = new DBOFactory("System.Data.SqlClient", _sqlCnnStr);
                _connection = _sqlServerDB.GetConnection();
                _connection.Open();

                MessageBox.Show("Conexión Exitosa", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isValid = true;

                cboDatabaseSQL.DataSource = _sqlServerDB.GetDatabasesSQLServer(_connection);
                cboDatabaseSQL.DisplayMember = "NameDb";
                cboDatabaseSQL.ValueMember = "ID";

                _factory = _sqlServerDB;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void chkUseCnnSQL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseCnnSQL.Checked)
            {
                txtConnectionStringSQL.Visible = true;
                labelServer.Text = "Cadena Cnx";
                labelUserSQL.Visible = false;
                labelPwdSQL.Visible = false;
                chkWinAuthSQL.Visible = false;
            }
            else
            {
                labelServer.Text = "Servidor";
                labelUserSQL.Visible = true;
                labelPwdSQL.Visible = true;
                chkWinAuthSQL.Visible = true;
                txtConnectionStringSQL.Visible = false;
            }
        }

        private void btnSearchDBSQLITE_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Filter = "SQLite Database Files (*.db;*.sqlite)|*.db;*.sqlite|All files (*.*)|*.*";
            if (openFD.ShowDialog() == DialogResult.OK)
            {
                txtDataSourceSQLITE.Text = openFD.FileName;
            }
        }

        private void btnValidarSQLITE_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDataSourceSQLITE.Text))
            {
                MessageBox.Show("Seleccione el archivo de base de datos SQLite", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string _sqliteCnnStr = "Data Source=" + txtDataSourceSQLITE.Text + ";Version=3;";
            try
            {
                
                DBOFactory _sqliteDB = new DBOFactory("System.Data.SQLite", _sqliteCnnStr);
                _connection = _sqliteDB.GetConnection();
                _connection.Open();
                if (!_simulator)
                {
                    MessageBox.Show("Conexión Exitosa", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                _isValid = true;
                _factory = _sqliteDB;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chkWinAuthSQL_CheckedChanged(object sender, EventArgs e)
        {
            txtUserSQL.Enabled = !chkWinAuthSQL.Checked;
            txtPasswordSQL.Enabled = !chkWinAuthSQL.Checked;
        }
    }
}
