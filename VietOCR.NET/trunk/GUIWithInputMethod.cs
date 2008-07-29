using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

using Net.SourceForge.Vietpad.InputMethod;

using Vietpad.NET.Controls;
using VietOCR.NET.Controls;

namespace VietOCR.NET
{
    public partial class GUIWithInputMethod : VietOCR.NET.GUIWithRegistry
    {
        string selectedInputMethod;
        ToolStripMenuItem miimChecked;

        const string strInputMethod = "InputMethod";

        public GUIWithInputMethod()
        {
            InitializeComponent();

            //
            // Keyboard InputMethod submenu
            //
            EventHandler eh = new EventHandler(MenuKeyboardInputMethodOnClick);

            List<ToolStripRadioButtonMenuItem> ar = new List<ToolStripRadioButtonMenuItem>();

            foreach (string inputMethod in Enum.GetNames(typeof(InputMethods)))
            {
                ToolStripRadioButtonMenuItem miim = new ToolStripRadioButtonMenuItem();
                miim.Text = inputMethod;
                miim.CheckOnClick = true;
                miim.Click += eh;
                ar.Add(miim);
            }

            vietInputMethodToolStripMenuItem.DropDownItems.AddRange(ar.ToArray());
            this.textBox1.KeyPress += new KeyPressEventHandler(new VietKeyHandler(this.textBox1).OnKeyPress);

        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            for (int i = 0; i < vietInputMethodToolStripMenuItem.DropDownItems.Count; i++)
            {
                if (vietInputMethodToolStripMenuItem.DropDownItems[i].Text == selectedInputMethod)
                {
                    // Select InputMethod last saved
                    miimChecked = (ToolStripMenuItem)vietInputMethodToolStripMenuItem.DropDownItems[i];
                    miimChecked.Checked = true;
                    break;
                }
            }

            VietKeyHandler.InputMethod = (InputMethods)Enum.Parse(typeof(InputMethods), selectedInputMethod);
            VietKeyHandler.SmartMark = true;
            VietKeyHandler.ConsumeRepeatKey = true;
        }

        void MenuKeyboardInputMethodOnClick(object obj, EventArgs ea)
        {
            miimChecked.Checked = false;
            miimChecked = (ToolStripMenuItem)obj;
            miimChecked.Checked = true;
            selectedInputMethod = miimChecked.Text;
            VietKeyHandler.InputMethod = (InputMethods)Enum.Parse(typeof(InputMethods), selectedInputMethod);
        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);
            selectedInputMethod = (string)regkey.GetValue(strInputMethod, Enum.GetName(typeof(InputMethods), InputMethods.Telex));
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);
            regkey.SetValue(strInputMethod, selectedInputMethod);
        }
        
    }
}

