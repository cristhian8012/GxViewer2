using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Collections.ObjectModel;

namespace GxKBBase18
{
    public class DBOQuerys
    {
        public static DataTable ReadQuery(DbProviderFactory _factory, DbConnection dbcnn, string strQuery)
        {
            try
            {
                using (var cmd = dbcnn.CreateCommand())
                {
                    cmd.CommandText = strQuery;
                    var adapter = _factory.CreateDataAdapter();
                    adapter.SelectCommand = cmd;
                    var table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("ReadQuery Method \n" + ex.Message, ex);
            }
            //return null;
        }

        public static object ReadScalar(DbConnection dbcnn, string strQuery)
        {
            try
            {
                using (var cmd = dbcnn.CreateCommand())
                {
                    cmd.CommandText = strQuery;
                    object _ret = cmd.ExecuteScalar();
                    return _ret;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("ReadScalar Method \n" + ex.Message, ex);
            }
            //return null;
        }


    }
}
