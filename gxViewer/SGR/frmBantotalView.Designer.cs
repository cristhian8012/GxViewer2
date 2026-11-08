namespace gxViewer.SGR
{
    partial class frmBantotalView
    {
        private System.ComponentModel.IContainer components = null;

        // === Panel Login ===
        private System.Windows.Forms.GroupBox grpLogin;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Label lblLoginStatus;

        // === Panel Búsqueda ===
        private System.Windows.Forms.GroupBox grpBusqueda;
        private System.Windows.Forms.Panel pnlContacto;
        private System.Windows.Forms.Label lblContactoNombre;
        private System.Windows.Forms.Label lblContactoSolicitante;
        private System.Windows.Forms.Label lblContactoRol;
        private System.Windows.Forms.Label lblContactoLogin;
        private System.Windows.Forms.Label lblContactoEmail;

        private System.Windows.Forms.Label lblPrograma;
        private System.Windows.Forms.TextBox txtPrograma;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cboProducto;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblStatus;

        private System.Windows.Forms.GroupBox grpDesc;
        private System.Windows.Forms.Label lblDescTitle;
        private System.Windows.Forms.Label lblDescValue;
        private System.Windows.Forms.Label lblTipoValue;
        private System.Windows.Forms.Label lblModeloValue;
        private System.Windows.Forms.Label lblFModValue;

        // Botones inferiores
        private System.Windows.Forms.Button btnObtener;   // "Obtener en GXViewer"
        private System.Windows.Forms.Button btnCerrar;    // "Cerrar"

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBantotalView));
            this.grpLogin = new System.Windows.Forms.GroupBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.lblLoginStatus = new System.Windows.Forms.Label();
            this.grpBusqueda = new System.Windows.Forms.GroupBox();
            this.pnlContacto = new System.Windows.Forms.Panel();
            this.lblContactoNombre = new System.Windows.Forms.Label();
            this.lblContactoSolicitante = new System.Windows.Forms.Label();
            this.lblContactoRol = new System.Windows.Forms.Label();
            this.lblContactoLogin = new System.Windows.Forms.Label();
            this.lblContactoEmail = new System.Windows.Forms.Label();
            this.lblPrograma = new System.Windows.Forms.Label();
            this.txtPrograma = new System.Windows.Forms.TextBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.cboProducto = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.grpDesc = new System.Windows.Forms.GroupBox();
            this.lblDescTitle = new System.Windows.Forms.Label();
            this.lblDescValue = new System.Windows.Forms.Label();
            this.lblTipoValue = new System.Windows.Forms.Label();
            this.lblModeloValue = new System.Windows.Forms.Label();
            this.lblFModValue = new System.Windows.Forms.Label();
            this.btnObtener = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.grpLogin.SuspendLayout();
            this.grpBusqueda.SuspendLayout();
            this.pnlContacto.SuspendLayout();
            this.grpDesc.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLogin
            // 
            this.grpLogin.Controls.Add(this.lblUsuario);
            this.grpLogin.Controls.Add(this.txtUsuario);
            this.grpLogin.Controls.Add(this.lblPass);
            this.grpLogin.Controls.Add(this.txtPass);
            this.grpLogin.Controls.Add(this.btnIngresar);
            this.grpLogin.Controls.Add(this.lblLoginStatus);
            this.grpLogin.Location = new System.Drawing.Point(15, 15);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(625, 140);
            this.grpLogin.TabIndex = 0;
            this.grpLogin.TabStop = false;
            this.grpLogin.Text = "Iniciar sesión";
            // 
            // lblUsuario
            // 
            this.lblUsuario.Location = new System.Drawing.Point(20, 30);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(100, 20);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(143, 28);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(240, 26);
            this.txtUsuario.TabIndex = 1;
            // 
            // lblPass
            // 
            this.lblPass.Location = new System.Drawing.Point(20, 65);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(100, 20);
            this.lblPass.TabIndex = 2;
            this.lblPass.Text = "Contraseña:";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(143, 63);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(240, 26);
            this.txtPass.TabIndex = 3;
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // btnIngresar
            // 
            this.btnIngresar.Location = new System.Drawing.Point(403, 28);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(120, 55);
            this.btnIngresar.TabIndex = 4;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // lblLoginStatus
            // 
            this.lblLoginStatus.ForeColor = System.Drawing.Color.DarkRed;
            this.lblLoginStatus.Location = new System.Drawing.Point(20, 100);
            this.lblLoginStatus.Name = "lblLoginStatus";
            this.lblLoginStatus.Size = new System.Drawing.Size(470, 30);
            this.lblLoginStatus.TabIndex = 5;
            // 
            // grpBusqueda
            // 
            this.grpBusqueda.Controls.Add(this.pnlContacto);
            this.grpBusqueda.Controls.Add(this.lblPrograma);
            this.grpBusqueda.Controls.Add(this.txtPrograma);
            this.grpBusqueda.Controls.Add(this.lblProducto);
            this.grpBusqueda.Controls.Add(this.cboProducto);
            this.grpBusqueda.Controls.Add(this.btnBuscar);
            this.grpBusqueda.Controls.Add(this.lblStatus);
            this.grpBusqueda.Controls.Add(this.grpDesc);
            this.grpBusqueda.Location = new System.Drawing.Point(15, 15);
            this.grpBusqueda.Name = "grpBusqueda";
            this.grpBusqueda.Size = new System.Drawing.Size(625, 400);
            this.grpBusqueda.TabIndex = 1;
            this.grpBusqueda.TabStop = false;
            this.grpBusqueda.Text = "Buscar objeto";
            this.grpBusqueda.Visible = false;
            // 
            // pnlContacto
            // 
            this.pnlContacto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlContacto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContacto.Controls.Add(this.lblContactoNombre);
            this.pnlContacto.Controls.Add(this.lblContactoSolicitante);
            this.pnlContacto.Controls.Add(this.lblContactoRol);
            this.pnlContacto.Controls.Add(this.lblContactoLogin);
            this.pnlContacto.Controls.Add(this.lblContactoEmail);
            this.pnlContacto.Location = new System.Drawing.Point(10, 20);
            this.pnlContacto.Name = "pnlContacto";
            this.pnlContacto.Size = new System.Drawing.Size(605, 55);
            this.pnlContacto.TabIndex = 0;
            // 
            // lblContactoNombre
            // 
            this.lblContactoNombre.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblContactoNombre.Location = new System.Drawing.Point(8, 5);
            this.lblContactoNombre.Name = "lblContactoNombre";
            this.lblContactoNombre.Size = new System.Drawing.Size(280, 20);
            this.lblContactoNombre.TabIndex = 0;
            // 
            // lblContactoSolicitante
            // 
            this.lblContactoSolicitante.Location = new System.Drawing.Point(300, 5);
            this.lblContactoSolicitante.Name = "lblContactoSolicitante";
            this.lblContactoSolicitante.Size = new System.Drawing.Size(295, 20);
            this.lblContactoSolicitante.TabIndex = 1;
            // 
            // lblContactoRol
            // 
            this.lblContactoRol.Location = new System.Drawing.Point(8, 28);
            this.lblContactoRol.Name = "lblContactoRol";
            this.lblContactoRol.Size = new System.Drawing.Size(200, 20);
            this.lblContactoRol.TabIndex = 2;
            // 
            // lblContactoLogin
            // 
            this.lblContactoLogin.Location = new System.Drawing.Point(210, 28);
            this.lblContactoLogin.Name = "lblContactoLogin";
            this.lblContactoLogin.Size = new System.Drawing.Size(140, 20);
            this.lblContactoLogin.TabIndex = 3;
            // 
            // lblContactoEmail
            // 
            this.lblContactoEmail.Location = new System.Drawing.Point(355, 28);
            this.lblContactoEmail.Name = "lblContactoEmail";
            this.lblContactoEmail.Size = new System.Drawing.Size(240, 20);
            this.lblContactoEmail.TabIndex = 4;
            // 
            // lblPrograma
            // 
            this.lblPrograma.Location = new System.Drawing.Point(15, 90);
            this.lblPrograma.Name = "lblPrograma";
            this.lblPrograma.Size = new System.Drawing.Size(80, 20);
            this.lblPrograma.TabIndex = 1;
            this.lblPrograma.Text = "Programa:";
            // 
            // txtPrograma
            // 
            this.txtPrograma.Location = new System.Drawing.Point(100, 88);
            this.txtPrograma.Name = "txtPrograma";
            this.txtPrograma.Size = new System.Drawing.Size(180, 26);
            this.txtPrograma.TabIndex = 2;
            // 
            // lblProducto
            // 
            this.lblProducto.Location = new System.Drawing.Point(300, 90);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(80, 20);
            this.lblProducto.TabIndex = 3;
            this.lblProducto.Text = "Producto:";
            // 
            // cboProducto
            // 
            this.cboProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProducto.Location = new System.Drawing.Point(380, 88);
            this.cboProducto.Name = "cboProducto";
            this.cboProducto.Size = new System.Drawing.Size(150, 28);
            this.cboProducto.TabIndex = 4;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(540, 86);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 25);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(15, 120);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(600, 20);
            this.lblStatus.TabIndex = 6;
            // 
            // grpDesc
            // 
            this.grpDesc.Controls.Add(this.lblDescTitle);
            this.grpDesc.Controls.Add(this.lblDescValue);
            this.grpDesc.Controls.Add(this.lblTipoValue);
            this.grpDesc.Controls.Add(this.lblModeloValue);
            this.grpDesc.Controls.Add(this.lblFModValue);
            this.grpDesc.Location = new System.Drawing.Point(15, 148);
            this.grpDesc.Name = "grpDesc";
            this.grpDesc.Size = new System.Drawing.Size(600, 160);
            this.grpDesc.TabIndex = 7;
            this.grpDesc.TabStop = false;
            this.grpDesc.Text = "Descripción del objeto";
            // 
            // lblDescTitle
            // 
            this.lblDescTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescTitle.Location = new System.Drawing.Point(10, 25);
            this.lblDescTitle.Name = "lblDescTitle";
            this.lblDescTitle.Size = new System.Drawing.Size(110, 24);
            this.lblDescTitle.TabIndex = 0;
            this.lblDescTitle.Text = "Descripción:";
            // 
            // lblDescValue
            // 
            this.lblDescValue.Location = new System.Drawing.Point(123, 25);
            this.lblDescValue.Name = "lblDescValue";
            this.lblDescValue.Size = new System.Drawing.Size(467, 40);
            this.lblDescValue.TabIndex = 1;
            // 
            // lblTipoValue
            // 
            this.lblTipoValue.Location = new System.Drawing.Point(10, 75);
            this.lblTipoValue.Name = "lblTipoValue";
            this.lblTipoValue.Size = new System.Drawing.Size(580, 20);
            this.lblTipoValue.TabIndex = 2;
            // 
            // lblModeloValue
            // 
            this.lblModeloValue.Location = new System.Drawing.Point(10, 100);
            this.lblModeloValue.Name = "lblModeloValue";
            this.lblModeloValue.Size = new System.Drawing.Size(580, 20);
            this.lblModeloValue.TabIndex = 3;
            // 
            // lblFModValue
            // 
            this.lblFModValue.Location = new System.Drawing.Point(10, 125);
            this.lblFModValue.Name = "lblFModValue";
            this.lblFModValue.Size = new System.Drawing.Size(580, 20);
            this.lblFModValue.TabIndex = 4;
            // 
            // btnObtener
            // 
            this.btnObtener.Enabled = false;
            this.btnObtener.Location = new System.Drawing.Point(285, 425);
            this.btnObtener.Name = "btnObtener";
            this.btnObtener.Size = new System.Drawing.Size(184, 30);
            this.btnObtener.TabIndex = 2;
            this.btnObtener.Text = "Obtener en GXViewer";
            this.btnObtener.Click += new System.EventHandler(this.btnObtener_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(512, 425);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(128, 30);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // frmBantotalView
            // 
            this.ClientSize = new System.Drawing.Size(655, 470);
            this.Controls.Add(this.grpLogin);
            this.Controls.Add(this.grpBusqueda);
            this.Controls.Add(this.btnObtener);
            this.Controls.Add(this.btnCerrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmBantotalView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bantotal - Visor";
            this.grpLogin.ResumeLayout(false);
            this.grpLogin.PerformLayout();
            this.grpBusqueda.ResumeLayout(false);
            this.grpBusqueda.PerformLayout();
            this.pnlContacto.ResumeLayout(false);
            this.grpDesc.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}