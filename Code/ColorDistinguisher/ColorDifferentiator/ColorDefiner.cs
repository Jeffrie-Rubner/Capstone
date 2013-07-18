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
            redShades.Add(Color.FromRgb(255, 100, 20));
            redShades.Add(Color.FromRgb(200, 40, 5));

            blueShades.Add(Color.FromRgb(0, 0, 255));
            blueShades.Add(Color.FromRgb(0, 0, 200));
            blueShades.Add(Color.FromRgb(0, 200, 255));
            blueShades.Add(Color.FromRgb(0, 150, 200));
            blueShades.Add(Color.FromRgb(15, 100, 255));
            blueShades.Add(Color.FromRgb(5, 150, 200));

        }


        //This method will be the machine learning method that determines what group a color belongs to
        public String ClassifyColor(Color input)
        {
            double redChance = 0.00;
            double blueChance = 0.00;
            foreach (Color c in redShades)
            {
                double redRatio = ((double)Math.Abs(c.R - input.R)) / 255;
                double greenRatio = ((double)Math.Abs(c.G - input.G)) / 255;
                double blueRatio = ((double)Math.Abs(c.B - input.B)) / 255;
                double totalColorDifferenceRatio = 1 - ((redRatio + greenRatio + blueRatio)/3);
                redChance = redChance > totalColorDifferenceRatio ? redChance : totalColorDifferenceRatio;
            }
            foreach (Color c in blueShades)
            {
                double redRatio = ((double)Math.Abs(c.R - input.R)) / 255.0;
                double greenRatio = ((double)Math.Abs(c.G - input.G)) / 255.0;
                double blueRatio = ((double)Math.Abs(c.B - input.B)) / 255.0;
                double totalColorDifferenceRatio = 1.0 - ((redRatio + greenRatio + blueRatio) / 3.0);
                blueChance = blueChance > totalColorDifferenceRatio ? blueChance : totalColorDifferenceRatio;
            }
            if (redChance > blueChance)
            {
                redShades.Add(input);
            }
            else
            {
                blueShades.Add(input);
            }

           // return redChance > blueChance ? "red": "blue";
            return "red: " + redChance + "blue: " + blueChance;

        }

    }
}
