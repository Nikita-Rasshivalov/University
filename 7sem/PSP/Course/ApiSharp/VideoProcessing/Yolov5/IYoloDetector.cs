using System.Drawing;


namespace Yolov5
{
    public interface IYoloDetector
    {
        List<YoloDetection> Detect(Image image, float? threshold);
    }
}
