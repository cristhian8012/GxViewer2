using System;
using System.Windows.Forms;

namespace gxViewer.GxDebugger.UI
{
    partial class frmParametroPanel
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox lst = new ListBox();

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
            this.lst.Size = new System.Drawing.Size(300, 200);
            this.lst.TabIndex = 0;
            // 
            // frmParametroPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.lst);
            this.Name = "frmParametroPanel";
            this.Text = "🔎 Variables";
            this.ResumeLayout(false);

            this.lst = new ListBox { Dock = DockStyle.Fill };
            
            this.Controls.Add(lst);
            this.ClientSize = new System.Drawing.Size(300, 200);

        }
    }
}
