/**
 * Copyright @ 2008 Quan Nguyen
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VietOCR.NET
{
    public partial class GUIWithSpellcheck : VietOCR.NET.GUIWithSettings
    {
        private SpellChecker sp;
        private string misspelled;

        public GUIWithSpellcheck()
        {
            InitializeComponent();
        }

        protected override void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            this.contextMenuStrip1.Items.Clear();

            string[] sug = { "test", "test2" };
            foreach (string word in sug)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(word);
                item.Click += new EventHandler(item_Click);
                this.contextMenuStrip1.Items.Add(item);
            }
            this.contextMenuStrip1.Items.Add("-");

            ToolStripMenuItem item1 = new ToolStripMenuItem("Ignore All");
            item1.Tag = "ignore";
            item1.Click += new EventHandler(item_Click);
            this.contextMenuStrip1.Items.Add(item1);

            item1 = new ToolStripMenuItem("Add to Dictionary");
            item1.Tag = "add";
            item1.Click += new EventHandler(item_Click);
            this.contextMenuStrip1.Items.Add(item1);

            this.contextMenuStrip1.Items.Add("-");

            base.contextMenuStrip1_Opening(sender, e);
        }

        void item_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            string command = item.Tag.ToString();
            if (command == "ignore")
            {
                sp.IgnoreWord(misspelled);
            }
            else if (command == "add")
            {
                sp.AddWord(misspelled);
            }
            else
            {
                int index = this.textBox1.GetCharIndexFromPosition(pointClicked);
                
                this.textBox1.Select(0, 0);
                this.textBox1.SelectedText = item.Text;
            }
            sp.SpellCheck();
        }

        protected override void toolStripButtonSpellCheck_Click(object sender, EventArgs e)
        {
            sp = new SpellChecker(this.textBox1, curLangCode);

            if (this.toolStripButtonSpellCheck.Checked)
            {
                sp.EnableSpellCheck();
            }
            else
            {
                sp.DisableSpellCheck();
            }
            this.textBox1.Refresh();


            //NHunspellExtender.NHunspellTextBoxExtender myNhunspellExtender = new NHunspellExtender.NHunspellTextBoxExtender();
            ////myNhunspellExtender.AddNewLanguage();
            //myNhunspellExtender.SetLanguage("vi_VN");

            //if (this.toolStripButtonSpellCheck.Checked)
            //{
            //    myNhunspellExtender.EnableTextBoxBase(this.textBox1);
            //}
            //else
            //{
            //    //myNhunspellExtender.DisableTextBoxBase(this.textBox1);
            //    myNhunspellExtender.Dispose();
            //    myNhunspellExtender = null;
            //    this.textBox1.Refresh();
            //}
        }
    }
}
