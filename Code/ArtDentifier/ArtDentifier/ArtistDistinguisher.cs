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
        private readonly int WorkingCellCount = 20;

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

        public string[] AnalyzePicture(ArtImage artImage)
        {
            string[] arrayOfEachColumnCellValue = new string[WorkingCellCount];

            //method that compares first metric
            Dictionary<string, double> firstMetricValues = testDimensionAspect(artImage);
            
            //method for second metric
            colorSAD.determineBitFrequency(artImage);
            Dictionary<string, double> secondMetricValues = testColor(artImage);

            //method for third metric
            //Dictionary<string, double> thirdMetricValues = testImageSaturation(artImage);

            //method for obtaining the mean column values
            Dictionary<string, double> averageColumnValues = getColumnAverages(firstMetricValues, secondMetricValues);

            //fills the string array for the fields
            int i = 0;
            foreach (string artistName in allArtists.Keys)
            {
                arrayOfEachColumnCellValue[i] = artistName;

                string temp1 = "" + firstMetricValues[artistName] + ".0000";
                arrayOfEachColumnCellValue[i + 5] = temp1.Substring(0, 6);

                string temp2 = "" + secondMetricValues[artistName] + ".0000";
                arrayOfEachColumnCellValue[i + 10] = temp2.Substring(0, 6);

           //     string temp3 = "" + thirdMetricValues[artistName] + ".0000";
           //     arrayOfEachColumnCellValue[i + 15] = temp3.Substring(0, 6);

                string temp4 = "" + averageColumnValues[artistName] + ".0000";
                arrayOfEachColumnCellValue[i + 15] = temp4.Substring(0, 6);
                i++;
            }

            arrayOfEachColumnCellValue = SortBySimilarity(arrayOfEachColumnCellValue);

            return arrayOfEachColumnCellValue;
        }

        public void AddArtImage(string artistName, ArtImage artImage)
        {
            allArtists[artistName].Add(artImage);
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
                int i = 1;
                foreach (ArtImage a in lists)
                {
                    double currentImageRatio = dimensionAnalyzer.getDimensionRatio(a);
                    double tempSimilarity = 1 - Math.Abs(dimensionalRatio - currentImageRatio);
                    if (tempSimilarity == 1)
                    {
                        dimensionalSimilarity = 1;
                        break;
                    }
                    else
                    {
                        if (tempSimilarity - (dimensionalSimilarity / i) > -20)
                        {
                            dimensionalSimilarity = dimensionalSimilarity + tempSimilarity;
                            i++;
                        }
                    }
                    
                }
                foreach (KeyValuePair<String, List<ArtImage>> kvp in allArtists)
                {
                    if(kvp.Value.Equals(lists))
                    {
                        dimensionalSimilarity = dimensionalSimilarity == 1 ? 1 : (dimensionalSimilarity / i); 
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
                int i = 1;
                foreach (ArtImage a in lists)
                {
                    byte redValue = a.getMostFrequentRed();
                    double redRatio = ((double)Math.Abs(redValue - inputRed)) / 255;
                    double tempSimilarity = 1 - redRatio;
                    if (tempSimilarity == 1)
                    {
                        redSimilarity = 1;
                        break;
                    }
                    else
                    {
                            redSimilarity = redSimilarity + tempSimilarity;
                            i++;
                    }
                }
                foreach (KeyValuePair<String, List<ArtImage>> kvp in allArtists)
                {
                    if (kvp.Value.Equals(lists))
                    {
                        redSimilarity = redSimilarity == 1 ? 1 : (redSimilarity / i); 
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
                int i = 1;
                foreach (ArtImage a in lists)
                {
                    byte greenValue = a.getMostFrequentGreen();
                    double redRatio = ((double)Math.Abs(greenValue - inputGreen)) / 255;
                    double tempSimilarity = 1 - redRatio;
                    if (tempSimilarity == 1)
                    {
                        greenSimilarity = 1;
                        break;
                    }
                    else
                    {
                            greenSimilarity = greenSimilarity + tempSimilarity;
                            i++;
                    }
                }
                foreach (KeyValuePair<String, List<ArtImage>> kvp in allArtists)
                {
                    if (kvp.Value.Equals(lists))
                    {
                        greenSimilarity = greenSimilarity == 1 ? 1 : (greenSimilarity / i);
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
                int i = 1;
                foreach (ArtImage a in lists)
                {
                    byte blueValue = a.getMostFrequentBlue();
                    double redRatio = ((double)Math.Abs(blueValue - inputBlue)) / 255;
                    double tempSimilarity = 1 - redRatio;
                    if (tempSimilarity == 1)
                    {
                        blueSimilarity = 1;
                        break;
                    }
                    else
                    {
                            blueSimilarity = blueSimilarity + tempSimilarity;
                            i++;
                    }
                }
                foreach (KeyValuePair<String, List<ArtImage>> kvp in allArtists)
                {
                    if (kvp.Value.Equals(lists))
                    {
                        blueSimilarity = blueSimilarity == 1 ? 1 : (blueSimilarity / i);
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
                    double tempComparitor = 1 - (Math.Abs(inputSaturation - imageSaturation) / inputSaturation);
                    saturationComparitor += tempComparitor;
                    imageCount++;
                }
                foreach (KeyValuePair<String, List<ArtImage>> kvp in allArtists)
                {
                    if (kvp.Value.Equals(lists))
                    {
                        double result = Math.Abs((saturationComparitor / (double)imageCount) * 100) > 100 ? 100 : Math.Abs((saturationComparitor / (double)imageCount) * 100);
                        returnValues.Add(kvp.Key, result);
                    }
                }
            }

            return returnValues;
        }

        #endregion

        #endregion

        #region PrivateMethods

        private string[] SortBySimilarity(string[] columnCells)
        {
            for (int i = 15; i < 19; i++)
            {
                if(Convert.ToDouble(columnCells[i]) > Convert.ToDouble(columnCells[i+1]))
                {
                    for (int j = 0; j <= 15; j += 5)
                    {
                        string temp = columnCells[i - j];
                        columnCells[i - j] = columnCells[(i - j) + 1];
                        columnCells[(i - j) + 1] = temp;
                    } 
                    i = 15;
                }
            }

            return columnCells;
        }

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

        private Dictionary<string, double> getColumnAverages(Dictionary<string, double> column1, Dictionary<string, double> column2)
        {
            Dictionary<string, double> averageColumnValues = new Dictionary<string, double>();
            foreach (string s in allArtists.Keys)
            {
                double value = (column1[s] + column2[s]) / 2;
                averageColumnValues.Add(s, value);
            }
            return averageColumnValues;
        }

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
                new ColorScaleAspectDeterminer().determineBitFrequency(image);
            }
        }
        #endregion
    }
}
