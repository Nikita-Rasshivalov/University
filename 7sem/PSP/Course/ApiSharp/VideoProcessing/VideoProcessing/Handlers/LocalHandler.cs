using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using VideoProcessing.Services;
using Yolov5;

namespace VideoProcessing.Handlers
{
    internal class LocalHandler : IDetectHandler<DetectedYolo5Object>
    {
        public Dictionary<string, IYoloDetector> AvailableYoloNets  { get; set; }
        public LocalHandler(Dictionary<string, IYoloDetector> availableYoloNets)
        {
            this.AvailableYoloNets = availableYoloNets;
        }

        public List<DetectedYolo5Object> GetData(Mat image, AppSettings settings)
        {
            return SendLocalData(image, AvailableYoloNets);
        }

        public  List<DetectedYolo5Object> SendLocalData(Mat image, Dictionary<string, IYoloDetector> availableYoloNets)
        {
            float threshold = 0.7f;
            string variation = "nano";
          
            Yolo5Service yolo5Service = new(new Yolo5ServiceOptions()
            {
                AvailableNets = availableYoloNets
            });

            var localResult = GetDetections(variation, threshold, image.ToBitmap(), yolo5Service);
            var data = localResult.Select(o => new DetectedYolo5Object
            {
                ClassId = o.Label.Id,
                Confidence = o.Confidence,
                BoxX = o.Rectangle.X,
                BoxY = o.Rectangle.Y,
                BoxWidth = o.Rectangle.Width,
                BoxHeight = o.Rectangle.Height,
            }).ToList();
            return data;
        }

        public  IEnumerable<YoloDetection> GetDetections(string variation, float? threshold, Image image, IYolo5Service yolo5Service)
        {
            var detections = yolo5Service.Detect(variation, image, threshold);
            return detections;
        }
    }
}
