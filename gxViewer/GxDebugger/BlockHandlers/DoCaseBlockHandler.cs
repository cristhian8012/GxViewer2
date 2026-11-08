using gxViewer.GxDebugger.Evaluators;
using gxViewer.GxObject;
using System;
using System.Collections.Generic;

namespace gxViewer.GxDebugger.BlockHandlers
{
    

    public class DoCaseBlockHandler
    {
        private readonly List<ParsedLine> _lineas;
        private readonly Dictionary<string, object> _memoria;
        private readonly Dictionary<string, Variable> _variables;
        private int _inicio, _fin;
        private int _inicioEjecutable = -1;

        public DoCaseBlockHandler(List<ParsedLine> lineas, int inicio, Dictionary<string, object> memoria, Dictionary<string, Variable> variables)
        {
            _lineas = lineas;
            _memoria = memoria;
            _variables = variables;
            _inicio = inicio;
            _fin = BuscarFin();
            DeterminarCasoEjecutable();
        }

        private int BuscarFin()
        {
            for (int i = _inicio + 1; i < _lineas.Count; i++)
                if (_lineas[i].Type == InstructionType.EndCase)
                    return i;
            throw new Exception("EndCase no encontrado");
        }

        private void DeterminarCasoEjecutable()
        {
            for (int i = _inicio + 1; i < _fin; i++)
            {
                var linea = _lineas[i];
                if (linea.Type == InstructionType.Case)
                {
                    string condicion = linea.RawCode.Substring("Case".Length).Trim();
                    if (ConditionEvaluator.Evaluar(condicion, _memoria, _variables))
                    {
                        _inicioEjecutable = i;
                        return;
                    }
                }
                if (linea.Type == InstructionType.Otherwise && _inicioEjecutable == -1)
                {
                    _inicioEjecutable = i;
                    return;
                }
            }
        }

        public int ObtenerInicioCaso() => _inicioEjecutable >= 0 ? _inicioEjecutable + 1 : _fin + 1;
        public int ObtenerFin() => _fin;
    }
}
