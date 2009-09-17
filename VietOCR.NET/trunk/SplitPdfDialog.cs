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

            if (dialog.ShowDialog() == DialogResult.Yes)
            {
                string filename = dialog.FileName;
            }
        }

        private void buttonBrowseOutput_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "PDF (*.pdf)|*.pdf";

            if (dialog.ShowDialog() == DialogResult.Yes)
            {
                string filename = dialog.FileName;
            }
        }

        private void buttonSplit_Click(object sender, EventArgs e)
        {
            try
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

                    int pageCount = Utilities.CountPagePdf(this.jTextFieldInputFile.getText());
                    int pageRange = Int32.Parse(this.textBoxNumOfPages.Text);
                    int startPage = 1;

                    while (startPage < pageCount)
                    {
                        int endPage = startPage + pageRange - 1;
                        String outputFileName = outputFilename + startPage + ".pdf";
                        Utilities.SplitPdf(inputFilename, outputFileName, startPage.ToString(), endPage.ToString());
                        startPage = endPage + 1;
                    }
                }
                MessageBox.Show(this, "Split PDF successful.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
    }
}
