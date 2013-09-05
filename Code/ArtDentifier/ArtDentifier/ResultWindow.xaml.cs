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
using System.Windows.Shapes;

namespace ArtDentifier
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        private ArtImage InputImage;
        private ArtistDistinguisher artDistinguisher;

        public ResultWindow(ArtistDistinguisher ad)
        {
            artDistinguisher = ad;
            InitializeComponent();
        }

        public void SetTextBox(String[] results, ArtImage image)
        {
            InputImage = image;
            List<string> artistNames = new List<string>();
            double[] averageValues = new double[5];
            for (int i = 0; i < 5; i++)
            {
                averageValues[i] = Convert.ToDouble(results[(results.Length - 5) + i]);
            }
            artistNames.Add(results[4]);
            for (int i = 0; i < 4; i++)
            {
                if (averageValues[4] - averageValues[i] < 5)
                {
                    artistNames.Add(results[i]);
                }
            }
            string resultString;
            if (artistNames.Count > 1)
            {
                resultString = "The results are inconclusive. The artist could be ";
                for (int j = 0; j < artistNames.Count - 1; j++)
                {
                    resultString += artistNames[j] + ", ";
                }
                resultString += "or " + artistNames[artistNames.Count - 1]; 
            }
            else
            {
                resultString = "The Artist of this image is " + artistNames[0];
            }
            
            ArtistResultBox.Text = resultString;
        }

        public void AutomaticBox(String[] results, ArtImage image)
        {
            InputImage = image;
            List<string> artistNames = new List<string>();
            double[] averageValues = new double[5];
            for (int i = 0; i < 5; i++)
            {
                averageValues[i] = Convert.ToDouble(results[(results.Length - 5) + i]);
            }
            artistNames.Add(results[4]);
            for (int i = 0; i < 4; i++)
            {
                if (averageValues[4] - averageValues[i] < 5)
                {
                    artistNames.Add(results[i]);
                }
            }
            string resultString;
            if (artistNames.Count > 1)
            {

            }
            else
            {
                artDistinguisher.AddArtImage(artistNames[0], InputImage);
            }
        }

        #region ClickListeners

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string confirmedArtist = null;
            if ((bool)RadioButton1.IsChecked)
            {
                confirmedArtist = (string)RadioButton1.Content;
            }
            else if ((bool)RadioButton2.IsChecked)
            {
                confirmedArtist = (string)RadioButton2.Content;
            }
            else if ((bool)RadioButton3.IsChecked)
            {
                confirmedArtist = (string)RadioButton3.Content;
            }
            else if ((bool)RadioButton4.IsChecked)
            {
                confirmedArtist = (string)RadioButton4.Content;
            }
            else if ((bool)RadioButton5.IsChecked)
            {
                confirmedArtist = (string)RadioButton5.Content;
            }

            artDistinguisher.AddArtImage(confirmedArtist, InputImage);
            this.Close();

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SubmitButton.Visibility = System.Windows.Visibility.Visible; 
        }

        #endregion
    }
}
