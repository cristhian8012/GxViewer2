using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class GXAtt
    {
        private AttInfo _AttInfo;
        private Attribute _Attribute = new Attribute();
        private AttDoc _AttDoc;
        private Help _help;

        public string Name
        {
            get { return _Attribute.Name; }
        }
        public string Tipo
        {
            get { return _Attribute.Type; }
        }

        public string TipoCompleto
        {
            get
            {
                if (_Attribute != null)
                {
                    return String.Format("{0}({1},{2})", _Attribute.Type, _Attribute.Length, _Attribute.Decimal);
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        public string Id
        {
            get { return _Attribute.Id; }
        }
        public int Tamanio
        {
            get { return _Attribute.Length; }
        }
        public int Decimals
        {
            get { return _Attribute.Decimal; }
        }
        public string Picture
        {
            get { return _Attribute.Picture; }
        }
        public string Title
        {
            get { return _Attribute.Title; }
        }
	

        public AttInfo AttInfo
        {
            get { return _AttInfo; }
            set { _AttInfo = value; }
        }
        public Attribute Attribute
        {
            get { return _Attribute; }
            set { _Attribute = value; }
        }
        public AttDoc AttDoc
        {
            get { return _AttDoc; }
            set { _AttDoc = value; }
        }

        public Help Help
        {
            get { return _help; }
            set { _help = value; }
        }
            
            
    }
}
