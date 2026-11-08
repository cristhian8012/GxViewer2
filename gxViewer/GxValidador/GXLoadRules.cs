using FastColoredTextBoxNS;
using gxViewer.GxObject;
using NetSpell.SpellChecker;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace gxViewer.GxValidador
{
    public static class GXLoadRules
    {
        private static GXObj _Objeto;
        private static string _totalCode = String.Empty;
        private static string _varsInLayout = String.Empty;

        public static void LoadVars(GXObj _Obj)
        {
            _Objeto = _Obj;
            _totalCode = _Objeto.GetTotalSource();
            _varsInLayout = _Obj.getLayoutVars();
        }


        public static void ValHardCode(GXValRegla _Regla)
        {

        }

        public static void ValWarning(GXValRegla _Regla)
        {

        }

        public static void ValVarNoUse(GXValRegla _Regla)
        {
            List<Variable> _lstVarUsada = new List<Variable>();
            List<Variable> _lstVarNOUsada = new List<Variable>();

            foreach (Variable _var in _Objeto.Variables)
            {
                if (_totalCode.Contains(_var.Name2) || _varsInLayout.Contains(_var.Name2))
                {
                    _lstVarUsada.Add(_var);
                }
                else
                {
                    switch (_var.Name2)
                    {
                        case "&today":
                        case "&pgmname":
                        case "&pgmdesc":
                        case "&time":
                        case "&page":
                        case "&line":
                        case "&output":
                            _lstVarUsada.Add(_var);
                            break;

                        default:
                            _lstVarNOUsada.Add(_var);
                            break;
                    }
                }
            }




            foreach (Variable _v in _lstVarUsada)
            {
                bool bComment = false;
                bool bNoComment = false;
                using (StringReader _strRead = new StringReader(_totalCode))
                {
                    while (true)
                    {
                        string _linea = _strRead.ReadLine();
                        if (_linea != null)
                        {
                            if (_linea.Contains(_v.Name2))
                            {
                                if (_linea.StartsWith("//"))
                                {
                                    bComment = true;
                                }
                                else
                                {
                                    bNoComment = true;
                                }
                            }
                            if (bComment && bNoComment)
                            {
                                break;
                            }

                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (bComment && !bNoComment)
                {
                    _lstVarNOUsada.Add(_v);
                }

            }


            //Ahora cargamos todaslas variables no utilizdas
            List<Coincidencia> _coincidencias = new List<Coincidencia>();
            foreach (Variable _v in _lstVarNOUsada)
            {
                Coincidencia _c = new Coincidencia();
                _c.Flag = "DataRowInutilizada";
                _c.RutaImg = "\\Flag_None.png";
                _c.NombreObj = GXLoadRules._Objeto.Name;
                _c.Linea = String.Format("&{0}", _v.Name);
                _c.Objeto = _v;
                _coincidencias.Add(_c);
            }
            _Regla.ListaConcidencias = _coincidencias;

        }


        public static void ValVarNoUse2(GXValRegla _Regla)
        {

        }
        public static void ValDefinedBy(GXValRegla _Regla)
        {
            
            List<Coincidencia> _coincidencias = new List<Coincidencia>();

            IList<string> _sourceLines = _Objeto.getLinesCode();

            for (int i = 0; i < _sourceLines.Count<string>(); i++)
            {
                string source = _sourceLines[i];

                if(String.IsNullOrEmpty(source) || source.StartsWith("---") || source.StartsWith("/*") || source.StartsWith("//") || source.StartsWith("===") || source.StartsWith("***"))
                {
                    continue;
                }

                string _txtline = source.Trim().ToUpper();
                bool _existeDefinedBy = false;
                if (_txtline.StartsWith("FOR EACH") && !_txtline.Contains("LINE"))
                {
                    //Empezamos a Buscar El defined By, en lineas mas abajo
                    for (int j = i + 1; j < _sourceLines.Count<string>(); j++)
                    {
                        string _txtnextline = _sourceLines[j].Trim().ToUpper();
                        if (_txtnextline.StartsWith("DEFINED BY"))
                        {
                            _existeDefinedBy = true;
                            break;
                        }

                        if (_txtnextline.StartsWith("FOR EACH") || _txtnextline.StartsWith("ENDFOR") || (_txtnextline.StartsWith("XFOR EACH") || _txtnextline.StartsWith("XENDFOR") || _txtnextline.StartsWith("WHEN NONE")))
                        {
                            break;
                        }
                    }

                    if (!_existeDefinedBy)
                    {
                        Coincidencia _c = new Coincidencia();
                        _c.Flag = "DefinedByError";
                        _c.NombreObj = GXLoadRules._Objeto.Name;
                        _c.Palabra = "pBlockCod";
                        _c.Linea = (i+1).ToString();
                        _c.Objeto = "Codigo Fuente";
                        _coincidencias.Add(_c);
                    }
                }
            }

            _Regla.ListaConcidencias = _coincidencias;

        }

        public static void ValErrorWhere(GXValRegla _Regla)
        {

        }

        public static void ValDocument(GXValRegla _Regla)
        {
            IEnumerable<string> _linesDocum = Functions.GetLinesFromString(_Objeto.getDocumentationText(), true);
            if ( _linesDocum.Count<string>() <= 0 )
            {
                List<Coincidencia> _ErrorSinDoc = new List<Coincidencia>();
                Coincidencia _sd = new Coincidencia();
                _sd.Flag = "DocumError";
                _sd.NombreObj = GXLoadRules._Objeto.Name;
                _sd.Palabra = $"No existe la documentación del objeto: " + _Objeto.GetName();
                _sd.Linea = "0";
                _sd.Objeto = "Documentación";
                _ErrorSinDoc.Add(_sd);

                _Regla.ListaConcidencias = _ErrorSinDoc;

                return;
            }

            Groups _listWordsDocument = Functions.getDocumentationWords();
            _listWordsDocument.resetValidGroups();

            foreach (string line in _linesDocum)
            {
                if (!String.IsNullOrEmpty(line))
                {
                    string _txt = line.Trim().ToUpper();
                    foreach (var g in _listWordsDocument.Group)
                    {

                        if (g.IsValid)
                        {
                            continue;
                        }

                        bool _ValidBloque = false;
                        
                        if (g.Inline)
                        {
                            int _countValid = 0;
                            foreach (var item in g.Words.Word)
                            {
                                if (_txt.Contains(item))
                                {
                                    _countValid++;
                                }
                            }
                            if (_countValid == g.Words.Word.Count)
                            {
                                _ValidBloque = true;
                            }
                            else
                            {
                                if (_countValid > 0)
                                {
                                    g.TextContent = line;
                                }
                            }
                        }
                        else
                        {

                            foreach (var item in g.Words.Word)
                            {
                                if (_txt.Contains(item))
                                {
                                    _ValidBloque = true;
                                    break;
                                }
                            }
                        }

                        g.IsValid = _ValidBloque;

                        if (g.IsValid)
                        {
                            g.TextContent = line;
                            break;
                        }
                    }
                }
            }


            //ahora cargamos las concidencias
            List<Coincidencia> _coincidencias = new List<Coincidencia>();
            foreach (var g in _listWordsDocument.Group)
            {
                if (g.Required )
                {
                    if (!g.IsValid)
                    {
                        Coincidencia _c = new Coincidencia();
                        _c.Flag = "DocumError";
                        _c.NombreObj = GXLoadRules._Objeto.Name;
                        _c.Palabra = $"No contiene la etiqueta: {g.Words.Word.FirstOrDefault()}";
                        if (g.Inline)
                        {
                            StringBuilder _strb = new StringBuilder();
                            foreach (var w in g.Words.Word)
                            {
                                if (!g.TextContent.ToUpper().Contains(w))
                                {
                                    _strb.AppendLine(w + ",");
                                }
                            }
                            _c.Palabra = $"No contiene las etiquetas: {_strb.ToString()}";
                        }
                        _c.Linea = "0";
                        _c.Objeto = g.Name;
                        _coincidencias.Add(_c);
                    }
                    
                    if (g.Name.Equals("NombrePrograma"))
                    {
                        bool _nameProgramOK = ( g.TextContent.Trim().ToUpper().Contains(_Objeto.GetName()) );

                        if (!_nameProgramOK) 
                        {
                            Coincidencia _c2 = new Coincidencia();
                            _c2.Flag = "DocumError";
                            _c2.NombreObj = GXLoadRules._Objeto.Name;
                            _c2.Palabra = "Nombre de programa no es correcto, debe ser : " + _Objeto.GetName() + "";
                            _c2.Linea = "0";
                            _c2.Objeto = g.Name;
                            _coincidencias.Add(_c2);
                        }
                    }

                    if (!String.IsNullOrEmpty(g.Pattern))
                    {

                        if (String.IsNullOrEmpty(g.TextContent))
                        {
                            Coincidencia _c3 = new Coincidencia();
                            _c3.Flag = "DocumError";
                            _c3.NombreObj = GXLoadRules._Objeto.Name;
                            _c3.Palabra = $"No existe etiqueta";
                            _c3.Linea = "0";
                            _c3.Objeto = g.Name;
                            _coincidencias.Add(_c3);
                        }
                        else
                        {
                            char[] _ch = new char[] { ':', '=' };
                            string[] _content = g.TextContent.Trim().Split(_ch, StringSplitOptions.RemoveEmptyEntries);
                            string _text = _content[_content.Length - 1];
                            _ch = new char[] { '-', '|', '@', '_' };

                            _content = _text.Split(_ch, StringSplitOptions.RemoveEmptyEntries);

                            string[] _strpatt = g.Pattern.Trim().Split(_ch, StringSplitOptions.RemoveEmptyEntries);
                            if (_content.Length != _strpatt.Length)
                            {
                                Coincidencia _c3 = new Coincidencia();
                                _c3.Flag = "DocumError";
                                _c3.NombreObj = GXLoadRules._Objeto.Name;
                                _c3.Palabra = $"No tiene formato correcto, debe ser asi: ({g.Pattern}) y dice : {_text}";
                                _c3.Linea = "0";
                                _c3.Objeto = g.Name;
                                _coincidencias.Add(_c3);
                            }
                            else //Si esta ok  el formato, buscamos lso tamaños
                            {
                                if (!String.IsNullOrEmpty(g.Pattern_Min))
                                {
                                    string[] _strLenMin = g.Pattern_Min.Trim().Split(_ch, StringSplitOptions.RemoveEmptyEntries);
                                    if (_strLenMin.Length == _content.Length)
                                    {
                                        for (int i = 0; i < _content.Length; i++)
                                        {
                                            int _minLengh = int.Parse(_strLenMin[i]);
                                            if (_minLengh > _content[i].Trim().Length)
                                            {
                                                Coincidencia _c3 = new Coincidencia();
                                                _c3.Flag = "DocumError";
                                                _c3.NombreObj = GXLoadRules._Objeto.Name;
                                                _c3.Palabra = $"{_strpatt[i]} - No tiene tamaño minimo({_strLenMin[i]} carateres) : {_content[i].Trim()}";
                                                _c3.Linea = "0";
                                                _c3.Objeto = g.Name;
                                                _coincidencias.Add(_c3);
                                            }
                                        }
                                    }



                                }
                                
                            }
                        }
                    }
                }

            }
            _Regla.ListaConcidencias = _coincidencias;

        }

        public static void ValErrorCALL(GXValRegla _Regla)
        {
            List<Coincidencia> _coincidencias = new List<Coincidencia>();

            IList<string> _sourceLines = _Objeto.getLinesCode();

            for (int i = 0; i < _sourceLines.Count; i++)
            {
                string input = _sourceLines[i].ToUpper().Trim();
                if (input.StartsWith("CALL"))
                {
                    if (input.EndsWith(@"\"))
                    {
                        for (int j = i + 1; j < _sourceLines.Count; j++)
                        {
                            input = input + _sourceLines[j];
                            if (!input.EndsWith(@"\"))
                            {
                                break;
                            }
                        }
                    }
                    if ((!input.Contains("'MOVSTRN'") && !input.Contains("'MOVSTRL'")) && !input.Contains("'SUBSTRL'"))
                    {
                        if (Regex.IsMatch(input, @"[,\\]\s*'"))
                        {
                            Coincidencia _c = new Coincidencia();
                            _c.Flag = "P0005";
                            _c.NombreObj = GXLoadRules._Objeto.Name;
                            _c.Palabra = input;
                            _c.Linea = (i + 1).ToString();
                            _c.Objeto = "Codigo Fuente";
                            _coincidencias.Add(_c);
                        }
                        else if (Regex.IsMatch(input, @"[,\\]\s*[0-9]"))
                        {
                            Coincidencia _c = new Coincidencia();
                            _c.Flag = "P0005";
                            _c.NombreObj = GXLoadRules._Objeto.Name;
                            _c.Palabra = input;
                            _c.Linea = (i + 1).ToString();
                            _c.Objeto = "Codigo Fuente";
                            _coincidencias.Add(_c);
                        }
                    }
                }
            }

            _Regla.ListaConcidencias = _coincidencias;

        }

        public static void ValWordRest(GXValRegla _Regla)
        {

            Groups _listreservedwords = Functions.getReservedWords();
            List<string> _allreserved = _listreservedwords.getAllWords();
            List<Coincidencia> _coincidencias = new List<Coincidencia>();

            _coincidencias.AddRange( _getReservedFromCode(_Objeto.getLinesCode(), "Codigo Fuente", _allreserved));
            _coincidencias.AddRange(_getReservedFromCode(_Objeto.getLinesRule(), "Reglas", _allreserved));
            _coincidencias.AddRange(_getReservedFromCode(_Objeto.getLinesCondition(), "Condiciones", _allreserved));

            _Regla.ListaConcidencias = _coincidencias;
        }

        public static void ValOrtografia(GXValRegla _Regla)
        {

            Spelling oSpell = Functions.getSpellChecker();

            List<Coincidencia> _coincidencias = new List<Coincidencia>();

            _coincidencias.AddRange(_getErrorFromTexts(0, _Objeto.Description, "Título Programa", ref oSpell));

            _coincidencias.AddRange(_getErrorFromTexts(_Objeto.getLayoutText(), "Diseño", ref oSpell));
            
            _coincidencias.AddRange(_getErrorTextFromCode(_Objeto.getLinesCode(), "Codigo Fuente", ref oSpell));

            _coincidencias.AddRange(_getErrorTextFromCode(_Objeto.getLinesRule(), "Reglas", ref oSpell));

            _coincidencias.AddRange(_getErrorFromTexts(_Objeto.getDocumentationText(), "Documentación", ref oSpell));

            _Regla.ListaConcidencias = _coincidencias;
        }


        private static List<Coincidencia> _getErrorTextFromCode(IList<string> pLinesCode, string pBlockCode, ref Spelling oSpell)
        {
            List<Coincidencia> _retcoincidencias = new List<Coincidencia>();
            IEnumerable<string> _sourceLines = pLinesCode; // Functions.GetLinesFromString(pCode, false);
            int _linea = 0;
            foreach (string str in _sourceLines)
            {
                _linea++;
                string source = str.Trim(); 
                if (String.IsNullOrEmpty(source) || source.StartsWith("---") || source.StartsWith("/*") || source.StartsWith("//") || source.StartsWith("===") || source.StartsWith("***"))
                {
                    continue;
                }

                //if (source.ToLower().Contains("movstr"))
                //{
                //    Console.Write(source);
                //}

                string _reserveWord = source.Trim().ToUpper();
                if (!_reserveWord.StartsWith("DO ") && !_reserveWord.StartsWith("DO'") && !_reserveWord.StartsWith("XFOR ") && !_reserveWord.StartsWith("COLOR(") && !_reserveWord.StartsWith("SUB ") && !_reserveWord.StartsWith("CALL") && !_reserveWord.StartsWith("PARM") && !_reserveWord.StartsWith("XFILE") && !_reserveWord.Contains("UDP('") && !_reserveWord.Contains("UDP ('")) //Evitar las llamadas a subrutinas
                {
                    MatchCollection matches = Regex.Matches(source, "\\\'(.*?)\\\'", RegexOptions.Compiled);
                    if (matches.Count > 0)
                    {
                        foreach (Match m in matches)
                        {
                            string _txtQoute = m.ToString().Replace("'", "").Trim();
                            if (!String.IsNullOrEmpty(_txtQoute))
                            {
                                long _number;
                                if (!long.TryParse(_txtQoute, out _number))
                                {
                                    _retcoincidencias.AddRange(_getErrorFromTexts(_linea, m.ToString().Replace("'", ""), pBlockCode, ref oSpell));
                                }
                            }
                        }
                    }
                }
            }
            return _retcoincidencias;
        }

        private static List<Coincidencia> _getErrorFromTexts(string pText, string pBlockCode, ref Spelling oSpell)
        {
            List<Coincidencia> _ret = new List<Coincidencia>();
            IEnumerable<string> _linesDocum = Functions.GetLinesFromString(pText, true);
            char[] chDelims = { ' ', '\n', '\t', '\r', ':', '=', '-', '.' };
            int _nrolinea = 0;
            foreach (var str in _linesDocum)
            {
                string line = str.Trim();
                _nrolinea++;
                if (String.IsNullOrEmpty(line) || line.StartsWith("---") || line.StartsWith("/*") || line.StartsWith("//") || line.StartsWith("===") || line.StartsWith("***") || line.StartsWith("*---"))
                {
                    continue;
                }

                //Invokar a SpellChecker
                foreach (string s in line.Split(chDelims))
                {
                    if (String.IsNullOrEmpty(s) || s.StartsWith("---") || s.StartsWith("/*") || s.StartsWith("//") || line.StartsWith("===") || s.StartsWith("***") || s.Equals("-") || s.Equals(".") || s.ToLower().StartsWith("call(") || s.Contains(_Objeto.Name))
                    {
                        continue;
                    }
                    else
                    {
                        if (s.Length < 3) { continue; }

                        if (s.Length == 3)
                        {
                            if (s.Contains("-") || s.Contains("*") || s.Contains("\\") || s.Contains("/") || s.Contains("_"))
                            {
                                continue;
                            }

                        }

                        long _l;
                        if (long.TryParse(s, out _l)) { continue; }

                        DateTime _d;
                        if (DateTime.TryParse(s, out _d)) { continue; }


                    }

                    if (s.Length > 0)
                    {
                        bool _isOk = oSpell.TestWord(s);
                        //Do something here...
                        if (_isOk)
                        {
                            //Console.WriteLine("OK    => " + s);
                        }
                        else
                        {
                            //Console.WriteLine("ERROR => " + s);

                            Coincidencia _c = new Coincidencia();
                            _c.Flag = "WordError";
                            _c.NombreObj = GXLoadRules._Objeto.Name;
                            _c.Palabra = s;
                            _c.Linea = _nrolinea.ToString();
                            _c.Objeto = pBlockCode;
                            _ret.Add(_c);

                        }
                    }
                }
            }

            return _ret;
        }

        private static List<Coincidencia> _getErrorFromTexts(int pLine, string pText, string pBlockCode, ref Spelling oSpell)
        {
            List<Coincidencia> _ret = new List<Coincidencia>();
            //IEnumerable<string> _linesDocum =  new   //Functions.GetLinesFromString(pText, true);
            char[] chDelims = { ' ', '\n', '\t', '\r', ':', '=', '-', '.', ',' , ';', '(', ')' };
            int _nrolinea = pLine;
            string line = pText;
            //foreach (var line in _linesDocum)
            {
                if (String.IsNullOrEmpty(line) || line.StartsWith("---") || line.StartsWith("/*") || line.StartsWith("//") || line.StartsWith("===") || line.StartsWith("***") || line.StartsWith("*---"))
                {
                    goto xfinal;
                }

                //Invokar a SpellChecker
                foreach (string x in line.Split(chDelims))
                {
                    string s = x.Trim();
                    if (String.IsNullOrEmpty(s) || s.StartsWith("---") || s.StartsWith("/*") || s.StartsWith("//") || line.StartsWith("===") || s.StartsWith("***") || s.Equals("-") || s.Equals(".") || s.Equals(";") || s.ToLower().StartsWith("call(") || s.Contains(_Objeto.Name))
                    {
                        continue;
                    }
                    else
                    {
                        if (s.Length < 3) { continue; }

                        if (s.Length == 3)
                        {
                            if (s.Contains("-") || s.Contains("*") || s.Contains("\\") || s.Contains("/") || s.Contains("_"))
                            {
                                continue;
                            }

                        }

                        long _l;
                        if (long.TryParse(s, out _l)) { continue; }

                        DateTime _d;
                        if (DateTime.TryParse(s, out _d)) { continue; }


                    }

                    if (s.Length > 0)
                    {
                        bool _isOk = oSpell.TestWord(s);
                        //Do something here...
                        if (_isOk)
                        {
                            //Console.WriteLine("OK    => " + s);
                        }
                        else
                        {
                            //Console.WriteLine("ERROR => " + s);

                            Coincidencia _c = new Coincidencia();
                            _c.Flag = "WordError";
                            _c.NombreObj = GXLoadRules._Objeto.Name;
                            _c.Palabra = s;
                            _c.Linea = _nrolinea.ToString();
                            _c.Objeto = pBlockCode;
                            _ret.Add(_c);

                        }
                    }
                }
            }
            
            xfinal: 


            return _ret;
        }



        private static List<Coincidencia> _getReservedFromCode(IList<string> pLinesCode, string pBlockCod, List<string> _reserved)
        {
            List<Coincidencia> _retcoincidencias = new List<Coincidencia>();
            if (pLinesCode == null)
            {
                return _retcoincidencias;
            }


            IEnumerable<string> _sourceLines = pLinesCode; 
            int _linea = 0;
            foreach (string source in _sourceLines)
            {
                _linea++;
                if (String.IsNullOrEmpty(source) || source.StartsWith("---") || source.StartsWith("/*") || source.StartsWith("//") || source.StartsWith("===") || source.StartsWith("***"))
                {
                    continue;
                }
                string _reserveWord = source.Trim().ToUpper();
                if (!_reserveWord.StartsWith("DO ") && !_reserveWord.StartsWith("XFOR ") && !_reserveWord.StartsWith("SUB ") && !_reserveWord.StartsWith("CALL") && !_reserveWord.StartsWith("PARM") && !_reserveWord.StartsWith("XFILE")) 
                {
                    MatchCollection matches = Regex.Matches(source, "\\\'(.*?)\\\'", RegexOptions.Compiled);
                    if (matches.Count > 0)
                    {
                        foreach (Match m in matches)
                        {
                            string _txtQoute = m.ToString().Replace("'", "").Trim();
                            if (!String.IsNullOrEmpty(_txtQoute))
                            {
                                if (_reserved.Contains(_txtQoute))
                                {
                                    Coincidencia _c = new Coincidencia();
                                    _c.Flag = "ReservedError";
                                    _c.NombreObj = GXLoadRules._Objeto.Name;
                                    _c.Palabra = pBlockCod;
                                    _c.Linea = _linea.ToString();
                                    _c.Objeto = _txtQoute;
                                    
                                    _retcoincidencias.Add(_c);  

                                }


                            }
                        }
                    }
                }
            }
            return _retcoincidencias;
        }


    }
}
