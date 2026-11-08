using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using gxViewer.GxObject;

namespace gxViewer.GxValidador
{
    public static class GXHtmlRules
    {
        private static GXObj _Objeto;

        public static void LoadVars(GXObj _Obj)
        {
            _Objeto = _Obj;

        }


        public static void ValHardCode(GXValRegla _Regla)
        {

        }

        public static void ValWarning(GXValRegla _Regla)
        {

        }

        public static void ValVarNoUse(GXValRegla _Regla)
        {
            StringBuilder _htmlStruct = new StringBuilder();

            string _titulo = @"<div>
                                <span class='TableVariablesTitulo'>#TITLE#</span><br>
                                <span class='TableVariablesSubTitulo'>Se encontraron un total de #NRO# variables no utilizadas:</span>
                                <br>
                                <table width='691' cellpadding='0' cellspacing='0'  class='TableVariablesNoUsada'>
                                ";
            _titulo = _titulo.Replace("#TITLE#", _Regla.Decripcion).Replace("#NRO#", _Regla.ListaConcidencias.Count.ToString());

            _htmlStruct.AppendLine(_titulo);


            int _NroVar = 1;
            string _v1 = String.Empty;
            string _v2 = String.Empty;
            string _v3 = String.Empty;
            string _v4 = String.Empty;
            string _v5 = String.Empty;
            string _filaret = String.Empty;
            string _FilaHtml = @"
                  <tr class='FilaVariablesNoUsada'>
                    <td class='CeldaVariablesNoUsada'>&nbsp;#V1#</td>
                    <td class='CeldaVariablesNoUsada'>&nbsp;#V2#</td>
                    <td class='CeldaVariablesNoUsada'>&nbsp;#V3#</td>
                    <td class='CeldaVariablesNoUsada'>&nbsp;#V4#</td>
                    <td class='CeldaVariablesNoUsada'>&nbsp;#V5#</td>
                  </tr>";

            //_Regla.ListaConcidencias.Sort

            //List<Coincidencia> _listaOrdenada = _Regla.ListaConcidencias.Sort(delegate(Coincidencia n1, Coincidencia n2) { return n1.Linea.CompareTo((n2.Linea); });
            _Regla.ListaConcidencias.Sort(Coincidencia.LineaComparison);

            foreach (Coincidencia _c in _Regla.ListaConcidencias)
            {

                _filaret = string.Empty;
                string _variable = _c.Linea.Replace("&", "&amp;");
                switch (_NroVar)
                {
                    case 1: _v1 = _variable; _NroVar++; break;
                    case 2: _v2 = _variable; _NroVar++; break;
                    case 3: _v3 = _variable; _NroVar++; break;
                    case 4: _v4 = _variable; _NroVar++; break;
                    case 5: _v5 = _variable;
                        _NroVar = 0;
                        _filaret = _FilaHtml.Replace("#V1#", _v1).Replace("#V2#", _v2).Replace("#V3#", _v3).Replace("#V4#", _v4).Replace("#V5#", _v5);
                        _htmlStruct.AppendLine(_filaret);

                        _v1 = String.Empty;
                        _v2 = String.Empty;
                        _v3 = String.Empty;
                        _v4 = String.Empty;
                        _v5 = String.Empty;

                        _NroVar = 1;
                        break;
                }
            }
            if ( (_NroVar > 0 && _NroVar <= 5) && _filaret == string.Empty )
            {
                _filaret = _FilaHtml.Replace("#V1#", _v1).Replace("#V2#", _v2).Replace("#V3#", _v3).Replace("#V4#", _v4).Replace("#V5#", _v5);
                _htmlStruct.AppendLine(_filaret);
            }

            _htmlStruct.AppendLine("</table></div>");

            _Regla._OutPutHTML = _htmlStruct.ToString();

        }


        public static void ValVarNoUse2(GXValRegla _Regla)
        {

        }
        public static void ValDefinedBy(GXValRegla _Regla)
        {
            if (_Regla.ListaConcidencias != null && _Regla.ListaConcidencias.Count == 0)
            {
                _Regla._OutPutHTML = string.Empty;
                return;
            }


            StringBuilder _htmlStruct = new StringBuilder();

            string _titulo = @"<br /> <div>
                    <span class='TableVariablesTitulo'>#TITLE#</span><br>
                    <span class='TableVariablesSubTitulo'>Se encontraron un total de #NRO# foreach sin defined by:</span>
                    <br>
                    <table width='691' cellpadding='0' cellspacing='0'  class='TableVariablesNoUsada'>
                    ";
            _titulo = _titulo.Replace("#TITLE#", _Regla.Decripcion).Replace("#NRO#", _Regla.ListaConcidencias.Count.ToString());

            _htmlStruct.AppendLine(_titulo);

            string _filaret = String.Empty;
            string _FilaHtml = @"
              <tr class=''>
                <td class='CeldaOrtografia' ColSpan='4' style='width:690px;' nowrap >#V1#</td>
              </tr>";

            _htmlStruct.AppendLine(_FilaHtml.Replace("#V1#", "Descripción Error").Replace("td", "th").Replace("CeldaOrtografia", "CabeceraOrtografia"));

            _Regla.ListaConcidencias.Sort(Coincidencia.LineaComparison);

            foreach (Coincidencia _c in _Regla.ListaConcidencias)
            {
                _filaret = string.Empty;
                string _descripcion = $"Error en ForEach sin Defined by en Línea:{_c.Linea} ";
                
                _filaret = _FilaHtml.Replace("#V1#", _descripcion);
                _htmlStruct.AppendLine(_filaret);
            }

            _htmlStruct.AppendLine("</table></div>");

            _Regla._OutPutHTML = _htmlStruct.ToString();
        }

