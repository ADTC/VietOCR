namespace VietOCR.NET
{
    partial class GUIWithInputMethod
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
            this.components = new System.ComponentModel.Container();

            //
            // toolStripMenuItem3
            //
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            //
            // vietInputMethodToolStripMenuItem
            //
            this.vietInputMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.vietInputMethodToolStripMenuItem
            });

            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(248, 6);
            // 
            // vietInputMethodToolStripMenuItem
            // 
            this.vietInputMethodToolStripMenuItem.Name = "vietInputMethodToolStripMenuItem";
            this.vietInputMethodToolStripMenuItem.Size = new System.Drawing.Size(251, 28);
            this.vietInputMethodToolStripMenuItem.Text = "Viet Input Method";

        }

        #endregion

        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem vietInputMethodToolStripMenuItem;

    }
}
