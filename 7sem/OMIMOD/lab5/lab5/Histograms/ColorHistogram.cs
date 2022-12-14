using System;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Histograms
{
    [Serializable]
    public class ColorHistogram
    {
        private readonly Color _color;
        private readonly int[] _histogram;

        private ColorHistogram(Color color, int length)
        {
            _color = color;
            _histogram = new int[length];
        }

        public bool IsContainsColorHistogram(ColorHistogram colorHistogram)
        {
            var isContainsColorHistogram = false;

            if (colorHistogram._color == _color)
            {
                for (var i = 0; i <= _histogram.Length - colorHistogram._histogram.Length && !isContainsColorHistogram; ++i)
                {
                    for (var j = 0; j < colorHistogram._histogram.Length; ++j)
                    {
                        if (colorHistogram._histogram[j] != _histogram[j + i])
                        {
                            break;
                        }

                        if (j == colorHistogram._histogram.Length - 1)
                        {
                            isContainsColorHistogram = true;
                        }
                    }
                }
            }

            return isContainsColorHistogram;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            for (var i = _histogram.Max(); i > 0; --i)
            {
                for (var j = 0; j < _histogram.Length; ++j)
                {
                    stringBuilder.Append(_histogram[j] >= i ? '*' : ' ');
                }

                stringBuilder.Append(Environment.NewLine);
            }

            return stringBuilder.ToString();
        }

        #region Factory Methods

        public static ColorHistogram CreateColorHistogramFromBitmap(Bitmap bitmap, Color color)
        {
            var colorHistogram = new ColorHistogram(color, bitmap.Size.Width);

            for (var x = 0; x < bitmap.Width; ++x)
            {
                for (var y = 0; y < bitmap.Height; ++y)
                {
                    if (bitmap.GetPixel(x, y).ToArgb() == color.ToArgb())
                    {
                        colorHistogram._histogram[x]++;
                    }
                }
            }

            return colorHistogram;
        }

        public static ColorHistogram CreateColorHistogramForCircle(int radius, Color color)
        {
            var colorHistogram = new ColorHistogram(color, radius * 2 + 1);

            colorHistogram._histogram[radius] = 2;
            colorHistogram._histogram[0] = 1;
            colorHistogram._histogram[2 * radius] = 1;

            for (double x0 = -radius; x0 < 0; ++x0)
            {
                var y0 = Math.Sqrt(radius * radius - x0 * x0);

                var x1 = x0 + 1;
                var y1 = Math.Sqrt(radius * radius - x1 * x1);

                var dx = x1 - x0;
                var dy = y1 - y0;

                var maxDelta = Math.Abs(Math.Max(dx, dy));

                dx = dx / maxDelta;
                dy = dy / maxDelta;

                var x = x0;
                var y = y0 + (x0 == -radius ? 1 : 0);

                while (x < x1 && y < y1)
                {
                    colorHistogram._histogram[radius + (int)Math.Round(x)] += 2;
                    colorHistogram._histogram[radius - (int)Math.Round(x)] += 2;

                    x += dx;
                    y += dy;
                }

                //if (y1 - y0 > 0.5)
                //{
                //    colorHistogram._histogram[x0 + radius] = ((int)Math.Round(Math.Sqrt(y1 - y0))) * 2 + (x0 == -radius ? 1 : 0);
                //    colorHistogram._histogram[radius - x0] = ((int)Math.Round(Math.Sqrt(y1 - y0))) * 2 + (x0 == -radius ? 1 : 0);
                //}
                //else
                //{
                //    colorHistogram._histogram[x0 + radius] = 2;
                //    colorHistogram._histogram[radius - x0] = 2;
                //}
            }

            return colorHistogram;
        }

        #endregion
    }
}