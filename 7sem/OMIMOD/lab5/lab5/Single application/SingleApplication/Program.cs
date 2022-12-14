using Histograms;
using System.Diagnostics;
using System.Drawing;

namespace SingleApplication
{
    internal static class Program
    {
        private const string PathToImagesFolder = "../../../../../Images";

        private static List<Bitmap> Images = new();

        private static void Main(string[] args)
        {
            foreach (var filename in Directory.EnumerateFiles(PathToImagesFolder))
            {
                var image = Image.FromFile(filename);

                image.Tag = filename.Split('\\').Last();

                Images.Add((Bitmap)image);
            }

            while (true)
            {
                Console.Write("Enter radius: ");

                var radius = int.Parse(Console.ReadLine()!);

                Console.Write("Enter color code: ");

                var color = ColorTranslator.FromHtml(Console.ReadLine()!);
                var timer = new Stopwatch();
                timer.Start();

                var searchingCircleColorHistrogram = ColorHistogram.CreateColorHistogramForCircle(radius, color);

                foreach (var image in Images)
                {
                    var imageColorHistogram = ColorHistogram.CreateColorHistogramFromBitmap(image, color);
                    
                    if (imageColorHistogram.IsContainsColorHistogram(searchingCircleColorHistrogram))
                    {
                        Console.WriteLine("Image \"{0}\" has searching circle.", image.Tag!);
                    }
                }
                timer.Stop();
                Console.WriteLine($"Execution time: {timer.ElapsedMilliseconds} milliseconds.");

                Console.WriteLine("Press any key for continue... (CTRL + C for exit)");

                Console.ReadKey();

                Console.Clear();
            }
        }
    }
}