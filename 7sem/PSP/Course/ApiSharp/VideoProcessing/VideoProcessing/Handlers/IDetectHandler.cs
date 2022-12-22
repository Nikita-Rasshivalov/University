using OpenCvSharp;
using System.Collections.Generic;

namespace VideoProcessing.Handlers
{
    internal interface IDetectHandler<T>
    {
        public List<T> GetData(Mat image, AppSettings settings);
    }
}
