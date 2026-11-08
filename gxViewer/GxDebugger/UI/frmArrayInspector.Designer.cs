using System.Windows.Forms;

namespace gxViewer.GxDebugger.UI
{
    partial class frmArrayInspector
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox lst;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lst = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lst
            // 
            this.lst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst.FormattingEnabled = true;
            this.lst.ItemHeight = 15;
            this.lst.Location = new System.Drawing.Point(0, 0);
            this.lst.Name = "lst";
            this.lst.Size = new System.Drawing.Size(350, 150);
            this.lst.TabIndex = 0;
            // 
            // frmArrayInspector
            // 
            this.ClientSize = new System.Drawing.Size(350, 150);
            this.Controls.Add(this.lst);
            this.Name = "frmArrayInspector";
            this.Text = "🔬 Array Inspector";
            this.ResumeLayout(false);

            lst.Dock = DockStyle.Fill;
            this.Controls.Add(lst);
            this.ClientSize = new System.Drawing.Size(350, 150);
        }
    }
}
