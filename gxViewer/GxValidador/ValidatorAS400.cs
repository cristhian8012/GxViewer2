using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gxViewer.GxObject;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using gxViewer.AS400;

namespace gxViewer.GxValidador
{
    public class ValidatorAS400 
    {
        private List<GXValRegla> _Reglas = HardCode.Reglas_AS400;
        private As400Object _Objeto;

        //public static List<Variable> VariablesNoUsadas(GXObj _obj)
        //{
        //    List<Variable> _ret = new List<Variable>();
        //    GXLoadRules.LoadVars(_obj);
        //    GXValRegla _regla = new GXValRegla("Variables no Utilizadas", "ValVarNoUse", 0);
        //    GXLoadRules.ValVarNoUse(_regla);

        //    if (_regla.ListaConcidencias != null)
        //    {
        //        foreach (Coincidencia _c in _regla.ListaConcidencias)
        //        {
        //            _ret.Add((Variable)_c.Objeto);
        //        }
        //    }
        //    return _ret;
        //}

        public string Name { get; set; } = "AS400Validator";

        public ValidatorAS400(As400Object _obj)
        {
            _Objeto = _obj;
            this.ValidateObject();
        }
        
        private void ValidateObject()
        {
            AS400LoadRules.LoadVars(_Objeto);

            foreach (GXValRegla _rule in _Reglas)
            {
                Type _tRule = typeof(AS400LoadRules); 
                MethodInfo _tRuleFunc = _tRule.GetMethod(_rule.Metodo);
                if (_tRuleFunc != null)
                {
                    object[] _parms = { _rule };
                    _tRuleFunc.Invoke(this, _parms);
                }
            }
        }


        public List<GXValRegla> Reglas 
        {
            get { return _Reglas; } 
        }

        public As400Object ObjetoAS
        {
            get { return _Objeto; }
        }


        

    }
}
