using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Yolov5.Models;
using Yolov5;

namespace VideoProcessing.Services
{
    public class JsonNetworkLoader : INetworkLoader
    {
        private readonly string _jsonPath;

        public JsonNetworkLoader(string jsonPath)
        {
            _jsonPath = jsonPath;
        }

        public Dictionary<string, IYoloDetector> LoadNets()
        {
            List<NetConfiguration>? nets = null;
            try
            {
                nets = JsonConvert.DeserializeObject<List<NetConfiguration>>(File.ReadAllText(_jsonPath));
                if (nets == null)
                {
                    new InvalidOperationException("Couldn't load nets configuration from JSON file");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Couldn't load nets configuration from JSON file", ex);
            }

            var availableYoloNets = nets.Select(nc =>
            new KeyValuePair<string, IYoloDetector>(nc.Name, new YoloDetector<IYoloModel>(
                nc.Model,
                Path.IsPathFullyQualified(nc.Path) ? nc.Path : $"{AppContext.BaseDirectory}/{nc.Path}"))).ToDictionary(p => p.Key, p => p.Value);

            return availableYoloNets;
        }
    }
}
