using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ArtDentifier
{
    class ArtImage
    {
        private BitmapImage ActualImage;
        private double width;
        public double Width {get{return width;} private set{width = value;}}
        private double height;
        public double Height {get{return height;} private set{height = value;}}
        private ColorDictionary ColorOccurenceCounter;
        private ColorDivider IndividualColorTracker;

        #region Constructor
        public ArtImage(BitmapImage bitmapImage)
        {
            ColorOccurenceCounter = new ColorDictionary();
            ActualImage = bitmapImage;
            Width = ActualImage.PixelWidth;
            Height = ActualImage.PixelHeight;
            IndividualColorTracker = new ColorDivider();
        }

        private ArtImage(BitmapImage bitmapImage, ColorDictionary colorDictionary)
        {
            ColorOccurenceCounter = colorDictionary;
            ActualImage = bitmapImage;
            Width = ActualImage.PixelWidth;
            Height = ActualImage.PixelHeight;
            IndividualColorTracker = new ColorDivider();
        }
        #endregion

        public BitmapImage getBitmapImage()
        {
            return ActualImage;
        }

        #region ColorDividerWrappers
        public byte getMostFrequentRed()
        {
            return IndividualColorTracker.getMostFrequentRed();
        }

        public byte getMostFrequentGreen()
        {
            return IndividualColorTracker.getMostFrequentGreen();
        }

        public byte getMostFrequentBlue()
        {
            return IndividualColorTracker.getMostFrequentBlue();
        }

        public byte getMostFrequentAlpha()
        {
            return IndividualColorTracker.getMostFrequentAlpha();
        }
        #endregion

        public void countColor(Color color)
        {
            ColorOccurenceCounter.addColor(color);
            IndividualColorTracker.addColor(color);
        }

        public Color getMostFrequentColor()
        {
            return ColorOccurenceCounter.getMostFrequentColor();
        }

        public ArtImage clone()
        {
            return new ArtImage(this.ActualImage, this.ColorOccurenceCounter);
        }




    }
}
