using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Diagnostics;
using VideoProcessing.Services;

namespace VideoProcessing
{
    internal class Program
    {
    
        static void Main(string[] args)
        {
            IConfiguration builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();
            var settings = new AppSettings();
            builder.Bind(settings);

            const int allowedError = 1;
            const int allowedDelay = 350;
            const int globalRefreshTime = 180000;

            var netLoader = new JsonNetworkLoader($"{AppContext.BaseDirectory}/Networks/networks.json");
            var availableYoloNets = netLoader.LoadNets();

            var globalTimer = new Stopwatch();
            globalTimer.Start();

            new VideoWorker(availableYoloNets,settings).Work(allowedError, allowedDelay, globalRefreshTime, globalTimer);
           
        }
    }
}
