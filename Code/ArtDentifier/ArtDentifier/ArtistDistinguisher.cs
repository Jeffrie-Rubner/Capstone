using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        private Dictionary<String, List<ArtImage>> allArtists = new Dictionary<string, List<ArtImage>>();
        private List<ArtImage> PicassoPieces = new List<ArtImage>();
        private List<ArtImage> MonetPieces = new List<ArtImage>();
        private List<ArtImage> RaphaelPieces = new List<ArtImage>();
        private List<ArtImage> BlakePieces = new List<ArtImage>();
        private List<ArtImage> VanGoghPieces = new List<ArtImage>();
        private ColorScaleAspectDeterminer colorSAD = new ColorScaleAspectDeterminer();
        private DimensionAnalyzer dimensionAnalyzer = new DimensionAnalyzer();

        public ArtistDistinguisher()
        {
            AddArtistsToDictionary();
            InitializeArtists();
        }

        public string[] AnalyzePicture(BitmapImage bitmapImage)
        {
            ArtImage artImage = new ArtImage(bitmapImage);
            //method that compares first metric
            testDimensionAspect(artImage);
           //method that compares the second metric
           testColorScaleAspect(artImage);
            return new string[] {"first", "second"};
        }

        #region Metric Measuring Methods
        //related to ColorScaleAspectDeterminer class
        private void testColorScaleAspect(ArtImage artImage)
        {
            colorSAD.determineBitFrequency(artImage);
        }
        //related to DimensionAnalyzer
        private void testDimensionAspect(ArtImage artImage)
        {
            dimensionAnalyzer.doesSomethingWithWidthHeight(artImage);
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
        }
        #endregion
    }
}
