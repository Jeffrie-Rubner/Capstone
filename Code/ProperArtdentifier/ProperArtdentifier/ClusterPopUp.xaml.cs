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

namespace ProperArtdentifier
{
    /// <summary>
    /// Interaction logic for ClusterPopUp.xaml
    /// </summary>
    public partial class ClusterPopUp : Window
    {
        public ClusterPopUp()
        {
            InitializeComponent();
        }

        internal void SetTextBox(string clusterName)
        {
            Result_Label.Content = "Image is placed in " + clusterName;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
