using Histograms;
using System.Drawing;
using System.ServiceModel;

namespace Image2ColorHistogramService
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface IConverter
    {
        [OperationContract]
        ColorHistogram Convert(Bitmap bitmap, Color color);
    }
}