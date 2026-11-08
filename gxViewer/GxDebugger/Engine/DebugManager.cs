using FastColoredTextBoxNS;
//using WeifenLuo.WinFormsUI.Docking;
using gxViewer.GxDebugger.UI;
using gxViewer.GxObject;
using System.Collections.Generic;


namespace gxViewer.GxDebugger.Engine
{
   
    public class DebugManager
    {
        private readonly GXObj _gxObject;
        private readonly FastColoredTextBox _txtCode;
        //private readonly DockPanel _dockPanel;
        private DebugSession _session;

        private frmParametroPanel _varPanel;
        private frmParametroPanel _paramPanel;
        private frmLogPanel _logPanel;
        private frmDebugControl _controlBar;

        public DebugManager(GXObj gxObject, FastColoredTextBox txtCode)
        {
            _gxObject = gxObject;
            _txtCode = txtCode;
            //_dockPanel = dockPanel;

            _session = new DebugSession(_gxObject);
            InicializarUI();
        }

        private void InicializarUI()
        {
            _controlBar = new frmDebugControl(_txtCode, _session.Engine);
            _controlBar.Show();

            _varPanel = new frmParametroPanel();
            //_varPanel.Show(_dockPanel, DockState.DockRight);

            _paramPanel = new frmParametroPanel();
            //_paramPanel.Show(_dockPanel, DockState.DockLeft);

            _logPanel = new frmLogPanel();
            //_logPanel.Show(_dockPanel, DockState.DockBottomAutoHide);

            _session.Engine.OnStep += OnStep;
        }

        private void OnStep(ParsedLine linea, Dictionary<string, object> memoria)
        {
            // actualizar interfaz visual
            _varPanel?.CargarVariables(memoria);
            //_paramPanel?.CargarParametros(_gxObject.ParametrosIniciales, memoria);
            _logPanel?.AgregarLog($"▶ Línea {linea.LineNumber}: {linea.RawCode}");
        }

        public void Finalizar()
        {
            _controlBar?.Close();
            _varPanel?.Close();
            _paramPanel?.Close();
            _logPanel?.Close();
        }
    }
}
