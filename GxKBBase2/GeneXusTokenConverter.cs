using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace GxKBBase18
{
    public static class GeneXusTokenConverter
    {

        public static string ConvertTokenDataListToGX_Rules(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                return string.Empty;

            var doc = XDocument.Parse(xml);
            var sb = new StringBuilder();

            string pendingComment = null;

            foreach (var t in doc.Descendants("TokenData"))
            {
                int token = int.Parse(t.Element("Token")?.Value ?? "0");
                string word = WebUtility.HtmlDecode(t.Element("Word")?.Value ?? "");

                switch (token)
                {
                    case 25: // comentarios
                        if (!string.IsNullOrWhiteSpace(word) &&
                            word.TrimStart().StartsWith("//"))
                        {
                            pendingComment = word.Trim();
                        }
                        break;

                    case 18: // ;
                        sb.Append(";");
                        if (!string.IsNullOrEmpty(pendingComment))
                        {
                            sb.Append(" ");
                            sb.Append(pendingComment);
                            pendingComment = null;
                        }
                        sb.AppendLine();
                        break;

                    case 0:  // (
                    case 4:  // )
                    case 7:  // ,
                    case 10: // =
                        sb.Append(word);
                        break;

                    default:
                        if (word.Equals("IF", StringComparison.OrdinalIgnoreCase))
                        {
                            sb.Append(" IF ");
                        }
                        else
                        {
                            sb.Append(word);
                        }
                        break;
                }
            }

            return NormalizeRules(sb.ToString());
        }

        public static string ConvertTokenDataListToGX_Events(string xml)
        {
            var parser = new GeneXusEventsParser();
            return parser.ParseXmlToFormattedCode(xml);
        }

        private static string Indent(int level)
        {
            return new string(' ', level * 3);
        }

        private static void FlushLine(StringBuilder output, StringBuilder line, int indent, string unit)
        {
            if (line.Length > 0)
            {
                output.AppendLine(Indent(indent, unit) + line.ToString().TrimEnd());
                line.Clear();
            }
        }

        private static bool NeedsSpaceBefore(StringBuilder line)
        {
            if (line.Length == 0)
                return false;

            char last = line[line.Length - 1];
            return last != '(' && last != '=' && last != ',' && last != ' ';
        }

        private static string Indent(int level, string unit)
        {
            return new string(' ', level * unit.Length);
        }

        private static string NormalizeRules(string text)
        {
            var lines = text.Replace("\r", "")
                            .Split('\n')
                            .Select(l => l.Trim())
                            .Where(l => l.Length > 0)
                            .ToList();

            return string.Join("\n", lines);
        }

        private static bool NeedsSpace(StringBuilder sb)
        {
            if (sb.Length == 0) return false;

            char last = sb[sb.Length - 1];
            return last != '(' && last != ',' && last != '=' && last != ' ';
        }

        // =========================
        // XML PARSER
        // =========================
        private static List<TokenData> ParseXml(string xml)
        {
            var doc = XDocument.Parse(xml);

            return doc.Descendants("TokenData")
                      .Select(t => new TokenData
                      {
                          Token = int.Parse(t.Element("Token")?.Value ?? "0"),
                          Word = t.Element("Word")?.Value ?? string.Empty
                      })
                      .ToList();
        }

        // =========================
        // CLEAN OUTPUT
        // =========================
        private static string NormalizeResult(string text)
        {
            var lines = text.Replace("\r", "")
                            .Split('\n')
                            .Select(l => l.TrimEnd())
                            .ToList();

            var result = new List<string>();
            bool lastEmpty = false;

            foreach (var line in lines)
            {
                bool empty = string.IsNullOrWhiteSpace(line);

                if (empty && lastEmpty)
                    continue;

                result.Add(line);
                lastEmpty = empty;
            }

            return string.Join("\n", result).Trim();
        }
    }

    
}