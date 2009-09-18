using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VietOCR.NET
{
    public partial class SplitPdfDialog : Form
    {
        public SplitPdfDialog()
        {
            InitializeComponent();
            disableBoxes(!this.radioButtonPages.Checked);
        }

        private void radioButtonPages_CheckedChanged(object sender, EventArgs e)
        {
            disableBoxes(false);
        }

        private void radioButtonRange_CheckedChanged(object sender, EventArgs e)
        {
            disableBoxes(true);
        }

        void disableBoxes(bool enabled)
        {
            this.textBoxNumOfPages.Enabled = enabled;
            this.textBoxFrom.Enabled = !enabled;
            this.textBoxTo.Enabled = !enabled;
        }

        private void buttonBrowseInput_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PDF (*.pdf)|*.pdf";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxInput.Text = dialog.FileName;
            }
        }

        private void buttonBrowseOutput_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PDF (*.pdf)|*.pdf";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxOutput.Text = dialog.FileName;

                if (!this.textBoxOutput.Text.EndsWith(".pdf") )
                {
                    this.textBoxOutput.Text += "pdf"; // seems not needed
                }
            }
        }

        private void buttonSplit_Click(object sender, EventArgs e)
        {
            try
            {
                //this.toolStripStatusLabel1.Text = Properties.Resources.Scanning;
                this.Cursor = Cursors.WaitCursor;
                //this.pictureBox1.UseWaitCursor = true;
                //this.textBox1.Cursor = Cursors.WaitCursor;
                //this.toolStripProgressBar1.Enabled = true;
                //this.toolStripProgressBar1.Visible = true;
                //this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;

            	// Start the asynchronous operation.
            	backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            String inputFilename = this.textBoxInput.Text;
            String outputFilename = this.textBoxOutput.Text;

            if (this.radioButtonPages.Checked)
            {
                Utilities.SplitPdf(inputFilename, outputFilename, this.textBoxFrom.Text, this.textBoxTo.Text);
            }
            else
            {
                if (outputFilename.EndsWith(".pdf"))
                {
                    outputFilename = outputFilename.Substring(0, outputFilename.LastIndexOf(".pdf"));
                }

                int pageCount = Utilities.GetPdfPageCount(inputFilename);
                if (pageCount == 0)
                {
                    throw new ApplicationException("Split PDF failed.");
                }

                int pageRange = Int32.Parse(this.textBoxNumOfPages.Text);
                int startPage = 1;

                while (startPage <= pageCount)
                {
                    int endPage = startPage + pageRange - 1;
                    String outputFileName = outputFilename + startPage + ".pdf";
                    Utilities.SplitPdf(inputFilename, outputFileName, startPage.ToString(), endPage.ToString());
                    startPage = endPage + 1;
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                //this.toolStripStatusLabel1.Text = String.Empty;
                //this.toolStripProgressBar1.Enabled = false;
                //this.toolStripProgressBar1.Visible = false;
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled the operation.
                // Note that due to a race condition in the DoWork event handler, the Cancelled
                // flag may not have been set, even though CancelAsync was called.
                //this.toolStripStatusLabel1.Text = Properties.Resources.Canceled;
            }
            else
            {
                // Finally, handle the case where the operation succeeded.
                //openFile(e.Result.ToString());
                //this.toolStripStatusLabel1.Text = Properties.Resources.Scancompleted;
                MessageBox.Show(this, "Split PDF successful.");
            }

            this.Cursor = Cursors.Default;
        }
    }
}
