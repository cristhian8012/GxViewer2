using System.Windows.Forms;

namespace gxViewer.GxDebugger.UI
{
    partial class frmLogPanel
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox lstLog = new ListBox();

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lstLog = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstLog
            // 
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.FormattingEnabled = true;
            this.lstLog.ItemHeight = 15;
            this.lstLog.Location = new System.Drawing.Point(0, 0);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(600, 200);
            this.lstLog.TabIndex = 0;
            // 
            // frmLogPanel
            // 
            this.ClientSize = new System.Drawing.Size(600, 200);
            this.Controls.Add(this.lstLog);
            this.Name = "frmLogPanel";
            this.Text = "🔍 Consola Debug";
            this.ResumeLayout(false);
        }
    }
}
