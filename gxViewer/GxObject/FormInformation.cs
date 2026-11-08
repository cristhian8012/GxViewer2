using System;
using System.Collections.Generic;
using System.Text;

namespace gxViewer.GxObject
{
    [Serializable]
    public class FormInformation
    {
        public string FormId { get; set; }
        public string FormClass { get; set; }
        public FormWindow FormWindow { get; set; }


    }
}
