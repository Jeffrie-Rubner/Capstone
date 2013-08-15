using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
/*
 * This class will make use of metrics to distinguish between artists
 */
namespace ArtDentifier
{
    class ArtistDistinguisher
    {
        //working cells is the value of the (number of working metrics +1) times 5
        private readonly int WorkingCellCount = 15;

        #region Storage Fields
        private Dictionary<string, List<ArtImage>> allArtists = new Dictionary<string, List<ArtImage>>();
        private List<ArtImage> PicassoPieces = new List<ArtImage>();
        private List<ArtImage> MonetPieces = new List<ArtImage>();
        private List<ArtImage> RaphaelPieces = new List<ArtImage>();
        private List<ArtImage> BlakePieces = new List<ArtImage>();
        private List<ArtImage> VanGoghPieces = new List<ArtImage>();
        #endregion

        #region Metric Class Fields
        private ColorScaleAspectDeterminer colorSAD = new ColorScaleAspectDeterminer();
        private DimensionAnalyzer dimensionAnalyzer = new DimensionAnalyzer();
        #endregion

        #region Constructor
        public ArtistDistinguisher()
        {
            AddArtistsToDictionary();
            InitializeArtists();
        }
        #endregion

        public string[] AnalyzePicture(BitmapImage bitmapImage)
        {
            ArtImage artImage = new ArtImage(bitmapImage);
            string[] arrayOfEachColumnCellValue = new string[WorkingCellCount];

            //method that compares first metric
            Dictionary<string, double> firstMetricValues = testDimensionAspect(artImage);
            
            //method that compares the second metric
            Dictionary<string, double> secondMetricValues = testColorScaleAspect(artImage);
            int i = 0;

            //fills the string array for the fields
            foreach (string artistName in allArtists.Keys)
            {
                arrayOfEachColumnCellValue[i] = artistName;

                string temp1 = "" + firstMetricValues[artistName] + ".0000";
                arrayOfEachColumnCellValue[i + 5] = temp1.Substring(0, 6);

                string temp2 = "" + secondMetricValues[artistName] + ".0000";
                arrayOfEachColumnCellValue[i + 10] = temp2.Substring(0, 6);
                i++;
            }
            return arrayOfEachColumnCellValue;
        }

        #region Metric Measuring Methods

        #region ColorFrequency
        private Dictionary<string, double> testColorScaleAspect(ArtImage artImage)
        {
            colorSAD.determineBitFrequency(artImage);
            Dictionary<string, double> colorCheckRatios = new Dictionary<string, double>();
            Color Inputcolor = artImage.getMostFrequentColor();
            foreach (List<ArtImage> lists in allArtists.Values)
            {
                double colorSimilarity = 0.00;
                foreach (ArtImage a in lists)
                {
                    Color comparitorColor = a.getMostFrequentColor();
                    double redRatio = ((double)Math.Abs(Inputcolor.R - comparitorColor.R)) / 255;
                    double greenRatio = ((double)Math.Abs(Inputcolor.G - comparitorColor.G)) / 255;
                    double blueRatio = ((double)Math.Abs(Inputcolor.B - comparitorColor.B)) / 255;
                    double alphaRatio = ((double)Math.Abs(Inputcolor.A - comparitorColor.A)) / 255;
                    double tempSimilarity = 1 - ((redRatio + greenRatio + blueRatio + alphaRatio) / 4.00);
                    colorSimilarity = tempSimilarity > colorSimilarity ? tempSimilarity : colorSimilarity;
                }
                foreach (KeyValuePair<String, List<ArtImage>> kvp in allArtists)
                {
                    if (kvp.Value.Equals(lists))
                    {
                        colorCheckRatios.Add(kvp.Key, colorSimilarity * 100);
                    }
                }
            }
            return colorCheckRatios;
        }
        #endregion

        #region Dimensions
        private Dictionary<string, double> testDimensionAspect(ArtImage artImage)
        {
            Dictionary<string, double> DimensionalCheckRatios = new Dictionary<string, double>();
            double dimensionalRatio = dimensionAnalyzer.getDimensionRatio(artImage);
            foreach (List<ArtImage> lists in allArtists.Values)
            {
                double dimensionalSimilarity = 0.00;
                foreach (ArtImage a in lists)
                {
                    double currentImageRatio = dimensionAnalyzer.getDimensionRatio(a);
                    double tempSimilarity = 1 - Math.Abs(dimensionalRatio - currentImageRatio);
                    dimensionalSimilarity = tempSimilarity > dimensionalSimilarity ? tempSimilarity : dimensionalSimilarity;
                }
                foreach (KeyValuePair<String, List<ArtImage>> kvp in allArtists)
                {
                    if(kvp.Value.Equals(lists))
                    {
                        DimensionalCheckRatios.Add(kvp.Key, dimensionalSimilarity * 100);
                    }
                }
            }
            return DimensionalCheckRatios;
        }
        #endregion

