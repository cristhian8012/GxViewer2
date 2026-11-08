using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gxViewer
{
    public partial class frmGxPublic : Form
    {
        public frmGxPublic()
        {
            InitializeComponent();
        }

        private void testControls_Load(object sender, EventArgs e)
        {

        }

        private void gxMEXp_OnGetObjects(object sender, GxModelExplorer.DistribuidosArgs e)
        {
            if (e.XMLDoc != null)
            {
                string _Name = String.Format("{0}_Exp-{1}", e.Modelo, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                bool _ret = ((frmMain)this.Owner).OpenXMLString(e.XMLDoc.OuterXml, _Name);
                if (_ret)
                {
                    ((frmMain)this.Owner).InvokeOpenGXObjectSelected();
                }
            }
        }

        private void gxMEXp_Load(object sender, EventArgs e)
        {

        }
    }
}