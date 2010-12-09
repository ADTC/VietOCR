using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.IO;
using VietOCR.NET.Utilities;
using System.Xml;
using System.Threading;


namespace VietOCR.NET
{
    public partial class DownloadDialog : Form
    {
        Dictionary<string, string> availableLanguageCodes;
        Dictionary<string, string> availableDictionaries;
        Dictionary<string, string> iso_3_1_Codes;
        Dictionary<string, string> lookupISO639;
        List<WebClient> clients;
        Dictionary<string, long> downloadTracker;
        int numberOfDownloads, numOfConcurrentTasks;
        long contentLength;
        String workingDir;

        public DownloadDialog()
        {
            InitializeComponent();

            workingDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            clients = new List<WebClient>();
            downloadTracker = new Dictionary<string, long>();
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            lookupISO639 = ((GUI)this.Owner).LookupISO639;
            iso_3_1_Codes = ((GUI)this.Owner).ISO_3_1_Codes;

            String xmlFilePath = Path.Combine(workingDir, "Data/Tess2DataURL.xml");
            availableLanguageCodes = new Dictionary<string, string>();
            Utilities.Utilities.LoadFromXML(availableLanguageCodes, xmlFilePath);

            xmlFilePath = Path.Combine(workingDir, "Data/OO-SpellDictionaries.xml");
            availableDictionaries = new Dictionary<string, string>();
            Utilities.Utilities.LoadFromXML(availableDictionaries, xmlFilePath);

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
            this.toolStripProgressBar1.Value = 0;
            this.toolStripProgressBar1.Visible = true;
            this.buttonDownload.Enabled = false;
            this.toolStripStatusLabel1.Text = "Downloading...";
            this.Cursor = Cursors.WaitCursor;

            clients.Clear();
            downloadTracker.Clear();
            contentLength = 0;
            numOfConcurrentTasks = this.listBox1.SelectedIndices.Count;

            foreach (object obj in this.listBox1.SelectedItems)
            {
                string key = FindKey(lookupISO639, obj.ToString()); // Vietnamese -> vie
                if (key != null)
                {
                    try
                    {
                        Uri uri = new Uri(availableLanguageCodes[key]);
                        DownloadDataFile(uri, string.Empty);  // download language data pack

                        if (iso_3_1_Codes.ContainsKey(key))
                        {
                            String iso_3_1_Code = iso_3_1_Codes[key]; // vie -> vi_VN
                            uri = new Uri(availableDictionaries[iso_3_1_Code]);
                            if (uri != null)
                            {
                                ++numOfConcurrentTasks;
                                DownloadDataFile(uri, "dict"); // download dictionary
                            }
                        }
                    }
                    catch (Exception)
                    {
                        if (--numOfConcurrentTasks <= 0)
                        {
                            this.toolStripStatusLabel1.Text = "Download error.";
                            this.toolStripProgressBar1.Visible = false;
                            resetUI();
                        }
                    }
                }
            }
        }

        string FindKey(IDictionary<string, string> lookup, string value)
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

        void DownloadDataFile(Uri uri, string destFolder)
        {
            try
            {
                // WebClient can only handle one download at a time.
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(Client_DownloadFileCompleted);
                clients.Add(client);
                WebRequest request = WebRequest.Create(uri);
                request.Timeout = 15000;
                WebResponse response = request.GetResponse();
                contentLength += response.ContentLength;
                string filePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(uri.AbsolutePath));
                client.DownloadFileAsync(uri, filePath, filePath);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("404"))
                {
                    MessageBox.Show("Resource does not exist."); //url does not exist
                }
                else
                {
                    MessageBox.Show(e.Message);
                }
                throw e;
            }
        }

        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            string filePath = e.UserState.ToString();

            if (!downloadTracker.ContainsKey(filePath))
            {
                downloadTracker.Add(filePath, e.BytesReceived);
            }
            else
            {
                downloadTracker[filePath] = e.BytesReceived;
            }

            long totalBytesReceived = 0;
            foreach (int bytesReceived in downloadTracker.Values)
            {
                totalBytesReceived += bytesReceived;
            }

            this.toolStripProgressBar1.Value = (int)(100 * totalBytesReceived / contentLength);
        }

        void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.toolStripStatusLabel1.Text = "Download cancelled.";
                resetUI();
            }
            else if (e.Error != null)
            {
                this.toolStripProgressBar1.Visible = false;
                this.toolStripStatusLabel1.Text = e.Error.Message;
                resetUI();
            }
            else
            {
                string fileName = e.UserState.ToString();
                string key = Path.GetFileNameWithoutExtension(fileName);
                FileExtractor.ExtractCompressedFile(fileName, availableDictionaries.ContainsKey(key) ? workingDir + "/dict" : workingDir);

                numberOfDownloads++;
                if (--numOfConcurrentTasks <= 0)
                {
                    this.toolStripStatusLabel1.Text = "Download completed.";
                    this.toolStripProgressBar1.Visible = false;
                    resetUI();
                }
            }
        }

        void resetUI()
        {
            this.buttonDownload.Enabled = true;
            this.buttonCancel.Enabled = false;
            this.Cursor = Cursors.Default;
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