        public static void ValErrorWhere(GXValRegla _Regla)
        {

        }

        public static void ValDocument(GXValRegla _Regla)
        {
            if (_Regla.ListaConcidencias != null && _Regla.ListaConcidencias.Count == 0)
            {
                _Regla._OutPutHTML = string.Empty;
                return;
            }

            StringBuilder _htmlStruct = new StringBuilder();

            string _titulo = @"<br /> <div>
                    <span class='TableVariablesTitulo'>#TITLE#</span><br>
                    <span class='TableVariablesSubTitulo'>Se encontraron un total de #NRO# problemas en la documentación del objeto:</span>
                    <br>
                    <table width='691' cellpadding='0' cellspacing='0'  class='TableVariablesNoUsada'>
                    ";
            _titulo = _titulo.Replace("#TITLE#", _Regla.Decripcion).Replace("#NRO#", _Regla.ListaConcidencias.Count.ToString());

            _htmlStruct.AppendLine(_titulo);



            string _filaret = String.Empty;
            string _FilaHtml = @"
      <tr class=''>
        <td class='CeldaOrtografia' style='width:100px;' nowrap>#V1#</td>
        <td class='CeldaOrtografia' ColSpan='4' style='width:555px;' nowrap >#V2#</td>
      </tr>";

            _htmlStruct.AppendLine(_FilaHtml.Replace("#V1#", "Atributo").Replace("#V2#", "Descripción Error").Replace("td", "th").Replace("CeldaOrtografia", "CabeceraOrtografia"));

            //_Regla.ListaConcidencias.Sort(Coincidencia.LineaComparison);

            foreach (Coincidencia _c in _Regla.ListaConcidencias)
            {
                _filaret = string.Empty;
                _filaret = _FilaHtml.Replace("#V1#", _c.Objeto.ToString()).Replace("#V2#", _c.Palabra);
                _htmlStruct.AppendLine(_filaret);
            }

            _htmlStruct.AppendLine("</table></div>");

            _Regla._OutPutHTML = _htmlStruct.ToString();


        }

        public static void ValErrorCALL(GXValRegla _Regla)
        {

            if (_Regla.ListaConcidencias != null && _Regla.ListaConcidencias.Count == 0)
            {
                _Regla._OutPutHTML = string.Empty;
                return;
            }


            StringBuilder _htmlStruct = new StringBuilder();

            string _titulo = @"<br /> <div>
                    <span class='TableVariablesTitulo'>#TITLE#</span><br>
                    <span class='TableVariablesSubTitulo'>Se encontraron un total de #NRO# en llamadas a programas con longitud fija:</span>
                    <br>
                    <table width='691' cellpadding='0' cellspacing='0'  class='TableVariablesNoUsada'>
                    ";
            _titulo = _titulo.Replace("#TITLE#", _Regla.Decripcion).Replace("#NRO#", _Regla.ListaConcidencias.Count.ToString());

            _htmlStruct.AppendLine(_titulo);

            string _filaret = String.Empty;
            string _FilaHtml = @"
              <tr class=''>
                <td class='CeldaOrtografia' ColSpan='4' style='width:690px;' nowrap >#V1#</td>
              </tr>";

            _htmlStruct.AppendLine(_FilaHtml.Replace("#V1#", "Descripción Error").Replace("td", "th").Replace("CeldaOrtografia", "CabeceraOrtografia"));

            _Regla.ListaConcidencias.Sort(Coincidencia.LineaComparison);

            foreach (Coincidencia _c in _Regla.ListaConcidencias)
            {
                _filaret = string.Empty;
                string _descripcion = $"Error Línea:{_c.Linea} <br>{_c.Palabra}";

                _filaret = _FilaHtml.Replace("#V1#", _descripcion);
                _htmlStruct.AppendLine(_filaret);
            }

            _htmlStruct.AppendLine("</table></div>");

            _Regla._OutPutHTML = _htmlStruct.ToString();

        }

