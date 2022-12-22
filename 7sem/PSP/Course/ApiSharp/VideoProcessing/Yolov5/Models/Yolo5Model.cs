

namespace Yolov5.Models
{
    public class Yolo5Model : IYoloModel
    {
        public int Width { get; set; } = 640;
        public int Height { get; set; } = 640;
        public int Dimensions { get; set; } = 7;
        public float Confidence { get; set; } = 0.75f;
        public float MulConfidence { get; set; } = 0.25f;
        public float Overlap { get; set; } = 0.45f;
        public string[] Outputs { get; set; } = new[] { "output0" };

        public List<YoloLabel> Labels { get; set; } = new List<YoloLabel>()
        {
            new YoloLabel { Id = 1, Name = "cucumber" },
            new YoloLabel { Id = 2, Name = "trash" },
        };
    }
}
