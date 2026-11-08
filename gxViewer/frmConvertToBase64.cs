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
    public partial class frmConvertToBase64 : Form
    {
        public frmConvertToBase64()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Byte[] bytes = File.ReadAllBytes(txtFile.Text);
            String strFile = Convert.ToBase64String(bytes);
            txtResult.Text = strFile;

        }

        private void lblReverse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmConvertToFile _frm = new frmConvertToFile();
            _frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo Beyond Compare";
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo _info = new FileInfo(ofd.FileName);
                if (_info.Exists)
                {
                    txtFile.Text = _info.FullName;
                }
                _info = null;
            }
            ofd.Dispose();
        }

        private void frmConvertToBase64_DragDrop(object sender, DragEventArgs e)
        {
            // Comprobar si el tipo de datos es el que queremos
            if (e.Data.GetDataPresent("FileName", false))
            {
                string[] _FilesDrop = (string[])(e.Data.GetData("FileDrop", false));
                foreach (string sfile in _FilesDrop)
                {
                    FileInfo _info = new FileInfo(sfile);
                    txtFile.Text = _info.FullName;
                    button2_Click(button2, null);
                    break;
                }
            }
        }

        private void frmConvertToBase64_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtResult.ResetText();
        }
    }
}
