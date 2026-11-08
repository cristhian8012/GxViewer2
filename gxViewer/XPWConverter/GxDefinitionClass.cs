using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gxViewer.XPWConverter
{
    public class GxDefinitionClass
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public bool IsMainObject { get; set; }
        public bool ChangeStringDef { get; set; }

        public List<GxDefinitionClassProperties> ListProperties { get; set; }

        public string IniBracket { get; set; }
        public string EndBracket { get; set; }


    }

    public class GxDefinitionClassProperties
    {
        public string PropName { get; set; }
        public string PropTypeName { get; set; }
        public bool IsPropGXObject { get; set; }
        public bool IsArray { get; set; }
        public bool IsDictionary { get; set; }

        public bool ChangeStringDef { get; set; }

        public string IniBracket { get; set; }
        public string EndBracket { get; set; }

        public string ReplaceWith { get; set; }

        public string getRepIniBracket()
        {
            return ":" + ReplaceWith +"\r\n";
        }
        public string getRepEndBracket()
        {
            return ":e" + ReplaceWith + "\r\n";
        }

    }


}
