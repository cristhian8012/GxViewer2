using GXBaseKB;
using mshtml;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;
using static gxViewer.GxEditorHelper.WebPanel;

namespace gxViewer.GxObject
{
    public class HTMLSFL : HtmlControlForm
    {

        
        public string ControlName { get; set; }


        /*
         
        
            <Property>
              <Type>object</Type>
              <Name>AttID</Name>
              <Value>[5:-14]</Value>
            </Property>
            <Property>
              <Type>string</Type>
              <Name>Class</Name>
              <Value />
            </Property>
            <Property>
              <Type>string</Type>
              <Name>ReadOnly</Name>
              <Value>-1</Value>
            </Property>
            <Property>
              <Type>integer</Type>
              <Name>BorderStyle</Name>
              <Value>0</Value>
            </Property>
         
         */



        public string Class { get; set; } = "Grid";
        public string AutoResize { get; set; }
        public string BackColorStyle { get; set; }
        public string AllowSelection { get; set; }
        public string SelectionColor { get; set; }


        public string HoverColor { get; set; }
        public string Border { get; set; }
        public string cellPadding { get; set; }
        public string cellSpacing { get; set; }

        public string[] ControlValues { get; set; } = new string[] { };
        
        public int Rows { get; set; } = 0;
        public List<HColum> Columns { get; set; } = new List<HColum>();


    }

    public class HTMLATT : HtmlControlForm
    {
        public string ControlName { get; set; }

        string _CollAtt = "";
        string _DType = "";
        string _IdVar = "";

        public string AttID
        {
            get { return _CollAtt; }
            set
            {
                _CollAtt = value;

                string[] _colAtt = _CollAtt.Replace("[", "").Replace("]", "").Split(":".ToCharArray());
                _DType = _colAtt[0];
                _IdVar = _colAtt[1];
            }
        }

        public bool Visible { get; set; } = true;

        public string DType { get { return _DType; } }
        public Variable getVarFromObject(GXObj _obj)
        {
            Variable _var = _obj.getVarById(_IdVar);
            return _var;
        }


        public string GetDataTypeAlignString()
        {
            DataTypeId dataType = HelperDataTypes.GetDataType(DType);
            if (dataType == DataTypeId.Bitmap)
            {
                return "\"CENTER\"";
            }
            if (dataType == DataTypeId.Numeric)
            {
                return "\"RIGHT\"";
            }
            return "\"LEFT\"";
        }

        public string[] ControlValues { get; set; } = new string[] { };

        public int Rows { get; set; } = 0;

        public string Class { get; set; } = "Grid";
        public string BackColorStyle { get; set; }       
        public string BorderStyle { get; set; }

        public string AutoResize { get; set; }
        public string ControlType { get; set; }

        public WebFormEditType EditType 
        {
            get
            {
                switch (ControlType)
                {
                    case "11": return WebFormEditType.CheckBox; 
                    case "8": return WebFormEditType.ComboBox; 
                    case "2": return WebFormEditType.EditBox; 
                    case "0": return WebFormEditType.ListBox; 

                    default:
                        return WebFormEditType.EditBox;
                }
            }
        }


        public string GxFormat { get; set; }
        


    }


    public class HColum
    {
        public string Name { get; set; }
        public string ColReadOnly { get; set; } = "0";

        public string ColWidth { get; set; } = "10";
        public string ColTitle { get; set; }
        public string ColLinesColor { get; set; }
        public string Class { get; set; }
        public string ColAutoResize { get; set; }

        string _CollAtt = "";
        string _DType = "";
        string _IdVar = "";

        public string ColAttId {
            get { return _CollAtt; }
            set { 
                _CollAtt = value;

                string[] _colAtt = _CollAtt.Replace("[", "").Replace("]", "").Split(":".ToCharArray());
                _DType = _colAtt[0];
                _IdVar = _colAtt[1];
            } 
        } 
        public string UserMode { get; set; }
        public string[] ControlValues { get; set; } = new string[] { };
        public string ControlType { get; set; }

        public bool Visible { get; set; } = true;
        public bool ReturnOnClick { get; set; } = false;
        

        public string DType { get { return _DType; } }
        public Variable getVarFromObject(GXObj _obj) 
        {
            Variable _var = _obj.getVarById(_IdVar);
            return _var;
        }


        public string GetDataTypeAlignString()
        {
            DataTypeId dataType = HelperDataTypes.GetDataType(DType);
            if (dataType == DataTypeId.Bitmap)
            {
                return "\"CENTER\"";
            }
            if (dataType == DataTypeId.Numeric)
            {
                return "\"RIGHT\"";
            }
            return "\"LEFT\"";
        }

        public WebFormEditType EditType
        {
            get
            {
                switch (ControlType)
                {
                    case "11": return WebFormEditType.CheckBox;
                    case "8": return WebFormEditType.ComboBox;
                    case "2": return WebFormEditType.EditBox;
                    case "0": return WebFormEditType.ListBox;

                    default:
                        return WebFormEditType.EditBox;
                }
            }
        }

    }
}
