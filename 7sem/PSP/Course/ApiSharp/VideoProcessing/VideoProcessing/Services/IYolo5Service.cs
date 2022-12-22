using System.Collections.Generic;
using System.Drawing;
using Yolov5;

namespace VideoProcessing.Services
{
    public interface IYolo5Service
    {
        IEnumerable<YoloDetection> Detect(string netName, Image image, float? threshold);
    }
}
