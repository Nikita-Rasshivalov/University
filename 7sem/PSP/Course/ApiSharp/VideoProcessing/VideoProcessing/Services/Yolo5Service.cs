using System.Collections.Generic;
using System.Drawing;
using Yolov5;

namespace VideoProcessing.Services
{

    public class Yolo5Service : IYolo5Service
    {
        private readonly Yolo5ServiceOptions _options;

        public Yolo5Service(Yolo5ServiceOptions options)
        {
            _options = options;
        }

        public IEnumerable<YoloDetection> Detect(string netName, Image image, float? threshold)
        {
            var net = _options.AvailableNets[netName];

            return net.Detect(image, threshold);
        }
    }
}
