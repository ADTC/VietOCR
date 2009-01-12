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
using System.Timers;
using System.Threading;
using System.IO;
using Microsoft.Win32;

namespace VietOCR.NET
{
    public partial class GUIWithWatch : VietOCR.NET.GUIWithInputMethod
    {
        const string strWatchEnable = "WatchEnable";
        const string strWatchFolder = "WatchFolder";
        const string strOutputFolder = "OutputFolder";

        private Queue<String> queue;
        private string watchFolder;
        private string outputFolder;
        private bool watchEnabled;

        WatchForm form;

        public GUIWithWatch()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs ea)
        {
            base.OnLoad(ea);

            queue = new Queue<String>();
            Watcher watcher = new Watcher(queue, watchFolder);

            System.Timers.Timer aTimer = new System.Timers.Timer(5000);
            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Enabled = true;
        }

        // Specify what you want to happen when the Elapsed event is raised.
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (queue.Count > 0)
            {
                Thread t = new Thread(new ThreadStart(AutoOCR));
                t.Start();
            }
        }

        private void AutoOCR()
        {
            imageFile = new FileInfo(queue.Dequeue());
            loadImage(imageFile);

            try
            {
                OCRImageEntity entity = new OCRImageEntity(imageList, -1, Rectangle.Empty, curLangCode);
                OCR ocrEngine = new OCR();
                string result = ocrEngine.RecognizeText(entity.Images, entity.Index, entity.Lang);

                using (StreamWriter sw = new StreamWriter(Path.Combine(outputFolder, imageFile.Name + ".txt"), false, new System.Text.UTF8Encoding()))
                {
                    sw.Write(result);
                }
            }
            catch
            {
                //ignore
            }
        }

        protected override void watchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form == null)
            {
                form = new WatchForm();
            }

            form.WatchFolder = watchFolder;
            form.OutputFolder = outputFolder;
            form.WatchEnabled = watchEnabled;

            if (form.ShowDialog() == DialogResult.OK)
            {
                watchFolder = form.WatchFolder;
                outputFolder = form.OutputFolder;
                watchEnabled = form.WatchEnabled;
            }
        }

        protected override void LoadRegistryInfo(RegistryKey regkey)
        {
            base.LoadRegistryInfo(regkey);
            watchEnabled = Convert.ToBoolean((int)regkey.GetValue(strWatchEnable, Convert.ToInt32(false)));
            watchFolder = (string)regkey.GetValue(strWatchFolder, System.Configuration.ConfigurationManager.AppSettings["WatchFolder"]);
            outputFolder = (string)regkey.GetValue(strOutputFolder, System.Configuration.ConfigurationManager.AppSettings["OutputFolder"]);
        }

        protected override void SaveRegistryInfo(RegistryKey regkey)
        {
            base.SaveRegistryInfo(regkey);
            regkey.SetValue(strWatchEnable, Convert.ToInt32(watchEnabled));
            regkey.SetValue(strWatchFolder, watchFolder);
            regkey.SetValue(strOutputFolder, outputFolder);
        }
    }
}
