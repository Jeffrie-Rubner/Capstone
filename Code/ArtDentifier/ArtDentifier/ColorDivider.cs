using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ArtDentifier
{
    class ColorDivider
    {
        private Dictionary<int, int> RedOccurences;
        private Dictionary<int, int> GreenOccurences;
        private Dictionary<int, int> BlueOccurences;
        private Dictionary<int, int> AlphaOccurences;
        private Dictionary<string, Dictionary<int, int>> OccurenceWrapper;

        public ColorDivider()
        {
            OccurenceWrapper = new Dictionary<string,Dictionary<int,int>>();
            initializeColorDictionaries();
        }

        public void addColor(Color color)
        {
            insertColor("red", color.R);
            insertColor("green", color.G);
            insertColor("blue", color.B);
            insertColor("alpha", color.A);
        }

        private void insertColor(string s, byte b)
        {
            if (OccurenceWrapper[s].ContainsKey(b))
            {
                OccurenceWrapper[s][b]++;
            }
            else
            {
                OccurenceWrapper[s].Add(b, 1);
            }
        }

        #region Getters
        public byte getMostFrequentRed()
        {
            return getMostFrequentValue("red");
        }

        public byte getMostFrequentGreen()
        {
            return getMostFrequentValue("green");
        }

        public byte getMostFrequentBlue()
        {
            return getMostFrequentValue("blue");
        }

        public byte getMostFrequentAlpha()
        {
            return getMostFrequentValue("alpha");
        }

        private byte getMostFrequentValue(string s)
        {
            int indexOfMax = 0;
            for (int i = 1; i < OccurenceWrapper[s].Count; i++)
            {
                if (OccurenceWrapper[s][i] > OccurenceWrapper[s][indexOfMax])
                {
                    indexOfMax = i;
                }
            }
            // need to get key value
            return OccurenceWrapper[s][indexOfMax];
        }
        #endregion

        private void initializeColorDictionaries()
        {
            RedOccurences = new Dictionary<int, int>();
            GreenOccurences = new Dictionary<int, int>();
            BlueOccurences = new Dictionary<int, int>();
            AlphaOccurences = new Dictionary<int, int>();
            OccurenceWrapper.Add("red", RedOccurences);
            OccurenceWrapper.Add("green", GreenOccurences);
            OccurenceWrapper.Add("blue", BlueOccurences);
            OccurenceWrapper.Add("alpha", AlphaOccurences);
        }
    }
}