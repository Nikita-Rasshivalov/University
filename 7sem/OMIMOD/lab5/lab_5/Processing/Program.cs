using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HoughLineTransformation lineTransform = new HoughLineTransformation();
            // apply Hough line transofrm
            Bitmap sourceImage = (Bitmap)Bitmap.FromFile("Images/RedSquare.bmp");
            lineTransform.ProcessImage(sourceImage);
            Bitmap houghLineImage = lineTransform.ToBitmap();
            // get lines using relative intensity
            HoughLine[] lines = lineTransform.GetLinesByRelativeIntensity(0.5);

            foreach (HoughLine line in lines)
            {
                // ...
            }
        }
    }
}
