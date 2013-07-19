using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;

namespace ColorDifferentiator
{

    class ColorDefiner
    {
        #region Fields
        private Dictionary<String, List<Color>> AllColorsList;
        private Dictionary<String, double> ColorProbabilityStorer;
        private List<Color> redShades = new List<Color>();
        private List<Color> blueShades = new List<Color>();
        #endregion  

        #region Constructor
        public ColorDefiner()
        {
            AllColorsList = new Dictionary<string, List<Color>>();
            ColorProbabilityStorer = new Dictionary<string, double>();
            redShades = new List<Color>();
            blueShades = new List<Color>();
            AllColorsList.Add("red", redShades);
            AllColorsList.Add("blue", blueShades);
            InitializeColorLists();
        }
        #endregion

        private void InitializeColorLists()
        {
            redShades.Add(Color.FromRgb(255, 0, 0));
            redShades.Add(Color.FromRgb(200, 0, 0));
            redShades.Add(Color.FromRgb(255, 0, 130));
            redShades.Add(Color.FromRgb(200, 0, 70));
            redShades.Add(Color.FromRgb(255, 100, 0));
            redShades.Add(Color.FromRgb(200, 40, 5));

            blueShades.Add(Color.FromRgb(0, 0, 255));
            blueShades.Add(Color.FromRgb(0, 0, 200));
            blueShades.Add(Color.FromRgb(130, 0, 255));
            blueShades.Add(Color.FromRgb(70, 0, 200));
            blueShades.Add(Color.FromRgb(0, 100, 255));
            blueShades.Add(Color.FromRgb(5, 40, 200));
        }


        //This method will be the machine learning method that determines what group a color belongs to
        public String ClassifyColor(Color input)
        {
            foreach (List<Color> lists in AllColorsList.Values)
            {
                double chance = new double();
                chance = 0.00;
                foreach (Color c in lists)
                {
                    double redRatio = ((double)Math.Abs(c.R - input.R)) / 255;
                    double greenRatio = ((double)Math.Abs(c.G - input.G)) / 255;
                    double blueRatio = ((double)Math.Abs(c.B - input.B)) / 255;
                    double totalColorDifferenceRatio = 1 - ((redRatio + greenRatio + blueRatio) / 3.00);
                    chance = chance > totalColorDifferenceRatio ? chance : totalColorDifferenceRatio;
                }
                ColorProbabilityStorer.Add(AllColorsList.FirstOrDefault(x => x.Equals(lists)).Key, chance);
            }

            #region commentedChunk
            /*
            foreach (Color c in redShades)
            {
                double redRatio = ((double)Math.Abs(c.R - input.R)) / 255;
                double greenRatio = ((double)Math.Abs(c.G - input.G)) / 255;
                double blueRatio = ((double)Math.Abs(c.B - input.B)) / 255;
                double totalColorDifferenceRatio = 1 - ((redRatio + greenRatio + blueRatio)/3.00);
                redChance = redChance > totalColorDifferenceRatio ? redChance : totalColorDifferenceRatio;
            }
            foreach (Color c in blueShades)
            {
                double redRatio = ((double)Math.Abs(c.R - input.R)) / 255.0;
                double greenRatio = ((double)Math.Abs(c.G - input.G)) / 255.0;
                double blueRatio = ((double)Math.Abs(c.B - input.B)) / 255.0;
                double totalColorDifferenceRatio = 1.0 - ((redRatio + greenRatio + blueRatio) / 3.00);
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
            return "red: " + redChance.ToString() + ", blue: " + blueChance.ToString();
           */
            #endregion
            return "method in progress";

        }

    }
}
