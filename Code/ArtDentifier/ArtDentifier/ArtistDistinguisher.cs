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
        private readonly int WorkingCellCount = 25;

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
            
            //method for second metric
            colorSAD.determineBitFrequency(artImage);
            Dictionary<string, double> secondMetricValues = testColor(artImage);

            //method for third metric
            Dictionary<string, double> thirdMetricValues = testImageSaturation(artImage);

            //method for obtaining the mean column values
            Dictionary<string, double> averageColumnValues = getColumnAverages(firstMetricValues, secondMetricValues, thirdMetricValues);

            //fills the string array for the fields
            int i = 0;
            foreach (string artistName in allArtists.Keys)
            {
                arrayOfEachColumnCellValue[i] = artistName;

                string temp1 = "" + firstMetricValues[artistName] + ".0000";
                arrayOfEachColumnCellValue[i + 5] = temp1.Substring(0, 6);

                string temp2 = "" + secondMetricValues[artistName] + ".0000";
                arrayOfEachColumnCellValue[i + 10] = temp2.Substring(0, 6);

                string temp3 = "" + thirdMetricValues[artistName] + ".0000";
                arrayOfEachColumnCellValue[i + 15] = temp3.Substring(0, 6);

                string temp4 = "" + averageColumnValues[artistName] + ".0000";
                arrayOfEachColumnCellValue[i + 20] = temp4.Substring(0, 6);
                i++;
            }
            return arrayOfEachColumnCellValue;
        }

        #region Metric Measuring Methods

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

        #region Colors

        private Dictionary<string, double> testColor(ArtImage artImage)
        {
            Dictionary<string, double> ColorResultReturns = new Dictionary<string, double>();

            Dictionary<string, double> redColorResults = compareImageRed(artImage);
            Dictionary<string, double> greenColorResults = compareImageGreen(artImage);
            Dictionary<string, double> blueColorResults = compareImageBlue(artImage);

            foreach (string artistName in allArtists.Keys)
            {
                double meanColorValues = (redColorResults[artistName] + greenColorResults[artistName] 
                    + blueColorResults[artistName])/3;
                ColorResultReturns.Add(artistName, meanColorValues);
            }

            return ColorResultReturns;
        }

        private Dictionary<string, double> compareImageRed(ArtImage artImage)
        {
            Dictionary<string, double> redCheckRatios = new Dictionary<string, double>();
            byte inputRed = artImage.getMostFrequentRed();
            foreach (List<ArtImage> lists in allArtists.Values)
            {
                double redSimilarity = 0.00;
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
            byte inputGreen = artImage.getMostFrequentGreen();
            foreach (List<ArtImage> lists in allArtists.Values)
            {
                double greenSimilarity = 0.00;
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
            byte inputBlue = artImage.getMostFrequentBlue();
            foreach (List<ArtImage> lists in allArtists.Values)
            {
                double blueSimilarity = 0.00;
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

        #endregion

        #region Saturation

        private Dictionary<string, double> testImageSaturation(ArtImage artImage)
        {
            int redMeanValue = artImage.getMeanValue("red");
            int greenMeanValue = artImage.getMeanValue("green");
            int blueMeanValue = artImage.getMeanValue("blue");
            int inputSaturation = redMeanValue + greenMeanValue + blueMeanValue;
            Dictionary<string, double> returnValues = new Dictionary<string, double>();
            foreach (List<ArtImage> lists in allArtists.Values)
            {
                double saturationComparitor = 0.0;
                int imageCount = 0;
                foreach (ArtImage a in lists)
                {
                    int imageSaturation = a.getMeanValue("red") 
                        + a.getMeanValue("green") + a.getMeanValue("blue");
                    double tempComparitor = 1 - (Math.Abs(inputSaturation - imageSaturation) / 765);
                    saturationComparitor += tempComparitor;
                    imageCount++;
                }
                foreach (KeyValuePair<String, List<ArtImage>> kvp in allArtists)
                {
                    if (kvp.Value.Equals(lists))
                    {
                        returnValues.Add(kvp.Key, saturationComparitor / imageCount);
                    }
                }
            }

            return returnValues;
        }

        #endregion

        #endregion

        private Dictionary<string, double> getColumnAverages(Dictionary<string, double> column1, Dictionary<string, double> column2, Dictionary<string, double> column3)
        {
            Dictionary<string, double> averageColumnValues = new Dictionary<string, double>();
            foreach (string s in allArtists.Keys)
            {
                double value = (column1[s] + column2[s] + column3[s])/3;
                averageColumnValues.Add(s, value);
            }
            return averageColumnValues;
        }

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
                new ColorScaleAspectDeterminer().determineBitFrequency(image);
            }
        }
        #endregion
    }
}
