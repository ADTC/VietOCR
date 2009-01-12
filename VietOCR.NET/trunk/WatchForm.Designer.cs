namespace VietOCR.NET
{
    partial class WatchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.textBoxWatch = new System.Windows.Forms.TextBox();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.btnWatch = new System.Windows.Forms.Button();
            this.btnOutput = new System.Windows.Forms.Button();
            this.checkBoxWatchEnabled = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // textBoxWatch
            // 
            this.textBoxWatch.Location = new System.Drawing.Point(95, 18);
            this.textBoxWatch.Name = "textBoxWatch";
            this.textBoxWatch.Size = new System.Drawing.Size(158, 20);
            this.textBoxWatch.TabIndex = 0;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(95, 52);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(158, 20);
            this.textBoxOutput.TabIndex = 1;
            // 
            // btnWatch
            // 
            this.btnWatch.Location = new System.Drawing.Point(259, 16);
            this.btnWatch.Name = "btnWatch";
            this.btnWatch.Size = new System.Drawing.Size(30, 23);
            this.btnWatch.TabIndex = 2;
            this.btnWatch.Text = "...";
            this.btnWatch.UseVisualStyleBackColor = true;
            this.btnWatch.Click += new System.EventHandler(this.btnWatch_Click);
            // 
            // btnOutput
            // 
            this.btnOutput.Location = new System.Drawing.Point(259, 50);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(30, 23);
            this.btnOutput.TabIndex = 3;
            this.btnOutput.Text = "...";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // checkBoxWatchEnabled
            // 
            this.checkBoxWatchEnabled.AutoSize = true;
            this.checkBoxWatchEnabled.Location = new System.Drawing.Point(24, 95);
            this.checkBoxWatchEnabled.Name = "checkBoxWatchEnabled";
            this.checkBoxWatchEnabled.Size = new System.Drawing.Size(94, 17);
            this.checkBoxWatchEnabled.TabIndex = 4;
            this.checkBoxWatchEnabled.Text = "Enable Watch";
            this.checkBoxWatchEnabled.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(214, 95);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Cancel";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Watch Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Output Folder:";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(133, 95);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // WatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 137);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.checkBoxWatchEnabled);
            this.Controls.Add(this.btnOutput);
            this.Controls.Add(this.btnWatch);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.textBoxWatch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WatchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Set Watch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxWatch;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Button btnWatch;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.CheckBox checkBoxWatchEnabled;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}