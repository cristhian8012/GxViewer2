using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace gxViewer.GxObject
{
    [Serializable]
    public class Transaction : GXObj
    {
        
        private Structure _structure;
        public Structure Structure
        {
            get { return _structure; }
            set
            {
                _structure = value;
            }
        }


        private string _events;
        [BrowsableAttribute(false)]
        public string Events
        {
            get { return _events; }
            set
            {
                _events = value;
                base.Source = value;
            }
        }

        public override string Tipo
        {
            get
            {
                return "Transaction";
            }
        }
    }
}
