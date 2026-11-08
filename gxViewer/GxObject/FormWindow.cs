using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace gxViewer.GxObject
{
    [Serializable]
    public class FormWindow
    {
        private Dictionary<string, GxObject.Property> _Props = new Dictionary<string, Property>();
        
        [BrowsableAttribute(false)]
        public Dictionary<string, GxObject.Property> Propiedades
        {
            get { return _Props; }
            set { _Props = value; }
        }

    }
}
