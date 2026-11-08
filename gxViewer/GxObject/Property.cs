using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Property : HtmlTagForm
    {
        private string _Type;
        private string _Name;
        private string _Value;
        private string _Rule;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string Type
        {
            get { return Type; }
            set { _Type = value; }
        }

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public string Rule
        {
            get { return _Rule; }
            set { _Rule = value; }
        }

    }
}
