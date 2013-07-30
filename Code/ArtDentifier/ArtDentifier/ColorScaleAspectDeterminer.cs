using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
/*
 * This Class measures the average color of a bitmapimage
 */
namespace ArtDentifier
{
    class ColorScaleAspectDeterminer
    {
        #region PublicMethods
        public Color determineBitMean(ArtImage bitmap)
        {
            Color mean;
            byte[] pixels = ConvertBitmapImageToByteArray(bitmap.getBitmapImage());
            int stride = bitmap.PixelWidth * 4;
            int meanR = 0;
            int meanG = 0;
            int meanB = 0;
            int meanA = 0;
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    int index = (y * stride) + (4 * x);
                    meanR += pixels[index];
                    meanG += pixels[index + 1];
                    meanB += pixels[index + 2];
                    meanA += pixels[index + 3];
                }
            }

            mean = Color.FromArgb((byte)meanA, (byte)meanR, (byte)meanG, (byte)meanB);

            return mean;
        }
        #endregion

        #region PrivateMethods
        private byte[] ConvertBitmapImageToByteArray(BitmapImage bitmap)
        {
            int stride = bitmap.PixelWidth * 4;
            int size = bitmap.PixelHeight * stride;
            byte[] pixels = new byte[size];
            bitmap.CopyPixels(pixels, stride, 0);
            return pixels;
        }
        #endregion
    }
}
