namespace gxViewer
{
    partial class frmGxPublic
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGxPublic));
            this.gxMEXp = new gxViewer.GxModelExplorer();
            this.SuspendLayout();
            // 
            // gxMEXp
            // 
            this.gxMEXp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gxMEXp.Location = new System.Drawing.Point(0, 0);
            this.gxMEXp.Name = "gxMEXp";
            this.gxMEXp.Size = new System.Drawing.Size(668, 508);
            this.gxMEXp.TabIndex = 0;
            this.gxMEXp.OnGetObjects += new System.EventHandler<gxViewer.GxModelExplorer.DistribuidosArgs>(this.gxMEXp_OnGetObjects);
            this.gxMEXp.Load += new System.EventHandler(this.gxMEXp_Load);
            // 
            // frmGxPublic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 508);
            this.Controls.Add(this.gxMEXp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGxPublic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GxViewer Explorer";
            this.Load += new System.EventHandler(this.testControls_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private GxModelExplorer gxMEXp;

    }
}