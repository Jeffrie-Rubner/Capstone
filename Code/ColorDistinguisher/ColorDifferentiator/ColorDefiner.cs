using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;

namespace ColorDifferentiator
{
    enum ColorTypes
    {
        red = 0,
        blue = 1
    }
    class ColorDefiner
    {
        private List<Color> redShades = new List<Color>();
        private List<Color> blueShades = new List<Color>();


        public ColorDefiner()
        {

        }

        public void InitializeColorLists()
        {
            redShades.Add(Color.FromRgb(255, 0, 0));
            redShades.Add(Color.FromRgb(200, 0, 0));
            redShades.Add(Color.FromRgb(255, 0, 130));
            redShades.Add(Color.FromRgb(200, 0, 70));
            redShades.Add(Color.FromRgb(255, 40, 0));
            redShades.Add(Color.FromRgb(200, 10, 0));

            blueShades.Add(Color.FromRgb(0, 0, 255));
            blueShades.Add(Color.FromRgb(0, 0, 200));
            blueShades.Add(Color.FromRgb(0, 255, 255));
            blueShades.Add(Color.FromRgb(0, 150, 200));
            blueShades.Add(Color.FromRgb(15, 100, 255));
            blueShades.Add(Color.FromRgb(5, 150, 200));

        }


        //This method will be the machine learning method that determines what group a color belongs to
        public void ClassifyColor()
        {

        }

    }
}
