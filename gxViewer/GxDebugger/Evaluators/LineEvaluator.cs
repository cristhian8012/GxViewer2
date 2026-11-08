namespace gxViewer.GxDebugger.Evaluators
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public static class LineEvaluator
    {
        public static void Evaluar(ParsedLine linea, Dictionary<string, object> memoria)
        {
            if (linea.Type == InstructionType.Asignacion)
                EjecutarAsignacion(linea.RawCode, memoria);
        }

        private static void EjecutarAsignacion(string linea, Dictionary<string, object> memoria)
        {
            if (!linea.Contains("=")) return;
            var partes = linea.Split('=');
            if (partes.Length < 2) return;

            string destino = partes[0].Trim();
            string expresion = partes[1].Trim();

            string exprEvaluada = ReemplazarVariables(expresion, memoria);
            var valor = EvaluarExpresion(exprEvaluada);
            memoria[destino] = valor;
        }

        private static string ReemplazarVariables(string expresion, Dictionary<string, object> memoria)
        {
            foreach (var kv in memoria)
                expresion = expresion.Replace(kv.Key, kv.Value?.ToString() ?? "0");
            return expresion;
        }

        private static object EvaluarExpresion(string expr)
        {
            try { return new DataTable().Compute(expr, ""); }
            catch { return null; }
        }
    }
}
