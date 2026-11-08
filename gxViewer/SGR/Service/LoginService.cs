using System;
using System.IO;
using System.Net;
using System.Text;
using gxViewer.SGR.Models;
using Newtonsoft.Json;

namespace gxViewer.SGR.Services
{
    public static class LoginService
    {
        private const string LoginUrl = "https://supportser.bantotal.com/login";

        public static LoginResponse Login(string usuario, string password)
        {
            var payload = new LoginRequest { Login = usuario, Pass = password };
            string body = JsonConvert.SerializeObject(payload);

            var req = (HttpWebRequest)WebRequest.Create(LoginUrl);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Accept = "application/json";
            req.Timeout = 60000;

            byte[] bytes = Encoding.UTF8.GetBytes(body);
            req.ContentLength = bytes.Length;
            using (var s = req.GetRequestStream()) s.Write(bytes, 0, bytes.Length);

            string json;
            try
            {
                using (var resp = (HttpWebResponse)req.GetResponse())
                using (var reader = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
                    json = reader.ReadToEnd();
            }
            catch (WebException wex)
            {
                string err = "";
                if (wex.Response != null)
                    using (var r = new StreamReader(wex.Response.GetResponseStream()))
                        err = r.ReadToEnd();
                throw new Exception("Error de login HTTP: " + wex.Message + " | " + err, wex);
            }

            var lr = JsonConvert.DeserializeObject<LoginResponse>(json);
            if (lr == null || lr.Ok != "S" || lr.sdtContacto == null)
            {
                string msg = lr != null && lr.Res != null ? "Res=" + lr.Res : "Respuesta inválida";
                throw new Exception("Login rechazado. " + msg);
            }
            return lr;
        }
    }
}