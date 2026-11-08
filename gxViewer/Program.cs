using FileToStrBase64;
using gxViewer.GxObject;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace gxViewer
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

            //SI CARGO BIEN EL ARCHIVO GX, entonces AGREGO A LA LISTA DE OBJETOS ABIERTOS
            //frmDebugger ftest = new frmDebugger();
            //FilesGXOpen _gxFile = GxObjectsHelper.getFilesGXOpen(new System.IO.FileInfo("C:\\Users\\user\\Downloads\\G18BTXC_PaseJudicial.xpz"));
            //if (_gxFile != null)
            //{
            //    if (_gxFile.Objetos2 != null)
            //    {
            //        ftest = new frmDebugger((GXObj)_gxFile.Objetos2[0]);
            //    }
            //}
            //Application.Run(ftest);
            //return;

            frmMain f = new frmMain();
            ////Para pruebas
            //frmTestTransformXLST _fx = new frmTestTransformXLST();
            //Application.Run(_fx);
            //return;
            ////Fin pruebas

            foreach (string s in args)
            {
                f.OpenFileForArguments(s);
            }

            Application.Run(f);

        }
        /*
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmViewXML f = new frmViewXML();

            foreach (string s in args)
            {
                f = new frmViewXML(s);
            }

            Application.Run(f);

        }
        */
        //[STAThread]
        //static void Main(string[] args)
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new frmMain(args));
        //}
    }
}