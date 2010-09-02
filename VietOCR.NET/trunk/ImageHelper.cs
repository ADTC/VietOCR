using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace VietOCR.NET
{
    class ImageHelper
    {
        public static Image Rescale(Image image, int dpiX, int dpiY)
        {
            Bitmap bm = new Bitmap((int)(image.Width * dpiX / image.HorizontalResolution), (int)(image.Height * dpiY / image.VerticalResolution));
            bm.SetResolution(dpiX, dpiY);
            Graphics g = Graphics.FromImage(bm);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(image, 0, 0);
            g.Dispose();

            return bm;
        }

        /// <summary>
        /// Get an Image from Clipboard
        /// </summary>
        /// <returns></returns>
        public static Image GetClipboardImage()
        {
            if (Clipboard.ContainsImage())
            {
                return Clipboard.GetImage();
            }
            return null;
        }

        /// <summary>
        /// Crop an image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="cropArea"></param>
        /// <returns></returns>
        //public static Image Crop(Image image, Rectangle cropArea)
        //{
        //    try
        //    {
        //        Bitmap bmp = new Bitmap(cropArea.Width, cropArea.Height);
        //        bmp.SetResolution(300, 300);

        //        Graphics gfx = Graphics.FromImage(bmp);
        //        gfx.SmoothingMode = SmoothingMode.AntiAlias;
        //        gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //        gfx.DrawImage(image, 0, 0, cropArea, GraphicsUnit.Pixel);
        //        gfx.Dispose();

        //        return bmp;
        //    }
        //    catch (Exception exc)
        //    {
        //        Console.WriteLine(exc.Message);
        //        return null;
        //    }
        //}

        /// <summary>
        /// Crop an image (another method).
        /// </summary>
        /// <param name="image"></param>
        /// <param name="cropArea"></param>
        /// <returns></returns>
        //public static Image Crop2(Image image, Rectangle cropArea)
        //{
        //    Bitmap bitmap = new Bitmap(image);
        //    bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
        //    return bitmap.Clone(cropArea, bitmap.PixelFormat);
        //}


    }
}
