using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ProperArtdentifier
{
    class Clusterer
    {
        private static readonly int CLUSTER_COUNT = 5;
        private static int EXPECTED_IMAGE_COUNT = 100;

        private List<ArtImage> imagesToCluster;
        private Dictionary<string, List<ArtImage>> clusters = new Dictionary<string, List<ArtImage>>();

        #region Metric Class Fields
        private ColorScaleAspectDeterminer colorSAD = new ColorScaleAspectDeterminer();
        #endregion

        public Clusterer()
        {
            imagesToCluster = new List<ArtImage>();
            Initialize();
        }

        public Dictionary<string, List<ArtImage>> Cluster()
        {
            IClusterableItem[] clusterPoints = new IClusterableItem[CLUSTER_COUNT];
            for (int i = 0; i < CLUSTER_COUNT; i++)
            {
                clusterPoints[i] = ClusterPoint.getRandomClusterPoint();
            }
            int loopCount = 0;
            bool ChangeOccurred = false;
            do
            {
                ChangeOccurred = false;
                foreach (ArtImage art_image in imagesToCluster)
                {
                    double bestDistance = 10000;
                    string clusterName = "cluster0";
                    for (int i = 0; i < CLUSTER_COUNT; i++)
                    {
                        double currentDistance = getDistance(clusterPoints[i], art_image);
                        if (currentDistance <= bestDistance)
                        {
                            bestDistance = currentDistance;
                            clusterName = "cluster" + (i+1);
                        }
                    }
                    clusters[clusterName].Add(art_image);
                }
                IClusterableItem[] newClusterPoints = new IClusterableItem[CLUSTER_COUNT];
                for (int i = 0; i < CLUSTER_COUNT; i++)
                {
                    String clusterNum = "cluster" + (i+1);
                    newClusterPoints[i] = getAverageClusterPointFromArtImageList(clusters[clusterNum]);
                }

                for (int i = 0; i < CLUSTER_COUNT; i++)
                {
                    if (!newClusterPoints[i].IsEqual(clusterPoints[i]))
                    {
                        ChangeOccurred = true;
                        clusterPoints[i] = newClusterPoints[i];
                    }
                }
                if (ChangeOccurred)
                {
                    clusters = new Dictionary<string, List<ArtImage>>();
                    CreateClusters();
                }
                Console.WriteLine(++loopCount);
            }
            while (ChangeOccurred);
            return clusters;
        }

        public string AnalyzePicture(ArtImage artImage)
        {
            string clusterName = "";
            double bestDistance = 10000;
            for (int i = 1; i <= CLUSTER_COUNT; i++)
            {
                string clusterNum = "cluster" + i;
                IClusterableItem ici = getAverageClusterPointFromArtImageList(clusters[clusterNum]);
                double currentDistance = getDistance(ici, artImage);
                if (currentDistance <= bestDistance)
                {
                    bestDistance = currentDistance;
                    clusterName = "Cluster " + i;
                }
            }
            return clusterName;
        }

        public ClusterPoint getAverageClusterPointFromArtImageList(List<ArtImage> artImageItems)
        {
            double width = 0;
            double height = 0;
            int red = 0;
            int blue = 0;
            int green = 0;
            int divisor = 0;
            ClusterPoint returnClusterPoint;
            foreach (ArtImage ai in artImageItems)
            {
                width += ai.Width;
                height += ai.Height;
                red += ai.Red();
                blue += ai.Blue();
                green += ai.Green();
                divisor++;
            }
            if (divisor != 0)
            {
                width = width / divisor;
                height = height / divisor;
                red = red / divisor;
                green = green / divisor;
                blue = blue / divisor;
                byte redByte = (byte)red;
                byte blueByte = (byte)blue;
                byte greenByte = (byte)green;
                returnClusterPoint = new ClusterPoint(width, height, redByte, greenByte, blueByte);
            }
            else
            {
                returnClusterPoint = ClusterPoint.getRandomClusterPoint();
            }
            return returnClusterPoint;
        }

        public double getDistance(IClusterableItem image1, IClusterableItem image2)
        {
            double distance = 0;

            double heightDistance = Math.Pow((image2.Height - image1.Height), 2);
            double widthDistance = Math.Pow((image2.Width - image1.Width), 2);
            double redDistance = Math.Pow((image2.Red() - image1.Red()), 2);
            double greenDistance = Math.Pow((image2.Green() - image1.Green()), 2);
            double blueDistance = Math.Pow((image2.Blue() - image1.Blue()), 2);

            distance = Math.Sqrt((heightDistance + widthDistance + redDistance + greenDistance + blueDistance));

            return distance;
        }


        #region Initializers
        private void Initialize()
        {
            CreateClusters();
            InitializeColors();
            ReadInData();
        }

        private void ReadInData()
        {
            string dir = @"c:\Users\Jeff\Documents\GitHub\Capstone\Documentation\Misc\ClusterableImages\";
            string[] filePaths = Directory.GetFiles(dir);
            int i = 1;
            foreach (string str in filePaths)
            {
                if (isAnImageType(str))
                {
                    Thread t = new Thread(new ParameterizedThreadStart(this.AddImageToSet));
                    t.Name = "Image Number: " + (i++);
                    t.Start(str);
                }
            }
        }

        private void AddImageToSet(object obj)
        {
            string str = (string)obj;
            Uri artistLocation = new Uri(str);
            try
            {
            BitmapImage myBitmapImage = BitmapImageFromURI.GetBitmapImage(artistLocation);
            if (myBitmapImage != null)
            {
                imagesToCluster.Add(new ArtImage(myBitmapImage, str));
            }
            else
            {
                EXPECTED_IMAGE_COUNT--;
            }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Occurred with image " + str);
            }
        }

        private void InitializeColors()
        {
            foreach (ArtImage image in imagesToCluster)
            {
                new ColorScaleAspectDeterminer().determineBitFrequency(image);
            }
        }

        private void CreateClusters()
        {
            for (int i = 1; i <= CLUSTER_COUNT; i++)
            {
                string clusterName = "cluster" + (i);
                clusters.Add(clusterName, new List<ArtImage>());
            }
        }
        #endregion

        public string getImageByName(string itemName)
        {
            string name ="";
            foreach (ArtImage ai in imagesToCluster)
            {
                string artImageName = "";
                string[] urlParts = ai.KnownArtistName.Split("\\".ToCharArray());
                artImageName = urlParts[urlParts.Length - 1].Split(".".ToCharArray())[0];
                if (itemName.Equals(artImageName))
                {
                    name = ai.KnownArtistName;
                }
            }
            return name;
        }

        private bool isAnImageType(string str)
        {
            bool isImage = false;
            string[] imageEndings = {".jpg", ".bmp", ".gif", ".png", ".jpeg"};
            int results = imageEndings.Where(x => str.EndsWith(x)).Select(y => y).Count();
            isImage = results > 0;
            return isImage;
        }

        public bool allImagesLoaded()
        {
            return imagesToCluster.Count == EXPECTED_IMAGE_COUNT;
        }

    }
}