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
        double Width;
        double Height;
        ColorDictionary ColorOccurenceCounter = new ColorDictionary();

        #region Constructor
        public ArtImage(BitmapImage bitmapImage)
        {
            ActualImage = bitmapImage;
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
