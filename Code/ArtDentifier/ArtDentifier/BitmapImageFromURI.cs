using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ArtDentifier
{
    static class BitmapImageFromURI
    {
        public static BitmapImage GetBitmapImage(Uri uri)
        {
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = uri;
            myBitmapImage.EndInit();
            return myBitmapImage;
        }
    }
}
