using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace VietOCR.NET
{
    public partial class OptionsDialog : Form
    {
        private string watchFolder;

        public string WatchFolder
        {
            get { return watchFolder; }
            set { watchFolder = value; }
        }
        private string outputFolder;

        public string OutputFolder
        {
            get { return outputFolder; }
            set { outputFolder = value; }
        }
        private bool watchEnabled;

        public bool WatchEnabled
        {
            get { return watchEnabled; }
            set { watchEnabled = value; }
        }

        public OptionsDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            this.textBoxWatch.Text = watchFolder;
            this.textBoxOutput.Text = outputFolder;
            this.checkBoxWatch.Checked = watchEnabled;
        }

        protected override void OnClosed(EventArgs ea)
        {
            base.OnClosed(ea);

            watchFolder = this.textBoxWatch.Text;
            outputFolder = this.textBoxOutput.Text;
            watchEnabled = this.checkBoxWatch.Checked;
        }

        private void btnWatch_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = "Set Watch Folder.";
            this.folderBrowserDialog1.SelectedPath = watchFolder;

            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                watchFolder = this.folderBrowserDialog1.SelectedPath;
                this.textBoxWatch.Text = watchFolder;
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = "Set Output Folder.";
            this.folderBrowserDialog1.SelectedPath = outputFolder;

            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                outputFolder = this.folderBrowserDialog1.SelectedPath;
                this.textBoxOutput.Text = outputFolder;
            }
        }

        private void btnDangAmbigs_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = "Set Path to DangAmbigs.txt.";
            this.folderBrowserDialog1.SelectedPath = outputFolder;

            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                outputFolder = this.folderBrowserDialog1.SelectedPath;
                this.textBoxOutput.Text = outputFolder;
            }

        }

        /// <summary>
        /// Changes localized text and messages
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="firstTime"></param>
        public virtual void ChangeUILanguage(string locale)
        {
            FormLocalizer localizer = new FormLocalizer(this, typeof(WatchForm));
            localizer.ApplyCulture(new CultureInfo(locale));
        }

    }
}
