using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArtDentifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private readonly int COUNT_OF_TOP_ARTISTS_SHOWN = 5;
        private readonly int METRICS_MESURED_ON = 4;
        private ArtistDistinguisher artdentifier;
        private string[,] artistChanceValues;
        private TextBlock[,] resultGrid;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            artdentifier = new ArtistDistinguisher();
            artistChanceValues = new string[METRICS_MESURED_ON, COUNT_OF_TOP_ARTISTS_SHOWN];
            resultGrid = new TextBlock[METRICS_MESURED_ON, COUNT_OF_TOP_ARTISTS_SHOWN];
            fillResultGrid();
        }
        #endregion

        #region ButtonEvents
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Filter = "Image Files (*.jpg, *.bmp, *.gif, *.png, *.jpeg)" +
                "|*.jpg;*.bmp;*.gif;*.png;*.jpeg";
            string fileName;
            if (openFD.ShowDialog() != DialogResult)
            {
                try
                {
                    fileName = openFD.FileName;
                    BitmapImage myBitmapImage = BitmapImageFromURI.GetBitmapImage(new Uri(@"" + openFD.FileName));
                    ImagePreview.Source = myBitmapImage;
                }
                catch (UriFormatException exception)
                {
                    //do nothing
                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (ImagePreview.Source != null)
            {
                ArtImage artImage = new ArtImage((BitmapImage)ImagePreview.Source);
                string[] results = artdentifier.AnalyzePicture(artImage);
                ResultBox.Text = "";
                ArtistHeader.Text = "Artist Names";
                Column1Header.Text = "Dimensions";
                Column2Header.Text = "Colors";
             //   Column3Header.Text = "Saturation";
                Column3Header.Text = "Average";
                for (int i = 0; i < results.Length; i++)
                { 
                    artistChanceValues[i / COUNT_OF_TOP_ARTISTS_SHOWN, i % (COUNT_OF_TOP_ARTISTS_SHOWN)] = results[i];
                }
                for (int j = 0; j < COUNT_OF_TOP_ARTISTS_SHOWN; j++)
                {
                    for (int k = 0; k < METRICS_MESURED_ON; k++)
                    {
                        resultGrid[k, j].Text = artistChanceValues[k, j];
                    }
                }
                DetermineArtist(results, artImage);
            }
            else
            {
                ResultBox.Text = "You must submit an image";
            }
        }
        #endregion

        #region Initialization Methods
        private void fillResultGrid()
        {
            switch (METRICS_MESURED_ON)
            {
                case 2:
                    resultGrid[0, 0] = Artist1;
                    resultGrid[0, 1] = Artist2;
                    resultGrid[0, 2] = Artist3;
                    resultGrid[0, 3] = Artist4;
                    resultGrid[0, 4] = Artist5;

                    resultGrid[1, 0] = Column1Row1;
                    resultGrid[1, 1] = Column1Row2;
                    resultGrid[1, 2] = Column1Row3;
                    resultGrid[1, 3] = Column1Row4;
                    resultGrid[1, 4] = Column1Row5;
                    break;
                case 3:
                    resultGrid[2, 0] = Column2Row1;
                    resultGrid[2, 1] = Column2Row2;
                    resultGrid[2, 2] = Column2Row3;
                    resultGrid[2, 3] = Column2Row4;
                    resultGrid[2, 4] = Column2Row5;
                    goto case 2;
                case 4:
                    resultGrid[3, 0] = Column3Row1;
                    resultGrid[3, 1] = Column3Row2;
                    resultGrid[3, 2] = Column3Row3;
                    resultGrid[3, 3] = Column3Row4;
                    resultGrid[3, 4] = Column3Row5;
                    goto case 3;
                case 5:
                    resultGrid[4, 0] = Column4Row1;
                    resultGrid[4, 1] = Column4Row2;
                    resultGrid[4, 2] = Column4Row3;
                    resultGrid[4, 3] = Column4Row4;
                    resultGrid[4, 4] = Column4Row5;
                    goto case 4;
                case 6:
                    resultGrid[5, 0] = Column5Row1;
                    resultGrid[5, 1] = Column5Row2;
                    resultGrid[5, 2] = Column5Row3;
                    resultGrid[5, 3] = Column5Row4;
                    resultGrid[5, 4] = Column5Row5;
                    goto case 5;
            }
        }
        #endregion

        private void DetermineArtist(String[] results, ArtImage image)
        {
            if ((bool)CheckBox1.IsChecked)
            {
                ResultWindow resultWindow = new ResultWindow(artdentifier);
                resultWindow.SetTextBox(results, image);
                resultWindow.ShowDialog();
            }
            else if ((bool)CheckBox2.IsChecked)
            {

            }
            
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            CheckBox2.IsChecked = false;
        }

        private void CheckBox_Checked_2(object sender, RoutedEventArgs e)
        {
            CheckBox1.IsChecked = false;
        }

    }
}
