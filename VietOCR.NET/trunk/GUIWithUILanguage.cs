using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vietpad.NET.Controls;
using System.Globalization;

namespace VietOCR.NET
{
    public partial class GUIWithUILanguage : VietOCR.NET.GUIWithInputMethod
    {
        ToolStripMenuItem miuilChecked;

        public GUIWithUILanguage()
        {
            InitializeComponent();

            //
            // Settings UI Language submenu
            //
            EventHandler eh1 = new EventHandler(MenuKeyboardUILangOnClick);

            List<ToolStripRadioButtonMenuItem> ar = new List<ToolStripRadioButtonMenuItem>();

            String[] uiLangs = { "en-US", "lt-LT", "vi-VN" };
            foreach (string uiLang in uiLangs)
            {
                ToolStripRadioButtonMenuItem miuil = new ToolStripRadioButtonMenuItem();
                CultureInfo ci = new CultureInfo(uiLang);
                miuil.Tag = ci.Name;
                miuil.Text = ci.Parent.DisplayName + " (" + ci.Parent.NativeName + ")";
                miuil.CheckOnClick = true;
                miuil.Click += eh1;
                ar.Add(miuil);
            }

            this.uiLanguageToolStripMenuItem.DropDownItems.AddRange(ar.ToArray());
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            for (int i = 0; i < this.uiLanguageToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (this.uiLanguageToolStripMenuItem.DropDownItems[i].Tag.ToString() == selectedUILanguage)
                {
                    // Select UI Language last saved
                    miuilChecked = (ToolStripMenuItem)uiLanguageToolStripMenuItem.DropDownItems[i];
                    miuilChecked.Checked = true;
                    break;
                }
            }
        }

        void MenuKeyboardUILangOnClick(object obj, EventArgs ea)
        {
            miuilChecked.Checked = false;
            miuilChecked = (ToolStripMenuItem)obj;
            miuilChecked.Checked = true;
            if (selectedUILanguage != miuilChecked.Tag.ToString())
            {
                selectedUILanguage = miuilChecked.Tag.ToString();
                ChangeUILanguage(selectedUILanguage);
            }
        }
    }
}
