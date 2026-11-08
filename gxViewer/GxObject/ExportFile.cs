using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    public class ExportFile
    {
        private Model _model;
        private string _Description;
        private KMW _KMW;
        //public GXObject _GXObject;

        public Model Model
        {
            get { return _model; }
            set { _model = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public KMW KMW
        {
            get { return _KMW; }
            set { _KMW = value; }
        }
        /*
        public GXObject GXObject
        {
            get { return _GXObject; }
            set { _GXObject = value; }
        }
        */


    }
}
