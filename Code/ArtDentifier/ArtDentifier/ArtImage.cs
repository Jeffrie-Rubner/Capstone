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
        Dictionary<Color, int> ColorOccurenceCounter = new Dictionary<Color, int>();


        public ArtImage(BitmapImage bitmapImage)
        {

        }

        public BitmapImage getBitmapImage()
        {
            return ActualImage;
        }

        public void countColor(Color color)
        {
            if (ColorOccurenceCounter.ContainsKey(color))
            {

            }
            else
            {
                ColorOccurenceCounter
            }
        }
    }
}
