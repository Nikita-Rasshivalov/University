using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace ClientServer
{

    public class Client
    {
        Socket client; // подключенный клиент
        HTTPHeaders Headers; // распарсенные заголовки

        public Client(Socket c)
        {
            client = c;
            byte[] data = new byte[1024];
            string request = "";
            client.Receive(data); // считываем входящий запрос и записываем его в наш буфер data
            request = Encoding.UTF8.GetString(data); // преобразуем принятые нами байты с помощью кодировки UTF8 в читабельный вид

            if (request == "")
            {
                client.Close();
                return;
            }

            Headers = HTTPHeaders.Parse(request);
            Console.WriteLine($@"[{client.RemoteEndPoint}]
                File: {Headers.File}
                Date: {DateTime.Now}");

            if (Headers.RealPath.IndexOf("..") != -1)
            {
                SendError(404);
                client.Close();
                return;
            }


            if (File.Exists(Headers.RealPath))
                GetSheet(Headers);
            else
                SendError(404);
            client.Close();
        }

        public void SendError(int code)
        {
            string html = $"<html><head><title></title></head><body><h1>Error {code}</h1></body></html>";
            string headers = $"HTTP/1.1 {code} OK\nContent-type: text/html\nContent-Length: {html.Length}\n\n{html}";
            byte[] data = Encoding.UTF8.GetBytes(headers);
            client.Send(data, data.Length, SocketFlags.None);
            client.Close();
        }

        string GetContentType(HTTPHeaders head)
        {
            string result = "";
            string format = HTTPHeaders.FileExtention(Headers.File);
            switch (format)
            {
                //image
                case "gif":
                case "jpeg":
                case "pjpeg":
                case "png":
                case "tiff":
                case "webp":
                    result = $"image/{format}";
                    break;
                case "svg":
                    result = $"image/svg+xml";
                    break;
                case "ico":
                    result = $"image/vnd.microsoft.icon";
                    break;
                case "wbmp":
                    result = $"image/vnd.map.wbmp";
                    break;
                case "jpg":
                    result = $"image/jpeg";
                    break;
                // text
                case "css":
                    result = $"text/css";
                    break;
                case "html":
                    result = $"text/{format}";
                    break;
                case "javascript":
                case "js":
                    result = $"text/javascript";
                    break;
                case "php":
                    result = $"text/html";
                    break;
                case "htm":
                    result = $"text/html";
                    break;
                default:
                    result = "application/unknown";
                    break;
            }
            return result;
        }
        public void GetSheet(HTTPHeaders head) {
            try
            {
                string content_type = GetContentType(head);
                FileStream fs = new FileStream(head.RealPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                string headers = $"HTTP/1.1 200 OK\nContent-type: {content_type}\nContent-Length: {fs.Length}\n\n";
                // OUTPUT HEADERS    
                byte[] data_headers = Encoding.UTF8.GetBytes(headers);
                client.Send(data_headers, data_headers.Length, SocketFlags.None);


                while (fs.Position < fs.Length)
                {
                    byte[] data = new byte[1024];
                    int length = fs.Read(data, 0, data.Length);
                    client.Send(data, data.Length, SocketFlags.None);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Func: GetSheet()    link: {head.RealPath}\nException: {ex}/nMessage: {ex.Message}");
            }
        }
    }
}
