using gxViewer.GxValidador;
using Microsoft.Win32;
using NetSpell.SpellChecker;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;


namespace gxViewer
{
    public static class Functions
    {
        private static string _pathModelSBP = @"\\SP-GENEXUS\modelos";
        private static Spelling oSpell = null;
        private static Groups _reserve_words = null;
        private static Groups _documentation_words = null;

        public static string GetProductName()
        {
            try
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // Si no hay atributos del producto, devuelva una cadena vacía
                if (attributes.Length == 0)
                    return "";
                // Si hay un atributo de producto, devuelva su valor
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
            catch (Exception)
            {
                return "GXVIEWER";
            }
        }

        public static string GetPathModelsSBP()
        {
            return _pathModelSBP;
        }

        private static string GetKeyRootRegEdit()
        {
            string _retKey = "HKEY_CURRENT_USER\\SOFTWARE\\" + GetProductName();
            return _retKey;
        }

        public static string GetPathRegEdit(string pPath)
        {
            return GetKeyRootRegEdit() + "\\" + pPath;
        }

        public static T DirectCast<T>(object o) { return (T)o; }

        public static string Serialize<T>(this T value, bool pSinNamespaces = false)
        {
            if (value == null)
            {
                return string.Empty;
            }

            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = pSinNamespaces,
            };

            try
            {
                var xmlserializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = System.Xml.XmlWriter.Create(stringWriter, settings))
                {
                    if (pSinNamespaces)
                    {
                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        ns.Add("", "");
                        xmlserializer.Serialize(writer, value, ns);
                    }
                    else
                    {
                        xmlserializer.Serialize(writer, value);
                    }

                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }

        public static T Deserialize<T>(string filenamexml )
        {
            T _objret;
            System.Xml.Serialization.XmlSerializer _ser = new System.Xml.Serialization.XmlSerializer(typeof(T));
            StreamReader _strm = new StreamReader(filenamexml, Encoding.Default);
            _objret = (T)_ser.Deserialize(_strm);

            return _objret;
        }

        public static T Deserialize<T>(string filenamexml, Encoding pEncode)
        {
            T _objret;
            System.Xml.Serialization.XmlSerializer _ser = new System.Xml.Serialization.XmlSerializer(typeof(T));
            StreamReader _strm = new StreamReader(filenamexml, pEncode);
            _objret = (T)_ser.Deserialize(_strm);

            return _objret;
        }

        public static T DeserializeXML<T>(string xmlXchema)
        {

            string _strFileName = String.Format("{0}\\archivotmp.xml", System.AppDomain.CurrentDomain.BaseDirectory);
            FileInfo _finfo = new FileInfo(_strFileName);
            if (_finfo.Exists)
            { File.Delete(_finfo.FullName); }

            File.WriteAllText(_finfo.FullName, xmlXchema, Encoding.Default);

            System.Windows.Forms.Application.DoEvents();

            return Deserialize<T>(_finfo.FullName);
        }

