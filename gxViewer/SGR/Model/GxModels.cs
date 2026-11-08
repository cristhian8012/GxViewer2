using System.Collections.Generic;
using Newtonsoft.Json;

namespace gxViewer.SGR.Models
{
    public class GxResponse
    {
        [JsonProperty("Id")] public int Id { get; set; }
        [JsonProperty("aUsrId")] public string aUsrId { get; set; }
        [JsonProperty("aPrograma")] public GxPrograma aPrograma { get; set; }
        [JsonProperty("pListaVariables")] public List<GxVariable> pListaVariables { get; set; }
        [JsonProperty("pSource")] public string pSource { get; set; }
        [JsonProperty("iv")] public string iv { get; set; }
        [JsonProperty("pParametros")] public string pParametros { get; set; }
        [JsonProperty("pDefinition")] public string pDefinition { get; set; }
        [JsonProperty("Error")] public int Error { get; set; }
        [JsonProperty("Descripcion")] public string Descripcion { get; set; }
        [JsonProperty("aProductoId")] public int aProductoId { get; set; }

        [JsonProperty("ivServidor")] public string ivServidor { get; set; }

    }

    public class GxPrograma
    {
        [JsonProperty("aPrgId")] public int aPrgId { get; set; }
        [JsonProperty("aPrgNom")] public string aPrgNom { get; set; }
        [JsonProperty("aPrgDesc")] public string aPrgDesc { get; set; }
        [JsonProperty("aPrgAlta")] public string aPrgAlta { get; set; }
        [JsonProperty("aPrgTipo")] public string aPrgTipo { get; set; }
        [JsonProperty("aNomMdl")] public string aNomMdl { get; set; }
        [JsonProperty("aMdlId")] public int aMdlId { get; set; }
        [JsonProperty("aMdlActivo")] public string aMdlActivo { get; set; }
        [JsonProperty("aObjeto")] public GxObjeto aObjeto { get; set; }
    }

    public class GxObjeto
    {
        [JsonProperty("ObjId")] public long ObjId { get; set; }
        [JsonProperty("ObjNom")] public string ObjNom { get; set; }
        [JsonProperty("ObjFAlta")] public string ObjFAlta { get; set; }
        [JsonProperty("ObjFMod")] public string ObjFMod { get; set; }
        [JsonProperty("TipObjId")] public int TipObjId { get; set; }
    }

    public class GxVariable
    {
        [JsonProperty("pVarNombre")] public string pVarNombre { get; set; }
        [JsonProperty("pVarDsc")] public string pVarDsc { get; set; }
        [JsonProperty("pVarTipo")] public string pVarTipo { get; set; }
        [JsonProperty("pVarLargo")] public double pVarLargo { get; set; }
        [JsonProperty("pVarDecimales")] public double pVarDecimales { get; set; }
        [JsonProperty("pVarBasadaEn")] public string pVarBasadaEn { get; set; }
        [JsonProperty("pVarPicture")] public string pVarPicture { get; set; }
        [JsonProperty("pVarRows")] public string pVarRows { get; set; }
    }

    // Para la tarea CONSGOL (obtener iv del servidor)
    public class GxKeyResponse
    {
        [JsonProperty("iv")] public string iv { get; set; }
    }
}