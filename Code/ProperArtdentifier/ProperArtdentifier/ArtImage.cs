using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProperArtdentifier
{
    public class ArtImage : IClusterableItem
    {
        #region Fields and Properties
        public readonly string KnownArtistName;
        private BitmapImage ActualImage;

        public override byte Red() { return getMostFrequentRed(); }
        public override byte Blue() { return getMostFrequentBlue(); }
        public override byte Green() { return getMostFrequentGreen(); }

        private ColorDivider IndividualColorTracker;
        #endregion

        #region Constructor
        public ArtImage(BitmapImage bitmapImage, string artistName)
        {
            KnownArtistName = artistName;
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
        private byte getMostFrequentRed()
        {
            return IndividualColorTracker.getMostFrequentRed();
        }

        private byte getMostFrequentGreen()
        {
            return IndividualColorTracker.getMostFrequentGreen();
        }

        private byte getMostFrequentBlue()
        {
            return IndividualColorTracker.getMostFrequentBlue();
        }
        #endregion

        public void countColor(Color color)
        {
            IndividualColorTracker.addColor(color);
        }
    }
}
