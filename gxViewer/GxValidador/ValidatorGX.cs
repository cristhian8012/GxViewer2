using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gxViewer.GxObject;
using System.Reflection;
using System.IO;
using System.Windows.Forms;

namespace gxViewer.GxValidador
{
    public class ValidatorGX
    {
        private List<GXValRegla> _Reglas = HardCode.Reglas_GX;
        private GXObj _Objeto;

        public static List<Variable> VariablesNoUsadas(GXObj _obj)
        {
            List<Variable> _ret = new List<Variable>();
            GXLoadRules.LoadVars(_obj);
            GXValRegla _regla = new GXValRegla("Variables no Utilizadas", "ValVarNoUse", 0);
            GXLoadRules.ValVarNoUse(_regla);

            if (_regla.ListaConcidencias != null)
            {
                foreach (Coincidencia _c in _regla.ListaConcidencias)
                {
                    _ret.Add((Variable)_c.Objeto);
                }
            }
            return _ret;
        }

        public string Name { get; set; } = Application.ProductName;

        public ValidatorGX(GXObj _obj)
        {
            _Objeto = _obj;
            this.ValidateObject();
        }
        
        private void ValidateObject()
        {
            GXLoadRules.LoadVars(_Objeto);

            foreach (GXValRegla _rule in _Reglas)
            {
                Type _tRule = typeof(GXLoadRules); // this.GetType();
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

        public GXObj ObjetoGX
        {
            get { return _Objeto; }
        }


        

    }
}
