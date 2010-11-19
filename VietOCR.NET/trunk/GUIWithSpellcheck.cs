﻿/**
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
        public GUIWithSpellcheck()
        {
            InitializeComponent();
        }

        protected override void toolStripButtonSpellCheck_Click(object sender, EventArgs e)
        {
            SpellChecker sp = new SpellChecker(this.textBox1, curLangCode);
            if (this.toolStripButtonSpellCheck.Checked)
            {
                sp.enableSpellCheck();
            }
            else
            {
                sp.disableSpellCheck();
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
