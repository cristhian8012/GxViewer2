
namespace gxViewer.GxDebugger.Helpers
{
    using gxViewer.GxObject;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text.RegularExpressions;
    using static System.Runtime.CompilerServices.RuntimeHelpers;

    public enum ParameterMode
    {
        In,
        Out,
        InOut
    }

    public static class ParameterHelper
    {
        public static Dictionary<string, ParameterMode> GetParameters(GXObj pObjGX)
        {
            Dictionary<string, ParameterMode> _ret = new Dictionary<string, ParameterMode>();
            if (pObjGX != null)
            {
                if (!String.IsNullOrEmpty(pObjGX.Rules))
                {
                    string _strParm = CleanCode(pObjGX.Rules);
                    var match = Regex.Match(_strParm, @"Parm\s*\(\s*(.*?)\s*\)\s*;", RegexOptions.Singleline);
                    if (!match.Success)
                        throw new Exception("No se encontró una llamada a Parm(...) válida.");


                    string paramContent = match.Groups[1].Value;
                    string[] rawParams = paramContent.Split(',');

                    ParameterMode currentMode = ParameterMode.InOut; // Por defecto
                    foreach (string param in rawParams.Select(p => p.Trim()))
                    {
                        if (param.StartsWith("In:", StringComparison.OrdinalIgnoreCase))
                        {
                            currentMode = ParameterMode.In;
                            continue;
                        }
                        else if (param.StartsWith("Out:", StringComparison.OrdinalIgnoreCase))
                        {
                            currentMode = ParameterMode.Out;
                            continue;
                        }
                        string _parname = param.TrimStart('&');
                        _ret.Add(_parname, currentMode);
                    }


                }

                //Para despues, evaluar las condiciones 
                if (!String.IsNullOrEmpty(pObjGX.Conditions))
                {

                }
            }

            //pObjGX

            return _ret;
        }

        public static string CleanCode(string code)
        {
            // Eliminar comentarios de línea
            code = Regex.Replace(code, @"//.*", "");

            // Eliminar comentarios multilínea
            code = Regex.Replace(code, @"/\*.*?\*/", "", RegexOptions.Singleline);

            return code;
        }




    }
}
