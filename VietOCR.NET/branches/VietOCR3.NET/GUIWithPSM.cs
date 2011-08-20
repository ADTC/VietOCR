using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Vietpad.NET.Controls;

namespace VietOCR.NET
{
    public partial class GUIWithPSM : VietOCR.NET.GUIWithSettings
    {
        String psModes = "0 - Orientation and script detection (OSD) only;"
        + "1 - Automatic page segmentation with OSD;"
        + "2 - Automatic page segmentation, but no OSD, or OCR;"
        + "3 - Fully automatic page segmentation, but no OSD (default);"
        + "4 - Assume a single column of text of variable sizes;"
        + "5 - Assume a single uniform block of vertically aligned text;"
        + "6 - Assume a single uniform block of text;"
        + "7 - Treat the image as a single text line;"
        + "8 - Treat the image as a single word;"
        + "9 - Treat the image as a single word in a circle;"
        + "10 - Treat the image as a single character";

        ToolStripMenuItem psmItemChecked;

        public GUIWithPSM()
        {
            currentPSM = "3";
            InitializeComponent();

            //
            // Settings PSM submenu
            //
            EventHandler eh = new EventHandler(MenuPSMOnClick);

            List<ToolStripRadioButtonMenuItem> ar = new List<ToolStripRadioButtonMenuItem>();

            foreach (string mode in psModes.Split(';'))
            {
                ToolStripRadioButtonMenuItem psmItem = new ToolStripRadioButtonMenuItem();
                psmItem.Text = mode;
                psmItem.CheckOnClick = true;
                psmItem.Click += eh;
                ar.Add(psmItem);
            }

            this.psmToolStripMenuItem.DropDownItems.AddRange(ar.ToArray());
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            for (int i = 0; i < this.psmToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (this.psmToolStripMenuItem.DropDownItems[i].Text.StartsWith(currentPSM))
                {
                    // Select PSM last saved
                    psmItemChecked = (ToolStripMenuItem)psmToolStripMenuItem.DropDownItems[i];
                    psmItemChecked.Checked = true;
                    break;
                }
            }
        }

        void MenuPSMOnClick(object obj, EventArgs ea)
        {
            psmItemChecked.Checked = false;
            psmItemChecked = (ToolStripMenuItem)obj;
            psmItemChecked.Checked = true;
            currentPSM = psmItemChecked.Text;
        }
    }
}
