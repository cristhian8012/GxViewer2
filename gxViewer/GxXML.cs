using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using gxViewer.GxObject;
using System.Runtime.Remoting;
using System.IO;

namespace gxViewer
{
    public class GxXML
    {
        //private static XmlDocument _xml;

        public static XmlDocument GetDocument(string filename)
        {
            string _xmlstr = System.IO.File.ReadAllText(filename, Encoding.Default);
            XmlDocument _xml = new XmlDocument();
            _xml.LoadXml(_xmlstr);
            return _xml;
        }
        public static XmlDocument GetDocument2(string xmlstring)
        {
            XmlDocument _xml = new XmlDocument();
            _xml.LoadXml(xmlstring);
            return _xml;
        }

        public static XmlNode ReadElement(XmlDocument _xml, string TagName)
        {
            XmlNodeList _nodos = _xml.GetElementsByTagName(TagName);
            return _nodos[0];
        }

        public static XmlNode ReadElementSingleNode(XmlDocument _xml, string xPath)
        {
            return _xml.SelectSingleNode(xPath);
        }

        public static XmlNodeList ReadElements(XmlNode _xmlObject, string TagName)
        {
            XmlNodeList _nodos = _xmlObject.OwnerDocument.GetElementsByTagName(TagName);
            return _nodos;
        }

        public static XmlNodeList ReadElementsNode(XmlNode _xmlObject, string TagName)
        {
            XmlNodeList _nodos = _xmlObject.OwnerDocument.GetElementsByTagName(TagName);
            return _nodos;
        }

        public static XmlNodeList ReadElements(XmlDocument _xmldoc, string TagName)
        {
            XmlNodeList _nodos = _xmldoc.GetElementsByTagName(TagName);
            return _nodos;
        }


        public static object XMLNodeToObject(XmlNode _xmlObject)
        {
            //T _objret = Activator.CreateInstance<T>();
            object _objret = Activator.CreateInstance(System.Type.GetType(String.Format("gxViewer.GxObject.{0}", _xmlObject.Name.Replace("Ver8", String.Empty))));

            PropertyInfo[] _props = _objret.GetType().GetProperties();
            Dictionary<string, int> _namesProp = new Dictionary<string, int>();


            for (int _cont = 0; _cont <= _props.Length - 1; _cont++)
            {
                PropertyInfo p = _props[_cont];
                _namesProp.Add(p.Name, _cont);
            }

            foreach (XmlNode nodo in _xmlObject.ChildNodes)
            {
                if (_namesProp.ContainsKey(nodo.Name))
                {
                    PropertyInfo p = _props[_namesProp[nodo.Name]];
                    //Console.WriteLine(p.Name);

                    switch (nodo.Name)
                    {
                        case "Property":
                            GxObject.Property _valuep = XMLNodeToObject<GxObject.Property>(nodo);
                            if (p.PropertyType.IsGenericType)
                            {
                                //p.SetValue(_objret, _valuep, null);
                                SetValueList<Property>(p, _objret, _valuep);
                            }
                            else
                            {
                                p.SetValue(_objret, _valuep, null);
                            }
                            
                            break;

                        case "Documentation":
                            GxObject.Documentation _valued = XMLNodeToObject<GxObject.Documentation>(nodo);
                            p.SetValue(_objret, _valued, null);
                            break;

                        case "Help":
                            GxObject.Help _valueh = XMLNodeToObject<GxObject.Help>(nodo);
                            p.SetValue(_objret, _valueh, null);
                            break;

                        case "Info":
                            GxObject.Info _valuei = XMLNodeToObject<GxObject.Info>(nodo);
                            p.SetValue(_objret, _valuei, null);
                            break;

                        case "ObjInfo":
                            GxObject.ObjInfo _valueoi = XMLNodeToObject<GxObject.ObjInfo>(nodo);
                            p.SetValue(_objret, _valueoi, null);
                            break;

                        case "Structure":
                            GxObject.Structure _valueSTR = XMLNodeToObject<GxObject.Structure>(nodo);
                            p.SetValue(_objret, _valueSTR, null);
                            break;

                        case "Layout":
                            GxObject.Layout _valueLy = (GxObject.Layout)XMLNodeToObject(nodo);
                            
                            /*LAYOUT contiene BLOCKS*/
                            XmlDocument _docXmlBlock = GxXML.GetDocument2(nodo.OuterXml);
                            XmlNodeList _xmlblocks = GxXML.ReadElements(_docXmlBlock, "Block");
                            List<GxObject.Block> _blocks = new List<GxObject.Block>();
                            foreach (XmlNode _vB in _xmlblocks)
                            {
                                GxObject.Block _Block = (GxObject.Block)GxXML.XMLNodeToObject(_vB);
                                //Obtenemos todos los Texts y Edits
                                SetBlocksEdits(_vB, ref _Block);
                                //
                                _blocks.Add(_Block);
                            }
                            _valueLy.Blocks = _blocks.ToArray();
                            p.SetValue(_objret, _valueLy, null);


                            break;

                        case "FormInfo":
                            GxObject.FormInfo _valueFI = (GxObject.FormInfo)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valueFI, null);
                            break;

                        case "ColorInfo":
                            GxObject.ColorInfo _valueColorInfo = (GxObject.ColorInfo)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valueColorInfo, null);
                            break;

                        case "Paper":
                            GxObject.Paper _valuePap = (GxObject.Paper)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valuePap, null);
                            break;

