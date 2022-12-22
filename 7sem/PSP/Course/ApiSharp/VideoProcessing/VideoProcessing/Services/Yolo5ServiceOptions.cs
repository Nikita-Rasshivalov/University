
using System.Collections.Generic;
using Yolov5;

namespace VideoProcessing.Services
{
    public class Yolo5ServiceOptions
    {
        public required Dictionary<string, IYoloDetector> AvailableNets { get; set; }

        public Yolo5Service Yolo5Service
        {
            get => default;
            set
            {
            }
        }
    }

}
