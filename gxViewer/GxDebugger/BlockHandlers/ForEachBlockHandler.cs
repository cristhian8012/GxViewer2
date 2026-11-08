namespace gxViewer.GxDebugger.BlockHandlers
{
    using gxViewer.GxDebugger.Generators;
    using gxViewer.GxDebugger.Engine;
    using System;
    using System.Collections.Generic;

    public class ForEachBlockHandler
    {
        private readonly List<ParsedLine> _lineas;
        private readonly Dictionary<string, object> _memoria;
        private readonly DebuggerEngine _engine;
        private readonly DebugSession _session;

        private int _inicioIndex, _finIndex;
        private int _filaActual = -1;
        private readonly List<Dictionary<string, object>> _filas;

        public ForEachBlockHandler(List<ParsedLine> lineas, int inicio, DebugSession session)
        {
            _lineas = lineas;
            _inicioIndex = inicio;
            _session = session;
            _memoria = session.Memory;
            _engine = session.Engine;
            _finIndex = BuscarFinDeBloque();
            _filas = new SQLGeneratorGX().Ejecutar(_lineas.GetRange(_inicioIndex, _finIndex - _inicioIndex + 1), _memoria);
        }

        private int BuscarFinDeBloque()
        {
            int profundidad = 1;
            for (int i = _inicioIndex + 1; i < _lineas.Count; i++)
            {
                if (_lineas[i].Type == InstructionType.ForEach) profundidad++;
                else if (_lineas[i].Type == InstructionType.EndFor)
                {
                    profundidad--;
                    if (profundidad == 0) return i;
                }
            }
            throw new Exception("For Each sin EndFor");
        }

        public bool TieneSiguienteIteracion => _filaActual + 1 < _filas.Count;

        public int ObtenerProximaLinea()
        {
            _filaActual++;
            if (_filaActual >= _filas.Count) return _finIndex + 1;

            var fila = _filas[_filaActual];
            foreach (var kv in fila)
                _engine.Memory[kv.Key] = kv.Value;

            return _inicioIndex + 1;
        }

        public int ObtenerFin() => _finIndex;
    }
}
