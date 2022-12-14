namespace Common
{
    public class Square
    {
        public Point TopLeft { get; set; }

        public Point TopRight { get; set; } 

        public Point BottomLeft { get; set; }

        public Point BottomRight { get; set; }

        public List<string> LineEquation { get; set; } = new List<string>();

        public Square(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight) 
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomLeft = bottomLeft;
            BottomRight = bottomRight;
            LineEquation.Add(GetEquation(bottomLeft, topLeft));
            LineEquation.Add(GetEquation(topLeft, topRight));
            LineEquation.Add(GetEquation(topRight, bottomRight));
            LineEquation.Add(GetEquation(bottomRight, bottomLeft));
        }

        public static Square? FromPoints(List<Point> points)
        {
            if(points.Count < 4)
            {
                return null;
            }

            var topLeft = points[0];
            for(var i = 0; i < points.Count; i++)
            {
                if (points[i].X <= topLeft.X && points[i].Y <= topLeft.Y)
                {
                    topLeft = points[i];
                }
            }

            var topRight = points[0];
            for (var i = 0; i < points.Count; i++)
            {
                if (points[i].X >= topRight.X && points[i].Y <= topRight.Y)
                {
                    topRight = points[i];
                }
            }

            var bottomLeft = points[0];
            for (var i = 0; i < points.Count; i++)
            {
                if (points[i].X <= bottomLeft.X && points[i].Y >= bottomLeft.Y)
                {
                    bottomLeft = points[i];
                }
            }


            var bottomRight = points[0];
            for (var i = 0; i < points.Count; i++)
            {
                if (points[i].X >= bottomRight.X && points[i].Y >= bottomRight.Y)
                {
                    bottomRight = points[i];
                }
            }

            var v1X = bottomLeft.X - topLeft.X;
            var v1Y = bottomLeft.Y - topLeft.Y;

            var v2X = topLeft.X - topRight.X;
            var v2Y = topLeft.Y - topRight.Y;
            var cos = Math.Abs(v1X * v2X + v1Y * v2Y) / (Math.Sqrt(Math.Pow(v1X, 2) + Math.Pow(v1Y, 2)) + Math.Sqrt(Math.Pow(v2X, 2) + Math.Pow(v2Y, 2)));
            if (cos != 0)
            {
                return null;
            }

            var line1 = GetLength(bottomLeft, topLeft);
            var line2 = GetLength(topLeft, topRight);
            var line3 = GetLength(topRight, bottomRight);
            var line4 = GetLength(bottomRight, bottomLeft);

            if(line1 != line2 || line1 != line3 || line1 != line4) 
            {
                return null;
            }

            return new Square(topLeft, topRight, bottomLeft, bottomRight);
        }

        private string GetEquation(Point point1, Point point2)
        {
            return $"x - {point1.X} / {point2.X - point1.X} = y - {point1.Y} / {point2.Y - point1.Y}";
        }

        private static double GetLength(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }
    }
}
