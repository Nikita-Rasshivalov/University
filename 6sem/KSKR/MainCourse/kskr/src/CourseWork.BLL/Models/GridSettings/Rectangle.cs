using CourseWork.Common.Models;

namespace CourseWork.BLL.Models.GridSettings
{
    public class Rectangle : IFigure
    {
        public Point StartPoint { get; set; }
        public bool IsFill { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public void Draw(int[,,] matrix, double step = 0.1)
        {
            var filler = IsFill ? 1 : 0;

            for (var z = (int)(StartPoint.Z / step); z < StartPoint.Z / step + Height / step; z++)
            {
                for (var x = (int)(StartPoint.X / step); x < StartPoint.X / step + Height / step; x++)
                {
                    for (var y = (int)(StartPoint.Y / step); y < StartPoint.Y / step + Height / step; y++)
                    {
                        matrix[x, y, z] = filler;
                    }
                }
            }
        }
    }
}
