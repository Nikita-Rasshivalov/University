using Image2ColorHistogramService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a URI to serve as the base address.  
            Uri baseAddress = new Uri("net.pipe://localhost/Image2ColorHistogramService");

            // Step 2: Create a ServiceHost instance.  
            ServiceHost selfHost = new ServiceHost(typeof(ConverterService), baseAddress);

            try
            {
                // Step 3: Add a service endpoint.  
                selfHost.AddServiceEndpoint(typeof(IConverter), new NetNamedPipeBinding(), "ConverterService");



                // Step 5: Start the service.  
                selfHost.Open();
                Console.WriteLine("The service is ready.");

                // Close the ServiceHost to stop the service.  
                Console.WriteLine("Press <Enter> to terminate the service.");
                Console.WriteLine();
                Console.ReadLine();
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }
    }
}