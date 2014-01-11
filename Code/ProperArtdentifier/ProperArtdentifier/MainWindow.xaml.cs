using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace ProperArtdentifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Clusterer clusterer;
        private ListBox[] listBoxes;
        private string fileName;
        private bool hasClustered = false;

        public MainWindow()
        {
            InitializeComponent();
            clusterer = new Clusterer();
            InitializeListBoxes();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
                Dictionary<string, List<ArtImage>> allClusters = clusterer.Cluster();
                ClearListBoxes();
                for (int i = 0; i < 5; i++)
                {
                    string clusterNum = "cluster" + (i + 1);
                    List<string> justClusterImages = (allClusters[clusterNum].OrderBy(g => g.KnownArtistName)
                        .Select(group => group.KnownArtistName).ToList());

                    foreach (string item in justClusterImages)
                    {
                        string imageItem = trimName(item);
                        ListBoxItem lbi = new ListBoxItem();
                        lbi.Content = imageItem;
                        listBoxes[i].Items.Add(lbi);
                    }
                }
                hasClustered = true;
                if (ImagePreview.Source != null)
                {
                    SubmitButton.Visibility = Visibility.Visible;
                }
                AccButton.Visibility = Visibility.Visible;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.Filter = "Image Files (*.jpg, *.bmp, *.gif, *.png, *.jpeg)" +
                "|*.jpg;*.bmp;*.gif;*.png;*.jpeg";
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
            if (hasClustered)
            {
                SubmitButton.Visibility = Visibility.Visible;
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            ArtImage artImage = new ArtImage((BitmapImage)ImagePreview.Source, fileName);
            string clusterName = clusterer.AnalyzePicture(artImage);
            ClusterPopUp clusterPopUp = new ClusterPopUp();
            clusterPopUp.SetTextBox(clusterName);
            clusterPopUp.ShowDialog();
            SubmitButton.Visibility = Visibility.Hidden;
        }

        private void InitializeListBoxes()
        {
            listBoxes = new ListBox[5];
            listBoxes[0] = c1_listBox;
            listBoxes[1] = c2_listBox;
            listBoxes[2] = c3_listBox;
            listBoxes[3] = c4_listBox;
            listBoxes[4] = c5_listBox;
        }

        private string trimName(String artistName)
        {
            string[] urlParts = artistName.Split("\\".ToCharArray());
            artistName = urlParts[urlParts.Length-1].Split(".".ToCharArray())[0];
            return artistName;
        }

        private void ClearListBoxes()
        {
            foreach (ListBox lb in listBoxes)
            {
                lb.Items.Clear();
            }
        }

        private void c1_listBox_MouseMove_1(object sender, MouseEventArgs e)
        {
            int index = c1_listBox.SelectedIndex;
            if (index >= 0)
            {
                ListBoxItem listBoxItem = (ListBoxItem)c1_listBox.Items.GetItemAt(index);
                string itemName = (string)listBoxItem.Content;

                BitmapImage myBitmapImage = BitmapImageFromURI.GetBitmapImage(new Uri(@"" + clusterer.getImageByName(itemName)));
                Image image = new Image();
                image.Source = myBitmapImage;
                ToolTip tt = new ToolTip();
                tt.MaxWidth = 200;
                tt.Content = image;
                listBoxItem.ToolTip = tt;
                ToolTipService.SetInitialShowDelay(listBoxItem, 1);
                
            } 
        }

        private void c2_listBox_MouseMove_1(object sender, MouseEventArgs e)
        {
            int index = c2_listBox.SelectedIndex;
            if (index >= 0)
            {
                ListBoxItem listBoxItem = (ListBoxItem)c2_listBox.Items.GetItemAt(index);
                string itemName = (string)listBoxItem.Content;

                BitmapImage myBitmapImage = BitmapImageFromURI.GetBitmapImage(new Uri(@"" + clusterer.getImageByName(itemName)));
                Image image = new Image();
                image.Source = myBitmapImage;
                ToolTip tt = new ToolTip();
                tt.MaxWidth = 200;
                tt.Content = image;
                listBoxItem.ToolTip = tt;
                ToolTipService.SetInitialShowDelay(listBoxItem, 1);
            } 
        }

        private void c3_listBox_MouseMove_1(object sender, MouseEventArgs e)
        {
            int index = c3_listBox.SelectedIndex;
            if (index >= 0)
            {
                ListBoxItem listBoxItem = (ListBoxItem)c3_listBox.Items.GetItemAt(index);
                string itemName = (string)listBoxItem.Content;

                BitmapImage myBitmapImage = BitmapImageFromURI.GetBitmapImage(new Uri(@"" + clusterer.getImageByName(itemName)));
                Image image = new Image();
                image.Source = myBitmapImage;
                ToolTip tt = new ToolTip();
                tt.MaxWidth = 200;
                tt.Content = image;
                listBoxItem.ToolTip = tt;
                ToolTipService.SetInitialShowDelay(listBoxItem, 1);
            } 
        }

        private void c4_listBox_MouseMove_1(object sender, MouseEventArgs e)
        {
            int index = c4_listBox.SelectedIndex;
            if (index >= 0)
            {
                ListBoxItem listBoxItem = (ListBoxItem)c4_listBox.Items.GetItemAt(index);
                string itemName = (string)listBoxItem.Content;

                BitmapImage myBitmapImage = BitmapImageFromURI.GetBitmapImage(new Uri(@"" + clusterer.getImageByName(itemName)));
                Image image = new Image();
                image.Source = myBitmapImage;
                ToolTip tt = new ToolTip();
                tt.MaxWidth = 200;
                tt.Content = image;
                listBoxItem.ToolTip = tt;
                ToolTipService.SetInitialShowDelay(listBoxItem, 1);
            } 
        }

        private void c5_listBox_MouseMove_1(object sender, MouseEventArgs e)
        {
            int index = c5_listBox.SelectedIndex;
            if (index >= 0)
            {
                ListBoxItem listBoxItem = (ListBoxItem)c5_listBox.Items.GetItemAt(index);
                string itemName = (string)listBoxItem.Content;

                BitmapImage myBitmapImage = BitmapImageFromURI.GetBitmapImage(new Uri(@"" + clusterer.getImageByName(itemName)));
                Image image = new Image();
                image.Source = myBitmapImage;
                ToolTip tt = new ToolTip();
                tt.MaxWidth = 200;
                tt.Content = image;
                listBoxItem.ToolTip = tt;
                ToolTipService.SetInitialShowDelay(listBoxItem, 1);
            } 
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            double accuracy = clusterer.calculateAccuracy();
            MessageBox.Show("Calculated Accuracy is " + (accuracy * 100) + "%");
        }
    }
}
