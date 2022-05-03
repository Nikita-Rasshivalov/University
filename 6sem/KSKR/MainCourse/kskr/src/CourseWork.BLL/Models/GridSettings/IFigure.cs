using CourseWork.Common.Models;

namespace CourseWork.BLL.Models.GridSettings
{
    public interface IFigure
    {

        Point StartPoint { get; set; }

        int Height { get; set; }

        bool IsFill { get; set; }

        void Draw(int[,,] matrix, double step = 0.1);
    }
}
