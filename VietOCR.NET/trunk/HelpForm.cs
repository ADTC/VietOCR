//-------------------------------------------
// HelpForm.cs © 2003 by Quan Nguyen for VietPad.NET
// Version: 1.1, 27 Mar 06
// See: http://vietpad.sourceforge.net
//-------------------------------------------
using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace VietOCR.NET
{
    /// <summary>
    /// Summary description for HelpForm.
    /// </summary>
    public class HelpForm : Form
    {
        private RichTextBox richTextBox;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private string helpFileName;

        public HelpForm(string helpFileName, string title)
        {
            this.helpFileName = helpFileName;
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            this.Text = title;
            //			this.richTextBox.LoadFile(Path.Combine(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName, "readme.rtf"), RichTextBoxStreamType.RichText);
            this.richTextBox.LoadFile(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("VietOCR.NET.Resources." + helpFileName), RichTextBoxStreamType.RichText);

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBox
            // 
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(792, 566);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            this.richTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox_LinkClicked);
            // 
            // HelpForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.richTextBox);
            this.Name = "HelpForm";
            this.ResumeLayout(false);

        }
        #endregion

        private void richTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            string linkText = e.LinkText;
            string urlProtocol = "file://"; // fake as embedded resources

            try
            {
                if (linkText.StartsWith(urlProtocol) && linkText.EndsWith(".rtf"))
                {
                    this.richTextBox.LoadFile(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("VietOCR.NET.Resources." + linkText.Substring(urlProtocol.Length)), RichTextBoxStreamType.RichText);
                }
                else
                {
                    System.Diagnostics.Process.Start(e.LinkText);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}