using System.Collections.Generic;

namespace gxViewer.GxDebugger
{
    public class ParsedLine
    {
        public int LineNumber { get; set; }
        public InstructionType Type { get; set; }
        public string RawCode { get; set; } = "";
        public List<string> VariablesInvolucradas { get; set; } 
    }
}
