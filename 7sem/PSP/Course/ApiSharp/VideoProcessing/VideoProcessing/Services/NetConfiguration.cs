using Yolov5.Models;

namespace VideoProcessing.Services
{
    public class NetConfiguration
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public Yolo5Model Model { get; set; }
    }
}
