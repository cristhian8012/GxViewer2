using gxViewer;
using log4net.helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.XPath;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Schema;
using System.Xml.Linq;
using System.Reflection;
using System.ComponentModel.Design;
using System.Xml.Serialization;
using gxViewer.GxObject.SDT;
using System.Net;

namespace FileToStrBase64
{
    public partial class frmTestTransformXLST : Form
    {
        public frmTestTransformXLST()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtResult.Text = "Resultado";

            // using System.Xml.Serialization;
            //XmlSerializer serializer = new XmlSerializer(typeof(Types));
            string xml = txtXML.Text;

            gxViewer.Gx18.ExportFile _export = Functions.Deserialize<gxViewer.Gx18.ExportFile>(txtFileXML.Text, Encoding.UTF8);

            txtResult.Text = _export.KMW.Path;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Abrir archivo XML";
            ofd.Filter = "Archivos XML (*.xml)|*.xml|Todos (*.*)|*.*";
            ofd.FilterIndex = 0;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo _info = new FileInfo(ofd.FileName);
                if (_info.Exists)
                {
                    txtFileXML.Text = _info.FullName;
                    LoadFile(_info.FullName, txtXML);
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
                    txtFileXML.Text = _info.FullName;
                    LoadFile(_info.FullName, txtXML);
                    break;
                }
            }
        }

        private void frmConvertToBase64_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void frmTestTransformXLST_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty( txtFileXML.Text))
            {
                LoadFile(txtFileXML.Text, txtXML);
            }

        }

        private void LoadFile(string pFile, RichTextBox ptxt)
        {
            FileInfo _info = new FileInfo(pFile);
            if (_info.Exists)
            {
                ptxt.LoadFile(_info.FullName, RichTextBoxStreamType.PlainText);
            }
        }

        private void btnCargarXML_Click(object sender, EventArgs e)
        {
            this.treeListView1.CanExpandGetter = delegate (Object x) {
                return (x is SdtMainTreeview) || (x is SdtChildTreeview);
            };

            this.treeListView1.ChildrenGetter = delegate (Object x) {
                if (x is SdtMainTreeview)
                    return ((SdtMainTreeview)x).ChildItems;
                if (x is SdtChildTreeview)
                    return ((SdtChildTreeview)x).ChildItems;

                throw new ArgumentException("Error al seleccionar Items");
            };


            // using System.Xml.Serialization;
            XmlSerializer serializer = new XmlSerializer(typeof(Types));
            string xml = txtXML.Text;

            using (StringReader reader = new StringReader(xml))
            {
                var test = (Types)serializer.Deserialize(reader);
                List<SdtMainTreeview> _lista = HelperSDT.getTreeview(test);

                this.treeListView1.SetObjects(_lista);
                this.treeListView1.ExpandAll();
            }




        }
    }
}
