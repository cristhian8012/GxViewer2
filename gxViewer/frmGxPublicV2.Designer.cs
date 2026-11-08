namespace gxViewer
{
    partial class frmGxPublicV2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGxPublicV2));
            this.gxMEXp = new gxViewer.GxModelExplorerv2();
            this.SuspendLayout();
            // 
            // gxMEXp
            // 
            this.gxMEXp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gxMEXp.Location = new System.Drawing.Point(0, 0);
            this.gxMEXp.Name = "gxMEXp";
            this.gxMEXp.Size = new System.Drawing.Size(668, 508);
            this.gxMEXp.TabIndex = 0;
            //this.gxMEXp.OnGetObjects += GxMEXp_OnGetObjects;
            this.gxMEXp.OnGetObjects += new System.EventHandler<gxViewer.GxModelExplorerv2.DistribuidosArgs2>(this.gxMEXp_OnGetObjects);
            this.gxMEXp.Load += new System.EventHandler(this.gxMEXp_Load);
            // 
            // frmGxPublicV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 508);
            this.Controls.Add(this.gxMEXp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGxPublicV2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GxViewer Explorer";
            this.Load += new System.EventHandler(this.testControls_Load);
            this.ResumeLayout(false);

        }

        private void GxMEXp_OnGetObjects(object sender, GxModelExplorerv2.DistribuidosArgs2 e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private GxModelExplorerv2 gxMEXp;

    }
}