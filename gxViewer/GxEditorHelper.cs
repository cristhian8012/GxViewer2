using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using gxViewer.GxObject;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using mshtml;
using System.Diagnostics;
using gxViewer.GxObject.SDT;
using static System.Net.WebRequestMethods;
using System.ComponentModel.Design;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Web;

namespace gxViewer
{
    public class GxEditorHelper
    {
        public static string GetColorInfo(string _Color)
        {
            string _retCodeHTMLColor = "#00FF00"; //Verde por Default
            switch (_Color)
            {
                case "Black": _retCodeHTMLColor = "#000000"; break;
                case "Maroon": _retCodeHTMLColor = "#800000"; break;
                case "Green": _retCodeHTMLColor = "#008000"; break;
                case "Olive": _retCodeHTMLColor = "#808000"; break;
                case "Navy": _retCodeHTMLColor = "#000080"; break;
                case "Purple": _retCodeHTMLColor = "#800080"; break;
                case "Teal": _retCodeHTMLColor = "#008080"; break;
                case "Gray": _retCodeHTMLColor = "#808080"; break;
                case "Silver": _retCodeHTMLColor = "#C0C0C0"; break;
                case "Red": _retCodeHTMLColor = "#FF0000"; break;
                case "Lime": _retCodeHTMLColor = "#00FF00"; break;
                case "Yellow": _retCodeHTMLColor = "#FF0000"; break;
                case "Blue": _retCodeHTMLColor = "#0000FF"; break;
                case "Fuchsia": _retCodeHTMLColor = "#FF0000"; break;
                case "Aqua": _retCodeHTMLColor = "#FF00FF"; break;
                case "White": _retCodeHTMLColor = "#FFFFFF"; break;
            }
            return _retCodeHTMLColor;
        }


        public static class Transaction
        {
            public static string GetHTMLStruct(GxObject.Transaction _trn, ImageList imageList1)
            {

                string _fileimg = String.Format("{0}\\img", System.Windows.Forms.Application.StartupPath);
                if (!System.IO.Directory.Exists(_fileimg))
                {
                    System.IO.Directory.CreateDirectory(_fileimg);
                    _fileimg = _fileimg + "\\llave.png";

                    imageList1.Images["key"].Save(_fileimg);
                }
                else
                { _fileimg = _fileimg + "\\llave.png"; }


                StringBuilder _htmlStruct = new StringBuilder("<html><body><table border=1>");
                if (_trn.Structure.Source != null)
                {
                    string[] _Structure = _trn.Structure.Source.Split("\n".ToCharArray());

                    _htmlStruct.AppendLine("<tr><td>&nbsp;</td><td>Structure</td><td>Type</td> <td>Description</td> <td>Formula</td>  </tr>\n");

                    foreach (string _field in _Structure)
                    {
                        int _isKey = _field.IndexOf("*");
                        string _fieldName = _field.Replace("*", "").Replace("\r", "").Replace("(", "").Replace(")", "").Trim();

                        if (_fieldName != String.Empty)
                        {

                            GXAtt _atrib = new GXAtt();
                            if (_trn.GXAtts != null)
                            {
                                foreach (GXAtt _at in _trn.GXAtts)
                                {
                                    if (_at.Name.ToLower() == _fieldName.ToLower())
                                    {
                                        _atrib = _at;
                                        break;
                                    }
                                }
                            }

                            if (_isKey > 0)
                            {
                                _htmlStruct.AppendFormat("<tr><td><img src='{4}'></td><td>{0}</td>  <td>{1}</td>  <td>{2}</td>  <td>{3}</td> </tr>\n", _fieldName, _atrib.TipoCompleto, (_atrib.Title == String.Empty ? "&nbsp;" : _atrib.Title), "&nbsp;", _fileimg);
                            }
                            else
                            {
                                _htmlStruct.AppendFormat("<tr><td>&nbsp;</td><td>{0}</td>  <td>{1}</td>  <td>{2}</td>  <td>{3}</td>  </tr>\n", _fieldName, _atrib.TipoCompleto, (_atrib.Title == String.Empty ? "&nbsp;" : _atrib.Title), "&nbsp;");
                            }
                        }


                    }

                }
                _htmlStruct.AppendLine("</table></body></html>");

                return _htmlStruct.ToString();

            }


        }

        public static class WorkPanel
        {

            public static void PrepareLayoutXML(GxObject.WorkPanel _wpn)
            {
                try
                {
                    XmlDocument _docXml = GxXML.GetDocument2(_wpn.XMLObj);
                    XmlNodeList _xmlForms = GxXML.ReadElements(_docXml, "Form");
                    foreach (XmlNode _f in _xmlForms)
                    {
                        GxObject.Form _form = GxXML.XMLNodeToObject<GxObject.Form>(_f);
                        _wpn.Form = _form;
                    }
                }
                catch (Exception ex)
                {
                    _wpn.Form = null;
                }
                

            }

            //public static string GetHTMLDesign(GxObject.WorkPanel _wpn, ImageList imageList1)
            //{

            //    StringBuilder _htmlDesign = new StringBuilder("<html><body>");

            //    //STATIC TEXTS
            //    if (_wpn.Form != null)
            //    {
            //        int _wwidth = (_wpn.Form.Block.Frame.right - _wpn.Form.Block.Frame.left);
            //        int _wheight = (_wpn.Form.Block.Frame.bottom - _wpn.Form.Block.Frame.top);
            //        int _wleft = _wpn.Form.Block.Frame.left;
            //        int _wtop = _wpn.Form.Block.Frame.top;
            //        string _backcolor = GxEditorHelper.GetColorInfo("Black");
            //        string _forecolor = GxEditorHelper.GetColorInfo("Lime");

            //        _htmlDesign.AppendLine(String.Format("<div id='Label' style='position:absolute; left:{0}; top:{1}; width:{2}px; height:{3}px; display:block; background-color:{4}; color:{5}'><br><br>", _wleft, _wtop, _wwidth, _wheight, _backcolor, _forecolor));

            //        foreach (Text _txt in _wpn.Form.Block.Texts)
            //        {
            //            string _stylefont = String.Empty;
            //            if (_txt.Font != null)
            //            {
            //                _stylefont = String.Format("font-family:{0}; font-size:{1}px; text-decoration:{2};", _txt.Font.Name, (_txt.Font.Size == 0 ? 10 : _txt.Font.Size) + 1, (_txt.Font.Underline == "Y" ? "underline" : "none"));
            //            }
            //            if (_txt.ColorInfo != null)
            //            {
            //                _stylefont = String.Format("{0} color:{1};", _stylefont, GxEditorHelper.GetColorInfo(_txt.ColorInfo.Foreground));
            //            }

            //            string _divText = String.Format("<div style='position:absolute; left:{0}; top:{1}; width:{2}px; height:{3}px; display:block; {5}'>{4} &nbsp;</div>", _txt.left, _txt.top, (_txt.right - _txt.left), (_txt.bottom - _txt.top), (_txt.Texto == null ? "&nbsp;" : _txt.Texto.Replace(" ", "&nbsp;")), _stylefont);
            //            _htmlDesign.AppendLine(_divText);
            //            _wpn.AddLayoutText(_txt.Texto);
            //        }


