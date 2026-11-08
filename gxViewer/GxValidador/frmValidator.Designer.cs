namespace gxViewer.GxValidador
{
    partial class frmValidator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmValidator));
            this.wbResult = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbResult
            // 
            this.wbResult.AllowNavigation = false;
            this.wbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbResult.IsWebBrowserContextMenuEnabled = false;
            this.wbResult.Location = new System.Drawing.Point(0, 0);
            this.wbResult.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbResult.Name = "wbResult";
            this.wbResult.Size = new System.Drawing.Size(787, 551);
            this.wbResult.TabIndex = 2;
            // 
            // frmValidator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 551);
            this.Controls.Add(this.wbResult);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmValidator";
            this.Text = "GX Validator";
            this.Load += new System.EventHandler(this.frmValidator_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbResult;
    }
}