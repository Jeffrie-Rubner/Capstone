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
        private List<Color> redShades = new List<Color>();
        private List<Color> blueShades = new List<Color>();
        #endregion  

        #region Constructor
        public ColorDefiner()
        {
            AllColorsList = new Dictionary<string, List<Color>>();
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
            Dictionary<String, double> ColorProbabilityStorer = new Dictionary<string, double>();
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
                foreach (KeyValuePair<String, List<Color>> kv in AllColorsList)
                {
                    if (kv.Value.Equals(lists))
                    {
                        ColorProbabilityStorer.Add(kv.Key, chance);
                    }            
                }
            }
            String returnString = "";
            String largestKeyName = "";
            double previousValue = 0.00;
            foreach(KeyValuePair<String, double> kv in ColorProbabilityStorer)
            {
                returnString += kv.Key + ": " + kv.Value + ", ";
                largestKeyName = previousValue > kv.Value ? largestKeyName : kv.Key;
                previousValue = previousValue > kv.Value ? previousValue : kv.Value;
            }
            AllColorsList[largestKeyName].Add(input);
            returnString = returnString.TrimEnd(",".ToCharArray());

            return returnString;

        }

    }
}
