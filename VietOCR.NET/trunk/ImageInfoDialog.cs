using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VietOCR.NET
{
    public partial class ImageInfoDialog : Form
    {
        Image image;

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        public ImageInfoDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            this.textBoxXRes.Text = this.image.HorizontalResolution.ToString();
            this.textBoxYRes.Text = this.image.VerticalResolution.ToString();
            this.textBoxWidth.Text = this.image.Width.ToString();
            this.textBoxHeight.Text = this.image.Height.ToString();

        }

        protected override void OnClosed(EventArgs ea)
        {
            base.OnClosed(ea);

        }
    }
}
