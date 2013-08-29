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
        public ResultWindow()
        {
            InitializeComponent();
        }

        public void SetTextBox(String[] results)
        {
            List<string> artistNames = new List<string>();
            double[] averageValues = new double[5];
            for (int i = 0; i < 5; i++)
            {
                averageValues[i] = Convert.ToDouble(results[(results.Length - 1) - i]);
            }
            for (int i = 0; i < 5; i++)
            {
                if (averageValues[0] - averageValues[i] < 5)
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

    }
}
