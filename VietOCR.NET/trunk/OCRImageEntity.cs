using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace VietOCR.NET
{
    class OCRImageEntity
    {
        IList<Image> images;

        public IList<Image> Images
        {
            get { return images; }
            set { images = value; }
        }

        int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        Rectangle rect;

        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }

        String lang;

        public String Lang
        {
            get { return lang; }
            set { lang = value; }
        }

        public OCRImageEntity(IList<Image> images, int index, Rectangle rect, String lang)
        {
            this.images = images;
            this.index = index;
            this.rect = rect;
            this.lang = lang;
        }
    }
}
