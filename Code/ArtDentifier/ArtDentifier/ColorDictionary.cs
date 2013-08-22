using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

//contains methods to interact with colors
namespace ArtDentifier
{
    class ColorDictionary
    {
        #region Fields
        private Color[] colors;
        private int[] CountValue;
        private int listSize = 0;
        #endregion

        #region Constructor
        public ColorDictionary()
        {
            colors = new Color[10];
            CountValue = new int[10];
        }
        #endregion

        public void addColor(Color color)
        {
            for(int i = 0; i < listSize; i++)
            {
                if (ColorComparitor.Compare(colors[i], color))
                {
                    CountValue[i]++;
                    return;
                }
            }
            checkSize();
            colors[listSize] = color;
            CountValue[listSize] = 1;
            listSize++;
        }

        #region Getters
        public Color getMostFrequentColor()
        {
            return colors[getIndexOfGreatestFrequency()];
        }

        public int getGreatestFrequency()
        {
            return CountValue[getIndexOfGreatestFrequency()];
        }

        public Dictionary<Color, int> getAllColors()
        {
            Dictionary<Color, int> allColorDictionary = new Dictionary<Color,int>();
            for (int i = 0; i < listSize; i++)
            {
                allColorDictionary.Add(colors[i], CountValue[i]);
            }
            return allColorDictionary;
        }
        #endregion

        #region Private Methods
        private void checkSize()
        {
            if (listSize >= colors.Length)
            {
                Color[] tempColors = new Color[colors.Length * 2];
                int[] tempCounts = new int[CountValue.Length * 2];
                for (int j = 0; j < listSize; j++)
                {
                    tempColors[j] = colors[j];
                    tempCounts[j] = CountValue[j];
                }
                colors = tempColors;
                CountValue = tempCounts;
            }
        }

        private int getIndexOfGreatestFrequency()
        {
            int indexOfMax = 0;
            for (int i = 1; i < listSize; i++)
            {
                if (CountValue[i] > CountValue[indexOfMax])
                {
                    indexOfMax = i;
                }
            }
            return indexOfMax;
        }
        #endregion
    }
}
