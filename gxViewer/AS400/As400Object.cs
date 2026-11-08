using gxViewer.GxObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using static log4net.Appender.ColoredConsoleAppender;

namespace gxViewer.AS400
{
    public class As400Object 
    {
        private IList<string> _linesCode = new List<string>();
        public string Name { get; set; } = "";
        public string Ext { get; set; } = "";
        public string Filename { get; set; }
        public string Documentation { get; set; } = "";
        public string Source { get; set; } = "";

        public string Rules { get; set; } = "";

        public AS400_Types Type { get; set; } = AS400_Types.Other;

        public string GetNameType()
        {
            string _ret = "";
            _ret = Enum.GetName(typeof(AS400_Types), this.Type);
            return _ret;
        }


        public List<Variable> Variables { get; set; }

        public IList<string> getLinesCode()
        {
            return _linesCode;
        }

        public void setLinesCode(IList<string> value)
        {
            _linesCode = value;
        }

    }
}
