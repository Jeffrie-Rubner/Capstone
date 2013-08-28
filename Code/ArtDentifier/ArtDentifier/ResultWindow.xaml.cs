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
            string[] artistNames = new string[5];
            string[] averages = new string[5];
            for (int i = 0; i < 5; i++)
            {
                artistNames[i] = results[4 - i];
                averages[i] = results[(results.Length - 1) - i];
            }


        }

    }
}
