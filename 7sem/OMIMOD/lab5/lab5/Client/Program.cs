using Histograms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;

namespace Client
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface IConverter
    {
        [OperationContract]
        ColorHistogram Convert(Bitmap bitmap, Color color);
    }

    internal class Program
    {
        private const string PathToImagesFolder = "../../../Images";

        private static List<Bitmap> Images = new List<Bitmap>();

        static void Main(string[] args)
        {
            foreach (var filename in Directory.EnumerateFiles(PathToImagesFolder))
            {
                var image = Image.FromFile(filename);

                image.Tag = filename.Split('\\').Last();

                Images.Add((Bitmap)image);
            }
            
            // Create a channel factory.  
            var myBinding = new NetNamedPipeBinding();
            var myEndpoint = new EndpointAddress("net.pipe://localhost/Image2ColorHistogramService/ConverterService");
            var myChannelFactory = new ChannelFactory<IConverter>(myBinding, myEndpoint);

            // Create a channel.  
            IConverter client = myChannelFactory.CreateChannel();

            while (true)
            {
                Console.Write("Enter radius: ");

                var radius = int.Parse(Console.ReadLine());

                Console.Write("Enter color code: ");

                var color = ColorTranslator.FromHtml(Console.ReadLine());
                var timer = new Stopwatch();
                timer.Start();

                var searchingCircleColorHistrogram = ColorHistogram.CreateColorHistogramForCircle(radius, color);

                foreach (var image in Images)
                {
                    var imageColorHistogram = client.Convert(image, color);

                    if (imageColorHistogram.IsContainsColorHistogram(searchingCircleColorHistrogram))
                    {
                        Console.WriteLine("Image \"{0}\" has searching circle.", image.Tag);
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