            //        //FIELDS
            //        foreach (Edit _edit in _wpn.Form.Block.Edits)
            //        {
            //            string _stylefont2 = String.Empty;
            //            if (_edit.Font != null)
            //            {
            //                _stylefont2 = String.Format("font-family:{0}; font-size:{1}px; text-decoration:{2}", _edit.Font.Name, (_edit.Font.Size == 0 ? 10 : _edit.Font.Size) + 1, (_edit.Font.Underline == "Y" ? "underline" : "none"));
            //            }
            //            if (_edit.ColorInfo != null)
            //            {
            //                _stylefont2 = String.Format("{0} color:{1};", _stylefont2, GxEditorHelper.GetColorInfo(_edit.ColorInfo.Foreground));
            //            }

            //            string _strEVName = _edit.Name;
            //            if (_edit.Row != null)
            //            {
            //                if (!String.IsNullOrEmpty(_edit.Row))
            //                {
            //                    _strEVName = String.Format("{0}({1})", _edit.Name, _edit.Row);
            //                }
            //            }
            //            string _divEdit = String.Format("<div id='Text' style='position:absolute; left:{0}; top:{1}; width:{2}px; height:{3}px; display:block; border:0px solid #eee24; overflow:hidden; {5}' title='&amp;{4}'>&amp;{4}</div>", _edit.left, _edit.top, (_edit.right - _edit.left), (_edit.bottom - _edit.top), _strEVName, _stylefont2);
            //            _htmlDesign.AppendLine(_divEdit);
            //        }

            //        //GRIDS
            //        string _fontHeader = "<span style='font-family:Courier New; font-size:11px; color:#00FF00;'>{0}</span>";
            //        string _fontRow = "<div id='RowGrid' style='font-family:Courier New; font-size:11px; text-decoration:underline; color:#00FF00; display:block; overflow:hidden; width:{1}px;' title='{2}'>{0} &nbsp;</div>"; ;

            //        foreach (SubFile _sf in _wpn.Form.Block.Subfiles)
            //        {
            //            int _Heigth = (_sf.bottom - _sf.top);
            //            int _Width = (_sf.right - _sf.left) - 30;
            //            string _divSubFile = String.Format("<div id='Grid' style='position:absolute; font-size:9px;font-family:Courier New; left:{0}; top:{1}; width:{2}px; height:{3}px; display:block; overflow-y:visible; overflow-x:hidden; '>", _sf.left, _sf.top, _Width, _Heigth);



            //            string _Table = "<table cellpadding='0' cellspacing='1'  id='" + _sf.CtrlName + "' style='border:0px solid #999999;' width='90%'; >";
            //            string _fieldsBlank = String.Empty;
            //            _Table += "<theader>";
            //            _Table += "<tr>";
            //            foreach (Item _i in _sf.Items)
            //            {
            //                GxObject.Variable _var = _wpn.getVarByName(_i.Name);

            //                string _sTitulo = String.Format(_fontHeader, (_i.Title == null ? "&nbsp;" : _i.Title.Replace(" ", "&nbsp;")));
            //                _wpn.AddLayoutText(_i.Title);
            //                string _sData = String.Empty;
            //                if (_var != null)
            //                {
            //                    _Table += String.Format("<td nowrap style='width:{0}px; border:0px solid #999999;'>", _i.Width) + _sTitulo + "</td>\n        ";
            //                    int _widthitem = (8 * _var.Length);
            //                    string wNameVar = _var.Name2;
            //                    string wTitle = _var.Title;
            //                    if (_i.Row != null)
            //                    {
            //                        if (_i.Col == null)
            //                        {
            //                            wNameVar = String.Format("{0}({1})", _var.Name2, _i.Row);
            //                        }
            //                        else
            //                        {
            //                            wNameVar = String.Format("{0}({1},{2})", _var.Name2, _i.Row, _i.Col);
            //                        }
            //                        wTitle = wTitle + " - " + wNameVar;
            //                    }


            //                    int _diff = _widthitem - (_var.Name2.Length * 8);
            //                    if (_diff > 0)
            //                    {
            //                        wNameVar = wNameVar.Replace("&", "&amp;");
            //                        wNameVar = wNameVar.PadRight((_diff / 8));
            //                        wNameVar = wNameVar.Replace(" ", "&nbsp;");
            //                    }
            //                    _sData = String.Format(_fontRow, wNameVar, _widthitem, wTitle);

            //                    _fieldsBlank += "<td style='border:0px solid #999999;'>" + _sData + "</td>";
            //                }
            //                else
            //                {
            //                    _Table += String.Format("<td nowrap style='width:{0}px; border:0px solid #999999;'>", _i.Width) + _sTitulo + "&nbsp;</td>\n        ";
            //                    _fieldsBlank += "<td style='border:0px solid #999999;'>&nbsp;</td>";
            //                }
            //            }
            //            _Table += "</tr>";
            //            _Table += "</theader>";


            //            _Table += "<tbody>";
            //            int _rowsInSF = (_Heigth / 21);
            //            if (_rowsInSF != (_rowsInSF * 20)) { _rowsInSF++; }


            //            if (_rowsInSF <= 0) { _rowsInSF = 7; }
            //            for (int x = 1; x <= _rowsInSF; x++)
            //            {
            //                _Table += "<tr style='height:15px;'>";
            //                _Table += _fieldsBlank;
            //                _Table += "</tr>";
            //            }
            //            _Table += "</tbody>";

            //            _Table += "<tfooter>";
            //            _Table += "</tfooter>";



            //            _Table += "</table>";

            //            _divSubFile += _Table + "</div>";

            //            _htmlDesign.AppendLine(_divSubFile);
            //        }
            //        //END GRID

            //        _htmlDesign.AppendLine("</div>");
            //    }

            //    _htmlDesign.AppendLine("</body></html>");

            //    return _htmlDesign.ToString();

            //}

