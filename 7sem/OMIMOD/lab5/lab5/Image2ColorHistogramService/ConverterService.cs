using System.Drawing;
using Histograms;

namespace Image2ColorHistogramService
{
   public class ConverterService : IConverter
    {
        public ColorHistogram Convert(Bitmap bitmap, Color color)
        {
            return ColorHistogram.CreateColorHistogramFromBitmap(bitmap, color);
        }
    }
}