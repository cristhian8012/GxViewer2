using gxViewer.GxObject;
using System.Collections.Generic;

namespace gxViewer.GxDebugger.Engine
{
    public class DebugSession
    {
        public GXObj GxObject { get; }
        public Dictionary<string, object> Memory { get; set; }
        public DebuggerEngine Engine { get; set; }
        public DebugLogger Logger { get; set; }

        public DebugSession(GXObj gxObject)
        {
            GxObject = gxObject;
            Engine = new DebuggerEngine(this);
            Logger = new DebugLogger();
        }
    }
}
