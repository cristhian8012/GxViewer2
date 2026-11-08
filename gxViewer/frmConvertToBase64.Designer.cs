
namespace FileToStrBase64
{
    partial class frmConvertToBase64
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lblReverse = new System.Windows.Forms.LinkLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.lblReverse);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(820, 55);
            this.panel1.TabIndex = 0;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(21, 37);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(70, 13);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Limpiar Texto";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblReverse
            // 
            this.lblReverse.AutoSize = true;
            this.lblReverse.Location = new System.Drawing.Point(646, 37);
            this.lblReverse.Name = "lblReverse";
            this.lblReverse.Size = new System.Drawing.Size(151, 13);
            this.lblReverse.TabIndex = 3;
            this.lblReverse.TabStop = true;
            this.lblReverse.Text = "Convertir de Base64 a Archivo";
            this.lblReverse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblReverse_LinkClicked);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(706, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 25);
            this.button2.TabIndex = 2;
            this.button2.Text = "Convertir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(660, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Archivo";
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(90, 10);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(568, 20);
            this.txtFile.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtResult);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(820, 443);
            this.panel2.TabIndex = 0;
            // 
            // txtResult
            // 
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Location = new System.Drawing.Point(0, 0);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(820, 443);
            this.txtResult.TabIndex = 0;
            this.txtResult.Text = "";
            // 
            // frmConvertToBase64
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 498);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "frmConvertToBase64";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Convertir de Archivo a Base64";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmConvertToBase64_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.frmConvertToBase64_DragOver);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.LinkLabel lblReverse;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

