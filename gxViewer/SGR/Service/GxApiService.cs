using System;
using System.IO;
using System.Net;
using System.Text;
using gxViewer.SGR.Models;
using Newtonsoft.Json;

namespace gxViewer.SGR.Services
{

    public class GxApiService
    {
        private const string _UrlApiHub = "https://support.dlya.com.uy/HubServices/api/";
        // Inyectadas desde el login
        public string UrlApiHub { get; set; }        // <-- viene de LoginResponse.hubUrl
        public string Token { get; set; }            // <-- sdtContacto.Sesion
        public int UsuarioId { get; set; }        // <-- sdtContacto.Id
        public string UsuarioLogin { get; set; }     // <-- sdtContacto.Login
        public int SolSteId { get; set; }         // <-- sdtContacto.TipoSolicitante
        public string SolTit { get; set; }           // <-- sdtContacto.Fuente
        public int ClienteId { get; set; }        // <-- sdtContacto.SolicitanteId
        public string ClienteNom { get; set; }       // <-- sdtContacto.SolicitanteNombre
        public string ContactoNom { get; set; }      // <-- sdtContacto.Nombre
        public string ContactoMail { get; set; }     // <-- sdtContacto.Email

        /// <summary>
        /// Construye el servicio a partir de la respuesta de login.
        /// </summary>
        public static GxApiService FromLogin(LoginResponse login)
        {
            var c = login.sdtContacto;
            return new GxApiService
            {
                UrlApiHub = _UrlApiHub, //login.hubUrl,   // https://support.dlya.com.uy/Hub_119/api/
                Token = c.Sesion,
                UsuarioId = c.Id,
                UsuarioLogin = c.Login,
                SolSteId = c.TipoSolicitante,
                SolTit = c.Fuente,
                ClienteId = c.SolicitanteId,
                ClienteNom = c.SolicitanteNombre,
                ContactoNom = c.Nombre,
                ContactoMail = (c.Email ?? "").Trim()
            };
        }

        public GxResponse BuscarPrograma(string programaNombre, int productoId)
        {
            var payload = new
            {
                pToken = Token,
                pUsrIdNro = UsuarioId,
                pTarea = "CODXPZ",
                pUsuarioId = (UsuarioLogin ?? "").ToUpper(),
                pProgramaNombre = programaNombre,
                pProductoId = productoId,
                pSolSteId = SolSteId,
                pSolTit = SolTit,
                ClienteId = ClienteId,
                ClienteNom = ClienteNom,
                ContactoNom = ContactoNom,
                ContactoMail = ContactoMail
            };

            string json = PostJson(UrlApiHub + "Visor", payload);
            var arr = JsonConvert.DeserializeObject<GxResponse[]>(json);
            if (arr == null || arr.Length == 0)
                throw new Exception("Respuesta vacía de la API Visor (CODXPZ).");
            return arr[0];
        }

        public string ObtenerIvServidor()
        {
            var payload = new
            {
                pToken = Token,
                pUsrIdNro = UsuarioId,
                pTarea = "CONSGOL",
                pUsuarioId = (UsuarioLogin ?? "").ToUpper(),
                pSolSteId = SolSteId,
                pSolTit = SolTit
            };

            string json = PostJson(UrlApiHub + "Visor", payload);
            var arr = JsonConvert.DeserializeObject<GxKeyResponse[]>(json);
            if (arr == null || arr.Length == 0 || string.IsNullOrEmpty(arr[0].iv))
                throw new Exception("No se obtuvo IV del servidor (CONSGOL).");
            return arr[0].iv;
        }

        private string PostJson(string url, object payload)
        {
            string body = JsonConvert.SerializeObject(payload);
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Accept = "application/json";
            req.Timeout = 60000;

            byte[] bytes = Encoding.UTF8.GetBytes(body);
            req.ContentLength = bytes.Length;
            using (var s = req.GetRequestStream()) s.Write(bytes, 0, bytes.Length);

            try
            {
                using (var resp = (HttpWebResponse)req.GetResponse())
                using (var reader = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
                    return reader.ReadToEnd();
            }
            catch (WebException wex)
            {
                string err = "";
                if (wex.Response != null)
                    using (var r = new StreamReader(wex.Response.GetResponseStream()))
                        err = r.ReadToEnd();
                throw new Exception("HTTP error: " + wex.Message + " | " + err, wex);
            }
        }
    }
}