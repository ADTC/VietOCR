namespace VietOCR.NET
{
    partial class GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oCRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oCRAllPagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.postprocessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wordWrapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnOCR = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripCbLang = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lblCurIndex = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnPrev = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnFitImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnFitHeight = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnFitWidth = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pictureBox1 = new VietOCR.NET.Controls.ScrollablePictureBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.commandToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 31);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 27);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(227, 28);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(227, 28);
            this.saveToolStripMenuItem.Text = "Save...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(224, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(227, 28);
            this.quitToolStripMenuItem.Text = "Exit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // commandToolStripMenuItem
            // 
            this.commandToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oCRToolStripMenuItem,
            this.oCRAllPagesToolStripMenuItem,
            this.toolStripMenuItem2,
            this.postprocessToolStripMenuItem});
            this.commandToolStripMenuItem.Name = "commandToolStripMenuItem";
            this.commandToolStripMenuItem.Size = new System.Drawing.Size(107, 27);
            this.commandToolStripMenuItem.Text = "Command";
            // 
            // oCRToolStripMenuItem
            // 
            this.oCRToolStripMenuItem.Name = "oCRToolStripMenuItem";
            this.oCRToolStripMenuItem.Size = new System.Drawing.Size(215, 28);
            this.oCRToolStripMenuItem.Text = "OCR";
            this.oCRToolStripMenuItem.Click += new System.EventHandler(this.oCRToolStripMenuItem_Click);
            // 
            // oCRAllPagesToolStripMenuItem
            // 
            this.oCRAllPagesToolStripMenuItem.Name = "oCRAllPagesToolStripMenuItem";
            this.oCRAllPagesToolStripMenuItem.Size = new System.Drawing.Size(215, 28);
            this.oCRAllPagesToolStripMenuItem.Text = "OCR All Pages";
            this.oCRAllPagesToolStripMenuItem.Click += new System.EventHandler(this.oCRAllPagesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(212, 6);
            // 
            // postprocessToolStripMenuItem
            // 
            this.postprocessToolStripMenuItem.Name = "postprocessToolStripMenuItem";
            this.postprocessToolStripMenuItem.Size = new System.Drawing.Size(215, 28);
            this.postprocessToolStripMenuItem.Text = "Post-process";
            this.postprocessToolStripMenuItem.Click += new System.EventHandler(this.postprocessToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wordWrapToolStripMenuItem,
            this.fontToolStripMenuItem
            
            });
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(89, 27);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.settingsToolStripMenuItem_DropDownOpening);
            // 
            // wordWrapToolStripMenuItem
            // 
            this.wordWrapToolStripMenuItem.Name = "wordWrapToolStripMenuItem";
            this.wordWrapToolStripMenuItem.Size = new System.Drawing.Size(251, 28);
            this.wordWrapToolStripMenuItem.Text = "Word Wrap";
            this.wordWrapToolStripMenuItem.Click += new System.EventHandler(this.wordWrapToolStripMenuItem_Click);
            // 
            // fontToolStripMenuItem
            // 
            this.fontToolStripMenuItem.Name = "fontToolStripMenuItem";
            this.fontToolStripMenuItem.Size = new System.Drawing.Size(251, 28);
            this.fontToolStripMenuItem.Text = "Font...";
            this.fontToolStripMenuItem.Click += new System.EventHandler(this.fontToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.aboutToolStripMenuItem1,
            this.aboutToolStripMenuItem2});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(71, 27);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(262, 28);
            this.helpToolStripMenuItem.Text = "VietOCR.NET Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(259, 6);
            // 
            // aboutToolStripMenuItem2
            // 
            this.aboutToolStripMenuItem2.Name = "aboutToolStripMenuItem2";
            this.aboutToolStripMenuItem2.Size = new System.Drawing.Size(262, 28);
            this.aboutToolStripMenuItem2.Text = "About VietOCR.NET";
            this.aboutToolStripMenuItem2.Click += new System.EventHandler(this.aboutToolStripMenuItem2_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 544);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnOpen,
            this.toolStripBtnOCR,
            this.toolStripBtnClear,
            this.toolStripSeparator1,
            this.toolStripCbLang,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 31);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 31);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolStripBtnOpen
            // 
            this.toolStripBtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnOpen.Image")));
            this.toolStripBtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnOpen.Name = "toolStripBtnOpen";
            this.toolStripBtnOpen.Size = new System.Drawing.Size(59, 28);
            this.toolStripBtnOpen.Tag = this.openToolStripMenuItem;
            this.toolStripBtnOpen.Text = "Open";
            this.toolStripBtnOpen.ToolTipText = "Open";
            // 
            // toolStripBtnOCR
            // 
            this.toolStripBtnOCR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBtnOCR.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnOCR.Image")));
            this.toolStripBtnOCR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnOCR.Name = "toolStripBtnOCR";
            this.toolStripBtnOCR.Size = new System.Drawing.Size(50, 28);
            this.toolStripBtnOCR.Tag = this.oCRToolStripMenuItem;
            this.toolStripBtnOCR.Text = "OCR";
            this.toolStripBtnOCR.ToolTipText = "OCR";
            // 
            // toolStripBtnClear
            // 
            this.toolStripBtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBtnClear.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnClear.Image")));
            this.toolStripBtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnClear.Name = "toolStripBtnClear";
            this.toolStripBtnClear.Size = new System.Drawing.Size(56, 28);
            this.toolStripBtnClear.Text = "Clear";
            this.toolStripBtnClear.ToolTipText = "Clear";
            this.toolStripBtnClear.Click += new System.EventHandler(this.toolStripBtnClear_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 100, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripCbLang
            // 
            this.toolStripCbLang.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripCbLang.Name = "toolStripCbLang";
            this.toolStripCbLang.Size = new System.Drawing.Size(121, 31);
            this.toolStripCbLang.SelectedIndexChanged += new System.EventHandler(this.toolStripCbLang_SelectedIndexChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(135, 28);
            this.toolStripLabel1.Text = "OCR Language";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 62);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Size = new System.Drawing.Size(792, 482);
            this.splitContainer1.SplitterDistance = 383;
            this.splitContainer1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.splitContainer2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(43, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(340, 482);
            this.panel1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lblCurIndex);
            this.splitContainer2.Panel1MinSize = 20;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AllowDrop = true;
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer2.Panel2.DragOver += new System.Windows.Forms.DragEventHandler(this.splitContainer2_Panel2_DragOver);
            this.splitContainer2.Panel2.DragDrop += new System.Windows.Forms.DragEventHandler(this.splitContainer2_Panel2_DragDrop);
            this.splitContainer2.Size = new System.Drawing.Size(340, 482);
            this.splitContainer2.SplitterDistance = 20;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 5;
            // 
            // lblCurIndex
            // 
            this.lblCurIndex.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCurIndex.Location = new System.Drawing.Point(134, 5);
            this.lblCurIndex.Name = "lblCurIndex";
            this.lblCurIndex.Size = new System.Drawing.Size(70, 13);
            this.lblCurIndex.TabIndex = 4;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnPrev,
            this.toolStripBtnNext,
            this.toolStripBtnFitImage,
            this.toolStripBtnFitHeight,
            this.toolStripBtnFitWidth,
            this.toolStripBtnZoomIn,
            this.toolStripBtnZoomOut});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(43, 482);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripBtnPrev
            // 
            this.toolStripBtnPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBtnPrev.Enabled = false;
            this.toolStripBtnPrev.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnPrev.Image")));
            this.toolStripBtnPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnPrev.Name = "toolStripBtnPrev";
            this.toolStripBtnPrev.Size = new System.Drawing.Size(40, 27);
            this.toolStripBtnPrev.Text = "<";
            this.toolStripBtnPrev.ToolTipText = "Previous Page";
            this.toolStripBtnPrev.Click += new System.EventHandler(this.toolStripBtnPrev_Click);
            // 
            // toolStripBtnNext
            // 
            this.toolStripBtnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBtnNext.Enabled = false;
            this.toolStripBtnNext.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnNext.Image")));
            this.toolStripBtnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnNext.Name = "toolStripBtnNext";
            this.toolStripBtnNext.Size = new System.Drawing.Size(40, 27);
            this.toolStripBtnNext.Text = ">";
            this.toolStripBtnNext.ToolTipText = "Next Page";
            this.toolStripBtnNext.Click += new System.EventHandler(this.toolStripBtnNext_Click);
            // 
            // toolStripBtnFitImage
            // 
            this.toolStripBtnFitImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBtnFitImage.Enabled = false;
            this.toolStripBtnFitImage.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripBtnFitImage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnFitImage.Image")));
            this.toolStripBtnFitImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnFitImage.Name = "toolStripBtnFitImage";
            this.toolStripBtnFitImage.Size = new System.Drawing.Size(40, 18);
            this.toolStripBtnFitImage.Text = "┼";
            this.toolStripBtnFitImage.ToolTipText = "Fit Image";
            this.toolStripBtnFitImage.Click += new System.EventHandler(this.toolStripBtnFitImage_Click);
            // 
            // toolStripBtnFitHeight
            // 
            this.toolStripBtnFitHeight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBtnFitHeight.Enabled = false;
            this.toolStripBtnFitHeight.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripBtnFitHeight.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnFitHeight.Image")));
            this.toolStripBtnFitHeight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnFitHeight.Name = "toolStripBtnFitHeight";
            this.toolStripBtnFitHeight.Size = new System.Drawing.Size(40, 18);
            this.toolStripBtnFitHeight.Text = "↕";
            this.toolStripBtnFitHeight.ToolTipText = "Fit Height";
            this.toolStripBtnFitHeight.Click += new System.EventHandler(this.toolStripBtnFitHeight_Click);
            // 
            // toolStripBtnFitWidth
            // 
            this.toolStripBtnFitWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBtnFitWidth.Enabled = false;
            this.toolStripBtnFitWidth.Font = new System.Drawing.Font("Arial", 8.25F);
            this.toolStripBtnFitWidth.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnFitWidth.Image")));
            this.toolStripBtnFitWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnFitWidth.Name = "toolStripBtnFitWidth";
            this.toolStripBtnFitWidth.Size = new System.Drawing.Size(40, 18);
            this.toolStripBtnFitWidth.Text = "↔";
            this.toolStripBtnFitWidth.ToolTipText = "Fit Width";
            this.toolStripBtnFitWidth.Click += new System.EventHandler(this.toolStripBtnFitWidth_Click);
            // 
            // toolStripBtnZoomIn
            // 
            this.toolStripBtnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBtnZoomIn.Enabled = false;
            this.toolStripBtnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnZoomIn.Image")));
            this.toolStripBtnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnZoomIn.Name = "toolStripBtnZoomIn";
            this.toolStripBtnZoomIn.Size = new System.Drawing.Size(40, 27);
            this.toolStripBtnZoomIn.Text = "(+)";
            this.toolStripBtnZoomIn.ToolTipText = "Zoom In";
            // 
            // toolStripBtnZoomOut
            // 
            this.toolStripBtnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBtnZoomOut.Enabled = false;
            this.toolStripBtnZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnZoomOut.Image")));
            this.toolStripBtnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnZoomOut.Name = "toolStripBtnZoomOut";
            this.toolStripBtnZoomOut.Size = new System.Drawing.Size(40, 27);
            this.toolStripBtnZoomOut.Text = "(-)";
            this.toolStripBtnZoomOut.ToolTipText = "Zoom Out";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(405, 482);
            this.textBox1.TabIndex = 0;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "VietOCR.NET";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripBtnOpen;
        private System.Windows.Forms.ToolStripButton toolStripBtnOCR;
        private System.Windows.Forms.ToolStripButton toolStripBtnClear;
        private System.Windows.Forms.SplitContainer splitContainer1;
        protected System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem commandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oCRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oCRAllPagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem postprocessToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wordWrapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        protected System.Windows.Forms.ToolStripComboBox toolStripCbLang;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripBtnPrev;
        private System.Windows.Forms.ToolStripButton toolStripBtnNext;
        private System.Windows.Forms.ToolStripButton toolStripBtnFitImage;
        private System.Windows.Forms.ToolStripButton toolStripBtnFitHeight;
        private System.Windows.Forms.ToolStripButton toolStripBtnFitWidth;
        private System.Windows.Forms.ToolStripButton toolStripBtnZoomIn;
        private System.Windows.Forms.ToolStripButton toolStripBtnZoomOut;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label lblCurIndex;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private VietOCR.NET.Controls.ScrollablePictureBox pictureBox1;
    }
}