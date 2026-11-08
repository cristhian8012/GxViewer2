using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using gxViewer.GxObject; // Asegúrate de tener el using de tu namespace

namespace Genexus_Definition_HtmlBuilder
{
    public class HtmlGenerator
    {
        private GXObj _gxObj;

        public string Generate(WebUiDefinition def, GXObj gxObj, string title = null)
        {
            if (string.IsNullOrEmpty(def?.RawXml))
                return "<html><body>Error: El objeto no contiene el XML original.</body></html>";

            _gxObj = gxObj ?? throw new ArgumentNullException(nameof(gxObj));
            return GenerateFromXml(def.RawXml);
        }

        public string Generate(WebUiDefinition def, string title = null)
        {
            return Generate(def, null, title);
        }

        public string GenerateFromXml(string xmlContent)
        {
            var doc = XDocument.Parse(xmlContent);
            var webUi = doc.Root?.Element("def")?.Element("object-definition")?.Element("web-ui")
                        ?? doc.Root?.Element("object-definition")?.Element("web-ui");

            if (webUi == null) return "<html><body>XML inválido</body></html>";

            string pageTitle = webUi.Attribute("title")?.Value ?? "Archivos";

            var html = new StringBuilder();
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine($" <base href=\"file://{Application.StartupPath}\\WebUI\\FormFiles\\\">");
            html.AppendLine(" <meta http-equiv=\"Designer\" content=\"Dlya.Basic:1.0:WebUI\"/>");
            html.AppendLine(" <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"/>");
            html.AppendLine($" <title>{Escape(pageTitle)}</title>");
            html.AppendLine(" <style>");
            html.AppendLine(@"
                body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #f0f0f0; margin: 0; padding: 20px; font-size: 12px; }
                
                .page-container { background-color: #ffffff; border: 1px solid #ccc; padding: 15px; width: 850px; margin: 0 auto; box-sizing: border-box; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }
                
                .page-title { font-size: 16pt; font-weight: bold; color: #9f2020; margin-top: 0; margin-bottom: 10px; }
                .category-title { font-size: 11pt; font-weight: bold; color: #333; margin-top: 10px; margin-bottom: 5px; }
                .subcategory-title { font-size: 10pt; font-weight: bold; color: #333; margin-top: 8px; margin-bottom: 4px; background-color: #f0f0f0; padding: 3px; border: 1px solid #d3d3d3; }
                
                .genexus-form-table { width: 100%; border-collapse: collapse; margin-bottom: 5px; background-color: #f8f8f8; border: 1px solid #d3d3d3; }
                .genexus-form-table td { padding: 3px 5px; font-size: 12px; border: 1px solid #e0e0e0; vertical-align: middle; height: 22px; }
                .genexus-form-table .CaptionBack { text-align: right; font-weight: bold; color: #333; background-color: #f0f0f0; min-width: 120px; white-space: nowrap; font-size: 11px; }
                .genexus-form-table .FieldBack { text-align: left; background-color: #ffffff; }
                
                .input-group { display: flex; align-items: center; gap: 2px; }
                
                .text-input, .filter-input { border: 1px solid #a9a9a9; padding: 2px 4px; font-size: 12px; width: 160px; font-family: 'Segoe UI', Tahoma, sans-serif; color: #000000; box-sizing: border-box; height: 24px; border-radius: 3px; }
                .combo-input { border: 1px solid #a9a9a9; padding: 2px; font-size: 12px; width: 165px; font-family: 'Segoe UI', Tahoma, sans-serif; color: #000000; box-sizing: border-box; height: 24px; border-radius: 3px; }
                .genexus-form-table label { font-family: 'Segoe UI', Tahoma, sans-serif; color: #000000; font-size: 12px; }

                .prompt-btn { height: 24px; width: 24px; background-color: #e0e0e0; border: 1px solid #a9a9a9; cursor: pointer; display: flex; align-items: center; justify-content: center; box-sizing: border-box; padding: 0; font-size: 12px; border-radius: 3px; }
                .prompt-btn:hover { background-color: #d0d0d0; }

                .grid-container-div { width: 100%; max-width: 100%; overflow-x: auto; border: 1px solid #d3d3d3; margin-bottom: 10px; background-color: #f8f8f8; box-sizing: border-box; }
                .data-table { width: 100%; border-collapse: collapse; font-size: 12px; white-space: nowrap; table-layout: auto; }
                .data-table th { background-color: #808080; color: white; text-align: left; padding: 5px 8px; border: 1px solid #666; font-weight: bold; font-size: 11px; }
                .data-table td { padding: 5px 8px; border: 1px solid #e0e0e0; background-color: #fff; font-family: 'Segoe UI', Tahoma, sans-serif; color: #000000; }
                .data-table tr:nth-child(even) td { background-color: #f0f0f0; }
                
                /* ESTILOS DE LOS BOTONES */
                .btn-genexus { padding: 3px 15px; font-family: 'Segoe UI', Tahoma, sans-serif; font-size: 12px; background-color: #e0e0e0; color: #000000; border: 1px solid #999; cursor: pointer; margin: 2px; border-radius: 4px; }
                .btn-genexus:hover { background-color: #d0d0d0; }
                
                .btn-filtrar { padding: 3px 20px; font-family: 'Segoe UI', Tahoma, sans-serif; font-size: 12px; background-color: #4CAF50; color: white; border: 1px solid #388E3C; border-radius: 4px; cursor: pointer; font-weight: bold; }
                .btn-filtrar:hover { background-color: #45a049; }

                .ops-container { text-align: right; padding: 5px; background-color: #f8f8f8; border-top: 1px solid #d3d3d3; }

                .filter-table { width: 100%; border-collapse: collapse; background-color: #f8f8f8; border: 1px solid #d3d3d3; margin-bottom: 5px; }
                .filter-table > tbody > tr > td { padding: 4px 5px; font-size: 12px; vertical-align: middle; }
                .filter-label { text-align: right; font-weight: bold; color: #333; min-width: 120px; white-space: nowrap; font-size: 11px; }
                .filter-btn-cell { width: 90px; text-align: right; }

                .hidden-data { margin-top: 10px; padding: 5px; border-top: 2px solid #cc0000; background-color: #f9f9f9; display: flex; flex-wrap: wrap; align-items: center; }
                .hidden-title { font-size: 11px; color: #666; margin-right: 10px; font-weight: bold; }
                .gear-icon { font-size: 14px; margin-right: 4px; opacity: 0.6; cursor: help; }
            ");
            html.AppendLine(" </style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("<div class=\"page-container\">");

            html.AppendLine($"<div class=\"page-title\">{Escape(pageTitle)}</div>");

            var mainForm = webUi.Element("form");
            if (mainForm != null)
            {
                var categories = mainForm.Elements("category").ToList();
                if (categories.Any())
                {
                    foreach (var cat in categories)
                    {
                        string catTitle = cat.Attribute("title")?.Value ?? "";
                        if (!string.IsNullOrEmpty(catTitle))
                            html.AppendLine($"<div class=\"category-title\">{Escape(catTitle)}</div>");

                        var catForm = cat.Element("form");
                        if (catForm != null)
                        {
                            var subCats = catForm.Elements("sub-category").ToList();
                            if (subCats.Any())
                            {
                                foreach (var subCat in subCats)
                                {
                                    html.AppendLine($"<div class=\"subcategory-title\">{Escape(subCat.Attribute("title")?.Value ?? "")}</div>");
                                    var subForm = subCat.Element("form");
                                    if (subForm != null) html.AppendLine(ProcessFormElements(subForm));
                                }
                            }
                            else
                            {
                                html.AppendLine(ProcessFormElements(catForm));
                            }
                        }
                        var catOps = cat.Element("operations");
                        if (catOps != null && catOps.Elements("operation").Any())
                        {
                            html.AppendLine("<div class=\"ops-container\">");
                            foreach (var op in catOps.Elements("operation")) html.AppendLine(BuildButtonFromOp(op));
                            html.AppendLine("</div>");
                        }
                    }
                }
                else
                {
                    html.AppendLine(ProcessFormElements(mainForm));

                    var formOps = mainForm.Element("operations");
                    if (formOps != null && formOps.Elements("operation").Any())
                    {
                        html.AppendLine("<div class=\"ops-container\">");
                        foreach (var op in formOps.Elements("operation")) html.AppendLine(BuildButtonFromOp(op));
                        html.AppendLine("</div>");
                    }
                }
            }

            var webUiOps = webUi.Element("operations");
            if (webUiOps != null && webUiOps.Elements("operation").Any())
            {
                html.AppendLine("<div class=\"ops-container\" style=\"border-top:none; background-color:transparent;\">");
                foreach (var op in webUiOps.Elements("operation")) html.AppendLine(BuildButtonFromOp(op));
                html.AppendLine("</div>");
            }

            var hiddenData = webUi.Element("hidden-data");
            if (hiddenData != null)
            {
                html.AppendLine("<div class=\"hidden-data\">");
                html.AppendLine("<span class=\"hidden-title\">Variables Ocultas:</span>");
                foreach (var data in hiddenData.Elements("data"))
                {
                    string name = data.Attribute("name")?.Value ?? "";
                    string tooltip = name.TrimStart('&');
                    html.AppendLine($"<span class=\"gear-icon\" title=\"&amp;{Escape(tooltip)}\">⚙️</span>");
                }
                html.AppendLine("</div>");
            }

            html.AppendLine("</div>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");
            return html.ToString();
        }

        private string ProcessFormElements(XElement formNode)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<table class=\"genexus-form-table\">");
            sb.AppendLine("<tr>");

            foreach (var element in formNode.Elements())
            {
                string name = element.Name.LocalName;

                if (name == "line-break")
                {
                    sb.AppendLine("</tr><tr>");
                }
                else if (name == "field" || name == "value-list" || name == "dependency" || name == "check-box" || name == "dynamic-value-list")
                {
                    sb.AppendLine(RenderFormField(element));
                }
                else if (name == "grid")
                {
                    sb.AppendLine("</tr></table>");

                    var filters = element.Element("filters");
                    if (filters != null && filters.Elements().Any())
                        sb.AppendLine(BuildFiltersFromXml(filters));

                    sb.AppendLine(BuildGridFromXml(element));

                    sb.AppendLine("<table class=\"genexus-form-table\"><tr>");
                }
            }

            sb.AppendLine("</tr></table>");
            return sb.ToString();
        }

        private string RenderFormField(XElement f)
        {
            string caption = f.Attribute("caption")?.Value ?? "";
            string value = f.Attribute("data")?.Value ?? f.Attribute("determinants")?.Value ?? "";
            string type = f.Name.LocalName;

            string visibleExpr = "";
            var visibleNode = f.Element("visible");
            if (visibleNode != null) visibleExpr = visibleNode.Attribute("expression")?.Value ?? "";

            string varName = value.TrimStart('&');
            Variable gxVar = _gxObj?.getVarByName(varName);

            string tooltip = $"Variable: {value}";
            if (gxVar != null)
            {
                tooltip += $" | Tipo: {gxVar.Type}";
                if (gxVar.Length > 0) tooltip += $"({gxVar.Length})";
                if (gxVar.Decimales > 0) tooltip += $",{gxVar.Decimales}";
            }
            if (!string.IsNullOrEmpty(visibleExpr)) tooltip += $" | Visible si: {visibleExpr}";

            if (type == "value-list" || type == "dynamic-value-list")
            {
                return $"<td class=\"CaptionBack\">{Escape(caption)}</td><td class=\"FieldBack\"><select class=\"combo-input\" title=\"{tooltip}\"><option>{Escape(value)}</option></select></td>";
            }
            else if (type == "check-box")
            {
                return $"<td class=\"CaptionBack\">{Escape(caption)}</td><td class=\"FieldBack\"><input type=\"checkbox\" title=\"{tooltip}\"></td>";
            }
            else if (type == "dependency")
            {
                string depName = f.Attribute("dependents")?.Value ?? "";
                return $"<td class=\"CaptionBack\">{Escape(caption)}</td><td class=\"FieldBack\"><label title=\"{tooltip}\">{Escape(value)} - {Escape(depName)}</label></td>";
            }
            else // Field normal
            {
                bool isReadonly = f.Attribute("readOnly")?.Value == "true";
                string readonlyAttr = isReadonly ? "readonly" : "";

                string maxLenAttr = "";
                if (gxVar != null && gxVar.Length > 0)
                {
                    maxLenAttr = $" maxlength=\"{gxVar.Length}\"";
                }

                string inputHtml = $"<input type=\"text\" class=\"text-input\" value=\"{Escape(value)}\" {readonlyAttr}{maxLenAttr} title=\"{tooltip}\">";

                var promptNode = f.Element("prompt");
                string promptHtml = "";
                if (promptNode != null)
                {
                    string pObj = promptNode.Attribute("object")?.Value ?? "";
                    string pParams = promptNode.Attribute("params")?.Value ?? "";
                    string promptTooltip = $"Call('{pObj}', {pParams})";
                    promptHtml = $"<button class=\"prompt-btn\" title=\"{Escape(promptTooltip)}\" onclick=\"alert('{Escape(promptTooltip)}');\">&#x2191;</button>";
                }

                bool isDate = false;
                if (gxVar != null && (gxVar.Type.ToLower() == "date" || gxVar.Type.ToLower() == "datetime"))
                {
                    isDate = true;
                }
                else if (value.ToLower().Contains("fec") || value.ToLower().Contains("dat") || caption.ToLower().Contains("fecha"))
                {
                    isDate = true;
                }

                string dateHtml = "";
                if (isDate)
                {
                    dateHtml = $"<button class=\"prompt-btn\" title=\"Abrir Calendario\" onclick=\"alert('Abrir Calendario');\">&#x1F4C5;</button>";
                }

                return $"<td class=\"CaptionBack\">{Escape(caption)}</td><td class=\"FieldBack\"><div class=\"input-group\">{inputHtml}{promptHtml}{dateHtml}</div></td>";
            }
        }

        private string BuildButtonFromOp(XElement op)
        {
            string caption = op.Attribute("caption")?.Value ?? "";
            bool hasConfirmation = op.Attribute("confirmation")?.Value == "true";
            string confirmText = op.Attribute("confirmationText")?.Value ?? "¿Está seguro?";

            string visibleExpr = "";
            var visibleNode = op.Element("visible");
            if (visibleNode != null) visibleExpr = visibleNode.Attribute("expression")?.Value ?? "";

            string tooltip = string.IsNullOrEmpty(visibleExpr) ? "" : $" title=\"Visible si: {visibleExpr}\"";

            if (hasConfirmation)
            {
                string safeConfirmText = Escape(confirmText).Replace("'", "\\'");
                return $"<button class=\"btn-genexus\"{tooltip} onclick=\"return confirm('{safeConfirmText}');\">{Escape(caption)}</button>";
            }
            else
            {
                return $"<button class=\"btn-genexus\"{tooltip}>{Escape(caption)}</button>";
            }
        }

        private string BuildFiltersFromXml(XElement filtersNode)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<table class=\"filter-table\"><tr>");
            sb.AppendLine("<td valign=\"top\">");
            sb.AppendLine("<table cellpadding=\"2\" cellspacing=\"0\">");

            var currentRow = new List<XElement>();
            foreach (var element in filtersNode.Elements())
            {
                if (element.Name == "line-break")
                {
                    RenderFilterRow(sb, currentRow);
                    currentRow.Clear();
                }
                else
                {
                    currentRow.Add(element);
                }
            }
            if (currentRow.Count > 0) RenderFilterRow(sb, currentRow);

            sb.AppendLine("</table>");
            sb.AppendLine("</td>");
            sb.AppendLine("<td class=\"filter-btn-cell\" valign=\"top\">");
            // AQUI SE APLICA LA CLASE btn-filtrar (Verde)
            sb.AppendLine("<button class=\"btn-filtrar\">Filtrar</button>");
            sb.AppendLine("</td>");
            sb.AppendLine("</tr></table>");
            return sb.ToString();
        }

        private void RenderFilterRow(StringBuilder sb, List<XElement> fields)
        {
            if (fields.Count == 0) return;

            sb.AppendLine("<tr>");
            foreach (var f in fields)
            {
                string caption = f.Attribute("caption")?.Value ?? "";
                string value = f.Attribute("variable")?.Value ?? f.Attribute("data")?.Value ?? f.Attribute("determinants")?.Value ?? "";
                string name = f.Name.LocalName;

                string visibleExpr = "";
                var visibleNode = f.Element("visible");
                if (visibleNode != null) visibleExpr = visibleNode.Attribute("expression")?.Value ?? "";

                string tooltip = $"Variable: {value}";
                if (!string.IsNullOrEmpty(visibleExpr)) tooltip += $" | Visible si: {visibleExpr}";

                sb.AppendLine($"<td class=\"filter-label\">{Escape(caption)}:</td>");
                sb.AppendLine("<td>");

                if (name == "value-list")
                    sb.AppendLine($"<select class=\"combo-input\" title=\"{tooltip}\"><option>{Escape(value)}</option></select>");
                else
                    sb.AppendLine($"<input type=\"text\" class=\"filter-input\" value=\"{Escape(value)}\" title=\"{tooltip}\" readonly>");

                sb.AppendLine("</td>");
            }
            sb.AppendLine("</tr>");
        }

        private string BuildGridFromXml(XElement gridNode)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<div class='grid-container-div'>");
            sb.AppendLine("<table class=\"data-table\">");
            sb.AppendLine("<thead><tr>");

            var columns = gridNode.Element("columns");
            var allColumns = new List<XElement>();
            if (columns != null)
            {
                allColumns.AddRange(columns.Elements("field-column"));
                allColumns.AddRange(columns.Elements("value-list-column"));
                allColumns.AddRange(columns.Elements("check-box-column"));
                allColumns.AddRange(columns.Elements("prompt-column"));
            }

            foreach (var col in allColumns)
            {
                string title = col.Attribute("title")?.Value ?? "";
                string align = title.Contains("Fecha") ? "right" : (col.Name == "check-box-column" ? "center" : "left");

                string visibleExpr = "";
                var visibleNode = col.Element("visible");
                if (visibleNode != null) visibleExpr = visibleNode.Attribute("expression")?.Value ?? "";

                string dataVar = col.Attribute("data")?.Value ?? "";

                string tooltip = $"Variable: {dataVar}";
                if (!string.IsNullOrEmpty(visibleExpr)) tooltip += $" | Visible si: {visibleExpr}";

                sb.AppendLine($"<th align=\"{align}\" title=\"{tooltip}\">{Escape(title)}</th>");
            }
            sb.AppendLine("</tr></thead>");

            sb.AppendLine("<tbody>");
            for (int i = 0; i < 4; i++)
            {
                sb.AppendLine("<tr>");
                foreach (var col in allColumns)
                {
                    string title = col.Attribute("title")?.Value ?? "";
                    string dataVar = col.Attribute("data")?.Value ?? "";

                    string tooltip = $"Variable: {dataVar}";
                    var visibleNode = col.Element("visible");
                    if (visibleNode != null) tooltip += $" | Visible si: {visibleNode.Attribute("expression")?.Value}";

                    if (col.Name == "check-box-column")
                    {
                        sb.AppendLine($"<td align=\"center\"><input type=\"checkbox\" title=\"{tooltip}\"></td>");
                    }
                    else
                    {
                        string align = title.Contains("Fecha") ? "right" : "left";
                        sb.AppendLine($"<td align=\"{align}\" title=\"{tooltip}\">{Escape(dataVar)}</td>");
                    }
                }
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");
            sb.AppendLine("</div>");

            var ops = gridNode.Element("operations");
            if (ops != null && ops.Elements("operation").Any())
            {
                sb.AppendLine("<div class=\"ops-container\">");
                foreach (var op in ops.Elements("operation")) sb.AppendLine(BuildButtonFromOp(op));
                sb.AppendLine("</div>");
            }

            return sb.ToString();
        }

        private static string Escape(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            return s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
        }
    }
}