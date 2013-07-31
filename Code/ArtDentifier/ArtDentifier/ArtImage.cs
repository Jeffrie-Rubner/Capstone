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
        public double Width {get{return width;}, private set{width = value;}}
        private double height;
        public double Height {get{return height;} private set{height = value;}}
        ColorDictionary ColorOccurenceCounter;

        #region Constructor
        public ArtImage(BitmapImage bitmapImage)
        {
            ColorOccurenceCounter = new ColorDictionary();
            ActualImage = bitmapImage;
            Width = ActualImage.PixelWidth;
            Height = ActualImage.PixelHeight;
        }
        #endregion

        public BitmapImage getBitmapImage()
        {
            return ActualImage;
        }

        public void countColor(Color color)
        {
            ColorOccurenceCounter.addColor(color);
        }
    }
}
