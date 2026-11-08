using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Net;
using System.Linq;

namespace GxKBBase18
{
    public class GeneXusEventsParser
    {
        public string ParseXmlToFormattedCode(string xmlContent)
        {
            if (string.IsNullOrEmpty(xmlContent)) return string.Empty;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TokenDataList));
                TokenDataList dataList;

                using (StringReader reader = new StringReader(xmlContent))
                {
                    dataList = (TokenDataList)serializer.Deserialize(reader);
                }

                StringBuilder result = new StringBuilder();
                int indentLevel = 0;
                bool lineStarted = false;

                // Palabras que SIEMPRE deben empezar en una línea nueva e indentada
                string[] forceNewLineKeywords = {
                    "do", "for each", "if", "else", "endif", "endfor", "endsub", "exit",
                    "endevent", "where", "defined by", "when none", "call", "msg", "sub", "event", "do while", "new", "endnew",
                    "enddo","when duplicate"
                };

                for (int i = 0; i < dataList.Tokens.Count; i++)
                {
                    var token = dataList.Tokens[i];
                    string word = WebUtility.HtmlDecode(token.Word ?? "");
                    string wordLower = word.Trim().ToLower();

                    // 1. Token 107: Salto de línea explícito en el XML
                    if (token.Token == 107)
                    {
                        AddNewLine(result, ref lineStarted);
                        continue;
                    }

                    // 2. Token 25: Espacios o Comentarios
                    if (token.Token == 25)
                    {
                        if (string.IsNullOrWhiteSpace(word))
                        {
                            if (lineStarted) result.Append(" ");
                        }
                        else if (word.Trim().StartsWith("//"))
                        {
                            // Si es un comentario, espacio previo y luego el comentario
                            if (lineStarted && !result.ToString().EndsWith(" ")) result.Append(" ");
                            result.Append(word.Trim());
                            AddNewLine(result, ref lineStarted);
                        }
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(word)) continue;

                    // 3. Lógica de formateo por Palabras Clave

                    // Detectar si es una asignación de variable (ej: &Var =)
                    bool isAssignment = word.Trim().StartsWith("&") && i + 1 < dataList.Tokens.Count &&
                                       (dataList.Tokens[i + 1].Word?.Trim() == "=" || dataList.Tokens[i + 2]?.Word?.Trim() == "=");

                    // Detectar si la palabra actual es parte de la cabecera (ej: después de 'Event' o 'Sub')
                    bool isHeaderContent = false;
                    if (i > 0)
                    {
                        string prev = dataList.Tokens[i - 1].Word?.Trim().ToLower();
                        if (prev == "event" || prev == "sub" || prev == "for each" || prev == "do while" || prev == "new") isHeaderContent = true;
                    }

                    // Forzar nueva línea para comandos o variables que no son parte de una cabecera
                    if ((forceNewLineKeywords.Contains(wordLower) || isAssignment) && !isHeaderContent)
                    {
                        // Reducir indentación antes de escribir palabras de cierre
                        if (wordLower == "endif" || wordLower == "endfor" || wordLower == "else" ||
                            wordLower == "when none" || wordLower == "endevent" || wordLower == "endsub" ||
                            wordLower == "when duplicated" || wordLower == "endnew" || wordLower == "enddo")
                        {
                            indentLevel = Math.Max(0, indentLevel - 1);
                        }

                        AddNewLine(result, ref lineStarted);
                    }

                    // Escribir la indentación si la línea acaba de empezar
                    if (!lineStarted)
                    {
                        result.Append(new string('\t', indentLevel));
                        lineStarted = true;
                    }
                    else
                    {
                        // Asegurar un espacio entre palabras en la misma línea
                        if (!result.ToString().EndsWith(" ") && !result.ToString().EndsWith("\t"))
                            result.Append(" ");
                    }

                    result.Append(word.Trim());

                    // Aumentar indentación DESPUÉS de palabras de apertura
                    if (wordLower == "event" || wordLower == "sub" || wordLower == "for each" ||
                        wordLower == "if" || wordLower == "else" || wordLower == "when none" ||
                        wordLower == "new" || wordLower == "do while")
                    {
                        indentLevel++;
                    }

                    // Doble salto de línea tras bloques grandes para limpieza visual
                    if (wordLower == "endevent" || wordLower == "endsub")
                    {
                        result.AppendLine().AppendLine();
                        lineStarted = false;
                    }
                }

                return result.ToString().Trim();
            }
            catch (Exception ex)
            {
                return "Error al parsear: " + ex.Message;
            }
        }

        private void AddNewLine(StringBuilder sb, ref bool lineStarted)
        {
            if (sb.Length > 0 && !sb.ToString().EndsWith(Environment.NewLine))
            {
                sb.AppendLine();
                lineStarted = false;
            }
        }
    }

    [XmlRoot("TokenDataList")]
    public class TokenDataList
    {
        [XmlElement("TokenData")]
        public List<TokenData> Tokens { get; set; } = new List<TokenData>();
    }

    public class TokenData
    {
        [XmlElement("Token")] public int Token { get; set; }
        [XmlElement("Word")] public string Word { get; set; }
        [XmlElement("Id")] public int Id { get; set; }
    }
}