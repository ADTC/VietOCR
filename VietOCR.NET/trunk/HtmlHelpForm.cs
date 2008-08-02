using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace VietOCR.NET
{
    public partial class HtmlHelpForm : Form
    {
        const string ABOUT = "about:";

        public HtmlHelpForm(string helpFileName, string title)
        {
            InitializeComponent();
            this.Text = title;

            //foreach (string name in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            //    System.Console.WriteLine(name);

            this.webBrowser1.DocumentStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("VietOCR.NET." + helpFileName);
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string url = e.Url.ToString();
            
            if (url.StartsWith(ABOUT) && url != "about:blank")
            {
                this.webBrowser1.DocumentStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("VietOCR.NET." + url.Substring(ABOUT.Length));
            }
            else if (url.StartsWith("http"))
            {
                // Display external links using default webbrowser
                e.Cancel = true;
                System.Diagnostics.Process.Start(url);
            }
        }

        private void webBrowser1_StatusTextChanged(object sender, System.EventArgs e)
        {
            this.toolStripStatusLabel1.Text = this.webBrowser1.StatusText;
        }
    }
}