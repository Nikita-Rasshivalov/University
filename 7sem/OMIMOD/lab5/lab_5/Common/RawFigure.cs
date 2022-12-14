using System.Collections.ObjectModel;

namespace Common
{
    [Serializable]
    public class RawFigure
    {
        public List<Point> Points { get; } = new List<Point>();

        public RawFigure(Point startPoint)
        {
            Points.Add(startPoint);
        }

        public bool AddRelated(int x, int y)
        {
            var isExistRelated = Points.Any(p => (p.X == x && p.Y - 1 == y) || (p.X == x && p.Y + 1 == y) || (p.X - 1 == x && p.Y == y) || (p.X + 1 == x && p.Y == y));

            if(isExistRelated )
            {
                Points.Add(new Point(x, y));
            }

            return isExistRelated;
        }
    }
}
