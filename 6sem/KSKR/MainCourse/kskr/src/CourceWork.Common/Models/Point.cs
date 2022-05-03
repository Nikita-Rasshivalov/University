using System;

namespace CourseWork.Common.Models
{
    public class Point : ICloneable
    {
        public Point(double x,double y,double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point()
        {
        }
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public object Clone()
        {
            return new Point(X, Y, Z);
        }

        public override string ToString()
        {
            return $" {X} {Y} {Z}; ";
        }
    }
}
