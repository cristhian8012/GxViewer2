using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer
{
    public class FilesGX
    {

        private static string[] _objs = new string[] { "WorkPanel", "Procedure", "Report", "Transaction", "DataView", "WebPanel", "StructureDataType"}; //, Falta Implementar "Table" 
        public static bool ObjectIsValid(string _TipoObject)
        {
            bool _ret = false;

            foreach (string s in _objs)
            {
                if (s == _TipoObject)
                {
                    _ret = true;
                    break;
                }
            }
            

            return _ret;
        }

        private static string[] _exts = new string[] {"xml","xpw","xpz","txt", "GXV","prc", "wpn", "rpt", "trn","clp","rpg", "cbl", "clle", "rpgle", "sqlrpg", "sql", "pf", "tbt"};

        private static string GetFillesPrmitid(string[] _extenciones)
        {
            string _ret = string.Empty;
            string _allObjectsName = String.Empty;
            string _allObjectsFilter = String.Empty;
            string _AllPermitid = String.Empty;

            foreach (string s in _extenciones)
            {
                _allObjectsName += String.Format(",*.{0}", s);
                _allObjectsFilter += String.Format(";*.{0}", s);

                _ret += String.Format("Archivos {1} (*.{0})|*.{0}|",s,s.ToUpper());
            }

            _AllPermitid = String.Format("Todos ({0})|{1}|", _allObjectsName.Substring(1), _allObjectsFilter.Substring(1));

            return _AllPermitid + _ret.Substring(0, _ret.Length - 1);
        }

        public static string GetFillesPrmitid(string _Extensiones)
        {
            string[] _extens = _Extensiones.Split(",".ToCharArray());
            return GetFillesPrmitid(_extens);
        }

        public static string GetFillesPrmitid()
        {
            return GetFillesPrmitid(_exts);
        }

        public static string GetFillesPrmitid_Gx18()
        {
            string[] _ext18 = new string[] { "xml", "xpz", "prc", "wpn", "rpt", "trn", "hpn" };
            return GetFillesPrmitid(_exts);
        }

    }

    public class FilesGXOpen
    {
        private string _name;
        private string _path;
        private string _XMLAll;
        private List<GxObject.GXObj> _objects;
        private List<object> _objects2;
        private bool _GxViewerFile = false;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public string XMLText
        {
            get { return _XMLAll; }
            set { _XMLAll = value; }
        }

        public List<GxObject.GXObj> Objetos
        {
            get { return _objects; }
            set { _objects = value; }
        }

        public List<object> Objetos2
        {
            get { return _objects2; }
            set { _objects2 = value; }
        }

        public bool IsGxViewerFile
        {
            get { return _GxViewerFile; }
            set { _GxViewerFile = value; }
        }

    }


}
