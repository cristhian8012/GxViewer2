using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GxKBBase18
{
    public class DBOFactory
    {
        private string _connectionString;
        private string _providerName;
        private string _curDatabase = "";
        private DbProviderFactory _factory;
        private DbConnection _connection;

        public DBOFactory(string providerName, string connectionString)
        {
            _providerName = providerName;
            _connectionString = connectionString;

            if (_providerName == "System.Data.SQLite")
            {
                _factory = GetFactorySQLITE();
            }
            else
            {
                _factory = DbProviderFactories.GetFactory(_providerName);
            }
        }

        public DbConnection GetConnection()
        {
            _connection = _factory.CreateConnection();
            _connection.ConnectionString = _connectionString;
            return _connection;
        }
        public void ReConnection(string connectionstring)
        {
            if (_connection!=null)
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            _connectionString = connectionstring;
            _connection = GetConnection();
            _connection.Open();
        }


        public DbProviderFactory GetFactory()
        {
            return _factory;
        }

        public string CurrentDatabase
        {
            get { return _curDatabase; }
            set { _curDatabase = value; }
            
        }

        public DbConnection GetCurrentConnection()
        {
            return _connection;
        }

        public static DbProviderFactory GetFactorySQLITE()
        {
            // Ruta donde tienes las DLL externas
            string basePath = AppDomain.CurrentDomain.BaseDirectory ;

            // Carga la DLL
            string _filename = Path.Combine(basePath, "System.Data.SQLite.dll");
            Assembly asm = Assembly.LoadFrom( _filename );
            
            Type factoryType = Type.GetType("System.Data.SQLite.SQLiteFactory, System.Data.SQLite");

            var prop = factoryType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
            DbProviderFactory factory;
            if (prop != null)
            {
                // Algunos proveedores (ej. SQL Server, MySQL) tienen Instance
                factory = (DbProviderFactory)prop.GetValue(null, null);
            }
            else
            {
                // Otros (como tu versión de SQLite) requieren crear la instancia
                factory = (DbProviderFactory)Activator.CreateInstance(factoryType);
            }

            return factory;
        }


        public DataTable GetDatabasesSQLServer(DbConnection dbcnn)
        {
            string strSQL = "";
            strSQL = "SELECT dbId AS 'ID' ,name AS 'NameDb' FROM SYSDATABASES where dbId > 4 ORDER BY NAME";

            DataTable dtDatabases = DBOQuerys.ReadQuery(_factory, dbcnn, strSQL);
            return dtDatabases;
        }

        


    }
}
