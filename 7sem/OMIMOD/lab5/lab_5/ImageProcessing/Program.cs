using Common;
using Common.SharedMemory;
using System.Diagnostics;
using System.Drawing;

namespace ImageProcessing
{
    internal class Program
    {
        private static int _colorR = 255;
        private static int _colorG = 53;
        private static int _colorB = 207;
        private const string FigureIdFileName = "FIGURE_ID_FILE_NAME10";
        private const string FigureFileName = "FIGURE_FILE_NAME";

        static async Task Main(string[] args)
        {
            await Produce();
            Console.ReadKey();
        }

        private static async Task Produce()
        {
            var sw = new Stopwatch();
            sw.Start();
            var intWriter = new IntWriter();
            var figureWriter = new FiguresDataWriter();
            var directory = new DirectoryInfo("Images");
            var id = 1;
            foreach (var file in directory.GetFiles())
            {
                if (file.Extension != ".bmp")
                {
                    continue;
                }

                var figures = new List<RawFigure>();
                var sourceImage = (Bitmap)Image.FromFile(file.FullName);
                for (var i = 0; i < sourceImage.Width; i++)
                {
                    for (var j = 0; j < sourceImage.Height; j++)
                    {
                        var pixel = sourceImage.GetPixel(i, j);
                        if (pixel.R == _colorR && pixel.G == _colorG && pixel.B == _colorB)
                        {
                            if (figures.Any(f => f.AddRelated(i, j)))
                            {
                                continue;
                            }

                            figures.Add(new RawFigure(new Common.Point(i, j)));
                        }
                    }
                }
                var imageFigures = new ImageFigures()
                {
                    FileName = file.Name,
                    Figures = figures,
                };
                Console.WriteLine($"Writing figures from {file.FullName}");
                await figureWriter.WriteAsync(imageFigures, $"{FigureFileName}{id}");
                await intWriter.WriteAsync(id, FigureIdFileName, (id - 1) * 4);
                id++;
            }

            await intWriter.WriteAsync(-1, FigureIdFileName, (id - 1) * 4);
            Console.WriteLine($"Elapsed for producing: {sw.ElapsedMilliseconds}");
        }
    }
}