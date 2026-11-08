using gxViewer.GxObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace gxViewer.GxDebugger.Evaluators
{
   

    public static class ConditionEvaluator
    {
        public static bool Evaluar(string expresion, Dictionary<string, object> memoria, Dictionary<string, Variable> variables)
        {
            foreach (var kv in memoria)
            {
                string nombreBase = kv.Key.TrimStart('&').Split('(')[0].ToLower();
                if (variables.TryGetValue(nombreBase, out var def))
                {
                    string valor = Formatear(def, kv.Value);
                    expresion = Regex.Replace(expresion, $@"\b{Regex.Escape(kv.Key)}\b", valor, RegexOptions.IgnoreCase);
                }
            }

            expresion = expresion.Replace("<>", "!=").Replace("=", "==").Replace("===", "==");
            try { return Convert.ToBoolean(new DataTable().Compute(expresion, "")); }
            catch { return false; }
        }

        private static string Formatear(Variable var, object val)
        {
            if (val == null) return "null";
            return "Revisar";

            //return var.Type.ToLower() switch
            //{
            //    "character" or "char" or "varchar" => $"'{val}'",
            //    "date" or "datetime" => $"'{Convert.ToDateTime(val):yyyy-MM-dd}'",
            //    _ => val.ToString() ?? "null"
            //};
        }
    }
}
