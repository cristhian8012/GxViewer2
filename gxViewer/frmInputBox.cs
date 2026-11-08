using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gxViewer
{
    public partial class frmInputBox : Form
    {
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string DefaultText { get; set; }
        public string Retorno { get; set; }

        public frmInputBox()
        {
            InitializeComponent();
        }

        private void frmInputBox_Load(object sender, EventArgs e)
        {
            this.Text = this.Titulo;
            lblMSG.Text = this.Texto;
            txtDescripcion.Text = this.DefaultText;
            txtDescripcion.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Hide();
        }

        private void frmInputBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Retorno = txtDescripcion.Text;
        }
    }
}
