using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JTextEditor
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmTextPlainEditor f = new frmTextPlainEditor();

            if (args.Length > 0)
            {
                foreach (string s in args)
                {
                    Application.Run(new frmTextPlainEditor(s));
                }
            }
            else
            {
                Application.Run(f);
            }
            //Application.Run(new frmTextPlainEditor());
        }



        public class FilesGX
        {

            private static string[] _objs = new string[] { "WorkPanel", "Procedure", "Report", "Transaction", "DataView" };
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

            private static string[] _exts = new string[] { "xml", "xpw", "xpz", "txt", "GXV" };

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

                    _ret += String.Format("Archivos {1} (*.{0})|*.{0}|", s, s.ToUpper());
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

        }
    }
}