        public static void ValWordRest(GXValRegla _Regla)
        {
            if (_Regla.ListaConcidencias != null && _Regla.ListaConcidencias.Count == 0)
            {
                _Regla._OutPutHTML = string.Empty;
                return;
            }

            StringBuilder _htmlStruct = new StringBuilder();

            string _titulo = @"<br /> <div>
                    <span class='TableVariablesTitulo'>#TITLE#</span><br>
                    <span class='TableVariablesSubTitulo'>Se encontraron un total de #NRO# palabras reservadas en uso:</span>
                    <br>
                    <table width='691' cellpadding='0' cellspacing='0'  class='TableVariablesNoUsada'>
                    ";
            _titulo = _titulo.Replace("#TITLE#", _Regla.Decripcion).Replace("#NRO#", _Regla.ListaConcidencias.Count.ToString());

            _htmlStruct.AppendLine(_titulo);

            string _filaret = String.Empty;
            string _FilaHtml = @"
      <tr class=''>
        <td class='CeldaOrtografia' style='width:150px;' nowrap>#V1#</td>
        <td class='CeldaOrtografia' ColSpan='4' style='width:530px;' nowrap >#V2#</td>
      </tr>";

            _htmlStruct.AppendLine(_FilaHtml.Replace("#V1#", "Palabra Reservada").Replace("#V2#", "Descripción Error").Replace("td", "th").Replace("CeldaOrtografia", "CabeceraOrtografia"));

            _Regla.ListaConcidencias.Sort(Coincidencia.LineaComparison);

            foreach (Coincidencia _c in _Regla.ListaConcidencias)
            {
                _filaret = string.Empty;
                string _descripcion = $"Error en <b>{_c.Palabra}</b>, Línea:{_c.Linea} ";
                if (_c.Linea.Equals("0"))
                {
                    _descripcion = _descripcion.Replace("Línea:0", String.Empty);
                }

                _filaret = _FilaHtml.Replace("#V1#", _c.Objeto.ToString()).Replace("#V2#", _descripcion);
                _htmlStruct.AppendLine(_filaret);
            }

            _htmlStruct.AppendLine("</table></div>");

            _Regla._OutPutHTML = _htmlStruct.ToString();


        }
        public static void ValOrtografia(GXValRegla _Regla)
        {
            if (_Regla.ListaConcidencias != null && _Regla.ListaConcidencias.Count == 0)
            {
                _Regla._OutPutHTML = string.Empty;
                return;
            }

            StringBuilder _htmlStruct = new StringBuilder();

            string _titulo = @"<br /> <div>
                    <span class='TableVariablesTitulo'>#TITLE#</span><br>
                    <span class='TableVariablesSubTitulo'>Se encontraron un total de #NRO# palabras que no están en el diccionario:</span>
                    <br>
                    <table width='691' cellpadding='0' cellspacing='0'  class='TableVariablesNoUsada'>
                    ";
            _titulo = _titulo.Replace("#TITLE#", _Regla.Decripcion).Replace("#NRO#", _Regla.ListaConcidencias.Count.ToString());

            _htmlStruct.AppendLine(_titulo);


           
            string _filaret = String.Empty;
            string _FilaHtml = @"
      <tr class=''>
        <td class='CeldaOrtografia' style='width:30px;' nowrap>#V1#</td>
        <td class='CeldaOrtografia' ColSpan='4' style='width:300px;' nowrap >#V2#</td>
      </tr>";

            _htmlStruct.AppendLine(_FilaHtml.Replace("#V1#", "Palabra").Replace("#V2#", "Origen / Descripción").Replace("td", "th").Replace("CeldaOrtografia", "CabeceraOrtografia"));

            //_Regla.ListaConcidencias.Sort(Coincidencia.LineaComparison);

            //var sortedList = _Regla.ListaConcidencias.OrderBy(x => x.Objeto).ToList();


            foreach (Coincidencia _c in _Regla.ListaConcidencias)
            {
                _filaret = string.Empty;
                string _variable = _c.Palabra;
                string _descripcion = $"Error en <b>{_c.Objeto}</b>, Línea:{_c.Linea} ";
                if (_c.Linea.Equals("0") || _c.Objeto.Equals("Diseño"))
                {
                    //_descripcion = _descripcion.Replace("Línea:0", String.Empty);
                    _descripcion = $"Error en <b>{_c.Objeto}</b>";
                }
                _filaret = _FilaHtml.Replace("#V1#", _variable).Replace("#V2#", _descripcion);
                _htmlStruct.AppendLine(_filaret);
            }
            
            _htmlStruct.AppendLine("</table></div>");

            _Regla._OutPutHTML = _htmlStruct.ToString();

        }
    }
}
