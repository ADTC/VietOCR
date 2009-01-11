using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VietOCR.NET
{
    public partial class WatchForm : Form
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

        public WatchForm()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Set Watch Folder";
            openFileDialog1.InitialDirectory = watchFolder;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Set Output Folder";
            openFileDialog1.InitialDirectory = outputFolder;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

            }
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            this.textBoxWatch.Text = watchFolder;
            this.textBoxOutput.Text = outputFolder;
            this.checkBoxWatchEnabled.Checked = watchEnabled;
        }

        protected override void OnClosed(EventArgs ea)
        {
            base.OnClosed(ea);

            watchFolder =this.textBoxWatch.Text;
            outputFolder = this.textBoxOutput.Text;
            watchEnabled = this.checkBoxWatchEnabled.Checked;
        }
    }
}
