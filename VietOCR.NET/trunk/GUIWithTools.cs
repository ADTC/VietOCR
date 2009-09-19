using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace VietOCR.NET
{
    public partial class GUIWithTools : VietOCR.NET.GUIWithSettings
    {
        const string strImageFolder = "ImageFolder";

        string imageFolder;
        int filterIndex;

        public GUIWithTools()
        {
            InitializeComponent();
        }

        protected override void mergeTiffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = imageFolder;
            openFileDialog1.Title = Properties.Resources.Select + " Input Images";
            openFileDialog1.Filter = "Image Files (*.tif;*.tiff)|*.tif;*.tiff|Image Files (*.bmp)|*.bmp|Image Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|Image Files (*.png)|*.png|All Image Files|*.tif;*.tiff;*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog1.FilterIndex = filterIndex;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filterIndex = openFileDialog1.FilterIndex;
                imageFolder = Path.GetDirectoryName(openFileDialog1.FileName);
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = imageFolder;
                saveFileDialog1.Title = Properties.Resources.Save + " Multi-page TIFF Image";
                saveFileDialog1.Filter = "Image Files (*.tif;*.tiff)|*.tif;*.tiff";
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.Delete(saveFileDialog1.FileName);
                    try
                    {
                        ImageIOHelper.MergeTiff(openFileDialog1.FileNames, saveFileDialog1.FileName);
                        MessageBox.Show(this, Properties.Resources.Mergecompleted + Path.GetFileName(saveFileDialog1.FileName) + Properties.Resources.created, strProgName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (ApplicationException ae)
                    {
                        Console.WriteLine("ERROR: " + ae.Message);
                        MessageBox.Show(this, ae.Message, strProgName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        protected override void splitPdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SplitPdfDialog dialog = new SplitPdfDialog();
            dialog.Owner = this;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                this.toolStripStatusLabel1.Text = "Splitting PDF";
                //this.pictureBox1.UseWaitCursor = true;
                this.textBox1.Cursor = Cursors.WaitCursor;
                this.toolStripProgressBar1.Enabled = true;
                this.toolStripProgressBar1.Visible = true;
                this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;


                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync(dialog.Args);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SplitPdfArgs args = (SplitPdfArgs)e.Argument;

            if (args.Pages)
            {
                Utilities.SplitPdf(args.InputFilename, args.OutputFilename, args.FromPage, args.ToPage);
            }
            else
            {
                string outputFilename = String.Empty;

                if (args.OutputFilename.EndsWith(".pdf"))
                {
                    outputFilename = args.OutputFilename.Substring(0, args.OutputFilename.LastIndexOf(".pdf"));
                }

                int pageCount = Utilities.GetPdfPageCount(args.InputFilename);
                if (pageCount == 0)
                {
                    throw new ApplicationException("Split PDF failed.");
                }

                int pageRange = Int32.Parse(args.NumOfPages);
                int startPage = 1;

                while (startPage <= pageCount)
                {
                    int endPage = startPage + pageRange - 1;
                    string outputFileName = outputFilename + startPage + ".pdf";
                    Utilities.SplitPdf(args.InputFilename, outputFileName, startPage.ToString(), endPage.ToString());
                    startPage = endPage + 1;
                }
            }

            e.Result = args.OutputFilename;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                this.toolStripStatusLabel1.Text = String.Empty;
                this.toolStripProgressBar1.Enabled = false;
                this.toolStripProgressBar1.Visible = false;
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled the operation.
                // Note that due to a race condition in the DoWork event handler, the Cancelled
                // flag may not have been set, even though CancelAsync was called.
                this.toolStripStatusLabel1.Text = Properties.Resources.Canceled;
            }
            else
            {
                // Finally, handle the case where the operation succeeded.
                this.toolStripStatusLabel1.Text = "Split PDF completed";
                MessageBox.Show(this, "Split PDF completed.\nPlease check output file(s) in " + Path.GetDirectoryName(e.Result.ToString()));
            }
            this.toolStripStatusLabel1.Text = String.Empty;
            this.toolStripProgressBar1.Enabled = false;
            this.toolStripProgressBar1.Visible = false;
            this.textBox1.Cursor = Cursors.Default;
            this.Cursor = Cursors.Default;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);
            imageFolder = (string)regkey.GetValue(strImageFolder, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);
            regkey.SetValue(strImageFolder, imageFolder);
        }
    }
}
