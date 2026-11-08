using System.Collections.Generic;
using Newtonsoft.Json;

namespace gxViewer.SGR.Models
{
    public class LoginRequest
    {
        [JsonProperty("Login")] public string Login { get; set; }
        [JsonProperty("Pass")] public string Pass { get; set; }
    }

    public class LoginResponse
    {
        [JsonProperty("sdtContacto")] public SdtContacto sdtContacto { get; set; }
        [JsonProperty("sdtErrores")] public List<object> sdtErrores { get; set; }
        [JsonProperty("sdtNotificaciones")] public List<object> sdtNotificaciones { get; set; }
        [JsonProperty("Res")] public string Res { get; set; }
        [JsonProperty("LoginLDAP")] public string LoginLDAP { get; set; }
        [JsonProperty("Ok")] public string Ok { get; set; }
        [JsonProperty("hubUrl")] public string hubUrl { get; set; }
    }

    public class SdtContacto
    {
        [JsonProperty("Id")] public int Id { get; set; }
        [JsonProperty("Nombre")] public string Nombre { get; set; }
        [JsonProperty("Foto")] public string Foto { get; set; }
        [JsonProperty("SolicitanteId")] public int SolicitanteId { get; set; }
        [JsonProperty("SolicitanteNombre")] public string SolicitanteNombre { get; set; }
        [JsonProperty("TipoSolicitante")] public int TipoSolicitante { get; set; }
        [JsonProperty("Rol")] public string Rol { get; set; }
        [JsonProperty("Telefono")] public string Telefono { get; set; }
        [JsonProperty("Extension")] public string Extension { get; set; }
        [JsonProperty("Email")] public string Email { get; set; }
        [JsonProperty("Login")] public string Login { get; set; }
        [JsonProperty("Master")] public string Master { get; set; }
        [JsonProperty("Baja")] public string Baja { get; set; }
        [JsonProperty("ConfiguracionMail")] public string ConfiguracionMail { get; set; }
        [JsonProperty("Sesion")] public string Sesion { get; set; }
        [JsonProperty("Owner")] public string Owner { get; set; }
        [JsonProperty("Gestion")] public string Gestion { get; set; }
        [JsonProperty("Contratos")] public string Contratos { get; set; }
        [JsonProperty("Fuente")] public string Fuente { get; set; }
        [JsonProperty("Orden")] public string Orden { get; set; }
        [JsonProperty("CtoAdmin")] public string CtoAdmin { get; set; }
        [JsonProperty("CtoEspecial")] public string CtoEspecial { get; set; }
        [JsonProperty("CtoFly")] public string CtoFly { get; set; }
        [JsonProperty("LoginFuentes")] public string LoginFuentes { get; set; }
        [JsonProperty("CliCod")] public string CliCod { get; set; }
        [JsonProperty("ReadOnly")] public string ReadOnly { get; set; }
        [JsonProperty("CtoManten")] public string CtoManten { get; set; }
        [JsonProperty("CtoEstadis")] public string CtoEstadis { get; set; }
        [JsonProperty("GrupoId")] public string GrupoId { get; set; }
        [JsonProperty("LstCon")] public string LstCon { get; set; }
    }
}