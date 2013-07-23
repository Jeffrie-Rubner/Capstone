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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Filter = "Image Files (*.jpg, *.bmp, *.gif, *.png, *.jpeg)" +
                "|*.jpg;*.bmp;*.gif;*.png;*.jpeg";
            string fileName;
            if (openFD.ShowDialog() != DialogResult)
            {
                fileName = openFD.FileName;
                BitmapImage myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri(@"" + openFD.FileName);
                myBitmapImage.DecodePixelWidth = 250;
                myBitmapImage.DecodePixelHeight = 250;
                myBitmapImage.EndInit();
                ImagePreview.Source = myBitmapImage;
            }
        }

    }
}
