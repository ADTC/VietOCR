using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace VietOCR.NET
{
    class OCRImages : OCR<Image>
    {
        /// <summary>
        /// Recognize text
        /// </summary>
        /// <param name="imageEntities"></param>
        /// <param name="index"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override string RecognizeText(IList<Image> imageEntities, string lang)
        {
            //tessnet3.Tesseract ocr = new tessnet3.Tesseract();

            //ocr.Init(null, lang, 3);

            //StringBuilder strB = new StringBuilder();

            //foreach (Image image in imageEntities)
            //{
            //    string result = ocr.DoOCR(image, rect);

            //    if (result == null) return String.Empty;
            //    strB.Append(result);

            //}
            //return strB.ToString();
            return null;
        }
    }
}
