using System;
using System.Windows.Forms;
using gxViewer.GxObject;
using gxViewer.SGR.Models;
using gxViewer.SGR.Services;

namespace gxViewer.SGR
{
    public partial class frmBantotalView : System.Windows.Forms.Form
    {
        // === Resultado: solo se llena cuando el usuario pulsa "Obtener en GXViewer" ===
        public GXObj ObjetoResultado { get; private set; }

        private LoginResponse _loginData;
        private GxApiService _api;
        private GxResponse _ultimaRespuesta;    // cache del JSON crudo
        private string _ultimoSource;           // source descifrado pendiente

        public frmBantotalView()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            MostrarPanelLogin();
        }

        // ============================================================
        //  ALTERNANCIA DE PANELES
        // ============================================================

        private void MostrarPanelLogin()
        {
            grpLogin.Visible = true;
            grpBusqueda.Visible = false;
            btnObtener.Enabled = false;
            btnObtener.Visible = false;

            this.AcceptButton = btnIngresar;
            this.CancelButton = btnCerrar;

            lblLoginStatus.Text = "";
            txtUsuario.Focus();
        }

        private void MostrarPanelBusqueda(LoginResponse login)
        {
            grpLogin.Visible = false;
            grpBusqueda.Visible = true;
            btnObtener.Visible = true;
            btnObtener.Enabled = false;

            this.AcceptButton = btnBuscar;
            this.CancelButton = btnCerrar;

            _api = GxApiService.FromLogin(login);
            PintarDatosContacto(login.sdtContacto);
            CargarProductosSegunSolicitante(login.sdtContacto);

            txtPrograma.Focus();
        }

        private void PintarDatosContacto(SdtContacto c)
        {
            if (c == null) return;
            lblContactoNombre.Text = "👤 " + (c.Nombre ?? "").Trim();
            lblContactoSolicitante.Text = "🏢 " + (c.SolicitanteNombre ?? "").Trim();
            lblContactoRol.Text = "Rol: " + (c.Rol ?? "").Trim();
            lblContactoLogin.Text = "Login: " + (c.Login ?? "").Trim();
            lblContactoEmail.Text = "✉ " + (c.Email ?? "").Trim();

            this.Text = string.Format("Bantotal - Visor — {0} ({1})",
                (c.Nombre ?? "").Trim(), (c.SolicitanteNombre ?? "").Trim());
        }

        private void CargarProductosSegunSolicitante(SdtContacto c)
        {
            cboProducto.Items.Clear();

            if (c.SolicitanteId == 194 || c.Id == 3974)
                cboProducto.Items.Add(new ComboItem(10, "Bantotal BROU"));
            else if (c.SolicitanteId == 70)
                cboProducto.Items.Add(new ComboItem(1, "Bantotal 8.0"));
            else if (c.SolicitanteId == 13)
                cboProducto.Items.Add(new ComboItem(2, "Bantotal 9.0"));
            else
            {
                cboProducto.Items.Add(new ComboItem(1, "Bantotal 8.0"));
                cboProducto.Items.Add(new ComboItem(2, "Bantotal 9.0"));
                cboProducto.Items.Add(new ComboItem(5, "Bantotal RPG"));
                cboProducto.Items.Add(new ComboItem(15, "Bantotal 16.0"));
            }
            cboProducto.SelectedIndex = 0;
        }

