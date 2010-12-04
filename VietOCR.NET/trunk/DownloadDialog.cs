using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;


namespace VietOCR.NET
{
    public partial class DownloadDialog : Form
    {
        const string url = "http://tesseract-ocr.googlecode.com/files/tesseract-2.00.{0}.tar.gz";
        string filePath;
        
        string[] availableCodes;

        public string[] AvailableCodes
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

            this.listBox1.Items.AddRange(availableCodes);
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

            // Starts the download
            Uri uri = new Uri(string.Format(url, this.listBox1.SelectedItem.ToString()));
            filePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(uri.AbsolutePath));
            client.DownloadFileAsync(uri, filePath);

            this.buttonDownload.Text = "Download In Process";
            this.buttonDownload.Enabled = false;
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            this.progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
        }


        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download Completed");

            this.buttonDownload.Text = "Start Download";
            this.buttonDownload.Enabled = true;
            String workingDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            ExtractTGZ(filePath, workingDir);
        }

        public void ExtractTGZ(String gzArchiveName, String destFolder)
        {
            Stream inStream = File.OpenRead(gzArchiveName);
            Stream gzipStream = new GZipInputStream(inStream);

            TarArchive tarArchive = TarArchive.CreateInputTarArchive(gzipStream);
            tarArchive.ExtractContents(destFolder);
            tarArchive.Close();

            gzipStream.Close();
            inStream.Close();
        }

    }
}
