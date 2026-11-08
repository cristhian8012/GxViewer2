using System;
using System.Collections.Generic;
using System.Linq;
using gxViewer.SGR.Models;          // modelos GxResponse, GxVariable, etc.
using gxViewer.GxObject;
using Attribute = gxViewer.GxObject.Attribute;       // GXObj, Info, Model, KMW, Variable, GXAtt

namespace gxViewer.SGR.Services
{
    /// <summary>
    /// Convierte la respuesta JSON de la API del Visor Bantotal
    /// en un GXObj compatible con tu GXViewer.
    /// Reemplaza a HTMLtoGxXML.ConvertXML3() para el flujo con API.
    /// </summary>
    public static class JsonToGxObj
    {
        public static GXObj Convert(GxResponse resp, string sourceDescifrado)
        {
            if (resp == null) throw new ArgumentNullException("resp");

            var prog = resp.aPrograma ?? new GxPrograma();
            var obj = prog.aObjeto ?? new GxObjeto();

            // 1) Tipo de objeto -> clase concreta (Procedure, WebPanel, WorkPanel, Report)
            string gxType = MapearTipoGx(prog.aPrgTipo, prog.aPrgNom);
            Type t = Type.GetType("gxViewer.GxObject." + gxType);
            if (t == null) t = typeof(GXObj);   // fallback
            var gxObj = (GXObj)Activator.CreateInstance(t);

            // 2) Info: Name (sin prefijo P/H/W/R), Description, Folder
            string nombreSinPrefijo = QuitarPrefijo(prog.aPrgNom);
            gxObj.Info = new Info
            {
                Name = nombreSinPrefijo,
                Description = prog.aPrgDesc,
                Folder = ""              // la API no lo expone
            };

            // 3) Model + KMW
            gxObj.Model = new Model
            {
                Id = prog.aMdlId.ToString(),
                Name = prog.aNomMdl ?? "Bantotal",
                Type = "Production"
            };
            gxObj.KMW = new KMW
            {
                Path = (prog.aNomMdl ?? "Bantotal").Replace(" ", "_"),
                MajorVersion = 16,
                MinorVersion = 0,
                MaxGxBuildSaved = 0
            };

            // 4) Fechas
            gxObj.CreationDate = NormalizarFecha(prog.aPrgAlta);
            gxObj.LastUpdate = NormalizarFecha(obj.ObjFMod);

            // 5) Rules (pParametros ya viene con "Parm(&X);")
            gxObj.Rules = resp.pParametros ?? "";

            // 6) Source (ya descifrado antes de llamar a este método)
            gxObj.Source = sourceDescifrado ?? "";

            // 7) Subroutines / Conditions: la API no los separa, van dentro del Source
            gxObj.Subroutines = "";
            gxObj.Conditions = "";

            // 8) Variables -> GxObject.Variable[]
            if (resp.pListaVariables != null && resp.pListaVariables.Count > 0)
            {
                var vars = new List<gxViewer.GxObject.Variable>();
                var atts = new List<GXAtt>();

                foreach (var v in resp.pListaVariables)
                {
                    var gv = new gxViewer.GxObject.Variable
                    {
                        Name = v.pVarNombre,
                        Title = v.pVarDsc ?? "",
                        Type = v.pVarTipo ?? "Numeric",
                        Length = (int)v.pVarLargo,
                        Decimals = (int)v.pVarDecimales,
                        Picture = v.pVarPicture ?? "",
                        BasedOn = v.pVarBasadaEn ?? ""
                    };
                    vars.Add(gv);

                    // Si está basada en un atributo, lo agregamos también como GXAtt
                    if (!string.IsNullOrEmpty(gv.BasedOn))
                    {
                        atts.Add(new GXAtt
                        {
                            Attribute = new Attribute
                            {
                                Name = gv.BasedOn,
                                Title = gv.Title,
                                Length = gv.Length
                            }
                        });
                    }
                }

                gxObj.Variables = vars.ToArray();
                if (atts.Count > 0)
                    gxObj.GXAtts = atts.ToArray();
            }

            // USER_METADATA ← pDefinition envuelto en <extra-info format="xml">
            CargarUserMetadata(gxObj, resp.pDefinition);


            // 9) XMLObj: guardamos el JSON crudo para debug / reparseo
            gxObj.XMLObj = Newtonsoft.Json.JsonConvert.SerializeObject(resp);
            gxObj.IsXML = false;

            return gxObj;
        }

