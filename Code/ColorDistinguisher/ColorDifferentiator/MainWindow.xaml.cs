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
using System.Drawing;

namespace ColorDifferentiator
{ 
    public partial class MainWindow : Window
    {

        #region Fields
        private ColorDefiner colorDefiner;
        private Color CurrentColorOfRectangle;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            colorDefiner = new ColorDefiner();
            updateColorBox();
        }
        #endregion

        #region EventHandlerMethods
        private void Slider_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var slider = (Slider)sender;
                Point position = e.GetPosition(slider);
                double d = 1.0d / slider.ActualWidth * position.X;
                var p = slider.Maximum * d;
                slider.Value = p;
                if (slider.Name.Equals("RedSlider"))
                {
                    RedNum.Text = "" + (int)slider.Value;
                }
                else if (slider.Name.Equals("BlueSlider"))
                {
                    BlueNum.Text = "" + (int)slider.Value;
                }
                else if (slider.Name.Equals("GreenSlider"))
                {
                    GreenNum.Text = "" + (int)slider.Value;
                }
                else if (slider.Name.Equals("ASlider"))
                {
                    ANum.Text = "" + (int)slider.Value;
                }
                updateColorBox();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ResultText.Text = "Result: " + colorDefiner.ClassifyColor(CurrentColorOfRectangle);
        }
        #endregion

        #region HelperMethods
        private void updateColorBox()
        {
            byte redval = ((int)RedSlider.Value) > 255 ? (byte)255 : (byte)((int)RedSlider.Value);
            byte blueval = ((int)BlueSlider.Value) > 255 ? (byte)255 : (byte)((int)BlueSlider.Value);
            byte greenval = ((int)GreenSlider.Value) > 255 ? (byte)255 : (byte)((int)GreenSlider.Value);
            byte aval = ((int)ASlider.Value) > 255 ? (byte)255 : (byte)((int)ASlider.Value);
            CurrentColorOfRectangle = Color.FromArgb(aval, redval, greenval, blueval);
            ColorChosen.Fill = new SolidColorBrush(CurrentColorOfRectangle);
        }
        #endregion

    }
}
