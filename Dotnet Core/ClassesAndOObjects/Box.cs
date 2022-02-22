using System;
using System.Collections.Generic;
using System.Text;

namespace ClassesAndOObjects
{
    class Box
    {
        public double Length { get; set; }
        public double Breadth { get; set; }
        public double Height { get; set; }

        public double getVolume()
        {
            return Length * Breadth * Height;
        }

        public double getArea()
        {
            return Length * Breadth;
        }
    }
}
