using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace VietOCR.NET
{
    public partial class GUIWithRegistry : VietOCR.NET.GUI
    {
        string strRegKey = "Software\\VietUnicode\\";
        const string strWinState = "WindowState";
        const string strLocationX = "LocationX";
        const string strLocationY = "LocationY";
        const string strWidth = "Width";
        const string strHeight = "Height";
        const string strOcrLanguage = "OcrLanguage";

        const string strWordWrap = "WordWrap";
        const string strFontFace = "FontFace";
        const string strFontSize = "FontSize";
        const string strFontStyle = "FontStyle";
        const string strForeColor = "ForeColor";
        const string strBackColor = "BackColor";

        Rectangle rectNormal;

        public GUIWithRegistry()
        {
            InitializeComponent();
            rectNormal = DesktopBounds;
        }

        protected override void OnMove(EventArgs ea)
        {
            base.OnMove(ea);

            if (WindowState == FormWindowState.Normal)
                rectNormal = DesktopBounds;
        }
        protected override void OnResize(EventArgs ea)
        {
            base.OnResize(ea);

            if (WindowState == FormWindowState.Normal)
                rectNormal = DesktopBounds;
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            // Construct complete registry key.

            strRegKey += strProgName;

            // Load registry information.

            RegistryKey regkey = Registry.CurrentUser.OpenSubKey(strRegKey);

            if (regkey == null)
                regkey = Registry.CurrentUser.CreateSubKey(strRegKey);

            LoadRegistryInfo(regkey);
            regkey.Close();
        }

        protected override void OnClosed(EventArgs ea)
        {
            base.OnClosed(ea);

            // Save registry information.

            RegistryKey regkey = Registry.CurrentUser.OpenSubKey(strRegKey, true);

            if (regkey == null)
                regkey = Registry.CurrentUser.CreateSubKey(strRegKey);

            SaveRegistryInfo(regkey);
            regkey.Close();
        }
        protected virtual void SaveRegistryInfo(RegistryKey regkey)
        {
            regkey.SetValue(strWinState, (int)WindowState);
            regkey.SetValue(strLocationX, rectNormal.X);
            regkey.SetValue(strLocationY, rectNormal.Y);
            regkey.SetValue(strWidth, rectNormal.Width);
            regkey.SetValue(strHeight, rectNormal.Height);
            regkey.SetValue(strOcrLanguage, this.toolStripCbLang.SelectedIndex);

            regkey.SetValue(strWordWrap, Convert.ToInt32(this.textBox1.WordWrap));
            regkey.SetValue(strFontFace, this.textBox1.Font.Name);
            regkey.SetValue(strFontSize, this.textBox1.Font.SizeInPoints.ToString());
            regkey.SetValue(strFontStyle, (int)this.textBox1.Font.Style);
            regkey.SetValue(strForeColor, this.textBox1.ForeColor.ToArgb());
            regkey.SetValue(strBackColor, this.textBox1.BackColor.ToArgb());

        }

        protected virtual void LoadRegistryInfo(RegistryKey regkey)
        {
            int x = (int)regkey.GetValue(strLocationX, 100);
            int y = (int)regkey.GetValue(strLocationY, 100);
            int cx = (int)regkey.GetValue(strWidth, 324);
            int cy = (int)regkey.GetValue(strHeight, 300);

            this.toolStripCbLang.SelectedIndex = (int)regkey.GetValue(strOcrLanguage, -1);

            rectNormal = new Rectangle(x, y, cx, cy);

            // Adjust rectangle for any change in desktop size.

            Rectangle rectDesk = SystemInformation.WorkingArea;

            rectNormal.Width = Math.Min(rectNormal.Width, rectDesk.Width);
            rectNormal.Height = Math.Min(rectNormal.Height, rectDesk.Height);
            rectNormal.X -= Math.Max(rectNormal.Right - rectDesk.Right, 0);
            rectNormal.Y -= Math.Max(rectNormal.Bottom - rectDesk.Bottom, 0);

            // Set form properties.

            DesktopBounds = rectNormal;
            WindowState = (FormWindowState)regkey.GetValue(strWinState, 0);

            this.textBox1.WordWrap = Convert.ToBoolean(
                (int)regkey.GetValue(strWordWrap, Convert.ToInt32(true)));
            this.textBox1.Font = new Font((string)regkey.GetValue(strFontFace, "Microsoft Sans Serif"),
                float.Parse((string)regkey.GetValue(strFontSize, "10")),
                (FontStyle)regkey.GetValue(strFontStyle, FontStyle.Regular));
            this.textBox1.ForeColor = Color.FromArgb(
                (int)regkey.GetValue(strForeColor, Color.FromKnownColor(KnownColor.Black).ToArgb()));
            this.textBox1.BackColor = Color.FromArgb(
                (int)regkey.GetValue(strBackColor, Color.FromKnownColor(KnownColor.White).ToArgb()));

        }
    }
}

