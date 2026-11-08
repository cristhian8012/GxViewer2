using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace gxViewer.GxValidador
{
    public class Coincidencia : IComparable<Coincidencia>
    {

        #region "Atributos"

        private string sTipoObj = String.Empty;
        private string sNombreObj = String.Empty;
        private string sPalabra = String.Empty;
        private string sNroLinea = String.Empty;
        private string sLinea = String.Empty;
        private string sContador = String.Empty;
        private string sFlag = String.Empty;
        private Image sImgFlag;
        private string sRutaImg = String.Empty;
        private object _obj = null;


        #endregion


        #region "Propiedades"

        public string TipoObj
        {
            get
            {
                return sTipoObj;
            }
            set
            {
                sTipoObj = value;
            }
        }


        public string NombreObj
        {
            get
            {
                return sNombreObj;
            }
            set
            {
                sNombreObj = value;
            }
        }

        public string Palabra
        {
            get
            {
                return sPalabra;
            }
            set
            {
                sPalabra = value;
            }
        }

        public string NroLinea
        {
            get
            {
                return sNroLinea;
            }
            set
            {
                sNroLinea = value;
            }
        }


        public string Linea
        {
            get
            {
                return sLinea;
            }
            set
            {
                sLinea = value;
            }
        }

        public string Contador
        {
            get
            {
                return sContador;
            }
            set
            {
                sContador = value;
            }
        }

        public string Flag
        {
            get
            {
                return sFlag;
            }
            set
            {
                sFlag = value;
            }
        }

        public Image ImgFlag
        {
            get
            {
                return sImgFlag;
            }
            set
            {
                sImgFlag = value;
            }
        }

        public string RutaImg
        {
            get
            {
                return sRutaImg;
            }
            set
            {
                sRutaImg = value;
            }
        }

        public object Objeto
        {
            get { return _obj; }
            set { _obj = value; }
        }


        public Int32 CompareTo(Coincidencia other)
        {
            return Linea.CompareTo(other.Linea);
        }  

        public static Comparison<Coincidencia> LineaComparison =
                delegate(Coincidencia p1, Coincidencia p2)
                {
                    return p1.Linea.CompareTo(p2.Linea);
                };

        public override string ToString()
        {
            return string.Format("Linea: {0}", sNroLinea);
        }


        #endregion

    }
}
