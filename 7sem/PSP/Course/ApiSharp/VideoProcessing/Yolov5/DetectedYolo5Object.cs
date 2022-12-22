namespace Yolov5
{
    public class DetectedYolo5Object
    {
        public required int ClassId { get; set; }
        public required double Confidence { get; set; }
        public required double BoxX { get; set; }
        public required double BoxY { get; set; }
        public required double BoxWidth { get; set; }
        public required double BoxHeight { get; set; }
    }
}
