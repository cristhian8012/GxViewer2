namespace gxViewer.GxDebugger.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class SQLGeneratorGX
    {
        public List<Dictionary<string, object>> Ejecutar(List<ParsedLine> bloque, Dictionary<string, object> memoria)
        {
            // Acá puedes vincular tu base real. Por ahora simulamos.
            return SimularConsulta(Generar(bloque, memoria));
        }

        public string Generar(List<ParsedLine> bloque, Dictionary<string, object> memoria)
        {
            if (bloque == null || bloque.Count == 0) return "";

            string tabla = ObtenerTablaDesdeComentario(bloque[0].RawCode);
            List<string> camposSelect = new List<string>();
            List<string> condiciones = new List<string>();
            List<string> orden = ObtenerOrdenDesdeCabecera(bloque[0].RawCode);

            foreach (var linea in bloque)
            {
                string txt = linea.RawCode.Trim();

                if (txt.StartsWith("Where", StringComparison.OrdinalIgnoreCase))
                {
                    string clausula = txt.Substring(5).Trim();
                    condiciones.Add(SustituirVariables(clausula, memoria));
                    camposSelect.AddRange(ExtraerAtributos(clausula));
                }
                else if (EsAsignacionDesdeAtributo(txt))
                {
                    string campo = txt.Split('=').Last().Trim();
                    camposSelect.Add(campo);
                }
            }

            camposSelect = camposSelect.Distinct().ToList();

            string select = $"SELECT {string.Join(", ", camposSelect)}";
            string from = $"FROM {tabla}";
            string where = condiciones.Count > 0 ? $"WHERE {string.Join(" AND ", condiciones)}" : "";
            string order = orden.Count > 0 ? $"ORDER BY {string.Join(", ", orden)}" : "";

            return $"{select} {from} {where} {order}".Trim();
        }

        private string ObtenerTablaDesdeComentario(string linea)
        {
            var match = Regex.Match(linea, @"//\s*(\w+)");
            return match.Success ? match.Groups[1].Value : "TablaDesconocida";
        }

        private List<string> ObtenerOrdenDesdeCabecera(string linea)
        {
            var tokens = linea.Replace("(", "#(").Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var orden = new List<string>();

            foreach (var token in tokens)
            {
                if (token.StartsWith("(") || token.StartsWith("#("))
                    orden.Add($"{token.Trim('(', ')').Trim()} DESC");
                else if (token.Equals("For", StringComparison.OrdinalIgnoreCase) ||
                         token.Equals("Each", StringComparison.OrdinalIgnoreCase) ||
                         token.Equals("Order", StringComparison.OrdinalIgnoreCase))
                    continue;
                else if (!token.StartsWith("//"))
                    orden.Add($"{token.Trim()} ASC");
            }

            return orden.Distinct().ToList();
        }

        private string SustituirVariables(string texto, Dictionary<string, object> memoria)
        {
            foreach (var kv in memoria)
            {
                if (texto.Contains(kv.Key))
                {
                    string valor = kv.Value is string ? $"'{kv.Value}'" : kv.Value?.ToString() ?? "null";
                    texto = texto.Replace(kv.Key, valor);
                }
            }
            return texto;
        }

        private List<string> ExtraerAtributos(string clausula)
        {
            var resultado = new List<string>();
            var tokens = clausula.Split(new[] { ' ', '=', '<', '>', '+', '-', '*', '/', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var token in tokens)
            {
                if (!token.StartsWith("&") && Regex.IsMatch(token, @"^[A-Za-z]\w+$"))
                    resultado.Add(token);
            }

            return resultado;
        }

        private bool EsAsignacionDesdeAtributo(string linea)
        {
            return linea.StartsWith("&") && linea.Contains("=") && !linea.Contains("If") && !linea.Contains("Case");
        }

        // Este es un mock, podés reemplazar con ejecución real
        private List<Dictionary<string, object>> SimularConsulta(string sql)
        {
            return new List<Dictionary<string, object>> {

            };
        }
    }
}
