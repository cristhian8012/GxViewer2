namespace gxViewer.GxDebugger.Engine
{
    using gxViewer.GxDebugger.BlockHandlers;
    using gxViewer.GxDebugger.Evaluators;
    using System;
    using System.Collections.Generic;

    public class DebuggerEngine
    {
        public List<ParsedLine> ParsedLines { get; set; } 
        public Dictionary<string, object> Memory { get; set; } 
        public int CurrentLineIndex { get; set; } = 0;

        private readonly DebugSession _session;
        private readonly Stack<ForEachBlockHandler> _forEachStack;

        public event Action<ParsedLine, Dictionary<string, object>> OnStep;

        public DebuggerEngine(DebugSession session) => _session = session;

        public void Step()
        {
            if (CurrentLineIndex >= ParsedLines.Count) return;

            var linea = ParsedLines[CurrentLineIndex];

            switch (linea.Type)
            {
                case InstructionType.ForEach:
                    var handler = new ForEachBlockHandler(ParsedLines, CurrentLineIndex, _session);
                    if (handler.TieneSiguienteIteracion)
                    {
                        _forEachStack.Push(handler);
                        CurrentLineIndex = handler.ObtenerProximaLinea();
                    }
                    else
                    {
                        CurrentLineIndex = handler.ObtenerFin() + 1;
                    }
                    return;

                case InstructionType.EndFor when _forEachStack.Count > 0:
                    var loop = _forEachStack.Peek();
                    if (loop.TieneSiguienteIteracion)
                        CurrentLineIndex = loop.ObtenerProximaLinea();
                    else
                    {
                        _forEachStack.Pop();
                        CurrentLineIndex++;
                    }
                    return;

                case InstructionType.DoCase:
                    var caseHandler = new DoCaseBlockHandler(ParsedLines, CurrentLineIndex, Memory, _session.GxObject.GXVariables);
                    CurrentLineIndex = caseHandler.ObtenerInicioCaso();
                    return;

                case InstructionType.EndCase:
                    CurrentLineIndex++;
                    return;
            }

            LineEvaluator.Evaluar(linea, Memory);
            OnStep?.Invoke(linea, new Dictionary<string, object>(Memory));
            CurrentLineIndex++;
        }

        public void Reset()
        {
            CurrentLineIndex = 0;
            Memory.Clear();
            _forEachStack.Clear();
        }
    }
}
