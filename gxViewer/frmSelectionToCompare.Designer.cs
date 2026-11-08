namespace gxViewer
{
    partial class frmSelectionToCompare
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
            this.chkRules = new System.Windows.Forms.CheckBox();
            this.chkConditions = new System.Windows.Forms.CheckBox();
            this.chkSource = new System.Windows.Forms.CheckBox();
            this.chkDocs = new System.Windows.Forms.CheckBox();
            this.txtBeyondCompare = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.chkVars = new System.Windows.Forms.CheckBox();
            this.chkLayout = new System.Windows.Forms.CheckBox();
            this.rbXML = new System.Windows.Forms.RadioButton();
            this.rbTXT = new System.Windows.Forms.RadioButton();
            this.panelTXT = new System.Windows.Forms.Panel();
            this.panelTXT.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCompare
            // 
            this.btnCompare.Image = global::gxViewer.Properties.Resources.editar_on;
            this.btnCompare.Location = new System.Drawing.Point(463, 164);
            this.btnCompare.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(152, 57);
            this.btnCompare.TabIndex = 0;
            this.btnCompare.Text = "Comparar";
            this.btnCompare.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 56);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Objeto 1 ";
            // 
            // cbo01
            // 
            this.cbo01.FormattingEnabled = true;
            this.cbo01.Location = new System.Drawing.Point(124, 51);
            this.cbo01.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbo01.Name = "cbo01";
            this.cbo01.Size = new System.Drawing.Size(458, 24);
            this.cbo01.TabIndex = 2;
            // 
            // cbo02
            // 
            this.cbo02.FormattingEnabled = true;
            this.cbo02.Location = new System.Drawing.Point(124, 84);
            this.cbo02.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbo02.Name = "cbo02";
            this.cbo02.Size = new System.Drawing.Size(458, 24);
            this.cbo02.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Objeto 2";
            // 
            // chkRules
            // 
            this.chkRules.AutoSize = true;
            this.chkRules.Location = new System.Drawing.Point(9, 6);
            this.chkRules.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkRules.Name = "chkRules";
            this.chkRules.Size = new System.Drawing.Size(66, 21);
            this.chkRules.TabIndex = 3;
            this.chkRules.Text = "Rules";
            this.chkRules.UseVisualStyleBackColor = true;
            // 
            // chkConditions
            // 
            this.chkConditions.AutoSize = true;
            this.chkConditions.Location = new System.Drawing.Point(9, 34);
            this.chkConditions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkConditions.Name = "chkConditions";
            this.chkConditions.Size = new System.Drawing.Size(96, 21);
            this.chkConditions.TabIndex = 3;
            this.chkConditions.Text = "Conditions";
            this.chkConditions.UseVisualStyleBackColor = true;
            // 
            // chkSource
            // 
            this.chkSource.AutoSize = true;
            this.chkSource.Checked = true;
            this.chkSource.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSource.Location = new System.Drawing.Point(119, 6);
            this.chkSource.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkSource.Name = "chkSource";
            this.chkSource.Size = new System.Drawing.Size(75, 21);
            this.chkSource.TabIndex = 3;
            this.chkSource.Text = "Source";
            this.chkSource.UseVisualStyleBackColor = true;
            // 
            // chkDocs
            // 
            this.chkDocs.AutoSize = true;
            this.chkDocs.Location = new System.Drawing.Point(119, 34);
            this.chkDocs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkDocs.Name = "chkDocs";
            this.chkDocs.Size = new System.Drawing.Size(125, 21);
            this.chkDocs.TabIndex = 3;
            this.chkDocs.Text = "Documentation";
            this.chkDocs.UseVisualStyleBackColor = true;
            // 
            // txtBeyondCompare
            // 
            this.txtBeyondCompare.Location = new System.Drawing.Point(161, 5);
            this.txtBeyondCompare.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBeyondCompare.Name = "txtBeyondCompare";
            this.txtBeyondCompare.Size = new System.Drawing.Size(421, 22);
            this.txtBeyondCompare.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Ruta(Beyond Compare):";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(584, 2);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(31, 28);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "...";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // chkVars
            // 
            this.chkVars.AutoSize = true;
            this.chkVars.Location = new System.Drawing.Point(257, 6);
            this.chkVars.Margin = new System.Windows.Forms.Padding(4);
            this.chkVars.Name = "chkVars";
            this.chkVars.Size = new System.Drawing.Size(89, 21);
            this.chkVars.TabIndex = 3;
            this.chkVars.Text = "Variables";
            this.chkVars.UseVisualStyleBackColor = true;
            // 
            // chkLayout
            // 
            this.chkLayout.AutoSize = true;
            this.chkLayout.Location = new System.Drawing.Point(257, 34);
            this.chkLayout.Margin = new System.Windows.Forms.Padding(4);
            this.chkLayout.Name = "chkLayout";
            this.chkLayout.Size = new System.Drawing.Size(120, 21);
            this.chkLayout.TabIndex = 3;
            this.chkLayout.Text = "Layout(Demo)";
            this.chkLayout.UseVisualStyleBackColor = true;
            // 
            // rbXML
            // 
            this.rbXML.AutoSize = true;
            this.rbXML.Location = new System.Drawing.Point(156, 130);
            this.rbXML.Name = "rbXML";
            this.rbXML.Size = new System.Drawing.Size(78, 21);
            this.rbXML.TabIndex = 6;
            this.rbXML.Text = "En XML";
            this.rbXML.UseVisualStyleBackColor = true;
            this.rbXML.CheckedChanged += new System.EventHandler(this.rbXML_CheckedChanged);
            // 
            // rbTXT
            // 
            this.rbTXT.AutoSize = true;
            this.rbTXT.Checked = true;
            this.rbTXT.Location = new System.Drawing.Point(40, 130);
            this.rbTXT.Name = "rbTXT";
            this.rbTXT.Size = new System.Drawing.Size(85, 21);
            this.rbTXT.TabIndex = 6;
            this.rbTXT.TabStop = true;
            this.rbTXT.Text = "En Texto";
            this.rbTXT.UseVisualStyleBackColor = true;
            this.rbTXT.CheckedChanged += new System.EventHandler(this.rbTXT_CheckedChanged);
            // 
            // panelTXT
            // 
            this.panelTXT.BackColor = System.Drawing.Color.White;
            this.panelTXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTXT.Controls.Add(this.chkVars);
            this.panelTXT.Controls.Add(this.chkRules);
            this.panelTXT.Controls.Add(this.chkConditions);
            this.panelTXT.Controls.Add(this.chkSource);
            this.panelTXT.Controls.Add(this.chkDocs);
            this.panelTXT.Controls.Add(this.chkLayout);
            this.panelTXT.Location = new System.Drawing.Point(40, 157);
            this.panelTXT.Name = "panelTXT";
            this.panelTXT.Size = new System.Drawing.Size(399, 64);
            this.panelTXT.TabIndex = 7;
            // 
            // frmSelectionToCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 253);
            this.Controls.Add(this.panelTXT);
            this.Controls.Add(this.rbTXT);
            this.Controls.Add(this.rbXML);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtBeyondCompare);
            this.Controls.Add(this.cbo02);
            this.Controls.Add(this.cbo01);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCompare);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectionToCompare";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Seleccionar Objeto a Comparar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSelectionToCompare_FormClosing);
            this.Load += new System.EventHandler(this.frmSelectionToCompare_Load);
            this.panelTXT.ResumeLayout(false);
            this.panelTXT.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbo01;
        private System.Windows.Forms.ComboBox cbo02;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkRules;
        private System.Windows.Forms.CheckBox chkConditions;
        private System.Windows.Forms.CheckBox chkSource;
        private System.Windows.Forms.CheckBox chkDocs;
        private System.Windows.Forms.TextBox txtBeyondCompare;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.CheckBox chkVars;
        private System.Windows.Forms.CheckBox chkLayout;
        private System.Windows.Forms.RadioButton rbXML;
        private System.Windows.Forms.RadioButton rbTXT;
        private System.Windows.Forms.Panel panelTXT;
    }
}