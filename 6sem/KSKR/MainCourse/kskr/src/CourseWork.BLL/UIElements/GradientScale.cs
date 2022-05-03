using System;
using System.Collections.Generic;
using System.Windows.Media;
using CourseWork.BLL.Tools;

namespace CourseWork.BLL.UIElements
{
    /// <summary>
    /// Градиентная шкала
    /// </summary>
    public class GradientScale
    {
        /// <summary>
        /// Создание градиентной шкалы
        /// </summary>
        /// <param name="gradientMaker">Класс, создающий градиент</param>
        /// <param name="minValue">Минимальное значение шкалы</param>
        /// <param name="maxValue">Максимальное значение шкалы</param>
        /// <param name="stepCount">Количество шагов градиента</param>
        public GradientScale(GradientMaker gradientMaker, double minValue, double maxValue, double minValueUnder, double maxValueUnder, int stepCount)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            MinValueUnder = minValueUnder;
            MaxValueUnder = maxValueUnder;

            double step = (maxValue - minValue) / stepCount;
            IList<GradientStop> gradientStops = new List<GradientStop>();
            double currentValue = minValue;
            while (currentValue < maxValue)
            {
                double currentValueNormalized = (currentValue - minValue) / (maxValue - minValue);
                (byte R, byte G, byte B) = gradientMaker.GetRGBGradientValue(minValue, maxValue, currentValue);
                var stop = new GradientStop(Color.FromArgb(0x99, R, G, B), 1 - currentValueNormalized);
                gradientStops.Add(stop);
                currentValue += step;
            }

            Brush = new LinearGradientBrush();
            Brush.GradientStops = new GradientStopCollection(gradientStops);
        }

        public LinearGradientBrush Brush { get; set; }
        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        public double MaxValueUnder { get; set; }
        public double MinValueUnder { get; set; }

        public string MaxValueText
        {
            get
            {
                if (MaxValue > 10000 || MaxValue < 1e-4)
                {
                    return MaxValue.ToString("e3");
                }
                return $"{Math.Round(MaxValue, 4)}";
            }
        }
        public string MinValueText
        {
            get
            {
                if (MinValue > 10000 || MinValue < 1e-4)
                {
                    return MinValue.ToString("e3");
                }
                return $"{Math.Round(MinValue, 4)}";
            }
        }

        public string MaxValueTextUnder
        {
            get
            {
                if (MaxValueUnder > 10000 || MaxValueUnder < 1e-4)
                {
                    return MaxValueUnder.ToString("e3");
                }
                return $"{Math.Round(MaxValueUnder, 4)}";
            }
        }
        public string MinValueTextUnder
        {
            get
            {
                if (MinValueUnder > 10000 || MinValueUnder < 1e-4)
                {
                    return MinValueUnder.ToString("e3");
                }
                return $"{Math.Round(MinValueUnder, 4)}";
            }
        }
    }
}