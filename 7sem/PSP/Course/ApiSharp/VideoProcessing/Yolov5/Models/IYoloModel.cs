
namespace Yolov5.Models
{
    /// <summary>
    /// Model descriptor.
    /// </summary>
    public interface IYoloModel
    {
        int Width { get; set; }
        int Height { get; set; }
        int Dimensions { get; set; }
        float Confidence { get; set; }
        float MulConfidence { get; set; }
        float Overlap { get; set; }

        string[] Outputs { get; set; }
        List<YoloLabel> Labels { get; set; }
    }
}
