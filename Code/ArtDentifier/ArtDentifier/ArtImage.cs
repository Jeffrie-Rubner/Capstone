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

        public int getMeanValue(string colorName)
        {
            Dictionary<Color,int> colors = ColorOccurenceCounter.getAllColors();
            long colorCount = 0;
            long redValue = 0;
            long blueValue = 0;
            long greenValue = 0;
            foreach (KeyValuePair<Color,int> kvp in colors)
            {
                redValue += kvp.Key.R * kvp.Value;
                blueValue += kvp.Key.B * kvp.Value;
                greenValue += kvp.Key.G * kvp.Value;
                colorCount ++;
            }
            int returnValue = 0;
            if (colorCount > 0)
            {  
                if (colorName.ToLower() == "red")
                {
                    returnValue = (int)(redValue / colorCount);
                }
                else if (colorName.ToLower() == "blue")
                {
                    returnValue = (int)(blueValue / colorCount);
                }
                else if (colorName.ToLower() == "green")
                {
                    returnValue = (int)(blueValue / colorCount);
                }
            }
            return returnValue;
        }


    }
}
