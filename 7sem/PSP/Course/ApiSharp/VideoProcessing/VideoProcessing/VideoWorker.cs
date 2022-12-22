using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using VideoProcessing.Handlers;
using Yolov5;

namespace VideoProcessing
{
    internal class VideoWorker
    {
        public Dictionary<string, IYoloDetector> AvailableYoloNets { get; set; }
        public AppSettings Settings { get; set; }

        public VideoWorker(Dictionary<string, IYoloDetector> availableYoloNets, AppSettings settings)
        {
            this.AvailableYoloNets = availableYoloNets;
            Settings = settings;
        }

        public Task Work(int allowedError, int allowedDelay, int globalRefreshTime, Stopwatch globalTimer)
        {
            var window = new Window("Object detection by Nikita");
            var capture = new VideoCapture(0);
            var image = new Mat();
            int countError = 0;
            var delay = 0;
            List<DetectedYolo5Object> data = new();
            var timer = new Stopwatch();
            timer.Start();
            while (capture.IsOpened())
            {
                capture.Read(image);
                if (image.Empty())
                    break;
                if (Cv2.WaitKey(1) == 113) //Q
                    break;
                window.ShowImage(image);
                try
                {
                    if (countError < allowedError)
                    {
                        data = GetLocalOrServerData(delay, allowedDelay, image);
                    }
                    else
                    {
                        Console.WriteLine($"Local processing.");
                        data = new LocalHandler(AvailableYoloNets).GetData(image, Settings);
                    }
                }
                catch (Exception)
                {
                    countError++;
                    Console.WriteLine($"Server error. Number requests:{countError}");
                }

                Console.WriteLine($"FPS:{1000 / (timer.ElapsedMilliseconds)} Processing delay {timer.ElapsedMilliseconds}");
                timer.Restart();

                image = GetBoxes(data, image);
                window.ShowImage(image);
                if (globalTimer.ElapsedMilliseconds >= globalRefreshTime)
                {
                    countError = 0;
                    globalTimer.Restart();
                }
            }
            return Task.CompletedTask;
        }

        public static Mat GetBoxes(List<DetectedYolo5Object> data, Mat image)
        {
            foreach (var i in data)
            {
                HersheyFonts fontFace = HersheyFonts.HersheySimplex;
                double scale = 1.0 / 3;
                var confidence = Math.Round(i.Confidence, 3);
                Cv2.Rectangle(image, new Point(i.BoxX, i.BoxY), new Point(i.BoxX + i.BoxWidth, i.BoxY + i.BoxHeight), 255, 1);
                Cv2.PutText(image, "Cucumber " + confidence, new Point(i.BoxX - 10, i.BoxY - 10), fontFace, scale, 255);
            }
            return image;
        }


        public List<DetectedYolo5Object> GetLocalOrServerData(int delay, int allowedDelay, Mat image)
        {
            List<DetectedYolo5Object> result;
            var sw = new Stopwatch();
            sw.Start();
            var checkResult = new ServerHandler().GetData(image, Settings);
            sw.Stop();
            delay = (int)sw.ElapsedMilliseconds;

            if (delay < allowedDelay)
            {
                result = checkResult;
                Console.WriteLine($"Processing on server. Delay:{delay}");
            }
            else
            {
                result = new LocalHandler(AvailableYoloNets).GetData(image, Settings);
                Console.WriteLine($"Local processing.");
            }
            return result;
        }
    }
}
