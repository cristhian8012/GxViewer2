namespace gxViewer
{
    partial class GxModelExplorer
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearchFolder = new System.Windows.Forms.Button();
            this.txtPathModel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.cboFolder = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboTipoBusqueda = new System.Windows.Forms.ComboBox();
            this.cboTarget = new System.Windows.Forms.ComboBox();
            this.cboModel = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelPie = new System.Windows.Forms.Panel();
            this.btnObtener = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvObjetos = new System.Windows.Forms.DataGridView();
            this.btnDistribuir = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelPie.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjetos)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearchFolder);
            this.panel1.Controls.Add(this.txtPathModel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnConnect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(658, 48);
            this.panel1.TabIndex = 0;
            // 
            // btnSearchFolder
            // 
            this.btnSearchFolder.Location = new System.Drawing.Point(516, 14);
            this.btnSearchFolder.Name = "btnSearchFolder";
            this.btnSearchFolder.Size = new System.Drawing.Size(24, 21);
            this.btnSearchFolder.TabIndex = 3;
            this.btnSearchFolder.Text = "...";
            this.btnSearchFolder.UseVisualStyleBackColor = true;
            this.btnSearchFolder.Click += new System.EventHandler(this.btnSearchFolder_Click);
            // 
            // txtPathModel
            // 
            this.txtPathModel.Location = new System.Drawing.Point(79, 15);
            this.txtPathModel.Name = "txtPathModel";
            this.txtPathModel.Size = new System.Drawing.Size(437, 20);
            this.txtPathModel.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ruta KB : ";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(556, 13);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(81, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Conectar";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(658, 136);
            this.panel2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.cboFolder);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboTipoBusqueda);
            this.groupBox1.Controls.Add(this.cboTarget);
            this.groupBox1.Controls.Add(this.cboModel);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtFind);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(24, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(565, 125);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros de Busqueda";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(455, 50);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 51);
            this.btnBuscar.TabIndex = 16;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cboFolder
            // 
            this.cboFolder.FormattingEnabled = true;
            this.cboFolder.Location = new System.Drawing.Point(70, 97);
            this.cboFolder.Name = "cboFolder";
            this.cboFolder.Size = new System.Drawing.Size(370, 21);
            this.cboFolder.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Folder : ";
            // 
            // cboTipoBusqueda
            // 
            this.cboTipoBusqueda.BackColor = System.Drawing.Color.White;
            this.cboTipoBusqueda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoBusqueda.FormattingEnabled = true;
            this.cboTipoBusqueda.Location = new System.Drawing.Point(70, 45);
            this.cboTipoBusqueda.Name = "cboTipoBusqueda";
            this.cboTipoBusqueda.Size = new System.Drawing.Size(139, 21);
            this.cboTipoBusqueda.TabIndex = 13;
            this.cboTipoBusqueda.SelectedIndexChanged += new System.EventHandler(this.cboTarget_SelectedIndexChanged);
            // 
            // cboTarget
            // 
            this.cboTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cboTarget.FormattingEnabled = true;
            this.cboTarget.Location = new System.Drawing.Point(70, 19);
            this.cboTarget.Name = "cboTarget";
            this.cboTarget.Size = new System.Drawing.Size(139, 21);
            this.cboTarget.TabIndex = 13;
            this.cboTarget.SelectedIndexChanged += new System.EventHandler(this.cboTarget_SelectedIndexChanged);
            // 
            // cboModel
            // 
            this.cboModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cboModel.FormattingEnabled = true;
            this.cboModel.Location = new System.Drawing.Point(215, 19);
            this.cboModel.Name = "cboModel";
            this.cboModel.Size = new System.Drawing.Size(225, 21);
            this.cboModel.TabIndex = 13;
            this.cboModel.SelectedIndexChanged += new System.EventHandler(this.cboModel_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AccessibleRole = System.Windows.Forms.AccessibleRole.Application;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Modelo : ";
            // 
            // cboType
            // 
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(70, 72);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(370, 21);
            this.cboType.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Tipo : ";
            // 
            // txtFind
            // 
            this.txtFind.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFind.Location = new System.Drawing.Point(215, 45);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(225, 20);
            this.txtFind.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Buscar : ";
            // 
            // panelPie
            // 
            this.panelPie.Controls.Add(this.btnDistribuir);
            this.panelPie.Controls.Add(this.btnObtener);
            this.panelPie.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPie.Location = new System.Drawing.Point(0, 480);
            this.panelPie.Name = "panelPie";
            this.panelPie.Size = new System.Drawing.Size(658, 36);
            this.panelPie.TabIndex = 3;
            // 
            // btnObtener
            // 
            this.btnObtener.Location = new System.Drawing.Point(19, 4);
            this.btnObtener.Name = "btnObtener";
            this.btnObtener.Size = new System.Drawing.Size(183, 27);
            this.btnObtener.TabIndex = 1;
            this.btnObtener.Text = "Obtener Objeto en GxViewer";
            this.btnObtener.UseVisualStyleBackColor = true;
            this.btnObtener.Click += new System.EventHandler(this.btnObtener_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 184);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(658, 296);
            this.panel4.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvObjetos);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(658, 296);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resultados de Busqueda";
            // 
            // dgvObjetos
            // 
            this.dgvObjetos.AllowUserToAddRows = false;
            this.dgvObjetos.AllowUserToDeleteRows = false;
            this.dgvObjetos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvObjetos.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvObjetos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvObjetos.ColumnHeadersHeight = 30;
            this.dgvObjetos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvObjetos.GridColor = System.Drawing.Color.Gray;
            this.dgvObjetos.Location = new System.Drawing.Point(3, 16);
            this.dgvObjetos.Name = "dgvObjetos";
            this.dgvObjetos.RowHeadersWidth = 25;
            this.dgvObjetos.Size = new System.Drawing.Size(652, 277);
            this.dgvObjetos.TabIndex = 0;
            this.dgvObjetos.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvObjetos_DataError);
            // 
            // btnDistribuir
            // 
            this.btnDistribuir.Location = new System.Drawing.Point(526, 6);
            this.btnDistribuir.Name = "btnDistribuir";
            this.btnDistribuir.Size = new System.Drawing.Size(120, 25);
            this.btnDistribuir.TabIndex = 2;
            this.btnDistribuir.Text = "Distribuir";
            this.btnDistribuir.UseVisualStyleBackColor = true;
            this.btnDistribuir.Click += new System.EventHandler(this.btnDistribuir_Click);
            // 
            // GxModelExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panelPie);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "GxModelExplorer";
            this.Size = new System.Drawing.Size(658, 516);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelPie.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjetos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtPathModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ComboBox cboFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearchFolder;
        private System.Windows.Forms.ComboBox cboModel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboTarget;
        private System.Windows.Forms.ComboBox cboTipoBusqueda;
        private System.Windows.Forms.Panel panelPie;
        private System.Windows.Forms.Button btnObtener;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvObjetos;
        private System.Windows.Forms.Button btnDistribuir;
    }
}