            public static string GetHTMLDesign(GxObject.WorkPanel _wpn, ImageList imageList1)
            {
                StringBuilder builder = new StringBuilder("<html><body>");
                if (_wpn.Form != null)
                {
                    int num = _wpn.Form.Block.Frame.right - _wpn.Form.Block.Frame.left;
                    int num2 = _wpn.Form.Block.Frame.bottom - _wpn.Form.Block.Frame.top;
                    int left = _wpn.Form.Block.Frame.left;
                    int top = _wpn.Form.Block.Frame.top;
                    string colorInfo = GxEditorHelper.GetColorInfo("Black");
                    string str2 = GxEditorHelper.GetColorInfo("Lime");
                    builder.AppendLine(string.Format("<div id='Label' style='position:absolute; left:{0}; top:{1}; width:{2}px; height:{3}px; display:block; background-color:{4}; color:{5}'><br><br>", new object[] { left, top, num, num2, colorInfo, str2 }));
                    if (_wpn.Form.Block != null)
                    {
                        foreach (GxObject.Text text in _wpn.Form.Block.Texts)
                        {
                            string str5 = string.Empty;
                            if (text.Font != null)
                            {
                                str5 = string.Format("font-family:{0}; font-size:{1}px; text-decoration:{2};", text.Font.Name, ((text.Font.Size == 0) ? 10 : text.Font.Size) + 1, (text.Font.Underline == "Y") ? "underline" : "none");
                            }
                            if (text.ColorInfo != null)
                            {
                                str5 = string.Format("{0} color:{1};", str5, GxEditorHelper.GetColorInfo(text.ColorInfo.Foreground));
                            }
                            string str6 = string.Format("<div style='position:absolute; left:{0}; top:{1}; width:{2}px; height:{3}px; display:block; {5}'>{4}</div>", new object[] { text.left, text.top, text.right - text.left, text.bottom - text.top, (text.Texto == null) ? "&nbsp;" : text.Texto.Replace(" ", "&nbsp;"), str5 });
                            _wpn.AddLayoutText(text.Texto);
                            builder.AppendLine(str6);
                        }
                        foreach (Edit edit in _wpn.Form.Block.Edits)
                        {
                            if (edit.right <= 0)
                            {
                                Variable variable = _wpn.getVarByName(edit.Name);
                                if (variable != null)
                                {
                                    edit.right = edit.left + (variable.Length * 8);
                                }
                            }

                            string str7 = string.Empty;
                            if (edit.Font != null)
                            {
                                str7 = string.Format("font-family:{0}; font-size:{1}px; text-decoration:{2}", edit.Font.Name, ((edit.Font.Size == 0) ? 10 : edit.Font.Size) + 1, (edit.Font.Underline == "Y") ? "underline" : "none");
                            }
                            if (edit.ColorInfo != null)
                            {
                                str7 = string.Format("{0} color:{1};", str7, GxEditorHelper.GetColorInfo(edit.ColorInfo.Foreground));
                            }
                            string name = edit.Name;
                            if ((edit.Row != null) && !string.IsNullOrEmpty(edit.Row))
                            {
                                name = string.Format("{0}({1})", edit.Name, edit.Row);
                            }
                            string str9 = string.Format("<div id='Text' style='position:absolute; left:{0}; top:{1}; width:{2}px; height:{3}px; display:block; border:0px solid #eee24; overflow:hidden; {5}' title='&amp;{4}'>&amp;{4}</div>", new object[] { edit.left, edit.top, edit.right - edit.left, edit.bottom - edit.top, name, str7 });
                            builder.AppendLine(str9);

                            _wpn.AddLayoutVars(edit.Name);
                        }
                    }

                    string format = "<span style='font-family:Courier New; font-size:11px; color:#00FF00;'>{0}</span>";
                    string str4 = "<div id='RowGrid' style='font-family:Courier New; font-size:11px; text-decoration:underline; color:#00FF00; display:block; overflow:hidden; width:{1}px;' title='{2}'>{0}</div>";
                    if (_wpn.Form.Block.Subfiles != null)
                    {
                        foreach (SubFile file in _wpn.Form.Block.Subfiles)
                        {
                            int num8 = file.bottom - file.top;
                            int num9 = (file.right - file.left) - 30;
                            string str10 = string.Format("<div id='Grid' style='position:absolute; font-size:9px;font-family:Courier New; left:{0}; top:{1}; width:{2}px; height:{3}px; display:block; overflow-y:visible; overflow-x:hidden; '>", new object[] { file.left, file.top, num9, num8 });
                            string str11 = "<table cellpadding='0' cellspacing='1'  id='" + file.CtrlName + "' style='border:0px solid #999999;' width='90%'; >";
                            string str12 = string.Empty;
                            str11 = str11 + "<theader>" + "<tr>";
                            if (file.Items != null) { 
                                foreach (Item item in file.Items)
                                {
                                    Variable variable = _wpn.getVarByName(item.Name);
                                    string str13 = string.Format(format, (item.Title == null) ? "&nbsp;" : item.Title.Replace(" ", "&nbsp;"));
                                    string str14 = string.Empty;
                                    if (variable != null)
                                    {
                                        str11 = str11 + string.Format("<td nowrap style='width:{0}px; border:0px solid #999999;'>", item.Width) + str13 + "</td>\n        ";
                                        int num12 = 8 * variable.Length;
                                        string str15 = variable.Name2;
                                        string title = variable.Title;
                                        if (item.Row != null)
                                        {
                                            if (item.Col == null)
                                            {
                                                str15 = string.Format("{0}({1})", variable.Name2, item.Row);
                                            }
                                            else
                                            {
                                                str15 = string.Format("{0}({1},{2})", variable.Name2, item.Row, item.Col);
                                            }
                                            title = title + " - " + str15;
                                        }
                                        int num13 = num12 - (variable.Name2.Length * 8);
                                        if (num13 > 0)
                                        {
                                            str15 = str15.Replace("&", "&amp;").PadRight(num13 / 8).Replace(" ", "&nbsp;");
                                        }
                                        str14 = string.Format(str4, str15, num12, title);
                                        str12 = str12 + "<td style='border:0px solid #999999;'>" + str14 + "</td>";

                                        _wpn.AddLayoutVars(variable.Name);
                                    }
                                    else
                                    {
                                        str11 = str11 + string.Format("<td nowrap style='width:{0}px; border:0px solid #999999;'>", item.Width) + str13 + "</td>\n        ";
                                        str12 = str12 + "<td style='border:0px solid #999999;'>&nbsp;</td>";
                                    }
                                }
                            }

                            str11 = (str11 + "</tr>") + "</theader>" + "<tbody>";
                            int num10 = num8 / 0x15;
                            if (num10 != (num10 * 20))
                            {
                                num10++;
                            }
                            if (num10 <= 0)
                            {
                                num10 = 7;
                            }
                            for (int i = 1; i <= num10; i++)
                            {
                                str11 = (str11 + "<tr style='height:15px;'>") + str12 + "</tr>";
                            }
                            str11 = (str11 + "</tbody>" + "<tfooter>") + "</tfooter>" + "</table>";
                            str10 = str10 + str11 + "</div>";
                            builder.AppendLine(str10);
                        }
                    }
                    builder.AppendLine("</div>");
                }
                builder.AppendLine("</body></html>");
                return builder.ToString();
            }


        }

        public static class WebPanel
        {
            public enum ObjWebFormElementControlType
            {
                Edit,
                Text,
                Grid,
                FreeStyleGrid,
                Image,
                WebComponent,
                UserControl
            }

            public enum ObjWebFormEditType
            {
                ComboBox = 0,
                RadioButton = 1,
                EditBox = 2,
                CheckBox = 3,
                ListBox = 6
            }

