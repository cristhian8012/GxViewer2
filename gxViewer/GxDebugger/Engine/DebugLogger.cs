namespace gxViewer.GxDebugger.Engine
{
    using System;
    using System.Collections.Generic;

    public class DebugLogger
    {
        public List<string> Logs { get; private set; } 

        public void Log(string msg)
        {
            Logs.Add($"[{DateTime.Now:HH:mm:ss}] {msg}");
        }

        public void Clear() => Logs.Clear();
    }
}
