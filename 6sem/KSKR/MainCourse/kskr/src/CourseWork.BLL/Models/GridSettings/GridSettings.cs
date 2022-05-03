using System.Collections.Generic;

namespace CourseWork.BLL.Models.GridSettings
{
    public class GridSettings
    {
        public List<IFigure> Figures { get; set; }

        public int Lenght { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
