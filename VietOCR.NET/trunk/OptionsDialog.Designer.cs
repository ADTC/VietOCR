namespace VietOCR.NET
{
    partial class OptionsDialog
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBoxWatch = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOutput = new System.Windows.Forms.Button();
            this.btnWatch = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.textBoxWatch = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBoxDangAmbigs = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDangAmbigs = new System.Windows.Forms.Button();
            this.textBoxDangAmbigs = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(292, 144);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBoxWatch);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnOutput);
            this.tabPage1.Controls.Add(this.btnWatch);
            this.tabPage1.Controls.Add(this.textBoxOutput);
            this.tabPage1.Controls.Add(this.textBoxWatch);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(284, 118);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Watch";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBoxWatch
            // 
            this.checkBoxWatch.AutoSize = true;
            this.checkBoxWatch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxWatch.Location = new System.Drawing.Point(82, 88);
            this.checkBoxWatch.Name = "checkBoxWatch";
            this.checkBoxWatch.Size = new System.Drawing.Size(59, 17);
            this.checkBoxWatch.TabIndex = 14;
            this.checkBoxWatch.Text = "Enable";
            this.checkBoxWatch.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(8, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Output Folder:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Watch Folder:";
            // 
            // btnOutput
            // 
            this.btnOutput.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput.Location = new System.Drawing.Point(246, 50);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(30, 23);
            this.btnOutput.TabIndex = 11;
            this.btnOutput.Text = "...";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnWatch
            // 
            this.btnWatch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnWatch.Location = new System.Drawing.Point(246, 16);
            this.btnWatch.Name = "btnWatch";
            this.btnWatch.Size = new System.Drawing.Size(30, 23);
            this.btnWatch.TabIndex = 10;
            this.btnWatch.Text = "...";
            this.btnWatch.UseVisualStyleBackColor = true;
            this.btnWatch.Click += new System.EventHandler(this.btnWatch_Click);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxOutput.Location = new System.Drawing.Point(82, 52);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.Size = new System.Drawing.Size(158, 20);
            this.textBoxOutput.TabIndex = 9;
            // 
            // textBoxWatch
            // 
            this.textBoxWatch.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxWatch.Location = new System.Drawing.Point(82, 18);
            this.textBoxWatch.Name = "textBoxWatch";
            this.textBoxWatch.ReadOnly = true;
            this.textBoxWatch.Size = new System.Drawing.Size(158, 20);
            this.textBoxWatch.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBoxDangAmbigs);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.btnDangAmbigs);
            this.tabPage2.Controls.Add(this.textBoxDangAmbigs);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(284, 118);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "DangAmbigs.txt";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBoxDangAmbigs
            // 
            this.checkBoxDangAmbigs.AutoSize = true;
            this.checkBoxDangAmbigs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxDangAmbigs.Location = new System.Drawing.Point(55, 52);
            this.checkBoxDangAmbigs.Name = "checkBoxDangAmbigs";
            this.checkBoxDangAmbigs.Size = new System.Drawing.Size(59, 17);
            this.checkBoxDangAmbigs.TabIndex = 18;
            this.checkBoxDangAmbigs.Text = "Enable";
            this.checkBoxDangAmbigs.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(17, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Path:";
            // 
            // btnDangAmbigs
            // 
            this.btnDangAmbigs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDangAmbigs.Location = new System.Drawing.Point(228, 15);
            this.btnDangAmbigs.Name = "btnDangAmbigs";
            this.btnDangAmbigs.Size = new System.Drawing.Size(30, 23);
            this.btnDangAmbigs.TabIndex = 16;
            this.btnDangAmbigs.Text = "...";
            this.btnDangAmbigs.UseVisualStyleBackColor = true;
            this.btnDangAmbigs.Click += new System.EventHandler(this.btnDangAmbigs_Click);
            // 
            // textBoxDangAmbigs
            // 
            this.textBoxDangAmbigs.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxDangAmbigs.Location = new System.Drawing.Point(55, 17);
            this.textBoxDangAmbigs.Name = "textBoxDangAmbigs";
            this.textBoxDangAmbigs.ReadOnly = true;
            this.textBoxDangAmbigs.Size = new System.Drawing.Size(167, 20);
            this.textBoxDangAmbigs.TabIndex = 15;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(124, 158);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(205, 158);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(8, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Watch Folder:";
            // 
            // OptionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 197);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControl1);
            this.Name = "OptionsDialog";
            this.Text = "Options";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.Button btnWatch;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.TextBox textBoxWatch;
        private System.Windows.Forms.CheckBox checkBoxWatch;
        private System.Windows.Forms.CheckBox checkBoxDangAmbigs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDangAmbigs;
        private System.Windows.Forms.TextBox textBoxDangAmbigs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}