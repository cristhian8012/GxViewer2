using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GxKBBase18
{
    public class KnowledgeBase
    {
        // Fields
        internal const int NO_MODEL = 0;
        public const int DESIGN_MODEL = 1;
        public const int BACKUP_MODEL = 0x3e7;
        internal const int ROOT_FOLDER = 0;
        private static KnowledgeBase kb;
        //private GXPublicConnection m_Connection = null;
        private bool m_IsOpen = false;
        private string m_Path = null;
        private string m_Uuid = "";

        private DBOFactory _factory;
        private DataSet _dsObjects = new DataSet();



        //private GXObjectLookup m_ObjectLookup;
        //private GXAttributeLookup m_AttributeLookup;
        //private GXDomainLookup m_DomainLookup;
        //private GXModelLookup m_ModelLookup;
        //private GXSubtypeGroupLookup m_SubtypeGroupLookup;
        //private GXTableLookup m_TableLookup;
        //private GXTransactionLookup m_TransactionLookup;
        //private InferenceLookup m_InferenceLookup;
        //private SubordinationLookup m_SubordinationLookup;
        //private int _modelID = 1;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // Methods
        public KnowledgeBase()
        {
            log.Debug("Init Knowledge Base module");
            //this.m_ObjectLookup = new GXObjectLookup(this);
            //this.m_AttributeLookup = new GXAttributeLookup(this);
            //this.m_DomainLookup = new GXDomainLookup(this);
            //this.m_ModelLookup = new GXModelLookup(this);
            //this.m_SubtypeGroupLookup = new GXSubtypeGroupLookup(this);
            //this.m_TableLookup = new GXTableLookup(this);
            //this.m_TransactionLookup = new GXTransactionLookup(this);
            //this.m_InferenceLookup = new InferenceLookup(this);
            //this.m_SubordinationLookup = new SubordinationLookup(this);
        }

        public bool ModelConnect(Form pParent)
        {
            frmConexionMdl frmConexion = new frmConexionMdl();
            DialogResult _res = frmConexion.ShowDialog(pParent);
            if (_res == DialogResult.OK)
            {
                _factory = frmConexion.getFactory();
                //lblNameModel.Text = _factory.CurrentDatabase;
                MessageBox.Show("Conexión exitosa.", "Conexión GxModel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //btnConectarBD.BackColor = Color.Lime;
                //LoadModels();
                //btnExplorarModelo.Enabled = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable getModels()
        {
            GXDB.dboFactory = this._factory;
            DataTable dt = GXDB.getModels();
            DataColumn _dc = dt.Columns.Add("Modelname2");
            _dc.Expression = "ModelName + ' (Path: ' + model_path + ')'";

            return dt;
        }

        public DataTable getFolders()
        {
            GXDB.dboFactory = this._factory;
            DataTable dt = GXDB.getFolders();
            DataRow _rAll = dt.NewRow();
            _rAll["FolderId"] = -1;
            _rAll["FolderName"] = "*ALL";
            dt.Rows.InsertAt(_rAll, 0);
            
            return dt;
        }

        public DataTable FindObjects(int pModelId = 1, int pObjectTyoeId = 0, int pFolderId = 0, string pSearchPattern="")
        {
            GXDB.dboFactory = this._factory;
            DataTable dt = GXDB.FindObjects(pModelId, pObjectTyoeId, pFolderId, pSearchPattern);
            return dt;
        }

        public DataTable FindTables(int pModelId = 1, string pSearchPattern = "")
        {
            GXDB.dboFactory = this._factory;
            DataTable dt = GXDB.getTables(pModelId, pSearchPattern);
            return dt;
        }

        //
        public List<GxObjectProperty> getProperties(int pModelId = 1, int pEntitytypeId = 0, int pEntityId = 0)
        {
            GXDB.dboFactory = this._factory;
            List<GxObjectProperty> _listParts = GXDB.getProperties(pModelId, pEntitytypeId, pEntityId);
            return _listParts;
        }

        public GxObjectMetadata getMetadata(int pModelId = 1, int pEntitytypeId = 0, int pEntityId = 0)
        {
            GXDB.dboFactory = this._factory;
            GxObjectMetadata _metadata = GXDB.getMetadata(pModelId, pEntitytypeId, pEntityId);
            return _metadata;
        }

        public List<GxObjectPart> getCompositionParts(int pModelId = 1, int pEntitytypeId=0 , int pEntityId=0 )
        {
            GXDB.dboFactory = this._factory;
            List<GxObjectPart> _listParts = GXDB.getComposition(pModelId, pEntitytypeId, pEntityId);
            return _listParts;
        }

        public GxDependencyReference getDependencies(int pModelId = 1, int pEntitytypeId = 0, int pEntityId = 0)
        {
            GXDB.dboFactory = this._factory;
            GxDependencyReference _ret = GXDB.getDependency(pModelId, pEntitytypeId, pEntityId);
            return _ret;
        }



        public string CurrentDatabase()
        {
            if (_factory != null)
            {
                return _factory.CurrentDatabase;
            }
            else
            {
                return "";
            }
        }

        public DBOFactory getfactoryDB
        {
            get { return _factory; }
        }


        //private void CheckDesignModel()
        //{
        //    string tempFileName = Path.GetTempFileName();
        //    try
        //    {
        //        string[] strArray;
        //        object[] objArray;
        //        try
        //        {
        //            File.Delete(tempFileName);
        //            strArray = new string[] { "att:-ThisCannotExist" };
        //            objArray = strArray;
        //            this.DistributeObjects(tempFileName, objArray);
        //            return;
        //        }
        //        catch (Exception exception)
        //        {
        //            log.Debug("First query in CheckDesignModel failed. Retrying...", exception);
        //        }
        //        try
        //        {
        //            File.Delete(tempFileName);
        //            strArray = new string[] { "att:-ThisCannotExist" };
        //            objArray = strArray;
        //            this.DistributeObjects(tempFileName, objArray);
        //        }
        //        catch (Exception exception2)
        //        {
        //            log.Debug("Design model could not be read", exception2);
        //            //throw new KnowledgeBaseOpenException("The design model could not be read.", exception2);
        //        }
        //    }
        //    finally
        //    {
        //        File.Delete(tempFileName);
        //    }
        //}

        private void CheckKBType(KnowledgeBaseType allowedType)
        {
            if (allowedType != KnowledgeBaseType.Unspecified)
            {
                string str = this.ReadKBProperty("KBType");
                log.Debug("Knowledge Base type is: " + str);
                if ((str != "") && (str != ((int)allowedType).ToString()))
                {
                   // throw new KnowledgeBaseOpenException("Only Knowledge Bases created with GeneXus '" + allowedType.ToString().ToUpper() + "' can be opened.");
                }
            }
        }

        private void ClearCaches()
        {
            //this.m_ObjectLookup.Clear();
            //this.m_AttributeLookup.Clear();
            //this.m_DomainLookup.Clear();
            //this.m_ModelLookup.Clear();
            //this.m_SubtypeGroupLookup.Clear();
            //this.m_TableLookup.Clear();
            //this.m_TransactionLookup.Clear();
            GC.Collect();
        }

        public void Close()
        {
            //this.InternalClose();
            this.ClearCaches();
            this.m_Path = "";
            this.m_Uuid = "";
        }

        //public GXConsolidationResults ConsolidateFile(string file, bool backupModel)
        //{
        //    GXConsolidationResults results = new GXConsolidationResults();
        //    using (KnowledgeBaseQuery query = this.ExecuteQuery(string.Format("CONSOLIDATE @\"{0}\"" + (backupModel ? " WITH (MakeBackup=1)" : ""), file)))
        //    {
        //        while (query.Read())
        //        {
        //            GXClass objCls = (GXClass)query["ObjCls"];
        //            int num = (int)query["ObjState"];
        //            string xmlText = (string)query["ObjNavPth"];
        //            GXConsolidationResult res = new GXConsolidationResult(objCls, (GXConsolidationResult.ResultType)num, xmlText);
        //            results.Add(res);
        //        }
        //    }
        //    return results;
        //}

        public XmlDocument DistributeObjects(int pModelId, List<string> ListObjectNames)
        {
            XmlDocument document;
            string tempFileName = Path.GetTempFileName();
            try
            {
                //object[] objArray = ListObjectNames;
                this.DistributeObjects(tempFileName, pModelId, ListObjectNames);
                XmlDocument document2 = new XmlDocument();
                document2.Load(tempFileName);
                document = document2;
            }
            finally
            {
                try
                {
                    File.Delete(tempFileName);
                }
                catch (IOException)
                {
                }
            }
            return document;
        }

        internal void DistributeObjects(string filename, int pModelId, List<string> pListObjectNames)
        {

            //KnowledgeBase.KB.log.Debug("Distributing objects: " + string.Join(", ", objectNames) + " to file: " + filename);
            log.Debug("Distributing objects: " + string.Join(", ", pListObjectNames) + " to file: " + filename);
            //Console.WriteLine("Distributing objects: " + string.Join(", ", pListObjectNames) + " to file: " + filename);
            //foreach (string str3 in objectNames)
            //{
            //    //str = str + ((str != "") ? ", " : "") + str3;
            //    Console.WriteLine(" -> " + str3);
            //}

            string _xml = GxDistribute.DistributeXML(pModelId, pListObjectNames);

            if (!String.IsNullOrEmpty(_xml))
            {
                File.WriteAllText(filename, _xml);
            }


            //try
            //{
            //    string str = "";
            //    foreach (string str3 in objectNames)
            //    {
            //        str = str + ((str != "") ? ", " : "") + str3;
            //    }
            //    Trace.Assert(str != "");
            //    string cmdText = "DISTRIBUTE " + str;
            //    Dictionary<int, object> properties = new Dictionary<int, object> {
            //    {
            //        15,
            //        this.ModelID
            //    },
            //    {
            //        0x13,
            //        filename
            //    },
            //    {
            //        0x21,
            //        1
            //    }
            //};
            //    //using (KnowledgeBaseQuery query = this.ExecuteQuery(cmdText, properties))
            //    //{
            //    //    while (query.Read())
            //    //    {
            //    //        if (((int)query["ObjExpState"]) != 1)
            //    //        {
            //    //            throw new KnowledgeBaseException("Could not distribute some objects.");
            //    //        }
            //    //    }
            //    //}
            //}
            //catch (Exception exception)
            //{
            //    //throw new KnowledgeBaseException("Error distributing:\r\n" + exception.Message, exception);
            //}
        }


        private string ReadKBProperty(string propName)
        {
            return "";
            //using (KnowledgeBaseQuery query = this.ExecuteQuery("SELECT * FROM KBProp"))
            //{
            //    bool flag3;
            //    goto Label_0059;
            //Label_0010:
            //    if (query.Read())
            //    {
            //        if (((string)query["PropId"]) == propName)
            //        {
            //            return (string)query["PropValue"];
            //        }
            //    }
            //    else
            //    {
            //        return "";
            //    }
            //Label_0059:
            //    flag3 = true;
            //    goto Label_0010;
            //}
        }

        // Properties
        public static KnowledgeBase KB
        {
            get
            {
                if (kb == null)
                {
                    kb = new KnowledgeBase();
                }
                return kb;
            }
        }

    }


}




