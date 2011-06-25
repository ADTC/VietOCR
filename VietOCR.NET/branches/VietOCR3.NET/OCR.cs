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
using System.Text;
using System.Drawing;
using System.Threading;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;

namespace VietOCR.NET
{
    class OCR
    {
        Rectangle rect = Rectangle.Empty;
        BackgroundWorker worker;

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string RecognizeText(IList<Image> images, string lang, Rectangle selection)
        {
            rect = selection;
            return RecognizeText(images, lang);
        }
        /// <summary>
        /// Recognize text
        /// </summary>
        /// <param name="images"></param>
        /// <param name="index"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string RecognizeText(IList<Image> images, string lang)
        {
            //tessnet3.Tesseract ocr = new tessnet3.Tesseract();

            //ocr.Init(null, lang, 3);

            //StringBuilder strB = new StringBuilder();

            //foreach (Bitmap image in images)
            //{
            //    string result = ocr.DoOCR(image, rect);

            //    if (result == null) return String.Empty;
            //    strB.Append(result);

            //}
            //return strB.ToString();
            return null;
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string RecognizeText(IList<Image> images, string lang, Rectangle selection, BackgroundWorker worker, DoWorkEventArgs e)
        {
            rect = selection;
            return RecognizeText(images, lang, worker, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="images">list of images</param>
        /// <param name="index">index of page (frame) of image; -1 for all</param>
        /// <param name="lang">the language OCR is going to be performed for</param>
        /// <returns>result text</returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string RecognizeText(IList<Image> images, string lang, BackgroundWorker worker, DoWorkEventArgs e)
        {
            // Abort the operation if the user has canceled.
            // Note that a call to CancelAsync may have set 
            // CancellationPending to true just after the
            // last invocation of this method exits, so this 
            // code will not have the opportunity to set the 
            // DoWorkEventArgs.Cancel flag to true. This means
            // that RunWorkerCompletedEventArgs.Cancelled will
            // not be set to true in your RunWorkerCompleted
            // event handler. This is a race condition.
            this.worker = worker;

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return String.Empty;
            }

            return RecognizeText(images, lang);
        }

        void ProgressEvent(int percent)
        {
            worker.ReportProgress(percent);
        }
    }
}
