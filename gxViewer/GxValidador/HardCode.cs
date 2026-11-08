using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gxViewer.GxValidador
{
    public class HardCode
    {
     
        private static List<GXValRegla> _Reglas;

        public static List<GXValRegla> Reglas_GX
        {
            get
            {
                if (_Reglas == null)
                {
                    HardCode.CargarReglas();
                }
                return _Reglas;
            }
            set { _Reglas = value; }
        }

        public static List<GXValRegla> Reglas_AS400
        {
            get
            {
                if (_Reglas == null)
                {
                    HardCode.CargarReglas_AS400();
                }
                return _Reglas;
            }
            set { _Reglas = value; }
        }


        private static void CargarReglas()
        {
            _Reglas = new List<GXValRegla>();
            _Reglas.Add(new GXValRegla("Constantes en Duro", "ValHardCode", 0));
            _Reglas.Add(new GXValRegla("Advertencias", "ValWarning", 0));
            _Reglas.Add(new GXValRegla("Posibles variables no usadas", "ValVarNoUse2", 0));
            _Reglas.Add(new GXValRegla("Variables no Utilizadas", "ValVarNoUse", 0));
            _Reglas.Add(new GXValRegla("For Each sin \"Defined by\"", "ValDefinedBy", 1));
            _Reglas.Add(new GXValRegla("Errores en Where de For Each", "ValErrorWhere", 2));
            _Reglas.Add(new GXValRegla("Documentación", "ValDocument", 10));
            _Reglas.Add(new GXValRegla("Errores en llamadas CALL", "ValErrorCALL", 1));
            _Reglas.Add(new GXValRegla("Palabras restringidas", "ValWordRest", 5));
            _Reglas.Add(new GXValRegla("Ortografía", "ValOrtografia", 8));
        }

        private static void CargarReglas_AS400()
        {
            _Reglas = new List<GXValRegla>();
            //_Reglas.Add(new GXValRegla("Constantes en Duro", "ValHardCode", 0));
            //_Reglas.Add(new GXValRegla("Advertencias", "ValWarning", 0));
            _Reglas.Add(new GXValRegla("Variables no Utilizadas", "ValVarNoUse", 0));
            _Reglas.Add(new GXValRegla("Documentación", "ValDocument", 10));
            //_Reglas.Add(new GXValRegla("Errores en llamadas CALL", "ValErrorCALL", 1));
            _Reglas.Add(new GXValRegla("Palabras restringidas", "ValWordRest", 5));
            _Reglas.Add(new GXValRegla("Ortografía", "ValOrtografia", 8));
        }


    }
}
