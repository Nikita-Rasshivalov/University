using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net;
using System.Text;
using Server.Sorting;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace Server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            HttpListener server = new HttpListener();
 
            server.Prefixes.Add("https://127.0.0.1:5003/");
            server.Start(); 

            while (true)
            {
                var context = await server.GetContextAsync();
                ThreadPool.QueueUserWorkItem(async
                    state =>
                    {
                        await ProcessClient(state as HttpListenerContext);
                    }, context);
            }
        }

        private static async Task ProcessClient(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;
           
            var responseText = GetResponseMessage(request, response);
            byte[] buffer = Encoding.UTF8.GetBytes(responseText);
         
            response.ContentLength64 = buffer.Length;
            using Stream output = response.OutputStream;
            
            await output.WriteAsync(buffer);
            await output.FlushAsync();
        }

        private static string GetResponseMessage(HttpListenerRequest request, HttpListenerResponse response)
        {
            var encoding = request.ContentEncoding;
            var reader = new StreamReader(request.InputStream, encoding);
            string body = reader.ReadToEnd();
            if (request.Url.PathAndQuery.Equals("/"))
            {
                return request.HttpMethod switch
                {
                    "GET" => GetMainPage(response),
                    _ => GetNotFoundPage(response),
                };
            }
            else if (request.Url.PathAndQuery.Equals("/sort/insertSort.txt", StringComparison.InvariantCultureIgnoreCase))
            {
                return request.HttpMethod switch
                {
                    "GET" => GetInsertSortPage(response),
                    "POST" => GetInsertSortResponsePage(response, body),
                    _ => GetNotFoundPage(response),
                };
            }

            return GetNotFoundPage(response);
        }

        private static string GetMainPage(HttpListenerResponse response)
        {
            StringBuilder bodyBuilder = new StringBuilder();
            bodyBuilder.Append("<a href='/sort/insertSort.txt'>Insert sort</a>");
            string body = bodyBuilder.ToString();

            ResponseHeaders(response);

            return body;
        }

        private static string GetNotFoundPage(HttpListenerResponse response)
        {
            StringBuilder bodyBuilder = new StringBuilder();
            bodyBuilder.Append("Page not found");
            bodyBuilder.Append("<br/>");
            bodyBuilder.Append("<a href='/'>Go to main page</a>");

            ResponseHeaders(response, responseStatus: 404);

            return bodyBuilder.ToString();
        }

        private static string GetBadPage(HttpListenerResponse response, string message = "Something went wrong")
        {
            StringBuilder bodyBuilder = new StringBuilder();
            bodyBuilder.Append(message);
            bodyBuilder.Append("<br/>");
            bodyBuilder.Append("<a href='/'>Go to main page</a>");
            string body = bodyBuilder.ToString();

            ResponseHeaders(response);

            return body;
        }

        private static string GetInsertSortPage(HttpListenerResponse response)
        {
            StringBuilder bodyBuilder = new StringBuilder();
            bodyBuilder.Append("<form method='post'>");
            bodyBuilder.Append("Input array:");
            bodyBuilder.Append("<br/>");
            bodyBuilder.Append("<input type='text' name='array'>");
            bodyBuilder.Append("<br/>");
            bodyBuilder.Append("<input type='submit' value='Get result'>");
            bodyBuilder.Append("</form>");
            string body = bodyBuilder.ToString();
            ResponseHeaders(response);
            return body;
        }

        private static string GetInsertSortResponsePage(HttpListenerResponse response, string body)
        {
            int[] array;
            var stringArray = body.Split('=')[1].Split("+", StringSplitOptions.RemoveEmptyEntries);

            StringBuilder bodyBuilder = new StringBuilder();
            array = stringArray.Select(item => int.Parse(item)).ToArray();
            var sorter = new InsertSortSolver();
            var sortedArray = array.ToArray();
            sorter.Sort(sortedArray);
            bodyBuilder.Append("Array before sorting:");
            bodyBuilder.Append("\n");
            bodyBuilder.Append(string.Join(",", array));
            bodyBuilder.Append("\n");
            bodyBuilder.Append("Sorted array:");
            bodyBuilder.Append("\n");
            bodyBuilder.Append(string.Join(",", sortedArray));

            ResponseHeaders(response, contentType: "application/octet-stream");

            return bodyBuilder.ToString();
        }

        public static void ResponseHeaders(HttpListenerResponse response, string contentType = "text/html;charset=UTF-8", int responseStatus = 200)
        {
            response.Headers.Set("Content-Type", contentType);
            response.Headers.Set("Connection", "Close");
            response.Headers.Set("Server", "lab6-server");
            response.StatusCode = responseStatus;
        }
    }


}