        private static string MapearTipoGx(string tipoPrg, string nombrePrg)
        {
            // Preferimos el prefijo del nombre (P/W/R/H) porque aPrgTipo puede variar
            string prefijo = string.IsNullOrEmpty(nombrePrg) ? "" : nombrePrg.Substring(0, 1).ToUpperInvariant();
            switch (prefijo)
            {
                case "P": return "Procedure";
                case "W": return "WorkPanel";
                case "R": return "Report";
                case "H": return "WebPanel";
            }

            // Fallback por aPrgTipo
            switch ((tipoPrg ?? "").ToUpperInvariant())
            {
                case "WBP": return "WebPanel";
                case "PRC": return "Procedure";
                case "RPT": return "Report";
                case "WKP": return "WorkPanel";
            }
            return "GXObj";
        }

        private static string QuitarPrefijo(string nombre)
        {
            if (string.IsNullOrEmpty(nombre)) return nombre;
            // Name del GXObj va sin prefijo (como hacía SetValuesProperties con Substring(1))
            return nombre.Length > 1 ? nombre.Substring(1) : nombre;
        }

        private static string NormalizarFecha(string iso)
        {
            if (string.IsNullOrEmpty(iso) || iso.StartsWith("0001-01-01")) return "";
            try
            {
                var dt = DateTime.Parse(iso);
                return dt.ToString("dd/MM/yyyy HH:mm:ss");
            }
            catch { return iso; }
        }

        /// <summary>
        /// Carga el pDefinition dentro de USER_METADATA envuelto en
        /// <extra-info format="xml">...</extra-info>.
        ///
        /// IMPORTANTE: duplicamos el escape de & (y de < > si los hubiera como texto)
        /// porque el GxEditor actual hace .Replace("&amp;","&") ANTES de BeautifyXML,
        /// lo cual rompería el XML si le pasáramos un solo &amp;. Con doble escape
        /// (&amp;amp;), el Replace lo colapsa a &amp; (válido para LoadXml), y luego
        /// el Replace final del GxEditor lo convierte a & para mostrar.
        /// </summary>
        private static void CargarUserMetadata(GXObj gxObj, string pDefinition)
        {
            if (gxObj == null) return;

            if (gxObj.Propiedades == null)
                gxObj.Propiedades = new Dictionary<string, Property>();

            if (string.IsNullOrWhiteSpace(pDefinition))
                return;

            // 1) pDefinition viene como XML plano (válido) desde el JSON.
            //    Ejemplo: <object-definition ...><web-ui title="&amp;Title" ...>
            //
            // 2) Para que el GxEditor (que hace .Replace("&amp;","&") antes de BeautifyXML)
            //    no rompa el XML, duplicamos el escape de & existentes:
            //    &amp;   → &amp;amp;
            //    &lt;    → &amp;lt;   (por si hubiera)
            //    &gt;    → &amp;gt;
            //    &quot;  → &amp;quot;
            //    &apos;  → &amp;apos;
            //
            //    El orden importa: primero duplicamos & de entidades existentes,
            //    NO tocamos las & que ya son &amp; de datos (porque todas las & en XML
            //    válido ya son &amp; o &lt; etc.).
            //
            //    Truco: reemplazamos "&" por "&amp;" SOLO cuando forma parte de una
            //    entidad HTML conocida, para no duplicar escapes incorrectos.
            //    En la práctica, un XML válido solo tiene & como inicio de entidad,
            //    así que reemplazar TODAS las & por &amp; es seguro.
            string xmlDobleEscapado = pDefinition
                .Replace("&", "&amp;");   // &amp;  → &amp;amp;  |  &lt; → &amp;lt;  |  etc.

            // 3) Envolver en <extra-info format="xml">
            string valor = "<extra-info format=\"xml\">" + xmlDobleEscapado + "</extra-info>";

            gxObj.Propiedades["USER_METADATA"] = new Property
            {
                Type = "string",
                Name = "USER_METADATA",
                Value = valor
            };
        }

    }
}