/**
 * http://www.codeproject.com/KB/graphics/Deskew_an_Image.aspx
 */

using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Imaging
{
    public unsafe class FastBitmap
    {
        private Bitmap bitmap;
        private int bitmapWidth;
        private Byte* pBase = null;
        private BitmapData bitmapData = null;

        public struct PixelData
        {
            public byte blue;
            public byte green;
            public byte red;
        }

        public FastBitmap(Image image)
        {
            Init(new Bitmap(image));
        }

        public FastBitmap(Bitmap bmp)
        {
            Init(bmp);
        }

        public void Init(Bitmap bmp)
        {
            this.bitmap = bmp;
            pBase = null;
            bitmapData = null;
        }

        public int Width
        {
            get { return bitmap.Width; }
        }

        public int Height
        {
            get { return bitmap.Height; }
        }

        public Bitmap Bitmap
        {
            get { return bitmap; }
        }

        public void SetPixel(int X, int Y, Color Colour)
        {
            try
            {
                PixelData* p = PixelAt(X, Y);
                p->red = Colour.R;
                p->green = Colour.G;
                p->blue = Colour.B;
            }
            catch (AccessViolationException ave)
            {
                throw (ave);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Color GetPixel(int X, int Y)
        {
            try
            {
                PixelData* p = PixelAt(X, Y);
                return Color.FromArgb((int)p->red, (int)p->green, (int)p->blue);
            }
            catch (AccessViolationException ave)
            {
                throw (ave);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LockBitmap()
        {
            GraphicsUnit unit = GraphicsUnit.Pixel;
            RectangleF boundsF = bitmap.GetBounds(ref unit);
            Rectangle bounds = new Rectangle((int)boundsF.X,
            (int)boundsF.Y,
            (int)boundsF.Width,
            (int)boundsF.Height);

            bitmapWidth = (int)boundsF.Width * sizeof(PixelData);
            if (bitmapWidth % 4 != 0)
            {
                bitmapWidth = 4 * (bitmapWidth / 4 + 1);
            }

            bitmapData = bitmap.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            pBase = (Byte*)bitmapData.Scan0.ToPointer();
        }

        private PixelData* PixelAt(int x, int y)
        {
            if (pBase == null)
                LockBitmap();

            return (PixelData*)(pBase + y * bitmapWidth + x * sizeof(PixelData));
        }

        public void UnlockBitmap()
        {
            if (bitmapData != null)
                bitmap.UnlockBits(bitmapData);

            bitmapData = null;
            pBase = null;
        }
    }
}