        public static T DeserializeXML_2<T>(string xmlXchema)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xmlXchema))
            {
                return (T)serializer.Deserialize(reader);

            }
        }


        public static string ObjectToString64(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Serialize(ms, obj);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static object String64ToObject(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Deserialize(ms);
            }
        }

        public static string TransformXMLToHTML(string inputXml, string xsltString)
        {
            try
            {
                XslCompiledTransform transform = GetAndCacheTransform(xsltString);
                StringWriter results = new StringWriter();
                using (XmlReader reader = XmlReader.Create(new StringReader(inputXml)))
                {
                    transform.Transform(reader, null, results);
                }
                return results.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return String.Empty;
            }

        }

        private static Dictionary<String, XslCompiledTransform> cachedTransforms = new Dictionary<string, XslCompiledTransform>();
        private static XslCompiledTransform GetAndCacheTransform(String xslt)
        {
            XslCompiledTransform transform;
            var xmlResolver = new XmlUrlResolver();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.XmlResolver = xmlResolver;
            settings.ProhibitDtd = false;

            if (!cachedTransforms.TryGetValue(xslt, out transform))
            {
                transform = new XslCompiledTransform(true);
                using (XmlReader reader = XmlReader.Create(new StringReader(xslt), settings))
                {
                    transform.Load(reader, new XsltSettings { EnableDocumentFunction = true }, xmlResolver);
                }
                cachedTransforms.Add(xslt, transform);
            }
            return transform;
        }

        public static bool getModelPath(string pModel, out string pathModel)
        {
            if (pModel == null)
            {
                pathModel = String.Empty;
                return false;
            }

            if (Directory.Exists(pModel))
            {
                int _nrofiles = Directory.GetFiles(pModel, "*.GXW", SearchOption.TopDirectoryOnly).Length;
                if (_nrofiles > 0)
                {
                    pathModel = pModel;
                    return true;
                }
            }



            string _tmpModelName = pModel;
            if (pModel.Contains("\\"))
            {
                DirectoryInfo _dm = new DirectoryInfo(pModel);
                _tmpModelName = _dm.Name;
            }

            string _pathModel = Path.Combine(GetPathModelsSBP(), _tmpModelName);
            DirectoryInfo _diModel = new DirectoryInfo(_pathModel);
            if (_diModel.Exists)
            {
                pathModel = _diModel.FullName;
                return true;
            }
            else
            {
                pathModel = String.Empty;
                return false;
            }
        }

        public static string getStringPathFile(string _path, string _patternFile)
        {
            string _retFileString = String.Empty;
            string[] _listDirs = null;

            if (_path.EndsWith("*"))
            {
                string _pathfind = String.Empty;
                string _dirpattern = String.Empty;

                string[] _paths = _path.Split("\\".ToCharArray());
                _dirpattern = _paths[_paths.Length - 1];
                _pathfind = _path.Replace(_dirpattern, String.Empty);
                _listDirs = Directory.GetDirectories(_pathfind, _dirpattern);
            }
            else
            {
                _listDirs = new string[] { _path };
            }

            foreach (string _dir in _listDirs)
            {
                DirectoryInfo _di = new DirectoryInfo(_dir);
                foreach (FileInfo _fi in _di.GetFiles(_patternFile, SearchOption.AllDirectories))
                {
                    _retFileString = _fi.FullName;
                    break;
                }
                if (!String.IsNullOrEmpty(_retFileString))
                {
                    break;
                }
            }
            return _retFileString;
        }

        public static string getStringFile(string _path, string _patternFile, ref string _AttributesFile)
        {
            string _retFileString = String.Empty;

            FileInfo _fiVal1 = new FileInfo(Path.Combine(_path, _patternFile));
            if (_fiVal1.Exists)
            {
                _retFileString = File.ReadAllText(_fiVal1.FullName, Encoding.Default);
                if (_AttributesFile.Equals("GET"))
                {
                    _AttributesFile = GetAttributesFile(_fiVal1);
                }
                return _retFileString;
            }

            string[] _listDirs = null;
            if (_path.EndsWith("*"))
            {
                string _pathfind = String.Empty;
                string _dirpattern = String.Empty;

                string[] _paths = _path.Split("\\".ToCharArray());
                _dirpattern = _paths[_paths.Length - 1];
                _pathfind = _path.Replace(_dirpattern, String.Empty);
                _listDirs = Directory.GetDirectories(_pathfind, _dirpattern);
            }
            else
            {
                _listDirs = new string[] { _path };
            }

            foreach (string _dir in _listDirs)
            {
                DirectoryInfo _di = new DirectoryInfo(_dir);
                foreach (FileInfo _fi in _di.GetFiles(_patternFile, SearchOption.AllDirectories))
                {
                    _retFileString = File.ReadAllText(_fi.FullName, Encoding.Default);
                    if (_AttributesFile.Equals("GET"))
                    {
                        _AttributesFile = GetAttributesFile(_fi);
                    }
                    break;
                }
                if (!String.IsNullOrEmpty(_retFileString))
                {
                    break;
                }
            }

            return _retFileString;
        }

        private static string GetAttributesFile(FileInfo _fi)
        {
            string _AttributesFile = String.Empty;
            try
            {
                string _strNameUsr = String.Empty;
                string _username = getUserOwnerFile(_fi);
                if (!String.IsNullOrEmpty(_username))
                {
                    string[] _usrs = _username.Split("\\".ToCharArray());
                    _strNameUsr = GetUserName(_usrs[0], _usrs[1]);
                }
                _AttributesFile = "Fecha=" + _fi.LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss") + "|Usuario=" + _username + " - " + _strNameUsr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return _AttributesFile;
        }

        public static string InputBox(string Texto, string Caption, string DefaultText)
        {
            string ret = String.Empty;
            frmInputBox f = new frmInputBox();
            f.Titulo = Caption;
            f.Texto = Texto;
            f.DefaultText = DefaultText;
            DialogResult _result = f.ShowDialog();
            if (_result == DialogResult.OK) { ret = f.Retorno; f.Dispose(); }
            return ret;

        }

        public static string getUserOwnerFile(FileInfo pfileName)
        {
            string _name = String.Empty;
            try
            {
                if (pfileName != null)
                {
                    FileSecurity fileSecurity = pfileName.GetAccessControl();
                    IdentityReference identityReference = fileSecurity.GetOwner(typeof(SecurityIdentifier));
                    NTAccount ntAccount = identityReference.Translate(typeof(NTAccount)) as NTAccount;
                    _name = ntAccount.Value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _name = "";
            }

            return _name;
        }

        public static string GetUserName(string _domain, string _user)
        {
            try
            {
                if (_domain.Equals("Error")) { return ""; }

                string _nombre = String.Empty;
                if (_domain.ToLower().Equals(Environment.MachineName.ToLower()))
                {
                    var identity = WindowsIdentity.GetCurrent();
                    System.DirectoryServices.DirectoryEntry ADEntry = new System.DirectoryServices.DirectoryEntry("WinNT://" + _domain + "/" + _user);
                    if (ADEntry.Properties.Contains("FullName"))
                    {
                        _nombre = ADEntry.Properties["FullName"].Value.ToString();
                    }

                }
                else
                {
                    DirectoryEntry entry = new DirectoryEntry("LDAP://" + _domain);
                    DirectorySearcher Dsearch = new DirectorySearcher(entry);
                    Dsearch.Filter = "(&(OU=Usuarios))";

                    foreach (SearchResult sResultSet in Dsearch.FindAll())
                    {
                        if (sResultSet.Properties.Contains("adspath"))
                        {
                            string _ldap2 = sResultSet.Properties["adspath"][0].ToString();

                            DirectoryEntry entry2 = new DirectoryEntry(_ldap2);
                            DirectorySearcher Dsearch2 = new DirectorySearcher(entry2);
                            Dsearch2.Filter = "(&(samaccountname=" + _user + "))"; //"(&(OU=Usuarios))"; //"(&(objectClass=user)(objectCategory=person))"; //  //"(&(objectClass=user)(l=" + _user + "))";



                            foreach (SearchResult sResultSet2 in Dsearch2.FindAll())
                            {

                                if (sResultSet2.Properties.Contains("displayname"))
                                {
                                    _nombre = sResultSet2.Properties["displayname"][0].ToString();
                                }

                                if (String.IsNullOrEmpty(_nombre))
                                {
                                    if (sResultSet2.Properties.Contains("name"))
                                    {
                                        _nombre = sResultSet2.Properties["name"][0].ToString();
                                    }
                                }
                                break;
                            }

                            if (!String.IsNullOrEmpty(_nombre))
                            {
                                break;
                            }
                        }
                    }
                }

                return _nombre;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }

        }


        #region Converting ObjectArray to Datatable
        public static DataTable ConvertToDataTable(Object[] array)
        {
            DataTable dt = new DataTable();
            if (array != null)
            {
                PropertyInfo[] properties = array.GetType().GetElementType().GetProperties();
                dt = CreateDataTable(properties);
                if (array.Length != 0)
                {
                    foreach (object o in array)
                    {
                        FillData(properties, dt, o);
                    }
                }
            }
            return dt;
        }
        private static DataTable CreateDataTable(PropertyInfo[] properties)
        {
            DataTable dt = new DataTable();
            DataColumn dc = null;
            foreach (PropertyInfo pi in properties)
            {
                dc = new DataColumn();
                dc.ColumnName = pi.Name;
                dc.DataType = pi.PropertyType;
                dt.Columns.Add(dc);
            }
            return dt;
        }
        private static void FillData(PropertyInfo[] properties, DataTable dt, Object o)
        {
            DataRow dr = dt.NewRow();
            foreach (PropertyInfo pi in properties)
            {
                dr[pi.Name] = pi.GetValue(o, null);
            }
            dt.Rows.Add(dr);
        }
        #endregion

        public static string GetValueRegEdit(string pKeyPath, string pKeyName)
        {
            object _ret = Registry.GetValue(pKeyPath, pKeyName, String.Empty);
            if (_ret != null)
            {
                return _ret.ToString();
            }
            else
            {
                return "";
            }
            //return _ret;
        }

        public static void SetValueRegEdit(string pKeyPath, string pKeyName, string pValue)
        {
            Registry.SetValue(pKeyPath, pKeyName, pValue);
        }


        #region String Functions

        public static string GetSubstrByString(string txt, string init, string end)
        {
            int _inipos = 0;
            int _star = txt.IndexOf(init, _inipos);
            int _end = txt.IndexOf(end, (_star + 1));
            string _ret = "";
            if (_star > -1 && _end > -1)
            {
                _ret = txt.Substring((_star + init.Length), (_end - _star - init.Length));
            }
            return _ret;
        }

        public static List<string> GetSubstringByString(string txt, string init, string end)
        {
            List<string> _lstRet = new List<string>();
            int _inipos = 0;
            while (_inipos > -1)
            {
                int _star = txt.IndexOf(init, _inipos);
                int _end = txt.IndexOf(end, (_star + 1));

                if (_star > -1 && _end > -1)
                {
                    _lstRet.Add(txt.Substring((_star + init.Length), (_end - _star - init.Length)));
                    //return txt.Substring((_star + init.Length), (_end - _star - init.Length));
                    _inipos = _end;
                }
                else
                {
                    _inipos = -1;
                    //return String.Empty;
                }
            }

            return _lstRet;
        }

        public static List<XPWConverter.xPosition> GetPositionsByString(string txt, string init, string end, bool withBrackets = false)
        {
            List<XPWConverter.xPosition> _lstRet = new List<XPWConverter.xPosition>();

            int _inipos = 0;
            while (_inipos > -1)
            {
                int _star = txt.IndexOf(init, _inipos);
                int _end = txt.IndexOf(end, (_star + 1));

                if (_star > -1 && _end > -1)
                {
                    XPWConverter.xPosition _pos = new XPWConverter.xPosition();
                    _pos.initOpenBracket = _star;
                    _pos.endOpenBracket = _star + init.Length;
                    _pos.initCloseBracket = _end;
                    _pos.endCloseBracket = _end + end.Length;
                    _pos.LengthTxt = _end - _pos.endOpenBracket;
                    string _txt = txt.Substring(_pos.endOpenBracket, _pos.LengthTxt);
                    if (withBrackets)
                    {
                        _pos.ContentText = String.Format("{0}{1}{2}", init, _txt, end);
                    }
                    else
                    {
                        _pos.ContentText = _txt;
                    }
                    _lstRet.Add(_pos);

                    _inipos = _end;
                }
                else
                {
                    _inipos = -1;
                }
            }

            return _lstRet;
        }


        #endregion String Functions

        public static string FromEscapedString(string s)
        {
            FastStringBuilder fastStringBuilder = new FastStringBuilder(s.Length);
            int num = 0;
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c == '\\' && i + 1 < s.Length)
                {
                    switch (s[i + 1])
                    {
                        case '\\':
                            fastStringBuilder.Append(s, num, i - num + 1);
                            i++;
                            num = i + 1;
                            break;
                        case 'n':
                            fastStringBuilder.Append(s, num, i - num);
                            fastStringBuilder.Append('\n');
                            i++;
                            num = i + 1;
                            break;
                        case 'r':
                            fastStringBuilder.Append(s, num, i - num);
                            fastStringBuilder.Append('\r');
                            i++;
                            num = i + 1;
                            break;
                        case 't':
                            fastStringBuilder.Append(s, num, i - num);
                            fastStringBuilder.Append('\r');
                            i++;
                            num = i + 1;
                            break;
                    }
                }
            }

            if (num < s.Length)
            {
                fastStringBuilder.Append(s, num, s.Length - num);
            }

            return fastStringBuilder.ToString();
        }


        public static string BeautifyXML(string pXML)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(pXML);

            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace,
                OmitXmlDeclaration = true
            };
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }


        public static string GetDownloadFolderPath()
        {
            if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
            {
                string pathDownload = System.IO.Path.Combine(GetHomePath(), "Downloads");
                return pathDownload;
            }

            return System.Convert.ToString(
                Microsoft.Win32.Registry.GetValue(
                     @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders"
                    , "{374DE290-123F-4565-9164-39C4925E467B}"
                    , String.Empty
                )
            );
        }

        public static string GetHomePath()
        {
            // Not in .NET 2.0
            // System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
                return System.Environment.GetEnvironmentVariable("HOME");

            return System.Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
        }

        public static IEnumerable<string> GetLinesFromString(this string str, bool removeEmptyLines = false)
        {
            using (var sr = new StringReader(str))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (removeEmptyLines && String.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    yield return line;
                }
            }
        }


        public static Spelling getSpellChecker()
        {
            if (oSpell == null)
            {
                NetSpell.SpellChecker.Dictionary.WordDictionary oDict = new NetSpell.SpellChecker.Dictionary.WordDictionary();
                oDict.DictionaryFile = "es-ES.dic";


                try
                {
                    //Cargar Palabras a Exnorerar en diccionario
                    string _filename = Path.Combine(Application.StartupPath, "validation", "ignore_words.dic");
                    FileInfo _f = new FileInfo(_filename);
                    if (_f.Exists)
                    {
                        string[] _lines = File.ReadAllLines(_f.FullName, Encoding.Default);
                        foreach (string line in _lines)
                        {
                            if (!String.IsNullOrEmpty(line))
                            {
                                oDict.Add(line);
                                //Console.WriteLine( oDict.PhoneticCode(line));
                            }
                        }
                    }

                    oDict.EnableUserFile = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el archivo : ignore_words.dic \n" + ex.Message);
                }
                

                //load and initialize the dictionary 
                oDict.Initialize();

                oSpell = new Spelling();
                oSpell.Dictionary = oDict;
                oSpell.IgnoreWordsWithDigits = false;
                oSpell.IgnoreAllCapsWords = true;

            }
            return oSpell;
        }


        public static Groups getReservedWords()
        {
            if (_reserve_words == null)
            {
                string _filename = Path.Combine(Application.StartupPath,"validation", "reserved.xml");
                FileInfo _f = new FileInfo(_filename);
                if (_f.Exists)
                {
                    _reserve_words = Deserialize<Groups>(_f.FullName);
                }
            }
            return _reserve_words;
        }


        public static Groups getDocumentationWords()
        {
            if (_documentation_words == null)
            {
                string _filename = Path.Combine(Application.StartupPath,"validation", "documentation.xml");
                FileInfo _f = new FileInfo(_filename);
                if (_f.Exists)
                {
                    _documentation_words = Deserialize<Groups>(_f.FullName, Encoding.UTF8);
                }
            }
            return _documentation_words;
        }





        // Método para leer el archivo con la codificación adecuada y fin de línea CR LF
        public static string ReadFileWithEncoding(string filePath, Encoding encoding)
        {
            try
            {
                // Leer todos los bytes del archivo
                byte[] fileBytes = File.ReadAllBytes(filePath);

                // Intentar decodificar con la codificación proporcionada
                string content = encoding.GetString(fileBytes);

                return content; // Devuelve el contenido convertido con la codificación
            }
            catch (Exception ex)
            {
                // Si ocurre un error al intentar leer con esta codificación, devuelve null
                Console.WriteLine($"Error con la codificación {encoding.EncodingName}: {ex.Message}");
                return null;
            }
        }

        // Método para intentar varias codificaciones y devolver el contenido legible
        public static string DetectAndReadFile(string filePath)
        {
            // Intentar con diferentes codificaciones comunes
            string content = null;

            // Intentar con UTF-8
            content = ReadFileWithEncoding(filePath, Encoding.UTF8);
            if (content != null && !HasInvalidChars(content))
            {
                return content;
            }

            // Intentar con Windows-1252 (ANSI)
            content = ReadFileWithEncoding(filePath, Encoding.GetEncoding("windows-1252"));
            if (content != null && !HasInvalidChars(content))
            {
                return content;
            }

            // Intentar con ISO-8859-1 (Latin-1)
            content = ReadFileWithEncoding(filePath, Encoding.GetEncoding("ISO-8859-1"));
            if (content != null && !HasInvalidChars(content))
            {
                return content;
            }

            // Si no se pudo leer con ninguna codificación
            return "No se pudo detectar correctamente la codificación.";
        }

        // Método para verificar si el contenido contiene caracteres inválidos
        private static bool HasInvalidChars(string content)
        {
            foreach (var c in content)
            {
                // Si encuentra un carácter fuera del rango imprimible, lo considera inválido
                if (char.IsControl(c) && c != '\r' && c != '\n' && c != '\t')
                {
                    return true;
                }
            }
            return false;
        }




    }


}

