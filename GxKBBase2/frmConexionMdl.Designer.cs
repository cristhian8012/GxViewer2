namespace GxKBBase18
{
    partial class frmConexionMdl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConexionMdl));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbSQLite = new System.Windows.Forms.GroupBox();
            this.txtDataSourceSQLITE = new System.Windows.Forms.TextBox();
            this.btnConectarModelSQLITE = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearchDBSQLITE = new System.Windows.Forms.Button();
            this.btnValidarSQLITE = new System.Windows.Forms.Button();
            this.grpSQL = new System.Windows.Forms.GroupBox();
            this.txtConnectionStringSQL = new System.Windows.Forms.TextBox();
            this.chkUseCnnSQL = new System.Windows.Forms.CheckBox();
            this.btnSearchServerSQL = new System.Windows.Forms.Button();
            this.txtServerSQL = new System.Windows.Forms.TextBox();
            this.btnConectarModelSQL = new System.Windows.Forms.Button();
            this.labelServer = new System.Windows.Forms.Label();
            this.btnValidarSQL = new System.Windows.Forms.Button();
            this.txtUserSQL = new System.Windows.Forms.TextBox();
            this.cboDatabaseSQL = new System.Windows.Forms.ComboBox();
            this.chkWinAuthSQL = new System.Windows.Forms.CheckBox();
            this.labelUserSQL = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPasswordSQL = new System.Windows.Forms.TextBox();
            this.labelPwdSQL = new System.Windows.Forms.Label();
            this.cboDBMS = new System.Windows.Forms.ComboBox();
            this.btnGetObjects = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.gbSQLite.SuspendLayout();
            this.grpSQL.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gbSQLite);
            this.groupBox1.Controls.Add(this.grpSQL);
            this.groupBox1.Controls.Add(this.cboDBMS);
            this.groupBox1.Controls.Add(this.btnGetObjects);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(753, 293);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // gbSQLite
            // 
            this.gbSQLite.Controls.Add(this.txtDataSourceSQLITE);
            this.gbSQLite.Controls.Add(this.btnConectarModelSQLITE);
            this.gbSQLite.Controls.Add(this.label3);
            this.gbSQLite.Controls.Add(this.btnSearchDBSQLITE);
            this.gbSQLite.Controls.Add(this.btnValidarSQLITE);
            this.gbSQLite.Location = new System.Drawing.Point(375, 49);
            this.gbSQLite.Name = "gbSQLite";
            this.gbSQLite.Size = new System.Drawing.Size(357, 224);
            this.gbSQLite.TabIndex = 14;
            this.gbSQLite.TabStop = false;
            this.gbSQLite.Text = "SQLite - Parámetros";
            // 
            // txtDataSourceSQLITE
            // 
            this.txtDataSourceSQLITE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.txtDataSourceSQLITE.Location = new System.Drawing.Point(6, 38);
            this.txtDataSourceSQLITE.Name = "txtDataSourceSQLITE";
            this.txtDataSourceSQLITE.Size = new System.Drawing.Size(316, 20);
            this.txtDataSourceSQLITE.TabIndex = 1;
            // 
            // btnConectarModelSQLITE
            // 
            this.btnConectarModelSQLITE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnConectarModelSQLITE.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConectarModelSQLITE.Image = ((System.Drawing.Image)(resources.GetObject("btnConectarModelSQLITE.Image")));
            this.btnConectarModelSQLITE.Location = new System.Drawing.Point(6, 180);
            this.btnConectarModelSQLITE.Name = "btnConectarModelSQLITE";
            this.btnConectarModelSQLITE.Size = new System.Drawing.Size(204, 26);
            this.btnConectarModelSQLITE.TabIndex = 12;
            this.btnConectarModelSQLITE.Text = "&Conectar Modelo";
            this.btnConectarModelSQLITE.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConectarModelSQLITE.UseVisualStyleBackColor = false;
            this.btnConectarModelSQLITE.Click += new System.EventHandler(this.btnConectarModelSQLITE_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Archivo (*.db, *.sqlite)";
            // 
            // btnSearchDBSQLITE
            // 
            this.btnSearchDBSQLITE.Location = new System.Drawing.Point(324, 37);
            this.btnSearchDBSQLITE.Name = "btnSearchDBSQLITE";
            this.btnSearchDBSQLITE.Size = new System.Drawing.Size(29, 22);
            this.btnSearchDBSQLITE.TabIndex = 6;
            this.btnSearchDBSQLITE.Text = "...";
            this.btnSearchDBSQLITE.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearchDBSQLITE.UseVisualStyleBackColor = true;
            this.btnSearchDBSQLITE.Click += new System.EventHandler(this.btnSearchDBSQLITE_Click);
            // 
            // btnValidarSQLITE
            // 
            this.btnValidarSQLITE.Image = ((System.Drawing.Image)(resources.GetObject("btnValidarSQLITE.Image")));
            this.btnValidarSQLITE.Location = new System.Drawing.Point(6, 64);
            this.btnValidarSQLITE.Name = "btnValidarSQLITE";
            this.btnValidarSQLITE.Size = new System.Drawing.Size(116, 24);
            this.btnValidarSQLITE.TabIndex = 6;
            this.btnValidarSQLITE.Text = "Validar Conexión";
            this.btnValidarSQLITE.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnValidarSQLITE.UseVisualStyleBackColor = true;
            this.btnValidarSQLITE.Click += new System.EventHandler(this.btnValidarSQLITE_Click);
            // 
            // grpSQL
            // 
            this.grpSQL.Controls.Add(this.txtConnectionStringSQL);
            this.grpSQL.Controls.Add(this.chkUseCnnSQL);
            this.grpSQL.Controls.Add(this.btnSearchServerSQL);
            this.grpSQL.Controls.Add(this.txtServerSQL);
            this.grpSQL.Controls.Add(this.btnConectarModelSQL);
            this.grpSQL.Controls.Add(this.labelServer);
            this.grpSQL.Controls.Add(this.btnValidarSQL);
            this.grpSQL.Controls.Add(this.txtUserSQL);
            this.grpSQL.Controls.Add(this.cboDatabaseSQL);
            this.grpSQL.Controls.Add(this.chkWinAuthSQL);
            this.grpSQL.Controls.Add(this.labelUserSQL);
            this.grpSQL.Controls.Add(this.label1);
            this.grpSQL.Controls.Add(this.txtPasswordSQL);
            this.grpSQL.Controls.Add(this.labelPwdSQL);
            this.grpSQL.Location = new System.Drawing.Point(12, 49);
            this.grpSQL.Name = "grpSQL";
            this.grpSQL.Size = new System.Drawing.Size(357, 224);
            this.grpSQL.TabIndex = 13;
            this.grpSQL.TabStop = false;
            this.grpSQL.Text = "SQL Server - Parámetros";
            // 
            // txtConnectionStringSQL
            // 
            this.txtConnectionStringSQL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.txtConnectionStringSQL.Location = new System.Drawing.Point(81, 19);
            this.txtConnectionStringSQL.Multiline = true;
            this.txtConnectionStringSQL.Name = "txtConnectionStringSQL";
            this.txtConnectionStringSQL.Size = new System.Drawing.Size(257, 72);
            this.txtConnectionStringSQL.TabIndex = 15;
            // 
            // chkUseCnnSQL
            // 
            this.chkUseCnnSQL.AutoSize = true;
            this.chkUseCnnSQL.Location = new System.Drawing.Point(217, 97);
            this.chkUseCnnSQL.Name = "chkUseCnnSQL";
            this.chkUseCnnSQL.Size = new System.Drawing.Size(128, 21);
            this.chkUseCnnSQL.TabIndex = 14;
            this.chkUseCnnSQL.Text = "Usar Cadena Conex";
            this.chkUseCnnSQL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkUseCnnSQL.UseVisualStyleBackColor = true;
            this.chkUseCnnSQL.CheckedChanged += new System.EventHandler(this.chkUseCnnSQL_CheckedChanged);
            // 
            // btnSearchServerSQL
            // 
            this.btnSearchServerSQL.Enabled = false;
            this.btnSearchServerSQL.Location = new System.Drawing.Point(304, 19);
            this.btnSearchServerSQL.Name = "btnSearchServerSQL";
            this.btnSearchServerSQL.Size = new System.Drawing.Size(29, 22);
            this.btnSearchServerSQL.TabIndex = 13;
            this.btnSearchServerSQL.Text = "...";
            this.btnSearchServerSQL.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearchServerSQL.UseVisualStyleBackColor = true;
            // 
            // txtServerSQL
            // 
            this.txtServerSQL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.txtServerSQL.Location = new System.Drawing.Point(81, 19);
            this.txtServerSQL.Name = "txtServerSQL";
            this.txtServerSQL.Size = new System.Drawing.Size(217, 20);
            this.txtServerSQL.TabIndex = 1;
            // 
            // btnConectarModelSQL
            // 
            this.btnConectarModelSQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnConectarModelSQL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConectarModelSQL.Image = ((System.Drawing.Image)(resources.GetObject("btnConectarModelSQL.Image")));
            this.btnConectarModelSQL.Location = new System.Drawing.Point(81, 180);
            this.btnConectarModelSQL.Name = "btnConectarModelSQL";
            this.btnConectarModelSQL.Size = new System.Drawing.Size(189, 26);
            this.btnConectarModelSQL.TabIndex = 12;
            this.btnConectarModelSQL.Text = "&Conectar Modelo";
            this.btnConectarModelSQL.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConectarModelSQL.UseVisualStyleBackColor = false;
            this.btnConectarModelSQL.Click += new System.EventHandler(this.btnConectarModelSQL_Click);
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Location = new System.Drawing.Point(32, 21);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(46, 13);
            this.labelServer.TabIndex = 0;
            this.labelServer.Text = "&Servidor";
            // 
            // btnValidarSQL
            // 
            this.btnValidarSQL.Image = ((System.Drawing.Image)(resources.GetObject("btnValidarSQL.Image")));
            this.btnValidarSQL.Location = new System.Drawing.Point(81, 119);
            this.btnValidarSQL.Name = "btnValidarSQL";
            this.btnValidarSQL.Size = new System.Drawing.Size(116, 24);
            this.btnValidarSQL.TabIndex = 6;
            this.btnValidarSQL.Text = "Validar Conexión";
            this.btnValidarSQL.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnValidarSQL.UseVisualStyleBackColor = true;
            this.btnValidarSQL.Click += new System.EventHandler(this.btnValidarSQL_Click);
            // 
            // txtUserSQL
            // 
            this.txtUserSQL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtUserSQL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.txtUserSQL.Location = new System.Drawing.Point(81, 45);
            this.txtUserSQL.Name = "txtUserSQL";
            this.txtUserSQL.Size = new System.Drawing.Size(161, 20);
            this.txtUserSQL.TabIndex = 3;
            // 
            // cboDatabaseSQL
            // 
            this.cboDatabaseSQL.AllowDrop = true;
            this.cboDatabaseSQL.FormattingEnabled = true;
            this.cboDatabaseSQL.Location = new System.Drawing.Point(81, 150);
            this.cboDatabaseSQL.Name = "cboDatabaseSQL";
            this.cboDatabaseSQL.Size = new System.Drawing.Size(179, 21);
            this.cboDatabaseSQL.Sorted = true;
            this.cboDatabaseSQL.TabIndex = 9;
            // 
            // chkWinAuthSQL
            // 
            this.chkWinAuthSQL.AutoSize = true;
            this.chkWinAuthSQL.Location = new System.Drawing.Point(81, 97);
            this.chkWinAuthSQL.Name = "chkWinAuthSQL";
            this.chkWinAuthSQL.Size = new System.Drawing.Size(123, 21);
            this.chkWinAuthSQL.TabIndex = 11;
            this.chkWinAuthSQL.Text = "Win.Authentication";
            this.chkWinAuthSQL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkWinAuthSQL.UseVisualStyleBackColor = true;
            this.chkWinAuthSQL.CheckedChanged += new System.EventHandler(this.chkWinAuthSQL_CheckedChanged);
            // 
            // labelUserSQL
            // 
            this.labelUserSQL.AutoSize = true;
            this.labelUserSQL.Location = new System.Drawing.Point(35, 47);
            this.labelUserSQL.Name = "labelUserSQL";
            this.labelUserSQL.Size = new System.Drawing.Size(43, 13);
            this.labelUserSQL.TabIndex = 2;
            this.labelUserSQL.Text = "&Usuario";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "&Base de Datos";
            // 
            // txtPasswordSQL
            // 
            this.txtPasswordSQL.Location = new System.Drawing.Point(81, 71);
            this.txtPasswordSQL.Name = "txtPasswordSQL";
            this.txtPasswordSQL.PasswordChar = '*';
            this.txtPasswordSQL.Size = new System.Drawing.Size(161, 20);
            this.txtPasswordSQL.TabIndex = 5;
            // 
            // labelPwdSQL
            // 
            this.labelPwdSQL.AutoSize = true;
            this.labelPwdSQL.Location = new System.Drawing.Point(17, 73);
            this.labelPwdSQL.Name = "labelPwdSQL";
            this.labelPwdSQL.Size = new System.Drawing.Size(61, 13);
            this.labelPwdSQL.TabIndex = 4;
            this.labelPwdSQL.Text = "&Contraseña";
            // 
            // cboDBMS
            // 
            this.cboDBMS.AllowDrop = true;
            this.cboDBMS.BackColor = System.Drawing.Color.White;
            this.cboDBMS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDBMS.FormattingEnabled = true;
            this.cboDBMS.Location = new System.Drawing.Point(61, 19);
            this.cboDBMS.Name = "cboDBMS";
            this.cboDBMS.Size = new System.Drawing.Size(179, 21);
            this.cboDBMS.TabIndex = 9;
            this.cboDBMS.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnGetObjects
            // 
            this.btnGetObjects.Enabled = false;
            this.btnGetObjects.Image = ((System.Drawing.Image)(resources.GetObject("btnGetObjects.Image")));
            this.btnGetObjects.Location = new System.Drawing.Point(828, 80);
            this.btnGetObjects.Name = "btnGetObjects";
            this.btnGetObjects.Size = new System.Drawing.Size(26, 21);
            this.btnGetObjects.TabIndex = 10;
            this.btnGetObjects.Text = "Retrieve Objects";
            this.btnGetObjects.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "DBMS :";
            // 
            // frmConexionMdl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 294);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConexionMdl";
            this.Text = "Conexión a Modelo GX";
            this.Load += new System.EventHandler(this.frmConexionMdl_Load);
            this.groupBox1.ResumeLayout(false);
            this.gbSQLite.ResumeLayout(false);
            this.gbSQLite.PerformLayout();
            this.grpSQL.ResumeLayout(false);
            this.grpSQL.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnValidarSQL;
        private System.Windows.Forms.CheckBox chkWinAuthSQL;
        public System.Windows.Forms.ComboBox cboDatabaseSQL;
        private System.Windows.Forms.Button btnGetObjects;
        private System.Windows.Forms.TextBox txtPasswordSQL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserSQL;
        private System.Windows.Forms.Label labelPwdSQL;
        private System.Windows.Forms.Label labelUserSQL;
        private System.Windows.Forms.TextBox txtServerSQL;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Button btnConectarModelSQL;
        public System.Windows.Forms.ComboBox cboDBMS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpSQL;
        private System.Windows.Forms.GroupBox gbSQLite;
        private System.Windows.Forms.TextBox txtDataSourceSQLITE;
        private System.Windows.Forms.Button btnConectarModelSQLITE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnValidarSQLITE;
        private System.Windows.Forms.Button btnSearchDBSQLITE;
        private System.Windows.Forms.Button btnSearchServerSQL;
        private System.Windows.Forms.TextBox txtConnectionStringSQL;
        private System.Windows.Forms.CheckBox chkUseCnnSQL;
    }
}