                        case "Att_Font":
                            GxObject.Att_Font _valueAttF = (GxObject.Att_Font)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valueAttF, null);
                            break;

                        case "Txt_Font":
                            GxObject.Txt_Font _valuetxtF = (GxObject.Txt_Font)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valuetxtF, null);
                            break;

                        case "Font":
                            GxObject.Font _valueFont = (GxObject.Font)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valueFont, null);
                            break;

                        case "Form":
                            GxObject.Form _valueForm = (GxObject.Form)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valueForm, null);
                            break;

                        case "Frame":
                            GxObject.Frame _valueFrame = (GxObject.Frame)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valueFrame, null);
                            break;

                        case "SubFile":
                            GxObject.SubFile _valueSubFile = (GxObject.SubFile)XMLNodeToObject(nodo);

                            XmlDocument _docXmlItems = GxXML.GetDocument2(nodo.OuterXml);
                            XmlNodeList _xmlItems = GxXML.ReadElements(_docXmlItems, "Item");
                            List<GxObject.Item> _Items = new List<GxObject.Item>();
                            foreach (XmlNode _xItem in _xmlItems)
                            {
                                GxObject.Item _Item = (GxObject.Item)GxXML.XMLNodeToObject(_xItem);
                                _Items.Add(_Item);
                            }
                            _valueSubFile.Items = _Items.ToArray();

                            p.SetValue(_objret, _valueSubFile, null);
                            break;


                        case "Block":
                            GxObject.Block _valueBlock = (GxObject.Block)XMLNodeToObject(nodo);
                            if (p.GetValue(_objret,null) == null)
                            {
                            //Obtenemos todos los Texts y Edits
                            SetBlocksEdits(nodo, ref _valueBlock);
                            //
                            
                                p.SetValue(_objret, _valueBlock, null);
                            }
                            break;

                        case "CodeBlock":
                        case "CodeBlockVer8":
                            GxObject.CodeBlock _valueCodeBlock = (GxObject.CodeBlock)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valueCodeBlock, null);
                            break;

                        case "PrintBlock":
                        case "PrintBlockVer8":
                            GxObject.PrintBlock _valuePrintBlock = (GxObject.PrintBlock)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valuePrintBlock, null);
                            break;

                        case "DataSource":
                            GxObject.DataSource _valueDataSource = (GxObject.DataSource)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valueDataSource, null);
                            break;

                        case "SDTXml":
                            GxObject.SDTXml _xmlSource = new SDTXml();
                            _xmlSource.Source = nodo.InnerText;
                            p.SetValue(_objret, _xmlSource, null);
                            break;

                        /*Agregarndo propiedades para el diseño del WebPanel*/
                        case "HTMLForm":
                            GxObject.HTMLForm _htmlForm = (GxObject.HTMLForm)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _htmlForm, null);
                            break;
                        case "HTMLBlock":
                            GxObject.HTMLBlock _htmlBlock = (GxObject.HTMLBlock)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _htmlBlock, null);
                            break;
                        case "HTMLSpan":
                            GxObject.HTMLSpan _htmlSpan = (GxObject.HTMLSpan)XMLNodeToObject(nodo);
                            SetValueList<HTMLSpan>(p, _objret, _htmlSpan);
                            break;
                        case "HTMLText":
                            GxObject.HTMLText _htmltext = (GxObject.HTMLText)XMLNodeToObject(nodo);
                            //p.SetValue(_objret, _htmltext, null);
                            SetValueList<HTMLText>(p, _objret, _htmltext);
                            break;
                        case "HTMLControl":
                            GxObject.HTMLControl _htmlControl = (GxObject.HTMLControl)XMLNodeToObject(nodo);
                            //p.SetValue(_objret, _htmlControl, null);
                            SetValueList<HTMLControl>(p, _objret, _htmlControl);
                            break;
                        case "InTag_HTMLBlock":
                            GxObject.InTag_HTMLBlock _Intaghtmlblock = (GxObject.InTag_HTMLBlock)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _Intaghtmlblock, null);
                            break;
                        case "InText_HTMLBlock":
                            GxObject.InText_HTMLBlock _InTextHTMLBlock = (GxObject.InText_HTMLBlock)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _InTextHTMLBlock, null);
                            break;

                        case "HTMLAttriValue":
                            GxObject.HTMLAttriValue _htmlAttrVal = (GxObject.HTMLAttriValue)XMLNodeToObject(nodo);
                            //p.SetValue(_objret, _htmlAttrVal, null);
                            SetValueList<HTMLAttriValue>(p, _objret, _htmlAttrVal);
                            break;
                        case "HTMLTag":
                            GxObject.HTMLTag _htmlTag = (GxObject.HTMLTag)XMLNodeToObject(nodo);
                            if (p.PropertyType.IsGenericType)
                            {
                                SetValueList<HTMLTag>(p, _objret, _htmlTag);
                            }
                            else
                            {
                                p.SetValue(_objret, _htmlTag, null);
                            }
                            break;
                        case "ElemAtts":
                            GxObject.ElemAtts _elemAtts = (GxObject.ElemAtts)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _elemAtts, null);
                            break;
                        case "ChildProperties":
                            GxObject.ChildProperties _childProp = (GxObject.ChildProperties)XMLNodeToObject(nodo);
                            if (p.PropertyType.IsGenericType)
                            {
                                SetValueList<ChildProperties>(p, _objret, _childProp);
                            }
                            else
                            {
                                p.SetValue(_objret, _childProp, null);
                            }
                            break;

                        default:
                           if (nodo.InnerText != "")
                            {
                                object _valueo = Convert.ChangeType(nodo.InnerText, p.PropertyType, null);
                                p.SetValue(_objret, _valueo, null);
                            }
                            break;
                    }
                   
                }

            }

            //XmlNodeList _nodos = _xmlObject.OwnerDocument.GetElementsByTagName(TagName);
            return _objret;
        }

        private static void SetValueList<T>(PropertyInfo p, object _objret, T newValue )
        {
            object pList = p.GetValue(_objret, null);
            if (pList == null)
            {
                pList = new List<T>();
            }
            ((List<T>)pList).Add(newValue);

            if (_objret.GetType() == typeof(HTMLBlock) )
            {
                ((HTMLBlock)_objret).AddItemOrder(newValue);
            }

            if (_objret.GetType() == typeof(InTag_HTMLBlock))
            {
                ((InTag_HTMLBlock)_objret).AddItemOrder(newValue);
            }

            if (p.PropertyType.IsGenericType)
            {
                p.SetValue(_objret, (List<T>)pList, null);
            }
            else
            {
                p.SetValue(_objret, newValue, null);
            }

        }


        public static T XMLNodeToObject<T>(XmlNode _xmlObject)
        {
            T _objret = Activator.CreateInstance<T>();
            PropertyInfo[] _props = _objret.GetType().GetProperties();
            Dictionary<string,int> _namesProp = new Dictionary<string,int>();

            
            for(int _cont=0;_cont<=_props.Length-1;_cont++)
            {
                PropertyInfo p = _props[_cont];
                _namesProp.Add(p.Name,_cont);
            }

            foreach (XmlNode nodo in _xmlObject.ChildNodes)
            {
                if (_namesProp.ContainsKey(nodo.Name))
                {
                    PropertyInfo p = _props[_namesProp[nodo.Name]];

                    switch (nodo.Name)
                    {
                        case "Property":
                            GxObject.Property _valuep = XMLNodeToObject<GxObject.Property>(nodo);
                            p.SetValue(_objret, _valuep, null);
                            break;

                        case "Attribute":
                            GxObject.Attribute _valued = XMLNodeToObject<GxObject.Attribute>(nodo);
                            p.SetValue(_objret, _valued, null);
                            break;

                        case "AttInfo":
                            GxObject.AttInfo _valueh = XMLNodeToObject<GxObject.AttInfo>(nodo);
                            p.SetValue(_objret, _valueh, null);
                            break;

                        case "AttDoc":
                            GxObject.AttDoc _valuei = XMLNodeToObject<GxObject.AttDoc>(nodo);
                            p.SetValue(_objret, _valuei, null);
                            break;

                        case "Help":
                            GxObject.Help _valuej = XMLNodeToObject<GxObject.Help>(nodo);
                            p.SetValue(_objret, _valuej, null);
                            break;

                        case "Structure":
                            GxObject.Structure _valueSTR = XMLNodeToObject<GxObject.Structure>(nodo);
                            p.SetValue(_objret, _valueSTR, null);
                            break;

                        case "DataSource":
                            GxObject.DataSource _valueDataSource = (GxObject.DataSource)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valueDataSource, null);
                            break;

                        case "ObjInfo":
                            GxObject.ObjInfo _valueoi = XMLNodeToObject<GxObject.ObjInfo>(nodo);
                            p.SetValue(_objret, _valueoi, null);
                            break;

                        case "Font":
                            GxObject.Font _valueFont = (GxObject.Font)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valueFont, null);
                            break;

                        case "Form":
                            GxObject.Form _valueForm = (GxObject.Form)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valueForm, null);
                            break;
                        case "FormInfo":
                            GxObject.FormInfo _valueForminfo = (GxObject.FormInfo)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valueForminfo, null);
                            break;

                        case "Frame":
                            GxObject.Frame _valueFrame = (GxObject.Frame)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valueFrame, null);
                            break;

                        case "SubFile":
                            GxObject.SubFile _valueSubFile = (GxObject.SubFile)XMLNodeToObject(nodo);

                            XmlDocument _docXmlItems = GxXML.GetDocument2(nodo.OuterXml);
                            XmlNodeList _xmlItems = GxXML.ReadElements(_docXmlItems, "Item");
                            List<GxObject.Item> _Items = new List<GxObject.Item>();
                            foreach (XmlNode _xItem in _xmlItems)
                            {
                                GxObject.Item _Item = (GxObject.Item)GxXML.XMLNodeToObject(_xItem);
                                _Items.Add(_Item);
                            }
                            _valueSubFile.Items = _Items.ToArray();

                            p.SetValue(_objret, _valueSubFile, null);
                            break;


                        case "Block":
                        case "BlockVer8":
                            GxObject.Block _valueBlock = (GxObject.Block)XMLNodeToObject(nodo);

                            //Obtenemos todos los Texts y Edits
                            SetBlocksEdits(nodo, ref _valueBlock);
                            //

                            p.SetValue(_objret, _valueBlock, null);
                            break;

                        case "CodeBlock":
                        case "CodeBlockVer8":
                            GxObject.CodeBlock _valueCodeBlock = (GxObject.CodeBlock)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valueCodeBlock, null);
                            break;

                        case "PrintBlock":
                        case "PrintBlockVer8":
                            GxObject.PrintBlock _valuePrintBlock = (GxObject.PrintBlock)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _valuePrintBlock, null);
                            break;

                         /*Agregarndo propiedades para el diseño del WebPanel*/
                        case "HTMLForm":
                            GxObject.HTMLForm _htmlForm = (GxObject.HTMLForm)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _htmlForm, null);
                            break;
                        case "HTMLBlock":
                            GxObject.HTMLBlock _htmlBlock = (GxObject.HTMLBlock)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _htmlBlock, null);
                            break;
                        case "HTMLSpan":
                            GxObject.HTMLSpan _htmlSpan = (GxObject.HTMLSpan)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _htmlSpan, null);
                            break;
                        case "HTMLText":
                            GxObject.HTMLText _htmltext = (GxObject.HTMLText)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _htmltext, null);
                            break;
                        case "HTMLControl":
                            GxObject.HTMLControl _htmlControl = (GxObject.HTMLControl)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _htmlControl, null);
                            break;
                        case "InTag_HTMLBlock":
                            GxObject.InTag_HTMLBlock _Intaghtmlblock = (GxObject.InTag_HTMLBlock)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _Intaghtmlblock, null);
                            break;
                        case "InText_HTMLBlock":
                            GxObject.InText_HTMLBlock _InTextHTMLBlock = (GxObject.InText_HTMLBlock)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _InTextHTMLBlock, null);
                            break;
                        case "HTMLAttriValue":
                            GxObject.HTMLAttriValue _htmlAttrVal = (GxObject.HTMLAttriValue)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _htmlAttrVal, null);
                            break;
                        case "HTMLTag":
                            GxObject.HTMLTag _htmlTag = (GxObject.HTMLTag)XMLNodeToObject(nodo);
                            p.SetValue(_objret, _htmlTag, null);
                            break;
                            

                        default:
                            if (nodo.InnerText != "")
                            {
                                object _valueo = Convert.ChangeType(nodo.InnerText, p.PropertyType, null);
                                p.SetValue(_objret, _valueo, null);
                            }
                            break;
                    }

                }
                
            }



            //XmlNodeList _nodos = _xmlObject.OwnerDocument.GetElementsByTagName(TagName);
            return _objret;
        }


        private static void SetBlocksEdits(XmlNode nodo, ref GxObject.Block _block)
        {
            XmlDocument _docFormBlock = GxXML.GetDocument2(nodo.OuterXml);


            XmlNodeList _xmlTexts = GxXML.ReadElements(_docFormBlock, "Text");
            List<GxObject.Text> _Texts = new List<GxObject.Text>();
            foreach (XmlNode _xText in _xmlTexts)
            {
                GxObject.Text _Text = (GxObject.Text)GxXML.XMLNodeToObject(_xText);
                _Texts.Add(_Text);
            }
            _block.Texts = _Texts.ToArray();

            
            XmlNodeList _xmlEdits = GxXML.ReadElements(_docFormBlock, "Edit");
            List<GxObject.Edit> _edits = new List<GxObject.Edit>();
            foreach (XmlNode _xedit in _xmlEdits)
            {
                GxObject.Edit _edit = (GxObject.Edit)GxXML.XMLNodeToObject(_xedit);
                _edits.Add(_edit);
            }
            _block.Edits = _edits.ToArray();


            XmlNodeList _xmlButtons = GxXML.ReadElements(_docFormBlock, "Button");
            List<GxObject.Button> _Buttons = new List<GxObject.Button>();
            foreach (XmlNode _xButton in _xmlButtons)
            {
                GxObject.Button _Button = (GxObject.Button)GxXML.XMLNodeToObject(_xButton);
                _Buttons.Add(_Button);
            }
            _block.Buttons = _Buttons.ToArray();


            XmlNodeList _xmlSubFiles = GxXML.ReadElements(_docFormBlock, "SubFile");
            List<GxObject.SubFile> _SubFiles = new List<GxObject.SubFile>();
            foreach (XmlNode _xSubFile in _xmlSubFiles)
            {
                GxObject.SubFile _SubFile = (GxObject.SubFile)GxXML.XMLNodeToObject(_xSubFile);


                XmlDocument _docXmlItems = GxXML.GetDocument2(_xSubFile.OuterXml);
                XmlNodeList _xmlItems = GxXML.ReadElements(_docXmlItems, "Item");
                List<GxObject.Item> _Items = new List<GxObject.Item>();
                foreach (XmlNode _xItem in _xmlItems)
                {
                    GxObject.Item _Item = (GxObject.Item)GxXML.XMLNodeToObject(_xItem);
                    _Items.Add(_Item);
                }
                _SubFile.Items = _Items.ToArray();

                


                _SubFiles.Add(_SubFile);
            }
            _block.Subfiles = _SubFiles.ToArray();




        }
        

    }
}
