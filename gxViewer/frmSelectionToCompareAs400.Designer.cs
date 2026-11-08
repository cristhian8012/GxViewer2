namespace gxViewer
{
    partial class frmSelectionToCompareAs400
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCompare = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbo01 = new System.Windows.Forms.ComboBox();
            this.cbo02 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBeyondCompare = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCompare
            // 
            this.btnCompare.Image = global::gxViewer.Properties.Resources.editar_on;
            this.btnCompare.Location = new System.Drawing.Point(93, 116);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(114, 46);
            this.btnCompare.TabIndex = 0;
            this.btnCompare.Text = "Comparar";
            this.btnCompare.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Objeto 1 ";
            // 
            // cbo01
            // 
            this.cbo01.FormattingEnabled = true;
            this.cbo01.Location = new System.Drawing.Point(93, 41);
            this.cbo01.Name = "cbo01";
            this.cbo01.Size = new System.Drawing.Size(344, 21);
            this.cbo01.TabIndex = 2;
            // 
            // cbo02
            // 
            this.cbo02.FormattingEnabled = true;
            this.cbo02.Location = new System.Drawing.Point(93, 68);
            this.cbo02.Name = "cbo02";
            this.cbo02.Size = new System.Drawing.Size(344, 21);
            this.cbo02.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Objeto 2";
            // 
            // txtBeyondCompare
            // 
            this.txtBeyondCompare.Location = new System.Drawing.Point(121, 4);
            this.txtBeyondCompare.Name = "txtBeyondCompare";
            this.txtBeyondCompare.Size = new System.Drawing.Size(317, 20);
            this.txtBeyondCompare.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Ruta(Beyond Compare):";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(438, 2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(23, 23);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "...";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // frmSelectionToCompareAs400
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 179);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtBeyondCompare);
            this.Controls.Add(this.cbo02);
            this.Controls.Add(this.cbo01);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCompare);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectionToCompareAs400";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Seleccionar Objeto a Comparar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSelectionToCompareAs400_FormClosing);
            this.Load += new System.EventHandler(this.frmSelectionToCompareAs400_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbo01;
        private System.Windows.Forms.ComboBox cbo02;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBeyondCompare;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBuscar;
    }
}