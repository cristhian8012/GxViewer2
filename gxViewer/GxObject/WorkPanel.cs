using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace gxViewer.GxObject
{
    [Serializable]
    public class WorkPanel : GXObj
    {
        private string _events;
        private string _subrutines;

        [BrowsableAttribute(false)]
        public string Events
        {
            get { return _events; }
            set
            {
                _events = value;
                base.Source = String.Format("{0}\n{1}",_events, _subrutines);
            }
        }
        [BrowsableAttribute(false)]
        public new string Subroutines
        {
            get { return _subrutines; }
            set 
            {
                _subrutines = value;
                base.Source = String.Format("{0}\n{1}", _events, _subrutines);
            }
        }


        private Form _form;
        [BrowsableAttribute(false)]
        public Form Form
        {
            get { return _form; }
            set { _form = value; }
        }

        private HTMLForm _htmlform;
        [BrowsableAttribute(false)]
        public HTMLForm HTMLForm
        {
            get { return _htmlform; }
            set { _htmlform = value; }
        }



        public override string Tipo
        {
            get
            {
                return "WorkPanel";
            }
        }

    }
}
