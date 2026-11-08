using System;
using System.Windows.Forms;
using gxViewer.GxObject;
using gxViewer.GxDebugger;

namespace gxViewer.GxDebugger.UI
{
    public partial class frmArrayInspector : System.Windows.Forms.Form
    {
        
        public frmArrayInspector(Variable variable)
        {
            InitializeComponent();
            this.Text = $"🔬 {variable.Name}";
            lst.Items.Add($"[Mock] {variable.Type} {variable.Name} con {variable.Length} elementos");
            lst.Items.Add($"Valor actual: {variable.CurrentValue ?? "(null)"}");
        }

    }
}
