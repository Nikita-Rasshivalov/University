using System.Windows.Media;

namespace FEM.BLL.Data
{
    /// <summary>
    /// Материал конечного элемента
    /// </summary>
    public class MaterialDTO
    {
        public MaterialDTO()
        {

        }
        /// <summary>
        /// Создание материала
        /// </summary>
        /// <param name="id">Идентификатор материала</param>
        /// <param name="name">Имя материала</param>
        /// <param name="color">Цвет материала</param>
        /// <param name="modulus">Модуль Юнга материала при 18 градусах цельсия (Н/м^2)</param>
        /// <param name="coefficient">Коэффициент Пуассона материала</param>
        public MaterialDTO(int id, string name, Color color, double modulus, double coefficient)
        {
            Id = id;
            Name = name;
            Color = color;
            YoungModulus = modulus;
            PuassonsCoefficient = coefficient;
        }
        /// <summary>
        /// Идентификатор материала
        /// </summary>
        public int Id { get; }
        /// <summary>
        /// Имя материала
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Цвет материала
        /// </summary>
        public Color Color { get; }
        /// <summary>
        /// Модуль Юнга материала при 18 градусах цельсия (Н/м^2)
        /// </summary>
        public double YoungModulus { get; }
        /// <summary>
        /// Коэффициент Пуассона материала
        /// </summary>
        public double PuassonsCoefficient { get; }

        private double? _shearModulus;
        /// <summary>
        /// Модуль сдвига материала
        /// </summary>
        public double ShearModulus
        {
            get
            {
                if (_shearModulus == null)
                {
                    _shearModulus = YoungModulus / (2 * (1 + PuassonsCoefficient));
                }
                return (double)_shearModulus;
            }
        }

        private double? _lameCoefficient;
        /// <summary>
        /// Коэффициент Ламе материала
        /// </summary>
        public double LameCoefficient
        {
            get
            {
                if (_lameCoefficient == null)
                {
                    _lameCoefficient = PuassonsCoefficient * YoungModulus / ((1 + PuassonsCoefficient) * (1 - 2 * PuassonsCoefficient));
                }
                return (double)_lameCoefficient;
            }
        }
    }
}
