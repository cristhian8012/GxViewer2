namespace gxViewer
{
    partial class Form1
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
            this.btnAbrir = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.tbObjeto = new System.Windows.Forms.TabControl();
            this.tabRules = new System.Windows.Forms.TabPage();
            this.txtRules = new System.Windows.Forms.RichTextBox();
            this.tabCode = new System.Windows.Forms.TabPage();
            this.txtCode = new System.Windows.Forms.RichTextBox();
            this.tabConditions = new System.Windows.Forms.TabPage();
            this.txtConditions = new System.Windows.Forms.RichTextBox();
            this.tabHelp = new System.Windows.Forms.TabPage();
            this.wbhelp = new System.Windows.Forms.WebBrowser();
            this.tabDocument = new System.Windows.Forms.TabPage();
            this.wbdoc = new System.Windows.Forms.WebBrowser();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tbObjeto.SuspendLayout();
            this.tabRules.SuspendLayout();
            this.tabCode.SuspendLayout();
            this.tabConditions.SuspendLayout();
            this.tabHelp.SuspendLayout();
            this.tabDocument.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAbrir
            // 
            this.btnAbrir.Location = new System.Drawing.Point(365, 33);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(75, 23);
            this.btnAbrir.TabIndex = 0;
            this.btnAbrir.Text = "Ver";
            this.btnAbrir.UseVisualStyleBackColor = true;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(12, 35);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(347, 20);
            this.txtPath.TabIndex = 1;
            // 
            // tbObjeto
            // 
            this.tbObjeto.Controls.Add(this.tabRules);
            this.tbObjeto.Controls.Add(this.tabCode);
            this.tbObjeto.Controls.Add(this.tabConditions);
            this.tbObjeto.Controls.Add(this.tabHelp);
            this.tbObjeto.Controls.Add(this.tabDocument);
            this.tbObjeto.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbObjeto.Location = new System.Drawing.Point(0, 62);
            this.tbObjeto.Multiline = true;
            this.tbObjeto.Name = "tbObjeto";
            this.tbObjeto.SelectedIndex = 0;
            this.tbObjeto.Size = new System.Drawing.Size(635, 350);
            this.tbObjeto.TabIndex = 2;
            // 
            // tabRules
            // 
            this.tabRules.Controls.Add(this.txtRules);
            this.tabRules.Location = new System.Drawing.Point(4, 22);
            this.tabRules.Name = "tabRules";
            this.tabRules.Padding = new System.Windows.Forms.Padding(3);
            this.tabRules.Size = new System.Drawing.Size(627, 324);
            this.tabRules.TabIndex = 0;
            this.tabRules.Text = "Reglas";
            this.tabRules.UseVisualStyleBackColor = true;
            // 
            // txtRules
            // 
            this.txtRules.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRules.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRules.Location = new System.Drawing.Point(3, 3);
            this.txtRules.Margin = new System.Windows.Forms.Padding(10);
            this.txtRules.Name = "txtRules";
            this.txtRules.Size = new System.Drawing.Size(621, 318);
            this.txtRules.TabIndex = 0;
            this.txtRules.Text = "";
            // 
            // tabCode
            // 
            this.tabCode.Controls.Add(this.txtCode);
            this.tabCode.Location = new System.Drawing.Point(4, 22);
            this.tabCode.Name = "tabCode";
            this.tabCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabCode.Size = new System.Drawing.Size(627, 324);
            this.tabCode.TabIndex = 1;
            this.tabCode.Text = "Codigo Fuente";
            this.tabCode.UseVisualStyleBackColor = true;
            // 
            // txtCode
            // 
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.Location = new System.Drawing.Point(3, 3);
            this.txtCode.Margin = new System.Windows.Forms.Padding(10);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(621, 318);
            this.txtCode.TabIndex = 1;
            this.txtCode.Text = "";
            // 
            // tabConditions
            // 
            this.tabConditions.Controls.Add(this.txtConditions);
            this.tabConditions.Location = new System.Drawing.Point(4, 22);
            this.tabConditions.Name = "tabConditions";
            this.tabConditions.Size = new System.Drawing.Size(627, 324);
            this.tabConditions.TabIndex = 2;
            this.tabConditions.Text = "Condiciones";
            this.tabConditions.UseVisualStyleBackColor = true;
            // 
            // txtConditions
            // 
            this.txtConditions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConditions.Location = new System.Drawing.Point(0, 0);
            this.txtConditions.Margin = new System.Windows.Forms.Padding(10);
            this.txtConditions.Name = "txtConditions";
            this.txtConditions.Size = new System.Drawing.Size(627, 324);
            this.txtConditions.TabIndex = 1;
            this.txtConditions.Text = "";
            // 
            // tabHelp
            // 
            this.tabHelp.Controls.Add(this.wbhelp);
            this.tabHelp.Location = new System.Drawing.Point(4, 22);
            this.tabHelp.Name = "tabHelp";
            this.tabHelp.Size = new System.Drawing.Size(627, 324);
            this.tabHelp.TabIndex = 3;
            this.tabHelp.Text = "Ayuda";
            this.tabHelp.UseVisualStyleBackColor = true;
            // 
            // wbhelp
            // 
            this.wbhelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbhelp.Location = new System.Drawing.Point(0, 0);
            this.wbhelp.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbhelp.Name = "wbhelp";
            this.wbhelp.Size = new System.Drawing.Size(627, 324);
            this.wbhelp.TabIndex = 0;
            // 
            // tabDocument
            // 
            this.tabDocument.Controls.Add(this.wbdoc);
            this.tabDocument.Location = new System.Drawing.Point(4, 22);
            this.tabDocument.Name = "tabDocument";
            this.tabDocument.Size = new System.Drawing.Size(627, 324);
            this.tabDocument.TabIndex = 4;
            this.tabDocument.Text = "Documentacion";
            this.tabDocument.UseVisualStyleBackColor = true;
            // 
            // wbdoc
            // 
            this.wbdoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbdoc.Location = new System.Drawing.Point(0, 0);
            this.wbdoc.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbdoc.Name = "wbdoc";
            this.wbdoc.Size = new System.Drawing.Size(627, 324);
            this.wbdoc.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Archivo XML, XPW";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(456, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Open zip";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(556, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Open";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 412);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbObjeto);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnAbrir);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tbObjeto.ResumeLayout(false);
            this.tabRules.ResumeLayout(false);
            this.tabCode.ResumeLayout(false);
            this.tabConditions.ResumeLayout(false);
            this.tabHelp.ResumeLayout(false);
            this.tabDocument.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAbrir;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TabControl tbObjeto;
        private System.Windows.Forms.TabPage tabRules;
        private System.Windows.Forms.TabPage tabCode;
        private System.Windows.Forms.TabPage tabConditions;
        private System.Windows.Forms.TabPage tabHelp;
        private System.Windows.Forms.TabPage tabDocument;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtRules;
        private System.Windows.Forms.RichTextBox txtCode;
        private System.Windows.Forms.RichTextBox txtConditions;
        private System.Windows.Forms.WebBrowser wbhelp;
        private System.Windows.Forms.WebBrowser wbdoc;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