            public static void PrepareLayoutXML(GxObject.WebPanel _wpn)
            {
                try
                {
                    if (_wpn.HTMLForm == null)
                    {
                        XmlDocument _docXml = GxXML.GetDocument2(_wpn.XMLObj);
                        XmlNodeList _xmlForms = GxXML.ReadElements(_docXml, "HTMLForm");
                        foreach (XmlNode _f in _xmlForms)
                        {
                            GxObject.HTMLForm _form = GxXML.XMLNodeToObject<GxObject.HTMLForm>(_f);
                            _wpn.HTMLForm = _form;
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    _wpn.HTMLForm = null;
                }


            }

            private static string getHTmlControl_EditBox(HTMLATT pObjATT, GXObj _wpn) //string designHtmlAttributes, int rows, int cols=1, string cls="")
            {

                string designHtmlAttributes = pObjATT.Class ; //, GXObj _wpn
                int rows = pObjATT.Rows;
                int cols = 1;
                string cls = "";
                string _varname = "";
                string _varnameTitle = "";
                Variable _var = pObjATT.getVarFromObject(_wpn);
                if (_var != null)
                {
                    _varname = _var.Name2;
                    _varnameTitle = _var.Title;
                    cols = _var.Length;
                }

                StringBuilder htmlBuilder = new StringBuilder();
                

                if (_varname.ToUpper().StartsWith("&ZG1_"))
                {
                    htmlBuilder.Append("<input type=\"hidden\" Id=\"" + _varname + "\" value=\"" + _varname + "-" + _varnameTitle + "\" >");
                    _wpn.AddLayoutVars(_varname);
                    return htmlBuilder.ToString();
                }

                

                if (pObjATT.ReadOnly)
                {
                    //htmlBuilder.Append("&nbsp;" + _varname + "&nbsp");
                    htmlBuilder.Append("<label title=\"" + _varnameTitle  + "\">&nbsp;" + _varname  + "</label> ");
                    _wpn.AddLayoutText(_varnameTitle);
                    return htmlBuilder.ToString();
                }


                if (rows <= 1)
                {

                    htmlBuilder.Append("<input type=\"text\" value=\"" + _varname + "\" size=\"" + cols.ToString() + "\" maxlength=\"" + cols + "\"");
                    if (cls != "")
                    {
                        htmlBuilder.Append(" class=\"");
                        htmlBuilder.Append(cls);
                        htmlBuilder.Append("\"");
                    }

                    if (designHtmlAttributes != "")
                    {
                        htmlBuilder.Append(' ');
                        htmlBuilder.Append(designHtmlAttributes);
                    }
                    if (!String.IsNullOrEmpty(_varnameTitle))
                    {
                        htmlBuilder.Append(" alt='"+ _varnameTitle + "'");
                    }

                    htmlBuilder.Append(">");
                }
                else
                {
                    htmlBuilder.Append("<textarea cols=" + cols + " rows=" + rows);
                    if (cls != "")
                    {
                        htmlBuilder.Append(" class=\"");
                        htmlBuilder.Append(cls);
                        htmlBuilder.Append("\"");
                    }

                    if (designHtmlAttributes != "")
                    {
                        htmlBuilder.Append(' ');
                        htmlBuilder.Append(designHtmlAttributes);
                    }

                    htmlBuilder.Append("></textarea>");
                }

                return htmlBuilder.ToString();
            }
            private static string getHTmlControl_CheckBox(HTMLControl _control)
            {
                return "";
            }

            private static string getHTmlControl_RadioButton(HTMLControl _control)
            {
                return "";
            }

            private static string getHTmlControl_ComboBox(HTMLATT pObjATT, GXObj _wpn)  //string designHtmlAttributes, int rows, int cols = 1, string cls = "", bool isComboBox=true, string[] values=null)
            {

                string designHtmlAttributes = pObjATT.Class; 
                int rows = pObjATT.Rows;
                int cols = 1;
                string cls = "";
                bool isComboBox = true;
                string[] values = pObjATT.ControlValues;


                StringBuilder htmlBuilder = new StringBuilder();
                htmlBuilder.Append("<select size=1");
                if (values != null && values.Length > 0)
                {

                    if (cls != "")
                    {
                        htmlBuilder.Append(" class=\"");
                        htmlBuilder.Append(cls);
                        htmlBuilder.Append("\"");
                    }

                    if (designHtmlAttributes != "")
                    {
                        htmlBuilder.Append(' ');
                        htmlBuilder.Append(designHtmlAttributes);
                    }
                    htmlBuilder.Append(">");

                    for (int i = 0; i < values.Length; i++)
                    {
                        string[] _values2 = values[i].Split("@".ToCharArray());
                        htmlBuilder.Append("<option value=\"");
                        htmlBuilder.Append(_values2[1]);
                        htmlBuilder.Append("\">");
                        htmlBuilder.Append(_values2[0]);
                        htmlBuilder.Append("</option>");
                    }
                }
                else
                {
                    htmlBuilder.Append(">");
                    htmlBuilder.Append("<option value=\"");
                    Variable _var = pObjATT.getVarFromObject(_wpn);
                    if (_var != null)
                    {
                        htmlBuilder.Append(_var.Name2);
                        htmlBuilder.Append("\">");
                        htmlBuilder.Append(_var.Name2);

                    }
                    else
                    {
                        htmlBuilder.Append("\"> Seleccionar");
                    }
                    htmlBuilder.Append("</option>");
                }
                
                return htmlBuilder.ToString();
            }

            private static string getHTmlControl_Grid(HTMLSFL pObjFL, GXObj _wpn)
            {
                StringBuilder htmlBuilder = new StringBuilder();
                
                if (pObjFL.ClassName == null) { pObjFL.ClassName = "Grid"; }
                if (pObjFL.Border == null) { pObjFL.Border = "0"; }
                if (pObjFL.cellPadding == null) { pObjFL.cellPadding = "2"; }
                if (pObjFL.cellSpacing == null) { pObjFL.cellSpacing = "1"; }


                htmlBuilder.Append("<table");
                htmlBuilder.Append(" class=\"");
                htmlBuilder.Append(pObjFL.ClassName);

                htmlBuilder.Append("\"");
                htmlBuilder.Append(" border=");
                htmlBuilder.Append(pObjFL.Border);
                htmlBuilder.Append(" style=\"border-width: ");
                htmlBuilder.Append(pObjFL.Border);
                htmlBuilder.Append("\" cellpadding=");
                htmlBuilder.Append(pObjFL.cellPadding);
                htmlBuilder.Append(" cellspacing=");
                htmlBuilder.Append(pObjFL.cellSpacing);
                htmlBuilder.Append(" rules=\"all\"");
                
                htmlBuilder.Append('>');
                htmlBuilder.Append("<tr>");
                for (int i = 0; i < pObjFL.Columns.Count; i++)
                {
                    HColum _Hcol = pObjFL.Columns[i];
                    if (_Hcol.Visible)
                    {
                        htmlBuilder.Append("<th align=");
                        htmlBuilder.Append(_Hcol.GetDataTypeAlignString());
                        htmlBuilder.Append(" width=");
                        if (!_Hcol.ColAutoResize.Equals("0") )
                        {
                            if (_Hcol.ColTitle == null)
                            {
                                Variable _var = _Hcol.getVarFromObject(_wpn);
                                if (_var != null)
                                {
                                    _Hcol.ColTitle = _var.Title;
                                }
                            }
                            //CalculateDataSize(_Hcol.Data, out var _, out var cols);
                            _Hcol.ColWidth = (Math.Max(_Hcol.ColTitle.Length, 1) * 13 + 5).ToString();
                            if (_Hcol.ColWidth == "0"){_Hcol.ColWidth = "10";}
                            _wpn.AddLayoutVars(_Hcol.Name);
                            htmlBuilder.Append(_Hcol.ColWidth);
                        }
                        else
                        {
                            //if (_Hcol.ColWidth == "0" || _Hcol.ColWidth == "") { _Hcol.ColWidth = "10"; }
                            htmlBuilder.Append(_Hcol.ColWidth);
                        }

                        htmlBuilder.Append(" class=\"");
                        htmlBuilder.Append("GridTitle");
                        
                        
                        htmlBuilder.Append("\"");
                        htmlBuilder.Append(">");
                        htmlBuilder.Append(_Hcol.ColTitle);
                        htmlBuilder.Append("</th>");
                        _wpn.AddLayoutText(_Hcol.ColTitle);
                    }
                }

                htmlBuilder.Append("</tr>");
                int num = ((pObjFL.Rows != 0) ? pObjFL.Rows : 10);
                StringBuilder _FilaTab = new StringBuilder("");
                for (int j = 0; j < num; j++)
                {
                    if (_FilaTab.Length == 0) //Solo cargar una vez
                    {
                        _FilaTab.Append("<tr ");
                        string text;
                        if (pObjFL.Class == "")
                        {
                            text = "";
                        }
                        else
                        {
                            text = pObjFL.Class + ((j % 2 == 0) ? "Odd" : "Even");
                            _FilaTab.Append(" class=\"");
                            _FilaTab.Append(text);
                            _FilaTab.Append("\"");
                        }

                        _FilaTab.Append(">");
                        for (int k = 0; k < pObjFL.Columns.Count; k++)
                        {
                            HColum _Hcol = pObjFL.Columns[k];
                            if (_Hcol.Visible)
                            {
                                _FilaTab.Append("<td valign=top");
                                _FilaTab.Append(" align=");
                                _FilaTab.Append(_Hcol.GetDataTypeAlignString());
                                _FilaTab.Append(">");

                                Variable _var = _Hcol.getVarFromObject(_wpn);
                                if (_var != null)
                                {
                                    if (_Hcol.EditType == WebFormEditType.CheckBox)
                                    {
                                        _FilaTab.Append("<input type=\"checkbox\"" + (_Hcol.ColReadOnly != "0" ? " disabled" : "") + " title=\"" + _var.Name2 + "-" + _var.Title + "\">");
                                    }
                                    else
                                    {
                                        if (_Hcol.EditType == WebFormEditType.ComboBox)
                                        {
                                            _FilaTab.Append("<select size=1");
                                            if (_Hcol.ControlValues != null && _Hcol.ControlValues.Length > 0)
                                            {
                                                if (_Hcol.Class != "")
                                                {
                                                    _FilaTab.Append(" class=\"");
                                                    _FilaTab.Append(_Hcol.Class);
                                                    _FilaTab.Append("\"");
                                                }
                                                _FilaTab.Append(">");

                                                for (int i = 0; i < _Hcol.ControlValues.Length; i++)
                                                {
                                                    string[] _values2 = _Hcol.ControlValues[i].Split("@".ToCharArray());
                                                    _FilaTab.Append("<option value=\"");
                                                    _FilaTab.Append(_values2[1]);
                                                    _FilaTab.Append("\">");
                                                    _FilaTab.Append(_values2[0]);
                                                    _FilaTab.Append("</option>");
                                                }
                                            }
                                            else
                                            {
                                                _FilaTab.Append(">");
                                                _FilaTab.Append("<option value=\"");
                                                _FilaTab.Append(_var.Name2);
                                                _FilaTab.Append("\">");
                                                _FilaTab.Append(_var.Name2);
                                                _FilaTab.Append("</option>");
                                            }

                                        }
                                        else
                                        {

                                            if (_Hcol.ColReadOnly != "0")
                                            {
                                                if (_Hcol.ReturnOnClick)
                                                {
                                                    string _classEVEN2 = ((j % 2 == 0) ? "GridOdd" : "GridEven");
                                                    _FilaTab.Append($"<img src=\"images/returnbutton.gif\" alt=\"{_var.Name2}\" class=\"{_classEVEN2}\" prototypeextrabtn=\"close\" border=\"0\">");
                                                }
                                                else
                                                { _FilaTab.Append(_var.Name2); }
                                                
                                            }
                                            else
                                            {
                                                _FilaTab.Append("<input type=\"text\" value=\"" + _var.Name2 + "\" size=\"" + _var.Length + "\" >");
                                            }


                                        }
                                    }

                                    //htmlBuilder.Append("&nbsp;" + _var.Name2);
                                    //&nbsp;Col&nbsp;
                                }
                                else
                                {
                                    _FilaTab.Append("&nbsp;Col&nbsp;");
                                }

                                _FilaTab.Append("</td>");
                            }
                        }

                        _FilaTab.Append("</tr>");
                    }

                    string _classEVEN = ((j % 2 == 0) ? "GridOdd" : "GridEven");
                    
                    htmlBuilder.AppendLine(_FilaTab.ToString().Replace("GridOdd", _classEVEN));
                }

                htmlBuilder.Append("</table>");
                //htmlBuilder.Append("</div>");

                return htmlBuilder.ToString();
            }

            private static string getHTmlControl_WebComponent(string designHtmlAttributes, string pObjectName)
            {

                StringBuilder htmlBuilder = new StringBuilder();
                htmlBuilder.Append("<div style=\"background-color:ivory;border-style:solid;border-color:dimgray;border-width:1px;padding:2px\"");
                if (designHtmlAttributes != "")
                {
                    htmlBuilder.Append(' ');
                    htmlBuilder.Append(designHtmlAttributes);
                }

                htmlBuilder.Append(">WebComponent: ");
                htmlBuilder.Append(pObjectName);
                htmlBuilder.Append("</div>");


                return htmlBuilder.ToString();
            }

            

            private static string getHTmlControl(HTMLControl _control, GXObj _wpn)
            {
                string _tagElement = "";

                HtmlControlForm _objCtl = new HTMLATT();

                _objCtl.ControlIdTypeId = _control.ControlIdType;

                if (_control.InTag_HTMLBlock != null)
                {
                    foreach (var att in _control.InTag_HTMLBlock.HTMLAttriValue)
                    {
                        //_tagElement += $" {att.Attribute}=\"{att.Value.Replace("\"", String.Empty)}\"";

                        if (att.Type.Equals("ClassId"))
                        {
                            _objCtl.Classid = att.Value;
                            switch (att.Value)
                            {
                                case "clsid:080B347A-DAD3-45B4-8539-F9300E250CA6":
                                    _objCtl.TypeControl = ObjWebFormElementControlType.Grid;
                                    break;

                                case "clsid:6B3A78ED-A000-43E9-83D7-BB40CD011397":
                                    _objCtl.TypeControl = ObjWebFormElementControlType.WebComponent;
                                    break;

                                case "clsid:320A44BA-E4E2-4143-8831-E4A7D0D3A283":
                                    _objCtl.TypeControl = ObjWebFormElementControlType.Edit;
                                    break;
                            }
                        }

                        if (att.Type.Equals("class"))
                        {
                            _objCtl.ClassName = att.Value;
                        }


                    }
                }

                if (_control.Property != null)
                {
                    foreach (Property p in _control.Property)
                    {
                        switch (p.Name)
                        {
                            case "UserMode": _objCtl.UserMode = int.Parse(p.Value.ToString()); break;
                            case "ReadOnly": 
                                if (p.Value != null)
                                {
                                    _objCtl.ReadOnly = (p.Value.Equals("-1") || p.Value.Equals("1"));
                                }
                                break;

                            default:
                                break;
                        }   
                    }
                }


                //Cargamos las columnas, si tuviera
                if (_objCtl.TypeControl == ObjWebFormElementControlType.Grid)
                {
                    HTMLSFL _objH = new HTMLSFL(); //new HTMLSFL();
                    _objCtl.CloneProperties(_objH);

                    if (_control.ChildProperties != null)
                    {
                        foreach (ChildProperties _child in _control.ChildProperties)
                        {
                            HColum _col = new HColum();
                            if (_child.Property != null)
                            {
                                foreach (Property _p in _child.Property)
                                {
                                    switch (_p.Name)
                                    {
                                        case "Name": _col.Name = _p.Value; break;
                                        case "ColReadOnly": _col.ColReadOnly = _p.Value; break;
                                        case "ColWidth": _col.ColWidth = _p.Value; break;
                                        case "ControlType": _col.ControlType = _p.Value; break;
                                        case "ColTitle": _col.ColTitle = _p.Value; break;
                                        case "ColLinesColor": _col.ColLinesColor = _p.Value; break;
                                        case "Class": _col.Class = _p.Value; break;
                                        case "ColAutoResize": _col.ColAutoResize = _p.Value; break;
                                        case "ColAttId": _col.ColAttId = _p.Value; break;
                                        case "UserMode": _col.UserMode = _p.Value; break;
                                        case "ColVisible": _col.Visible = (_p.Value!="0") ; break;
                                        case "ReturnOnClick": _col.ReturnOnClick = (_p.Value != "0"); break;
                                        case "ControlValues": 
                                            if (_p.Value != null)
                                            {
                                                string _valores = _p.Value.ToString().Replace("\\t", "@").Replace("\\n", "|");
                                                _col.ControlValues = _valores.Split('|');
                                            }
                                            break;

                                        default:
                                            break;
                                    }
                                }
                            }
                            _objH.Columns.Add(_col);
                        }
                    }

                    _tagElement += getHTmlControl_Grid(_objH, _wpn);
                }

                if (_objCtl.TypeControl == ObjWebFormElementControlType.Edit)
                {
                    HTMLATT _objTxt = (HTMLATT)_objCtl;

                    foreach (Property p in _control.Property)
                    {
                        if (p.Value != null)
                        {
                            switch (p.Name)
                            {
                                case "BorderStyle": _objTxt.BorderStyle = p.Value; break;
                                case "AttID": _objTxt.AttID = p.Value; break;
                                case "Class": _objTxt.Class = p.Value.Replace("\"", String.Empty); break;
                                case "AutoResize": _objTxt.AutoResize = p.Value; break;
                                case "ControlType": _objTxt.ControlType = p.Value; break;
                                case "ControlValues":
                                    if (p.Value != null)
                                    {
                                        string _valores = p.Value.ToString().Replace("\\t", "@").Replace("\\n", "|");
                                        _objTxt.ControlValues = _valores.Split('|');
                                    }
                                    break;
                                case "GxFormat":
                                    _objTxt.GxFormat = p.Value; break;

                                default:
                                    break;
                            }
                        }
                    }

                    //Variable _var = _objTxt.getVarFromObject(_wpn);
                    //Console.WriteLine($"_varname:{_var.Name2} -> EditType:{_objTxt.EditType} - AttID:{_objTxt.AttID} - Visible:{_objTxt.Visible} - ReadOnly:{_objTxt.ReadOnly} - ControlType:{_objTxt.ControlType}");

                    if (_objTxt.ControlValues.Length > 0 || (_objTxt.EditType == WebFormEditType.ComboBox || _objTxt.EditType == WebFormEditType.ListBox))
                    {
                        _tagElement += getHTmlControl_ComboBox(_objTxt, _wpn);
                    }
                    else
                    {
                        _tagElement += getHTmlControl_EditBox(_objTxt, _wpn);
                    }

                }
  



                return _tagElement;
            }
            public static string GetHTMLDesign(GxObject.WebPanel _wpn, ImageList imageList1)
            {

                StringBuilder _htmlDesign = new StringBuilder("");
                _htmlDesign.AppendLine("<html>");
                _htmlDesign.AppendLine("<head>");
                _htmlDesign.AppendLine($"<base href=\"file://{ System.Windows.Forms.Application.StartupPath}\\WebUI\\FormFiles\\\">");
                _htmlDesign.AppendLine(" <meta http-equiv=\"Designer\" content=\"Dlya.Basic:1.0:WebUI\"/>");
                _htmlDesign.AppendLine(" <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"/>");
                _htmlDesign.AppendLine($" <title>{_wpn.Info.Description}</title>");
                _htmlDesign.AppendLine(" <link rel=\"stylesheet\" type=\"text/css\" href=\"styles.css\"/>");
                _htmlDesign.AppendLine(" <link rel=\"stylesheet\" type=\"text/css\" href=\"DescartesTheme.css\"/>");
                _htmlDesign.AppendLine(" <link rel=\"stylesheet\" type=\"text/css\" href=\"theme.css\"/>");
                _htmlDesign.AppendLine(" <script language=\"javascript\" src=\"theme.js\"></script>");
                _htmlDesign.AppendLine("</head>");
                _htmlDesign.AppendLine($"<body class=\"FormBase\" >");

                //STATIC TEXTS
                if (_wpn.HTMLForm != null)
                {
                    _htmlDesign.AppendLine($"<form id=\"MAINFORM\" {_wpn.HTMLForm.ElemAtts.Property.Name}='{_wpn.HTMLForm.ElemAtts.Property.Value}'  method=\"POST\" ACTION=\"\">");

                    if (_wpn.HTMLForm.HTMLBlock != null)
                    {
                        foreach (HtmlTagForm _t in _wpn.HTMLForm.HTMLBlock.getAllTags())
                        {
                            string _tagElement = String.Empty;
                            if (_t.GetType() == typeof(HTMLSpan))
                            {
                                HTMLSpan _span = _t as HTMLSpan;
                                _tagElement = $"<{_span.Name}";
                                if (_span.InTag_HTMLBlock != null)
                                {
                                    if (_span.InTag_HTMLBlock.HTMLAttriValue != null)
                                    {
                                        foreach (var att in _span.InTag_HTMLBlock.HTMLAttriValue)
                                        {
                                            _tagElement += $" {att.Attribute}=\"{att.Value.Replace("\"", String.Empty)}\"";
                                        }
                                    }
                                    _tagElement += ">";
                                    if (_span.InText_HTMLBlock.HTMLText != null)
                                    {
                                        if (_span.InText_HTMLBlock.HTMLText.Source != null)
                                        {
                                            if (!_span.InText_HTMLBlock.HTMLText.Source.Contains("(Cambio en lista de valores)") &&
                                                !_span.InText_HTMLBlock.HTMLText.Source.Contains("(prompt-operation al servidor)") &&
                                                !_span.InText_HTMLBlock.HTMLText.Source.Contains("(Empty event)  ")
                                                )
                                            {
                                                _tagElement += $" {_span.InText_HTMLBlock.HTMLText.Source}";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    _tagElement += ">";
                                }
                                if (_span.HTMLTag != null)
                                {
                                    _tagElement += $"<{_span.HTMLTag.Name}>";
                                }
                                _htmlDesign.Append(_tagElement);
                            }

                            /*Para los Text Plain*/
                            if (_t.GetType() == typeof(HTMLText))
                            {
                                HTMLText _text = _t as HTMLText;
                                _tagElement = $"{_text.Source}";

                                _htmlDesign.Append(_tagElement);
                            }

                            /*Para los TAGS*/
                            if (_t.GetType() == typeof(HTMLTag))
                            {
                                HTMLTag _tag = _t as HTMLTag;
                                _tagElement = $"<{_tag.Name}";

                                if (_tag.InTag_HTMLBlock != null)
                                {
                                    if (_tag.InTag_HTMLBlock.HTMLAttriValue != null)
                                    {
                                        foreach (var att in _tag.InTag_HTMLBlock.HTMLAttriValue)
                                        {
                                            if (att.Attribute.ToUpper().Equals("XWIDTH"))
                                            {
                                                _tagElement += $" style=\"width:{att.Value.Replace("\"", String.Empty)}\"";
                                            }
                                            else
                                            {
                                                _tagElement += $" {att.Attribute}=\"{att.Value.Replace("\"", String.Empty)}\"";
                                            }

                                        }
                                    }
                                    _tagElement += ">";
                                    if (_tag.InText_HTMLBlock.HTMLText != null)
                                    {
                                        if (_tag.InText_HTMLBlock.HTMLText.Source != null)
                                        {
                                            _tagElement += $" {_tag.InText_HTMLBlock.HTMLText.Source}";
                                        }
                                    }
                                }
                                else
                                {
                                    _tagElement += ">";
                                }


                                if (_tag.Type != null && _tag.Type.ToLower().Equals("close"))
                                {
                                    _htmlDesign.Append(_tagElement);
                                }
                                else
                                {
                                    _htmlDesign.Append(_tagElement);
                                }


                            }

                            /*Para Controls*/
                            if (_t.GetType() == typeof(HTMLControl))
                            {
                                _tagElement = getHTmlControl(_t as HTMLControl, _wpn);

                                _htmlDesign.Append(_tagElement);

                            }

                        }
                    }


                    _htmlDesign.Append("</form>");

                }
                else
                {
                    if (!String.IsNullOrEmpty( _wpn.strHTMLForm)) //2026-06-06 Para Gx16 y Gx18
                    {
                        _htmlDesign.AppendLine(HttpUtility.HtmlDecode(_wpn.strHTMLForm) );
                    }
                }

                _htmlDesign.Append("</body>");
                _htmlDesign.Append("</html>");

                return _htmlDesign.ToString();
            }

        }


