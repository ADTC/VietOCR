using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using VietOCR.NET.Utilities;


namespace VietOCR.NET
{
    public partial class DownloadDialog : Form
    {
        const string url = "http://tesseract-ocr.googlecode.com/files/tesseract-2.00.{0}.tar.gz";
        string filePath;

        Dictionary<string, string> availableCodes;

        public Dictionary<string, string> AvailableCodes
        {
            set { availableCodes = value; }
        }

        string[] installedCodes;

        public string[] InstalledCodes
        {
            set { installedCodes = value; }
        }

        public DownloadDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            string[] available = new string[availableCodes.Count];
            availableCodes.Values.CopyTo(available, 0);

            this.listBox1.Items.Clear();
            this.listBox1.Items.AddRange(available);
            this.ActiveControl = this.listBox1;

            foreach (string installed in installedCodes)
            {
                for (int i = 0; i < available.Length; ++i)
                {
                    if (installed == available[i])
                    {
                        this.listBox1.DisableItem(i);
                        break;
                    }
                }
            }
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex == -1)
            {
                return;
            }

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

            // Starts the download
            string value = FindKey(availableCodes, this.listBox1.SelectedItem.ToString());
            if (value != null)
            {
                try
                {
                    Uri uri = new Uri(string.Format(url, value));
                    WebRequest request = WebRequest.Create(uri);
                    request.Timeout = 15000;
                    WebResponse response = request.GetResponse();

                    filePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(uri.AbsolutePath));
                    client.DownloadFileAsync(uri, filePath);
                    this.toolStripProgressBar1.Visible = true;
                    this.buttonDownload.Enabled = false;
                }
                catch (Exception)
                {
                    MessageBox.Show("Resource does not exist."); //url does not exist
                    this.listBox1.SelectedIndex = -1;
                }
            }
        }

        public string FindKey(IDictionary<string, string> lookup, string value)
        {
            foreach (var pair in lookup)
            {
                if (pair.Value == value)
                {
                    return pair.Key;
                }
            }
            return null;
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            this.toolStripProgressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download Completed");

            this.buttonDownload.Enabled = true;
            String workingDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            FileExtractor.ExtractTGZ(filePath, workingDir);
            this.toolStripProgressBar1.Visible = false;
            this.listBox1.SelectedIndex = -1;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
