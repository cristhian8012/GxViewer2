using Genexus_Definition_HtmlBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Genexus_Definition_HtmlBuilder
{
    public class GenexusXmlParser
    {
        public WebUiDefinition Parse(string xmlContent)
        {
            if (string.IsNullOrWhiteSpace(xmlContent))
                throw new ArgumentException("El XML está vacío.");

            string safeXml = SanitizeXml(xmlContent);
            XDocument doc = XDocument.Parse(safeXml, LoadOptions.PreserveWhitespace);

            XElement webUi = doc.Descendants("web-ui").FirstOrDefault();
            if (webUi == null)
                throw new InvalidOperationException("No se encontró el nodo <web-ui> en el XML.");

            var def = new WebUiDefinition
            {
                Title = (string)webUi.Attribute("title") ?? string.Empty,
                ClassName = (string)webUi.Attribute("class") ?? "other",
                GenerateStart = ParseBool((string)webUi.Attribute("generateStart")),
                GenerateInit = ParseBool((string)webUi.Attribute("generateInit")),
                GenerateMsgReport = ParseBool((string)webUi.Attribute("generateMsgReport")),
                GenerateCall = (string)webUi.Attribute("generateCall"),
                Form = new FormDefinition(),
                HiddenData = new HiddenData(),
                RawXml = safeXml
            };

            XElement mainForm = webUi.Element("form");
            if (mainForm != null)
            {
                // 1. Cargar Categorías
                foreach (XElement catEl in mainForm.Elements("category"))
                {
                    var catDef = new CategoryDefinition
                    {
                        Title = (string)catEl.Attribute("title"),
                        Form = new FormDefinition(),
                        Operations = new List<OperationDefinition>()
                    };

                    XElement innerForm = catEl.Element("form");
                    if (innerForm != null)
                    {
                        // A) Cargar Grids
                        foreach (XElement gridEl in innerForm.Elements("grid"))
                        {
                            catDef.Form.Grids.Add(ParseGrid(gridEl));
                        }

                        // B) Cargar Campos sueltos (para "Borrar Archivos según Criterio")
                        // Los agrupamos en un Grid "Falso" para que el HtmlGenerator los pueda pintar.
                        var fakeGrid = new GridDefinition
                        {
                            Name = catDef.Title,
                            Filters = new List<FilterDefinition>(),
                            Columns = new List<ColumnDefinition>() // Vacío para indicar que no es grilla real
                        };

                        foreach (XElement fieldEl in innerForm.Elements("field"))
                        {
                            fakeGrid.Filters.Add(new FilterDefinition
                            {
                                Type = "field",
                                Caption = (string)fieldEl.Attribute("caption"),
                                Variable = (string)fieldEl.Attribute("data") ?? (string)fieldEl.Attribute("variable")
                            });
                        }

                        foreach (XElement valListEl in innerForm.Elements("value-list"))
                        {
                            fakeGrid.Filters.Add(new FilterDefinition
                            {
                                Type = "value-list",
                                Caption = (string)valListEl.Attribute("caption"),
                                Variable = (string)valListEl.Attribute("data") ?? (string)valListEl.Attribute("variable")
                            });
                        }

                        if (fakeGrid.Filters.Count > 0)
                        {
                            catDef.Form.Grids.Add(fakeGrid);
                        }
                    }

                    // C) Cargar Operaciones de la Categoría
                    XElement catOps = catEl.Element("operations");
                    if (catOps != null)
                    {
                        foreach (XElement o in catOps.Elements("operation"))
                        {
                            catDef.Operations.Add(new OperationDefinition
                            {
                                Caption = (string)o.Attribute("caption"),
                                Selection = (string)o.Attribute("selection")
                            });
                        }
                    }

                    def.Form.Categories.Add(catDef);
                }

                // 2. (Opcional) Grids a nivel raíz si existieran
                foreach (XElement gridEl in mainForm.Elements("grid"))
                {
                    def.Form.Grids.Add(ParseGrid(gridEl));
                }
            }

            // Hidden data
            XElement hiddenData = webUi.Element("hidden-data");
            if (hiddenData != null)
            {
                def.HiddenData.Auto = ParseBool((string)hiddenData.Attribute("auto"));
                foreach (XElement d in hiddenData.Elements("data"))
                {
                    string name = (string)d.Attribute("name");
                    if (!string.IsNullOrEmpty(name))
                        def.HiddenData.DataNames.Add(name);
                }
            }

            return def;
        }

        private static string SanitizeXml(string xml)
        {
            var regex = new Regex(@"&(?!(?:amp|lt|gt|quot|apos|#\d+|#x[0-9a-fA-F]+);)");
            return regex.Replace(xml, "&amp;");
        }

        private GridDefinition ParseGrid(XElement gridEl)
        {
            var grid = new GridDefinition
            {
                Name = (string)gridEl.Attribute("name"),
                OrderBy = (string)gridEl.Attribute("orderBy"),
                Filters = new List<FilterDefinition>(),
                Columns = new List<ColumnDefinition>(),
                HiddenColumns = new List<ColumnDefinition>(),
                Operations = new List<OperationDefinition>(),
                Conditions = new List<ConditionDefinition>()
            };

            // Conditions
            XElement conditions = gridEl.Element("conditions");
            if (conditions != null)
            {
                foreach (XElement c in conditions.Elements("condition"))
                {
                    string expr = (string)c.Attribute("expression");
                    if (!string.IsNullOrEmpty(expr))
                        grid.Conditions.Add(new ConditionDefinition { Expression = expr });
                }
            }

            // Filters (Field, Value-List, Line-break)
            XElement filters = gridEl.Element("filters");
            if (filters != null)
            {
                foreach (XElement f in filters.Elements())
                {
                    string localName = f.Name.LocalName;
                    if (localName == "line-break")
                    {
                        grid.Filters.Add(new FilterDefinition { Type = "line-break" });
                        continue;
                    }

                    if (localName == "dependency")
                    {
                        var fd = new FilterDefinition
                        {
                            Type = "dependency",
                            Caption = (string)f.Attribute("caption"),
                            Determinants = (string)f.Attribute("determinants"),
                            Dependents = (string)f.Attribute("dependents")
                        };
                        grid.Filters.Add(fd);
                    }
                    else if (localName == "field")
                    {
                        grid.Filters.Add(new FilterDefinition
                        {
                            Type = "field",
                            Caption = (string)f.Attribute("caption"),
                            Variable = (string)f.Attribute("variable")
                        });
                    }
                    else if (localName == "value-list")
                    {
                        grid.Filters.Add(new FilterDefinition
                        {
                            Type = "value-list",
                            Caption = (string)f.Attribute("caption"),
                            Variable = (string)f.Attribute("variable")
                        });
                    }
                }
            }

            // Columns
            XElement columns = gridEl.Element("columns");
            if (columns != null)
            {
                foreach (XElement c in columns.Elements("field-column"))
                {
                    grid.Columns.Add(new ColumnDefinition
                    {
                        Title = (string)c.Attribute("title"),
                        Data = (string)c.Attribute("data"),
                        IsHidden = false
                    });
                }
            }

            // Hidden columns
            XElement hiddenCols = gridEl.Element("hidden-columns");
            if (hiddenCols != null)
            {
                foreach (XElement c in hiddenCols.Elements("column"))
                {
                    grid.HiddenColumns.Add(new ColumnDefinition
                    {
                        Data = (string)c.Attribute("data"),
                        IsHidden = true
                    });
                }
            }

            // Operations
            XElement ops = gridEl.Element("operations");
            if (ops != null)
            {
                foreach (XElement o in ops.Elements("operation"))
                {
                    var op = new OperationDefinition
                    {
                        Caption = (string)o.Attribute("caption"),
                        Selection = (string)o.Attribute("selection")
                    };
                    grid.Operations.Add(op);
                }
            }

            return grid;
        }

        private bool ParseBool(string value)
        {
            return !string.IsNullOrEmpty(value) &&
                   (value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                    value.Equals("1", StringComparison.OrdinalIgnoreCase));
        }
    }
}