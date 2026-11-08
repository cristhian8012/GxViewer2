using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace gxViewer
{
    public static class GxUtil
    {

        public static string GenerateSQL_From_ForEach(string txtForEach)
        {
            string _resultSQL = string.Empty;
            try
            {
                _resultSQL = generaSQLForEach(txtForEach);
            }
            catch (Exception ex)
            {
                _resultSQL = "";
                Console.WriteLine(ex.Message);
            }



            return _resultSQL;
        }

        private static string generaSQLForEach(string genexusCode)
        {
            // Expresión regular para detectar las condiciones Where (sin distinguir mayúsculas/minúsculas)
            var wherePattern = new Regex(@"Where\s+([A-Za-z0-9_]+)\s*=\s*&([A-Za-z0-9_]+)", RegexOptions.IgnoreCase);

            // Expresión regular para detectar las asignaciones de variables (como &char1 = VAW3char1) (sin distinguir mayúsculas/minúsculas)
            var varPattern = new Regex(@"&([A-Za-z0-9_]+)\s*=\s*([A-Za-z0-9_]+)", RegexOptions.IgnoreCase);
            //var varPattern = new Regex(@"&([A-Za-z0-9_]+)\s*=\s*([A-Za-z0-9_.,\(\)\+\-]+)", RegexOptions.IgnoreCase);

            // Expresión regular para detectar el nombre de la tabla después de "//" (sin distinguir mayúsculas/minúsculas)
            var tablePattern = new Regex(@"//\s*([A-Za-z0-9_]+)", RegexOptions.IgnoreCase);

            // Separar el código en líneas
            string[] lines = genexusCode.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // Variables para construir la consulta SQL
            List<string> selectedFields = new List<string>();
            List<string> whereConditions = new List<string>();
            List<string> joinConditions = new List<string>();  // Para almacenar las condiciones de JOIN
            List<string> tableAliases = new List<string>();    // Para almacenar los alias de las tablas
            string tableName = "Tabla01";  // Asignamos el nombre de la tabla principal por defecto
            bool inForEach = false;
            bool inInnerForEach = false;
            string orderByFields = "";
            string innerSelectFields = "";  // Campos de selección del segundo For Each (tabla secundaria)
            Dictionary<string, string> aliasTableMap = new Dictionary<string, string>();  // Mapa de alias a tablas

            char aliasCounter = 'A';  // Inicializamos el contador para los alias de una sola letra (A, B, C...)

            // Procesar cada línea
            foreach (var line in lines)
            {
                string trimmedLine = line.Trim().ToLower();  // Convertir la línea a minúsculas

                // Si la línea es un comentario (empieza con // o /*), la ignoramos
                if (trimmedLine.StartsWith("//") || trimmedLine.StartsWith("/*"))
                {
                    continue;
                }

                // Si encontramos el inicio de un For Each (para la tabla principal)
                if (trimmedLine.StartsWith("for each"))
                {
                    if (!inForEach)
                    {
                        inForEach = true;
                        // Buscar el nombre de la tabla principal
                        var tableMatch = tablePattern.Match(trimmedLine);
                        if (tableMatch.Success)
                        {
                            tableName = tableMatch.Groups[1].Value; // Nombre de la tabla principal
                            aliasTableMap[aliasCounter.ToString()] = tableName; // Asignamos un alias para esta tabla
                            tableAliases.Add(aliasCounter.ToString()); // Añadimos el alias
                        }

                        // Buscar el ORDER BY si está presente en la misma línea
                        var orderByMatch = Regex.Match(trimmedLine, @"order\s+([A-Za-z0-9_]+(\s+[A-Za-z0-9_]+)*)", RegexOptions.IgnoreCase);
                        if (orderByMatch.Success)
                        {
                            orderByFields = orderByMatch.Groups[1].Value;
                        }
                    }
                    else
                    {
                        // Si encontramos un segundo For Each (para la tabla secundaria)
                        inInnerForEach = true;
                        var tableMatch = tablePattern.Match(trimmedLine);
                        if (tableMatch.Success)
                        {
                            string secondaryTable = tableMatch.Groups[1].Value; // Nombre de la tabla secundaria
                            aliasCounter++; // Aumentar el alias
                            aliasTableMap[aliasCounter.ToString()] = secondaryTable; // Asignamos un alias para esta tabla
                            tableAliases.Add(aliasCounter.ToString()); // Añadimos el alias
                        }
                    }
                }

                // Si estamos dentro de un For Each, evaluamos las condiciones WHERE
                else if (inForEach && !inInnerForEach && trimmedLine.StartsWith("where"))
                {
                    var whereMatch = wherePattern.Match(trimmedLine);
                    if (whereMatch.Success)
                    {
                        string columnName = whereMatch.Groups[1].Value;  // VAW3Cod, VAW3Usu, etc.
                        string valueName = whereMatch.Groups[2].Value;  // &pgcod, &vaw3usu, etc.
                        if (tableAliases.Count == 0)
                        {
                            tableAliases.Add("X");
                        }
                        whereConditions.Add($"{tableAliases[0]}.{columnName} = &{valueName}");  // Usamos el alias de la tabla principal
                    }
                }

                // Si estamos en el For Each anidado (segundo For Each)
                else if (inInnerForEach && trimmedLine.StartsWith("where"))
                {
                    // Detectamos el campo de la condición de unión entre tablas (por ejemplo MONEDA)
                    var match = Regex.Match(trimmedLine, @"where\s+([A-Za-z0-9_]+)\s*=\s*&([A-Za-z0-9_]+)", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        string fieldName = match.Groups[1].Value;  // MONEDA
                        string joinVariable = match.Groups[2].Value; // &scmda
                        joinConditions.Add($"ON {tableAliases[0]}.{joinVariable} = {tableAliases[tableAliases.Count - 1]}.{fieldName}");  // Usamos los alias de la tabla principal y secundaria
                    }
                }

                // Si encontramos el final del For Each anidado, terminamos el ciclo
                else if (inInnerForEach && trimmedLine.StartsWith("endfor"))
                {
                    inInnerForEach = false;  // Termina el ciclo For Each anidado
                }

                // Si encontramos el final del primer For Each, terminamos el ciclo principal
                else if (inForEach && trimmedLine.StartsWith("endfor"))
                {
                    inForEach = false;  // Termina el ciclo For Each principal
                    break;
                }

                // Si la línea contiene una asignación de variable (campos de selección)
                else if (inForEach && varPattern.IsMatch(trimmedLine))
                {
                    var varMatch = varPattern.Match(trimmedLine);
                    if (varMatch.Success)
                    {
                        string varName = varMatch.Groups[1].Value;  // &char1, &scsdo, etc.
                        string columnName = varMatch.Groups[2].Value;  // VAW3char1, VAW3imp1, etc.

                        if (!columnName.Trim().StartsWith("&") )
                        {
                            if (columnName.Contains("("))
                            {
                                //eliminar la funcion para obtener solo el campo
                                int _pos = columnName.IndexOf('(');
                                int _pos2 = columnName.IndexOf(',', _pos);
                                if (_pos2 < 0)
                                {
                                    _pos2 = columnName.IndexOf(')', _pos);
                                }
                                if (_pos2 > _pos)
                                {
                                    varName = $"{varName.Substring(_pos, _pos2-_pos)},--{varName} ";
                                }
                            }

                            if (tableAliases.Count ==0)
                            {
                                tableAliases.Add("X");
                            }

                            selectedFields.Add($"{tableAliases[0]}.{columnName} AS \"{varName}\"");
                        }

                    }
                }

                // Si estamos en el For Each anidado, agregamos el campo Monom
                else if (inInnerForEach && trimmedLine.Contains("&monom"))
                {
                    selectedFields.Add($"{tableAliases[tableAliases.Count - 1]}.Monom AS \"&monom\"");
                }
            }

            // Construir la consulta SQL
            string sqlQuery = "SELECT ";

            // Agregar las variables seleccionadas a la consulta
            if (selectedFields.Count <= 0)
            {
                sqlQuery += "*";
            }
            else
            {
                sqlQuery += string.Join(",\n       ", selectedFields);
            }

            // Añadir la cláusula FROM con la tabla principal
            sqlQuery += $"\n  FROM {tableName} {tableAliases[0]}";

            // Añadir los LEFT JOIN con las tablas secundarias si existen
            for (int i = 1; i < tableAliases.Count; i++)
            {
                sqlQuery += $"\n  LEFT JOIN {aliasTableMap[tableAliases[i]]} {tableAliases[i]} {joinConditions[i - 1]}";
            }

            // Añadir la cláusula WHERE
            if (whereConditions.Count > 0)
            {
                sqlQuery += "\n  WHERE " + string.Join("\n   AND ", whereConditions);
            }
            else
            {
                sqlQuery += "\n  WHERE 1 = 1"; // Placeholder
            }

            // Añadir la cláusula ORDER BY
            if (!string.IsNullOrEmpty(orderByFields))
            {
                sqlQuery += "\n  ORDER BY " + orderByFields;
            }

            // Finalizar la consulta con un punto y coma
            sqlQuery += ";";

            return sqlQuery;
        }


    }
}