        #region ImageTypes
        //need 4 methods to compare ARGB values.  ARGB values can help determine the type of image a painting is (such as landscape, portrait, mural, etc.)
        private Dictionary<string, double> compareImageRed(ArtImage artImage)
        {
            Dictionary<string, double> redCheckRatios = new Dictionary<string, double>();
            foreach (List<ArtImage> lists in allArtists.Values)
            {
                double redSimilarity = 0.00;
                byte inputRed = artImage.getMostFrequentRed();
                foreach (ArtImage a in lists)
                {
                    byte redValue = a.getMostFrequentRed();
                    double redRatio = ((double)Math.Abs(redValue - inputRed)) / 255;
                    double tempSimilarity = 1 - redRatio;
                    redSimilarity = tempSimilarity > redSimilarity ? tempSimilarity : redSimilarity;
                }
                foreach (KeyValuePair<String, List<ArtImage>> kvp in allArtists)
                {
                    if (kvp.Value.Equals(lists))
                    {
                        redCheckRatios.Add(kvp.Key, redSimilarity * 100);
                    }
                }
            }
            return redCheckRatios;
        }

        private Dictionary<string, double> compareImageGreen(ArtImage artImage)
        {
            Dictionary<string, double> greenCheckRatios = new Dictionary<string, double>();
            foreach (List<ArtImage> lists in allArtists.Values)
            {
                double greenSimilarity = 0.00;
                byte inputGreen = artImage.getMostFrequentGreen();
                foreach (ArtImage a in lists)
                {
                    byte greenValue = a.getMostFrequentGreen();
                    double greenRatio = ((double)Math.Abs(greenValue - inputGreen)) / 255;
                    double tempSimilarity = 1 - greenRatio;
                    greenSimilarity = tempSimilarity > greenSimilarity ? tempSimilarity : greenSimilarity;
                }
                foreach (KeyValuePair<String, List<ArtImage>> kvp in allArtists)
                {
                    if (kvp.Value.Equals(lists))
                    {
                        greenCheckRatios.Add(kvp.Key, greenSimilarity * 100);
                    }
                }
            }
            return greenCheckRatios;
        }

        private Dictionary<string, double> compareImageBlue(ArtImage artImage)
        {
            Dictionary<string, double> blueCheckRatios = new Dictionary<string, double>();
            foreach (List<ArtImage> lists in allArtists.Values)
            {
                double blueSimilarity = 0.00;
                byte inputBlue = artImage.getMostFrequentBlue();
                foreach (ArtImage a in lists)
                {
                    byte blueValue = a.getMostFrequentBlue();
                    double blueRatio = ((double)Math.Abs(blueValue - inputBlue)) / 255;
                    double tempSimilarity = 1 - blueRatio;
                    blueSimilarity = tempSimilarity > blueSimilarity ? tempSimilarity : blueSimilarity;
                }
                foreach (KeyValuePair<String, List<ArtImage>> kvp in allArtists)
                {
                    if (kvp.Value.Equals(lists))
                    {
                        blueCheckRatios.Add(kvp.Key, blueSimilarity * 100);
                    }
                }
            }
            return blueCheckRatios;
        }

        private Dictionary<string, double> compareImageAlpha(ArtImage artImage)
        {
            Dictionary<string, double> alphaCheckRatios = new Dictionary<string, double>();
            foreach (List<ArtImage> lists in allArtists.Values)
            {
                double alphaSimilarity = 0.00;
                byte inputAlpha = artImage.getMostFrequentAlpha();
                foreach (ArtImage a in lists)
                {
                    byte alphaValue = a.getMostFrequentAlpha();
                    double alphaRatio = ((double)Math.Abs(alphaValue - inputAlpha)) / 255;
                    double tempSimilarity = 1 - alphaRatio;
                    alphaSimilarity = tempSimilarity > alphaSimilarity ? tempSimilarity : alphaSimilarity;
                }
                foreach (KeyValuePair<String, List<ArtImage>> kvp in allArtists)
                {
                    if (kvp.Value.Equals(lists))
                    {
                        alphaCheckRatios.Add(kvp.Key, alphaSimilarity * 100);
                    }
                }
            }
            return alphaCheckRatios;
        }

        #endregion

        #endregion

        #region InitializationMethods
        private void AddArtistsToDictionary()
        {
            allArtists.Add("Picasso", PicassoPieces);
            allArtists.Add("Monet", MonetPieces);
            allArtists.Add("Raphael", RaphaelPieces);
            allArtists.Add("Blake", BlakePieces);
            allArtists.Add("VanGogh", VanGoghPieces);
        }
        private void InitializeArtists()
        {
            foreach (KeyValuePair<String, List<ArtImage>> kvp in allArtists)
            {
                Thread t = new Thread(new ParameterizedThreadStart(this.ImageGrabMethod));
                t.Start(kvp);
            }
        }

        private void ImageGrabMethod(object obj)
        {
            KeyValuePair<String, List<ArtImage>> kvp = (KeyValuePair<String, List<ArtImage>>)obj;
            string dir = @"c:\Users\Jeff\Documents\GitHub\Capstone\Documentation\Misc\TeachingImages\" + kvp.Key;
            string[] filePaths = Directory.GetFiles(dir);
            foreach (string str in filePaths)
            {
                Uri artistLocation = new Uri(str);
                try
                {
                    BitmapImage myBitmapImage = BitmapImageFromURI.GetBitmapImage(artistLocation);
                    kvp.Value.Add(new ArtImage(myBitmapImage));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            InitializeColors(kvp);
        }

        private void InitializeColors(KeyValuePair<String, List<ArtImage>> kvp)
        {
            foreach (ArtImage image in kvp.Value)
            {
                ArtImage clonedImage = image.clone();
                new ColorScaleAspectDeterminer().determineBitFrequency(clonedImage);
            }
        }
        #endregion
    }
}
