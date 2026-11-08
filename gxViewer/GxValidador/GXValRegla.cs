using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gxViewer.GxValidador
{
    public class GXValRegla
    {
        #region "Atributos"
        string _description = String.Empty;
        string _Status = String.Empty;
        public string _OutPutHTML = String.Empty;
        string _Code = String.Empty;
        int _Point = 0;
        System.Drawing.Image _Icon = null;

        List<Coincidencia> _listaConcidencias = new List<Coincidencia>();

        #endregion

        #region "Propiedades"
        public string Decripcion 
        {
            get { return _description; }
            set { _description = value; } 
        }

        public string Estado
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public System.Drawing.Image Icono
        {
            get { return _Icon; }
            set { _Icon = value; }
        }

        public string Metodo
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public int Punto 
        {
            get { return _Point; }
            set { _Point = value; }
        }


        public List<Coincidencia> ListaConcidencias
        {
            get { return _listaConcidencias; }
            set { _listaConcidencias = value; }
        }

        
        #endregion

        public GXValRegla()
        { }

        public GXValRegla(string pDescription, string pMethod, int pPuntos)
        {
            this._description = pDescription;
            this._Code = pMethod;
            this._Point = pPuntos;
        }


    }
}
