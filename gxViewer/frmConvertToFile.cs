using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileToStrBase64
{
    public partial class frmConvertToFile : Form
    {
        public frmConvertToFile()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string _path = txtFile.Text;
            Byte[] bytes = Convert.FromBase64String(txtResult.Text);
            File.WriteAllBytes(_path, bytes);
            MessageBox.Show("Se creó correctamente el archivo :" + _path);

        }

        private void lblReverse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmConvertToBase64 _frm = new frmConvertToBase64();
            _frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Title = "Abrir archivo Beyond Compare";
            //ofd.FilterIndex = 1;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                txtFile.Text = ofd.FileName;
            }
            ofd.Dispose();
        }
    }
}
