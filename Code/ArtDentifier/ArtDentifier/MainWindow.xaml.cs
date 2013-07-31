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
        private ArtistDistinguisher artdentifier;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            artdentifier = new ArtistDistinguisher();
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
                artdentifier.AnalyzePicture((BitmapImage)ImagePreview.Source);
            }
            else
            {
                ResultBox.Text = "You Must Put In an image";
            }
        }
        #endregion

    }
}
