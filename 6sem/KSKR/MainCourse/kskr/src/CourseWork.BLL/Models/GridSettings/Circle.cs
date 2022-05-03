using CourseWork.Common.Models;
using System;

namespace CourseWork.BLL.Models.GridSettings
{
    public class Circle : IFigure
    {
        public int Radius { get; set; }

        public bool IsFill { get; set; }

        public Point StartPoint { get; set; }

        public int Height { get; set; }

        public void Draw(int[,,] matrix, double step = 0.1)
        {
            var filler = IsFill ? 1 : 0;
            for (var z = (int)(StartPoint.Z/step); z < StartPoint.Z/step + Height/step; z++)
            {
                for(var x = 0; x < matrix.GetLength(0); x++)
                {
                    for (var y = 0; y < matrix.GetLength(1); y++)
                    {
                        if(Math.Sqrt(Math.Pow(StartPoint.X/step - x, 2) + Math.Pow(StartPoint.Y / step - y, 2)) <= Radius / step )
                        {
                            matrix[x, y, z] = filler;
                        }
                    }
                }
            }
        }
    }
}
