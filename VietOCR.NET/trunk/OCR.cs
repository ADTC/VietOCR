using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;
using System.ComponentModel;

namespace VietOCR.NET
{
    class OCR
    {
        Rectangle rect = Rectangle.Empty;
        ManualResetEvent m_event;
        BackgroundWorker worker;

        public string RecognizeText(IList<Image> images, int index, string lang, Rectangle selection, BackgroundWorker worker, DoWorkEventArgs e)
        {
            rect = selection;
            return RecognizeText(images, index, lang, worker, e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="images">list of images</param>
        /// <param name="index">index of page (frame) of image; -1 for all</param>
        /// <param name="lang">the language OCR is going to be performed for</param>
        /// <returns>result text</returns>
        public string RecognizeText(IList<Image> images, int index, string lang, BackgroundWorker worker, DoWorkEventArgs e)
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

            using (tessnet2.Tesseract ocr = new tessnet2.Tesseract())
            {
                ocr.Init(lang, false);

                IList<Image> workingImages;

                if (index == -1)
                {
                    workingImages = images; // all images
                }
                else
                {
                    workingImages = new List<Image>();
                    workingImages.Add(images[index]); // specific image
                }

                StringBuilder strB = new StringBuilder();

                foreach (Bitmap image in workingImages)
                {
                    // If the OcrDone delegate is not null then this'll be the multithreaded version
                    //ocr.OcrDone = new tessnet2.Tesseract.OcrDoneHandler(Finished);
                    // For event to work, must use the multithreaded version
                    //ocr.ProgressEvent += new tessnet2.Tesseract.ProgressHandler(ProgressEvent);

                    m_event = new ManualResetEvent(false);

                    List<tessnet2.Word> result = ocr.DoOCR(image, rect);

                    // Wait here it's finished
                    //m_event.WaitOne();

                    if (result == null) return String.Empty;

                    for (int i = 0; i < tessnet2.Tesseract.LineCount(result); i++)
                    {
                        strB.AppendLine(tessnet2.Tesseract.GetLineText(result, i));
                    }

                    //int lineIndex = 0;
                    //foreach (tessnet2.Word word in result)
                    //{
                    //    if (lineIndex != word.LineIndex)
                    //    {
                    //        strB.AppendLine();
                    //        lineIndex = word.LineIndex;
                    //    }
                    //    strB.Append(new string(' ', word.Blanks)).Append(word.Text);
                    //}
                    //strB.AppendLine();
                }
                return strB.ToString();
            }
        }
        public void Finished(List<tessnet2.Word> result)
        {
            m_event.Set();
        }

        void ProgressEvent(int percent)
        {
            worker.ReportProgress(percent);
        }
    }
}
