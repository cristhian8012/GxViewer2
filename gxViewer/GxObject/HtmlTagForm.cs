using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using static gxViewer.GxEditorHelper.WebPanel;

namespace gxViewer.GxObject
{
    [Serializable]
    public class HtmlTagForm
    {

    }

    [Serializable]
    public abstract class HtmlControlForm
    {

        public string ControlIdTypeId { get; set; }
        public string Classid { get; set; }
        public string ClassName { get; set; }
        public ObjWebFormElementControlType TypeControl { get; set; }
        public int UserMode { get; set; }

        public bool ReadOnly { get; set; } = false;

        public void CloneProperties(HtmlControlForm pDest)
        {
            pDest.ControlIdTypeId = this.ControlIdTypeId;
            pDest.ReadOnly = this.ReadOnly;
            pDest.ClassName = this.ClassName;
            pDest.TypeControl = this.TypeControl;
            pDest.Classid = this.Classid;
            pDest.UserMode = this.UserMode;
        }


    }

}
