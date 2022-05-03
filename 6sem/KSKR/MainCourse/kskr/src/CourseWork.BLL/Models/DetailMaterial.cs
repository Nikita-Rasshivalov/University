using System.Windows.Media;

namespace CourseWork.BLL.Models
{
    public class DetailMaterial
    {
        public DetailMaterial(string name, double youngModulus, double poissonCoefficient, Color color)
        {
            Name = name;
            YoungModulus = youngModulus;
            PoissonCoefficient = poissonCoefficient;
            LameCoefficient = PoissonCoefficient * YoungModulus / ((1 + PoissonCoefficient) * (1 - 2 * PoissonCoefficient));
            ShiftModulus = YoungModulus / (2 * (1 + PoissonCoefficient));
            Color = color;
        }

        public string Name { get; }

        public double YoungModulus { get; }

        public double PoissonCoefficient { get; }

        public double LameCoefficient { get; }

        public double ShiftModulus { get; }

        public Color Color { get; }

        public string GetInfo()
        {
            return $"Коэффицент Пуассона:{PoissonCoefficient}\nМодуль Юнга: {YoungModulus}";
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
