using System;


namespace CourseWork.BLL.Tools
{
    /// <summary>
    /// Класс для создания градиента
    /// </summary>
    public class GradientMaker
    {
        /// <summary>
        /// Возвращает RGB значения определенного значения в диапазоне значений градиента
        /// </summary>
        /// <param name="minValue">Минимальное значение градиента</param>
        /// <param name="maxValue">Максимальное значение градиента</param>
        /// <param name="value">Текущее значение градиента</param>
        /// <returns>Цвет RGB, отражающий собой цвет данного значения в диапазоне значений градиента</returns>
        public (byte, byte, byte) GetRGBGradientValue(double minValue, double maxValue, double value)
        {
            double ratio = 2 * (value - minValue) / (maxValue - minValue);
            byte B = (byte)Math.Max(0, 255 * (1 - ratio));
            byte R = (byte)Math.Max(0, 255 * (ratio - 1));
            byte G = (byte)(255 - B - R);
            return (R, G, B);
        }
    }
}
