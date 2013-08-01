using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtDentifier
{
    class DimensionAnalyzer
    {
        public double getDimensionRatio(ArtImage artImage)
        {
            double widthHeightRatio = (double)artImage.Width / artImage.Height;
            return widthHeightRatio;
        }
    }
}
