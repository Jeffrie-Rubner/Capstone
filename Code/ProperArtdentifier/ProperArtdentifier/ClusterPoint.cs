using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProperArtdentifier
{
    class ClusterPoint : IClusterableItem
    {
        public override byte Red()
        {
            return red;
        }
        public override byte Blue()
        {
            return blue;
        }
        public override byte Green()
        {
            return green;
        }

        public ClusterPoint(double _width, double _height, byte redVal, byte greenVal, byte blueVal)
        {
            width = _width;
            height = _height;
            red = redVal;
            blue = blueVal;
            green = greenVal;
        }

        public static ClusterPoint getRandomClusterPoint()
        {
            Random gen = new Random();
            double _width = gen.NextDouble() * 1000;
            System.Threading.Thread.Sleep(50);
            double _height = gen.NextDouble() * 1000;

            byte _red = (byte)gen.Next(255);
            System.Threading.Thread.Sleep(50);
            byte _green = (byte)gen.Next(255);
            System.Threading.Thread.Sleep(50);
            byte _blue = (byte)gen.Next(255);

            return new ClusterPoint(_width, _height, _red, _green, _blue);
        }


    }
}
