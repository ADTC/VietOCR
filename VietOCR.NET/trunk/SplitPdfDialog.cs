﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace VietOCR.NET
{
    public partial class SplitPdfDialog : Form
    {
        SplitPdfArgs args;
        string pdfFolder = null;

        internal SplitPdfArgs Args
        {
            get { return args; }
        }

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
            dialog.InitialDirectory = pdfFolder;
            dialog.Filter = "PDF (*.pdf)|*.pdf";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxInput.Text = dialog.FileName;
                pdfFolder = Path.GetDirectoryName(dialog.FileName);
            }
        }

        private void buttonBrowseOutput_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = pdfFolder;
            dialog.Filter = "PDF (*.pdf)|*.pdf";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxOutput.Text = dialog.FileName;

                if (!this.textBoxOutput.Text.EndsWith(".pdf") )
                {
                    this.textBoxOutput.Text += ".pdf"; // seems not needed
                }
            }
        }
       
        private void buttonSplit_Click(object sender, EventArgs e)
        {
            SplitPdfArgs args = new SplitPdfArgs();
            args.InputFilename = this.textBoxInput.Text;
            args.OutputFilename = this.textBoxOutput.Text;
            args.FromPage = this.textBoxFrom.Text;
            args.ToPage = this.textBoxTo.Text;
            args.NumOfPages = this.textBoxNumOfPages.Text;
            args.Pages = this.radioButtonPages.Checked;

            if (args.InputFilename.Length > 0 && args.OutputFilename.Length > 0 && 
                ((this.radioButtonPages.Checked && args.FromPage.Length > 0) || 
                (this.radioButtonRange.Checked && args.NumOfPages.Length > 0)))
            {
                Regex regexNums = new Regex(@"^\d+$");

                if ((this.radioButtonPages.Checked && regexNums.IsMatch(args.FromPage)) || (this.radioButtonRange.Checked && regexNums.IsMatch(args.NumOfPages)))
                {
                    this.args = args;
                }
                else
                {
                    MessageBox.Show("Input invalid.", "Error");
                    this.DialogResult = DialogResult.None;       
                }
            }
            else
            {
                MessageBox.Show("Input incomplete.", "Error");
                this.DialogResult = DialogResult.None;
            }
        }

        /// <summary>
        /// Changes localized text and messages
        /// </summary>
        /// <param name="locale"></param>
        public virtual void ChangeUILanguage(string locale)
        {
            FormLocalizer localizer = new FormLocalizer(this, typeof(SplitPdfDialog));
            localizer.ApplyCulture(new CultureInfo(locale));
        }
    }
}
