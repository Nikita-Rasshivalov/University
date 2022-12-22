using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Yolov5;

namespace VideoProcessing.Handlers
{
    internal class ServerHandler : IDetectHandler<DetectedYolo5Object>
    {  
        public ServerHandler()
        {

        }

        public List<DetectedYolo5Object> GetData(Mat image, AppSettings settings)
        {
           var result = SendData(image,settings).Result;
           return JsonConvert.DeserializeObject<List<DetectedYolo5Object>>(result);
        }

        public  async Task<string> SendData(Mat image, AppSettings settings)
        {
            var client = new HttpClient();
            var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture))
            {
                { new StreamContent(new MemoryStream(image.ToBytes())), "file", "my.jpg" }
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{settings.ConnectionURL}")
            {
                Content = content
            };
            var responseMessage = await client.SendAsync(requestMessage);
            var output = await responseMessage.Content.ReadAsStringAsync();
            return output;
        }
    }
}
