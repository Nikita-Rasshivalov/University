using System.Drawing;

namespace Yolov5
{
    /// <summary>
    /// Object prediction.
    /// </summary>
    public class YoloDetection
    {
        public required YoloLabel Label { get; set; }
        public required RectangleF Rectangle { get; set; }
        public required float Confidence { get; set; }
    }
}
