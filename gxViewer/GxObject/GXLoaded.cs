using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    public static class GXLoaded
    {
        public static WorkPanel LoadWorkPanel()
        {
            WorkPanel _wpn = new WorkPanel();





            return _wpn;
        }

        public static Procedure LoadProcedure()
        {
            Procedure _prc = new Procedure();



            return _prc;
        }


        public static Report LoadReport()
        {
            Report _rpt = new Report();



            return _rpt;
        }

        private static GXObj LoadGXObj()
        {
            GXObj _obj = new GXObj();



            return _obj;
        }



    }
}
