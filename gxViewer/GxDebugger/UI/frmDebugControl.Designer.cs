namespace gxViewer.GxDebugger.UI
{
    partial class frmDebugControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnStep = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // btnStep
            // 
            this.btnStep.Name = "btnStep";
            this.btnStep.Text = "▶ Step";
            this.btnStep.AutoSize = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // btnReset
            // 
            this.btnReset.Name = "btnReset";
            this.btnReset.Text = "↺ Reset";
            this.btnReset.AutoSize = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // flowPanel
            // 
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel.Controls.Add(this.btnStep);
            this.flowPanel.Controls.Add(this.btnReset);
            // 
            // frmDebugControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(200, 50);
            this.Controls.Add(this.flowPanel);
            this.Name = "frmDebugControl";
            this.Text = "🎮 Depuración";
            this.ResumeLayout(false);
        }
    }
}