        public static class Procedure
        {

            public static void PrepareLayoutXML(GxObject.Procedure _prc)
            {
                XmlDocument _docXml = GxXML.GetDocument2(_prc.XMLObj);
                XmlNodeList _xmlvariables = GxXML.ReadElements(_docXml, "Layout");
                foreach (XmlNode _f in _xmlvariables)
                {
                    GxObject.Layout _form = GxXML.XMLNodeToObject<GxObject.Layout>(_f);
                    _prc.Layout = _form;
                }

            }

            public static string GetHTMLDesign(GxObject.Procedure _prc, ImageList imageList1)
            {

                if (_prc.Layout == null)
                {
                    return String.Empty;
                }
                if (_prc.Layout.Blocks == null)
                {
                    return String.Empty;
                }

                StringBuilder _htmlBlocks = new StringBuilder();
                List<string> _printBlockAdds = new List<string>();
                //STATIC TEXTS
                foreach (Block _blk in _prc.Layout.Blocks)
                {
                    PrintBlock _pblk = _blk.PrintBlock;

                    if (_pblk != null && !_printBlockAdds.Contains(_pblk.CtrlName))
                    {
                        _printBlockAdds.Add(_pblk.CtrlName);

                        StringBuilder _edits = new StringBuilder();
                        //FIELDS

                        if (_blk.Edits != null)
                        {
                            foreach (GxObject.Text _text in _blk.Texts)
                            {
                                string _stylefont2 = String.Empty;
                                if (_text.Font != null)
                                {
                                    _stylefont2 = String.Format("font-family:{0}; font-size:{1}px; text-decoration:{2}", _text.Font.Name, (_text.Font.Size == 0 ? 10 : _text.Font.Size) + 1, (_text.Font.Underline == "Y" ? "underline" : "none"));
                                }
                                else
                                {
                                    _stylefont2 = "font-family:Courier New; font-size:10px;";
                                }

                                string _divEdit = String.Format("<div id='Label' class='TextAndVar' style='left:{0}; top:{1}; width:{2}px; height:{3}px; {5}'>{4}</div>", _text.left, _text.top, (_text.right - _text.left), (_text.bottom - _text.top), _text.Texto, _stylefont2);
                                _prc.AddLayoutText(_text.Texto);
                                _edits.AppendLine(_divEdit);
                            }


                            foreach (Edit _edit in _blk.Edits)
                            {
                                if (_edit.right <= 0)
                                {
                                    Variable variable = _prc.getVarByName(_edit.Name);
                                    if (variable != null)
                                    {
                                        _edit.right = _edit.left + (variable.Length * 8);
                                    }
                                }

                                string _stylefont2 = String.Empty;
                                if (_edit.Font != null)
                                {
                                    string _tamanio = String.Empty;
                                    _stylefont2 = String.Format("font-family:{0}; font-size:{1}px; text-decoration:{2}; {3}", _edit.Font.Name, (_edit.Font.Size == 0 ? 10 : _edit.Font.Size) + 1, (_edit.Font.Underline == "Y" ? "underline" : "none"), _tamanio);
                                }
                                else
                                {
                                    _stylefont2 = "font-family:Courier New; font-size:10px;";
                                }

                                string _strEVName = _edit.Name;
                                if (_edit.Row != null)
                                {
                                    if (!String.IsNullOrEmpty(_edit.Row))
                                    {
                                        if (_edit.Col != null)
                                        {
                                            _strEVName = String.Format("{0}({1},{2})", _edit.Name, _edit.Row, _edit.Col);
                                        }
                                        else
                                        {
                                            _strEVName = String.Format("{0}({1})", _edit.Name, _edit.Row);
                                        }
                                    }

                                }

                                string _divEdit = String.Format("<div id='Text' class='TextAndVar' style='left:{0}; top:{1}; width:{2}px; height:{3}px; {5}' title='&amp;{4}'>&amp;{4}</div>", _edit.left, _edit.top, (_edit.right - _edit.left), (_edit.bottom - _edit.top), _strEVName, _stylefont2);
                                _prc.AddLayoutVars(_edit.Name);
                                _edits.AppendLine(_divEdit);
                            }

                        }

                        //string _divPBLock = String.Format("<div style='position:absolute; border:1px solid; left:{0}; top:{1}; width:{2}px; height:{4}px; display:block;'>{3}</div>", _pblk.left, _pblk.top, (_pblk.right - _pblk.left), _edits.ToString(), (_pblk.bottom - _pblk.top));
                        int _Width = (_pblk.right - _pblk.left);
                        int _Height = (_pblk.bottom - _pblk.top);

                        string _divPBLock = ""; //String.Format("<div style='position:absolute; border:1px solid; width:{0}px; height:{2}px; display:block;'>\n{1}        </div>", (_pblk.right - _pblk.left), _edits.ToString(), (_pblk.bottom - _pblk.top));

                        _divPBLock = CSS_HTML.Block_Layout_HTML(_pblk.CtrlName, _Width, _Height, _edits.ToString());


                        //_htmlDesign.AppendLine(String.Format(_formatBlock, _divLabelBLock, _divPBLock));

                        _htmlBlocks.AppendLine(_divPBLock);

                        //_htmlDesign.AppendLine(_divLabelBLock);
                        //_htmlDesign.AppendLine(_divPBLock);
                    }
                }

                string _Style = CSS.CSS_Style_Layout_Procedure;
                string _body = String.Format(CSS_HTML.Body_Table_Format, _htmlBlocks.ToString());
                string _footer = String.Empty;
                string _HTML = String.Format(CSS_HTML.Page_Layout_Format, _Style, _body, _footer); //String.Format("<head>\n<style>{0}\n</style>\n</head>", CSS.CSS_Style_Layout_Procedure);


                //_htmlDesign.AppendLine("</body>\n</html>");

                return _HTML; //_htmlDesign.ToString();

            }


        }

