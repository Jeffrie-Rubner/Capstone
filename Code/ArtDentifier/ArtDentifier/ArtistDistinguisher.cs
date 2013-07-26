﻿using System;
using System.Collections.Generic;
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
        private Dictionary<String, List<BitmapImage>> allArtists = new Dictionary<string, List<BitmapImage>>();
        private List<BitmapImage> PicassoPieces = new List<BitmapImage>();
        private List<BitmapImage> MonetPieces = new List<BitmapImage>();
        private List<BitmapImage> RaphaelPieces = new List<BitmapImage>();
        private List<BitmapImage> BlakePieces = new List<BitmapImage>();
        private List<BitmapImage> VanGoghPieces = new List<BitmapImage>();
        private ColorScaleAspectDeterminer colorSAD = new ColorScaleAspectDeterminer();

        public ArtistDistinguisher()
        {
          AddArtistsToDictionary();
         //   InitializeArtists();
        }

        public void AnalyzePicture(BitmapImage bitmap)
        {
           //create a method that allows images to be compared to each artists image on every metric
            Color c = colorSAD.determineBitMean(bitmap);
            foreach (KeyValuePair<String, List<BitmapImage>> kvp in allArtists)
            {

            }
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
            foreach (KeyValuePair<String, List<BitmapImage>> kvp in allArtists)
            {
                Uri artistLocation = new Uri(@"/TeachingImages/" + kvp.Key);

                //kvp.Value.Add(new BitmapImage());    
            }
        }
        #endregion
    }
}