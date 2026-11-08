using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace gxViewer.GxDebugger.UI
{
    public partial class frmVariablePanel : Form
    {
        
        public frmVariablePanel()
        {
            InitializeComponent();
            this.Text = "🔎 Variables";

            //this.lst.DoubleClick += (s, e) => OnInspeccionarArray?.Invoke(lst.SelectedItem?.ToString() ?? "");
            

        }

        public void CargarVariables(Dictionary<string, object> memoria)
        {
            lst.Items.Clear();
            foreach (var kv in memoria)
                lst.Items.Add($"{kv.Key} = {kv.Value}");
        }

        //public Action<string> OnInspeccionarArray;

    }
}
