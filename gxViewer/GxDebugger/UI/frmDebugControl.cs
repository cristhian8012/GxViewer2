using System;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using gxViewer.GxDebugger.Engine;

namespace gxViewer.GxDebugger.UI
{
    

    public partial class frmDebugControl : Form
    {
        private readonly DebuggerEngine _engine;
        private readonly FastColoredTextBox _txtCode;

        public frmDebugControl(FastColoredTextBox txtCode, DebuggerEngine engine)
        {
            InitializeComponent();
            _engine = engine;
            _txtCode = txtCode;
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            _engine.Step();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _engine.Reset();
        }
    }
}
