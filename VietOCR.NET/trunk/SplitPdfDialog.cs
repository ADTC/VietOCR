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
    }
}
