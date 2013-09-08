using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ProperArtdentifier
{
    class Clusterer
    {
        List<BitmapImage> imagesToCluster;

        public Clusterer()
        {
            imagesToCluster = new List<BitmapImage>();
        }

        public void Cluster()
        {

        }

        public void ReadInData()
        {
            string dir = @"c:\Users\Jeff\Documents\GitHub\Capstone\Documentation\Misc\TeachingImages\";
            string[] filePaths = Directory.GetFiles(dir);
            foreach (string str in filePaths)
            {
                Uri artistLocation = new Uri(str);
                try
                {
                    BitmapImage myBitmapImage = GetBitmapImage(artistLocation);
                    imagesToCluster.Add(myBitmapImage);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private BitmapImage GetBitmapImage(Uri uri)
        {
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = uri;
            myBitmapImage.EndInit();
            return myBitmapImage;
        }

    }
}
