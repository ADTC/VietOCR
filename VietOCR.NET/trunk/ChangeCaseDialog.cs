//-------------------------------------------
// ChangeCaseDialog.cs � 2003 by Quan Nguyen for VietPad.NET
// Version: 1.0.2, 10 May 04
// See: http://vietpad.sourceforge.net
//-------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Resources;

namespace VietOCR.NET
{
    /// <summary>
    /// Summary description for ChangeCaseDialog.
    /// </summary>
    public class ChangeCaseDialog : Form
    {
        private Button btnChange;
        private Button btnClose;
        private RadioButton[] radioButtons;

        private Panel panel1;
        private string selectedCase;

        public event EventHandler CloseDlg;
        public event EventHandler ChangeCase;

        public string SelectedCase
        {
            set { selectedCase = value; }
            get { return selectedCase; }
        }
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        ResourceManager resources;

        public ChangeCaseDialog()
        {
            //resources = new ResourceManager("VietPad.NET.Resource", typeof(ChangeCaseDialog).Assembly);
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

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
            this.btnChange = new Button();
            this.btnClose = new Button();
            this.panel1 = new Panel();
            this.SuspendLayout();
            // 
            // btnChange
            // 
            this.btnChange.Location = new Point(5, 120);
            this.btnChange.Name = "btnChange";
            this.btnChange.TabIndex = 1;
            this.btnChange.Text = "Change"; // resources.GetString("Change");
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new Point(85, 120);
            this.btnClose.Name = "btnClose";
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close"; // resources.GetString("Close");
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            string[] cases = {
								 "Sentence_case",
								 "lowercase",
								 "UPPERCASE",
								 "Title_Case"
							 };

            radioButtons = new RadioButton[cases.Length];

            for (int i = 0; i < cases.Length; i++)
            {
                radioButtons[i] = new RadioButton();
                radioButtons[i].Location = new Point(8, 16 + 24 * i);
                radioButtons[i].Size = new Size(120, 20);
                radioButtons[i].Name = cases[i];
                radioButtons[i].TabIndex = i;
                radioButtons[i].Text = cases[i]; // resources.GetString(cases[i]);
                this.panel1.Controls.Add(radioButtons[i]);
            }
            // 
            // panel1
            // 
            this.panel1.Location = new Point(4, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(160, 112);
            this.panel1.TabIndex = 3;
            // 
            // ChangeCaseDialog
            // 
            this.AcceptButton = this.btnChange;
            this.AutoScaleBaseSize = new Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new Size(164, 156);
            this.Controls.AddRange(new Control[] {
																		  this.panel1,
																		  this.btnClose,
																		  this.btnChange});
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeCaseDialog";
            this.Text = "Change Case"; // resources.GetString("Change_Case");
            this.Load += new System.EventHandler(this.ChangeCaseDialog_Load);
            this.ResumeLayout(false);

        }
        #endregion
        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            for (int i = 0; i < radioButtons.Length; i++)
            {
                if (radioButtons[i].Name == selectedCase)
                {
                    // Select Case last saved
                    radioButtons[i].Checked = true;
                    break;
                }
            }
        }
        private void btnChange_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < radioButtons.Length; i++)
            {
                if (radioButtons[i].Checked)
                {
                    selectedCase = radioButtons[i].Name;
                    break;
                }
            }

            if (ChangeCase != null)
                ChangeCase(this, EventArgs.Empty);
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            if (CloseDlg != null)
                CloseDlg(this, EventArgs.Empty);

            Close();
        }

        private void ChangeCaseDialog_Load(object sender, System.EventArgs e)
        {
            int locationX = this.Owner.Location.X + this.Owner.Width / 2 - this.Width / 2;
            int locationY = this.Owner.Location.Y + this.Owner.Height / 2 - this.Height / 2;
            this.Location = new Point(locationX, locationY);
        }
    }

}