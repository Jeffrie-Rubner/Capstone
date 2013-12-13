using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperArtdentifier
{
    public abstract class IClusterableItem
    {
        protected double width;
        public double Width { get { return width; } protected set { width = value; } }
        protected double height;
        public double Height { get { return height; } protected set { height = value; } }

        protected byte red = 0;
        protected byte blue = 0;
        protected byte green = 0;

        public abstract byte Red();
        public abstract byte Blue();
        public abstract byte Green();

        public bool IsEqual(IClusterableItem item)
        {
            bool b1 = width == item.Width;
            bool b2 = height == item.Height;
            bool b3 = red == item.Red();
            bool b4 = green == item.Green();
            bool b5 = blue == item.Blue();

            return (b1 && b2 && b3 && b4 && b5);

        }
    }
}
