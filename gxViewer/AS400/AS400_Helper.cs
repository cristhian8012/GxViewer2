using gxViewer.GxObject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace gxViewer.AS400
{
    public static class AS400_Helper
    {

        public static As400Object Get400Object(FileInfo _ftxt)
        {
            As400Object _ret = new As400Object();
            _ret.Type = AS400_Types.Other;

            _ret.Filename = _ftxt.FullName;

            string _tipo = _ftxt.Extension.Replace(".", String.Empty);
            string _name = _ftxt.Name.Replace(_ftxt.Extension, String.Empty);

            //Encoding _enc = GetEncoding(_ftxt.FullName);
            _ret.Name = _name.Trim().ToUpper();
            _ret.Ext = _tipo.ToLower();

            _ret.Source = Functions.DetectAndReadFile(_ftxt.FullName); //  File.ReadAllText(_ftxt.FullName, _enc);

            switch (_tipo.ToLower())
            {
                case "cbl": 
                    _ret.Type = AS400_Types.COBOL; 
                    break;
                case "rpg": 
                    _ret.Type = AS400_Types.RPG;  
                    break;
                case "clp":
                case "clle":
                case "cl":
                    _ret.Ext = "clp";
                    _ret.Type = AS400_Types.CL;
                    _ret.Variables = getVariablesCL(_ret.Source);
                    break;

                case "jcl":
                    _ret.Type = AS400_Types.JCL; break;

                case "sql":
                    _ret.Type = AS400_Types.SQL; break;

                case "pf":
                    _ret.Type = AS400_Types.PF; break;

                case "lf":
                    _ret.Type = AS400_Types.LF; break;
            }

            

            return _ret;
        }

        public static List<Variable> getVariablesCL(string _source)
        {
            List<Variable> _ret = new List<Variable>();

            IEnumerable<string> _lines = Functions.GetLinesFromString(_source, true);

            if (_lines.Count<string>() > 0)
            {

                foreach (string line in _lines)
                {
                    if (!String.IsNullOrEmpty(line))
                    {
                        if (line.Trim().StartsWith("DCL ") && (!line.Trim().StartsWith("\\") && !line.Trim().StartsWith("/*") && !line.Trim().StartsWith("*")))
                        {
                            Variable _var = new Variable();
                            //DCL VAR(&VAW3APL)  TYPE(*CHAR) LEN(02) VALUE('CC')
                            string[] _parts = line.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var item in _parts)
                            {
                                if ( item.Contains("VAR") )
                                {
                                    _var.Name = Functions.GetSubstrByString(item, "(", ")").Replace("&", "");
                                    continue;
                                }

                                if (item.Contains("TYPE"))
                                {
                                    _var.Type = Functions.GetSubstrByString(item, "(", ")");
                                    continue;
                                }

                                if (item.Contains("LEN"))
                                {
                                   //string _tmpItem = Functions.GetSubstrByString(line, "LEN(", ")").Trim();

                                    int _lengh;
                                    int _dec;
                                    string _valLength = Functions.GetSubstrByString(line, "LEN(", ")").Trim(); //Functions.GetSubstrByString(item, "(", ")").Trim();
                                    string[] _vals = _valLength.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);   

                                    if (int.TryParse(_vals[0], out _lengh))
                                    {
                                        _var.Length = _lengh;
                                    }

                                    if (_vals.Length > 1)
                                    {
                                        if (int.TryParse(_vals[1], out _dec))
                                        {
                                            _var.Decimals = _dec;
                                        }
                                    }

                                    continue;
                                }

                                if (item.Contains("VALUE"))
                                {
                                    _var.DefaultValue = Functions.GetSubstrByString(item, "(", ")").Trim();
                                    continue;
                                }

                                if (item.Contains("BASPTR"))
                                {
                                    _var.BasedOn = Functions.GetSubstrByString(item, "(", ")").Trim();
                                    continue;
                                }

                            }

                            _ret.Add(_var);

                        }
                    }
                }

            }

            return _ret;

        }

        public static string getDocumentationCL(string _source)
        {
            StringBuilder _ret = new StringBuilder();
            IEnumerable<string> _lines = Functions.GetLinesFromString(_source, true);

            if (_lines.Count<string>() > 0)
            {

                foreach (string line in _lines)
                {
                    if (!String.IsNullOrEmpty(line))
                    {
                        if (line.Trim().StartsWith("DCL ") && (!line.Trim().StartsWith("\\") && !line.Trim().StartsWith("/*") && !line.Trim().StartsWith("*")))
                        {
                            break;
                        }
                        else
                        {
                            if (!line.Trim().StartsWith("PGM"))
                            {
                                _ret.AppendLine(line.Replace("/*", "").Replace("*/", "").Replace("*-", "").Replace("-*", "").Trim());
                            }
                                
                        }
                    }
                }

            }
            return _ret.ToString();
        }

        public static string getOnlyCodeCL(string _source)
        {
            StringBuilder _ret = new StringBuilder();
            IEnumerable<string> _lines = Functions.GetLinesFromString(_source, true);

            if (_lines.Count<string>() > 0)
            {

                foreach (string line in _lines)
                {
                    if (!String.IsNullOrEmpty(line))
                    {
                        if (!line.Trim().StartsWith("DCL ") && (!line.Trim().StartsWith("\\") && !line.Trim().StartsWith("/*") && !line.Trim().StartsWith("*")))
                        {
                            _ret.AppendLine(line.Replace("/*", "").Replace("*/", "").Trim());   
                        }
                    }
                }

            }

            return _ret.ToString();

        }

        public static string getDocumentationPF(string _source)
        {
            StringBuilder _ret = new StringBuilder();
            IEnumerable<string> _lines = Functions.GetLinesFromString(_source, true);

            if (_lines.Count<string>() > 0)
            {

                foreach (string line in _lines)
                {
                    if (!String.IsNullOrEmpty(line))
                    {
                        if (line.Trim().StartsWith("A  ") && (!line.Trim().StartsWith("\\") && !line.Trim().StartsWith("/*") && !line.Trim().StartsWith("*")))
                        {
                            break;
                        }
                        else
                        {
                            if (!line.Trim().StartsWith("A "))
                            {
                                _ret.AppendLine(line.Replace("/*", "").Replace("*/", "").Replace("*-", "").Replace("-*", "").Trim());
                            }

                        }
                    }
                }

            }
            return _ret.ToString();
        }



    }
}
