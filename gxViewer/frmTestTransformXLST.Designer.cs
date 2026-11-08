
namespace FileToStrBase64
{
    partial class frmTestTransformXLST
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileXML = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.treeListView1 = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.txtXML = new System.Windows.Forms.RichTextBox();
            this.btnCargarXML = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtFileXML);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(856, 28);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(669, 11);
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
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Archivo XML";
            // 
            // txtFileXML
            // 
            this.txtFileXML.Location = new System.Drawing.Point(99, 12);
            this.txtFileXML.Name = "txtFileXML";
            this.txtFileXML.Size = new System.Drawing.Size(568, 20);
            this.txtFileXML.TabIndex = 0;
            this.txtFileXML.Text = "C:\\Users\\user\\Desktop\\tmp\\GX18\\FUENTES-EXP.xml";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 100);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 25);
            this.button2.TabIndex = 2;
            this.button2.Text = "Leer XML";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.treeListView1);
            this.panel2.Controls.Add(this.txtResult);
            this.panel2.Controls.Add(this.txtXML);
            this.panel2.Controls.Add(this.btnCargarXML);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(856, 721);
            this.panel2.TabIndex = 0;
            // 
            // treeListView1
            // 
            this.treeListView1.AllColumns.Add(this.olvColumn1);
            this.treeListView1.AllColumns.Add(this.olvColumn2);
            this.treeListView1.AllColumns.Add(this.olvColumn3);
            this.treeListView1.AllColumns.Add(this.olvColumn4);
            this.treeListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4});
            this.treeListView1.HideSelection = false;
            this.treeListView1.Location = new System.Drawing.Point(16, 307);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.OwnerDraw = true;
            this.treeListView1.ShowGroups = false;
            this.treeListView1.Size = new System.Drawing.Size(787, 192);
            this.treeListView1.TabIndex = 3;
            this.treeListView1.UseCompatibleStateImageBehavior = false;
            this.treeListView1.View = System.Windows.Forms.View.Details;
            this.treeListView1.VirtualMode = true;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.Text = "Nombre";
            this.olvColumn1.Width = 345;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Type";
            this.olvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn2.Text = "Tipo Dato";
            this.olvColumn2.Width = 102;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Description";
            this.olvColumn3.AspectToStringFormat = "";
            this.olvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn3.Text = "Descripción";
            this.olvColumn3.Width = 110;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "IsCollection";
            this.olvColumn4.CheckBoxes = true;
            this.olvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn4.Text = "Es Coleccion?";
            this.olvColumn4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn4.Width = 85;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(3, 131);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(820, 157);
            this.txtResult.TabIndex = 0;
            this.txtResult.Text = "";
            // 
            // txtXML
            // 
            this.txtXML.DetectUrls = false;
            this.txtXML.Location = new System.Drawing.Point(0, 0);
            this.txtXML.Name = "txtXML";
            this.txtXML.Size = new System.Drawing.Size(820, 94);
            this.txtXML.TabIndex = 0;
            this.txtXML.Text = "";
            // 
            // btnCargarXML
            // 
            this.btnCargarXML.Location = new System.Drawing.Point(16, 585);
            this.btnCargarXML.Name = "btnCargarXML";
            this.btnCargarXML.Size = new System.Drawing.Size(82, 25);
            this.btnCargarXML.TabIndex = 2;
            this.btnCargarXML.Text = "Cargar XML";
            this.btnCargarXML.UseVisualStyleBackColor = true;
            this.btnCargarXML.Click += new System.EventHandler(this.btnCargarXML_Click);
            // 
            // frmTestTransformXLST
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 749);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "frmTestTransformXLST";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transformar XML/XLST a HTML";
            this.Load += new System.EventHandler(this.frmTestTransformXLST_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmConvertToBase64_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.frmConvertToBase64_DragOver);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileXML;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox txtXML;
        private System.Windows.Forms.RichTextBox txtResult;
        private BrightIdeasSoftware.TreeListView treeListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private System.Windows.Forms.Button btnCargarXML;
    }
}

