using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ArtDentifier
{
    class ColorDictionary
    {
        private Color[] Colors;
        private int[] CountValue;
        private int listSize = 0;

        public ColorDictionary()
        {
            Colors = new Color[10];
            CountValue = new int[10];
        }

        public void addColor(Color color)
        {
            for(int i = 0; i < listSize; i++)
            {
                if (isSameColor(Colors[i], color))
                {
                    CountValue[i]++;
                    return;
                }
            }
            if (listSize < Colors.Length)
            {
                Color[] tempColors = new Color[Colors.Length * 2];
                int[] tempCounts = new int[CountValue.Length * 2];
                for (int j = 0; j < listSize; j++)
                {
                    tempColors[j] = Colors[j];
                    tempCounts[j] = CountValue[j];
                }
                Colors = tempColors;
                CountValue = tempCounts;
            }
            Colors[listSize] = color;
            CountValue[listSize] = 0;
            listSize++;
        }

        public bool isSameColor(Color color1, Color color2)
        {
            bool isSame = false;
            if (color1.A == color2.A)
            {
                if (color1.R == color2.R)
                {
                    if (color1.G == color2.G)
                    {
                        if (color1.B == color2.B)
                        {
                            isSame = true;
                        }
                    }
                }
            }
            return isSame;
        }
    }
}
