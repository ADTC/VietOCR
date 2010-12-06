using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.IO;
using VietOCR.NET.Utilities;
using System.Xml;


namespace VietOCR.NET
{
    public partial class DownloadDialog : Form
    {
        string filePath;
        Dictionary<string, string> availableLanguageCodes;
        Dictionary<string, string> lookupISO639;
        WebClient client;

        public DownloadDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            lookupISO639 = ((GUI)this.Owner).LookupISO639;
            availableLanguageCodes = new Dictionary<string, string>();
            XmlDocument doc = new XmlDocument();

            String workingDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            String xmlFilePath = Path.Combine(workingDir, "Data/Tess2DataURL.xml");
            doc.Load(xmlFilePath);

            XmlNodeList list = doc.GetElementsByTagName("entry");
            foreach (XmlNode node in list)
            {
                availableLanguageCodes.Add(node.Attributes[0].Value, node.InnerText);
            }

            string[] available = new string[availableLanguageCodes.Count];
            availableLanguageCodes.Keys.CopyTo(available, 0);
            List<String> names = new List<String>();
            foreach (String key in available)
            {
                names.Add(this.lookupISO639[key]);
            }
            names.Sort();

            this.listBox1.Items.Clear();
            this.listBox1.Items.AddRange(names.ToArray());

            foreach (string installed in ((GUI)this.Owner).InstalledLanguages)
            {
                for (int i = 0; i < names.Count; ++i)
                {
                    if (installed == names[i])
                    {
                        this.listBox1.DisableItem(i);
                        break;
                    }
                }
            }
            this.ActiveControl = this.listBox1;
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex == -1)
            {
                return;
            }

            this.buttonDownload.Enabled = false;
            this.buttonCancel.Enabled = true;

            client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

            // Starts the download
            string value = FindKey(lookupISO639, this.listBox1.SelectedItem.ToString());

            if (value != null)
            {
                try
                {
                    Uri uri = new Uri(availableLanguageCodes[value]);
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
            if (e.Cancelled)
            {
                MessageBox.Show("Download cancelled.");
                return;
            }

            MessageBox.Show("Download completed.");

            this.buttonDownload.Enabled = true;
            this.buttonCancel.Enabled = false;
            String workingDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            FileExtractor.ExtractCompressedFile(filePath, workingDir);
            this.toolStripProgressBar1.Visible = false;
            this.listBox1.SelectedIndex = -1;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (client != null && client.IsBusy)
            {
                client.CancelAsync();
                client.Dispose();
                client = null;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            base.Close();
            this.Dispose();
        }
    }
}
