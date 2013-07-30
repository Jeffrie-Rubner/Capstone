using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ArtDentifier
{
    static class ColorComparitor
    {
        public static bool Compare(Color color1, Color color2)
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