        // ============================================================
        //  LOGIN
        // ============================================================

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string pass = txtPass.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(pass))
            {
                lblLoginStatus.ForeColor = System.Drawing.Color.DarkRed;
                lblLoginStatus.Text = "Ingresa usuario y contraseña.";
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                btnIngresar.Enabled = false;
                lblLoginStatus.ForeColor = System.Drawing.Color.Black;
                lblLoginStatus.Text = "Autenticando...";
                Application.DoEvents();

                _loginData = LoginService.Login(usuario, pass);
                MostrarPanelBusqueda(_loginData);
            }
            catch (Exception ex)
            {
                lblLoginStatus.ForeColor = System.Drawing.Color.DarkRed;
                lblLoginStatus.Text = "Error: " + ex.Message;
            }
            finally
            {
                Cursor = Cursors.Default;
                btnIngresar.Enabled = true;
            }
        }

        // ============================================================
        //  BUSCAR PROGRAMA (NO entrega nada al Owner todavía)
        // ============================================================

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (_api == null)
            {
                MessageBox.Show("Debes iniciar sesión primero.");
                return;
            }

            string prog = txtPrograma.Text.Trim();
            if (string.IsNullOrEmpty(prog))
            {
                lblStatus.Text = "Ingresa un nombre (ej: HWELCOME).";
                return;
            }

            char c0 = char.ToUpperInvariant(prog[0]);
            if (c0 != 'P' && c0 != 'H' && c0 != 'R' && c0 != 'W')
            {
                lblStatus.Text = "Solo P/H/R/W.";
                return;
            }

            var item = cboProducto.SelectedItem as ComboItem;
            if (item == null) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                btnBuscar.Enabled = false;
                btnObtener.Enabled = false;
                lblStatus.Text = "Consultando API (CODXPZ)...";
                Application.DoEvents();

                var resp = _api.BuscarPrograma(prog, item.Id);
                if (resp.Error != 0)
                {
                    lblStatus.Text = "Error API: " + resp.Descripcion;
                    return;
                }

                // Mostrar SOLO la descripción del objeto
                var p = resp.aPrograma ?? new GxPrograma();
                var o = p.aObjeto ?? new GxObjeto();
                lblDescValue.Text = p.aPrgDesc ?? "(sin descripción)";
                lblTipoValue.Text = "Tipo        : " + (p.aPrgTipo ?? "") + "   |   Nombre: " + p.aPrgNom;
                lblModeloValue.Text = "Modelo      : " + (p.aNomMdl ?? "") + " (Id=" + p.aMdlId + ")";
                lblFModValue.Text = "Últ. modif. : " + (o.ObjFMod ?? "N/D") + "   |   ObjId: " + o.ObjId;
                lblStatus.Text = "Descripción cargada. Pulsa 'Obtener en GXViewer'.";

                // Descifrar source (queda listo, pero NO se envía al Owner todavía)
                lblStatus.Text = "Descifrando source...";
                Application.DoEvents();

                string source = "";
                if (!string.IsNullOrEmpty(resp.pSource))
                {
                    string ivServidor = _api.ObtenerIvServidor();
                    resp.ivServidor = ivServidor;
                    source = GxCrypto.DescifrarSource(resp.pSource, resp.iv, resp.ivServidor);
                }

                // Guardar en caché interna
                _ultimoSource = source;
                _ultimaRespuesta = resp;
                

                // Habilitar botón "Obtener en GXViewer"
                btnObtener.Enabled = true;
                lblStatus.Text = "Listo. " + prog + " cargado. Pulsa 'Obtener en GXViewer' o busca otro.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error: " + ex.Message;
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnBuscar.Enabled = true;
            }
        }

        // ============================================================
        //  OBTENER EN GXVIEWER (aquí SÍ se entrega al Owner)
        // ============================================================

        private void btnObtener_Click(object sender, EventArgs e)
        {
            if (_ultimaRespuesta == null)
            {
                MessageBox.Show("Primero busca un programa.");
                return;
            }

            try
            {
                // 1) Construir GXObj (mismo esquema que HTMLtoGxXML.ConvertXML3)
                ObjetoResultado = JsonToGxObj.Convert(_ultimaRespuesta, _ultimoSource);

                if (ObjetoResultado != null)
                {
                    ((frmMain)this.Owner).Open_GXObj_DLYA(ObjetoResultado);
                }

                // 2) Cerrar este formulario con OK
                DialogResult = DialogResult.OK;
               // Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al preparar GXObj: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        //  CERRAR (sin entregar nada)
        // ============================================================

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private class ComboItem
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public ComboItem(int id, string nombre) { Id = id; Nombre = nombre; }
            public override string ToString() { return Nombre; }
        }
    }
}