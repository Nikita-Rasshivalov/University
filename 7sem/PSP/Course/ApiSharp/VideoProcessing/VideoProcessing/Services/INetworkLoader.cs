using System.Collections.Generic;
using Yolov5;

namespace VideoProcessing.Services
{
    public interface INetworkLoader
    {
        Dictionary<string, IYoloDetector> LoadNets();
    }
}
