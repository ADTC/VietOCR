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
using tesseract;
using System.IO;
using System.Diagnostics;

namespace VietOCR.NET
{
    class OCRFiles
    {
        Rectangle rect = Rectangle.Empty;
        BackgroundWorker worker;

        /// <summary>
        /// Recognizes TIFF files.
        /// </summary>
        /// <param name="tiffFiles"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        string RecognizeText(IList<FileInfo> tiffFiles, string lang)
        {
            string tempTessOutputFile = Path.GetTempFileName();
            File.Delete(tempTessOutputFile);
            tempTessOutputFile = Path.ChangeExtension(tempTessOutputFile, ".txt");
            string outputFileName = Path.GetFileNameWithoutExtension(tempTessOutputFile); // chop the .txt extension
            FileInfo fiTempTessOutputFile = new FileInfo(tempTessOutputFile);

            // Start the child process.
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.FileName = "tesseract.exe";
            p.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            StringBuilder result = new StringBuilder();

            foreach (FileInfo tiffFile in tiffFiles)
            {
                p.StartInfo.Arguments = string.Format("{0} {1} -l {2}", tiffFile.FullName, outputFileName, lang);
                p.Start();

                // Read the output stream first and then wait.
                string output = p.StandardOutput.ReadToEnd(); // ignore standard output
                string error = p.StandardError.ReadToEnd(); // 

                p.WaitForExit();

                if (p.ExitCode == 0)
                {
                    using (StreamReader sr = new StreamReader(tempTessOutputFile, Encoding.UTF8, true))
                    {
                        result.Append(sr.ReadToEnd());
                    }
                }
                else
                {
                    fiTempTessOutputFile.Delete();
                    if (error.Trim().Length == 0)
                    {
                        error = "Errors occurred.";
                    }
                    throw new ApplicationException(error);
                }
            }

            fiTempTessOutputFile.Delete();
            return result.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="images">list of images</param>
        /// <param name="index">index of page (frame) of image; -1 for all</param>
        /// <param name="lang">the language OCR is going to be performed for</param>
        /// <returns>result text</returns>
        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public string RecognizeText(IList<FileInfo> tiffFiles, string lang, BackgroundWorker worker, DoWorkEventArgs e)
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

            return RecognizeText(tiffFiles, lang);
        }

        void ProgressEvent(int percent)
        {
            worker.ReportProgress(percent);
        }
    }
}
