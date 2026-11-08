using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net;

namespace GxKBBase18
{
    public static class GxConvertVarbinary
    {
        public static int FindGzipStart(byte[] data)
        {
            for (int i = 0; i < data.Length - 2; i++)
            {
                if (data[i] == 0x1F && data[i + 1] == 0x8B && data[i + 2] == 0x08)
                    return i;
            }
            return -1;
        }
        //public static string DecompressGzipToString(byte[] gzipData)
        //{
        //    using (var input = new MemoryStream(gzipData))
        //    using (var gzip = new GZipStream(input, CompressionMode.Decompress))
        //    using (var reader = new StreamReader(gzip, Encoding.Unicode)) // UTF-16
        //    {
        //        return reader.ReadToEnd();
        //    }
        //}

        public static string DecodeEntityVersionData(byte[] data)
        {
            if (data.Length <= 0)
            {
                return "";
            }

            using (var input = new MemoryStream(data))
            using (var gzip = new GZipStream(input, CompressionMode.Decompress))
            using (var output = new MemoryStream())
            {
                gzip.CopyTo(output);
                return Encoding.UTF8.GetString(output.ToArray());
            }
            
        }

        public static string DecompressAndFormatGzip(byte[] gzipData)
        {
            if (gzipData.Length <= 0)
            {
                return String.Empty;
            }

            int gzipStart = FindGzipHeader(gzipData);
            if (gzipStart < 0)
            {
                return String.Empty;
            }

            using (MemoryStream ms = new MemoryStream(gzipData, gzipStart, gzipData.Length - gzipStart))
            using (GZipStream gzip = new GZipStream(ms, CompressionMode.Decompress))
            using (MemoryStream outStream = new MemoryStream())
            {
                gzip.CopyTo(outStream);
                outStream.Position = 0;

                // Leer el XML como UTF-8 explícito
                string xmlContent;
                using (StreamReader reader = new StreamReader(outStream, Encoding.UTF8))
                {
                    xmlContent = reader.ReadToEnd();
                }

                // Formatear XML
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlContent); // Usar LoadXml en vez de Load para evitar detección de BOM

                StringBuilder sb = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ",
                    NewLineChars = "\r\n",
                    NewLineHandling = NewLineHandling.Replace,
                    Encoding = Encoding.UTF8
                };

                using (XmlWriter writer = XmlWriter.Create(sb, settings))
                {
                    doc.Save(writer);
                }

                return sb.ToString();
            }
        }

        private static int FindGzipHeader(byte[] data)
        {
            for (int i = 0; i < data.Length - 2; i++)
            {
                if (data[i] == 0x1F &&
                    data[i + 1] == 0x8B &&
                    data[i + 2] == 0x08)
                {
                    return i;
                }
            }
            return -1;
        }

        public static string ExtractStorableContent(string xml, string pTagXML)
        {
            if (string.IsNullOrWhiteSpace(xml))
                return string.Empty;

            var match = Regex.Match(
                xml,
                $@"<{pTagXML}>(.*?)</{pTagXML}>",
                RegexOptions.Singleline | RegexOptions.IgnoreCase
            );

            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        public static string ConvertHelpToPart(string xml, string pGuid)
        {
            if (string.IsNullOrWhiteSpace(xml))
                return string.Empty;

            // 1. Extraer Content (CDATA)
            var contentMatch = Regex.Match(
                xml,
                @"<Content><!\[CDATA\[(.*?)\]\]></Content>",
                RegexOptions.Singleline | RegexOptions.IgnoreCase
            );

            string content = contentMatch.Success
                ? contentMatch.Groups[1].Value.Trim()
                : string.Empty;

            // 2. Extraer LanguageReference
            var langMatch = Regex.Match(
                xml,
                @"<LanguageReference>(.*?)</LanguageReference>",
                RegexOptions.IgnoreCase
            );

            string languageRef = langMatch.Success
                ? langMatch.Groups[1].Value
                : string.Empty;

            // 3. Normalizar Language base (quitar sufijo -7)
            string languageBase = Regex.Replace(languageRef, @"-\d+$", "");

            // 4. Construir Part
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"<Part type=\"{pGuid}\">");
            sb.AppendLine("  <Help>");
            sb.AppendLine("    <HelpItem>");
            sb.AppendLine($"      <Language>{languageBase}-Spanish</Language>");
            sb.AppendLine("      <Content />");
            sb.AppendLine("    </HelpItem>");
            sb.AppendLine("    <HelpItem>");
            sb.AppendLine($"      <Language>{languageBase}-English</Language>");
            sb.AppendLine("      <Content />");
            sb.AppendLine("    </HelpItem>");
            sb.AppendLine("  </Help>");
            sb.AppendLine("  <Properties>");
            sb.AppendLine("    <Property>");
            sb.AppendLine("      <Name>IsDefault</Name>");
            sb.AppendLine("      <Value>False</Value>");
            sb.AppendLine("    </Property>");
            sb.AppendLine("  </Properties>");
            sb.AppendLine("</Part>");

            return sb.ToString();
        }



        public static string ConvertXmlToCode(string xml, string pType)
        {
            switch (pType)
            {
                case "Rules":
                case "Conditions":
                    return GeneXusTokenConverter.ConvertTokenDataListToGX_Rules(xml);

                case "Events":
                case "Source":
                    return GeneXusTokenConverter.ConvertTokenDataListToGX_Events(xml);

                default:
                    return "";
            }

                
        }

    }
}
