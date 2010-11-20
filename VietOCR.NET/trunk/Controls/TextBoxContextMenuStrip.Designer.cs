//
// Adapted from http://bytes.com/topic/c-sharp/answers/265836-custom-paste-context-menu-fvor-text-boxes
//

using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace VietOCR.NET.Controls
{
    /// <summary>
    /// 
    /// </summary>
    partial class TextBoxContextMenuStrip
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.miUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.miSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.miCut = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.miSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.miSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            //
            // miUndo
            //
            this.miUndo.Text = "&Undo";
            this.miUndo.Click += new System.EventHandler(this.miUndo_Click);
            //
            // miCut
            //
            this.miCut.Text = "Cu&t";
            this.miCut.Click += new System.EventHandler(this.miCut_Click);
            //
            // miCopy
            //
            this.miCopy.Text = "&Copy";
            this.miCopy.Click += new System.EventHandler(this.miCopy_Click);
            //
            // miPaste
            //
            this.miPaste.Text = "&Paste";
            this.miPaste.Click += new System.EventHandler(this.miPaste_Click);
            //
            // miDelete
            //
            this.miDelete.Text = "&Delete";
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            //
            // miSelectAll
            //
            this.miSelectAll.Text = "Select &All";
            this.miSelectAll.Click += new System.EventHandler(this.miSelectAll_Click);

            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.miUndo,
                this.miSeparator,
                this.miCut,
                this.miCopy,
                this.miPaste,
                this.miDelete,
                this.miSeparator2,
                this.miSelectAll
            });

            this.Opening += new System.ComponentModel.CancelEventHandler(StandardTextBoxContextMenuStrip_Opening);
        }

        public void RepopulateContextMenu()
        {
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.miUndo,
                this.miSeparator,
                this.miCut,
                this.miCopy,
                this.miPaste,
                this.miDelete,
                this.miSeparator2,
                this.miSelectAll
            });
        }

        void StandardTextBoxContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Get the text box that the context menu was popped on
            if (this.SourceControl is TextBox)
            {
                TextBox clickedBox = (TextBox)this.SourceControl;

                // Enable and disable standard menu items as necessary
                bool isSelection = clickedBox.SelectionLength > 0;
                IDataObject clipObject = Clipboard.GetDataObject();
                bool textOnClipboard = clipObject.GetDataPresent(DataFormats.Text);

                this.miUndo.Enabled = clickedBox.CanUndo;
                this.miCut.Enabled = isSelection;
                this.miCopy.Enabled = isSelection;
                this.miPaste.Enabled = textOnClipboard;
                this.miDelete.Enabled = isSelection;
            }
        }

        private void miUndo_Click(object sender, System.EventArgs e)
        {
            // Get the text box that the context menu was popped on
            if (this.SourceControl is TextBox)
            {
                TextBox clickedBox = (TextBox)this.SourceControl;

                if (clickedBox.CanUndo)
                {
                    clickedBox.Undo();
                }
            }
        }

        private void miCut_Click(object sender, System.EventArgs e)
        {
            // Get the text box that the context menu was popped on
            if (this.SourceControl is TextBox)
            {
                TextBox clickedBox = (TextBox)this.SourceControl;

                if (clickedBox.SelectionLength > 0)
                {
                    clickedBox.Cut();
                }
            }
        }

        private void miCopy_Click(object sender, System.EventArgs e)
        {
            // Get the text box that the context menu was popped on
            if (this.SourceControl is TextBox)
            {
                TextBox clickedBox = (TextBox)this.SourceControl;

                if (clickedBox.SelectionLength > 0)
                {
                    clickedBox.Copy();
                }
            }
        }

        private void miPaste_Click(object sender, System.EventArgs e)
        {
            // Get the text box that the context menu was popped on
            if (this.SourceControl is TextBox)
            {
                TextBox clickedBox = (TextBox)this.SourceControl;
                clickedBox.Paste();
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(System.IntPtr hWnd, int msg, int lParam, int wParam);
        private const int WM_CLEAR = 0x0303;

        private void miDelete_Click(object sender, System.EventArgs e)
        {
            // Get the text box that the context menu was popped on
            if (this.SourceControl is TextBox)
            {
                TextBox clickedBox = (TextBox)this.SourceControl;

                if (clickedBox.SelectionLength > 0)
                {
                    SendMessage(clickedBox.Handle, WM_CLEAR, 0, 0);
                }
            }
        }

        private void miSelectAll_Click(object sender, System.EventArgs e)
        {
            // Get the text box that the context menu was popped on
            if (this.SourceControl is TextBox)
            {
                TextBox clickedBox = (TextBox)this.SourceControl;
                clickedBox.SelectAll();
            }
        }
        #endregion

        private System.Windows.Forms.ToolStripMenuItem miUndo;
        private System.Windows.Forms.ToolStripMenuItem miCut;
        private System.Windows.Forms.ToolStripMenuItem miCopy;
        private System.Windows.Forms.ToolStripMenuItem miPaste;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
        private System.Windows.Forms.ToolStripMenuItem miSelectAll;
        private System.Windows.Forms.ToolStripSeparator miSeparator;
        private System.Windows.Forms.ToolStripSeparator miSeparator2;
    }
}