        public static class Report
        {

            public static void PrepareLayoutXML(GxObject.Report _prc)
            {
                XmlDocument _docXml = GxXML.GetDocument2(_prc.XMLObj);
                XmlNodeList _xmlvariables = GxXML.ReadElements(_docXml, "Layout");
                foreach (XmlNode _f in _xmlvariables)
                {
                    GxObject.Layout _form = GxXML.XMLNodeToObject<GxObject.Layout>(_f);
                    _prc.Layout = _form;
                }

            }

            public static string GetHTMLDesign(GxObject.Report _rpt, ImageList imageList1)
            {

                StringBuilder _htmlDesign = new StringBuilder("<html><body><br><br>");


                //STATIC TEXTS
                if (_rpt.Layout != null)
                {
                    if (_rpt.Layout.Blocks != null)
                    {
                        List<string> _printBlockAdds = new List<string>();
                        foreach (Block _blk in _rpt.Layout.Blocks)
                        {
                            PrintBlock _pblk = _blk.PrintBlock;

                            if (_pblk != null &&  !_printBlockAdds.Contains(_pblk.CtrlName))
                            {
                                _printBlockAdds.Add(_pblk.CtrlName);

                                string _divLabelBLock = String.Format("<div id='LabelBlock' style='position:absolute; font-size:10px;font-family:Arial; left:0px; top:{0}; display:block; '>{1}</div>", _pblk.top, _pblk.CtrlName);

                                StringBuilder _edits = new StringBuilder();

                                //FIELDS
                                if (_blk.Edits != null)
                                {
                                    foreach (GxObject.Text _text in _blk.Texts)
                                    {
                                        string _stylefont2 = String.Empty;
                                        if (_text.Font != null)
                                        {
                                            _stylefont2 = String.Format("font-family:{0}; font-size:{1}px; text-decoration:{2}", _text.Font.Name, (_text.Font.Size == 0 ? 10 : _text.Font.Size) + 1, (_text.Font.Underline == "Y" ? "underline" : "none"));
                                        }
                                        else
                                        {
                                            _stylefont2 = "font-family:Courier New; font-size:10px;";
                                        }

                                        string _divEdit = String.Format("<div id='Label' style='position:absolute; left:{0}; top:{1}; width:{2}px; height:{3}px; display:block; border:0px solid #eee24; overflow:hidden; {5}'>{4}</div>", _text.left, _text.top, (_text.right - _text.left), (_text.bottom - _text.top), _text.Texto, _stylefont2);
                                        _rpt.AddLayoutText(_text.Texto);
                                        _edits.AppendLine(_divEdit);
                                    }


                                    foreach (Edit _edit in _blk.Edits)
                                    {
                                        string _stylefont2 = String.Empty;
                                        if (_edit.Font != null)
                                        {
                                            _stylefont2 = String.Format("font-family:{0}; font-size:{1}px; text-decoration:{2}", _edit.Font.Name, (_edit.Font.Size == 0 ? 10 : _edit.Font.Size) + 1, (_edit.Font.Underline == "Y" ? "underline" : "none"));
                                        }
                                        else
                                        {
                                            _stylefont2 = "font-family:Courier New; font-size:10px;";
                                        }


                                        string _strEVName = _edit.Name;
                                        if (_edit.Row != null)
                                        {
                                            if (!String.IsNullOrEmpty(_edit.Row))
                                            {
                                                if (_edit.Col != null)
                                                {
                                                    _strEVName = String.Format("{0}({1},{2})", _edit.Name, _edit.Row, _edit.Col);
                                                }
                                                else
                                                {
                                                    _strEVName = String.Format("{0}({1})", _edit.Name, _edit.Row);
                                                }
                                            }
                                        }

                                        string _divEdit = String.Format("<div id='Text' style='position:absolute;left:{0}; top:{1}; width:{2}px; height:{3}px; display:block; border:0px solid #eee24; {5}' title='&amp;{4}'>&amp;{4}</div>", _edit.left, _edit.top, (_edit.right - _edit.left), (_edit.bottom - _edit.top), _strEVName, _stylefont2);
                                        _rpt.AddLayoutVars(_edit.Name);
                                        _edits.AppendLine(_divEdit);
                                    }


                                }
                                string _divPBLock = String.Format("<div id='Block' style='position:absolute; border:1px solid; left:{0}; top:{1}; width:{2}px; height:{4}px; display:block;'>{3}</div>", _pblk.left, _pblk.top, (_pblk.right - _pblk.left), _edits.ToString(), (_pblk.bottom - _pblk.top));

                                _htmlDesign.AppendLine(_divLabelBLock);
                                _htmlDesign.AppendLine(_divPBLock);
                            }
                        }
                    }
                }

                _htmlDesign.AppendLine("</body></html>");

                return _htmlDesign.ToString();
            }

        }

        public static string GetHTMLDocumentation(string _html)
        {
        
            StringBuilder _htmlDesign = new StringBuilder("<html><body>");
            string _divLabelBLock = String.Format("<div style='font-size:10px;font-family:Consolas; display:inline-block;white-space: nowrap; '>{0}</div>", _html);
            _htmlDesign.AppendLine(_divLabelBLock);
            _htmlDesign.AppendLine("</body></html>");
            return _htmlDesign.ToString();
        }
    }
}
