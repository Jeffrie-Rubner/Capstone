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

        public ArtistDistinguisher()
        {
            AddArtistsToDictionary();
            InitializeArtists();
        }

        public string[] AnalyzePicture(BitmapImage bitmapImage)
        {
            ArtImage artImage = new ArtImage(bitmapImage);
            string[] arrayOfEachColumnCellValue = new string[15];

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

        #region colorFrequency
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
        private string[] getDimensionsStrings(Dictionary<string, double> dimensionRatios)
        {
            string[] results = new string[10];
            int i = 0;
            foreach (KeyValuePair<string, double> kvp in dimensionRatios)
            {
                results[i] = kvp.Key;
                string temp = "" + kvp.Value + ".0000";

                results[i + 5] = temp.Substring(0,6);
                i++;
            }
            return results;
        }

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
                Thread secondT = new Thread(new ParameterizedThreadStart(this.InitializeColors));
                secondT.Start(kvp);
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
        }

        private void InitializeColors(object obj)
        {
            KeyValuePair<String, List<ArtImage>> kvp = (KeyValuePair<String, List<ArtImage>>)obj;
            foreach (ArtImage image in kvp.Value)
            {
                colorSAD.determineBitFrequency(image);
            }
        }
        #endregion
    }
}
