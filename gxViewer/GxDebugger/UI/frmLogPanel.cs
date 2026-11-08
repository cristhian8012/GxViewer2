using System;
using System.Windows.Forms;
//using WeifenLuo.WinFormsUI.Docking;

namespace gxViewer.GxDebugger.UI
{
    
    public partial class frmLogPanel : Form
    {
        
        public frmLogPanel()
        {
            InitializeComponent();
            this.Text = "🔍 Consola Debug";
        }

        public void AgregarLog(string mensaje)
        {
            lstLog.Items.Add($"{DateTime.Now:HH:mm:ss} | {mensaje}");
            lstLog.TopIndex = lstLog.Items.Count - 1;
        }

        public void Limpiar() => lstLog.Items.Clear();
    }
}
