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
        Dictionary<string, string> availableLanguageCodes;
        Dictionary<string, string> lookupISO639;
        List<WebClient> clients;
        double bytesIn, totalBytes;
        int numberOfDownloads, numOfConcurrentTasks;
        String workingDir;

        public DownloadDialog()
        {
            InitializeComponent();

            workingDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
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
            clients = new List<WebClient>();

            bytesIn = totalBytes = 0;
            numOfConcurrentTasks = this.listBox1.SelectedIndices.Count;
            this.buttonDownload.Enabled = false;
            this.buttonCancel.Enabled = true;
            this.toolStripProgressBar1.Value = 0;
            this.Cursor = Cursors.WaitCursor;

            foreach (object obj in this.listBox1.SelectedItems)
            {
                // WebClient can only handle one download at a time.
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Client_DownloadFileCompleted);
                clients.Add(client);

                // Starts the download
                string key = FindKey(lookupISO639, obj.ToString());

                if (key != null)
                {
                    try
                    {
                        Uri uri = new Uri(availableLanguageCodes[key]);
                        WebRequest request = WebRequest.Create(uri);
                        request.Timeout = 15000;
                        WebResponse response = request.GetResponse();

                        string filePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(uri.AbsolutePath));
                        client.DownloadFileAsync(uri, filePath, filePath);
                        this.toolStripProgressBar1.Visible = true;
                        this.buttonDownload.Enabled = false;
                        this.toolStripStatusLabel1.Text = "Downloading...";
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Resource does not exist."); //url does not exist
                        //this.listBox1.SelectedIndex = -1;
                    }
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

        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //bytesIn = double.Parse(e.BytesReceived.ToString());
            //totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            //double percentage = 100 * bytesIn / totalBytes;

            //this.toolStripProgressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            this.toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.buttonDownload.Enabled = true;
            this.buttonCancel.Enabled = false;
            this.Cursor = Cursors.Default;

            if (e.Cancelled)
            {
                this.toolStripStatusLabel1.Text = "Download cancelled.";
                //this.toolStripProgressBar1.Visible = false;
            }
            else if (e.Error != null)
            {
                this.toolStripStatusLabel1.Text = "Download error.";
            }
            else
            {
                FileExtractor.ExtractCompressedFile(e.UserState.ToString(), workingDir);

                numberOfDownloads++;
                if (--numOfConcurrentTasks <= 0)
                {
                    this.toolStripStatusLabel1.Text = "Download completed.";
                    this.toolStripProgressBar1.Visible = false;
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (clients != null)
            {
                foreach (WebClient client in clients)
                {
                    client.CancelAsync();
                    client.Dispose();
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            if (numberOfDownloads > 0)
            {
                MessageBox.Show(this, "Please restart the program so that it could register the new language pack(s).", GUI.strProgName);
            }
            base.Close();
        }
    }
}
