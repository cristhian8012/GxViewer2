using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;
using System.Security.Policy;
using mshtml;

namespace gxViewer
{
    public partial class frmViewBrowser : Form
    {
        private Uri _URL = null;
        private string _nameDefaultSave = "";
        private string _HTML = "";


        public frmViewBrowser(string pUrl)
        {
            InitializeComponent();
            _URL = new Uri(pUrl);
            InitBrowser();
        }

        public frmViewBrowser()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getdata();
        }

        private void webView21_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                this.Text = "GxViewer | " + webView21.CoreWebView2.DocumentTitle;
            }
        }
        private void webView21_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                _HTML = e.TryGetWebMessageAsString();

                if (!String.IsNullOrEmpty(_HTML))
                {
                    ((frmMain)this.Owner).Open_HTML_DLYA(_HTML);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }

        public async void InitBrowser()
        {
            await initizated();
            webView21.CoreWebView2.Navigate(_URL.ToString());
        }

        private async Task initizated()
        {
            await webView21.EnsureCoreWebView2Async(null);
        }

        public async void getdata()
        {
            await webView21.ExecuteScriptAsync("window.chrome.webview.postMessage(document.documentElement.outerHTML)");
        }


    }
}