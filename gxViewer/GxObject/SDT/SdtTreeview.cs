using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace gxViewer.GxObject.SDT
{

    public abstract class SdtTreeview
    {
        public string Name { get; set; }
        public string Type { get; set; } = "";
        public string Description { get; set; } = "";
        public bool IsCollection { get; set; } = false;

        public SdtTreeview()
        { }

    }

    public class SdtMainTreeview : SdtTreeview
    {
        public List<SdtChildTreeview> ChildItems { get; set; } = new List<SdtChildTreeview> ();

        public SdtMainTreeview()
        { }

        public SdtMainTreeview(string pName )
        {
            this.Name = pName;
        }
    }

    public class SdtChildTreeview : SdtTreeview
    {
        public List<SdtTreeview> ChildItems { get; set; }=new List<SdtTreeview>();

        public SdtChildTreeview()
        { }

        public SdtChildTreeview(string pName)
        {
            this.Name = pName;
        }
    }


    public static class HelperSDT
    {
        public static List<SdtMainTreeview> getTreeview(Types _objTypes)
        {
            List<SdtMainTreeview> _retTree = new List<SdtMainTreeview>();

            SdtMainTreeview _main = new SdtMainTreeview(_objTypes.CGxExpandClassDef.Name);
            _main.ChildItems = new List<SdtChildTreeview>();

            if (_objTypes.CGxExpandClassDef.Properties != null)
            {
                foreach (var item in _objTypes.CGxExpandClassDef.Properties.CGxExpandPropertyDef)
                {
                    SdtChildTreeview _tree = new SdtChildTreeview();
                    string _pNameType = HelperDataTypes.GetNameDataType(item.Type.Text);
                    _tree.Name = item.Name;

                    _tree.Description = item.Type.Obj;

                    var xCol = item.Modifiers.CModifierDefinition.Find(p => p.Name.Equals("Collection"));
                    if (xCol != null)
                    {
                        _tree.IsCollection = Boolean.Parse(xCol.Values.Value);
                    }

                    var xLen = item.Modifiers.CModifierDefinition.Find(p => p.Name.Equals("Length"));
                    int _Tamanio = 0;
                    if (xLen != null)
                    {
                        _Tamanio = int.Parse(xLen.Values.Value);
                    }

                    var xDec = item.Modifiers.CModifierDefinition.Find(p => p.Name.Equals("Decimals"));
                    int _decimals = 0;
                    if (xDec != null)
                    {
                        _decimals = int.Parse(xDec.Values.Value);
                    }

                    if (item.Type.Obj == "Domain")
                    {
                        _pNameType = $"{_pNameType}";
                    }
                    else
                    {
                        if (_Tamanio > 0)
                        {
                            if (_decimals > 0)
                            {
                                _pNameType = $"{_pNameType} ({_Tamanio},{_decimals})";
                            }
                            else
                            {
                                _pNameType = $"{_pNameType} ({_Tamanio})";
                            }
                        }
                    }

                    _tree.Type = _pNameType;

                    _main.ChildItems.Add(_tree);
                }
            }
            else
            {
                /*Cuando no tiene mas columnas*/
                var xDatatype = _objTypes.CGxExpandClassDef.Modifiers.CModifierDefinition.Find(p => p.Name.Equals("Datatype"));
                if (xDatatype != null)
                {
                    _main.Type = HelperDataTypes.GetNameDataType(xDatatype.Values.Value);
                }

                var xCol = _objTypes.CGxExpandClassDef.Modifiers.CModifierDefinition.Find(p => p.Name.Equals("Collection"));
                if (xCol != null)
                {
                    _main.IsCollection = Boolean.Parse(xCol.Values.Value);
                }
            }

            
            _retTree.Add(_main);

            return _retTree;    
        }
    }


}
