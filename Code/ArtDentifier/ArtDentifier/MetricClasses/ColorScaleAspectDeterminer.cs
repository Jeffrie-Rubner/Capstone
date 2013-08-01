using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

/*
 * This Class counts the frequency of color in a bitmapimage
 */
namespace ArtDentifier
{
    class ColorScaleAspectDeterminer
    {
        #region PublicMethods
        //checks every pixel in an artImage, and counts each time a specific color occurs
        public void determineBitFrequency(ArtImage artImage)
        {
            BitmapImage bitmap = artImage.getBitmapImage();
            byte[] pixels = ConvertBitmapImageToByteArray(bitmap);
            int stride = bitmap.PixelWidth * 4;
            int currentR = 0;
            int currentG = 0;
            int currentB = 0;
            int currentA = 0;
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    int index = (y * stride) + (4 * x);
                    currentR = pixels[index];
                    currentG = pixels[index + 1];
                    currentB = pixels[index + 2];
                    currentA = pixels[index + 3];
                    Color currentColor = Color.FromArgb((byte)currentA, (byte)currentR, (byte)currentG, (byte)currentB);
                    artImage.countColor(currentColor);
                }
            }
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
