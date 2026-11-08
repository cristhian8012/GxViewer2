using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Attribute
    {
        private string _id;
        private string _Name;
        private string _Title= String.Empty;
        private int _Length = 0;
        private int _decimal;
        private string _Picture;
        private string _Type = "numeric";

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        public int Length
        {
            get { return _Length; }
            set { _Length = value; }
        }

        public int Decimal
        {
            get { return _decimal; }
            set { _decimal = value; }
        }

        public int Decimals
        {
            get { return _decimal; }
            set { _decimal = value; }
        }

        public string Picture
        {
            get { return _Picture; }
            set { _Picture = value; }
        }

    }
}
