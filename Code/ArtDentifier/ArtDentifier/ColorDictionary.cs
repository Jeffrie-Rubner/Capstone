﻿using System;
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
        private Color[] Colors;
        private int[] CountValue;
        private int listSize = 0;
        #endregion

        #region Constructor
        public ColorDictionary()
        {
            Colors = new Color[10];
            CountValue = new int[10];
        }
        #endregion

        public void addColor(Color color)
        {
            for(int i = 0; i < listSize; i++)
            {
                if (ColorComparitor.Compare(Colors[i], color))
                {
                    CountValue[i]++;
                    return;
                }
            }
            checkSize();
            Colors[listSize] = color;
            CountValue[listSize] = 0;
            listSize++;
        }

        #region Getters
        public Color getMostFrequentColor()
        {
            return Colors[getIndexOfGreatestFrequency()];
        }

        public int getGreatestFrequency()
        {
            return CountValue[getIndexOfGreatestFrequency()];
        }
        #endregion

        #region Private Methods
        private void checkSize()
        {
            if (listSize > Colors.Length)
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