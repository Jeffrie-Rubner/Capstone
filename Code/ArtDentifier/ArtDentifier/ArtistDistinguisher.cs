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

        public ArtistDistinguisher()
        {
          AddArtistsToDictionary();
         //InitializeArtists();
        }

        public void AnalyzePicture(BitmapImage bitmap)
        {
           //method that compares the first metric
           testColorScaleAspect(bitmap);
        }

        #region Metric Measuring Methods
        private void testColorScaleAspect(BitmapImage bitmap)
        {

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
                Uri artistLocation = new Uri(@"/TeachingImages/" + kvp.Key);
                string[] filePaths = Directory.GetFiles(@"/TeachingImages/" + kvp.Key);
                //kvp.Value.Add(new ArtImage());    
            }
        }
        #endregion
    }
}
