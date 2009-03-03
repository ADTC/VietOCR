using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace VietOCR.NET
{
    public partial class GUIWithFormat : VietOCR.NET.GUIWithRegistry
    {
        const string strSelectedCase = "SelectedCase";

        private string selectedCase;

        public GUIWithFormat()
        {
            InitializeComponent();
        }

        protected override void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            mi.Checked ^= true;
            this.textBox1.WordWrap = mi.Checked;
        }

        protected override void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontdlg = new FontDialog();

            fontdlg.ShowColor = true;
            fontdlg.Font = this.textBox1.Font;
            fontdlg.Color = this.textBox1.ForeColor;

            if (fontdlg.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Font = fontdlg.Font;
                this.textBox1.ForeColor = fontdlg.Color;
            }
        }

        protected override void changeCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OwnedForms.Length > 0)
                return;

            textBox1.HideSelection = false;

            ChangeCaseDialog1 changeCaseDlg = new ChangeCaseDialog1();
            changeCaseDlg.Owner = this;
            changeCaseDlg.SelectedCase = selectedCase;
            changeCaseDlg.ChangeCase += new EventHandler(ChangeCaseDialogChangeCase);
            changeCaseDlg.CloseDlg += new EventHandler(ChangeCaseDialogCloseDlg);

            if (textBox1.SelectedText == "")
            {
                textBox1.SelectAll();
            }
            changeCaseDlg.Show();
        }

        void ChangeCaseDialogChangeCase(object obj, EventArgs ea)
        {
            if (textBox1.SelectedText == "")
            {
                textBox1.SelectAll();
                return;
            }

            ChangeCaseDialog1 dlg = (ChangeCaseDialog1)obj;
            selectedCase = dlg.SelectedCase;
            changeCase(selectedCase);
        }
        
        void ChangeCaseDialogCloseDlg(object obj, EventArgs ea)
        {
            textBox1.HideSelection = true;
            this.Focus();
        }

        /**
          *  Change Case
          *
          *@param  string typeOfCase
          */
        private void changeCase(string typeOfCase)
        {
            string result = textBox1.SelectedText;
            int start = textBox1.SelectionStart;

            if (typeOfCase == "UPPER CASE")
            {
                result = result.ToUpper();
            }
            else if (typeOfCase == "lower case")
            {
                result = result.ToLower();
            }
            else if (typeOfCase == "Title Case")
            {
                StringBuilder strB = new StringBuilder(result.ToLower());

                Regex regex = new Regex("(?<!\\p{IsCombiningDiacriticalMarks}|\\p{L})\\p{L}");      // word boundary \\b\\w
                MatchCollection mc = regex.Matches(result);

                // Loop through  the match collection to retrieve all 
                // matches and positions.
                for (int i = 0; i < mc.Count; i++)
                {
                    int index = mc[i].Index;
                    strB[index] = Char.ToUpper(strB[index]);
                }

                result = strB.ToString();
            }
            else if (typeOfCase == "Sentence case")
            {
                StringBuilder strB = new StringBuilder(result.ToUpper() == result ? result.ToLower() : result);
                Regex regex = new Regex("\\p{L}(\\p{L}+)");
                MatchCollection mc = regex.Matches(result);

                for (Match m = regex.Match(result); m.Success; m = m.NextMatch())
                {
                    if (!(
                        m.Groups[0].Value.ToUpper() == m.Groups[0].Value ||
                        m.Groups[1].Value.ToLower() == m.Groups[1].Value
                        ))
                    {
                        for (int i = 0; i < mc.Count; i++)
                        {
                            int j = mc[i].Index;
                            strB[j] = Char.ToLower(strB[j]);
                        }
                    }
                }

                const string QUOTE = "\"'`,<>\u00AB\u00BB\u2018-\u203A";
                regex = new Regex("(?:[.?!\u203C-\u2049][])}"
                    + QUOTE + "]*|^|\n|:\\s+["
                    + QUOTE + "])[-=_*\u2010-\u2015\\s]*["
                    + QUOTE + "\\[({]*\\p{L}"
                    ); // begin of a sentence                  

                // Use the Matches method to find all matches in the input string.
                mc = regex.Matches(result);
                // Loop through  the match collection to retrieve all 
                // matches and positions.
                for (int i = 0; i < mc.Count; i++)
                {
                    int j = mc[i].Index + mc[i].Length - 1;
                    strB[j] = Char.ToUpper(strB[j]);
                }

                result = strB.ToString();
            }
            else
            {
                return;
            }

            textBox1.SelectedText = result;
            textBox1.SelectionStart = start;
            textBox1.SelectionLength = result.Length;
        }

        protected override void removeLineBreaksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectedText == "")
            {
                textBox1.SelectAll();
                if (textBox1.SelectedText == "") return;
            }

            int start = textBox1.SelectionStart;

            Regex regex = new Regex("(?<=\n|^)[\t ]+|[\t ]+(?=$|\n)");
            string result = regex.Replace(textBox1.SelectedText, "");
            regex = new Regex("(?<=.)\n(?=.)");
            result = regex.Replace(result, " ");

            textBox1.SelectedText = result;
            textBox1.Select(start, result.Length);
        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);
            selectedCase = (string)regkey.GetValue(strSelectedCase, String.Empty);
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);
            regkey.SetValue(strSelectedCase, selectedCase);
        }
    }
}
